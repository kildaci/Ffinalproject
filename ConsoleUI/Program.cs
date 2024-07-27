using Business.Concrete;
using System;
using DataAccess.Concrete.InMemory;
using DataAccess.Concrete.EntityFramework;
using Business.Abstract;

namespace ConsoleUI
{
    class Program 
    {

        static void Main(string[] args)
        {
            ProductTest();
           //CategoryTest();
        }

        private static void CategoryTest()
        {
            CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
            foreach (var arg in categoryManager.GetAll().Data)
            {
                Console.WriteLine(arg.CategoryName);

            }
        }

        private static void ProductTest()
        {
            ProductManager productManager = new ProductManager(new EfProductDal(),new CategoryManager(new EfCategoryDal()));

            foreach (var item in productManager.GetProductDetailDtos().Data)
            {
                Console.WriteLine(item.ProductName + " / "+item.CategoryName);


            }
        }
    }

}





