using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gymora.Database.Entities;

public class PlanTemplateMovementModel
{
    public int Id { get; set; }
    public int TempDetailId { get; set; }
    public PlanTemplateDetailModel TempDetail { get; set; }
    public int MovementId { get; set; }
    public MovementModel Movement { get; set; }
    [MaxLength(100)]
    public string Pattern { get; set; }
    public int? ParentId { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
    [ForeignKey(nameof(ParentId))]
    public PlanTemplateMovementModel Parent { get; set; }
}