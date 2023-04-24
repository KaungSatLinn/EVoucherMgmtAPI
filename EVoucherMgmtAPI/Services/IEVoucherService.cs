using EVoucherMgmtAPI.Dtos;

namespace EVoucherMgmtAPI.Services
{
    public interface IEVoucherService
    {
        Task<string> CreateEVoucher(CreateEVoucherDto newEVoucher);
        Task<string> UpdateEVoucher(UpdateEVoucherDto updateEVoucher);
        Task<string> SetEVoucherInActive(int eVoucherId);
        Task<List<GetEVoucherDto>> GetAllEVouchers();
        Task<GetEVoucherDto> GetEVoucherById(int eVoucherId);
    }
}
