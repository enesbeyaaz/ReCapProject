using Business.Abstract;
using Business.Constants;
using Core.Utilities.Business;
using Core.Utilities.Helpers.FileHelper;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        ICarImageDal _carImageDal;
       

        public CarImageManager(ICarImageDal carImageDal)
        {
            _carImageDal = carImageDal;
        }

        public IResult Add(IFormFile file ,CarImage carImage)
        {
            IResult result = BusinessRules.Run(CheckIfImageLimitExceded(carImage.CarId));
            if (result != null)
            {
                return result;
            }

            var imageResult = FileHelper.Upload(file);
            if (!imageResult.Success)
            {
                return new ErrorResult(imageResult.Message);

            }
            carImage.ImagePath = imageResult.Message;


            _carImageDal.Add(carImage);
            return new SuccessResult(Messages.CarImageAdded);
        }

        public IResult Delete( CarImage carImage)
        {
            var image = _carImageDal.Get(c => c.Id == carImage.Id);
            if (image == null)
            {
                return new ErrorResult(Messages.ImageNotFound);
            }

            FileHelper.Delete(image.ImagePath);
            _carImageDal.Delete(carImage);
           
            return new SuccessResult(Messages.CarImageDeleted);

        }

        public IDataResult<CarImage> Get(int carId)
        {
            var result = _carImageDal.Get(c => c.CarId == carId);
       
            return new SuccessDataResult<CarImage>(result);
        }

        public IDataResult<List<CarImage>> GetAll()
        {
            var result = _carImageDal.GetAll();
            return new SuccessDataResult<List<CarImage>>(result,Messages.CarImagesListed);
        }

        public IDataResult<List<CarImage>> GetImagesByCarId(int carId)
        {
            BusinessRules.Run(CheckIfCarImageNull(carId));
            var result = _carImageDal.GetAll(c => c.CarId == carId).ToList();
            return new SuccessDataResult<List<CarImage>>(result);
        }

        public IResult Update(IFormFile file,CarImage carImage)
        { var isImage = _carImageDal.Get(c => c.Id == carImage.Id);
            if (isImage==null)
            {
                return new ErrorResult(Messages.ImageNotFound);

            }
            var updateFile = FileHelper.Update(file, isImage.ImagePath);
            if (!updateFile.Success)
            {
                return new ErrorResult(updateFile.Message);
            }

            carImage.ImagePath = updateFile.Message;
            _carImageDal.Update(carImage);
            return new SuccessResult(Messages.CarImageUpdated);
        }

        private IResult CheckIfImageLimitExceded(int carId)
        {
            var result = _carImageDal.GetAll(c => c.CarId == carId).Count;
            if (result>5)
            {
                return new ErrorResult(Messages.ImageLimitExceded);
            }
            return new SuccessResult();

        }

        private IResult CheckIfCarImageNull(int id)
        {
            try
            {
                string imagePath = @"/images/default.jpg";
                var result = _carImageDal.GetAll(c => c.Id == id).Any();

                if (!result)
                {
                    List<CarImage> carimage = new List<CarImage>();
                    carimage.Add(new CarImage { CarId = id, ImagePath = imagePath, Date = DateTime.Now });
                    return new SuccessDataResult<List<CarImage>>(carimage);
                }
            }
            catch (Exception exception)
            {


                return new ErrorDataResult<List<CarImage>>(exception.Message);
            }
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(c => c.CarId == id).ToList());


        }
    }
}
