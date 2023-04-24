using AutoMapper;
using EVoucherMgmtAPI.Dtos;
using EVoucherMgmtAPI.Models;

namespace EVoucherMgmtAPI
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<EVoucher, CreateEVoucherDto>().ReverseMap();
            CreateMap<EVoucher, UpdateEVoucherDto>().ReverseMap();
            CreateMap<EVoucher, GetEVoucherDto>().ReverseMap();
            CreateMap<PromoCode, PromoCodeDto>().ReverseMap();
        }
    }
}
