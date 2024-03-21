using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecycLab.Models
{
	public class Confirmation
	{
		[Key]
		public int IdConfirmation { get; set; }
		[ForeignKey("Transaction")]
		public int IdTransaction { get; set; }
		[ForeignKey("Collector")]
		public int IdCollector { get; set; }
		public DateTime ExecutionDate { get; set; }
        [Timestamp]
		public DateTime? ConfirmationDate { get; set; }

        public virtual Collector Collector { get; set; }
        public virtual Transaction Transaction { get; set; }



        public Confirmation()
		{
		}
		public Confirmation(int idTransaction , int idCollector , DateTime executionDate)
		{
			this.IdTransaction = idTransaction;
			this.IdCollector = idCollector;
			this.ExecutionDate = executionDate;
		}
	}
}

