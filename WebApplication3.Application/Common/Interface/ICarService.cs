using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.Application.DTOs;
using WebApplication3.Domain.Entities;

namespace WebApplication3.Application.Common.Interface
{
    public interface ICarService
    {
        Task<Car> AddCarDetails(AddCarDTO car);

        Task<Car> GetCar(int id);

        Task<List<AddCarDTO>> GetAllCars();
         
        Task<Car> UpdateCarAsync(int id, AddCarDTO car);

        Task DeleteCarAsync(int id);

        Task<Car> CountNumberOfRents(int id, NumberOfRentsDTO numberOfRents);
    }
}
