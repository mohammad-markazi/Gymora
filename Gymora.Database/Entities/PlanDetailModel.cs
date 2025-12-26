namespace Gymora.Database.Entities;

public class PlanDetailModel
{
    public int Id { get; set; }
    public int PlanId { get; set; }
    public PlanModel Plan { get; set; }
    public byte Number { get; set; }
    public bool Complete { get; set; }
    public bool IsActive { get; set; }
    public List<PlanMovementModel> PlanMovements { get; set; }
}