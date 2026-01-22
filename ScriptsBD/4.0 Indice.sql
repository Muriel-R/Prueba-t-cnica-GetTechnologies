USE GetechnologiesTestDb;
GO

IF NOT EXISTS (
    SELECT 1 
    FROM sys.indexes 
    WHERE name = 'UX_Persona_Identificacion'
)
BEGIN
    CREATE UNIQUE INDEX UX_Persona_Identificacion
    ON dbo.Persona (Identificacion);
END
GO
