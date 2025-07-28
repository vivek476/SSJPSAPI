using System.ComponentModel.DataAnnotations;

namespace SSJPSAPI.Model
{
    public class Postjob
    {
        [Key]
        public int Id { get; set; }

        public string Jobtitle { get; set; }
        public string Degree { get; set; }
        public string Skill { get; set; }
        public string Experience { get; set; }
        public string Salary { get; set; }
        public string Vacancy { get; set; }
        public string Detail { get; set; }
    }
}
