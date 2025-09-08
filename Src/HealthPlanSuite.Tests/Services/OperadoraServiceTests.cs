using AutoMapper;
using FluentAssertions;
using HealthPlanSuite.Quote.Domain.Implementation;
using HealthPlanSuite.Quote.DTO;
using HealthPlanSuite.Quote.Mapping;
using HealthPlanSuite.Quote.Repository.Interface;
using HealthPlanSuite.Quote.Services.Implementation;
using Moq;
using Xunit;

namespace HealthPlanSuite.Tests.Services
{
    /// <summary>
    /// Testes unitários para o serviço de Operadoras
    /// </summary>
    public class OperadoraServiceTests
    {
        private readonly Mock<IOperadoraRepository> _mockRepository;
        private readonly IMapper _mapper;
        private readonly OperadoraService _service;

        public OperadoraServiceTests()
        {
            _mockRepository = new Mock<IOperadoraRepository>();
            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<OperadoraProfile>();
            });
            _mapper = config.CreateMapper();
            
            _service = new OperadoraService(_mockRepository.Object, _mapper);
        }

        [Fact]
        public async Task GetAllAsync_WhenOperadorasExist_ReturnsOperadoraDtos()
        {
            // Arrange
            var operadoras = new List<Operadora>
            {
                new Operadora { Id = 1, Nome = "Unimed", RegistroANS = "123456", CNPJ = "12.345.678/0001-90", Ativa = true },
                new Operadora { Id = 2, Nome = "Bradesco Saúde", RegistroANS = "789012", CNPJ = "98.765.432/0001-10", Ativa = true }
            };

            _mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(operadoras);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.First().Nome.Should().Be("Unimed");
            result.Last().Nome.Should().Be("Bradesco Saúde");
        }

        [Fact]
        public async Task GetByIdAsync_WhenOperadoraExists_ReturnsOperadoraDto()
        {
            // Arrange
            var operadora = new Operadora 
            { 
                Id = 1, 
                Nome = "Unimed", 
                RegistroANS = "123456", 
                CNPJ = "12.345.678/0001-90", 
                Ativa = true 
            };

            _mockRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(operadora);

            // Act
            var result = await _service.GetByIdAsync(1);

            // Assert
            result.Should().NotBeNull();
            result!.Id.Should().Be(1);
            result.Nome.Should().Be("Unimed");
            result.RegistroANS.Should().Be("123456");
        }

        [Fact]
        public async Task GetByIdAsync_WhenOperadoraDoesNotExist_ReturnsNull()
        {
            // Arrange
            _mockRepository.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Operadora?)null);

            // Act
            var result = await _service.GetByIdAsync(999);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task CreateAsync_WhenValidOperadoraCreateDto_ReturnsOperadoraDto()
        {
            // Arrange
            var createDto = new OperadoraCreateDto
            {
                Nome = "SulAmérica",
                RegistroANS = "345678",
                CNPJ = "11.222.333/0001-44",
                Telefone = "(11) 3000-3000"
            };

            var createdOperadora = new Operadora
            {
                Id = 3,
                Nome = createDto.Nome,
                RegistroANS = createDto.RegistroANS,
                CNPJ = createDto.CNPJ,
                Telefone = createDto.Telefone,
                Ativa = true,
                DataCriacao = DateTime.UtcNow,
                DataAtualizacao = DateTime.UtcNow
            };

            _mockRepository.Setup(r => r.CreateAsync(It.IsAny<Operadora>())).ReturnsAsync(createdOperadora);

            // Act
            var result = await _service.CreateAsync(createDto);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(3);
            result.Nome.Should().Be("SulAmérica");
            result.RegistroANS.Should().Be("345678");
            result.Ativa.Should().BeTrue();

            _mockRepository.Verify(r => r.CreateAsync(It.Is<Operadora>(o => 
                o.Nome == createDto.Nome && 
                o.RegistroANS == createDto.RegistroANS &&
                o.CNPJ == createDto.CNPJ)), Times.Once);
        }

        [Fact]
        public async Task ExistsAsync_WhenOperadoraExists_ReturnsTrue()
        {
            // Arrange
            _mockRepository.Setup(r => r.ExistsAsync(1)).ReturnsAsync(true);

            // Act
            var result = await _service.ExistsAsync(1);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task ExistsAsync_WhenOperadoraDoesNotExist_ReturnsFalse()
        {
            // Arrange
            _mockRepository.Setup(r => r.ExistsAsync(999)).ReturnsAsync(false);

            // Act
            var result = await _service.ExistsAsync(999);

            // Assert
            result.Should().BeFalse();
        }
    }
}