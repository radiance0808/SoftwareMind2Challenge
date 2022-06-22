
using SoftwareMind2.DTOs.LocationDTO;
using System.Threading.Tasks;

namespace SoftwareMind2.Services.LocationService
{
    public interface ILocationService
    {
        Task<CreateLocationResponse> CreateLocation(CreateLocationRequest request);

        Task DeleteLocation(int idLocation);

    }
}
