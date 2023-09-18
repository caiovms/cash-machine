CREATE DATABASE IF NOT EXISTS db_cash_machine;

USE db_cash_machine

CREATE TABLE IF NOT EXISTS Account (
    Id INTEGER AUTO_INCREMENT PRIMARY KEY,
    Agency VARCHAR(3) NOT NULL,
    Type VARCHAR(2) NOT NULL,
    Number VARCHAR(8) NOT NULL,
    Digit CHAR(1) NOT NULL,
    Balance DECIMAL(15,2) NOT NULL,
    CreatedOn DATETIME  NOT NULL
) ENGINE=INNODB;

CREATE TABLE IF NOT EXISTS Operation (
    Id INTEGER AUTO_INCREMENT PRIMARY KEY,
    Description VARCHAR(50) NOT NULL
) ENGINE=INNODB;

CREATE TABLE IF NOT EXISTS Movement (
    Id INTEGER AUTO_INCREMENT PRIMARY KEY,
    AccountId INTEGER NOT NULL, 
    OperationId INTEGER NOT NULL, 
    Amount  DECIMAL(15,2) NOT NULL,
    Date DATETIME  NOT NULL,
    BarCode VARCHAR(48) NULL,

    INDEX Account_Id (AccountId),
    INDEX Operation_Id (OperationId),


    FOREIGN KEY(AccountId) REFERENCES Account(Id),
    FOREIGN KEY(OperationId) REFERENCES Operation(Id)
    
) ENGINE=INNODB;


INSERT INTO Operation (Description) VALUES ('WITHDRAW');
INSERT INTO Operation (Description) VALUES ('DEPOSIT');
INSERT INTO Operation (Description) VALUES ('PAYMENT');
INSERT INTO Operation (Description) VALUES ('MONETIZE');

INSERT INTO Account (Agency, Type, Number, Digit, Balance, CreatedOn) VALUES ('037', '01', '01783451', '9', 5000, NOW());
