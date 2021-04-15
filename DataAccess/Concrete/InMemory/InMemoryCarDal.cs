using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryCarDal : ICarDal
    {
        List<Car> _car;
        public InMemoryCarDal()
        {
            _car = new List<Car>
           {
               new Car{Id=1, BrandId=1 , ColorId=2, DailyPrice=300, ModelYear=2015, Description="2015 model spor araba" },
               new Car{Id=2, BrandId=2 , ColorId=2, DailyPrice=350, ModelYear=2016, Description="2015 model spor araba" },
               new Car{Id=3, BrandId=2 , ColorId=3, DailyPrice=400, ModelYear=2020, Description="2015 model spor araba" },
               new Car{Id=4, BrandId=3 , ColorId=4, DailyPrice=250, ModelYear=2013, Description="2015 model ticari araba" },

           };   
        }

        public void Add(Car car)
        {
            _car.Add(car);
        }

        public void Delete(Car car)
        {
            Car CarToDelete;
            CarToDelete = _car.SingleOrDefault(c => c.Id == car.Id);
            _car.Remove(CarToDelete);

        }

        public Car Get(Expression<Func<Car, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<Car> GetAll()
        {
            return _car;
        }

        public List<Car> GetAll(Expression<Func<Car, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public List<Car> GetById(int Id)
        {
            return _car.Where(c => c.Id == Id).ToList();
        }

        public List<CarDetailDto> GetCarDetails()
        {
            throw new NotImplementedException();
        }

        public void Update(Car car)
        {
            Car CarToUpdate = _car.SingleOrDefault(c => c.Id == car.Id);
            CarToUpdate.BrandId = car.BrandId;
            CarToUpdate.ColorId = car.ColorId;
            CarToUpdate.DailyPrice = car.DailyPrice;
            CarToUpdate.ModelYear = car.ModelYear;
        }
    }
}
