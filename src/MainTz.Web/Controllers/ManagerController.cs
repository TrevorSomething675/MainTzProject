using MainTz.Web.ViewModels.CarViewModels;
using Microsoft.AspNetCore.Authorization;
using MainTz.Application.Services;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using MainTz.Application.Models.CarEntities;

namespace MainTz.Web.Controllers
{
    [Authorize(Roles = "Manager")]
    public class ManagerController : Controller
    {
        private readonly ICarService _carService;
        private readonly IMapper _mapper;

        public ManagerController(ICarService carService, IMapper mapper)
        {
            _mapper = mapper;
            _carService = carService;
        }
        public async Task<IActionResult> Index()
        {
            var carsDomainEntity = await _carService.GetCars();
            var carsResponse = _mapper.Map<CarResponse>(carsDomainEntity);
            return View(carsResponse);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCar(CarRequest carRequest)
        {
            var carDomainEntity = _mapper.Map<CarDomainEntity>(carRequest);
            var result = await _carService.CreateCar(carDomainEntity);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteCar(CarRequest carRequest)
        {
            var carDomainEntity = _mapper.Map<CarDomainEntity>(carRequest);
            var result = await _carService.DeleteCar(carDomainEntity);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCar(CarRequest carRequest)
        {
            var carDomainEntity = _mapper.Map<CarDomainEntity>(carRequest);
            var result = await _carService.UpdateCar(carDomainEntity);
            return RedirectToAction("Index");
        }
    }
}
