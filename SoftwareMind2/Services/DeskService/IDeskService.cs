
using SoftwareMind2.DTOs.DeskDTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoftwareMind2.Services.DeskService
{
    public interface IDeskService
    {
        Task<CreateDeskResponse> CreateDesk(CreateDeskRequest request);
        Task DeleteDesk(int idDesk);

        Task MakeUnavailable(int idDesk);
        Task MakeAvailable(int idDesk);

        Task<List<SearchDesksByLocationResponse>> SearchDesksOnLocation(int idLocation);

       
    }
}
