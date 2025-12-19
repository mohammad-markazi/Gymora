using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gymora.Database;
using Gymora.Database.Entities.Utility;
using Gymora.Service.Question.Messaging;
using Gymora.Service.User;
using Gymora.Service.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Gymora.Service.Question
{
    public class QuestionService(IAuthService authService,IGymoraDbContext context):IQuestionService
    {
        public async Task<ApiResponse<int>> CreateAsync(CreateQuestionRequest request, CancellationToken cancellationToken)
        {
            var coachId = authService.GetCurrentCoachId();
            var entity = new QuestionModel()
            {
                CreateCoachId = coachId,
                Body = request.Body,
                IsActive = true
            };
            await context.QuestionModels.AddAsync(entity, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            return ResponseFactory.Success(entity.Id);
        }

        public async Task<ApiResponse<List<QuestionViewModel>>> GetAllAsync(CancellationToken cancellationToken)
        {
            var coachId = authService.GetCurrentCoachId();
            var questions =await context.QuestionModels
                .AsNoTracking()
                .Where(x => x.IsActive && x.CreateCoachId == coachId)
                .Select(x=> new QuestionViewModel()
                {
                    Id = x.Id,
                    Body = x.Body
                })
                .ToListAsync(cancellationToken);
            return ResponseFactory.Success(questions);
        }

        public async Task<ApiResponse> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var coachId = authService.GetCurrentCoachId();
            var question = await context.QuestionModels
                .SingleOrDefaultAsync(x => x.CreateCoachId == coachId && x.Id == id, cancellationToken);
            if (question is null)
                return ResponseFactory.Fail("سوال یافت نشد");
            question.IsActive = false;
            await context.SaveChangesAsync(cancellationToken);
            return ResponseFactory.Success(question);
        }
    }
}
