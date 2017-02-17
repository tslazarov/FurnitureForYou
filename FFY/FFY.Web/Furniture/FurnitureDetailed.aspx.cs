﻿using FFY.MVP.Furniture.FurnitureDetailed;
using FFY.Order;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebFormsMvp;
using WebFormsMvp.Web;

namespace FFY.Web.Furniture
{
    [PresenterBinding(typeof(FurnitureDetailedPresenter))]
    public partial class FurnitureDetailed : MvpPage<FurnitureDetailedViewModel>, IFurnitureDetailedView
    {
        private string username;
        public event EventHandler<FurnitureDetailedEventArgs> GettingProductById;

        protected void Page_Load(object sender, EventArgs e)
        {
            var productIdParameter = this.Page.RouteData.Values["productId"].ToString();

            int productId;

            if(!(int.TryParse(productIdParameter, out productId)))
            {
                this.Server.Transfer("~/Errors/PageNotFound.aspx");
            }

            this.GettingProductById?.Invoke(this, new FurnitureDetailedEventArgs(productId));

            if(this.Model.Product == null)
            {
                this.Server.Transfer("~/Errors/PageNotFound.aspx");
            }

            this.username = this.User.Identity.GetUserName();
        }

        protected void add_Click(object sender, EventArgs e)
        {
            //TODO: Refactor
            var cart = this.Session[string.Format("cart-{0}", username)] as SessionShoppingCart;

            cart.ShoppingCart.Add(2, this.Model.Product.Id);

            // this.Session[string.Format("cart-{0}", username)] = cart;
        }
    }
}