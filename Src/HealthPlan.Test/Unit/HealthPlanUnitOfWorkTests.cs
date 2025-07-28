using FluentAssertions;
using HealthPlan.Quote.Domain.HealthPlan.Implementation;
using HealthPlan.Quote.Services.HealthPlan.Implementation;
using HealthPlan.Quote.UnitOfWork.Interface;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace HealthPlan.Test.Unit
{
    public class HealthPlanUnitOfWorkTests
    {
        [Fact]
        public void HealthInsuranceOperatorService_ShouldUseUnitOfWork_ForTransactionalOperations()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IHealthPlanUnitOfWork>();
            var mockRepository = new Mock<HealthPlan.Quote.Repository.HealthPlan.Interface.IHealthInsuranceOperatorRepository>();
            
            var testOperator = new HealthInsuranceOperator 
            { 
                Id = 1, 
                Name = "Test Operator", 
                CNPJ = "12.345.678/0001-90" 
            };

            // Setup mocks
            mockUnitOfWork.Setup(x => x.HealthInsuranceOperatorRepository).Returns(mockRepository.Object);
            mockRepository.Setup(x => x.Add(It.IsAny<HealthInsuranceOperator>())).Returns(testOperator);
            
            var service = new HealthInsuranceOperatorService(mockUnitOfWork.Object);

            // Act
            var result = service.Add(testOperator);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(1);
            result.Name.Should().Be("Test Operator");
            
            // Verify that ExecuteInTransaction was called
            mockUnitOfWork.Verify(x => x.ExecuteInTransaction(It.IsAny<Action>()), Times.Once);
            mockRepository.Verify(x => x.Add(testOperator), Times.Once);
        }

        [Fact]
        public void HealthInsuranceOperatorService_ShouldUseUnitOfWork_ForReadOperations()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IHealthPlanUnitOfWork>();
            var mockRepository = new Mock<HealthPlan.Quote.Repository.HealthPlan.Interface.IHealthInsuranceOperatorRepository>();
            
            var operators = new List<HealthInsuranceOperator>
            {
                new HealthInsuranceOperator { Id = 1, Name = "Operator 1", CNPJ = "12.345.678/0001-90" },
                new HealthInsuranceOperator { Id = 2, Name = "Operator 2", CNPJ = "98.765.432/0001-10" }
            };

            // Setup mocks
            mockUnitOfWork.Setup(x => x.HealthInsuranceOperatorRepository).Returns(mockRepository.Object);
            mockRepository.Setup(x => x.GetAll()).Returns(operators);
            
            var service = new HealthInsuranceOperatorService(mockUnitOfWork.Object);

            // Act
            var result = service.GetAll();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.First().Name.Should().Be("Operator 1");
            
            // Verify that the repository was accessed through UnitOfWork
            mockUnitOfWork.Verify(x => x.HealthInsuranceOperatorRepository, Times.Once);
            mockRepository.Verify(x => x.GetAll(), Times.Once);
        }

        [Fact]
        public void HealthPlanService_ShouldUseUnitOfWork_ForTransactionalOperations()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IHealthPlanUnitOfWork>();
            var mockRepository = new Mock<HealthPlan.Quote.Repository.HealthPlan.Interface.IHealthPlanRepository>();
            
            var testPlan = new HealthPlan.Quote.Domain.HealthPlan.Implementation.HealthPlan
            { 
                Id = 1, 
                Name = "Test Plan",
                HealthInsuranceOperatorId = 1
            };

            // Setup mocks
            mockUnitOfWork.Setup(x => x.HealthPlanRepository).Returns(mockRepository.Object);
            mockRepository.Setup(x => x.Add(It.IsAny<HealthPlan.Quote.Domain.HealthPlan.Implementation.HealthPlan>())).Returns(testPlan);
            
            var service = new HealthPlanService(mockUnitOfWork.Object);

            // Act
            var result = service.Add(testPlan);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(1);
            result.Name.Should().Be("Test Plan");
            
            // Verify that ExecuteInTransaction was called
            mockUnitOfWork.Verify(x => x.ExecuteInTransaction(It.IsAny<Action>()), Times.Once);
            mockRepository.Verify(x => x.Add(testPlan), Times.Once);
        }

        [Fact]
        public void PriceTableService_ShouldUseUnitOfWork_ForTransactionalOperations()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IHealthPlanUnitOfWork>();
            var mockRepository = new Mock<HealthPlan.Quote.Repository.HealthPlan.Interface.IPriceTableRepository>();
            
            var testPriceTable = new PriceTable
            { 
                Id = 1, 
                HealthPlanId = 1,
                AgeRangeId = 1,
                MonthlyFee = 100.50m
            };

            // Setup mocks
            mockUnitOfWork.Setup(x => x.PriceTableRepository).Returns(mockRepository.Object);
            mockRepository.Setup(x => x.Add(It.IsAny<PriceTable>())).Returns(testPriceTable);
            
            var service = new PriceTableService(mockUnitOfWork.Object);

            // Act
            var result = service.Add(testPriceTable);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(1);
            result.MonthlyFee.Should().Be(100.50m);
            
            // Verify that ExecuteInTransaction was called
            mockUnitOfWork.Verify(x => x.ExecuteInTransaction(It.IsAny<Action>()), Times.Once);
            mockRepository.Verify(x => x.Add(testPriceTable), Times.Once);
        }

        [Fact]
        public void AllHealthPlanServices_ShouldFollowSamePatternAsAuthentication()
        {
            // This test verifies that all HealthPlan services follow the same UnitOfWork pattern as Authentication
            var mockUnitOfWork = new Mock<IHealthPlanUnitOfWork>();

            // Test that all service constructors accept IHealthPlanUnitOfWork
            var healthInsuranceOperatorService = new HealthInsuranceOperatorService(mockUnitOfWork.Object);
            var healthPlanService = new HealthPlanService(mockUnitOfWork.Object);
            var priceTableService = new PriceTableService(mockUnitOfWork.Object);
            var ageRangeService = new AgeRangeService(mockUnitOfWork.Object);
            var planTypeService = new PlanTypeService(mockUnitOfWork.Object);
            var planAdjustmentService = new PlanAdjustmentService(mockUnitOfWork.Object);
            var healthEstablishmentService = new HealthEstablishmentService(mockUnitOfWork.Object);
            var planCoverageService = new PlanCoverageService(mockUnitOfWork.Object);

            // Assert all services are properly instantiated with UnitOfWork
            healthInsuranceOperatorService.Should().NotBeNull();
            healthPlanService.Should().NotBeNull();
            priceTableService.Should().NotBeNull();
            ageRangeService.Should().NotBeNull();
            planTypeService.Should().NotBeNull();
            planAdjustmentService.Should().NotBeNull();
            healthEstablishmentService.Should().NotBeNull();
            planCoverageService.Should().NotBeNull();
        }
    }
}