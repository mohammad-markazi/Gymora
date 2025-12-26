namespace Gymora.Service.Plan.Messaging;

public class PlanQuestionRequest
{
    public int QuestionId { get; set; }
    public string Answer { get; set; }

}

public class PlanQuestionViewModel
{
    public int QuestionId { get; set; }
    public string Question { get; set; }
    public string Answer { get; set; }

}