using System.ComponentModel.DataAnnotations;

namespace JobPortal.Models
{
  
        public class Requirement
        {
            [Key]
            public int Id { get; set; }

            [Required]
            public string Description { get; set; }
            public Job job { get; set; }

        }
    
}
