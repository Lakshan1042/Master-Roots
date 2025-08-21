using System.ComponentModel.DataAnnotations;

namespace Master_Roots.Models
{
    public class Student
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Standard { get; set; }
        [Required]
        public char Section { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public char Gender { get; set; }
        [Required]
        public string Father_Name { get; set; }
        [Required]

        public long Father_Mobile_Number { get; set; }

        [Required]
        public long Mother_Mobile_Number { get; set; }

        [Required]
        public string Mother_Name { get; set; }
        [Required]

        public string Address { get; set; }

        [Required]
        public string Blood_Group { get; set; }

        public Student()
        {
            
        }

    }
}
