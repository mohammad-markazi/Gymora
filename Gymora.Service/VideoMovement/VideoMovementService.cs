using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gymora.Database;
using Gymora.Database.Entities;
using Gymora.Service.Common;
using Gymora.Service.User;
using Gymora.Service.Utilities;
using Gymora.Service.VideoMovement.Messaging;
using Microsoft.EntityFrameworkCore;

namespace Gymora.Service.VideoMovement
{
    public class VideoMovementService(IAuthService authService,IFileUploader fileUploader,IGymoraDbContext context):IVideoMovementService
    {
        public async Task<ApiResponse<List<VideoMovementViewModel>>> GetAllAsync(CancellationToken cancellationToken)
        {
            var coachId = authService.GetCurrentCoachId();

            var videos =await context.VideoMovementModels.AsNoTracking()
                .Where(x => x.IsActive && x.CreateCoachId == coachId)
                .Select(x => new VideoMovementViewModel()
                {
                    Id = x.Id,
                    MovementName = x.Movement.Name,
                    Link = x.Link
                }).ToListAsync(cancellationToken);

            return ResponseFactory.Success(videos);
        }

        public async Task<ApiResponse<int>> CreateAsync(CreateVideoMovementRequest request, CancellationToken cancellationToken)
        {
            var path =await fileUploader.Upload(request.File, "MovementVideo");
            var coachId = authService.GetCurrentCoachId();

            var entity = new VideoMovementModel()
            {
                Link = path,
                CreateCoachId = coachId,
                IsActive = true,
                MovementId = request.MovementId
            };
           await context.VideoMovementModels.AddAsync(entity, cancellationToken);
           await context.SaveChangesAsync(cancellationToken);
           return ResponseFactory.Success(entity.Id);
        }

        public async Task<ApiResponse> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var coachId = authService.GetCurrentCoachId();
            var video =await context.VideoMovementModels.SingleOrDefaultAsync(x => x.Id == id && x.CreateCoachId == coachId,
                cancellationToken);
            if (video is null)
                return ResponseFactory.Fail("ویدیو یافت نشد");
            video.IsActive = false;
            fileUploader.RemoveFile(video.Link);
            await context.SaveChangesAsync(cancellationToken);
            return ResponseFactory.Success();
        }
    }
}
