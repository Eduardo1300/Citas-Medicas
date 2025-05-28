-- Crear la base de datos
CREATE DATABASE BDClinica;
GO

-- Usar la base de datos
USE BDClinica;
GO

-- Crear tabla de pacientes
CREATE TABLE paciente (
    pacienteid INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(50) NOT NULL,
    apellido VARCHAR(50) NOT NULL,
    edad INT NOT NULL,
    telefono CHAR(9),
    estado BIT NOT NULL, -- 1 = activo, 0 = inactivo
    dni VARCHAR(15) NOT NULL UNIQUE
);
GO

-- Crear tabla de médicos
CREATE TABLE medico (
    medicoid INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(50) NOT NULL,
    apellido VARCHAR(50) NOT NULL,
    telefono CHAR(9),
    especialidad VARCHAR(100) NOT NULL
);
GO

-- Crear tabla de citas médicas
CREATE TABLE citamedica (
    citaid INT IDENTITY(1,1) PRIMARY KEY,
    fecha DATE NOT NULL,
    pacienteid INT NOT NULL,
    medicoid INT NOT NULL,
    CONSTRAINT fk_paciente FOREIGN KEY (pacienteid) REFERENCES paciente(pacienteid),
    CONSTRAINT fk_medico FOREIGN KEY (medicoid) REFERENCES medico(medicoid),
    CONSTRAINT uk_medico_fecha UNIQUE (medicoid, fecha) -- evita duplicar citas el mismo día para el mismo médico
);
GO

-- Crear tabla de usuarios
CREATE TABLE usuario (
    pacienteid INT PRIMARY KEY,
    usuario1 NVARCHAR(50) NOT NULL UNIQUE,
    contraseña NVARCHAR(255) NOT NULL,
    CONSTRAINT fk_usuario_paciente FOREIGN KEY (pacienteid) REFERENCES paciente(pacienteid)
);
GO

-- Insertar pacientes de prueba
INSERT INTO paciente (nombre, apellido, edad, telefono, estado, dni) VALUES
('Juan', 'Pérez', 30, '987654321', 1, '12345678'),
('María', 'Lopez', 25, '923456789', 1, '23456789'),
('Carlos', 'Gonzales', 40, '912345678', 1, '34567890'),
('Ana', 'Ramirez', 35, '976543210', 0, '45678901'),
('Luis', 'Torres', 50, '988888888', 1, '56789012');
GO

-- Insertar médicos de prueba
INSERT INTO medico (nombre, apellido, telefono, especialidad) VALUES
('José', 'Sánchez', '912345678', 'Pediatría'),
('Laura', 'Morales', '923456789', 'Dermatología'),
('Pedro', 'Gómez', '934567890', 'Cardiología'),
('Carmen', 'Vargas', '945678901', 'Ginecología'),
('Andrés', 'Salas', '956789012', 'Neurología');
GO

-- Insertar usuarios de prueba (relacionados a pacientes)
INSERT INTO usuario (pacienteid, usuario1, contraseña) VALUES
(1, 'juan', '123'),
(2, 'maria', '123'),
(3, 'carlos', '123');
GO

-- Insertar una cita médica de prueba
INSERT INTO citamedica (fecha, pacienteid, medicoid)
VALUES ('2025-05-30', 1, 2);
GO
