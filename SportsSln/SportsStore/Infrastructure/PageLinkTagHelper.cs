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
		private IUrlHelperFactory urlHelperFactory;

		public PageLinkTagHelper(IUrlHelperFactory urlHelperFactory) {
			this.urlHelperFactory = urlHelperFactory;
		}

		[ViewContext]
		[HtmlAttributeNotBound]
		public ViewContext ViewContext { get; set; }
		public PagingInfo PageModel { get; set; }
		public string PageAction { get; set; }
		public override void Process(TagHelperContext context, TagHelperOutput output) {
			var urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
			var result = new TagBuilder("div");

			for (int i = 1; i <= PageModel.TotalPages; i++) {
				var tag = new TagBuilder("a");
				tag.Attributes["href"] = urlHelper.Action(PageAction, new { productPage = i });
				tag.InnerHtml.Append(i.ToString());
				result.InnerHtml.AppendHtml(tag);
			}

			output.Content.AppendHtml(result.InnerHtml);
		}
	}
}
