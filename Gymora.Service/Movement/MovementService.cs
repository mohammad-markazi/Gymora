using Gymora.Database;
using Gymora.Database.Entities;
using Gymora.Service.Movement.Messaging;
using Gymora.Service.User;
using Gymora.Service.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Gymora.Service.Movement;

public class MovementService(IGymoraDbContext context,IAuthService authService) : IMovementService
{
    public async Task<ApiResponse<List<MovementViewModel>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var coachId = authService.GetCurrentCoachId();
        var movements=await context.MovementModels
            .AsNoTracking()
            .Where(x => x.IsActive && (x.CreateCoachId == coachId || x.CreateCoachId == null))
            .Select(x => new MovementViewModel()
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync(cancellationToken);
        return ResponseFactory.Success(movements);
    }

    public async Task<ApiResponse<int>> CreateAsync(CreateMovementRequest request, CancellationToken cancellationToken)
    {
        var coachId = authService.GetCurrentCoachId();

        var entity = new MovementModel()
        {
            Name = request.Name,
            CreateCoachId = coachId,
            IsActive = true
        };
        await context.MovementModels.AddAsync(entity,cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return ResponseFactory.Success(entity.Id);
    }

    public async Task<ApiResponse> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var coachId = authService.GetCurrentCoachId();

        var movement =await context.MovementModels
            .SingleOrDefaultAsync(x => x.CreateCoachId == coachId && x.Id == id,cancellationToken);

        if (movement is null)
            return ResponseFactory.Fail("حرکت یافت نشد");
        movement.IsActive=false;
        await context.SaveChangesAsync(cancellationToken);
        return ResponseFactory.Success();
    }
}