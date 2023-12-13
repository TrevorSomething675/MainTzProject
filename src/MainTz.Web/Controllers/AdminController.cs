using Microsoft.AspNetCore.Authorization;
using MainTz.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace MainTz.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        //private readonly IUserService _userService;

        //public AdminController(IUserService userService)
        //{
        //    _userService = userService;
        //}

        //[HttpGet]
        //public async Task<IActionResult> Index()
        //{
        //    var model = await _userService.GetUsersAsync();

        //    return View(model);
        //}

        //public async Task<IActionResult> CreateUser(UserDto userDto)
        //{
        //    var result = await _userService.CreateAsync(userDto);
        //    return RedirectToAction("Index");
        //}

        //[HttpPost]
        //public async Task<IActionResult> DeleteUser(UserDto userDto)
        //{
        //    var result = await _userService.DeleteAsync(userDto);
        //    return RedirectToAction("Index");
        //}

        //public async Task<IActionResult> UpdateUser(UserDto userDto)
        //{
        //    var result = await _userService.UpdateAsync(userDto);
        //    return RedirectToAction("Index");
        //}
    }
}
