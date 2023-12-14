using System.ComponentModel.DataAnnotations;

namespace CRUDApi.Model
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }
}
