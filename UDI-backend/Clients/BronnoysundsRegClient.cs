using System.Globalization;
using UDI_backend.Clients.Models;

namespace UDI_backend.Clients {
	public class BronnoysundsRegClient {
		private readonly HttpClient _client;
		public string hello = "hello";
		public BronnoysundsRegClient(HttpClient client) { 
			_client = client;
		}

		public async Task<string?> GetOrganisationDetails(int orgNr) {
			HttpResponseMessage? respone = await _client.GetAsync($"https://data.brreg.no/enhetsregisteret/api/enheter/{orgNr}");

			if (!respone.IsSuccessStatusCode) return null;

			OrganisationDetails? org = await respone.Content.ReadFromJsonAsync<OrganisationDetails>();

			return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(org?.Navn.ToLower() ?? "");
		}
	}
}
