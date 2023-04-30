using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace JobPortal.Models
{
    public class Job
    {
        [Key]
        public int id { get; set; }
        public string Title { get; set; }
        public string CompanyName { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }

        public string Qualifications { get; set; }
        public float Salary { get; set; }

        public string ApplyHelp { get; set; }

        public string JobType { get; set; }
        public DateTime creationDate { get; set; }
		public virtual ICollection<Requirement> Requirments { get; set; }

    }


}
