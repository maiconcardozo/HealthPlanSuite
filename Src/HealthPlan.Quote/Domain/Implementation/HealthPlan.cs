using Foundation.Base.Domain.Implementation;
using HealthPlan.Quote.Domain.Interface;

namespace HealthPlan.Quote.Domain.Implementation
{
    /// <summary>
    /// Represents a health insurance plan implementation offered by a company.
    /// Health plans define the coverage and terms for health insurance.
    /// Inherits from Entity base class providing audit fields and implements IHealthPlan interface.
    /// </summary>
    public class HealthPlan : Entity, IHealthPlan
    {
        /// <summary>
        /// Gets or sets the health plan ID.
        /// Maps to SQL column: IdPlanoSaude
        /// </summary>
        public int IdPlanoSaude 
        { 
            get => Id; 
            set => Id = value; 
        }

        /// <summary>
        /// Gets or sets the company ID that offers this health plan.
        /// References the Company entity.
        /// Maps to SQL column: IdEmpresa
        /// </summary>
        public int IdEmpresa { get; set; }
        
        /// <summary>
        /// Gets or sets the company that offers this health plan.
        /// Navigation property for IdEmpresa foreign key.
        /// </summary>
        public Company? Company { get; set; }

        /// <summary>
        /// Gets or sets the accommodation ID for this health plan.
        /// References the Accommodation entity.
        /// Maps to SQL column: IdAcomodacao
        /// </summary>
        public int IdAcomodacao { get; set; }

        /// <summary>
        /// Gets or sets the accommodation for this health plan.
        /// Navigation property for IdAcomodacao foreign key.
        /// </summary>
        public Accommodation? Accommodation { get; set; }
        
        // DEPRECATED properties for backward compatibility
        /// <summary>
        /// Gets or sets the company ID that offers this health plan.
        /// DEPRECATED: Use IdEmpresa instead.
        /// </summary>
        [Obsolete("Use IdEmpresa instead")]
        public int CompanyId 
        { 
            get => IdEmpresa; 
            set => IdEmpresa = value; 
        }

        /// <summary>
        /// Gets or sets the accommodation ID for this health plan.
        /// DEPRECATED: Use IdAcomodacao instead.
        /// </summary>
        [Obsolete("Use IdAcomodacao instead")]
        public int AccommodationId 
        { 
            get => IdAcomodacao; 
            set => IdAcomodacao = value; 
        }
        
        /// <summary>
        /// Gets or sets the name of the health plan.
        /// For example: "Plano Básico", "Plano Executivo", "Plano Premium".
        /// Maps to SQL column: Nome
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the description of the health plan.
        /// Detailed explanation of the plan benefits and coverage.
        /// Maps to SQL column: Descricao
        /// </summary>
        public string? Description { get; set; }
        
        /// <summary>
        /// Gets or sets the unique code of the health plan.
        /// Must be unique across the system.
        /// Maps to SQL column: Codigo
        /// </summary>
        public string Code { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the category of the health plan.
        /// Possible values: Individual, Familiar, Empresarial.
        /// Maps to SQL column: Categoria
        /// </summary>
        public string Category { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the type of the health plan.
        /// DEPRECATED: Use Category instead for new code.
        /// Maintained for backward compatibility with IHealthPlan interface.
        /// </summary>
        [Obsolete("Use Category instead")]
        public string PlanType 
        { 
            get => Category; 
            set => Category = value; 
        }
        
        /// <summary>
        /// Gets or sets the type of contract.
        /// Possible values: Individual, Coletivo por Adesão, Empresarial.
        /// Maps to SQL column: TipoContratacao
        /// </summary>
        public string ContractType { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the geographical coverage.
        /// Possible values: Municipal, Estadual, Regional, Nacional.
        /// Maps to SQL column: AbrangenciaGeografica
        /// </summary>
        public string GeographicalCoverage { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the healthcare segmentation.
        /// Possible values: Ambulatorial, Hospitalar, Obstetrícia, Odontológica.
        /// Maps to SQL column: SegmentacaoAssistencial
        /// </summary>
        public string HealthcareSegmentation { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the base premium for the health plan.
        /// Maps to SQL column: PremioBase
        /// </summary>
        public decimal BasePremium { get; set; } = 0.00m;

        /// <summary>
        /// Gets or sets the co-participation value for consultations.
        /// Maps to SQL column: CoparticipacaoConsulta
        /// </summary>
        public decimal ConsultationCoParticipation { get; set; } = 0.00m;

        /// <summary>
        /// Gets or sets the co-participation value for exams.
        /// Maps to SQL column: CoparticipacaoExame
        /// </summary>
        public decimal ExamCoParticipation { get; set; } = 0.00m;

        /// <summary>
        /// Gets or sets the waiting period for consultations (in days).
        /// Maps to SQL column: CarenciaConsulta
        /// </summary>
        public int ConsultationWaitingPeriod { get; set; } = 0;

        /// <summary>
        /// Gets or sets the waiting period for exams (in days).
        /// Maps to SQL column: CarenciaExame
        /// </summary>
        public int ExamWaitingPeriod { get; set; } = 0;

        /// <summary>
        /// Gets or sets the waiting period for hospitalizations (in days).
        /// Maps to SQL column: CarenciaInternacao
        /// </summary>
        public int HospitalizationWaitingPeriod { get; set; } = 0;

        /// <summary>
        /// Gets or sets the collection of plan coverages.
        /// Navigation property for the relationship with PlanCoverage.
        /// </summary>
        public ICollection<PlanCoverage>? PlanCoverages { get; set; }

        /// <summary>
        /// Gets or sets the collection of acceptance rules.
        /// Navigation property for the relationship with AcceptanceRule.
        /// </summary>
        public ICollection<AcceptanceRule>? AcceptanceRules { get; set; }

        /// <summary>
        /// Gets or sets the collection of quotes for this plan.
        /// Navigation property for the relationship with Quote.
        /// </summary>
        public ICollection<Quote>? Quotes { get; set; }
    }
}
