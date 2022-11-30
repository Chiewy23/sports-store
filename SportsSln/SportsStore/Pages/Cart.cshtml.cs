using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SportsStore.Infrastructure;
using SportsStore.Models;

namespace SportsStore.Pages
{
    /*
     * The class associated with a Razor page is known is known as its page model class.
     * It defines the handler methods which are invoked for different types of HTTP requests;
     * these update state before rendering the view.
     * 
     * The handler methods use parameter names which match the input elements in the HTML forms
     * produced by the ProductSummary.cshtml view. This allows ASP.NET Core to associate incoming
     * form POST variables with those parameters. Thus, we don't need to process the form directly.
     * This is known as model binding.
     */
    public class CartModel : PageModel
    {
        private readonly IStoreRepository repository;

        public CartModel(IStoreRepository repo) {
            repository = repo;
        }

        public Cart? Cart { get; set; }
        public string? ReturnUrl { get; set; }

        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
            Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
        }

        public IActionResult OnPost(long productId, string returnUrl) {
            var product = repository.Products.FirstOrDefault(p => p.ProductID == productId);

            if (product == null) {
                return RedirectToPage(returnUrl);
            }
            
            Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
            Cart.AddItem(product, 1);

            HttpContext.Session.SetJson("cart", Cart);

            return RedirectToPage(new { ReturnUrl = returnUrl });
        }
    }
}
