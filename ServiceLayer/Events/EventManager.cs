using DomainLayer.Models;
using ServiceLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.Events
{
    public static class EventManager
    {
        public static event ShowBrandEventHandler ShowBrand;
        public static event ShowProductEventHandler ShowProduct;
        public static event ShowUserEventHandler ShowUser;

        public static void OnBrandShowing(Brand brand)
        {
            BrandVM brandVM = new BrandVM(brand, true);
            if (ShowBrand != null)
            {
                ShowBrand.Invoke(brandVM);
            }
        }

        public static void OnProductShowing(Product product)
        {
            ProductVM productVM = new ProductVM(product, true);
            if (ShowProduct != null)
            {
                ShowProduct.Invoke(productVM);
            }
        }

        public static void OnUserShowing(User user)
        {
            UserVM userVM = new UserVM(user, true);
            if (ShowUser != null)
            {
                ShowUser.Invoke(userVM);
            }
        }
    }
}
