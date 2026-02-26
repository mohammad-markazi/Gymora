namespace Gymora.Database.Entities;

public class PlanTemplateDetailModel
{
    public int Id { get; set; }
    public int TemplateId { get; set; }
    public PlanTemplateModel Template { get; set; }
    public byte Number { get; set; }
    public bool Complete { get; set; }
    public bool IsActive { get; set; }
    public List<PlanTemplateMovementModel> TemplateMovements { get; set; }
}