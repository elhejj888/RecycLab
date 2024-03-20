using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
namespace RecycLab.Models
{
	public class Dechet
	{
        [Key]
        public int IdDechet { get; set; }

        [Required]
        public string TypeDechet { get; set; }

        [Required]
        public float PrixUnitaire { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }

        public Dechet()
		{
		}
		public Dechet(string typeDechet , float prixU)
		{
			this.TypeDechet = typeDechet;
			this.PrixUnitaire = prixU;
		}
	}
}

