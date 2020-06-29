CREATE DATABASE IF NOT EXISTS db_warren;

USE db_warren

CREATE TABLE IF NOT EXISTS Conta (
    id INTEGER AUTO_INCREMENT PRIMARY KEY,
    agencia VARCHAR(3) NOT NULL,
    tipo VARCHAR(2) NOT NULL,
    numero VARCHAR(8) NOT NULL,
    digito CHAR(1) NOT NULL,
    saldo  DECIMAL(15,2) NOT NULL,
    dataCadastro DATETIME  NOT NULL
) ENGINE=INNODB;

CREATE TABLE IF NOT EXISTS Operacao (
    id INTEGER AUTO_INCREMENT PRIMARY KEY,
    descricao VARCHAR(50) NOT NULL
) ENGINE=INNODB;

CREATE TABLE IF NOT EXISTS Movimento (
    id INTEGER AUTO_INCREMENT PRIMARY KEY,
    idConta INTEGER NOT NULL, 
    idOperacao INTEGER NOT NULL, 
    valor  DECIMAL(15,2) NOT NULL,
    data DATETIME  NOT NULL,
    codigoBarras VARCHAR(48) NULL,

    INDEX conta_id (idConta),
    INDEX operacao_id (idOperacao),


    FOREIGN KEY(idConta) REFERENCES Conta(id),
    FOREIGN KEY(idOperacao) REFERENCES Operacao(id)
    
) ENGINE=INNODB;


INSERT INTO Operacao (descricao) VALUES ('SAQUE');
INSERT INTO Operacao (descricao) VALUES ('DEPOSITO');
INSERT INTO Operacao (descricao) VALUES ('PAGAMENTO');
INSERT INTO Operacao (descricao) VALUES ('RENTABILIZIACAO');

INSERT INTO Conta (agencia, tipo, numero, digito, saldo, dataCadastro) VALUES ('037', '01', '01783451', '9', 5000, NOW());
