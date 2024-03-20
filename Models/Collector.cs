using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecycLab.Models
{
	public class Collector : Person
    {

        public string? Auto { get; set; }

        public string? TypeAuto { get; set; }

        public int Points { get; set; }

        public bool isApproved { get; set; } = false;

        public string UserType { get; } = "Collector";

        // Define the one-to-one relationship with the Person class
        [ForeignKey("IdPerson")]
        public virtual Person Person { get; set; }

        // Define the one-to-many relationship with the Confirmation class
        public virtual ICollection<Confirmation> Confirmations { get; set; }

        public Collector()
		{
		}

		public Collector(string auto , string typeAuto)
		{
			this.Auto = auto;
			this.TypeAuto = typeAuto;
		}
	}
}

