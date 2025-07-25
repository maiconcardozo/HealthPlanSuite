using AutoMapper;
using HealthPlanSuite.Core.Domain.Implementation;
using HealthPlanSuite.Core.DTO;

namespace HealthPlanSuite.Core.Mapping
{
    public class HealthPlanSuiteMappingProfile : Profile
    {
        public HealthPlanSuiteMappingProfile()
        {
            // Operadora mappings
            CreateMap<Operadora, OperadoraResponseDTO>();
            CreateMap<OperadoraPayLoadDTO, Operadora>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.DataCriacao, opt => opt.Ignore())
                .ForMember(dest => dest.DataAtualizacao, opt => opt.Ignore())
                .ForMember(dest => dest.Planos, opt => opt.Ignore());

            // TipoPlano mappings
            CreateMap<TipoPlano, TipoPlanoResponseDTO>();
            CreateMap<TipoPlanoPayLoadDTO, TipoPlano>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.DataCriacao, opt => opt.Ignore())
                .ForMember(dest => dest.DataAtualizacao, opt => opt.Ignore())
                .ForMember(dest => dest.Planos, opt => opt.Ignore());

            // Plano mappings
            CreateMap<Plano, PlanoResponseDTO>()
                .ForMember(dest => dest.OperadoraNome, opt => opt.MapFrom(src => src.Operadora.Nome))
                .ForMember(dest => dest.TipoPlanoDescricao, opt => opt.MapFrom(src => src.TipoPlano.Descricao));
            CreateMap<PlanoPayLoadDTO, Plano>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.DataCriacao, opt => opt.Ignore())
                .ForMember(dest => dest.DataAtualizacao, opt => opt.Ignore())
                .ForMember(dest => dest.Operadora, opt => opt.Ignore())
                .ForMember(dest => dest.TipoPlano, opt => opt.Ignore())
                .ForMember(dest => dest.TabelasPreco, opt => opt.Ignore())
                .ForMember(dest => dest.Reajustes, opt => opt.Ignore())
                .ForMember(dest => dest.PlanoAbrangencias, opt => opt.Ignore());

            // FaixaEtaria mappings
            CreateMap<FaixaEtaria, FaixaEtariaResponseDTO>();
            CreateMap<FaixaEtariaPayLoadDTO, FaixaEtaria>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.DataCriacao, opt => opt.Ignore())
                .ForMember(dest => dest.DataAtualizacao, opt => opt.Ignore())
                .ForMember(dest => dest.TabelasPreco, opt => opt.Ignore());

            // TabelaPreco mappings
            CreateMap<TabelaPreco, TabelaPrecoResponseDTO>()
                .ForMember(dest => dest.PlanoNome, opt => opt.MapFrom(src => src.Plano.Nome))
                .ForMember(dest => dest.FaixaEtariaDescricao, opt => opt.MapFrom(src => src.FaixaEtaria.Descricao));
            CreateMap<TabelaPrecoPayLoadDTO, TabelaPreco>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.DataCriacao, opt => opt.Ignore())
                .ForMember(dest => dest.DataAtualizacao, opt => opt.Ignore())
                .ForMember(dest => dest.Plano, opt => opt.Ignore())
                .ForMember(dest => dest.FaixaEtaria, opt => opt.Ignore());

            // Reajuste mappings
            CreateMap<Reajuste, ReajusteResponseDTO>()
                .ForMember(dest => dest.PlanoNome, opt => opt.MapFrom(src => src.Plano.Nome));
            CreateMap<ReajustePayLoadDTO, Reajuste>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.DataCriacao, opt => opt.Ignore())
                .ForMember(dest => dest.DataAtualizacao, opt => opt.Ignore())
                .ForMember(dest => dest.Plano, opt => opt.Ignore());

            // EstabelecimentoSaude mappings
            CreateMap<EstabelecimentoSaude, EstabelecimentoSaudeResponseDTO>();
            CreateMap<EstabelecimentoSaudePayLoadDTO, EstabelecimentoSaude>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.DataCriacao, opt => opt.Ignore())
                .ForMember(dest => dest.DataAtualizacao, opt => opt.Ignore())
                .ForMember(dest => dest.PlanoAbrangencias, opt => opt.Ignore());

            // PlanoAbrangencia mappings
            CreateMap<PlanoAbrangencia, PlanoAbrangenciaResponseDTO>()
                .ForMember(dest => dest.PlanoNome, opt => opt.MapFrom(src => src.Plano.Nome))
                .ForMember(dest => dest.EstabelecimentoNome, opt => opt.MapFrom(src => src.EstabelecimentoSaude.Nome))
                .ForMember(dest => dest.EstabelecimentoTipo, opt => opt.MapFrom(src => src.EstabelecimentoSaude.Tipo));
            CreateMap<PlanoAbrangenciaPayLoadDTO, PlanoAbrangencia>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.DataCriacao, opt => opt.Ignore())
                .ForMember(dest => dest.DataAtualizacao, opt => opt.Ignore())
                .ForMember(dest => dest.Plano, opt => opt.Ignore())
                .ForMember(dest => dest.EstabelecimentoSaude, opt => opt.Ignore());
        }
    }
}