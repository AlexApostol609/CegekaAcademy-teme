﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShelterDemo.Domain
{
        public interface TitleRegistry<T> where T : TitleEntity
    {
        Task Register(T instance);
        Task<IReadOnlyList<T>> GetAll();
        Task<T> GetByName(string name);
        Task<T> GetByTitle(string title);
        Task<IReadOnlyList<T>> Find(Func<T, bool> filter);
    }
}
