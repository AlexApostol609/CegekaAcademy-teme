using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShelterDemo.Domain
{
    public class Donations
    {
        private readonly IDictionary<string, int> donations;

        public Donations()
        {
            donations = new Dictionary<string, int>();
        }

        public void AddDonation(string currency, int amount)
        {
            if (!donations.ContainsKey(currency))
            {
                donations[currency] = 0;
            }
            donations[currency] += amount;
        }

        public int GetTotalDonations(string currency)
        {
            if (!donations.ContainsKey(currency))
            {
                return 0;
            }
            return donations[currency];
        }
    }
}
