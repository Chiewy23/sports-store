using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SportsStore.Infrastructure;
using SportsStore.Models;

namespace SportsStore.Pages
{
    /*
     * The class associated with a Razor page is known as its page model class.
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

		public Cart? Cart { get; set; }
		public string? ReturnUrl { get; set; }

		public CartModel(IStoreRepository repo, Cart cartService) {
            repository = repo;
            Cart = cartService;
        }

        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
        }

        public IActionResult OnPost(long productId, string returnUrl) {
            var product = repository.Products.FirstOrDefault(p => p.ProductID == productId);
            Cart.AddItem(product, 1);

            return RedirectToPage(new { ReturnUrl = returnUrl });
        }

        public IActionResult OnPostRemove(long productId, string returnUrl) {
            Cart.RemoveLine(Cart.Lines.First(x => x.Product.ProductID == productId).Product);
            return RedirectToPage(new { returnUrl = returnUrl });
        }
    }
}
