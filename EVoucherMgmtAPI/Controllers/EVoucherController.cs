using EVoucherMgmtAPI.Dtos;
using EVoucherMgmtAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EVoucherMgmtAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EVoucherController : ControllerBase
    {
        private readonly IEVoucherService _eVoucherService;

        public EVoucherController(IEVoucherService eVoucherService)
        {
            _eVoucherService = eVoucherService;
        }

        [HttpPost("CreateEVoucher")]
        public async Task<ActionResult<string>> CreateEVoucher(CreateEVoucherDto newEVoucher)
        {
            var response = await _eVoucherService.CreateEVoucher(newEVoucher);
            return Ok(response);
        }

        [HttpPut("UpdateEVoucher")]
        public async Task<ActionResult<string>> UpdateEVoucher(UpdateEVoucherDto updateEVoucher)
        {
            var result = await _eVoucherService.UpdateEVoucher(updateEVoucher);
            if (result == null)
                return NotFound("E-Voucher not found.");
            return Ok(result);
        }

        [HttpPut("SetEVoucherInActive/{eVoucherId}")]
        public async Task<ActionResult<string>> SetEVoucherInActive(int eVoucherId)
        {
            var result = await _eVoucherService.SetEVoucherInActive(eVoucherId);
            if (result == null)
                return NotFound("E-Voucher not found.");
            return Ok(result);
        }

        [HttpGet("GetAllEVouchers")]
        public async Task<ActionResult<List<GetEVoucherDto>>> GetAllEVouchers()
        {
            return Ok(await _eVoucherService.GetAllEVouchers());
        }

        [HttpGet("Detail/{eVoucherId}")]
        public async Task<ActionResult<GetEVoucherDto>> GetEVoucherById(int eVoucherId)
        {
            var result = await _eVoucherService.GetEVoucherById(eVoucherId);
            if (result == null)
                return NotFound("E-Voucher not found.");
            return Ok(result);
        }
    }
}
