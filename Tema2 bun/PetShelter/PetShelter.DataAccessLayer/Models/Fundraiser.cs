using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShelter.DataAccessLayer.Models
{
    public class Fundraiser : IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

        public string Target { get; set; }
        public float TotalDonations { get; set; }

        public ICollection<Donation> FundraiserDonations { get; set; }
        public ICollection<Person> Donors { get; set; }





    }
}
