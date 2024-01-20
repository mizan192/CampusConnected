using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CampusConnected.Models
{
    public class Department
    {
        [Key]
        public int DId { get; set; }
        

        [Column("Name", TypeName = "varchar(100)")]
        [Required] public string Name { get; set; } = string.Empty;
        [Column("Department Code", TypeName = "varchar(100)")]
        [Required] public string Code { get; set; } = string.Empty;
        [Required] public FacultyList Faculty { get; set; }


        public enum FacultyList
        {
            Arts,
            Engineering,
            Business,
            Pharmacy
        }


        


    }
}

