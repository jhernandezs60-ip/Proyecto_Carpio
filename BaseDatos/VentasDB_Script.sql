USE VentasDB;
GO

INSERT INTO Usuarios (NombreUsuario, Password, Rol)
VALUES ('admin', '123', 'Administrador');
GO

INSERT INTO Vendedores (Nombre)
VALUES 
('Steven'),
('Carlos'),
('Luis');
GO

INSERT INTO Ventas 
(IdVendedor, NombreVendedor, MontoVenta, PorcentajeComision, Comision, Fecha)
VALUES
(1, 'Steven', 4500.00, 0.07, 315.00, GETDATE()),
(1, 'Steven', 6000.00, 0.10, 600.00, GETDATE()),
(2, 'Carlos', 3000.00, 0.07, 210.00, GETDATE()),
(3, 'Luis', 1200.00, 0.05, 60.00, GETDATE());
GO