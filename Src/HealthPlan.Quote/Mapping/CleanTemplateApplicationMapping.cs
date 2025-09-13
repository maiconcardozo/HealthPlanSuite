using HealthPlan.Quote.Domain.Implementation;
using HealthPlan.Quote.DTO;
using AutoMapper;

namespace HealthPlan.Quote.Mapping
{
    public class CleanTemplateApplicationMapping : Profile
    {
        public CleanTemplateApplicationMapping()
        {
            // Quote mappings
            CreateMap<QuotePayLoadDTO, Domain.Implementation.Quote>()
                .ForMember(dest => dest.QuoteNumber, opt => opt.Ignore())
                .ForMember(dest => dest.QuoteDate, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.Ignore())
                .ForMember(dest => dest.DtCreated, opt => opt.Ignore())
                .ForMember(dest => dest.DtUpdated, opt => opt.Ignore())
                .ForMember(dest => dest.DtDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.DeletedBy, opt => opt.Ignore());
            
            CreateMap<Domain.Implementation.Quote, QuotePayLoadDTO>();
            CreateMap<Domain.Implementation.Quote, QuoteResponseDTO>();
            CreateMap<QuoteResponseDTO, Domain.Implementation.Quote>();
        }
    }
}