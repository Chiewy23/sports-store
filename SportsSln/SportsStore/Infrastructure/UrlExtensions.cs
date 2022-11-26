
namespace SportsStore.Infrastructure {
	public static class UrlExtensions {

		/* Generate the URL the browser will be returned to after the cart has been
		 * updated, taking into account the query string (if there is one).
		 */
		public static string PathAndQuery(this HttpRequest request) {
			return request.QueryString.HasValue ? $"{request.Path}{request.QueryString}" : request.Path.ToString();
		}
	}
}
