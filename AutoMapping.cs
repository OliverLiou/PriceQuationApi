using AutoMapper;
using PriceQuationApi.Model;
using PriceQuationApi.ViewModels;

namespace PriceQuationApi
{
    public class AutoMapping: Profile
    {
        public AutoMapping()
        {
            CreateMap<AdminUser, VUser>().ReverseMap();
        }
    }
}