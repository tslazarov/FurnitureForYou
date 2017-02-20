﻿using FFY.MVP.Furniture.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebFormsMvp;
using WebFormsMvp.Web;

namespace FFY.Web.Furniture
{
    [PresenterBinding(typeof(ProductsPresenter))]
    public partial class FurnitureList : MvpPage<ProductsViewModel>, IProductsView
    {
        private const int DefaultFrom = 0;
        private const int DefaultTo = 100000;

        public event EventHandler<ProductsEventArgs> ListingProducts;
        public event EventHandler<QueryEventArgs> BuildingQuery;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.CheckPageParameters();

            this.Products.DataSource = this.Model.Products;
            this.Products.DataBind();
        }

        private void CheckPageParameters()
        {
            string path = HttpContext.Current.Request.Url.AbsolutePath;

            string room = this.RouteData.Values["room"] != null ?
                this.RouteData.Values["room"].ToString() : null;

            string category = this.RouteData.Values["category"] != null ?
                this.RouteData.Values["category"].ToString() : null;

            string search = (!string.IsNullOrEmpty(this.Request.QueryString["search"])) ?
                this.Request.QueryString["search"] : null;

            bool rangeProvided = (!string.IsNullOrEmpty(this.Request.QueryString["from"]))
                || (!string.IsNullOrEmpty(this.Request.QueryString["to"]));

            int from = (!string.IsNullOrEmpty(this.Request.QueryString["from"])) ?
                int.Parse(this.Request.QueryString["from"]) : DefaultFrom;

            int to = (!string.IsNullOrEmpty(this.Request.QueryString["to"])) ?
                int.Parse(this.Request.QueryString["to"]) : DefaultTo;

            this.ListingProducts?.Invoke(this, new ProductsEventArgs(
                path,
                room,
                category,
                search,
                rangeProvided,
                from,
                to));
        }

        protected void SearchClick(object sender, EventArgs e)
        {
            var search = this.SearchBox.Text;
            var from = this.FromBox.Text;
            var to = this.ToBox.Text;

            string path = HttpContext.Current.Request.Url.AbsolutePath;

            this.BuildingQuery?.Invoke(this, new QueryEventArgs(path, search, from, to));

            this.Response.Redirect(this.Model.Query);
        }
    }
}