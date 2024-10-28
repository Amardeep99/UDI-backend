using System.Threading.Tasks;

namespace UDI_backend.Clients {
	public interface IBronnoysundsRegClient {
		Task<string?> GetOrganisationDetails(int orgNr);
	}
}
