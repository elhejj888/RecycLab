using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RecycLab.Models;

public class Person
{
    [Key]
    public int IdPerson { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public DateTime BirthDate { get; set; }



    // Define the one-to-many relationship with the User class
    public virtual ICollection<User> Users { get; set; }

    // Define the one-to-many relationship with the Collector class
    public virtual ICollection<Collector> Collectors { get; set; }

    public Person()
		{
			
		}

		public Person(string firstName , string lastName ,string email, string address , string phone , DateTime birthDate)
		{
			this.FirstName = firstName;
			this.LastName = lastName;
            this.Email = email;
			this.Address = address;
			this.PhoneNumber = phone;
			this.BirthDate = birthDate;
		}
}
		
	


