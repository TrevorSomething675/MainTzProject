using MainTz.Web.ViewModels.CarViewModels;
using MainTz.Application.Services;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace MainTz.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly ICarService _carService;
        private readonly IMapper _mapper;
        public UserController(ICarService carService, IMapper mapper)
        {
            _carService = carService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var carsDomainModels = await _carService.GetCars();
            var carsResponse = _mapper.Map<CarResponse>(carsDomainModels);
            return View(carsResponse);
        }
    }
}