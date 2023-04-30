namespace JobPortal.DTOs
{
    public class DestinationCreateJob
    {
        public int id { get; set; }
        public string Title { get; set; } = "";
        public string CompanyName { get; set; } = "";
        public string Location { get; set; } = "";
        public string Description { get; set; } = "";

        public string Qualifications { get; set; } = "";
        public string SalaryRange { get; set; } = "";

        public string ApplyHelp { get; set; } = "";
    }
}
