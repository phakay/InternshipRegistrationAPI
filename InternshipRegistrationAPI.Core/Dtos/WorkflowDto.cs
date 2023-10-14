using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using InternshipRegistrationAPI.Core.Models;

namespace InternshipRegistrationAPI.Core.Dtos;

public class WorkflowDto
{
    public string Id { get; set; }
    [Required]
    public string Type { get; set; }
    [Required]
    public List<StageTemplate> Stages { get; set;} = new List<StageTemplate>();
}