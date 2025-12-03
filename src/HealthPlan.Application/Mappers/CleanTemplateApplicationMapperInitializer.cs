using AutoMapper;
using Microsoft.Extensions.Logging.Abstractions;

namespace HealthPlan.Application.Mappers
{
    public static class CleanTemplateApplicationMapperInitializer
    {
        private static IMapper? _mapper;
        private static MapperConfiguration? _config;

        public static void Initialize()
        {
            var configExpr = new MapperConfigurationExpression();
            configExpr.AddProfile<CleanTemplateApplicationMapping>();

            _config = new MapperConfiguration(configExpr);
            _mapper = _config.CreateMapper();
        }

        public static IMapper Mapper
        {
            get
            {
                if (_mapper == null)
                {
                    Initialize();
                }
                return _mapper!;
            }
        }
    }
}
