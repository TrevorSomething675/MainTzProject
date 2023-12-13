using MainTz.Application.Models.UserEntities;
using MainTz.Application.Services;
using MainTz.Extensions.Models;
using Microsoft.AspNetCore.Mvc;
using MainTz.Web.ViewModels;
using AutoMapper;

namespace MainTz.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly IUserService _usersService;
        private readonly ILogger<AuthController> _logger;
        public AuthController(IUserService usersService, ITokenService tokenService, IMapper mapper, ILogger<AuthController> logger)
        {
            _tokenService = tokenService;
            _usersService = usersService;
            _mapper = mapper;
            _logger = logger;
        }
        /// <summary>
        /// Получение токена из сервиса, путём отправки запроса с ролью
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IResult> GetToken([FromBody] string role) // отправка сообщения и получение токена из AuthApi
        {
            var tokensModel = new TokensModel
            {
                RefreshToken = _tokenService.CreateRefreshToken(role),
                AccessToken = _tokenService.CreateAccessToken(role),
                Role = role
            };

            return Results.Json(tokensModel);
        }
        /// <summary>
        /// Получение разметки с формой логина
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            _logger.LogDebug("Log Debug");
            _logger.LogTrace("Log Trace");
            return View();
        }
        /// <summary>
        /// Отправка формы логина, она возрвращает json, js код в FormScripts.js 
        /// получает её и в зависимости от роли перенаправляет пользователя на страницу роли.
        /// </summary>
        /// <param name="userDto">Модель, которая приходит с фронта</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IResult> Login(LoginFormRequest loginFormRequest)
        {
            if (!ModelState.IsValid)
                return Results.BadRequest("Wrong Data");
            try
            {
                var userDomain = _mapper.Map<UserDomainEntity>(loginFormRequest);
                var user = await _usersService.GetUserByNameAsync(loginFormRequest.Name);

                if (user == null)
                    return Results.BadRequest("Пользователя не существует");
                if (loginFormRequest.Password != user.Password)
                    return Results.BadRequest("Неверный пароль");

                //var tokens = await GetToken(user.Role.ToString());

                return Results.Json("Jija"/*tokens*/);
            }
            catch (Exception ex)
            {
                return Results.BadRequest($"{ex.Message}");
            }
        }
        /// <summary>
        /// Получение разметки с формой регистрации
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }
        /// <summary>
        /// Регистрация пользователя, по умолчанию роль всегда 'User', 
        /// мы регистрируем пользователя, а после вызываем метод Login, результат обрабатывает RegisterFormScript.js
        /// и перенаправляет пользователя на нужную страницу после регистрации.
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IResult> Register(RegisterFormRequest registerFormRequest)
        {
            if (!ModelState.IsValid)
                return Results.BadRequest("Wrong Data");

            var user = await _usersService.GetUserByNameAsync(registerFormRequest.Name);

            if (user != null)
                return Results.BadRequest("Пользователь уже существует");

            registerFormRequest.Role = "User";
            var userDomainEntity = _mapper.Map<UserDomainEntity>(registerFormRequest);
            await _usersService.CreateAsync(userDomainEntity);
            var result = await Login(registerFormRequest);

            return result;
        }
    }
}
