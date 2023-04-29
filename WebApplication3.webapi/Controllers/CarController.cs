using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Application.Common.Interface;
using WebApplication3.Application.DTOs;
using WebApplication3.Domain.Entities;
using WebApplication3.Infrastructure.Services;

namespace WebApplication3.webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController:ControllerBase
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpPost]
        public async Task<Car> AddCarDetails(AddCarDTO car) 
        {
            var data = await _carService.AddCarDetails(car);
             return data;
        }

        [HttpGet("{id}")]
        public async Task<Car> GetCar(int id) { 
            var car = await _carService.GetCar(id);
            if (car == null)
            {
                return null;
            }
            return car;
        }

        [HttpGet]
        public async Task<ActionResult<List<Car>>> GetAllCars()
        {
            var cars = await _carService.GetAllCars();
            return Ok(cars);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCar(int id, [FromBody] AddCarDTO carDto)
        {
            if (carDto == null)
            {
                return BadRequest();
            }

            try
            {
                var updatedCar = await _carService.UpdateCarAsync(id, carDto);
                return Ok(updatedCar);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            try
            {
                await _carService.DeleteCarAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }



    }
}
