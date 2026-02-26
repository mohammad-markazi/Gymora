using Gymora.Database;
using Gymora.Database.Entities;
using Gymora.Service.Common;
using Gymora.Service.Plan.Messaging;
using Gymora.Service.PlanTemplate.Messaging;
using Gymora.Service.User;
using Gymora.Service.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Gymora.Service.PlanTemplate;

public class PlanTemplateService(IGymoraDbContext context,IAuthService authService) : IPlanTemplateService
{
    public async Task<ApiResponse<int>> CreateAsync(CreateTemplateRequest request, CancellationToken cancellationToken)
    {
        var coachId = authService.GetCurrentCoachId();

        var entity = new PlanTemplateModel()
        {
            CreateCoachId = coachId,
            Description = request.Description,
            Name = request.Name,
            IsActive = true,
            CreateDateTime = DateTime.Now
        };

        entity.TemplateDetails ??= new List<PlanTemplateDetailModel>();
        for (byte i = 1; i <= 5; i++)
        {
            entity.TemplateDetails.Add(new PlanTemplateDetailModel()
            {
                Complete = false,
                Number = i,
                IsActive = true
            });
        }

        await context.TemplateModels.AddAsync(entity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return ResponseFactory.Success(entity.Id);
    }

    public async Task<ApiResponse<List<TemplateViewModel>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var coachId = authService.GetCurrentCoachId();

        var result =await context.TemplateModels.AsNoTracking()
            .Where(x => x.CreateCoachId == coachId && x.IsActive)
            .OrderByDescending(x => x.CreateDateTime)
            .Select(x => new TemplateViewModel()
            {
                Id = x.Id,
                Description = x.Description,
                Name = x.Name,
                Status = x.Status
            }).ToListAsync(cancellationToken);

        return ResponseFactory.Success(result);
    }

    public async Task<ApiResponse> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var coachId = authService.GetCurrentCoachId();

        var template =await context.TemplateModels
            .SingleOrDefaultAsync(x => x.Id == id && x.CreateCoachId == coachId, cancellationToken);
        if (template is null)
            return ResponseFactory.Fail("قالب یافت نشد");

        template.IsActive=false;

       await context.SaveChangesAsync(cancellationToken);

        return ResponseFactory.Success();
    }

    public async Task<ApiResponse<TemplateByIdViewModel>> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var coachId = authService.GetCurrentCoachId();

        var templateModel = await context.TemplateModels.AsNoTracking()
            .Include(x => x.TemplateDetails).ThenInclude(x=>x.TemplateMovements)
            .SingleOrDefaultAsync(x => x.Id == id && x.CreateCoachId == coachId, cancellationToken);
        if (templateModel is null)
            return ResponseFactory.Fail<TemplateByIdViewModel>("برنامه یافت نشد");
        var planViewModel = new TemplateByIdViewModel()
        {
            Id = templateModel.Id,
            Status = templateModel.Status,
            Name = templateModel.Name,
            Description = templateModel.Description,
            CreateDateTime = templateModel.CreateDateTime,
            ModifiedDateTime = templateModel.ModifiedDateTime,
            Details = templateModel.TemplateDetails.Select(x => new TemplateDetailViewModel()
            {
                Id = x.Id,
                Complete = x.Complete,
                Number = x.Number,
                Movements = MapPlanMovementsToViewModel(x.TemplateMovements.Where(y => y.IsActive).ToList()).ToList()
            }).ToList()
        };
        return ResponseFactory.Success(planViewModel);
    }

    public async Task<ApiResponse> UpdateAsync(EditTemplateRequest request, CancellationToken cancellationToken)
    {
        var coachId = authService.GetCurrentCoachId();

        var template = await context.TemplateModels
            .SingleOrDefaultAsync(x => x.CreateCoachId == coachId && x.Id == request.Id, cancellationToken);

        if (template is null)
            return ResponseFactory.Fail("قالب یافت نشد");

        template.Name = request.Name ?? template.Name;
       template.Description = request.Description ?? template.Description;
       template.ModifiedDateTime=DateTime.Now;
      await context.SaveChangesAsync(cancellationToken);
      return ResponseFactory.Success();
    }

    public async Task<ApiResponse<TemplateByIdViewModel>> OverrideIntoPlan(OverrideIntoPlanRequest request, CancellationToken cancellationToken)
    {
        var coachId = authService.GetCurrentCoachId();

        var template =await context.TemplateModels.Include(x => x.TemplateDetails)
            .ThenInclude(x => x.TemplateMovements)
            .SingleOrDefaultAsync(x => x.Id == request.TemplateId && x.CreateCoachId== coachId, cancellationToken);
        if (template is null)
            return ResponseFactory.Fail<TemplateByIdViewModel>("قالب یافت نشد");

        var plan = await context.PlanModels.Include(x => x.PlanDetails)
            .ThenInclude(x => x.PlanMovements)
            .SingleOrDefaultAsync(x => x.Id == request.TemplateId && x.CreateCoachId== coachId, cancellationToken);
        if (plan is null)
            return ResponseFactory.Fail<TemplateByIdViewModel>("برنامه یافت نشد");

        if (plan.PlanDetails.Any())
        {
            context.PlanDetailModels.RemoveRange(plan.PlanDetails);
            plan.PlanDetails = new List<PlanDetailModel>();
        }

        plan.PlanDetails = template.TemplateDetails.Select(x => new PlanDetailModel()
        {
            Complete = x.Complete,
            Number = x.Number,
            PlanId = plan.Id,
            IsActive = x.IsActive,
            PlanMovements = x.TemplateMovements.Select(y=>new PlanMovementModel()
            {
                MovementId = y.MovementId,
                IsActive = y.IsActive,
                ParentId = y.ParentId,
                Pattern = y.Pattern,
                Description = y.Description
            }).ToList()
        }).ToList();

        template.UsedCount += 1;
        await context.SaveChangesAsync(cancellationToken);

          return ResponseFactory.Success(new TemplateByIdViewModel());

    }

    private List<TemplateMovementViewModel> MapPlanMovementsToViewModel(List<PlanTemplateMovementModel> movements)
    {
        var result = new List<TemplateMovementViewModel>();

        var parents = movements.Where(m => m.ParentId == null).ToList();

        foreach (var parent in parents)
        {
            result.Add(new TemplateMovementViewModel()
            {
                Id = parent.Id,
                MovementId = parent.MovementId,
                MovementName = parent.Movement.Name,
                Code = parent.Id,
                Parent = true,
                OrderBy = 0,
                Pattern = parent.Pattern,
                Description = parent.Description
            });

            var children = movements
                .Where(m => m.ParentId == parent.Id)
                .OrderBy(m => m.Id)
                .ToList();

            for (int i = 0; i < children.Count; i++)
            {
                result.Add(new TemplateMovementViewModel
                {
                    Id = children[i].Id,
                    MovementId = children[i].MovementId,
                    MovementName = children[i].Movement.Name,
                    Code = parent.Id,
                    Parent = false,
                    OrderBy = i + 1,
                    Pattern = children[i].Pattern,
                    Description = children[i].Description
                });
            }
        }

        return result;
    }

}