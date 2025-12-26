namespace Gymora.Database.Entities;

public class PlanMovementModel
{
    public int Id { get; set; }
    public int PlanDetailId { get; set; }
    public PlanDetailModel PlanDetail { get; set; }
    public int  MovementId{ get; set; }
    public MovementModel Movement { get; set; }
    public byte Set { get; set; }
    public byte Count { get; set; }
    public int Code { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
}