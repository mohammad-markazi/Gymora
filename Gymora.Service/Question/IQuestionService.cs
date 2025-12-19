using Gymora.Service.Question.Messaging;
using Gymora.Service.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymora.Service.Question
{
    public interface IQuestionService
    {
        Task<ApiResponse<int>> CreateAsync(CreateQuestionRequest request,CancellationToken cancellationToken);
        Task<ApiResponse<List<QuestionViewModel>>> GetAllAsync(CancellationToken cancellationToken);
        Task<ApiResponse> DeleteAsync(int id,CancellationToken cancellationToken);
    }
}
