using System.ComponentModel.DataAnnotations;
using Gymora.Database.Entities.Utility;

namespace Gymora.Database.Entities;

public class PlanQuestionModel
{
    public int Id { get; set; }
    public int PlanId { get; set; }
    public PlanModel Plan { get; set; }
    [MaxLength(500)]
    public string Answer { get; set; }
    public int QuestionId { get; set; }
    public QuestionModel Question { get; set; }
    public bool IsActive { get; set; }
}