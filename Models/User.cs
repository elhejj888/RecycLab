using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecycLab.Models
{
    public class User : Person
    {

        public int Points { get; set; }
        public string UserType { get; } = "User";

        [ForeignKey("IdPerson")]
        public virtual Person Person { get; set; }
        // Define the one-to-many relationship with the Transaction class
        public virtual ICollection<Transaction> Transactions { get; set; }

        public User()
        {
		}

		public User(int points)
		{
			this.Points = points;
		}
	}
}

