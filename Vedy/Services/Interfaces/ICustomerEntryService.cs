﻿using Vedy.Common.DTOs.Company;
using Vedy.Common.DTOs.CustomerEntry;
using Vedy.Models;

namespace Vedy.Services.Interfaces
{
    public interface ICustomerEntryService
    {
        Task<CustomerEntryModel> GetById(int id, CancellationToken cancellationToken);

        Task<CustomerEntryModel> GetByName(string name, CancellationToken cancellationToken);
        Task<List<CustomerEntryModel>> GetByDate(DateTime startDate, DateTime endDate, CancellationToken cancellationToken);

        Task<PaginationModel<CustomerEntryModel>> GetPage(int page, int size, CancellationToken cancellationToken);
        Task<List<CustomerEntryModel>> GetList(CancellationToken cancellationToken);

        Task<CustomerEntryModel> Add(CustomerEntryModel entry, CancellationToken cancellationToken);
        Task<bool> Update(CustomerEntryModel entry, CancellationToken cancellationToken);
        Task<bool> Update(List<CustomerEntryModel> entries, long settlementId, CancellationToken cancellationToken);
        Task<long> Delete(long id, CancellationToken cancellationToken);
    }
}
