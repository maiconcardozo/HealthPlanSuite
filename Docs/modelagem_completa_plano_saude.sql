-- ============================================================================
-- Modelagem Completa do Domínio de Planos de Saúde
-- HealthPlanSuite - Sistema de Gestão de Planos de Saúde e Cotações
-- ============================================================================

-- Criação do banco de dados
CREATE DATABASE IF NOT EXISTS HealthPlanSuiteDB
CHARACTER SET utf8mb4 
COLLATE utf8mb4_unicode_ci;

USE HealthPlanSuiteDB;

-- ============================================================================
-- TABELAS PRINCIPAIS DO DOMÍNIO
-- ============================================================================

-- Tabela de Operadoras de Planos de Saúde
CREATE TABLE Operadoras (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Nome VARCHAR(255) NOT NULL,
    RegistroANS VARCHAR(10) UNIQUE NOT NULL,
    CNPJ VARCHAR(18) UNIQUE NOT NULL,
    Telefone VARCHAR(20),
    Email VARCHAR(255),
    Site VARCHAR(255),
    Ativa BOOLEAN DEFAULT TRUE,
    DataCriacao DATETIME DEFAULT CURRENT_TIMESTAMP,
    DataAtualizacao DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    
    INDEX idx_operadora_ans (RegistroANS),
    INDEX idx_operadora_cnpj (CNPJ),
    INDEX idx_operadora_ativa (Ativa)
);

-- Tabela de Tipos de Planos
CREATE TABLE TiposPlano (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Nome VARCHAR(100) NOT NULL,
    Descricao TEXT,
    Categoria ENUM('AMBULATORIAL', 'HOSPITALAR', 'OBSTETRICO', 'ODONTOLOGICO') NOT NULL,
    Ativo BOOLEAN DEFAULT TRUE,
    DataCriacao DATETIME DEFAULT CURRENT_TIMESTAMP,
    
    INDEX idx_tipo_categoria (Categoria),
    INDEX idx_tipo_ativo (Ativo)
);

-- Tabela de Planos de Saúde
CREATE TABLE Planos (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Nome VARCHAR(255) NOT NULL,
    Codigo VARCHAR(50) UNIQUE NOT NULL,
    OperadoraId INT NOT NULL,
    TipoPlanoId INT NOT NULL,
    Descricao TEXT,
    AbrangenciaGeografica ENUM('MUNICIPAL', 'ESTADUAL', 'REGIONAL', 'NACIONAL') NOT NULL,
    TipoContratacao ENUM('INDIVIDUAL', 'FAMILIAR', 'EMPRESARIAL') NOT NULL,
    IdadeMinima INT DEFAULT 0,
    IdadeMaxima INT DEFAULT 99,
    Ativo BOOLEAN DEFAULT TRUE,
    DataCriacao DATETIME DEFAULT CURRENT_TIMESTAMP,
    DataAtualizacao DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    
    FOREIGN KEY (OperadoraId) REFERENCES Operadoras(Id),
    FOREIGN KEY (TipoPlanoId) REFERENCES TiposPlano(Id),
    
    INDEX idx_plano_operadora (OperadoraId),
    INDEX idx_plano_tipo (TipoPlanoId),
    INDEX idx_plano_codigo (Codigo),
    INDEX idx_plano_ativo (Ativo),
    INDEX idx_plano_contratacao (TipoContratacao)
);

-- Tabela de Faixas Etárias para Precificação
CREATE TABLE FaixasEtarias (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Nome VARCHAR(50) NOT NULL,
    IdadeMinima INT NOT NULL,
    IdadeMaxima INT NOT NULL,
    Ativa BOOLEAN DEFAULT TRUE,
    
    UNIQUE KEY uk_faixa_etaria (IdadeMinima, IdadeMaxima),
    INDEX idx_faixa_ativa (Ativa)
);

-- Tabela de Preços dos Planos por Faixa Etária
CREATE TABLE PrecosPlanos (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    PlanoId INT NOT NULL,
    FaixaEtariaId INT NOT NULL,
    Valor DECIMAL(10,2) NOT NULL,
    DataInicioVigencia DATE NOT NULL,
    DataFimVigencia DATE NULL,
    Ativo BOOLEAN DEFAULT TRUE,
    DataCriacao DATETIME DEFAULT CURRENT_TIMESTAMP,
    
    FOREIGN KEY (PlanoId) REFERENCES Planos(Id),
    FOREIGN KEY (FaixaEtariaId) REFERENCES FaixasEtarias(Id),
    
    UNIQUE KEY uk_preco_vigencia (PlanoId, FaixaEtariaId, DataInicioVigencia),
    INDEX idx_preco_plano (PlanoId),
    INDEX idx_preco_faixa (FaixaEtariaId),
    INDEX idx_preco_vigencia (DataInicioVigencia, DataFimVigencia),
    INDEX idx_preco_ativo (Ativo)
);

-- Tabela de Beneficiários/Segurados
CREATE TABLE Beneficiarios (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Nome VARCHAR(255) NOT NULL,
    CPF VARCHAR(14) UNIQUE NOT NULL,
    RG VARCHAR(20),
    DataNascimento DATE NOT NULL,
    Sexo ENUM('M', 'F') NOT NULL,
    EstadoCivil ENUM('SOLTEIRO', 'CASADO', 'DIVORCIADO', 'VIUVO', 'UNIAO_ESTAVEL') NOT NULL,
    Telefone VARCHAR(20),
    Celular VARCHAR(20),
    Email VARCHAR(255),
    Profissao VARCHAR(100),
    RendaFamiliar DECIMAL(10,2),
    PossuiPlanoSaude BOOLEAN DEFAULT FALSE,
    PlanoAtual VARCHAR(255),
    DataCriacao DATETIME DEFAULT CURRENT_TIMESTAMP,
    DataAtualizacao DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    
    INDEX idx_beneficiario_cpf (CPF),
    INDEX idx_beneficiario_nascimento (DataNascimento),
    INDEX idx_beneficiario_sexo (Sexo)
);

-- Tabela de Endereços dos Beneficiários
CREATE TABLE EnderecosBeneficiarios (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    BeneficiarioId INT NOT NULL,
    TipoEndereco ENUM('RESIDENCIAL', 'COMERCIAL', 'CORRESPONDENCIA') DEFAULT 'RESIDENCIAL',
    CEP VARCHAR(10) NOT NULL,
    Logradouro VARCHAR(255) NOT NULL,
    Numero VARCHAR(20),
    Complemento VARCHAR(100),
    Bairro VARCHAR(100) NOT NULL,
    Cidade VARCHAR(100) NOT NULL,
    Estado VARCHAR(2) NOT NULL,
    Pais VARCHAR(50) DEFAULT 'Brasil',
    Principal BOOLEAN DEFAULT FALSE,
    
    FOREIGN KEY (BeneficiarioId) REFERENCES Beneficiarios(Id) ON DELETE CASCADE,
    
    INDEX idx_endereco_beneficiario (BeneficiarioId),
    INDEX idx_endereco_cep (CEP),
    INDEX idx_endereco_cidade_estado (Cidade, Estado)
);

-- Tabela de Dependentes
CREATE TABLE Dependentes (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    BeneficiarioTitularId INT NOT NULL,
    Nome VARCHAR(255) NOT NULL,
    CPF VARCHAR(14),
    DataNascimento DATE NOT NULL,
    Sexo ENUM('M', 'F') NOT NULL,
    Parentesco ENUM('CONJUGE', 'FILHO', 'PAI', 'MAE', 'IRMAO', 'OUTRO') NOT NULL,
    EstudanteAte24Anos BOOLEAN DEFAULT FALSE,
    PossuiDeficiencia BOOLEAN DEFAULT FALSE,
    DataCriacao DATETIME DEFAULT CURRENT_TIMESTAMP,
    
    FOREIGN KEY (BeneficiarioTitularId) REFERENCES Beneficiarios(Id) ON DELETE CASCADE,
    
    INDEX idx_dependente_titular (BeneficiarioTitularId),
    INDEX idx_dependente_cpf (CPF),
    INDEX idx_dependente_nascimento (DataNascimento),
    INDEX idx_dependente_parentesco (Parentesco)
);

-- Tabela de Cotações
CREATE TABLE Cotacoes (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Protocolo VARCHAR(50) UNIQUE NOT NULL,
    BeneficiarioTitularId INT NOT NULL,
    Status ENUM('PENDENTE', 'EM_ANALISE', 'APROVADA', 'REJEITADA', 'EXPIRADA') DEFAULT 'PENDENTE',
    DataCotacao DATETIME DEFAULT CURRENT_TIMESTAMP,
    DataExpiracao DATETIME NOT NULL,
    ObservacoesCliente TEXT,
    ObservacoesInternas TEXT,
    ValorTotalMensal DECIMAL(10,2),
    DataCriacao DATETIME DEFAULT CURRENT_TIMESTAMP,
    DataAtualizacao DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    
    FOREIGN KEY (BeneficiarioTitularId) REFERENCES Beneficiarios(Id),
    
    INDEX idx_cotacao_protocolo (Protocolo),
    INDEX idx_cotacao_titular (BeneficiarioTitularId),
    INDEX idx_cotacao_status (Status),
    INDEX idx_cotacao_data (DataCotacao),
    INDEX idx_cotacao_expiracao (DataExpiracao)
);

-- Tabela de Itens da Cotação (Planos cotados)
CREATE TABLE ItensCotacao (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    CotacaoId INT NOT NULL,
    PlanoId INT NOT NULL,
    QuantidadeTitulares INT DEFAULT 1,
    QuantidadeDependentes INT DEFAULT 0,
    ValorTitular DECIMAL(10,2) NOT NULL,
    ValorDependentes DECIMAL(10,2) DEFAULT 0.00,
    ValorTotal DECIMAL(10,2) NOT NULL,
    Selecionado BOOLEAN DEFAULT FALSE,
    ObservacoesItem TEXT,
    
    FOREIGN KEY (CotacaoId) REFERENCES Cotacoes(Id) ON DELETE CASCADE,
    FOREIGN KEY (PlanoId) REFERENCES Planos(Id),
    
    UNIQUE KEY uk_cotacao_plano (CotacaoId, PlanoId),
    INDEX idx_item_cotacao (CotacaoId),
    INDEX idx_item_plano (PlanoId),
    INDEX idx_item_selecionado (Selecionado)
);

-- Tabela de Beneficiários incluídos na Cotação
CREATE TABLE BeneficiariosCotacao (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    CotacaoId INT NOT NULL,
    BeneficiarioId INT NULL, -- NULL para dependentes ainda não cadastrados
    DependenteId INT NULL,
    Nome VARCHAR(255) NOT NULL,
    DataNascimento DATE NOT NULL,
    Sexo ENUM('M', 'F') NOT NULL,
    Parentesco ENUM('TITULAR', 'CONJUGE', 'FILHO', 'PAI', 'MAE', 'IRMAO', 'OUTRO') NOT NULL,
    FaixaEtariaId INT NOT NULL,
    
    FOREIGN KEY (CotacaoId) REFERENCES Cotacoes(Id) ON DELETE CASCADE,
    FOREIGN KEY (BeneficiarioId) REFERENCES Beneficiarios(Id),
    FOREIGN KEY (DependenteId) REFERENCES Dependentes(Id),
    FOREIGN KEY (FaixaEtariaId) REFERENCES FaixasEtarias(Id),
    
    INDEX idx_beneficiario_cotacao (CotacaoId),
    INDEX idx_beneficiario_ref (BeneficiarioId),
    INDEX idx_dependente_ref (DependenteId),
    INDEX idx_beneficiario_faixa (FaixaEtariaId)
);

-- Tabela de Coberturas/Serviços dos Planos
CREATE TABLE Coberturas (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Nome VARCHAR(255) NOT NULL,
    Codigo VARCHAR(50) UNIQUE NOT NULL,
    Categoria ENUM('CONSULTA', 'EXAME', 'CIRURGIA', 'INTERNACAO', 'EMERGENCIA', 'MATERNO_INFANTIL', 'ODONTOLOGIA', 'OUTROS') NOT NULL,
    Descricao TEXT,
    Obrigatoria BOOLEAN DEFAULT FALSE,
    Ativa BOOLEAN DEFAULT TRUE,
    
    INDEX idx_cobertura_codigo (Codigo),
    INDEX idx_cobertura_categoria (Categoria),
    INDEX idx_cobertura_obrigatoria (Obrigatoria),
    INDEX idx_cobertura_ativa (Ativa)
);

-- Tabela de Coberturas por Plano
CREATE TABLE CoberturasPorPlano (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    PlanoId INT NOT NULL,
    CoberturaId INT NOT NULL,
    TipoCobertura ENUM('TOTAL', 'PARCIAL', 'NAO_COBERTO') DEFAULT 'TOTAL',
    PercentualCobertura DECIMAL(5,2) DEFAULT 100.00,
    CarenciaEmDias INT DEFAULT 0,
    LimiteAnual INT NULL,
    LimiteMensal INT NULL,
    ValorFranquia DECIMAL(10,2) DEFAULT 0.00,
    ObservacoesCobertura TEXT,
    
    FOREIGN KEY (PlanoId) REFERENCES Planos(Id) ON DELETE CASCADE,
    FOREIGN KEY (CoberturaId) REFERENCES Coberturas(Id),
    
    UNIQUE KEY uk_plano_cobertura (PlanoId, CoberturaId),
    INDEX idx_cobertura_plano (PlanoId),
    INDEX idx_cobertura_tipo (TipoCobertura)
);

-- ============================================================================
-- DADOS DE EXEMPLO E CONFIGURAÇÃO INICIAL
-- ============================================================================

-- Inserção de Faixas Etárias padrão ANS
INSERT INTO FaixasEtarias (Nome, IdadeMinima, IdadeMaxima) VALUES
('0 a 18 anos', 0, 18),
('19 a 23 anos', 19, 23),
('24 a 28 anos', 24, 28),
('29 a 33 anos', 29, 33),
('34 a 38 anos', 34, 38),
('39 a 43 anos', 39, 43),
('44 a 48 anos', 44, 48),
('49 a 53 anos', 49, 53),
('54 a 58 anos', 54, 58),
('59+ anos', 59, 999);

-- Inserção de Tipos de Planos
INSERT INTO TiposPlano (Nome, Descricao, Categoria) VALUES
('Ambulatorial', 'Consultas, exames e procedimentos ambulatoriais', 'AMBULATORIAL'),
('Hospitalar com Obstetrícia', 'Internações, cirurgias e parto', 'HOSPITALAR'),
('Hospitalar sem Obstetrícia', 'Internações e cirurgias sem cobertura obstétrica', 'HOSPITALAR'),
('Referência', 'Cobertura completa ambulatorial e hospitalar', 'HOSPITALAR'),
('Odontológico Básico', 'Procedimentos odontológicos básicos', 'ODONTOLOGICO'),
('Odontológico Completo', 'Procedimentos odontológicos completos', 'ODONTOLOGICO');

-- Inserção de Operadoras de exemplo
INSERT INTO Operadoras (Nome, RegistroANS, CNPJ, Telefone, Email, Site) VALUES
('Unimed Nacional', '123456', '12.345.678/0001-90', '(11) 3000-1000', 'contato@unimed.com.br', 'www.unimed.com.br'),
('Bradesco Saúde', '789012', '98.765.432/0001-10', '(11) 3000-2000', 'saude@bradesco.com.br', 'www.bradescosaude.com.br'),
('SulAmérica Saúde', '345678', '11.222.333/0001-44', '(11) 3000-3000', 'contato@sulamerica.com.br', 'www.sulamerica.com.br'),
('Amil', '901234', '22.333.444/0001-55', '(11) 3000-4000', 'atendimento@amil.com.br', 'www.amil.com.br');

-- Inserção de Planos de exemplo
INSERT INTO Planos (Nome, Codigo, OperadoraId, TipoPlanoId, Descricao, AbrangenciaGeografica, TipoContratacao) VALUES
('Unimed Essencial', 'UNI-ESS-001', 1, 4, 'Plano referência com cobertura nacional', 'NACIONAL', 'INDIVIDUAL'),
('Unimed Empresarial', 'UNI-EMP-001', 1, 4, 'Plano empresarial com cobertura regional', 'REGIONAL', 'EMPRESARIAL'),
('Bradesco Saúde Premium', 'BRA-PRE-001', 2, 4, 'Plano premium com cobertura completa', 'NACIONAL', 'INDIVIDUAL'),
('SulAmérica Clássico', 'SUL-CLA-001', 3, 1, 'Plano ambulatorial básico', 'ESTADUAL', 'INDIVIDUAL'),
('Amil Fácil', 'AMI-FAC-001', 4, 2, 'Plano hospitalar com obstetrícia', 'REGIONAL', 'FAMILIAR');

-- Inserção de Coberturas básicas
INSERT INTO Coberturas (Nome, Codigo, Categoria, Descricao, Obrigatoria) VALUES
('Consulta Médica', 'CONS-MED', 'CONSULTA', 'Consulta com clínico geral', TRUE),
('Consulta Especialista', 'CONS-ESP', 'CONSULTA', 'Consulta com médico especialista', TRUE),
('Exames Laboratoriais', 'EXAM-LAB', 'EXAME', 'Exames de sangue, urina, fezes', TRUE),
('Exames de Imagem', 'EXAM-IMG', 'EXAME', 'Raio-X, ultrassom, tomografia', TRUE),
('Internação Clínica', 'INT-CLI', 'INTERNACAO', 'Internação para tratamento clínico', TRUE),
('Cirurgia Geral', 'CIR-GER', 'CIRURGIA', 'Procedimentos cirúrgicos gerais', TRUE),
('Emergência 24h', 'EME-24H', 'EMERGENCIA', 'Atendimento de emergência', TRUE),
('Parto Normal', 'PAR-NOR', 'MATERNO_INFANTIL', 'Parto normal', FALSE),
('Parto Cesárea', 'PAR-CES', 'MATERNO_INFANTIL', 'Parto por cesárea', FALSE),
('Consulta Odontológica', 'CONS-ODO', 'ODONTOLOGIA', 'Consulta odontológica', FALSE);

-- Inserção de Preços de exemplo (para o primeiro plano)
INSERT INTO PrecosPlanos (PlanoId, FaixaEtariaId, Valor, DataInicioVigencia) VALUES
(1, 1, 89.90, '2024-01-01'),   -- 0-18 anos
(1, 2, 125.50, '2024-01-01'),  -- 19-23 anos
(1, 3, 145.80, '2024-01-01'),  -- 24-28 anos
(1, 4, 175.20, '2024-01-01'),  -- 29-33 anos
(1, 5, 225.90, '2024-01-01'),  -- 34-38 anos
(1, 6, 285.40, '2024-01-01'),  -- 39-43 anos
(1, 7, 365.75, '2024-01-01'),  -- 44-48 anos
(1, 8, 465.20, '2024-01-01'),  -- 49-53 anos
(1, 9, 585.85, '2024-01-01'),  -- 54-58 anos
(1, 10, 745.95, '2024-01-01'); -- 59+ anos

-- Inserção de Coberturas por Plano (para o primeiro plano)
INSERT INTO CoberturasPorPlano (PlanoId, CoberturaId, TipoCobertura, PercentualCobertura, CarenciaEmDias) VALUES
(1, 1, 'TOTAL', 100.00, 0),    -- Consulta Médica
(1, 2, 'TOTAL', 100.00, 30),   -- Consulta Especialista
(1, 3, 'TOTAL', 100.00, 30),   -- Exames Laboratoriais
(1, 4, 'TOTAL', 100.00, 60),   -- Exames de Imagem
(1, 5, 'TOTAL', 100.00, 180),  -- Internação Clínica
(1, 6, 'TOTAL', 100.00, 180),  -- Cirurgia Geral
(1, 7, 'TOTAL', 100.00, 0),    -- Emergência 24h
(1, 8, 'TOTAL', 100.00, 300),  -- Parto Normal
(1, 9, 'TOTAL', 100.00, 300);  -- Parto Cesárea

-- ============================================================================
-- VIEWS ÚTEIS PARA CONSULTAS
-- ============================================================================

-- View para consultar planos com operadoras
CREATE VIEW vw_PlanosCompletos AS
SELECT 
    p.Id,
    p.Nome AS PlanoNome,
    p.Codigo AS PlanoCodigo,
    o.Nome AS OperadoraNome,
    o.RegistroANS,
    tp.Nome AS TipoPlano,
    tp.Categoria,
    p.AbrangenciaGeografica,
    p.TipoContratacao,
    p.Ativo
FROM Planos p
INNER JOIN Operadoras o ON p.OperadoraId = o.Id
INNER JOIN TiposPlano tp ON p.TipoPlanoId = tp.Id;

-- View para consultar preços vigentes dos planos
CREATE VIEW vw_PrecosVigentes AS
SELECT 
    pp.PlanoId,
    p.Nome AS PlanoNome,
    fe.Nome AS FaixaEtaria,
    fe.IdadeMinima,
    fe.IdadeMaxima,
    pp.Valor,
    pp.DataInicioVigencia,
    pp.DataFimVigencia
FROM PrecosPlanos pp
INNER JOIN Planos p ON pp.PlanoId = p.Id
INNER JOIN FaixasEtarias fe ON pp.FaixaEtariaId = fe.Id
WHERE pp.Ativo = TRUE 
AND (pp.DataFimVigencia IS NULL OR pp.DataFimVigencia >= CURDATE());

-- View para consultar cotações com status
CREATE VIEW vw_CotacoesResumo AS
SELECT 
    c.Id,
    c.Protocolo,
    b.Nome AS BeneficiarioNome,
    b.CPF,
    c.Status,
    c.DataCotacao,
    c.DataExpiracao,
    c.ValorTotalMensal,
    COUNT(ic.Id) AS QuantidadePlanos,
    SUM(CASE WHEN ic.Selecionado = TRUE THEN 1 ELSE 0 END) AS PlanosSelecionados
FROM Cotacoes c
INNER JOIN Beneficiarios b ON c.BeneficiarioTitularId = b.Id
LEFT JOIN ItensCotacao ic ON c.Id = ic.CotacaoId
GROUP BY c.Id, c.Protocolo, b.Nome, b.CPF, c.Status, c.DataCotacao, c.DataExpiracao, c.ValorTotalMensal;

-- ============================================================================
-- TRIGGERS PARA AUDITORIA E VALIDAÇÕES
-- ============================================================================

-- Trigger para gerar protocolo automático para cotações
DELIMITER $$
CREATE TRIGGER tr_cotacao_protocolo 
BEFORE INSERT ON Cotacoes
FOR EACH ROW
BEGIN
    IF NEW.Protocolo IS NULL OR NEW.Protocolo = '' THEN
        SET NEW.Protocolo = CONCAT('COT', YEAR(NOW()), LPAD(MONTH(NOW()), 2, '0'), LPAD(DAY(NOW()), 2, '0'), LPAD(HOUR(NOW()), 2, '0'), LPAD(MINUTE(NOW()), 2, '0'), LPAD(SECOND(NOW()), 2, '0'));
    END IF;
    
    IF NEW.DataExpiracao IS NULL THEN
        SET NEW.DataExpiracao = DATE_ADD(NOW(), INTERVAL 30 DAY);
    END IF;
END$$
DELIMITER ;

-- Trigger para calcular valor total da cotação
DELIMITER $$
CREATE TRIGGER tr_item_cotacao_valor_total 
BEFORE INSERT ON ItensCotacao
FOR EACH ROW
BEGIN
    SET NEW.ValorTotal = NEW.ValorTitular + NEW.ValorDependentes;
END$$
DELIMITER ;

DELIMITER $$
CREATE TRIGGER tr_item_cotacao_valor_total_update 
BEFORE UPDATE ON ItensCotacao
FOR EACH ROW
BEGIN
    SET NEW.ValorTotal = NEW.ValorTitular + NEW.ValorDependentes;
END$$
DELIMITER ;

-- ============================================================================
-- ÍNDICES ADICIONAIS PARA PERFORMANCE
-- ============================================================================

-- Índices compostos para queries frequentes
CREATE INDEX idx_planos_operadora_tipo ON Planos(OperadoraId, TipoPlanoId, Ativo);
CREATE INDEX idx_precos_plano_vigencia ON PrecosPlanos(PlanoId, DataInicioVigencia, DataFimVigencia, Ativo);
CREATE INDEX idx_beneficiarios_nome_cpf ON Beneficiarios(Nome, CPF);
CREATE INDEX idx_cotacoes_status_data ON Cotacoes(Status, DataCotacao);
CREATE INDEX idx_itens_cotacao_selecionado ON ItensCotacao(CotacaoId, Selecionado);

-- ============================================================================
-- COMENTÁRIOS DAS TABELAS
-- ============================================================================

ALTER TABLE Operadoras COMMENT = 'Operadoras de planos de saúde registradas na ANS';
ALTER TABLE TiposPlano COMMENT = 'Tipos de planos de saúde conforme classificação ANS';
ALTER TABLE Planos COMMENT = 'Planos de saúde oferecidos pelas operadoras';
ALTER TABLE FaixasEtarias COMMENT = 'Faixas etárias para cálculo de preços dos planos';
ALTER TABLE PrecosPlanos COMMENT = 'Preços dos planos por faixa etária com controle de vigência';
ALTER TABLE Beneficiarios COMMENT = 'Beneficiários/segurados principais (titulares)';
ALTER TABLE EnderecosBeneficiarios COMMENT = 'Endereços dos beneficiários';
ALTER TABLE Dependentes COMMENT = 'Dependentes dos beneficiários titulares';
ALTER TABLE Cotacoes COMMENT = 'Cotações de planos de saúde solicitadas';
ALTER TABLE ItensCotacao COMMENT = 'Planos incluídos em cada cotação com valores calculados';
ALTER TABLE BeneficiariosCotacao COMMENT = 'Beneficiários e dependentes incluídos na cotação';
ALTER TABLE Coberturas COMMENT = 'Coberturas/serviços médicos disponíveis';
ALTER TABLE CoberturasPorPlano COMMENT = 'Coberturas oferecidas por cada plano com detalhes específicos';