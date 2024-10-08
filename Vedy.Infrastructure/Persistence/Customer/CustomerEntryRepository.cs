﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vedy.Application.Interfaces;
using Vedy.Data;

namespace Vedy.Infrastructure.Persistence.Users
{
    public class CustomerEntryRepository :GenericRepository<CustomerEntry>,  ICustomerEntryRepository
    {
        public CustomerEntryRepository(IAppDbContext context) :base(context) 
        {
        }

        public Task<List<CustomerEntry>> GetAll()
        {
            return context.CustomerEntries
                .Include(x => x.Company)
                .Include(x => x.Settlement)
                .ToListAsync();
        }

        public Task<List<CustomerEntry>> GetByDate(DateTime startDate, DateTime endDate)
        {
            var query = context.CustomerEntries
                .Where(x => x.CreatedDate >= startDate && x.CreatedDate <= endDate).AsQueryable();
            return query
                .Include(x => x.Company)
                .Include(x => x.Settlement)
                .ToListAsync();
        }
    }
}
