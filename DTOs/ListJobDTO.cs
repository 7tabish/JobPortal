namespace JobPortal.DTOs
{
    public class ListJobDTO
    {
        public int id { get; set; } = 0;
        public string Title { get; set; } = "";
        public string CompanyName { get; set; } = "";
        public string Location { get; set; } = "";
        public string Description { get; set; } = "";

        public float Salary { get; set; }

        public string JobType { get; set; } = "";

        public DateTime CreationDate { get; set; }

    }
}
