﻿namespace MonsterMexa.Domain
{
    public static class Cart
    {
        private static List<Product> Products { get; set; } = new List<Product>();

        public static List<Product> GetAllProducts()
        {
            return Products;
        }

        public static void AddProduct(Product product)
        {
            Products.Add(product);
        }
    }
}
