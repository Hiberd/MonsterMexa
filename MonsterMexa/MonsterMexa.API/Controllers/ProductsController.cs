using Microsoft.AspNetCore.Mvc;

namespace MonsterMexa.API.Controllers
{

    public class ProductsController : Controller
    {
         List<Product>? products;
        public async Task Main()
        {
            string form = @$" <form method='post'>
            <label>Id: </label><br>
            <input type='number' name='id'/><br>
            <label>Name: </label><br>
            <input name='Name'/><br>
            <label>Size: </label><br>
            <input type='number' name='Size'/><br>
            <input type='submit' value='Send'/>
            <input type='submit' value='Update' formaction='UpdateProduct'/>
            </form><p></p>
            <form method='get' action='AllList'>
            <input  type='submit' value='List of products'/></form>
            <form method='get' action='ListById'>
            <input type='number' name='Id'>
            <input  type='submit' value='Product id'/></form>
";



            Response.ContentType = "text/html;charset=utf-8";
            
            await Response.WriteAsync(form);
          
        }
        public async Task AllList()
        {
            products = Store.GetAllProducts();

            string list = @"<table><caption><b>List</b></captiom>
            <tr><td>Id</td><td>Name</td><td>Size</td></tr>";

            foreach (var product in products)
            {
                list = $"{list}<tr><td>{product.Id}</td><td>{product.Name}</td><td>{product.Size}</td></tr>";
            }
            list = $"{list}</table>";

            await Response.WriteAsync(list);
        }


        public async Task ListById(int Id)
        {
            products = Store.GetAllProducts().Where(p => p.Id == Id).ToList();

            string list = @"<table><caption><b>List</b></captiom>
            <tr><td>Id</td><td>Name</td><td>Size</td></tr>";

            foreach (var product in products)
            {
                list = $"{list}<tr><td>{product.Id}</td><td>{product.Name}</td><td>{product.Size}</td></tr>";
            }
            list = $"{list}</table>";

            await Response.WriteAsync(list);
        }
        public IActionResult UpdateProduct(int Id,string Name, int Size)
        {
            Store.GetAllProducts().Where(p => p.Id == Id).Select(u => (u.Name = Name,u.Size=Size)).ToList();
            return RedirectPermanent("~/Products/Main");
        }
        [HttpPost]
        public IActionResult Main(int Id,string Name, int Size)
        {
            if (Store.GetAllProducts().Exists(p =>p.Id==Id))
            {
                return Content("The product with this Id exists");
            }
            Store.CreateProduct(Id, Name, Size);
            return RedirectPermanent("~/Products/Main");
        }
    }
   
   
}
