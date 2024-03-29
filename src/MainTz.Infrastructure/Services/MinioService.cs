﻿using MainTz.Application.Models.OptionsModels;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MainTz.Application.Services;
using MainTz.Application.Models;
using Minio.DataModel.Args;
using Minio;
using System.Data;

namespace MainTz.Infrastructure.Services
{
    public class MinioService : IMinioService
    {
        private readonly IMinioClientFactory _minioClientFactory;
        private readonly MinioOptions _minioOptions;
        private readonly ILogger<MinioService> _logger;
        public MinioService(IMinioClientFactory minioClientFactory, ILogger<MinioService> logger, IOptions<MinioOptions> minioOptions)
        {
            _minioClientFactory = minioClientFactory;
            _minioOptions = minioOptions.Value;
            _logger = logger;
        }
        //public async Task<bool> AddObjectToBucketAsync(string path, FileStream file)
        //{
        //    string bucketName;
        //    string objectName;
        //    GetBucketNameAndFileNameByPath(path, out bucketName, out objectName);
        //    try
        //    {
        //        var args = new PutObjectArgs()
        //            .WithBucket(bucketName)
        //            .WithObject(objectName);
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex.Message);
        //        return false;
        //    }
        //}
        public async Task<string> CreateObjectAsync(Image image)
        {
            using(var client = _minioClientFactory.CreateClient())
            {
                byte[] bytes = Convert.FromBase64String(NormalizedBased64String(image.FileBase64String));

                var args = new PutObjectArgs()
                    .WithBucket(_minioOptions.DefaultImageBucketName)
                    .WithObject(image.Name)
                    .WithObjectSize(bytes.LongLength)
                    .WithStreamData(new MemoryStream(bytes))
                    .WithContentType("application/octet-stream");

                await client.PutObjectAsync(args).ConfigureAwait(false);

                var resultPath = $"{_minioOptions.DefaultImageBucketName}/{image.Name}";

                return resultPath;
            }
        }
        public async Task<string> GetObjectAsync(string path)
        {
            string bucketName = Path.GetDirectoryName(path);
            string objectName = Path.GetFileName(path);
            byte[] objectBytes = null;
            try
            {
                using (var client = _minioClientFactory.CreateClient())
                {
                    var args = new GetObjectArgs()
                        .WithBucket(bucketName)
                        .WithObject(objectName)
                        .WithFile(objectName)
                        .WithCallbackStream(async (stream) => // колбек стрим, который конвертирует пикчу в массив байт
                        {
                            await using (var ms = new MemoryStream())
                            {
                                stream.CopyTo(ms);
                                objectBytes = ms.ToArray();
                            }
                        });
                    await client.GetObjectAsync(args);
                    return Convert.ToBase64String(objectBytes);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<string>> GetBucketsAsync()
        {
            try
            {
                using (var client = _minioClientFactory.CreateClient())
                {
                    var buckets = await client.ListBucketsAsync();
                    var bucketNames = buckets.Buckets.Select(b => b.Name).ToList();
                    return bucketNames;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Не удалось получить список бакетов {ex.Message}", ex.Message);
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> CreateBucketAsync(string bucketName)
        {
            try
            {
                using(var client = _minioClientFactory.CreateClient())
                {
                    var beArgs = new BucketExistsArgs().WithBucket(bucketName);
                    if (!await client.BucketExistsAsync(beArgs).ConfigureAwait(false))
                    {
                        var mb = new MakeBucketArgs().WithBucket(bucketName);
                        await client.MakeBucketAsync(mb).ConfigureAwait(false);
                    }
                }
                return true;
            }
            catch(Exception ex)
            {
                _logger.LogError("Ошибка при создании бакета {ex.Message}", ex.Message);
                return false;
            }
        }
        //public async Task<bool> RemoveObjectInBucketAsync(string path)
        //{
        //    string bucketName;
        //    string objectName;
        //    try
        //    {
        //        GetBucketNameAndFileNameByPath(path, out bucketName, out objectName);
        //        using (var client = _minioClientFactory.CreateClient())
        //        {
        //            var args = new RemoveObjectArgs()
        //                .WithBucket(bucketName)
        //                .WithObject(objectName);
        //            await client.RemoveObjectAsync(args).ConfigureAwait(false);
                    
        //            return true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex.Message);
        //        throw new Exception(ex.Message);
        //    }
        //}
        private void GetBucketNameAndFileNameByPath(string filePath, out string bucketName, out string fileName)
        {
            fileName = Path.GetFileName(filePath);
            bucketName = Path.GetDirectoryName(filePath);
        }
        private string NormalizedBased64String(string base64String)
        {
            var deleteString = "data:image/jpeg;base64,";
            var result = base64String.Replace(deleteString, String.Empty);

            return result;
        }
    }
}