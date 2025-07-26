using HealthPlanSuite.Domain.Implementation;
using HealthPlanSuite.DTO;

namespace HealthPlanSuite.Mapping
{
    public static class OperadoraMapping
    {
        public static OperadoraResponseDTO MapToResponseDTO(this Operadora entity)
        {
            return new OperadoraResponseDTO
            {
                Id = entity.Id,
                Nome = entity.Nome,
                CNPJ = entity.CNPJ,
                Site = entity.Site,
                Telefone = entity.Telefone,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }

        public static Operadora MapToEntity(this OperadoraRequestDTO dto)
        {
            return new Operadora(dto.Nome, dto.CNPJ, dto.Site, dto.Telefone);
        }
    }

    public static class TipoPlanoMapping
    {
        public static TipoPlanoResponseDTO MapToResponseDTO(this TipoPlano entity)
        {
            return new TipoPlanoResponseDTO
            {
                Id = entity.Id,
                Descricao = entity.Descricao,
                RegulacaoANS = entity.RegulacaoANS,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }

        public static TipoPlano MapToEntity(this TipoPlanoRequestDTO dto)
        {
            return new TipoPlano(dto.Descricao, dto.RegulacaoANS);
        }
    }

    public static class FaixaEtariaMapping
    {
        public static FaixaEtariaResponseDTO MapToResponseDTO(this FaixaEtaria entity)
        {
            return new FaixaEtariaResponseDTO
            {
                Id = entity.Id,
                Descricao = entity.Descricao,
                IdadeMin = entity.IdadeMin,
                IdadeMax = entity.IdadeMax,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }

        public static FaixaEtaria MapToEntity(this FaixaEtariaRequestDTO dto)
        {
            return new FaixaEtaria(dto.Descricao, dto.IdadeMin, dto.IdadeMax);
        }
    }

    public static class EstabelecimentoSaudeMapping
    {
        public static EstabelecimentoSaudeResponseDTO MapToResponseDTO(this EstabelecimentoSaude entity)
        {
            return new EstabelecimentoSaudeResponseDTO
            {
                Id = entity.Id,
                Nome = entity.Nome,
                Tipo = entity.Tipo,
                Endereco = entity.Endereco,
                Cidade = entity.Cidade,
                Estado = entity.Estado,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }

        public static EstabelecimentoSaude MapToEntity(this EstabelecimentoSaudeRequestDTO dto)
        {
            return new EstabelecimentoSaude(dto.Nome, dto.Tipo, dto.Endereco, dto.Cidade, dto.Estado);
        }
    }

    public static class PlanoMapping
    {
        public static PlanoResponseDTO MapToResponseDTO(this Plano entity, bool includeRelations = false)
        {
            var dto = new PlanoResponseDTO
            {
                Id = entity.Id,
                OperadoraId = entity.OperadoraId,
                TipoPlanoId = entity.TipoPlanoId,
                Nome = entity.Nome,
                Cobertura = entity.Cobertura,
                PossuiCoparticipacao = entity.PossuiCoparticipacao,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };

            if (includeRelations)
            {
                dto.Operadora = entity.Operadora?.MapToResponseDTO();
                dto.TipoPlano = entity.TipoPlano?.MapToResponseDTO();
            }

            return dto;
        }

        public static Plano MapToEntity(this PlanoRequestDTO dto)
        {
            return new Plano(dto.OperadoraId, dto.TipoPlanoId, dto.Nome, dto.Cobertura, dto.PossuiCoparticipacao);
        }
    }

    public static class TabelaPrecoMapping
    {
        public static TabelaPrecoResponseDTO MapToResponseDTO(this TabelaPreco entity, bool includeRelations = false)
        {
            var dto = new TabelaPrecoResponseDTO
            {
                Id = entity.Id,
                PlanoId = entity.PlanoId,
                FaixaEtariaId = entity.FaixaEtariaId,
                Mensalidade = entity.Mensalidade,
                ValorCoparticipacao = entity.ValorCoparticipacao,
                DataInicioVigencia = entity.DataInicioVigencia,
                DataFimVigencia = entity.DataFimVigencia,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };

            if (includeRelations)
            {
                dto.Plano = entity.Plano?.MapToResponseDTO();
                dto.FaixaEtaria = entity.FaixaEtaria?.MapToResponseDTO();
            }

            return dto;
        }

        public static TabelaPreco MapToEntity(this TabelaPrecoRequestDTO dto)
        {
            return new TabelaPreco(dto.PlanoId, dto.FaixaEtariaId, dto.Mensalidade, 
                                  dto.DataInicioVigencia, dto.ValorCoparticipacao, dto.DataFimVigencia);
        }
    }

    public static class ReajusteMapping
    {
        public static ReajusteResponseDTO MapToResponseDTO(this Reajuste entity, bool includeRelations = false)
        {
            var dto = new ReajusteResponseDTO
            {
                Id = entity.Id,
                PlanoId = entity.PlanoId,
                Percentual = entity.Percentual,
                DataReajuste = entity.DataReajuste,
                TipoReajuste = entity.TipoReajuste,
                Observacao = entity.Observacao,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };

            if (includeRelations)
            {
                dto.Plano = entity.Plano?.MapToResponseDTO();
            }

            return dto;
        }

        public static Reajuste MapToEntity(this ReajusteRequestDTO dto)
        {
            return new Reajuste(dto.PlanoId, dto.Percentual, dto.DataReajuste, dto.TipoReajuste, dto.Observacao);
        }
    }

    public static class PlanoAbrangenciaMapping
    {
        public static PlanoAbrangenciaResponseDTO MapToResponseDTO(this PlanoAbrangencia entity, bool includeRelations = false)
        {
            var dto = new PlanoAbrangenciaResponseDTO
            {
                Id = entity.Id,
                PlanoId = entity.PlanoId,
                EstabelecimentoId = entity.EstabelecimentoId,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };

            if (includeRelations)
            {
                dto.Plano = entity.Plano?.MapToResponseDTO();
                dto.EstabelecimentoSaude = entity.EstabelecimentoSaude?.MapToResponseDTO();
            }

            return dto;
        }

        public static PlanoAbrangencia MapToEntity(this PlanoAbrangenciaRequestDTO dto)
        {
            return new PlanoAbrangencia(dto.PlanoId, dto.EstabelecimentoId);
        }
    }
}