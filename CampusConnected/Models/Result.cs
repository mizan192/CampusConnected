using System.ComponentModel.DataAnnotations;

namespace CampusConnected.Models
{
    public class Result
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        
    }
}
