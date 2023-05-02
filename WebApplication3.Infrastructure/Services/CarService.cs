
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

        public async Task<List<RequestCarDTO>> GetAllCars()
        {
            var cars = await _dbContext.Car.ToListAsync();
            var carDTOs = new List<RequestCarDTO>();
            foreach (var car in cars)
            {
                var createdByUser = await _dbContext.Users.FindAsync(car.CreatedBy);
                carDTOs.Add(new RequestCarDTO
                {
                    CarId = car.CarId,
                    CarName = car.CarName,
                    Image = car.Image,
                    Brand = car.Brand,
                    Price = car.Price,
                    Condition = car.Condition,
                    Description = car.Description,
                    NumberOfRents = (int)car.NumberOfRents,
                    CarStatus = car.CarStatus,
                    CreatedBy = car.CreatedBy,
                    CreatedByUser = createdByUser.UserName

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

        public async Task<Car> CountNumberOfRents(int id, NumberOfRentsDTO numberOfRents)
        {
            var car = await _dbContext.Car.FindAsync(id);
            if (car == null)
            {
                throw new Exception("Car not found");
            }
            int carID = id;
            string status = "Completed";

            int numberOfCompletedRents = _dbContext.Request.Count(r => r.CarID == carID && r.status == status);

            car.NumberOfRents = numberOfCompletedRents;
            await _dbContext.SaveChangesAsync();

            return car;
        }

        //public async Task<List<CarSalesDTO>> GetCarSales()
        //{
        //    var cars = await _dbContext.Car.ToListAsync();
        //    var carSalesDTOs = new List<CarSalesDTO>();
        //    foreach (var car in cars)
        //    {
        //        //var requests = await _dbContext.Request
        //        //    .Where(r => r.CarID == car.CarId && r.status == "Completed")
        //        //    .ToListAsync();

        //        var carSales = await _dbContext.Request
        //            .Where(r => r.CarID == car.CarId && r.status == "Completed")
        //            .Join(_dbContext.Payment,
        //                r => r.RequestId,
        //                p => p.RequestsId,
        //                (r, p) => new { Request = r, Payment = p })
        //            .GroupBy(rp => rp.Request.CarID)
        //            .Select(g => new { CarID = g.Key, TotalSales = g.Sum(rp => rp.Payment.Amount) })
        //            .FirstOrDefaultAsync();

        //        var customers = await _dbContext.Users
        //            .Join(_dbContext.Request,
        //                u => u.Id,
        //                r => r.UserId,

        //                (u, r) => new { Users = u, Request = r })
        //            .Where(r=>r.Request.CarID ==car.CarId && r.Request.status == "Completed")

        //            .Select(x => x.Users.UserName)
        //            .ToListAsync();

        //        carSalesDTOs.Add(new CarSalesDTO
        //        {
        //            CarId = car.CarId,
        //            CarName = car.CarName,
        //            CarBrand =car.Brand,
        //            CarCondition =car.Condition,
        //            CarPrice=car.Price,
        //            Customers = customers,
        //            TotalSales = (float)(carSales?.TotalSales ?? 0)


        //        });
        //    }
        //    return carSalesDTOs;
        //}

        public async Task<List<CarSalesDTO>> GetCarSales(DateTime? startDate, DateTime? endDate)
        {
            var cars = await _dbContext.Car.ToListAsync();
            var carSalesDTOs = new List<CarSalesDTO>();
            foreach (var car in cars)
            {
                var carSalesQuery = _dbContext.Request
                    .Where(r => r.CarID == car.CarId && r.status == "Completed");

                if (startDate.HasValue && endDate.HasValue)
                {
                    carSalesQuery = carSalesQuery
                        .Where(r => r.RequestedDate >= startDate && r.RequestedDate <= endDate);
                }

                var carSales = await carSalesQuery
                    .Join(_dbContext.Payment,
                        r => r.RequestId,
                        p => p.RequestsId,
                        (r, p) => new { Request = r, Payment = p })
                    .GroupBy(rp => rp.Request.CarID)
                    .Select(g => new { CarID = g.Key, TotalSales = g.Sum(rp => rp.Payment.Amount) })
                    .FirstOrDefaultAsync();

                var customersQuery = _dbContext.Users
                    .Join(_dbContext.Request,
                        u => u.Id,
                        r => r.UserId,
                        (u, r) => new { Users = u, Request = r })
                    .Where(r => r.Request.CarID == car.CarId && r.Request.status == "Completed");

                if (startDate.HasValue && endDate.HasValue)
                {
                    customersQuery = customersQuery
                        .Where(r => r.Request.RequestedDate >= startDate && r.Request.RequestedDate <= endDate);
                }

                var customers = await customersQuery
                    .Select(x => x.Users.UserName)
                    .ToListAsync();

                carSalesDTOs.Add(new CarSalesDTO
                {
                    CarId = car.CarId,
                    CarName = car.CarName,
                    CarBrand = car.Brand,
                    CarCondition = car.Condition,
                    CarPrice = car.Price,
                    Customers = customers,
                    TotalSales = (float)(carSales?.TotalSales ?? 0)
                });
            }
            return carSalesDTOs;
        }



    }
}
