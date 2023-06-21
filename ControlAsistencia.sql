CREATE DATABASE ControlAsistencia

USE ControlAsistencia
GO


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
('Juan', 'García', 'Presente', '2022-01-01', 'Blanca', 'Primer turno', '1997-05-10'),
('María', 'Rodríguez', 'Ausente', '2022-01-01', 'Verd