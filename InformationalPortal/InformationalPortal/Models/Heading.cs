using System.ComponentModel.DataAnnotations;


namespace InformationalPortal.Models
{
    public class Heading
    {
        public int Id { get; set; }

        [Required(ErrorMessage="Heading is required")]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}