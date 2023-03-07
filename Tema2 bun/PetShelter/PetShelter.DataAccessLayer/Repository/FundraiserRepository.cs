using Microsoft.EntityFrameworkCore;
using PetShelter.DataAccessLayer.Models;
using PetShelter.DataAccessLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShelter.DataAccessLayer.Repository
{
    public class FundraiserRepository : BaseRepository<Fundraiser>, IFundraiserRepository
    {
        public FundraiserRepository(PetShelterContext context) : base(context)
        {
        }

        public decimal GetCurrentRaisedAmount(int fundraiserId)
        {
            decimal totalDonations = 0;

            var donations = _context.Donations
                .Where(p => p.FundraiserId == fundraiserId)
                .ToList();

            foreach (var donation in donations)
            {
                totalDonations += donation.Amount;
            }

            return totalDonations;
        }
    }
}


