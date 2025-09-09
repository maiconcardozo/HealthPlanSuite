using HealthPlan.Quote.Domain.Implementation;
using HealthPlan.Quote.DTO;
using AutoMapper;

namespace HealthPlan.Quote.Mapping
{
    public class CleanTemplateApplicationMapping : Profile
    {
        public CleanTemplateApplicationMapping()
        {
            // CleanEntity mappings
            CreateMap<CleanEntityPayLoadDTO, CleanEntity>();
            CreateMap<CleanEntity, CleanEntityPayLoadDTO>();
            CreateMap<CleanEntity, CleanEntityResponseDTO>();
            CreateMap<CleanEntityResponseDTO, CleanEntity>();
        }
    }
}