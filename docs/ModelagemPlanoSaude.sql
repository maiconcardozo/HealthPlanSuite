-- ==================================================
-- MODELAGEM RELACIONAL COMPLETA - SISTEMA PLANO DE SAÚDE
-- Arquivo: ModelagemPlanoSaude.sql
-- Versão: 1.0
-- Data: 2025-01-17
-- Descrição: Modelagem completa para sistema de cotação de planos de saúde
-- ==================================================

-- Configurações do banco
SET FOREIGN_KEY_CHECKS = 0;
SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";

-- ==================================================
-- TABELA: Empresas (Seguradoras)
-- ==================================================
CREATE TABLE IF NOT EXISTS `Empresa` (
    `IdCompany` int NOT NULL AUTO_INCREMENT,
    `Nome` varchar(255) NOT NULL COMMENT 'Nome da empresa seguradora',
    `NomeFantasia` varchar(255) DEFAULT NULL COMMENT 'Nome fantasia da empresa',
    `CNPJ` varchar(18) NOT NULL UNIQUE COMMENT 'CNPJ da empresa (formato: XX.XXX.XXX/XXXX-XX)',
    `Email` varchar(255) DEFAULT NULL COMMENT 'Email de contato da empresa',
    `Telefone` varchar(20) DEFAULT NULL COMMENT 'Telefone de contato',
    `Endereco` varchar(500) DEFAULT NULL COMMENT 'Endereço completo',
    `Cidade` varchar(100) DEFAULT NULL COMMENT 'Cidade',
    `Estado` varchar(2) DEFAULT NULL COMMENT 'UF do estado',
    `CEP` varchar(10) DEFAULT NULL COMMENT 'CEP (formato: XXXXX-XXX)',
    `IsActive` tinyint(1) NOT NULL DEFAULT '1' COMMENT 'Registro ativo',
    `DtCreated` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) COMMENT 'Data de criação',
    `DtUpdated` datetime(6) DEFAULT NULL COMMENT 'Data de atualização',
    `DtDeleted` datetime(6) DEFAULT NULL COMMENT 'Data de exclusão lógica',
    `CreatedBy` varchar(256) NOT NULL DEFAULT 'System' COMMENT 'Usuário que criou',
    `UpdatedBy` varchar(256) DEFAULT NULL COMMENT 'Usuário que atualizou',
    `DeletedBy` varchar(256) DEFAULT NULL COMMENT 'Usuário que excluiu',
    PRIMARY KEY (`IdCompany`),
    INDEX `IX_Empresa_CNPJ` (`CNPJ`),
    INDEX `IX_Empresa_Nome` (`Nome`),
    INDEX `IX_Empresa_IsActive` (`IsActive`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci COMMENT='Empresas seguradoras que oferecem planos de saúde';

-- ==================================================
-- TABELA: Faixas Etárias
-- ==================================================
CREATE TABLE IF NOT EXISTS `FaixaEtaria` (
    `IdAgeRange` int NOT NULL AUTO_INCREMENT,
    `Descricao` varchar(50) NOT NULL COMMENT 'Descrição da faixa etária (ex: 0-18 anos)',
    `IdadeMinima` int NOT NULL COMMENT 'Idade mínima da faixa',
    `IdadeMaxima` int NOT NULL COMMENT 'Idade máxima da faixa',
    `Multiplicador` decimal(10,4) NOT NULL DEFAULT '1.0000' COMMENT 'Multiplicador para cálculo do prêmio',
    `IsActive` tinyint(1) NOT NULL DEFAULT '1' COMMENT 'Registro ativo',
    `DtCreated` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) COMMENT 'Data de criação',
    `DtUpdated` datetime(6) DEFAULT NULL COMMENT 'Data de atualização',
    `DtDeleted` datetime(6) DEFAULT NULL COMMENT 'Data de exclusão lógica',
    `CreatedBy` varchar(256) NOT NULL DEFAULT 'System' COMMENT 'Usuário que criou',
    `UpdatedBy` varchar(256) DEFAULT NULL COMMENT 'Usuário que atualizou',
    `DeletedBy` varchar(256) DEFAULT NULL COMMENT 'Usuário que excluiu',
    PRIMARY KEY (`IdAgeRange`),
    INDEX `IX_FaixaEtaria_IdadeMinima_IdadeMaxima` (`IdadeMinima`, `IdadeMaxima`),
    INDEX `IX_FaixaEtaria_IsActive` (`IsActive`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci COMMENT='Faixas etárias para cálculo de prêmios';

-- ==================================================
-- TABELA: Acomodações
-- ==================================================
CREATE TABLE IF NOT EXISTS `Acomodacao` (
    `IdAccommodation` int NOT NULL AUTO_INCREMENT,
    `Tipo` varchar(50) NOT NULL COMMENT 'Tipo de acomodação (Apartamento, Enfermaria, etc.)',
    `Descricao` varchar(255) NOT NULL COMMENT 'Descrição detalhada da acomodação',
    `ValorAdicional` decimal(10,2) NOT NULL DEFAULT '0.00' COMMENT 'Valor adicional da acomodação',
    `IsActive` tinyint(1) NOT NULL DEFAULT '1' COMMENT 'Registro ativo',
    `DtCreated` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) COMMENT 'Data de criação',
    `DtUpdated` datetime(6) DEFAULT NULL COMMENT 'Data de atualização',
    `DtDeleted` datetime(6) DEFAULT NULL COMMENT 'Data de exclusão lógica',
    `CreatedBy` varchar(256) NOT NULL DEFAULT 'System' COMMENT 'Usuário que criou',
    `UpdatedBy` varchar(256) DEFAULT NULL COMMENT 'Usuário que atualizou',
    `DeletedBy` varchar(256) DEFAULT NULL COMMENT 'Usuário que excluiu',
    PRIMARY KEY (`IdAccommodation`),
    INDEX `IX_Acomodacao_Tipo` (`Tipo`),
    INDEX `IX_Acomodacao_IsActive` (`IsActive`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci COMMENT='Tipos de acomodações hospitalares';

-- ==================================================
-- TABELA: Coberturas
-- ==================================================
CREATE TABLE IF NOT EXISTS `Cobertura` (
    `IdCoverage` int NOT NULL AUTO_INCREMENT,
    `Nome` varchar(255) NOT NULL COMMENT 'Nome da cobertura',
    `Descricao` text DEFAULT NULL COMMENT 'Descrição detalhada da cobertura',
    `Tipo` varchar(100) NOT NULL COMMENT 'Tipo de cobertura (Ambulatorial, Hospitalar, Obstétrica, etc.)',
    `ValorBase` decimal(10,2) NOT NULL DEFAULT '0.00' COMMENT 'Valor base da cobertura',
    `IsObrigatoria` tinyint(1) NOT NULL DEFAULT '0' COMMENT 'Indica se a cobertura é obrigatória',
    `IsActive` tinyint(1) NOT NULL DEFAULT '1' COMMENT 'Registro ativo',
    `DtCreated` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) COMMENT 'Data de criação',
    `DtUpdated` datetime(6) DEFAULT NULL COMMENT 'Data de atualização',
    `DtDeleted` datetime(6) DEFAULT NULL COMMENT 'Data de exclusão lógica',
    `CreatedBy` varchar(256) NOT NULL DEFAULT 'System' COMMENT 'Usuário que criou',
    `UpdatedBy` varchar(256) DEFAULT NULL COMMENT 'Usuário que atualizou',
    `DeletedBy` varchar(256) DEFAULT NULL COMMENT 'Usuário que excluiu',
    PRIMARY KEY (`IdCoverage`),
    INDEX `IX_Cobertura_Tipo` (`Tipo`),
    INDEX `IX_Cobertura_Nome` (`Nome`),
    INDEX `IX_Cobertura_IsActive` (`IsActive`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci COMMENT='Coberturas disponíveis para os planos de saúde';

-- ==================================================
-- TABELA: Planos de Saúde
-- ==================================================
CREATE TABLE IF NOT EXISTS `PlanoSaude` (
    `IdHealthPlan` int NOT NULL AUTO_INCREMENT,
    `IdCompany` int NOT NULL COMMENT 'ID da empresa que oferece o plano',
    `IdAccommodation` int NOT NULL COMMENT 'ID da acomodação padrão do plano',
    `Nome` varchar(255) NOT NULL COMMENT 'Nome do plano de saúde',
    `Descricao` text DEFAULT NULL COMMENT 'Descrição detalhada do plano',
    `Codigo` varchar(50) NOT NULL COMMENT 'Código único do plano',
    `Categoria` varchar(100) NOT NULL COMMENT 'Categoria do plano (Individual, Familiar, Empresarial)',
    `TipoContratacao` varchar(50) NOT NULL COMMENT 'Tipo de contratação (Individual, Coletivo por Adesão, Empresarial)',
    `AbrangenciaGeografica` varchar(100) NOT NULL COMMENT 'Abrangência (Municipal, Estadual, Regional, Nacional)',
    `SegmentacaoAssistencial` varchar(100) NOT NULL COMMENT 'Segmentação (Ambulatorial, Hospitalar, Obstetrícia, Odontológica)',
    `PremioBase` decimal(10,2) NOT NULL COMMENT 'Prêmio base mensal do plano',
    `CoparticipacaoConsulta` decimal(10,2) DEFAULT '0.00' COMMENT 'Valor da coparticipação em consultas',
    `CoparticipacaoExame` decimal(10,2) DEFAULT '0.00' COMMENT 'Valor da coparticipação em exames',
    `CarenciaConsulta` int DEFAULT '0' COMMENT 'Carência para consultas (em dias)',
    `CarenciaExame` int DEFAULT '0' COMMENT 'Carência para exames (em dias)',
    `CarenciaInternacao` int DEFAULT '0' COMMENT 'Carência para internações (em dias)',
    `IsActive` tinyint(1) NOT NULL DEFAULT '1' COMMENT 'Registro ativo',
    `DtCreated` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) COMMENT 'Data de criação',
    `DtUpdated` datetime(6) DEFAULT NULL COMMENT 'Data de atualização',
    `DtDeleted` datetime(6) DEFAULT NULL COMMENT 'Data de exclusão lógica',
    `CreatedBy` varchar(256) NOT NULL DEFAULT 'System' COMMENT 'Usuário que criou',
    `UpdatedBy` varchar(256) DEFAULT NULL COMMENT 'Usuário que atualizou',
    `DeletedBy` varchar(256) DEFAULT NULL COMMENT 'Usuário que excluiu',
    PRIMARY KEY (`IdHealthPlan`),
    UNIQUE KEY `UK_PlanoSaude_Codigo` (`Codigo`),
    INDEX `IX_PlanoSaude_IdCompany` (`IdCompany`),
    INDEX `IX_PlanoSaude_IdAccommodation` (`IdAccommodation`),
    INDEX `IX_PlanoSaude_Categoria` (`Categoria`),
    INDEX `IX_PlanoSaude_Nome` (`Nome`),
    INDEX `IX_PlanoSaude_IsActive` (`IsActive`),
    CONSTRAINT `FK_PlanoSaude_Empresa` FOREIGN KEY (`IdCompany`) REFERENCES `Empresa` (`IdCompany`) ON DELETE CASCADE,
    CONSTRAINT `FK_PlanoSaude_Acomodacao` FOREIGN KEY (`IdAccommodation`) REFERENCES `Acomodacao` (`IdAccommodation`) ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci COMMENT='Planos de saúde oferecidos pelas empresas';

-- ==================================================
-- TABELA: Coberturas do Plano (Relacionamento N:N)
-- ==================================================
CREATE TABLE IF NOT EXISTS `PlanoCobertura` (
    `IdPlanoCobertura` int NOT NULL AUTO_INCREMENT,
    `IdHealthPlan` int NOT NULL COMMENT 'ID do plano de saúde',
    `IdCoverage` int NOT NULL COMMENT 'ID da cobertura',
    `ValorPremio` decimal(10,2) NOT NULL DEFAULT '0.00' COMMENT 'Valor adicional da cobertura no plano',
    `IsIncluida` tinyint(1) NOT NULL DEFAULT '1' COMMENT 'Indica se a cobertura está incluída no plano',
    `IsActive` tinyint(1) NOT NULL DEFAULT '1' COMMENT 'Registro ativo',
    `DtCreated` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) COMMENT 'Data de criação',
    `DtUpdated` datetime(6) DEFAULT NULL COMMENT 'Data de atualização',
    `DtDeleted` datetime(6) DEFAULT NULL COMMENT 'Data de exclusão lógica',
    `CreatedBy` varchar(256) NOT NULL DEFAULT 'System' COMMENT 'Usuário que criou',
    `UpdatedBy` varchar(256) DEFAULT NULL COMMENT 'Usuário que atualizou',
    `DeletedBy` varchar(256) DEFAULT NULL COMMENT 'Usuário que excluiu',
    PRIMARY KEY (`IdPlanoCobertura`),
    UNIQUE KEY `UK_PlanoCobertura_PlanoSaude_Cobertura` (`IdHealthPlan`, `IdCoverage`),
    INDEX `IX_PlanoCobertura_IdHealthPlan` (`IdHealthPlan`),
    INDEX `IX_PlanoCobertura_IdCoverage` (`IdCoverage`),
    INDEX `IX_PlanoCobertura_IsActive` (`IsActive`),
    CONSTRAINT `FK_PlanoCobertura_PlanoSaude` FOREIGN KEY (`IdHealthPlan`) REFERENCES `PlanoSaude` (`IdHealthPlan`) ON DELETE CASCADE,
    CONSTRAINT `FK_PlanoCobertura_Cobertura` FOREIGN KEY (`IdCoverage`) REFERENCES `Cobertura` (`IdCoverage`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci COMMENT='Relacionamento entre planos e coberturas';

-- ==================================================
-- TABELA: Beneficiários
-- ==================================================
CREATE TABLE IF NOT EXISTS `Beneficiario` (
    `IdBeneficiary` int NOT NULL AUTO_INCREMENT,
    `Nome` varchar(255) NOT NULL COMMENT 'Nome completo do beneficiário',
    `CPF` varchar(14) NOT NULL UNIQUE COMMENT 'CPF do beneficiário (formato: XXX.XXX.XXX-XX)',
    `Email` varchar(255) DEFAULT NULL COMMENT 'Email do beneficiário',
    `Telefone` varchar(20) DEFAULT NULL COMMENT 'Telefone de contato',
    `DataNascimento` date NOT NULL COMMENT 'Data de nascimento do beneficiário',
    `Sexo` varchar(1) DEFAULT NULL COMMENT 'Sexo do beneficiário (M/F)',
    `EstadoCivil` varchar(20) DEFAULT NULL COMMENT 'Estado civil',
    `Profissao` varchar(100) DEFAULT NULL COMMENT 'Profissão do beneficiário',
    `RendaFamiliar` decimal(10,2) DEFAULT NULL COMMENT 'Renda familiar mensal',
    `Endereco` varchar(500) DEFAULT NULL COMMENT 'Endereço completo',
    `Cidade` varchar(100) DEFAULT NULL COMMENT 'Cidade',
    `Estado` varchar(2) DEFAULT NULL COMMENT 'UF do estado',
    `CEP` varchar(10) DEFAULT NULL COMMENT 'CEP (formato: XXXXX-XXX)',
    `NomeMae` varchar(255) DEFAULT NULL COMMENT 'Nome da mãe',
    `CartaoSUS` varchar(18) DEFAULT NULL COMMENT 'Número do Cartão SUS',
    `IsActive` tinyint(1) NOT NULL DEFAULT '1' COMMENT 'Registro ativo',
    `DtCreated` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) COMMENT 'Data de criação',
    `DtUpdated` datetime(6) DEFAULT NULL COMMENT 'Data de atualização',
    `DtDeleted` datetime(6) DEFAULT NULL COMMENT 'Data de exclusão lógica',
    `CreatedBy` varchar(256) NOT NULL DEFAULT 'System' COMMENT 'Usuário que criou',
    `UpdatedBy` varchar(256) DEFAULT NULL COMMENT 'Usuário que atualizou',
    `DeletedBy` varchar(256) DEFAULT NULL COMMENT 'Usuário que excluiu',
    PRIMARY KEY (`IdBeneficiary`),
    INDEX `IX_Beneficiario_CPF` (`CPF`),
    INDEX `IX_Beneficiario_Nome` (`Nome`),
    INDEX `IX_Beneficiario_DataNascimento` (`DataNascimento`),
    INDEX `IX_Beneficiario_IsActive` (`IsActive`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci COMMENT='Beneficiários dos planos de saúde';

-- ==================================================
-- TABELA: Regras de Aceitação
-- ==================================================
CREATE TABLE IF NOT EXISTS `RegraAceitacao` (
    `IdRegraAceitacao` int NOT NULL AUTO_INCREMENT,
    `IdHealthPlan` int NOT NULL COMMENT 'ID do plano de saúde',
    `TipoRegra` varchar(100) NOT NULL COMMENT 'Tipo da regra (Idade, Renda, Profissão, etc.)',
    `Operador` varchar(20) NOT NULL COMMENT 'Operador da regra (=, >, <, >=, <=, BETWEEN, IN)',
    `ValorMinimo` varchar(255) DEFAULT NULL COMMENT 'Valor mínimo da regra',
    `ValorMaximo` varchar(255) DEFAULT NULL COMMENT 'Valor máximo da regra',
    `ListaValores` text DEFAULT NULL COMMENT 'Lista de valores aceitos (JSON)',
    `Descricao` varchar(500) NOT NULL COMMENT 'Descrição da regra',
    `MensagemRejeicao` varchar(500) DEFAULT NULL COMMENT 'Mensagem exibida quando a regra não é atendida',
    `IsObrigatoria` tinyint(1) NOT NULL DEFAULT '1' COMMENT 'Indica se a regra é obrigatória',
    `IsActive` tinyint(1) NOT NULL DEFAULT '1' COMMENT 'Registro ativo',
    `DtCreated` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) COMMENT 'Data de criação',
    `DtUpdated` datetime(6) DEFAULT NULL COMMENT 'Data de atualização',
    `DtDeleted` datetime(6) DEFAULT NULL COMMENT 'Data de exclusão lógica',
    `CreatedBy` varchar(256) NOT NULL DEFAULT 'System' COMMENT 'Usuário que criou',
    `UpdatedBy` varchar(256) DEFAULT NULL COMMENT 'Usuário que atualizou',
    `DeletedBy` varchar(256) DEFAULT NULL COMMENT 'Usuário que excluiu',
    PRIMARY KEY (`IdRegraAceitacao`),
    INDEX `IX_RegraAceitacao_IdHealthPlan` (`IdHealthPlan`),
    INDEX `IX_RegraAceitacao_TipoRegra` (`TipoRegra`),
    INDEX `IX_RegraAceitacao_IsActive` (`IsActive`),
    CONSTRAINT `FK_RegraAceitacao_PlanoSaude` FOREIGN KEY (`IdHealthPlan`) REFERENCES `PlanoSaude` (`IdHealthPlan`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci COMMENT='Regras de aceitação para os planos de saúde';

-- ==================================================
-- TABELA: Cotações
-- ==================================================
CREATE TABLE IF NOT EXISTS `Cotacao` (
    `IdQuote` int NOT NULL AUTO_INCREMENT,
    `IdCompany` int NOT NULL COMMENT 'ID da empresa que está cotando',
    `IdBeneficiary` int NOT NULL COMMENT 'ID do beneficiário solicitante',
    `IdHealthPlan` int NOT NULL COMMENT 'ID do plano cotado',
    `IdAgeRange` int NOT NULL COMMENT 'ID da faixa etária do beneficiário',
    `NumeroCotacao` varchar(50) NOT NULL UNIQUE COMMENT 'Número único da cotação',
    `DataCotacao` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) COMMENT 'Data da cotação',
    `DataVencimento` datetime(6) NOT NULL COMMENT 'Data de vencimento da cotação',
    `PremioCalculado` decimal(10,2) NOT NULL COMMENT 'Prêmio mensal calculado',
    `ValorTotal` decimal(10,2) NOT NULL COMMENT 'Valor total da cotação',
    `PercentualDesconto` decimal(5,2) DEFAULT '0.00' COMMENT 'Percentual de desconto aplicado',
    `ValorDesconto` decimal(10,2) DEFAULT '0.00' COMMENT 'Valor do desconto em reais',
    `Status` varchar(50) NOT NULL DEFAULT 'Pendente' COMMENT 'Status da cotação (Pendente, Aprovada, Rejeitada, Expirada, Contratada)',
    `MotivoRejeicao` text DEFAULT NULL COMMENT 'Motivo da rejeição (se aplicável)',
    `Observacoes` text DEFAULT NULL COMMENT 'Observações adicionais',
    `IdadeCalculada` int NOT NULL COMMENT 'Idade calculada do beneficiário na data da cotação',
    `ValidadeDias` int NOT NULL DEFAULT '30' COMMENT 'Validade da cotação em dias',
    `IsActive` tinyint(1) NOT NULL DEFAULT '1' COMMENT 'Registro ativo',
    `DtCreated` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) COMMENT 'Data de criação',
    `DtUpdated` datetime(6) DEFAULT NULL COMMENT 'Data de atualização',
    `DtDeleted` datetime(6) DEFAULT NULL COMMENT 'Data de exclusão lógica',
    `CreatedBy` varchar(256) NOT NULL DEFAULT 'System' COMMENT 'Usuário que criou',
    `UpdatedBy` varchar(256) DEFAULT NULL COMMENT 'Usuário que atualizou',
    `DeletedBy` varchar(256) DEFAULT NULL COMMENT 'Usuário que excluiu',
    PRIMARY KEY (`IdQuote`),
    INDEX `IX_Cotacao_IdCompany` (`IdCompany`),
    INDEX `IX_Cotacao_IdBeneficiary` (`IdBeneficiary`),
    INDEX `IX_Cotacao_IdHealthPlan` (`IdHealthPlan`),
    INDEX `IX_Cotacao_IdAgeRange` (`IdAgeRange`),
    INDEX `IX_Cotacao_NumeroCotacao` (`NumeroCotacao`),
    INDEX `IX_Cotacao_Status` (`Status`),
    INDEX `IX_Cotacao_DataCotacao` (`DataCotacao`),
    INDEX `IX_Cotacao_IsActive` (`IsActive`),
    CONSTRAINT `FK_Cotacao_Empresa` FOREIGN KEY (`IdCompany`) REFERENCES `Empresa` (`IdCompany`) ON DELETE RESTRICT,
    CONSTRAINT `FK_Cotacao_Beneficiario` FOREIGN KEY (`IdBeneficiary`) REFERENCES `Beneficiario` (`IdBeneficiary`) ON DELETE RESTRICT,
    CONSTRAINT `FK_Cotacao_PlanoSaude` FOREIGN KEY (`IdHealthPlan`) REFERENCES `PlanoSaude` (`IdHealthPlan`) ON DELETE RESTRICT,
    CONSTRAINT `FK_Cotacao_FaixaEtaria` FOREIGN KEY (`IdAgeRange`) REFERENCES `FaixaEtaria` (`IdAgeRange`) ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci COMMENT='Cotações de planos de saúde';

-- ==================================================
-- TABELA: Histórico de Cotações
-- ==================================================
CREATE TABLE IF NOT EXISTS `HistoricoCotacao` (
    `IdHistoricoCotacao` int NOT NULL AUTO_INCREMENT,
    `IdQuote` int NOT NULL COMMENT 'ID da cotação',
    `StatusAnterior` varchar(50) DEFAULT NULL COMMENT 'Status anterior da cotação',
    `StatusNovo` varchar(50) NOT NULL COMMENT 'Novo status da cotação',
    `Motivo` varchar(500) DEFAULT NULL COMMENT 'Motivo da mudança de status',
    `Observacoes` text DEFAULT NULL COMMENT 'Observações sobre a mudança',
    `DataMudanca` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) COMMENT 'Data da mudança de status',
    `UsuarioResponsavel` varchar(256) NOT NULL COMMENT 'Usuário responsável pela mudança',
    `IsActive` tinyint(1) NOT NULL DEFAULT '1' COMMENT 'Registro ativo',
    `DtCreated` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) COMMENT 'Data de criação',
    `DtUpdated` datetime(6) DEFAULT NULL COMMENT 'Data de atualização',
    `DtDeleted` datetime(6) DEFAULT NULL COMMENT 'Data de exclusão lógica',
    `CreatedBy` varchar(256) NOT NULL DEFAULT 'System' COMMENT 'Usuário que criou',
    `UpdatedBy` varchar(256) DEFAULT NULL COMMENT 'Usuário que atualizou',
    `DeletedBy` varchar(256) DEFAULT NULL COMMENT 'Usuário que excluiu',
    PRIMARY KEY (`IdHistoricoCotacao`),
    INDEX `IX_HistoricoCotacao_IdQuote` (`IdQuote`),
    INDEX `IX_HistoricoCotacao_StatusNovo` (`StatusNovo`),
    INDEX `IX_HistoricoCotacao_DataMudanca` (`DataMudanca`),
    INDEX `IX_HistoricoCotacao_IsActive` (`IsActive`),
    CONSTRAINT `FK_HistoricoCotacao_Cotacao` FOREIGN KEY (`IdQuote`) REFERENCES `Cotacao` (`IdQuote`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci COMMENT='Histórico de mudanças de status das cotações';

-- ==================================================
-- INSERÇÃO DE DADOS INICIAIS
-- ==================================================

-- Inserir faixas etárias padrão
INSERT INTO `FaixaEtaria` (`Descricao`, `IdadeMinima`, `IdadeMaxima`, `Multiplicador`, `CreatedBy`) VALUES
('0 a 18 anos', 0, 18, 0.8000, 'System'),
('19 a 23 anos', 19, 23, 1.0000, 'System'),
('24 a 28 anos', 24, 28, 1.2000, 'System'),
('29 a 33 anos', 29, 33, 1.4000, 'System'),
('34 a 38 anos', 34, 38, 1.6000, 'System'),
('39 a 43 anos', 39, 43, 1.8000, 'System'),
('44 a 48 anos', 44, 48, 2.0000, 'System'),
('49 a 53 anos', 49, 53, 2.2000, 'System'),
('54 a 58 anos', 54, 58, 2.6000, 'System'),
('59+ anos', 59, 120, 3.0000, 'System');

-- Inserir acomodações padrão
INSERT INTO `Acomodacao` (`Tipo`, `Descricao`, `ValorAdicional`, `CreatedBy`) VALUES
('Enfermaria', 'Quarto compartilhado com outros pacientes', 0.00, 'System'),
('Apartamento', 'Quarto individual com acompanhante', 150.00, 'System'),
('Apartamento Luxo', 'Quarto individual de luxo com comodidades especiais', 300.00, 'System'),
('UTI', 'Unidade de Terapia Intensiva', 0.00, 'System');

-- Inserir coberturas básicas
INSERT INTO `Cobertura` (`Nome`, `Descricao`, `Tipo`, `ValorBase`, `IsObrigatoria`, `CreatedBy`) VALUES
('Consultas Médicas', 'Consultas com clínicos gerais e especialistas', 'Ambulatorial', 50.00, 1, 'System'),
('Exames Simples', 'Exames laboratoriais e radiológicos básicos', 'Ambulatorial', 30.00, 1, 'System'),
('Internação Hospitalar', 'Internação em hospital geral', 'Hospitalar', 200.00, 1, 'System'),
('Cirurgias', 'Procedimentos cirúrgicos de pequeno e médio porte', 'Hospitalar', 500.00, 1, 'System'),
('Urgência e Emergência', 'Atendimento de urgência e emergência 24h', 'Emergencial', 100.00, 1, 'System'),
('Parto e Obstetrícia', 'Acompanhamento pré-natal, parto e pós-parto', 'Obstétrica', 300.00, 0, 'System'),
('Fisioterapia', 'Sessões de fisioterapia', 'Ambulatorial', 80.00, 0, 'System'),
('Psicologia', 'Consultas psicológicas', 'Ambulatorial', 120.00, 0, 'System'),
('Medicina Alternativa', 'Acupuntura, homeopatia e outras terapias', 'Ambulatorial', 150.00, 0, 'System'),
('Odontologia', 'Tratamentos odontológicos básicos', 'Odontológica', 100.00, 0, 'System');

-- ==================================================
-- EXEMPLOS DE COTAÇÃO (10 exemplos reais)
-- ==================================================

-- Empresa Seguradora de Exemplo
INSERT INTO `Empresa` (`Nome`, `NomeFantasia`, `CNPJ`, `Email`, `Telefone`, `Endereco`, `Cidade`, `Estado`, `CEP`, `CreatedBy`) VALUES
('Saúde & Vida Seguros Ltda', 'Saúde & Vida', '12.345.678/0001-90', 'contato@saudeevida.com.br', '(11) 3456-7890', 'Av. Paulista, 1000 - Sala 1501', 'São Paulo', 'SP', '01310-100', 'System'),
('MedLife Seguros S.A.', 'MedLife', '98.765.432/0001-10', 'vendas@medlife.com.br', '(21) 2345-6789', 'Rua do Ouvidor, 123 - 10º andar', 'Rio de Janeiro', 'RJ', '20040-020', 'System'),
('PlanoMax Saúde', 'PlanoMax', '11.222.333/0001-44', 'atendimento@planomax.com.br', '(31) 3333-4444', 'Av. Afonso Pena, 567', 'Belo Horizonte', 'MG', '30130-002', 'System');

-- Planos de Saúde de Exemplo
INSERT INTO `PlanoSaude` (`EmpresaId`, `AcomodacaoId`, `Nome`, `Descricao`, `Codigo`, `Categoria`, `TipoContratacao`, `AbrangenciaGeografica`, `SegmentacaoAssistencial`, `PremioBase`, `CoparticipacaoConsulta`, `CoparticipacaoExame`, `CarenciaConsulta`, `CarenciaExame`, `CarenciaInternacao`, `CreatedBy`) VALUES
(1, 1, 'Essencial Ambulatorial', 'Plano básico com cobertura ambulatorial completa', 'ESS-AMB-001', 'Individual', 'Individual', 'Estadual', 'Ambulatorial', 89.90, 25.00, 15.00, 30, 30, 0, 'System'),
(1, 2, 'Completo Hospitalar', 'Plano completo com internação em apartamento', 'CMP-HOSP-001', 'Individual', 'Individual', 'Nacional', 'Hospitalar', 189.90, 30.00, 20.00, 30, 60, 180, 'System'),
(2, 1, 'Familiar Básico', 'Plano familiar com cobertura nacional', 'FAM-BAS-001', 'Familiar', 'Coletivo por Adesão', 'Nacional', 'Ambulatorial + Hospitalar', 299.90, 35.00, 25.00, 60, 90, 180, 'System'),
(2, 2, 'Executivo Premium', 'Plano executivo com todas as coberturas', 'EXEC-PREM-001', 'Individual', 'Individual', 'Nacional', 'Ambulatorial + Hospitalar + Obstetrícia', 599.90, 50.00, 30.00, 0, 30, 180, 'System'),
(3, 1, 'Empresarial Standard', 'Plano empresarial padrão', 'EMP-STD-001', 'Empresarial', 'Empresarial', 'Regional', 'Ambulatorial + Hospitalar', 149.90, 20.00, 10.00, 30, 60, 180, 'System');

-- Beneficiários de Exemplo
INSERT INTO `Beneficiario` (`Nome`, `CPF`, `Email`, `Telefone`, `DataNascimento`, `Sexo`, `EstadoCivil`, `Profissao`, `RendaFamiliar`, `Endereco`, `Cidade`, `Estado`, `CEP`, `NomeMae`, `CreatedBy`) VALUES
('Maria Silva Santos', '123.456.789-01', 'maria.silva@email.com', '(11) 98765-4321', '1985-03-15', 'F', 'Casada', 'Enfermeira', 4500.00, 'Rua das Flores, 123', 'São Paulo', 'SP', '01234-567', 'Ana Silva', 'System'),
('João Carlos Oliveira', '987.654.321-09', 'joao.oliveira@email.com', '(21) 99887-6543', '1990-07-22', 'M', 'Solteiro', 'Engenheiro', 7800.00, 'Av. Copacabana, 456', 'Rio de Janeiro', 'RJ', '22050-001', 'Carmen Oliveira', 'System'),
('Ana Paula Costa', '456.789.123-45', 'ana.costa@email.com', '(31) 98765-1234', '1978-12-10', 'F', 'Divorciada', 'Advogada', 9200.00, 'Rua da Liberdade, 789', 'Belo Horizonte', 'MG', '30140-000', 'Isabel Costa', 'System'),
('Pedro Henrique Lima', '789.123.456-78', 'pedro.lima@email.com', '(11) 97654-3210', '1995-05-08', 'M', 'Solteiro', 'Programador', 5500.00, 'Alameda Santos, 321', 'São Paulo', 'SP', '01419-001', 'Lucia Lima', 'System'),
('Carla Fernanda Souza', '321.654.987-01', 'carla.souza@email.com', '(47) 99123-4567', '1988-09-25', 'F', 'Casada', 'Professora', 3800.00, 'Rua XV de Novembro, 654', 'Joinville', 'SC', '89201-100', 'Regina Souza', 'System'),
('Roberto Silva Nascimento', '654.321.789-12', 'roberto.nascimento@email.com', '(85) 98888-7777', '1972-11-30', 'M', 'Casado', 'Médico', 15000.00, 'Av. Beira Mar, 987', 'Fortaleza', 'CE', '60165-121', 'Maria Nascimento', 'System'),
('Juliana Santos Pereira', '159.753.486-20', 'juliana.pereira@email.com', '(61) 99999-8888', '1992-02-18', 'F', 'Solteira', 'Dentista', 6200.00, 'SHIS QI 15 Conjunto 3', 'Brasília', 'DF', '71635-230', 'Helena Pereira', 'System'),
('Marcos Antonio Silva', '852.741.963-30', 'marcos.silva@email.com', '(81) 97777-6666', '1980-08-14', 'M', 'Divorciado', 'Contador', 4200.00, 'Rua do Sol, 147', 'Recife', 'PE', '52061-070', 'Antonia Silva', 'System'),
('Fernanda Costa Lima', '963.852.741-40', 'fernanda.lima@email.com', '(71) 96666-5555', '1986-04-03', 'F', 'Casada', 'Fisioterapeuta', 3900.00, 'Av. Oceânica, 258', 'Salvador', 'BA', '40140-110', 'Rosa Lima', 'System'),
('Carlos Eduardo Santos', '741.852.963-50', 'carlos.santos@email.com', '(51) 95555-4444', '1983-10-27', 'M', 'Casado', 'Arquiteto', 8500.00, 'Rua da Praia, 369', 'Porto Alegre', 'RS', '90010-150', 'Elvira Santos', 'System');

-- Cotações de Exemplo (10 cotações reais)
INSERT INTO `Cotacao` (`EmpresaId`, `BeneficiarioId`, `PlanoSaudeId`, `FaixaEtariaId`, `NumeroCotacao`, `DataCotacao`, `DataVencimento`, `PremioCalculado`, `ValorTotal`, `PercentualDesconto`, `ValorDesconto`, `Status`, `Observacoes`, `IdadeCalculada`, `ValidadeDias`, `CreatedBy`) VALUES
(1, 1, 1, 5, 'COT-2025-000001', '2025-01-17 10:30:00', '2025-02-16 23:59:59', 143.84, 143.84, 0.00, 0.00, 'Aprovada', 'Cotação aprovada automaticamente', 39, 30, 'System'),
(2, 2, 3, 4, 'COT-2025-000002', '2025-01-17 11:15:00', '2025-02-16 23:59:59', 419.86, 419.86, 0.00, 0.00, 'Pendente', 'Aguardando análise de subscrição', 34, 30, 'System'),
(1, 3, 2, 6, 'COT-2025-000003', '2025-01-17 14:20:00', '2025-02-16 23:59:59', 341.82, 324.73, 5.00, 17.09, 'Aprovada', 'Desconto especial por indicação', 46, 30, 'System'),
(2, 4, 4, 3, 'COT-2025-000004', '2025-01-17 09:45:00', '2025-02-16 23:59:59', 719.88, 719.88, 0.00, 0.00, 'Rejeitada', 'Beneficiário não atende critério de renda mínima', 29, 30, 'System'),
(3, 5, 5, 5, 'COT-2025-000005', '2025-01-17 16:30:00', '2025-02-16 23:59:59', 239.84, 215.86, 10.00, 23.98, 'Aprovada', 'Desconto corporativo aplicado', 36, 30, 'System'),
(1, 6, 2, 9, 'COT-2025-000006', '2025-01-17 13:10:00', '2025-02-16 23:59:59', 493.74, 493.74, 0.00, 0.00, 'Contratada', 'Plano contratado com sucesso', 52, 30, 'System'),
(2, 7, 3, 3, 'COT-2025-000007', '2025-01-17 15:50:00', '2025-02-16 23:59:59', 419.86, 419.86, 0.00, 0.00, 'Aprovada', 'Análise finalizada - aprovada', 32, 30, 'System'),
(3, 8, 5, 6, 'COT-2025-000008', '2025-01-17 12:25:00', '2025-02-16 23:59:59', 269.82, 269.82, 0.00, 0.00, 'Expirada', 'Cotação expirou sem contratação', 44, 30, 'System'),
(1, 9, 1, 5, 'COT-2025-000009', '2025-01-17 11:40:00', '2025-02-16 23:59:59', 143.84, 136.65, 5.00, 7.19, 'Pendente', 'Aguardando confirmação do beneficiário', 38, 30, 'System'),
(2, 10, 4, 6, 'COT-2025-000010', '2025-01-17 08:20:00', '2025-02-16 23:59:59', 1079.82, 1079.82, 0.00, 0.00, 'Aprovada', 'Plano premium aprovado para contratação', 41, 30, 'System');

SET FOREIGN_KEY_CHECKS = 1;
COMMIT;

-- ==================================================
-- FIM DA MODELAGEM
-- ==================================================