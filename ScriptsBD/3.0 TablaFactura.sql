USE GetechnologiesTestDb;
GO

IF OBJECT_ID('dbo.Factura', 'U') IS NOT NULL
    DROP TABLE dbo.Factura;
GO

CREATE TABLE dbo.Factura
(
    FacturaId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,

    PersonaId INT NOT NULL,
    Monto DECIMAL(18,2) NOT NULL,
    Fecha DATETIME2 NOT NULL DEFAULT SYSDATETIME(),

    CONSTRAINT FK_Factura_Persona
        FOREIGN KEY (PersonaId)
        REFERENCES dbo.Persona(PersonaId)
        ON DELETE CASCADE
);
GO
