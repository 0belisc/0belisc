CREATE DATABASE IzumuDB;
GO
USE IzumuDB;
GO

CREATE TABLE TiposDocumento (
    Id INT PRIMARY KEY IDENTITY,
    Descripcion NVARCHAR(50) NOT NULL
);

CREATE TABLE Planes (
    Id INT PRIMARY KEY IDENTITY,
    Nombre NVARCHAR(100) NOT NULL
);

CREATE TABLE Clientes (
    Id INT PRIMARY KEY IDENTITY,
    TipoDocumentoId INT FOREIGN KEY REFERENCES TiposDocumento(Id),
    NumeroDocumento NVARCHAR(20) NOT NULL,
    FechaNacimiento DATE NOT NULL,
    PrimerNombre NVARCHAR(50) NOT NULL,
    SegundoNombre NVARCHAR(50),
    PrimerApellido NVARCHAR(50) NOT NULL,
    SegundoApellido NVARCHAR(50),
    Direccion NVARCHAR(200),
    Celular NVARCHAR(15),
    Email NVARCHAR(100) NOT NULL,
    PlanPreferenciaId INT FOREIGN KEY REFERENCES Planes(Id),
    CONSTRAINT UC_Cliente UNIQUE (TipoDocumentoId, NumeroDocumento)
);

-- Data inicial para listas
INSERT INTO TiposDocumento (Descripcion) VALUES ('Cédula de Ciudadanía'), ('Pasaporte'), ('Cédula de Extranjería');
INSERT INTO Planes (Nombre) VALUES ('Plan Básico'), ('Plan Premium'), ('Plan Excelencia');