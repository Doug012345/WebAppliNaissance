using System.ComponentModel.DataAnnotations;

namespace WebAppliNaissance.Models
{
    public class Naissance
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nom de l'enfant")]
        public string NomEnfant { get; set; }

        [Required]
        [Display(Name = "Prénom de l'enfant")]
        public string PrenomEnfant { get; set; }

        [Required]
        [Display(Name = "Date de naissance")]
        [DataType(DataType.Date)]
        public DateTime DateNaissance { get; set; }

        [Required]
        [Display(Name = "Heure de naissance")]
        [DataType(DataType.Time)]
        public DateTime HeureNaissance { get; set; }

        [Required]
        [Display(Name = "Lieu de naissance")]
        public string LieuNaissance { get; set; }

        [Required]
        [Display(Name = "Sexe")]
        public string Sexe { get; set; }

        [Required]
        [Display(Name = "Nom du père")]
        public string NomPere { get; set; }

        [Required]
        [Display(Name = "Prénom du père")]
        public string PrenomPere { get; set; }

        [Required]
        [Display(Name = "Nom de la mère")]
        public string NomMere { get; set; }

        [Required]
        [Display(Name = "Prénom de la mère")]
        public string PrenomMere { get; set; }

        [Display(Name = "Date d'enregistrement")]
        public DateTime DateEnregistrement { get; set; } = DateTime.Now;

        [Display(Name = "Numéro d'acte")]
        public string NumeroActe { get; set; }

        public int DeclarantId { get; set; }
        public Declarant Declarant { get; set; }
    }
}
