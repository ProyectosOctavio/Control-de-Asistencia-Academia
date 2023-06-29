CREATE DATABASE ControlAsistencia;

USE ControlAsistencia;
GO;

CREATE TABLE DatosAsistencia (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(50) NOT NULL,
    Apellido VARCHAR(50) NOT NULL,
    Asistencia VARCHAR(20) NOT NULL,
    Fecha DATETIME NOT NULL,
    Cinta VARCHAR(20) NOT NULL,
    Turno VARCHAR(20) NOT NULL,
    FechaNacimiento DATE NOT NULL,
    Edad INT
);

INSERT INTO DatosAsistencia (Nombre, Apellido, Asistencia, Fecha, Cinta, Turno, FechaNacimiento)
VALUES 
('Juan', 'García', 'Presente', '2022-01-01', 'Blanca', 'Primer', '1997-05-10'),
('María', 'Rodríguez', 'Ausente', '2022-01-01', 'Verde', 'Segundo', '1992-02-15'),
('Pedro', 'López', 'Presente', '2022-01-02', 'Azul', 'Primer', '1981-12-31'),
('Ana', 'Sánchez', 'Presente', '2022-01-02', 'Negra', 'Segundo', '1986-07-25'),
('Luis', 'Martínez', 'Ausente', '2022-01-03', 'Naranja', 'Primer', '2002-11-02'),
('Elena', 'Fernández', 'Presente', '2022-01-03', 'Roja', 'Segundo', '1994-08-12');

CREATE PROCEDURE Rango_Fecha
(
@Fecha1 date,
@Fecha2 date
)
AS
BEGIN

Select *From DatosAsistencia where fecha between @Fecha1 and @Fecha2
END;
