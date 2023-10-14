using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using InternshipRegistrationAPI.Core.Contracts;
using Newtonsoft.Json;

namespace InternshipRegistrationAPI.Core.Models;
public class Form : IDistributableEntity
{
    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; }
    public string UploadCoverImageUrl { get; set; }
    [Required]
    public string Type { get; set; }
    [Required]
    public PersonalInformation PersonalInformation { get; set; }
    [Required]
    public Profile Profile { get; set; }

    public List<QuestionTemplate> AdditionalQuestions { get; set; }


    #region IDistributable Property
    [JsonIgnore]
    public string PartitionKey => Type;
    #endregion
}

public class PersonalInformation
{
    public PersonalInformationTemplate FirstName { get; set; }
    public PersonalInformationTemplate LastName { get; set; }
    public PersonalInformationTemplate Email { get; set; }
    public PersonalInformationTemplate PhoneNumber { get; set; }
    public PersonalInformationTemplate Nationality { get; set; }
    public PersonalInformationTemplate CurrentResidence { get; set; }
    public PersonalInformationTemplate IdNumber { get; set; }
    public PersonalInformationTemplate DateOfBirth { get; set; }
    public PersonalInformationTemplate Gender { get; set; }
    public List<QuestionTemplate> PersonalQuestions { get; set; }
    
}

public class PersonalInformationTemplate
{
    public bool IsInternal { get; set; }
    public bool Hidden { get; set; }
}

public class Profile
{
    public ProfileTemplate Education { get; set; }
    public ProfileTemplate Experience { get; set; }
    public ProfileTemplate Resume { get; set; }
    public List<QuestionTemplate> ProfileQuestions { get; set; }
}
public class ProfileTemplate
{
    public bool IsMandatory { get; set; }
    public bool Hidden { get; set; }
}

public class QuestionTemplate
{
    public string Id { get; set; }
    public QuestionType TypeOfQuestion { get; set;  }
    public string Question { get; set; }
    public List<string> Choices { get; set; }
    public int MaxChoice { get; set; }
    public bool EnableOtherOption { get; set; }
    public bool DisqualifyIfAnswerNo { get; set; }
}

public enum QuestionType
{
    Paragraph,
    ShortAnswer,
    YesNo,
    DropDown,
    MultipleChoice,
    Date,
    Number,
    FileUpload
}