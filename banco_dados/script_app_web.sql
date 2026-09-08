CREATE DATABASE IF NOT EXISTS pds_app_web;
USE pds_app_web;

CREATE TABLE IF NOT EXISTS processos (
    id_pro INT NOT NULL AUTO_INCREMENT,
    numero_pro VARCHAR(200) NOT NULL,
    data_pro DATE NULL,
    interessado_pro VARCHAR(200) NOT NULL,
    assunto_pro VARCHAR(200) NOT NULL,
    descricao_pro TEXT NULL,
    situacao_pro VARCHAR(50) NOT NULL,
    PRIMARY KEY (id_pro)
);

-- Registro de exemplo para teste
INSERT INTO processos (numero_pro, data_pro, interessado_pro, assunto_pro, descricao_pro, situacao_pro)
VALUES ('PROC-2026-001', '2026-01-10', 'Joao da Silva', 'Solicitacao de licenca',
 'Descricao do processo de exemplo.', 'Aberto');
