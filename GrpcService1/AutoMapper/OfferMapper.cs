using AutoMapper;
using GrpcService1.Entities;
using GrpcService1.Protos;

namespace GrpcService1.AutoMapper
{
    public class OfferMapper : Profile
    {
        public OfferMapper()
        {
            CreateMap<Offer, OfferDetail>().ReverseMap();
        }
    }
}
