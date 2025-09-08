using AutoMapper;
using HealthPlanSuite.Quote.Domain.Implementation;
using HealthPlanSuite.Quote.DTO;

namespace HealthPlanSuite.Quote.Mapping
{
    /// <summary>
    /// Profile do AutoMapper para mapeamento da Cotação
    /// </summary>
    public class CotacaoProfile : Profile
    {
        public CotacaoProfile()
        {
            // Entity -> DTO
            CreateMap<Cotacao, CotacaoDto>()
                .ForMember(dest => dest.BeneficiarioTitularNome, opt => opt.MapFrom(src => src.BeneficiarioTitular.Nome));

            // Entity -> ResumoDto
            CreateMap<Cotacao, CotacaoResumoDto>()
                .ForMember(dest => dest.BeneficiarioTitularNome, opt => opt.MapFrom(src => src.BeneficiarioTitular.Nome))
                .ForMember(dest => dest.CPF, opt => opt.MapFrom(src => src.BeneficiarioTitular.CPF))
                .ForMember(dest => dest.QuantidadePlanos, opt => opt.MapFrom(src => src.ItensCotacao.Count))
                .ForMember(dest => dest.PlanosSelecionados, opt => opt.MapFrom(src => src.ItensCotacao.Count(i => i.Selecionado)));

            // CreateDto -> Entity
            CreateMap<CotacaoCreateDto, Cotacao>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Protocolo, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.Ignore())
                .ForMember(dest => dest.DataCotacao, opt => opt.Ignore())
                .ForMember(dest => dest.DataExpiracao, opt => opt.Ignore())
                .ForMember(dest => dest.ValorTotalMensal, opt => opt.Ignore())
                .ForMember(dest => dest.DataCriacao, opt => opt.Ignore())
                .ForMember(dest => dest.DataAtualizacao, opt => opt.Ignore())
                .ForMember(dest => dest.BeneficiarioTitular, opt => opt.Ignore())
                .ForMember(dest => dest.ItensCotacao, opt => opt.Ignore())
                .ForMember(dest => dest.BeneficiariosCotacao, opt => opt.Ignore());

            // ItemCotacao Entity -> DTO
            CreateMap<ItemCotacao, ItemCotacaoDto>()
                .ForMember(dest => dest.PlanoNome, opt => opt.MapFrom(src => src.Plano.Nome))
                .ForMember(dest => dest.PlanoOperadora, opt => opt.MapFrom(src => src.Plano.Operadora.Nome));

            // BeneficiarioCotacao Entity -> DTO
            CreateMap<BeneficiarioCotacao, BeneficiarioCotacaoDto>()
                .ForMember(dest => dest.FaixaEtariaNome, opt => opt.MapFrom(src => src.FaixaEtaria.Nome));

            // BeneficiarioCotacao CreateDto -> Entity
            CreateMap<BeneficiarioCotacaoCreateDto, BeneficiarioCotacao>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CotacaoId, opt => opt.Ignore())
                .ForMember(dest => dest.FaixaEtariaId, opt => opt.Ignore())
                .ForMember(dest => dest.DataCriacao, opt => opt.Ignore())
                .ForMember(dest => dest.DataAtualizacao, opt => opt.Ignore())
                .ForMember(dest => dest.Cotacao, opt => opt.Ignore())
                .ForMember(dest => dest.Beneficiario, opt => opt.Ignore())
                .ForMember(dest => dest.Dependente, opt => opt.Ignore())
                .ForMember(dest => dest.FaixaEtaria, opt => opt.Ignore());
        }
    }
}