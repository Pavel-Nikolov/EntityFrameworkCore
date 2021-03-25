using ServiceLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.Events
{
    public delegate void ShowBrandEventHandler(BrandVM brandVM);

    public delegate void ShowProductEventHandler(ProductVM productVM);

    public delegate void ShowUserEventHandler(UserVM userVM);
}
