using HealthPlan.Domain.Entities;
using HealthPlan.Application.DTOs;
using AutoMapper;

namespace HealthPlan.Application.Mappers
{
    public class CleanTemplateApplicationMapping : Profile
    {
        public CleanTemplateApplicationMapping()
        {
            // Quote mappings
            CreateMap<QuotePayLoadDTO, Domain.Entities.Quote>()
                .ForMember(dest => dest.QuoteNumber, opt => opt.Ignore())
                .ForMember(dest => dest.QuoteDate, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.Ignore())
                .ForMember(dest => dest.DtCreated, opt => opt.Ignore())
                .ForMember(dest => dest.DtUpdated, opt => opt.Ignore())
                .ForMember(dest => dest.DtDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.DeletedBy, opt => opt.Ignore());
            
            CreateMap<Domain.Entities.Quote, QuotePayLoadDTO>();
            CreateMap<Domain.Entities.Quote, QuoteResponseDTO>();
            CreateMap<QuoteResponseDTO, Domain.Entities.Quote>();

            // AdhesionFee mappings
            CreateMap<AdhesionFeePayLoadDTO, AdhesionFee>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.HealthPlan, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.Ignore())
                .ForMember(dest => dest.DtCreated, opt => opt.Ignore())
                .ForMember(dest => dest.DtUpdated, opt => opt.Ignore())
                .ForMember(dest => dest.DtDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.DeletedBy, opt => opt.Ignore());
            
            CreateMap<AdhesionFee, AdhesionFeePayLoadDTO>();
            CreateMap<AdhesionFee, AdhesionFeeResponseDTO>();
            CreateMap<AdhesionFeeResponseDTO, AdhesionFee>();

            // PromotionalDiscount mappings
            CreateMap<PromotionalDiscountPayLoadDTO, PromotionalDiscount>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.HealthPlan, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.Ignore())
                .ForMember(dest => dest.DtCreated, opt => opt.Ignore())
                .ForMember(dest => dest.DtUpdated, opt => opt.Ignore())
                .ForMember(dest => dest.DtDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.DeletedBy, opt => opt.Ignore());
            
            CreateMap<PromotionalDiscount, PromotionalDiscountPayLoadDTO>();
            CreateMap<PromotionalDiscount, PromotionalDiscountResponseDTO>();
            CreateMap<PromotionalDiscountResponseDTO, PromotionalDiscount>();

            // ProcedureCoparticipation mappings
            CreateMap<ProcedureCoparticipationPayLoadDTO, ProcedureCoparticipation>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.HealthPlan, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.Ignore())
                .ForMember(dest => dest.DtCreated, opt => opt.Ignore())
                .ForMember(dest => dest.DtUpdated, opt => opt.Ignore())
                .ForMember(dest => dest.DtDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.DeletedBy, opt => opt.Ignore());
            
            CreateMap<ProcedureCoparticipation, ProcedureCoparticipationPayLoadDTO>();
            CreateMap<ProcedureCoparticipation, ProcedureCoparticipationResponseDTO>();
            CreateMap<ProcedureCoparticipationResponseDTO, ProcedureCoparticipation>();

            // PlanPriceRange mappings
            CreateMap<PlanPriceRangePayLoadDTO, PlanPriceRange>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.HealthPlan, opt => opt.Ignore())
                .ForMember(dest => dest.AgeRange, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.Ignore())
                .ForMember(dest => dest.DtCreated, opt => opt.Ignore())
                .ForMember(dest => dest.DtUpdated, opt => opt.Ignore())
                .ForMember(dest => dest.DtDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.DeletedBy, opt => opt.Ignore());
            
            CreateMap<PlanPriceRange, PlanPriceRangePayLoadDTO>();
            CreateMap<PlanPriceRange, PlanPriceRangeResponseDTO>();
            CreateMap<PlanPriceRangeResponseDTO, PlanPriceRange>();
        }
    }
}