using AutoMapper;
using Grpc.Core;
using GrpcService1.Entities;
using GrpcService1.Protos;
using GrpcService1.Repositories;

namespace GrpcService1.Services
{
    public class OfferService : Protos.ProductOfferService.ProductOfferServiceBase
    { 
        private readonly IProductOfferService _prductOfferService;
        private readonly IMapper _mapper;

        public OfferService(IProductOfferService prductOfferService, IMapper mapper)
        {
            _prductOfferService = prductOfferService;
            _mapper = mapper;
        }

        public async override Task<Offers> GetOfferList(Empty request, ServerCallContext context)
        {
            var offersData = await _prductOfferService.GetOfferListAsync();

            Offers response = new Offers();
            foreach (Offer offer in offersData)
            {
                response.Items.Add(_mapper.Map<OfferDetail>(offer));
            }

            return response;
        }

        public async override Task<OfferDetail> GetOffer(GetOfferDetailRequest request, ServerCallContext context)
        {
            var offer = await _prductOfferService.GetOfferByIdAsync(request.ProductId);
            var offerDetail = _mapper.Map<OfferDetail>(offer);
            return offerDetail;
        }

        public async override Task<OfferDetail> CreateOffer(CreateOfferDetailRequest request, ServerCallContext context)
        {
            var offer = _mapper.Map<Offer>(request.Offer);

            await _prductOfferService.AddOfferAsync(offer);

            var offerDetail = _mapper.Map<OfferDetail>(offer);
            return offerDetail;
        }

        public async override Task<OfferDetail> UpdateOffer(UpdateOfferDetailRequest request, ServerCallContext context)
        {
            var offer = _mapper.Map<Offer>(request.Offer);

            await _prductOfferService.UpdateOfferAsync(offer);

            var offerDetail = _mapper.Map<OfferDetail>(offer);
            return offerDetail;
        }

        public async override Task<DeleteOfferDetailResponse> DeleteOffer(DeleteOfferDetailRequest request, ServerCallContext context)
        {
            var isDeleted = await _prductOfferService.DeleteOfferAsync(request.ProductId);
            var response = new DeleteOfferDetailResponse
            {
                IsDelete = isDeleted
            };

            return response;
        }
    }
}
