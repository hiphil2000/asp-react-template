using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ReactTest.API.Models
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum Role
	{
		Anonymous,
		User,
		Admin
	}
}