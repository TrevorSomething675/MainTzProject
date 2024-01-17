﻿namespace MainTz.Application.Services
{
    public interface IMinioService
    {
        public Task<List<string>> GetBucketsAsync();
        public Task<bool> CreateBucketAsync(string bucketName);

        public Task<string> GetObjectAsync(string path);
        //public Task<bool> AddObjectToBucketAsync(string path);
        //public Task<bool> RemoveObjectInBucketAsync(string path);
    }
}