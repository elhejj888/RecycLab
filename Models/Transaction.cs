using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecycLab.Models
{
    public class Transaction
    {
        [Key]
        public int IdTransaction { get; set; }

        [ForeignKey("User")]
        public int IdUser { get; set; }

        [ForeignKey("Dechet")]
        public int IdDechet { get; set; }

        [Required]
        public string Etat { get; set; }

        [Required]
        public float Quantity { get; set; }

        public float TotalPrice { get; set; }

        public bool isConfirmed { get; set; }

        public string TransactionType { get; set; }

        [Timestamp]
        public DateTime? TransactionDate { get; set; }

        // Navigation property for the User entity
        public virtual User User { get; set; }

        // Navigation property for the Dechet entity
        public virtual Dechet Dechet { get; set; }

        // Navigation property for the Confirmation entity
        public virtual Confirmation Confirmation { get; set; }

        public Transaction()
		{
		}

		public Transaction(int idUser , int idDechet ,float quantity , float totalPrice ,string etat, string transactionType)
		{
			this.IdUser = idUser;
            this.Etat = etat;
			this.IdDechet = idDechet;
			this.Quantity = quantity;
			this.TotalPrice = totalPrice;
			this.TransactionType = transactionType;
		}
	}
}

