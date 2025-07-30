using System.ComponentModel.DataAnnotations;

namespace WebAppliNaissance.Models
{
    public class Declarant
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nom")]
        public string Nom { get; set; }

        [Required]
        [Display(Name = "Prénom")]
        public string Prenom { get; set; }

        [Required]
        [Display(Name = "Adresse")]
        public string Adresse { get; set; }

        [Required]
        [Display(Name = "CNI")]
        public string CNI { get; set; }

        [Required]
        [Display(Name = "Lien avec l'enfant")]
        public string LienAvecEnfant { get; set; }
    }
}
