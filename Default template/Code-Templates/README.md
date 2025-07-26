# 🧩 Templates de Código - Padrões Implementados

Esta seção contém templates de código para todos os padrões arquiteturais utilizados no projeto. Use estes templates como base para criar novos componentes.

## 📋 Índice

- [Domain Layer Templates](#domain-layer-templates)
- [Service Layer Templates](#service-layer-templates)
- [Repository Layer Templates](#repository-layer-templates)
- [Infrastructure Templates](#infrastructure-templates)
- [API Layer Templates](#api-layer-templates)
- [DTO Templates](#dto-templates)

## 🏛️ Domain Layer Templates

### 1. **Entidade de Domínio Base**
```csharp
// Domain/Implementation/[NomeEntidade].cs
using Foundation.Base.Domain.Implementation;

namespace [Projeto].Domain.Implementation
{
    public class [NomeEntidade] : Entity
    {
        // Propriedades privadas com validação
        public string Nome { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        
        // Relacionamentos (se necessário)
        public virtual ICollection<[EntidadeRelacionada]> [Relacionamentos] { get; set; }
        
        // Construtor privado para EF Core
        private [NomeEntidade]() { }
        
        // Construtor público com validações
        public [NomeEntidade](string nome)
        {
            SetNome(nome);
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }
        
        // Métodos de negócio
        public void SetNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome não pode ser vazio");
                
            Nome = nome;
            UpdatedAt = DateTime.UtcNow;
        }
        
        // Métodos de validação de negócio
        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Nome);
        }
    }
}
```

### 2. **Interface de Domínio**
```csharp
// Domain/Interface/I[NomeEntidade].cs
namespace [Projeto].Domain.Interface
{
    public interface I[NomeEntidade]
    {
        int Id { get; }
        string Nome { get; }
        DateTime CreatedAt { get; }
        DateTime UpdatedAt { get; }
        
        void SetNome(string nome);
        bool IsValid();
    }
}
```

## 🔧 Service Layer Templates

### 1. **Interface de Serviço**
```csharp
// Services/Interface/I[Nome]Service.cs
using [Projeto].DTO;

namespace [Projeto].Services.Interface
{
    public interface I[Nome]Service
    {
        Task<[Nome]ResponseDTO> GetByIdAsync(int id);
        Task<IEnumerable<[Nome]ResponseDTO>> GetAllAsync();
        Task<[Nome]ResponseDTO> CreateAsync([Nome]RequestDTO request);
        Task<[Nome]ResponseDTO> UpdateAsync(int id, [Nome]RequestDTO request);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
```

### 2. **Implementação de Serviço**
```csharp
// Services/Implementation/[Nome]Service.cs
using [Projeto].Services.Interface;
using [Projeto].UnitOfWork.Interface;
using [Projeto].Domain.Implementation;
using [Projeto].DTO;
using [Projeto].Mapping;

namespace [Projeto].Services.Implementation
{
    public class [Nome]Service : I[Nome]Service
    {
        private readonly I[Projeto]UnitOfWork _unitOfWork;
        
        public [Nome]Service(I[Projeto]UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task<[Nome]ResponseDTO> GetByIdAsync(int id)
        {
            var entity = await _unitOfWork.[Nome]Repository.GetByIdAsync(id);
            
            if (entity == null)
                throw new KeyNotFoundException($"[Nome] com ID {id} não encontrado");
                
            return entity.MapToResponseDTO();
        }
        
        public async Task<IEnumerable<[Nome]ResponseDTO>> GetAllAsync()
        {
            var entities = await _unitOfWork.[Nome]Repository.GetAllAsync();
            return entities.Select(e => e.MapToResponseDTO());
        }
        
        public async Task<[Nome]ResponseDTO> CreateAsync([Nome]RequestDTO request)
        {
            // Validações de negócio
            await ValidateBusinessRulesForCreate(request);
            
            // Criar entidade
            var entity = new [Nome](request.Nome);
            
            // Persistir
            await _unitOfWork.[Nome]Repository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            
            return entity.MapToResponseDTO();
        }
        
        public async Task<[Nome]ResponseDTO> UpdateAsync(int id, [Nome]RequestDTO request)
        {
            var entity = await _unitOfWork.[Nome]Repository.GetByIdAsync(id);
            
            if (entity == null)
                throw new KeyNotFoundException($"[Nome] com ID {id} não encontrado");
            
            // Validações de negócio
            await ValidateBusinessRulesForUpdate(request, entity);
            
            // Atualizar entidade
            entity.SetNome(request.Nome);
            
            // Persistir
            await _unitOfWork.[Nome]Repository.UpdateAsync(entity);
            await _unitOfWork.CommitAsync();
            
            return entity.MapToResponseDTO();
        }
        
        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _unitOfWork.[Nome]Repository.GetByIdAsync(id);
            
            if (entity == null)
                return false;
            
            await _unitOfWork.[Nome]Repository.DeleteAsync(entity);
            await _unitOfWork.CommitAsync();
            
            return true;
        }
        
        public async Task<bool> ExistsAsync(int id)
        {
            return await _unitOfWork.[Nome]Repository.ExistsAsync(id);
        }
        
        // Métodos de validação privados
        private async Task ValidateBusinessRulesForCreate([Nome]RequestDTO request)
        {
            // Implementar validações específicas de criação
            if (string.IsNullOrWhiteSpace(request.Nome))
                throw new ArgumentException("Nome é obrigatório");
                
            // Exemplo: verificar duplicatas
            var existingEntity = await _unitOfWork.[Nome]Repository.GetByNomeAsync(request.Nome);
            if (existingEntity != null)
                throw new InvalidOperationException("Já existe um(a) [Nome] com este nome");
        }
        
        private async Task ValidateBusinessRulesForUpdate([Nome]RequestDTO request, [Nome] existingEntity)
        {
            // Implementar validações específicas de atualização
            if (string.IsNullOrWhiteSpace(request.Nome))
                throw new ArgumentException("Nome é obrigatório");
                
            // Verificar se o novo nome já existe em outra entidade
            var duplicateEntity = await _unitOfWork.[Nome]Repository.GetByNomeAsync(request.Nome);
            if (duplicateEntity != null && duplicateEntity.Id != existingEntity.Id)
                throw new InvalidOperationException("Já existe um(a) [Nome] com este nome");
        }
    }
}
```

## 🗃️ Repository Layer Templates

### 1. **Interface de Repositório**
```csharp
// Repository/Interface/I[Nome]Repository.cs
using Foundation.Base.Repository.Interface;
using [Projeto].Domain.Implementation;

namespace [Projeto].Repository.Interface
{
    public interface I[Nome]Repository : IEntityRepository<[Nome]>
    {
        Task<[Nome]?> GetByNomeAsync(string nome);
        Task<IEnumerable<[Nome]>> GetByFilterAsync(string filter);
        Task<bool> ExistsByNomeAsync(string nome);
        Task<int> CountAsync();
    }
}
```

### 2. **Implementação de Repositório**
```csharp
// Repository/Implementation/[Nome]Repository.cs
using Foundation.Base.Repository.Implementation;
using [Projeto].Repository.Interface;
using [Projeto].Domain.Implementation;
using [Projeto].Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;

namespace [Projeto].Repository.Implementation
{
    public class [Nome]Repository : EntityRepository<[Nome]>, I[Nome]Repository
    {
        public [Nome]Repository(I[Projeto]Context context) : base(context) { }
        
        public async Task<[Nome]?> GetByNomeAsync(string nome)
        {
            return await Context.Set<[Nome]>()
                .FirstOrDefaultAsync(e => e.Nome == nome);
        }
        
        public async Task<IEnumerable<[Nome]>> GetByFilterAsync(string filter)
        {
            var query = Context.Set<[Nome]>().AsQueryable();
            
            if (!string.IsNullOrWhiteSpace(filter))
            {
                query = query.Where(e => e.Nome.Contains(filter));
            }
            
            return await query
                .OrderBy(e => e.Nome)
                .ToListAsync();
        }
        
        public async Task<bool> ExistsByNomeAsync(string nome)
        {
            return await Context.Set<[Nome]>()
                .AnyAsync(e => e.Nome == nome);
        }
        
        public async Task<int> CountAsync()
        {
            return await Context.Set<[Nome]>().CountAsync();
        }
    }
}
```

## 🏗️ Infrastructure Templates

### 1. **Mapeamento de Entidade (EF Core)**
```csharp
// Infrastructure/Implementation/[Nome]Map.cs
using Foundation.Base.Infrastructure.Data;
using [Projeto].Domain.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace [Projeto].Infrastructure.Implementation
{
    public class [Nome]Map : EntityConfiguration<[Nome]>
    {
        public override void Configure(EntityTypeBuilder<[Nome]> builder)
        {
            base.Configure(builder);
            
            // Nome da tabela
            builder.ToTable("[NomePlural]");
            
            // Configuração das propriedades
            builder.Property(e => e.Nome)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("nome");
                
            builder.Property(e => e.CreatedAt)
                .IsRequired()
                .HasColumnName("created_at");
                
            builder.Property(e => e.UpdatedAt)
                .IsRequired()
                .HasColumnName("updated_at");
            
            // Índices
            builder.HasIndex(e => e.Nome)
                .IsUnique()
                .HasDatabaseName("IX_[NomePlural]_Nome");
            
            // Relacionamentos (se necessário)
            builder.HasMany(e => e.[Relacionamentos])
                .WithOne(r => r.[Nome])
                .HasForeignKey(r => r.[Nome]Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
```

### 2. **Contexto de Dados**
```csharp
// Infrastructure/Implementation/[Projeto]Context.cs
using Foundation.Base.Infrastructure.Data;
using [Projeto].Infrastructure.Interface;
using [Projeto].Domain.Implementation;
using Microsoft.EntityFrameworkCore;

namespace [Projeto].Infrastructure.Implementation
{
    public class [Projeto]Context : EntityContext, I[Projeto]Context
    {
        public [Projeto]Context(DbContextOptions<[Projeto]Context> options) : base(options) { }
        
        // DbSets
        public DbSet<[Nome]> [NomePlural] { get; set; }
        // Adicione outros DbSets aqui
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Aplicar configurações de mapeamento
            modelBuilder.ApplyConfiguration(new [Nome]Map());
            // Adicione outras configurações aqui
        }
    }
}
```

### 3. **Unit of Work**
```csharp
// UnitOfWork/Interface/I[Projeto]UnitOfWork.cs
using Foundation.Base.UnitOfWork.Interface;
using [Projeto].Repository.Interface;

namespace [Projeto].UnitOfWork.Interface
{
    public interface I[Projeto]UnitOfWork : IBaseUnitOfWork
    {
        I[Nome]Repository [Nome]Repository { get; }
        // Adicione outros repositórios aqui
    }
}

// UnitOfWork/Implementation/[Projeto]UnitOfWork.cs
using Foundation.Base.UnitOfWork.Implementation;
using [Projeto].UnitOfWork.Interface;
using [Projeto].Repository.Interface;
using [Projeto].Repository.Implementation;
using [Projeto].Infrastructure.Interface;

namespace [Projeto].UnitOfWork.Implementation
{
    public class [Projeto]UnitOfWork : BaseUnitOfWork, I[Projeto]UnitOfWork
    {
        public I[Nome]Repository [Nome]Repository { get; private set; }
        // Adicione outros repositórios aqui
        
        public [Projeto]UnitOfWork(I[Projeto]Context context) : base(context)
        {
            [Nome]Repository = new [Nome]Repository(context);
            // Inicialize outros repositórios aqui
        }
    }
}
```

## 🌐 API Layer Templates

### 1. **Controller REST**
```csharp
// Controllers/[Nome]Controller.cs
using [Projeto].API.Resource;
using [Projeto].API.Swagger;
using [Projeto].Services.Interface;
using [Projeto].DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;
using Foundation.Base.Util;

namespace [Projeto].API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class [Nome]Controller : ControllerBase
    {
        private readonly I[Nome]Service _service;
        
        public [Nome]Controller(I[Nome]Service service)
        {
            _service = service;
        }
        
        [HttpGet([Nome]Routes.Get[NomePlural])]
        [SwaggerOperation(Summary = "Listar todos os [NomePlural]")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<[Nome]ResponseDTO>))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> Get[NomePlural]()
        {
            try
            {
                var [nomeMinusculo]s = await _service.GetAllAsync();
                return Ok([nomeMinusculo]s);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ProblemDetails
                {
                    Status = 500,
                    Title = "Erro interno do servidor",
                    Detail = ex.Message
                });
            }
        }
        
        [HttpGet([Nome]Routes.Get[Nome]ById)]
        [SwaggerOperation(Summary = "Obter [Nome] por ID")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof([Nome]ResponseDTO))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> Get[Nome]ById(int id)
        {
            try
            {
                var [nomeMinusculo] = await _service.GetByIdAsync(id);
                return Ok([nomeMinusculo]);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new ProblemDetails
                {
                    Status = 404,
                    Title = "[Nome] não encontrado",
                    Detail = $"[Nome] com ID {id} não foi encontrado"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ProblemDetails
                {
                    Status = 500,
                    Title = "Erro interno do servidor",
                    Detail = ex.Message
                });
            }
        }
        
        [HttpPost([Nome]Routes.Add[Nome])]
        [SwaggerOperation(Summary = "Criar novo [Nome]")]
        [SwaggerResponse(StatusCodes.Status201Created, Type = typeof([Nome]ResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> Create[Nome]([FromBody] [Nome]RequestDTO request, [FromServices] IServiceProvider serviceProvider)
        {
            var validationResult = await ValidationHelper.ValidateEntityAsync(request, serviceProvider, this);
            if (validationResult != null) return validationResult;
            
            try
            {
                var [nomeMinusculo] = await _service.CreateAsync(request);
                return CreatedAtAction(nameof(Get[Nome]ById), new { id = [nomeMinusculo].Id }, [nomeMinusculo]);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new ProblemDetails
                {
                    Status = 400,
                    Title = "Dados inválidos",
                    Detail = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ProblemDetails
                {
                    Status = 500,
                    Title = "Erro interno do servidor",
                    Detail = ex.Message
                });
            }
        }
        
        [HttpPut([Nome]Routes.Update[Nome])]
        [SwaggerOperation(Summary = "Atualizar [Nome]")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof([Nome]ResponseDTO))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> Update[Nome](int id, [FromBody] [Nome]RequestDTO request, [FromServices] IServiceProvider serviceProvider)
        {
            var validationResult = await ValidationHelper.ValidateEntityAsync(request, serviceProvider, this);
            if (validationResult != null) return validationResult;
            
            try
            {
                var [nomeMinusculo] = await _service.UpdateAsync(id, request);
                return Ok([nomeMinusculo]);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new ProblemDetails
                {
                    Status = 404,
                    Title = "[Nome] não encontrado",
                    Detail = $"[Nome] com ID {id} não foi encontrado"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ProblemDetails
                {
                    Status = 500,
                    Title = "Erro interno do servidor",
                    Detail = ex.Message
                });
            }
        }
        
        [HttpDelete([Nome]Routes.Delete[Nome])]
        [SwaggerOperation(Summary = "Excluir [Nome]")]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> Delete[Nome](int id)
        {
            try
            {
                var deleted = await _service.DeleteAsync(id);
                
                if (!deleted)
                {
                    return NotFound(new ProblemDetails
                    {
                        Status = 404,
                        Title = "[Nome] não encontrado",
                        Detail = $"[Nome] com ID {id} não foi encontrado"
                    });
                }
                
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ProblemDetails
                {
                    Status = 500,
                    Title = "Erro interno do servidor",
                    Detail = ex.Message
                });
            }
        }
    }
}
```

### 2. **Constantes de Rotas**
```csharp
// Swagger/[Nome]Routes.cs
namespace [Projeto].API.Swagger
{
    public static class [Nome]Routes
    {
        public const string Get[NomePlural] = "Get[NomePlural]";
        public const string Get[Nome]ById = "Get[Nome]ById/{id:int}";
        public const string Add[Nome] = "Add[Nome]";
        public const string Update[Nome] = "Update[Nome]/{id:int}";
        public const string Delete[Nome] = "Delete[Nome]/{id:int}";
    }
}
```

## 📦 DTO Templates

### 1. **DTO de Request**
```csharp
// DTO/[Nome]RequestDTO.cs
using System.ComponentModel.DataAnnotations;

namespace [Projeto].DTO
{
    public class [Nome]RequestDTO
    {
        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(100, ErrorMessage = "Nome deve ter no máximo 100 caracteres")]
        public string Nome { get; set; } = string.Empty;
        
        // Adicione outras propriedades conforme necessário
    }
}
```

### 2. **DTO de Response**
```csharp
// DTO/[Nome]ResponseDTO.cs
namespace [Projeto].DTO
{
    public class [Nome]ResponseDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // Adicione outras propriedades conforme necessário
    }
}
```

### 3. **Validador FluentValidation**
```csharp
// DTO/[Nome]RequestValidator.cs
using FluentValidation;

namespace [Projeto].DTO
{
    public class [Nome]RequestValidator : AbstractValidator<[Nome]RequestDTO>
    {
        public [Nome]RequestValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("Nome é obrigatório")
                .MaximumLength(100).WithMessage("Nome deve ter no máximo 100 caracteres")
                .MinimumLength(2).WithMessage("Nome deve ter no mínimo 2 caracteres");
                
            // Adicione outras validações conforme necessário
        }
    }
}
```

### 4. **Extensões de Mapeamento**
```csharp
// Mapping/[Nome]Extensions.cs
using [Projeto].Domain.Implementation;
using [Projeto].DTO;

namespace [Projeto].Mapping
{
    public static class [Nome]Extensions
    {
        public static [Nome]ResponseDTO MapToResponseDTO(this [Nome] entity)
        {
            return new [Nome]ResponseDTO
            {
                Id = entity.Id,
                Nome = entity.Nome,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }
        
        public static [Nome] MapToEntity(this [Nome]RequestDTO dto)
        {
            return new [Nome](dto.Nome);
        }
    }
}
```

---

## 💡 Como Usar os Templates

1. **Substitua** `[Projeto]` pelo nome do seu projeto
2. **Substitua** `[Nome]` pelo nome da sua entidade (singular)
3. **Substitua** `[NomePlural]` pelo nome da sua entidade (plural)
4. **Substitua** `[nomeMinusculo]` pelo nome da sua entidade em camelCase
5. **Adapte** as propriedades específicas da sua entidade
6. **Configure** os relacionamentos conforme necessário

Estes templates garantem consistência e seguem todos os padrões arquiteturais do projeto!