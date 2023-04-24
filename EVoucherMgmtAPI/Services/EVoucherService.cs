using AutoMapper;
using EVoucherMgmtAPI.Data;
using EVoucherMgmtAPI.Dtos;
using EVoucherMgmtAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EVoucherMgmtAPI.Services
{
    public class EVoucherService : IEVoucherService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public EVoucherService(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<string> CreateEVoucher(CreateEVoucherDto newEVoucher)
        {
            try
            {
                if (newEVoucher.Quantity < 1)
                    return "Create Failed";

                for (var i = 0; i < newEVoucher.Quantity; i++)
                {
                    var dbEvoucher = _mapper.Map<EVoucher>(newEVoucher);
                    _dataContext.EVoucher.Add(dbEvoucher);
                }
                var result = await _dataContext.SaveChangesAsync();
                if (result > 0)
                {
                    return "Create Success";
                }
                return "Create Failed";
            }
            catch(Exception ex)
            {   
                return "Create Failed";
            }
        }

        public async Task<string> UpdateEVoucher(UpdateEVoucherDto updateEVoucher)
        {
            try
            {
                var dbEVoucher = _dataContext.EVoucher.FirstOrDefault(x => x.Id == updateEVoucher.Id && x.IsDelete == 0);
                if (dbEVoucher is null)
                    throw new Exception($"E-Voucher with Id '{updateEVoucher.Id}' not found.");

                dbEVoucher.Title = updateEVoucher.Title;
                dbEVoucher.Description = updateEVoucher.Description;
                dbEVoucher.ExpiredDate = updateEVoucher.ExpiredDate;
                dbEVoucher.Amount = updateEVoucher.Amount;
                dbEVoucher.InActive = updateEVoucher.InActive;
                dbEVoucher.UpdatedBy = 1;
                dbEVoucher.UpdatedDate = DateTime.Now;
                _dataContext.EVoucher.Update(dbEVoucher);
                var result = await _dataContext.SaveChangesAsync();
                if (result > 0)
                {
                    return "Update Success";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "Update Failed";
        }

        public async Task<string> SetEVoucherInActive(int eVoucherId)
        {
            try
            {
                var dbEVoucher = _dataContext.EVoucher.FirstOrDefault(x => x.Id == eVoucherId && x.IsDelete == 0);
                if (dbEVoucher is null)
                    throw new Exception($"E-Voucher with Id '{eVoucherId}' not found.");

                dbEVoucher.InActive = false;
                dbEVoucher.UpdatedBy = 1;
                dbEVoucher.UpdatedDate = DateTime.Now;
                _dataContext.EVoucher.Update(dbEVoucher);
                var result = await _dataContext.SaveChangesAsync();
                if (result > 0)
                {
                    return "Update Success";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "Update Failed";
        }

        public async Task<List<GetEVoucherDto>> GetAllEVouchers()
        {
            var dbEVouchers = await _dataContext.EVoucher.Where(x => x.IsDelete == 0)
                .Include(e => e.BuyTypeLimit)
                .Include(e => e.PaymentMethod)
                .ToListAsync();
            var eVouchers = dbEVouchers.Select(_mapper.Map<GetEVoucherDto>).ToList();
            return eVouchers;
        }

        public async Task<GetEVoucherDto> GetEVoucherById(int eVoucherId)
        {
            var dbEVoucher = await _dataContext.EVoucher
                .Include(e => e.BuyTypeLimit)
                .Include(e => e.PaymentMethod)
                .FirstOrDefaultAsync(x => x.Id == eVoucherId && x.IsDelete == 0);
            var eVoucher = _mapper.Map<GetEVoucherDto>(dbEVoucher);
            return eVoucher;
        }
    }
}
