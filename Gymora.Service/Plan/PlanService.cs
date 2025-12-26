using Gymora.Database;
using Gymora.Database.Entities;
using Gymora.Service.Plan.Messaging;
using Gymora.Service.User;
using Gymora.Service.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Gymora.Service.Plan;

public class PlanService(IGymoraDbContext context,IAuthService authService) : IPlanService
{
    public Task<ApiResponse> CreateAsync(CreatePlanRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ApiResponse> UpdateAsync(CreatePlanRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<ApiResponse<List<PlanViewModel>>> GetAllAsync(PlanStatus? status, CancellationToken cancellationToken)
    {
        var models = context.PlanModels.AsNoTracking()
            .Select(x => new PlanViewModel()
            {
                Id = x.Id,
                FullName = x.FullName,
                Weight = x.Weight,
                FilePath = x.Files.First(),
                CreateDate = x.CreateDateTime.ToRelativeTime(),
                Status = x.Status
            });

        if (status is not null)
            models = models.Where(x => x.Status == status);
        var data=await models.ToListAsync(cancellationToken);
        return ResponseFactory.Success(data);
    }

    public async Task<ApiResponse<PlanByIdViewModel>> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var coachId = authService.GetCurrentCoachId();

        var planModel =await context.PlanModels.AsNoTracking()
            .Include(x => x.PlanDetails)
            .ThenInclude(x => x.PlanMovements)
            .ThenInclude(x=>x.Movement)
            .Include(x => x.Questions)
            .ThenInclude(x=>x.Question)
            .SingleOrDefaultAsync(x=>x.Id==id && x.CreateCoachId==coachId,cancellationToken);
        if (planModel is null)
            return ResponseFactory.Fail<PlanByIdViewModel>("برنامه یافت نشد");
        var planViewModel = new PlanByIdViewModel()
        {
            Id = planModel.Id,
            Files = planModel.Files,
            FullName = planModel.FullName,
            Number = planModel.Number,
            Weight = planModel.Weight,
            WeakMuscle = planModel.WeakMuscle.SeparateBinaries(),
            PhoneNumber = planModel.PhoneNumber,
            Questions = planModel.Questions.Select(x => new PlanQuestionViewModel()
            {
                Answer = x.Answer,
                Question = x.Question.Body,
                QuestionId = x.QuestionId
            }).ToList(),
            Details = planModel.PlanDetails.Select(x => new PlanDetailViewModel()
            {
                Complete = x.Complete,
                Number = x.Number,
                Movements = x.PlanMovements.Select(y => new PlanMovementViewModel()
                {
                    Code = y.Code,
                    Count = y.Count,
                    Description = y.Description,
                    Id = y.Id,
                    Movement = y.Movement,
                    MovementId = y.MovementId,
                    Set = y.Set
                }).ToList()
            }).ToList()
        };
        return ResponseFactory.Success(planViewModel);
    }
}