using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShelterDemo.Domain
{
    public interface AddFundRegistry<T> where T : TitleEntity
    {
        
        Task<T> GetByTitle(string title);
        Task Register(T instance);
        Task<IReadOnlyList<T>> GetAll();
    }
}
