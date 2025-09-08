using AutoMapper;
using HealthPlanSuite.Quote.Domain.Implementation;
using HealthPlanSuite.Quote.DTO;

namespace HealthPlanSuite.Quote.Mapping
{
    /// <summary>
    /// Profile do AutoMapper para mapeamento da Operadora
    /// </summary>
    public class OperadoraProfile : Profile
    {
        public OperadoraProfile()
        {
            // Entity -> DTO
            CreateMap<Operadora, OperadoraDto>();

            // CreateDto -> Entity
            CreateMap<OperadoraCreateDto, Operadora>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.DataCriacao, opt => opt.Ignore())
                .ForMember(dest => dest.DataAtualizacao, opt => opt.Ignore())
                .ForMember(dest => dest.Planos, opt => opt.Ignore());
        }
    }
}