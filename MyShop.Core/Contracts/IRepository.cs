﻿using System.Linq;
using MyShop.Core.Models;

namespace MyShop.Core.Contracts
{
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> Collection();
        T Find(string Id);
        void Commit();
        void Insert(T t);
        void Update(T t);
        void Delete(string Id);
    }
}