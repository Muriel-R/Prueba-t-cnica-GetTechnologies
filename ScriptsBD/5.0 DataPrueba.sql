USE GetechnologiesTestDb;
GO

INSERT INTO dbo.Persona
(Nombre, ApellidoPaterno, ApellidoMaterno, Identificacion)
VALUES
('Juan', 'Pérez', 'López', 'ABC123'),
('María', 'Gómez', NULL, 'XYZ789');

INSERT INTO dbo.Factura
(PersonaId, Monto)
VALUES
(1, 1500.00),
(1, 2500.50),
(2, 800.00);
GO
