using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gymora.Service.Utilities;
using Gymora.Service.VideoMovement.Messaging;

namespace Gymora.Service.VideoMovement
{
    public interface IVideoMovementService
    {
        Task<ApiResponse<List<VideoMovementViewModel>>> GetAllAsync(CancellationToken  cancellationToken );
        Task<ApiResponse<int>> CreateAsync(CreateVideoMovementRequest request, CancellationToken cancellationToken);
        Task<ApiResponse> DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
