using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            //ColorTest();

            CarTest();

            //UserTest();

            //CustomerTest();

        }

        private static void CustomerTest()
        {
            Console.WriteLine("----müşteri test-------");

            CustomerManager customerManager = new CustomerManager(new EfCustomerDal());

            customerManager.Add(new Customer { UserId = 1, CompanyName = "Nisan Fırsatları" });

            var result = customerManager.GetAll();
            foreach (var customer in result.Data)
            {
                Console.WriteLine(customer.CompanyName);
            }
        }

        private static void UserTest()
        {
            Console.WriteLine("----Kullanıcı test-------");

            UserManager userManager = new UserManager(new EfUserDal());
            userManager.Add(new User { FirstName = "Enes", LastName = "Beyaz", Email = "enesbeyaaz@gmail.com", Password = "eb1234" });
            var result = userManager.GetAll();
            foreach (var user in result.Data)
            {
                Console.WriteLine(user.UserId + "  " + user.FirstName);

            }
        }

        private static void CarTest()
        {
            Console.WriteLine("Car test----------");

            CarManager carManager = new CarManager(new EfCarDal());

           // carManager.Add(new Car { BrandId = 1, ColorId = 1, ModelYear = 2009, DailyPrice = 300, Description = "Ticari Araba" });

            var result = carManager.GetAll();
            if (result.Success == true)
            {
                foreach (var car in result.Data)
                {
                    Console.WriteLine(car.DailyPrice + " " + car.ModelYear);
                }


            }
            else
            {
                Console.WriteLine(result.Message);
            }
        }

        private static void ColorTest()
        {
            Console.WriteLine("-----Color Test -----------");
            ColorManager colorManager = new ColorManager(new EfColorDal());

            Console.WriteLine("-------GetAll-------");
            var result= colorManager.GetAll();
            foreach (var color in result.Data)
            {
                Console.WriteLine(color.ColorId + " " + color.ColorName);
            }
            colorManager.Add(new Color { ColorName = "Kirmizi" });

            foreach (var color in result.Data)
            {
                Console.WriteLine(color.ColorName);
            }
            Console.WriteLine("-------Delete-------");

            colorManager.Delete(new Color { ColorId = 2 });
            foreach (var color in result.Data)
            {
                Console.WriteLine(color.ColorName + "" + color.ColorId);
            }

            colorManager.Update(new Color { ColorId = 1, ColorName = "KirmiziUpdate" });
            foreach (var color in result.Data)
            {
                Console.WriteLine(color.ColorName + "" + color.ColorId);
            }
        }
    }
}
