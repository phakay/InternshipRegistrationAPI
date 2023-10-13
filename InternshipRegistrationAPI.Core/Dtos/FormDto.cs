using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using InternshipRegistrationAPI.Core.Models;
using Newtonsoft.Json;

namespace InternshipRegistrationAPI.Core.Dtos
{

    public class FormDto 
    {
        public string Id { get; set; }
        public string UploadCoverImageUrl { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public PersonalInformation PersonalInformation { get; set; }
        [Required]
        public Profile Profile { get; set; }
        public List<QuestionTemplate> AdditionalQuestions { get; set; }

    }
}
