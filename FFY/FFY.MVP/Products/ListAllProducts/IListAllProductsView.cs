﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFormsMvp;

namespace FFY.MVP.Products.ListAllProducts
{
    public interface IListAllProductsView : IView<ListAllProductsViewModel>
    {
        event EventHandler<ListAllProductsEventArgs> ListingAllProducts;
    }
}
