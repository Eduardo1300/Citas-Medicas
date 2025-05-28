-- Crear base de datos
CREATE DATABASE BDClinica;
GO

USE BDClinica;
GO

-- ========================
-- TABLAS
-- ========================

CREATE TABLE paciente (
    pacienteid INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(50) NOT NULL,
    apellido VARCHAR(50) NOT NULL,
    edad INT NOT NULL,
    telefono CHAR(9),
    estado BIT,
    dni VARCHAR(15) NOT NULL UNIQUE
);

CREATE TABLE medico (
    medicoid INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(50) NOT NULL,
    apellido VARCHAR(50) NOT NULL,
    telefono CHAR(9),
    especialidad VARCHAR(100) NOT NULL
);

CREATE TABLE citamedica (
    citaid INT IDENTITY(1,1) PRIMARY KEY,
    fecha DATE NOT NULL,
    pacienteid INT NOT NULL,
    medicoid INT NOT NULL,
    CONSTRAINT fk_paciente FOREIGN KEY (pacienteid) REFERENCES paciente(pacienteid),
    CONSTRAINT fk_medico FOREIGN KEY (medicoid) REFERENCES medico(medicoid),
    CONSTRAINT uk_medico_fecha UNIQUE (medicoid, fecha)
);

CREATE TABLE usuario (
    idusuario INT IDENTITY(1,1) PRIMARY KEY,
    nomusuario VARCHAR(100) NOT NULL,
    email VARCHAR(200) NOT NULL,
    password VARCHAR(100) NOT NULL,
    nombres VARCHAR(100) NOT NULL,
    apellidos VARCHAR(100) NOT NULL,
    activo BIT NOT NULL
);

-- ========================
-- DATOS DE PRUEBA
-- ========================

INSERT INTO paciente (nombre, apellido, edad, telefono, estado, dni) VALUES
('Juan', 'Pérez', 30, '987654321', 1, '12345678'),
('María', 'Lopez', 25, '923456789', 1, '23456789'),
('Carlos', 'Gonzales', 40, '912345678', 1, '34567890'),
('Ana', 'Ramirez', 35, '976543210', 0, '45678901'),
('Luis', 'Torres', 50, '988888888', 1, '56789012');

INSERT INTO medico (nombre, apellido, telefono, especialidad) VALUES
('José', 'Sánchez', '912345678', 'Pediatría'),
('Laura', 'Morales', '923456789', 'Dermatología'),
('Pedro', 'Gómez', '934567890', 'Cardiología'),
('Carmen', 'Vargas', '945678901', 'Ginecología'),
('Andrés', 'Salas', '956789012', 'Neurología');

INSERT INTO usuario (nomusuario, email, password, nombres, apellidos, activo) VALUES
('admin', 'admin@clinica.com', '123', 'Administrador', 'General', 1);

INSERT INTO citamedica (fecha, pacienteid, medicoid) VALUES 
('2025-04-25', 1, 2);

-- ========================
-- PROCEDIMIENTOS ALMACENADOS
-- ========================

-- USUARIOS
GO
CREATE OR ALTER PROCEDURE registrarusuario
    @Nomusuario NVARCHAR(100),
    @Email NVARCHAR(100),
    @Password NVARCHAR(100),
    @Nombres NVARCHAR(100),
    @Apellidos NVARCHAR(100),
    @Activo BIT
AS
BEGIN
    INSERT INTO Usuario (Nomusuario, Email, Password, Nombres, Apellidos, Activo)
    VALUES (@Nomusuario, @Email, @Password, @Nombres, @Apellidos, @Activo);
END;
GO

CREATE OR ALTER PROCEDURE validarusuario
    @Nomusuario NVARCHAR(100),
    @Password NVARCHAR(100)
AS
BEGIN
    SELECT Idusuario, Nomusuario, Email, Nombres, Apellidos, Activo
    FROM Usuario
    WHERE Nomusuario = @Nomusuario AND Password = @Password;
END;
GO

CREATE OR ALTER PROCEDURE listadousuarios
AS
BEGIN
    SELECT * FROM Usuario;
END;
GO

-- PACIENTES
CREATE OR ALTER PROCEDURE registrarpaciente
    @nombre VARCHAR(50),
    @apellido VARCHAR(50),
    @edad INT,
    @telefono CHAR(9),
    @estado BIT,
    @dni VARCHAR(15)
AS
BEGIN
    INSERT INTO paciente (nombre, apellido, edad, telefono, estado, dni)
    VALUES (@nombre, @apellido, @edad, @telefono, @estado, @dni);
END;
GO

CREATE OR ALTER PROCEDURE actualizarpaciente
    @pacienteid INT,
    @nombre VARCHAR(50),
    @apellido VARCHAR(50),
    @edad INT,
    @telefono CHAR(9),
    @estado BIT,
    @dni VARCHAR(15)
AS
BEGIN
    UPDATE paciente
    SET nombre = @nombre,
        apellido = @apellido,
        edad = @edad,
        telefono = @telefono,
        estado = @estado,
        dni = @dni
    WHERE pacienteid = @pacienteid;
END;
GO

CREATE OR ALTER PROCEDURE listarpacientes
AS
BEGIN
    SELECT * FROM paciente;
END;
GO

-- MÉDICOS
CREATE OR ALTER PROCEDURE registrarmedico
    @nombre VARCHAR(50),
    @apellido VARCHAR(50),
    @telefono CHAR(9),
    @especialidad VARCHAR(100)
AS
BEGIN
    INSERT INTO medico (nombre, apellido, telefono, especialidad)
    VALUES (@nombre, @apellido, @telefono, @especialidad);
END;
GO

CREATE OR ALTER PROCEDURE actualizarmedico
    @medicoid INT,
    @nombre VARCHAR(50),
    @apellido VARCHAR(50),
    @telefono CHAR(9),
    @especialidad VARCHAR(100)
AS
BEGIN
    UPDATE medico
    SET nombre = @nombre,
        apellido = @apellido,
        telefono = @telefono,
        especialidad = @especialidad
    WHERE medicoid = @medicoid;
END;
GO

CREATE OR ALTER PROCEDURE listarmedicos
AS
BEGIN
    SELECT * FROM medico;
END;
GO

-- CITAS
CREATE OR ALTER PROCEDURE registrarcitas
    @fecha DATE,
    @pacienteid INT,
    @medicoid INT
AS
BEGIN
    INSERT INTO citamedica (fecha, pacienteid, medicoid)
    VALUES (@fecha, @pacienteid, @medicoid);
END;
GO

CREATE OR ALTER PROCEDURE actualizarcita
    @citaid INT,
    @fecha DATE,
    @pacienteid INT,
    @medicoid INT
AS
BEGIN
    UPDATE citamedica
    SET fecha = @fecha,
        pacienteid = @pacienteid,
        medicoid = @medicoid
    WHERE citaid = @citaid;
END;
GO

CREATE OR ALTER PROCEDURE listarcitas
AS
BEGIN
    SELECT 
        c.citaid,
        c.fecha,
        CONCAT(p.nombre, ' ', p.apellido) AS paciente,
        CONCAT(m.nombre, ' ', m.apellido) AS medico,
        m.especialidad
    FROM citamedica c
    INNER JOIN paciente p ON c.pacienteid = p.pacienteid
    INNER JOIN medico m ON c.medicoid = m.medicoid;
END;
GO
