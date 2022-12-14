using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SportsStore.Models.ViewModels;

namespace SportsStore.Infrastructure {
	/*
	 * The Infrastructure folder stores classes which deliver the 
	 * plumbing for an application but which are not related to the app's
	 * main functionality.
	 */

	/*
	 * This tag helper populates a <div> element with elements corresponding
	 * to pages of products.
	 * 
	 * Tag helpers are preferable to including blocks of C# code in a view
	 * because they can be easily unit tested.
	 * 
	 * Note that tag helpers have to be registered in order to be utilised
	 * (see _ViewImports.cshtml).
	 */
	[HtmlTargetElement("div", Attributes = "page-model")]
	public class PageLinkTagHelper : TagHelper {
		private readonly IUrlHelperFactory urlHelperFactory;

		public PageLinkTagHelper(IUrlHelperFactory urlHelperFactory) {
			this.urlHelperFactory = urlHelperFactory;
		}

		[ViewContext]
		[HtmlAttributeNotBound]
		public ViewContext ViewContext { get; set; }
		public PagingInfo PageModel { get; set; }
		public string PageAction { get; set; }

		/** Using Html attribute allows us to specify a prefix for attribute
		 * names on an element.
		 * 
		 * The value of any attribute whose name begins with this prefix will
		 * be added to the dictionary assigned to PageUrlValues property.
		 * 
		 * This is then passed to the IUrlHelper.Action method to generate the
		 * URL for the href attribute of the <a> elements produced by the
		 * tag helper.
		 */
		[HtmlAttributeName(DictionaryAttributePrefix = "page-url-")]
		public Dictionary<string, object> PageUrlValues { get; set; } = new Dictionary<string, object>();

		// ##### STYLING #####
		public bool PageClassesEnabled { get; set; } = false;
		public string PageClass { get; set; }
		public string PageClassNormal { get; set; }
		public string PageClassSelected { get; set; }

		public override void Process(TagHelperContext context, TagHelperOutput output) {
			var urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
			var result = new TagBuilder("div");

			for (int i = 1; i <= PageModel.TotalPages; i++) {
				var tag = new TagBuilder("a");
				PageUrlValues["productPage"] = i;
				
				tag.Attributes["href"] = urlHelper.Action(PageAction, PageUrlValues);

				if (PageClassesEnabled) {
					tag.AddCssClass(PageClass);
					tag.AddCssClass(i == PageModel.CurrentPage ? PageClassSelected : PageClassNormal);
				}

				tag.InnerHtml.Append(i.ToString());
				result.InnerHtml.AppendHtml(tag);
			}

			output.Content.AppendHtml(result.InnerHtml);
		}
	}
}
