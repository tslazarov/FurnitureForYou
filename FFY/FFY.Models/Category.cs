﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FFY.Models
{
    public class Category
    {
        private ICollection<Product> products;

        public Category()
        {
            this.Products = new HashSet<Product>();
        }

        public Category(string name, string imagePath) : this()
        {
            this.Name = name;
            this.ImagePath = imagePath;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        [Index(IsUnique = true)]
        public string Name { get; set; }

        public string ImagePath { get; set; }

        public virtual ICollection<Product> Products
        {
            get
            {
                return this.products;
            }
            set
            {
                this.products = value;
            }
        }
    }
}