using JobPortal.Models;

namespace JobPortal.DTOs
{
    public class EditJobDTO
    {
        public int id { get; set; }
        public string Title { get; set; } = "";
        public string CompanyName { get; set; } = "";
        public string Location { get; set; } = "";
        public string Description { get; set; } = "";

        public string Qualifications { get; set; } = "";
        public float Salary { get; set; }

        public string ApplyHelp { get; set; } = "";

        public ICollection<Requirement> Requirements { get; set; }
    }
}
