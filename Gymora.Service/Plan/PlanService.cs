using Gymora.Database;
using Gymora.Database.Entities;
using Gymora.Service.Common;
using Gymora.Service.Plan.Messaging;
using Gymora.Service.User;
using Gymora.Service.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Gymora.Service.Plan;

public class PlanService(IGymoraDbContext context, IAuthService authService, IFileUploader fileUploader) : IPlanService
{
    public async Task<ApiResponse<int>> CreateAsync(CreatePlanRequest request, CancellationToken cancellationToken)
    {
        var coachId = authService.GetCurrentCoachId();
        if (request.Files is {Count:<=0})
            request.Files.Add(fileUploader.GetPathImageNotFound());

        var entity = new PlanModel()
        {
            FullName = request.FullName,
            PhoneNumber = request.PhoneNumber,
            ModifiedDateTime = DateTime.Now,
            CreateCoachId = coachId,
            Number = request.Number ?? 0,
            Status = PlanStatus.Unknown,
            Weight = request.Weight ?? 0,
            WeakMuscle = request.WeakMuscle,
            Files = request.Files
        };
        if (request.Questions is {Count:>0})
        {
            entity.Questions ??= new List<PlanQuestionModel>();
            request.Questions.ForEach(item =>
            {
                entity.Questions.Add(new PlanQuestionModel()
                {
                    Answer = item.Answer,
                    QuestionId = item.QuestionId,
                    IsActive = true
                });
            });
        }

        entity.PlanDetails ??= new List<PlanDetailModel>();
        for (byte i = 1; i <= 5; i++)
        {
            entity.PlanDetails.Add(new PlanDetailModel()
            {
                Complete = false,
                Number = i,
                IsActive = true
            });
        }

        await context.PlanModels.AddAsync(entity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return ResponseFactory.Success(entity.Id);
    }

    public async Task<ApiResponse> UpdateAsync(EditPlanRequest request, CancellationToken cancellationToken)
    {
        var coachId = authService.GetCurrentCoachId();

        var plan = await context.PlanModels.Include(x => x.PlanDetails)
            .Include(x => x.Questions)
            .SingleOrDefaultAsync(x => x.CreateCoachId == coachId && x.Id == request.Id, cancellationToken);

        if (plan is null)
            return ResponseFactory.Fail("برنامه یافت نشد");

        plan.FullName = request.FullName ?? plan.FullName;
        plan.PhoneNumber = request.PhoneNumber ?? plan.PhoneNumber;
        plan.Number = request.Number ?? plan.Number;
        plan.Weight = request.Weight ?? plan.Weight;
        plan.WeakMuscle = request.WeakMuscle ?? plan.WeakMuscle;
        plan.ModifiedDateTime = DateTime.Now;
        plan.Files = request.Files ?? plan.Files;

        if (request.Questions is { Count: > 0 })
        {
            plan.Questions.ForEach(x => x.IsActive = false);

            request.Questions.ForEach(item =>
            {
                plan.Questions.Add(new PlanQuestionModel()
                {
                    Answer = item.Answer,
                    QuestionId = item.QuestionId,
                    IsActive = true
                });
            });
        }

        await context.SaveChangesAsync(cancellationToken);
        return ResponseFactory.Success();
    }

    public async Task<ApiResponse<List<PlanViewModel>>> GetAllAsync(PlanStatus? status, CancellationToken cancellationToken)
    {
        var coachId = authService.GetCurrentCoachId();

        var models = context.PlanModels
            .Where(x => x.CreateCoachId == coachId).AsNoTracking();

        if (status is not null)
            models = models.Where(x => x.Status == status);

        var plans = await models.ToListAsync(cancellationToken);

        var data = plans
            .Select(x => new PlanViewModel()
            {
                Id = x.Id,
                FullName = x.FullName,
                Weight = x.Weight,
                FilePath = x.Files.First(),
                CreateDate = x.CreateDateTime.ToRelativeTime(),
                Status = x.Status
            }).ToList();
        return ResponseFactory.Success(data);
    }

    public async Task<ApiResponse<PlanByIdViewModel>> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var coachId = authService.GetCurrentCoachId();

        var planModel = await context.PlanModels.AsNoTracking()
            .Include(x => x.PlanDetails)
            .ThenInclude(x => x.PlanMovements)
            .ThenInclude(x => x.Movement)
            .Include(x => x.Questions)
            .ThenInclude(x => x.Question)
            .SingleOrDefaultAsync(x => x.Id == id && x.CreateCoachId == coachId, cancellationToken);
        if (planModel is null)
            return ResponseFactory.Fail<PlanByIdViewModel>("برنامه یافت نشد");
        var planViewModel = new PlanByIdViewModel()
        {
            Id = planModel.Id,
            Files = planModel.Files,
            FullName = planModel.FullName,
            Status = planModel.Status,
            Number = planModel.Number,
            Weight = planModel.Weight,
            WeakMuscle = planModel.WeakMuscle.SeparateBinaries(),
            PhoneNumber = planModel.PhoneNumber,
            Questions = planModel.Questions.Where(x => x.IsActive).Select(x => new PlanQuestionViewModel()
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