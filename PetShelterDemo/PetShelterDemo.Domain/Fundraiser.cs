using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShelterDemo.Domain
{
    public class Fundraiser  :  TitleEntity
    {

        public string Title { get; }
        public string Description { get; }
        public string Target { get; }

        public Fundraiser(string title, string description, string target)
        {
            Title = title;
            Description = description;
            Target = target;
        }
    }
}
