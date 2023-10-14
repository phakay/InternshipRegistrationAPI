using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using InternshipRegistrationAPI.Core.Contracts;
using Newtonsoft.Json;

namespace InternshipRegistrationAPI.Core.Models;

public class Workflow : IDistributableEntity
{
    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; }
    [Required]
    public string Type { get; set; }
    [Required]
    public List<StageTemplate> Stages { get; set; } = new List<StageTemplate>();


    #region IDistributable Property
    [JsonIgnore]
    public string PartitionKey => Type;
    #endregion
}

public class StageTemplate
{
    public StageType StageType { get; set; }
    public string StageName { get; set; }
    public int StageOrder { get; set; }
    public bool ShowCandidate { get; set; }
    public List<VideoInterviewQuestionTemplate> VideoInterviewQuestions { get; set; }
}

public enum StageType
{
    Shortlisting,
    VideoInterview,
    Placement
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