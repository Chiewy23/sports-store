using System.Text.Json;

namespace SportsStore.Infrastructure {

	/*
	 * The session state feature in ASP.NET Core only stores int, string, and byte[]
	 * values. We need to store a Cart object, so will define extension methods to the
	 * ISession interface, which provide access to the session state data to serialise
	 * Cart objects into JSON and convert them back.
	 */
	public static class SessionExtensions {
		public static void SetJson(this ISession session, string key, object value) {
			session.SetString(key, JsonSerializer.Serialize(value));
		}

		public static T? GetJson<T>(this ISession session, string key) {
			var sessionData = session.GetString(key);
			return sessionData == null ? default(T) : JsonSerializer.Deserialize<T>(sessionData);
		}
	}
}
