﻿using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repository
{
    public interface ISerilogRepository
    {
        Task<IEnumerable<SeriLog>> GetAllAsync();
        Task<SeriLog> GetByIdAsync(int id);
        Task<int> AddAsync(SeriLog entity);
        Task<bool> UpdateAsync(SeriLog entity);
        Task<bool> DeleteAsync(int id);
    }
}
