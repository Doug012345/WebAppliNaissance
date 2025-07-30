using System;
using System.ComponentModel.DataAnnotations;

namespace WebAppliNaissance.Models
{
    public class Naissance
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nom de l'enfant")]
        public required string NomEnfant { get; set; }

        [Required]
        [Display(Name = "Prénom de l'enfant")]
        public required string PrenomEnfant { get; set; }

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
        public required string LieuNaissance { get; set; }

        [Required]
        [Display(Name = "Sexe")]
        public required string Sexe { get; set; }

        [Required]
        [Display(Name = "Nom du père")]
        public required string NomPere { get; set; }

        [Required]
        [Display(Name = "Prénom du père")]
        public required string PrenomPere { get; set; }

        [Required]
        [Display(Name = "Nom de la mère")]
        public required string NomMere { get; set; }

        [Required]
        [Display(Name = "Prénom de la mère")]
        public required string PrenomMere { get; set; }

        [Display(Name = "Date d'enregistrement")]
        public DateTime DateEnregistrement { get; set; } = DateTime.Now;

        [Display(Name = "Numéro d'acte")]
        public string? NumeroActe { get; set; }  // si pas toujours obligatoire, sinon ajoute [Required] et retire le ?

        public int DeclarantId { get; set; }

        [Required]
        public required Declarant Declarant { get; set; }
    }
}
