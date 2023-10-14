using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using InternshipRegistrationAPI.Core.Contracts;
using Newtonsoft.Json;

namespace InternshipRegistrationAPI.Core.Models;

public class Workflow : IDistributableEntity
{
    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; }
    [Required]
    public string Type { get; set; }
    public List<StageTemplate> Stages = new List<StageTemplate>();
    [RegularExpression("true", ErrorMessage = "The 'Stages' must contain a distinct order")]
    public bool ValidateStages => Stages.DistinctBy(b => b.StageOrderId).Count() == Stages.Count;


    #region IDistributable Property
    [JsonIgnore]
    public string PartitionKey => Type;
    string IDistributableEntity.Id => Id;
    #endregion
}

public class StageTemplate
{
    public StageType StageType;
    public string StageName;
    public int StageOrderId;

}

public enum StageType
{
    Shortlisting,
    VideoInterview,
    Placement
}

public class VideoInterview
{
    public List<VideoInterviewQuestionTemplate> VideoInterviewQuestions;
}

public class Shortlisting
{
    public string ShortlistingInfo;
}

public class Placement
{
    public string PlacementInfo;
}


public class VideoInterviewQuestionTemplate
{
    public string VideoInterviewQuestion { get; set; }
    public string AdditionalInformation { get; set; }
    public int MaxDurationOfVideo { get; set; }
    public VideoDurationUnit DurationUnit { get; set; }
    public int MaxDaysForVideoSubmission { get; set; }
}

public enum VideoDurationUnit
{
    Seconds,
    Minutes
}