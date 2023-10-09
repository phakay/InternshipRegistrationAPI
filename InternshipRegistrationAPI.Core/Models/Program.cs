using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipRegistrationAPI.Core.Models
{
    public class Program
    {
        public string Id { get; set; }
        [Required]
        public  string ProgramTitle { get; set; }
        public string ProgramSummary { get; set; }
        [Required]
        public string ProgramDescription { get; set; }
        public string SkillsRequiredForProgram { get; set; }
        public string ProgramBenefits { get; set; }
        public string ApplicationCriteria { get; set; }
        public AdditionalProgramInformation AdditionalProgramInformation { get; set; }
    }

    public class AdditionalProgramInformation
    {
        [Required]
        public ProgramType ProgramType { get; set; }
        public DateTime ProgramStart { get; set; }
        [Required]
        public DateTime ApplicationOpen { get; set; }
        [Required]
        public DateTime ApplicationClose { get; set; }
        public Duration Duration { get; set; }
        [Required]
        public Location ProgramLocation { get; set; }
        public Qualification MinimumQualification { get; set; }
        [Range(0, int.MaxValue)]
        public int MaxNumberOfApplication { get; set; }
    }


    public class Location
    {
        public string CountryShortName { get; set; }
        public string City { get; set; }
        public bool FullyRemote { get; set; }
    }

    public class Duration
    {
        public int Value { get; set; }
        public DurationUnit Unit { get; set; }
    }

    public enum Qualification
    {
        HighSchool,
        University
    }

    public enum DurationUnit
    {
        Day,
        Month,
        Year
    }
    public enum ProgramType
    {
        FullTime,
        Partime
    }
}
