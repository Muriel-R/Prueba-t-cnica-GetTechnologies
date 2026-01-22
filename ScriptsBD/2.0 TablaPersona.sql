USE GetechnologiesTestDb;
GO

IF OBJECT_ID('dbo.Persona', 'U') IS NOT NULL
    DROP TABLE dbo.Persona;
GO

CREATE TABLE dbo.Persona
(
    PersonaId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,

    Nombre NVARCHAR(200) NOT NULL,
    ApellidoPaterno NVARCHAR(200) NOT NULL,
    ApellidoMaterno NVARCHAR(200) NULL,
    Identificacion NVARCHAR(50) NOT NULL,

    FechaCreacion DATETIME2 NOT NULL DEFAULT SYSDATETIME()
);
GO
