using Gymora.Database.Entities;
using Gymora.Service.Movement.Messaging;
using Gymora.Service.User.Messaging;
using Gymora.Service.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymora.Service.Movement
{
    public interface IMovementService
    {
        Task<ApiResponse<List<MovementViewModel>>> GetAllAsync(CancellationToken cancellationToken);
        Task<ApiResponse<int>> CreateAsync(CreateMovementRequest request, CancellationToken cancellationToken);
        Task<ApiResponse> DeleteAsync(int id, CancellationToken cancellationToken);

    }
}
