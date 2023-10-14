using InternshipRegistrationAPI.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipRegistrationAPI.Core.Views
{
    public  class PreviewView
    {
        public string ProgramTitle { get; set; }
        public string UploadCoverImageUrl { get; set; }
        public string ProgramDescription { get; set; }
        public string ProgramSummary { get; set; }
        public List<string> SkillsRequiredForProgram { get; set; }
        public string ProgramBenefits { get; set; }
        public string ApplicationCriteria { get; set; }
        public ProgramType? ProgramType { get; set; }
        public DateTime ProgramStart { get; set; }
        public DateTime ApplicationOpen { get; set; }
        public DateTime ApplicationClose { get; set; }
        public Duration Duration { get; set; }
        public Location ProgramLocation { get; set; }
    }
}
