namespace MonsterMexa.API
{
    public static class Store 
    {
       
        static  List<Product> products = new List<Product>();

        public static void CreateProduct(int id, string name, int size)
        {
            products.Add(new Product { Id = id, Name = name, Size = size });    
        }
        public static List<Product> GetAllProducts()
        {
            return products;
        }
    }
}
