using AutoMapper;
using InvoiceTask.Models;

namespace InvoiceTask.Dto
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Cashier, CashierToReturn>()
                 .ForMember(dest => dest.BranchName, opt =>
                    opt.MapFrom(src => src.Branch.BranchName));

            //CreateMap<CashierDto, Cashier>()
            //    .ForMember(dest => dest., opt =>
            //       opt.b;


        }
    }
}
