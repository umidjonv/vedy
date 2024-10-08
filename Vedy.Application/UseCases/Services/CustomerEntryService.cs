﻿using Vedy.Application.Interfaces;
using Vedy.Common.DTOs.Company;
using Vedy.Common.DTOs.CustomerEntry;
using Vedy.Common.DTOs.User;
using Vedy.Data;

namespace Vedy.Infrastructure.Services
{
    public class CustomerEntryService
    {
        private readonly ICustomerEntryRepository _customerRepository;
        private readonly ICompanyRepository _companyRepository;

        public CustomerEntryService(ICustomerEntryRepository customerRepository,
                            ICompanyRepository companyRepository) 
        {
            this._customerRepository = customerRepository;
            this._companyRepository = companyRepository;
        }

        public async Task<List<CustomerEntryModel>> GetByDate(DateRangeModel dateRange)
        {
            var entries = await _customerRepository.GetByDate(dateRange.StartDate, dateRange.EndDate);
            var response = new List<CustomerEntryModel>();
            foreach (var customerEntry in entries)
            {

                response.Add(new CustomerEntryModel
                {
                    Id = customerEntry.Id,
                    CarNumber = customerEntry.CarNumber,
                    Amount = customerEntry.Amount,
                    Sum = customerEntry.Sum,
                    CompanyId = customerEntry.CompanyId,
                    CompanyName = customerEntry.Company.CompanyName,
                    CreatedDate = customerEntry.CreatedDate,
                    FullName = customerEntry.FullName,
                    SettlementDate = customerEntry.Settlement == null ? null : customerEntry.Settlement.Date ,
                    SettlementId = customerEntry.Settlement == null ? null : customerEntry.SettlementId,
                    SettlementNumber = customerEntry.Settlement == null ? null : customerEntry.Settlement.Number,
                    SignHash = customerEntry.SignHash,
                });
            }

            return response;
        }

        public async Task<List<CustomerEntryModel>> GetAll()
        {
            var entries = await _customerRepository.GetAll();
            var response = new List<CustomerEntryModel>();
            foreach (var customerEntry in entries)
            {

                response.Add(new CustomerEntryModel 
                {
                    Id = customerEntry.Id,
                    CarNumber = customerEntry.CarNumber,
                    Amount = customerEntry.Amount,
                    Sum = customerEntry.Sum,
                    CompanyId = customerEntry.CompanyId,
                    CompanyName = customerEntry.Company.CompanyName,
                    CreatedDate = customerEntry.CreatedDate,
                    FullName = customerEntry.FullName,
                    SettlementDate = customerEntry.Settlement.Date,
                    SettlementId = customerEntry.SettlementId,
                    SettlementNumber = customerEntry.Settlement.Number,
                    SignHash = customerEntry.SignHash,
                });
            }

            return response;
        }

        public async Task<CustomerEntryModel?> Add(CustomerEntryModel entry)
        {
            var result = await _customerRepository.AddAsync(new CustomerEntry
            {
                FullName = entry.FullName,
                CompanyId = entry.CompanyId,
                CreatedDate = entry.CreatedDate,
                Amount = entry.Amount,
                Sum = entry.Sum,
                CarNumber = entry.CarNumber,
                SettlementId = entry.SettlementId,
                SignHash = entry.SignHash
                
            });

            return new CustomerEntryModel 
            {
                Id = result.Id,
                CompanyId = result.CompanyId,
                CreatedDate = result.CreatedDate,
                FullName = result.FullName,
                CarNumber = result.CarNumber,
                SignHash= result.SignHash,
                SettlementId= result.SettlementId,
                Amount= result.Amount,
                Sum = result.Sum,
                CompanyName = entry.CompanyName,
                SettlementDate = entry.SettlementDate,
                SettlementNumber = entry.SettlementNumber  
            };
        }

        public async Task Update(CustomerEntryModel entry)
        {
            if (entry.Id == null)
            {
                throw new Exception("Id not found");
            }

            await _customerRepository.UpdateAsync(new CustomerEntry
            {
                Id = entry.Id.Value,
                FullName = entry.FullName,
                CompanyId = entry.CompanyId,
                CreatedDate = entry.CreatedDate,
                Amount = entry.Amount,
                Sum = entry.Sum,
                CarNumber = entry.CarNumber,
                SettlementId = entry.SettlementId,
                SignHash = entry.SignHash

            });
        }

        public async Task UpdateEntries(IEnumerable<CustomerEntryModel> entries, long settlementId)
        {
            
            //foreach()
            //await _customerRepository.UpdateAsync();
        }

        public async Task Delete(long id)
        {
            await _customerRepository.DeleteAsync(id);
        }
    }
}
