using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using InternshipRegistrationAPI.Core.Models;

namespace InternshipRegistrationAPI.Core.Dtos;

public class WorkflowDto
{
    public string Id { get; set; }
    [Required]
    public string Type { get; set; }
    public List<StageTemplate> Stages = new List<StageTemplate>();
    [RegularExpression("true", ErrorMessage = "The 'Stages' must contain a distinct order")]
    public bool ValidateStages => Stages.DistinctBy(b => b.StageOrderId).Count() == Stages.Count;
}