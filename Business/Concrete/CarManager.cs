using Business.Abstract;
using Entities.Concrete;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Results;
using Business.Constants;

namespace Business.Concrete
{
   public class CarManager : ICarService
    {
        ICarDal _carDal;

        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }

        public IResult Add(Car car)
        {
            if (car.DailyPrice>0)
            {
                _carDal.Add(car);
                return new SuccessResult(Messages.CarAdded);
            }
            else
            {
                return new ErrorResult ("Daily Price 0 dan büyük olmalı.");
            }
        }

        public IResult Delete(int id)
        {
            _carDal.Delete(new Car {Id = id });

            return new SuccessResult(Messages.CarDeleted);
        }

        public IDataResult< List<Car>> GetAll()
        {
            if (DateTime.Now.Hour==19)
            {
                return new ErrorDataResult<List<Car>>(Messages.MaintenanceTime);
            }
            else
            {
                return new SuccessDataResult<List<Car>>(_carDal.GetAll(), Messages.CarsListed);
            }
           
            
        }

        public IDataResult<Car> GetById(int id)
        {
            return new SuccessDataResult<Car>(_carDal.Get(c => c.Id == id));
        }

        public IDataResult<List<Car>> GetCarsByBrandId(int id)
        {
            return new SuccessDataResult<List<Car>> (_carDal.GetAll(c => c.BrandId == id));
        }

        public IDataResult<List<Car>> GetCarsByColorId(int id)
        {
            return new SuccessDataResult<List<Car>>( _carDal.GetAll(c => c.ColorId == id));
        }

        public IResult Update(Car car)
        {
            _carDal.Update(car);
            return new SuccessResult(Messages.CarUpdated);
        }
    }
}
