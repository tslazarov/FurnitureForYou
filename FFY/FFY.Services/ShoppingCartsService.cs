﻿using FFY.Data.Contracts;
using FFY.Data.Factories;
using FFY.Models;
using FFY.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFY.Services
{
    public class ShoppingCartsService : IShoppingCartsService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IGenericRepository<ShoppingCart> shoppingCartRepository;
        private readonly ICartProductFactory cartProductFactory;

        public ShoppingCartsService(IUnitOfWork unitOfWork,
            ICartProductFactory cartProductFactory,
            IGenericRepository<ShoppingCart> shoppingCartRepository)
        {
            if(unitOfWork == null)
            {
                throw new ArgumentNullException("Unit of work cannot be null.");
            }

            if(shoppingCartRepository == null)
            {
                throw new ArgumentNullException("Users repository cannot be null.");
            }

            if (cartProductFactory == null)
            {
                throw new ArgumentNullException("Cart product factory cannot be null.");
            }

            this.unitOfWork = unitOfWork;
            this.shoppingCartRepository = shoppingCartRepository;
            this.cartProductFactory = cartProductFactory;
        }

        public void AssignShoppingCart(ShoppingCart shoppingCart)
        {
            if (shoppingCart == null)
            {
                throw new ArgumentNullException("Shopping cart cannot be null.");
            }

            using (this.unitOfWork)
            {
                this.shoppingCartRepository.Add(shoppingCart);
                this.unitOfWork.Commit();
            }
        }

        public void Add(int quantity, Product product, string cartId)
        {
            var shoppingCart = this.shoppingCartRepository.GetById(cartId);

            var currentCartProduct = shoppingCart.CartProducts.FirstOrDefault(p => p.ProductId == product.Id);

            if (currentCartProduct == null)
            {
                currentCartProduct = this.cartProductFactory.CreateCartProduct(quantity, product);
                shoppingCart.CartProducts.Add(currentCartProduct);
            }
            else
            {
                currentCartProduct.Quantity += quantity;
            }

            currentCartProduct.Total = currentCartProduct.Quantity * currentCartProduct.Product.DiscountedPrice;

            shoppingCart.Total = shoppingCart.CartProducts.Sum(p =>
            (p.Product.DiscountedPrice * p.Quantity));

            using (this.unitOfWork)
            {
                this.shoppingCartRepository.Update(shoppingCart);
                this.unitOfWork.Commit();
            }
        }

        public void Remove(int productId, string cartId)
        {
            var shoppingCart = this.shoppingCartRepository.GetById(cartId);

            var currentCartProduct = shoppingCart.CartProducts.FirstOrDefault(p => p.ProductId == productId);

            if (currentCartProduct != null)
            {
                shoppingCart.CartProducts.Remove(currentCartProduct);
            }

            shoppingCart.Total = shoppingCart.CartProducts.Sum(p =>
            (p.Product.DiscountedPrice * p.Quantity));

            using (this.unitOfWork)
            {
                this.shoppingCartRepository.Update(shoppingCart);
                this.unitOfWork.Commit();
            }
        }

        public void Clear(ShoppingCart shoppingCart)
        {
            shoppingCart.CartProducts.Clear();
            shoppingCart.Total = 0;

            using (this.unitOfWork)
            {
                this.shoppingCartRepository.Update(shoppingCart);
                this.unitOfWork.Commit();
            }
        }

        public int CartProductsCount(string cartId)
        {
            return this.shoppingCartRepository.GetById(cartId)
                .CartProducts.Count();
        }

        public ShoppingCart GetCart(string cartId)
        {
            return this.shoppingCartRepository.GetById(cartId);
        }
    }
}
