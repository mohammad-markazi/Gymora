using Gymora.Service.Plan.Messaging;
using Gymora.Service.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gymora.Database.Entities;

namespace Gymora.Service.Plan
{
    public interface IPlanService
    {
        Task<ApiResponse<int>> CreateAsync(CreatePlanRequest request,CancellationToken cancellationToken);
        Task<ApiResponse> UpdateAsync(EditPlanRequest request, CancellationToken cancellationToken);
        Task<ApiResponse<List<PlanViewModel>>> GetAllAsync(PlanStatus? status,CancellationToken cancellationToken);
        Task<ApiResponse<PlanByIdViewModel>> GetByIdAsync(int id, CancellationToken cancellationToken);

        Task<ApiResponse> AddMovementToPlan(PlanDetailMovementRequest request, CancellationToken cancellationToken);

    }
}
