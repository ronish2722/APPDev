
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.Application.Common.Interface;
using WebApplication3.Application.DTOs;
using WebApplication3.Domain.Entities;

namespace WebApplication3.Infrastructure.Services
{
    public class CarService:ICarService
    {
        private readonly IApplicationDBContext _dbContext;
        

        public CarService(IApplicationDBContext dBContext)
        {
            _dbContext = dBContext;
            
        }
        public async Task<Car> AddCarDetails(AddCarDTO car)
        {
            var carDetails = new Car()
            {
                CarName = car.CarName,
                Image = car.Image,
                Brand = car.Brand,
                Price =car.Price,
                Condition = car.Condition,
                Description = car.Description,
                NumberOfRents = car.NumberOfRents,
                CreatedBy = car.CreatedBy,
            };
            await _dbContext.Car.AddAsync(carDetails);
            await _dbContext.SaveChangesAsync();
            return carDetails;
        }

        public async Task<Car> GetCar(int id) { 
            var car = await _dbContext.Car.FindAsync(id);
            return car;
        }

        public async Task<List<AddCarDTO>> GetAllCars()
        {
            var cars = await _dbContext.Car.ToListAsync();
            var carDTOs = new List<AddCarDTO>();
            foreach (var car in cars)
            {
                carDTOs.Add(new AddCarDTO
                {
                    CarName = car.CarName,
                    Image = car.Image,
                    Brand = car.Brand,
                    Price = car.Price,
                    Condition = car.Condition,
                    Description = car.Description,
                    NumberOfRents = (int)car.NumberOfRents,
                    CreatedBy = car.CreatedBy,

                    // Map other properties as needed
                });
            }
            return carDTOs;
        }

        public async Task<Car> UpdateCarAsync(int id, AddCarDTO carDto)
        {
            var car = await _dbContext.Car.FindAsync(id);
            if (car == null)
            {
                throw new Exception("Car not found");
            }
            car.CarName = carDto.CarName;
            car.Image = carDto.Image;   
            car.Brand = carDto.Brand;
            car.Price = carDto.Price;
            car.Condition = carDto.Condition;
            car.Description = carDto.Description;
            car.NumberOfRents = carDto.NumberOfRents;
            // Update other properties as needed
            await _dbContext.SaveChangesAsync();
            return car;
        }

        public async Task DeleteCarAsync(int id)
        {
            var car = await _dbContext.Car.FindAsync(id);
            if (car == null)
            {
                throw new Exception("Car not found");
            }
            _dbContext.Car.Remove(car);
            await _dbContext.SaveChangesAsync();
        }




    }
}
