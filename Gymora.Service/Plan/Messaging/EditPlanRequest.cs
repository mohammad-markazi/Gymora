namespace Gymora.Service.Plan.Messaging;

public class EditPlanRequest 
{
    public int Id { get; set; }
    public string? PhoneNumber { get; set; }
    public string? FullName { get; set; }
    public List<string>? Files { get; set; }
    public byte? Weight { get; set; }
    public byte? Number { get; set; }
    public short? WeakMuscle { get; set; }
    public List<PlanQuestionRequest>? Questions { get; set; }

}