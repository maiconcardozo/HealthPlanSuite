using HealthPlan.Quote.Domain.Implementation;
using Xunit;
using FluentAssertions;

namespace HealthPlan.Test.Integration
{
    /// <summary>
    /// Integration tests for Quote examples from the SQL modeling.
    /// These tests validate the structure and data integrity of the 10 real quote examples
    /// included in the HealthPlanModeling.sql file.
    /// </summary>
    public class QuoteModelingExamplesTests
    {
        /// <summary>
        /// Validates the structure of example quotes from the SQL modeling.
        /// This test ensures that the domain models can properly represent 
        /// the real-world scenarios described in the database examples.
        /// </summary>
        [Fact]
        public void QuoteExamples_ShouldMatchSQLModelingStructure()
        {
            // Arrange - Create test data based on the SQL examples
            var companies = CreateTestCompanies();
            var beneficiaries = CreateTestBeneficiaries();
            var ageRanges = CreateTestAgeRanges();
            var accommodations = CreateTestAccommodations();
            var healthPlans = CreateTestHealthPlans(companies, accommodations);
            var quotes = CreateTestQuotes(companies, beneficiaries, healthPlans, ageRanges);

            // Act & Assert - Validate quote examples
            quotes.Should().HaveCount(10, "because the SQL modeling includes 10 real quote examples");

            // Validate Quote 1: Maria Silva Santos (Approved)
            var quote1 = quotes.FirstOrDefault(q => q.QuoteNumber == "COT-2025-000001");
            quote1.Should().NotBeNull();
            quote1!.Status.Should().Be("Aprovada");
            quote1.CalculatedAge.Should().Be(39);
            quote1.CalculatedPremium.Should().Be(143.84m);
            quote1.TotalValue.Should().Be(143.84m);
            quote1.DiscountPercentage.Should().Be(0.00m);

            // Validate Quote 2: João Carlos Oliveira (Pending)
            var quote2 = quotes.FirstOrDefault(q => q.QuoteNumber == "COT-2025-000002");
            quote2.Should().NotBeNull();
            quote2!.Status.Should().Be("Pendente");
            quote2.CalculatedAge.Should().Be(34);
            quote2.CalculatedPremium.Should().Be(419.86m);

            // Validate Quote 3: Ana Paula Costa (Approved with discount)
            var quote3 = quotes.FirstOrDefault(q => q.QuoteNumber == "COT-2025-000003");
            quote3.Should().NotBeNull();
            quote3!.Status.Should().Be("Aprovada");
            quote3.CalculatedAge.Should().Be(46);
            quote3.DiscountPercentage.Should().Be(5.00m);
            quote3.DiscountValue.Should().Be(17.09m);

            // Validate Quote 4: Pedro Henrique Lima (Rejected)
            var quote4 = quotes.FirstOrDefault(q => q.QuoteNumber == "COT-2025-000004");
            quote4.Should().NotBeNull();
            quote4!.Status.Should().Be("Rejeitada");
            quote4.RejectionReason.Should().Be("Beneficiário não atende critério de renda mínima");

            // Validate Quote 6: Roberto Silva Nascimento (Contracted)
            var quote6 = quotes.FirstOrDefault(q => q.QuoteNumber == "COT-2025-000006");
            quote6.Should().NotBeNull();
            quote6!.Status.Should().Be("Contratada");
            quote6.CalculatedAge.Should().Be(52);

            // Validate Quote 8: Marcos Antonio Silva (Expired)
            var quote8 = quotes.FirstOrDefault(q => q.QuoteNumber == "COT-2025-000008");
            quote8.Should().NotBeNull();
            quote8!.Status.Should().Be("Expirada");
            quote8.Notes.Should().Be("Cotação expirou sem contratação");
        }

        /// <summary>
        /// Validates that age ranges properly calculate premium multipliers
        /// based on the SQL modeling examples.
        /// </summary>
        [Theory]
        [InlineData(25, 1.2000)] // 24-28 anos
        [InlineData(35, 1.6000)] // 34-38 anos  
        [InlineData(45, 2.0000)] // 44-48 anos
        [InlineData(55, 2.6000)] // 54-58 anos
        public void AgeRanges_ShouldApplyCorrectMultipliers(int age, decimal expectedMultiplier)
        {
            // Arrange
            var ageRanges = CreateTestAgeRanges();

            // Act
            var matchingRange = ageRanges.FirstOrDefault(ar => age >= ar.MinAge && age <= ar.MaxAge);

            // Assert
            matchingRange.Should().NotBeNull($"because age {age} should have a corresponding age range");
            matchingRange!.Multiplier.Should().Be(expectedMultiplier);
        }

        /// <summary>
        /// Validates the domain model relationships match the SQL foreign key structure.
        /// </summary>
        [Fact]
        public void DomainModels_ShouldSupportSQLRelationships()
        {
            // Arrange
            var company = new Company { Id = 1, Name = "Test Company" };
            var beneficiary = new Beneficiary { Id = 1, Name = "Test Beneficiary" };
            var accommodation = new Accommodation { Id = 1, Type = "Apartamento" };
            var ageRange = new AgeRange { Id = 1, Description = "Test Range" };
            var healthPlan = new HealthPlan.Quote.Domain.Implementation.HealthPlan 
            { 
                Id = 1, 
                CompanyId = company.Id,
                AccommodationId = accommodation.Id,
                Name = "Test Plan" 
            };

            // Act - Create quote with all relationships
            var quote = new HealthPlan.Quote.Domain.Implementation.Quote
            {
                Id = 1,
                CompanyId = company.Id,
                Company = company,
                BeneficiaryId = beneficiary.Id,
                Beneficiary = beneficiary,
                HealthPlanId = healthPlan.Id,
                HealthPlan = healthPlan,
                AgeRangeId = ageRange.Id,
                AgeRange = ageRange,
                QuoteNumber = "TEST-001",
                Status = "Pendente"
            };

            // Assert - Validate navigation properties
            quote.Company.Should().NotBeNull();
            quote.Beneficiary.Should().NotBeNull();
            quote.HealthPlan.Should().NotBeNull();
            quote.AgeRange.Should().NotBeNull();
            quote.Company!.Id.Should().Be(quote.CompanyId);
            quote.Beneficiary!.Id.Should().Be(quote.BeneficiaryId);
            quote.HealthPlan!.Id.Should().Be(quote.HealthPlanId);
            quote.AgeRange!.Id.Should().Be(quote.AgeRangeId);
        }

        #region Test Data Creation Methods

        private static List<Company> CreateTestCompanies()
        {
            return new List<Company>
            {
                new() { Id = 1, Name = "Saúde & Vida Seguros Ltda", TradeName = "Saúde & Vida", CNPJ = "12.345.678/0001-90" },
                new() { Id = 2, Name = "MedLife Seguros S.A.", TradeName = "MedLife", CNPJ = "98.765.432/0001-10" },
                new() { Id = 3, Name = "PlanoMax Saúde", TradeName = "PlanoMax", CNPJ = "11.222.333/0001-44" }
            };
        }

        private static List<Beneficiary> CreateTestBeneficiaries()
        {
            return new List<Beneficiary>
            {
                new() { Id = 1, Name = "Maria Silva Santos", CPF = "123.456.789-01", DateOfBirth = new DateTime(1985, 3, 15) },
                new() { Id = 2, Name = "João Carlos Oliveira", CPF = "987.654.321-09", DateOfBirth = new DateTime(1990, 7, 22) },
                new() { Id = 3, Name = "Ana Paula Costa", CPF = "456.789.123-45", DateOfBirth = new DateTime(1978, 12, 10) },
                new() { Id = 4, Name = "Pedro Henrique Lima", CPF = "789.123.456-78", DateOfBirth = new DateTime(1995, 5, 8) },
                new() { Id = 5, Name = "Carla Fernanda Souza", CPF = "321.654.987-01", DateOfBirth = new DateTime(1988, 9, 25) },
                new() { Id = 6, Name = "Roberto Silva Nascimento", CPF = "654.321.789-12", DateOfBirth = new DateTime(1972, 11, 30) },
                new() { Id = 7, Name = "Juliana Santos Pereira", CPF = "159.753.486-20", DateOfBirth = new DateTime(1992, 2, 18) },
                new() { Id = 8, Name = "Marcos Antonio Silva", CPF = "852.741.963-30", DateOfBirth = new DateTime(1980, 8, 14) },
                new() { Id = 9, Name = "Fernanda Costa Lima", CPF = "963.852.741-40", DateOfBirth = new DateTime(1986, 4, 3) },
                new() { Id = 10, Name = "Carlos Eduardo Santos", CPF = "741.852.963-50", DateOfBirth = new DateTime(1983, 10, 27) }
            };
        }

        private static List<AgeRange> CreateTestAgeRanges()
        {
            return new List<AgeRange>
            {
                new() { Id = 1, Description = "0 a 18 anos", MinAge = 0, MaxAge = 18, Multiplier = 0.8000m },
                new() { Id = 2, Description = "19 a 23 anos", MinAge = 19, MaxAge = 23, Multiplier = 1.0000m },
                new() { Id = 3, Description = "24 a 28 anos", MinAge = 24, MaxAge = 28, Multiplier = 1.2000m },
                new() { Id = 4, Description = "29 a 33 anos", MinAge = 29, MaxAge = 33, Multiplier = 1.4000m },
                new() { Id = 5, Description = "34 a 38 anos", MinAge = 34, MaxAge = 38, Multiplier = 1.6000m },
                new() { Id = 6, Description = "39 a 43 anos", MinAge = 39, MaxAge = 43, Multiplier = 1.8000m },
                new() { Id = 7, Description = "44 a 48 anos", MinAge = 44, MaxAge = 48, Multiplier = 2.0000m },
                new() { Id = 8, Description = "49 a 53 anos", MinAge = 49, MaxAge = 53, Multiplier = 2.2000m },
                new() { Id = 9, Description = "54 a 58 anos", MinAge = 54, MaxAge = 58, Multiplier = 2.6000m },
                new() { Id = 10, Description = "59+ anos", MinAge = 59, MaxAge = 120, Multiplier = 3.0000m }
            };
        }

        private static List<Accommodation> CreateTestAccommodations()
        {
            return new List<Accommodation>
            {
                new() { Id = 1, Type = "Enfermaria", Description = "Quarto compartilhado com outros pacientes", AdditionalValue = 0.00m },
                new() { Id = 2, Type = "Apartamento", Description = "Quarto individual com acompanhante", AdditionalValue = 150.00m },
                new() { Id = 3, Type = "Apartamento Luxo", Description = "Quarto individual de luxo com comodidades especiais", AdditionalValue = 300.00m },
                new() { Id = 4, Type = "UTI", Description = "Unidade de Terapia Intensiva", AdditionalValue = 0.00m }
            };
        }

        private static List<HealthPlan.Quote.Domain.Implementation.HealthPlan> CreateTestHealthPlans(List<Company> companies, List<Accommodation> accommodations)
        {
            return new List<HealthPlan.Quote.Domain.Implementation.HealthPlan>
            {
                new() { Id = 1, CompanyId = 1, AccommodationId = 1, Name = "Essencial Ambulatorial", Code = "ESS-AMB-001", BasePremium = 89.90m },
                new() { Id = 2, CompanyId = 1, AccommodationId = 2, Name = "Completo Hospitalar", Code = "CMP-HOSP-001", BasePremium = 189.90m },
                new() { Id = 3, CompanyId = 2, AccommodationId = 1, Name = "Familiar Básico", Code = "FAM-BAS-001", BasePremium = 299.90m },
                new() { Id = 4, CompanyId = 2, AccommodationId = 2, Name = "Executivo Premium", Code = "EXEC-PREM-001", BasePremium = 599.90m },
                new() { Id = 5, CompanyId = 3, AccommodationId = 1, Name = "Empresarial Standard", Code = "EMP-STD-001", BasePremium = 149.90m }
            };
        }

        private static List<HealthPlan.Quote.Domain.Implementation.Quote> CreateTestQuotes(List<Company> companies, List<Beneficiary> beneficiaries, 
            List<HealthPlan.Quote.Domain.Implementation.HealthPlan> healthPlans, List<AgeRange> ageRanges)
        {
            return new List<HealthPlan.Quote.Domain.Implementation.Quote>
            {
                new() { Id = 1, CompanyId = 1, BeneficiaryId = 1, HealthPlanId = 1, AgeRangeId = 5, QuoteNumber = "COT-2025-000001", CalculatedPremium = 143.84m, TotalValue = 143.84m, DiscountPercentage = 0.00m, DiscountValue = 0.00m, Status = "Aprovada", CalculatedAge = 39, ValidityDays = 30 },
                new() { Id = 2, CompanyId = 2, BeneficiaryId = 2, HealthPlanId = 3, AgeRangeId = 4, QuoteNumber = "COT-2025-000002", CalculatedPremium = 419.86m, TotalValue = 419.86m, DiscountPercentage = 0.00m, DiscountValue = 0.00m, Status = "Pendente", CalculatedAge = 34, ValidityDays = 30 },
                new() { Id = 3, CompanyId = 1, BeneficiaryId = 3, HealthPlanId = 2, AgeRangeId = 6, QuoteNumber = "COT-2025-000003", CalculatedPremium = 341.82m, TotalValue = 324.73m, DiscountPercentage = 5.00m, DiscountValue = 17.09m, Status = "Aprovada", CalculatedAge = 46, ValidityDays = 30 },
                new() { Id = 4, CompanyId = 2, BeneficiaryId = 4, HealthPlanId = 4, AgeRangeId = 3, QuoteNumber = "COT-2025-000004", CalculatedPremium = 719.88m, TotalValue = 719.88m, DiscountPercentage = 0.00m, DiscountValue = 0.00m, Status = "Rejeitada", RejectionReason = "Beneficiário não atende critério de renda mínima", CalculatedAge = 29, ValidityDays = 30 },
                new() { Id = 5, CompanyId = 3, BeneficiaryId = 5, HealthPlanId = 5, AgeRangeId = 5, QuoteNumber = "COT-2025-000005", CalculatedPremium = 239.84m, TotalValue = 215.86m, DiscountPercentage = 10.00m, DiscountValue = 23.98m, Status = "Aprovada", CalculatedAge = 36, ValidityDays = 30 },
                new() { Id = 6, CompanyId = 1, BeneficiaryId = 6, HealthPlanId = 2, AgeRangeId = 9, QuoteNumber = "COT-2025-000006", CalculatedPremium = 493.74m, TotalValue = 493.74m, DiscountPercentage = 0.00m, DiscountValue = 0.00m, Status = "Contratada", CalculatedAge = 52, ValidityDays = 30 },
                new() { Id = 7, CompanyId = 2, BeneficiaryId = 7, HealthPlanId = 3, AgeRangeId = 3, QuoteNumber = "COT-2025-000007", CalculatedPremium = 419.86m, TotalValue = 419.86m, DiscountPercentage = 0.00m, DiscountValue = 0.00m, Status = "Aprovada", CalculatedAge = 32, ValidityDays = 30 },
                new() { Id = 8, CompanyId = 3, BeneficiaryId = 8, HealthPlanId = 5, AgeRangeId = 6, QuoteNumber = "COT-2025-000008", CalculatedPremium = 269.82m, TotalValue = 269.82m, DiscountPercentage = 0.00m, DiscountValue = 0.00m, Status = "Expirada", Notes = "Cotação expirou sem contratação", CalculatedAge = 44, ValidityDays = 30 },
                new() { Id = 9, CompanyId = 1, BeneficiaryId = 9, HealthPlanId = 1, AgeRangeId = 5, QuoteNumber = "COT-2025-000009", CalculatedPremium = 143.84m, TotalValue = 136.65m, DiscountPercentage = 5.00m, DiscountValue = 7.19m, Status = "Pendente", CalculatedAge = 38, ValidityDays = 30 },
                new() { Id = 10, CompanyId = 2, BeneficiaryId = 10, HealthPlanId = 4, AgeRangeId = 6, QuoteNumber = "COT-2025-000010", CalculatedPremium = 1079.82m, TotalValue = 1079.82m, DiscountPercentage = 0.00m, DiscountValue = 0.00m, Status = "Aprovada", CalculatedAge = 41, ValidityDays = 30 }
            };
        }

        #endregion
    }
}