using Gymora.Service.PlanTemplate.Messaging;
using Gymora.Service.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymora.Service.PlanTemplate
{
    public interface IPlanTemplateService
    {
        Task<ApiResponse<int>> CreateAsync(CreateTemplateRequest request, CancellationToken cancellationToken);
        Task<ApiResponse<List<TemplateViewModel>>> GetAllAsync(CancellationToken cancellationToken);
        Task<ApiResponse> DeleteAsync(int id,CancellationToken cancellationToken);
        Task<ApiResponse<TemplateByIdViewModel>> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<ApiResponse> UpdateAsync(EditTemplateRequest request, CancellationToken cancellationToken);
        Task<ApiResponse<TemplateByIdViewModel>> OverrideIntoPlan(OverrideIntoPlanRequest request, CancellationToken cancellationToken);
    }
}
