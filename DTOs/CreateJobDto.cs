using System.Data.SqlTypes;

namespace JobPortal.DTOs
{
    public class CreateJobDto
    {
        public string Title { get; set; } = "";
        public string CompanyName { get; set; } = "";
        public string Location { get; set; } = "";
        public string Description { get; set; } = "";

        public string Qualifications { get; set; } = "";
        public float Salary { get; set; } = 0;

        public string ApplyHelp { get; set; } = "";

        public JobType JobType { get; set; }//parttime,fulltime or contract

        public List<string> requirements { get; set; }

    }

    public enum JobType{
        PartTime,
        FullTime,
        Contract
    }
    
  
}
