CREATE DATABASE db_proyecto
GO
USE db_proyecto
GO


-- ===========================================
-- Tabla: Usuario
-- ===========================================
CREATE TABLE Usuario (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Email NVARCHAR(150) NOT NULL UNIQUE,
    Contrasena NVARCHAR(255) NOT NULL,
    RutaAvatar NVARCHAR(MAX) NULL,
    Activo BIT NOT NULL DEFAULT 1
);

CREATE TABLE rol (
    id INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(100) UNIQUE NOT NULL
);

CREATE TABLE rol_usuario (
    fkemail NVARCHAR(150) NOT NULL REFERENCES usuario (Email) ON UPDATE CASCADE ON DELETE CASCADE,
    fkidrol INT NOT NULL REFERENCES rol (id),
    PRIMARY KEY (fkemail, fkidrol)
);

CREATE TABLE rutarol (
    ruta VARCHAR(4000) NOT NULL,
    rol VARCHAR(100) NOT NULL REFERENCES rol (nombre),
    PRIMARY KEY (ruta, rol)
);

-- ===========================================
-- Tabla: TipoResponsable
-- ===========================================
CREATE TABLE TipoResponsable (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Titulo NVARCHAR(50) NOT NULL,
    Descripcion NVARCHAR(255) NOT NULL,
    CONSTRAINT UQ_TipoResponsable_Titulo UNIQUE (Titulo)
);

-- ===========================================
-- Tabla: Responsable
-- ===========================================
CREATE TABLE Responsable (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    IdTipoResponsable INT NOT NULL,
    IdUsuario INT NOT NULL,
    Nombre NVARCHAR(255) NOT NULL,
    CONSTRAINT FK_Responsable_TipoResponsable FOREIGN KEY (IdTipoResponsable) REFERENCES TipoResponsable(Id),
    CONSTRAINT FK_Responsable_Usuario FOREIGN KEY (IdUsuario) REFERENCES Usuario(Id) ON DELETE CASCADE
);

-- ===========================================
-- Tabla: TipoProyecto
-- ===========================================
CREATE TABLE TipoProyecto (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(150) NOT NULL,
    Descripcion NVARCHAR(255) NOT NULL,
    CONSTRAINT UQ_TipoProyecto_Nombre UNIQUE (Nombre)
);

-- ===========================================
-- Tabla: Estado
-- ===========================================
CREATE TABLE Estado (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(50) NOT NULL,
    Descripcion NVARCHAR(255) NOT NULL,
    CONSTRAINT UQ_Estado_Nombre UNIQUE (Nombre)
);

-- ===========================================
-- Tabla: Proyecto
-- ===========================================
CREATE TABLE Proyecto (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    IdProyectoPadre INT NULL,
    IdResponsable INT NOT NULL,
    IdTipoProyecto INT NOT NULL,
    Codigo NVARCHAR(50) NULL,
    Titulo NVARCHAR(255) NOT NULL,
    Descripcion NVARCHAR(MAX) NULL,
    FechaInicio DATE NULL,
    FechaFinPrevista DATE NULL,
    FechaModificacion DATE NULL,
    FechaFinalizacion DATE NULL,
    RutaLogo NVARCHAR(MAX) NULL,
    CONSTRAINT FK_Proyecto_ProyectoPadre FOREIGN KEY (IdProyectoPadre) REFERENCES Proyecto(Id) ON DELETE NO ACTION,
    CONSTRAINT FK_Proyecto_Responsable FOREIGN KEY (IdResponsable) REFERENCES Responsable(Id),
    CONSTRAINT FK_Proyecto_TipoProyecto FOREIGN KEY (IdTipoProyecto) REFERENCES TipoProyecto(Id)
);

-- ===========================================
-- Tabla: Estado_Proyecto
-- ===========================================
CREATE TABLE Estado_Proyecto (
    IdProyecto INT PRIMARY KEY,
    IdEstado INT NOT NULL,
    CONSTRAINT FK_EstadoProyecto_Proyecto FOREIGN KEY (IdProyecto) REFERENCES Proyecto(Id) ON DELETE CASCADE,
    CONSTRAINT FK_EstadoProyecto_Estado FOREIGN KEY (IdEstado) REFERENCES Estado(Id)
);

-- ===========================================
-- Tabla: TipoProducto
-- ===========================================
CREATE TABLE TipoProducto (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(150) NOT NULL,
    Descripcion NVARCHAR(255) NOT NULL,
    CONSTRAINT UQ_TipoProducto_Nombre UNIQUE (Nombre)
);

-- ===========================================
-- Tabla: Producto
-- ===========================================
CREATE TABLE Producto (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    IdTipoProducto INT NOT NULL,
    Codigo NVARCHAR(50) NULL,
    Titulo NVARCHAR(255) NOT NULL,
    Descripcion NVARCHAR(MAX) NULL,
    FechaInicio DATE NULL,
    FechaFinPrevista DATE NULL,
    FechaModificacion DATE NULL,
    FechaFinalizacion DATE NULL,
    RutaLogo NVARCHAR(MAX) NULL,
    CONSTRAINT FK_Producto_TipoProducto FOREIGN KEY (IdTipoProducto) REFERENCES TipoProducto(Id)
);

-- ===========================================
-- Tabla: Proyecto_Producto (relación N:M)
-- ===========================================
CREATE TABLE Proyecto_Producto (
    IdProyecto INT NOT NULL,
    IdProducto INT NOT NULL,
    FechaAsociacion DATE NULL,
    PRIMARY KEY (IdProyecto, IdProducto),
    CONSTRAINT FK_ProyectoProducto_Proyecto FOREIGN KEY (IdProyecto) REFERENCES Proyecto(Id) ON DELETE CASCADE,
    CONSTRAINT FK_ProyectoProducto_Producto FOREIGN KEY (IdProducto) REFERENCES Producto(Id) ON DELETE CASCADE
);

-- ===========================================
-- Tabla: Entregable
-- ===========================================
CREATE TABLE Entregable (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Codigo NVARCHAR(50) NULL,
    Titulo NVARCHAR(255) NOT NULL,
    Descripcion NVARCHAR(MAX) NULL,
    FechaInicio DATE NULL,
    FechaFinPrevista DATE NULL,
    FechaModificacion DATE NULL,
    FechaFinalizacion DATE NULL
);

-- ===========================================
-- Tabla: Producto_Entregable (relación N:M)
-- ===========================================
CREATE TABLE Producto_Entregable (
    IdProducto INT NOT NULL,
    IdEntregable INT NOT NULL,
    FechaAsociacion DATE NULL,
    PRIMARY KEY (IdProducto, IdEntregable),
    CONSTRAINT FK_ProductoEntregable_Producto FOREIGN KEY (IdProducto) REFERENCES Producto(Id) ON DELETE CASCADE,
    CONSTRAINT FK_ProductoEntregable_Entregable FOREIGN KEY (IdEntregable) REFERENCES Entregable(Id) ON DELETE CASCADE
);

-- ===========================================
-- Tabla: Responsable_Entregable (relación N:M)
-- ===========================================
CREATE TABLE Responsable_Entregable (
    IdResponsable INT NOT NULL,
    IdEntregable INT NOT NULL,
    FechaAsociacion DATE NULL,
    PRIMARY KEY (IdResponsable, IdEntregable),
    CONSTRAINT FK_ResponsableEntregable_Responsable FOREIGN KEY (IdResponsable) REFERENCES Responsable(Id) ON DELETE CASCADE,
    CONSTRAINT FK_ResponsableEntregable_Entregable FOREIGN KEY (IdEntregable) REFERENCES Entregable(Id) ON DELETE CASCADE
);

-- ===========================================
-- Tabla: Archivo
-- ===========================================
CREATE TABLE Archivo (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    IdUsuario INT NOT NULL,
    Ruta NVARCHAR(MAX) NOT NULL,
    Nombre NVARCHAR(255) NOT NULL,
    Tipo NVARCHAR(50) NULL,
    Fecha DATE NULL,
    CONSTRAINT FK_Archivo_Usuario FOREIGN KEY (IdUsuario) REFERENCES Usuario(Id) ON DELETE CASCADE
);

-- ===========================================
-- Tabla: Archivo_Entregable (relación N:M)
-- ===========================================
CREATE TABLE Archivo_Entregable (
    IdArchivo INT NOT NULL,
    IdEntregable INT NOT NULL,
    PRIMARY KEY (IdArchivo, IdEntregable),
    CONSTRAINT FK_ArchivoEntregable_Archivo FOREIGN KEY (IdArchivo) REFERENCES Archivo(Id) ON DELETE CASCADE,
    CONSTRAINT FK_ArchivoEntregable_Entregable FOREIGN KEY (IdEntregable) REFERENCES Entregable(Id) ON DELETE CASCADE
);

-- ===========================================
-- Tabla: Actividad
-- ===========================================
CREATE TABLE Actividad (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    IdEntregable INT NOT NULL,
    Titulo NVARCHAR(255) NOT NULL,
    Descripcion NVARCHAR(MAX) NULL,
    FechaInicio DATE NULL,
    FechaFinPrevista DATE NULL,
    FechaModificacion DATE NULL,
    FechaFinalizacion DATE NULL,
    Prioridad INT NULL,
    PorcentajeAvance INT CHECK (PorcentajeAvance BETWEEN 0 AND 100),
    CONSTRAINT FK_Actividad_Entregable FOREIGN KEY (IdEntregable) REFERENCES Entregable(Id) ON DELETE CASCADE
);

-- ===========================================
-- Tabla: Presupuesto
-- ===========================================
CREATE TABLE Presupuesto (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    IdProyecto INT NOT NULL,
    MontoSolicitado DECIMAL(15,2) NOT NULL,
    Estado NVARCHAR(20) NOT NULL DEFAULT 'Pendiente' CHECK (Estado IN ('Pendiente','Aprobado','Rechazado')),
    MontoAprobado DECIMAL(15,2) NULL,
    PeriodoAnio INT NULL,
    FechaSolicitud DATE NULL,
    FechaAprobacion DATE NULL,
    Observaciones NVARCHAR(MAX) NULL,
    CONSTRAINT FK_Presupuesto_Proyecto FOREIGN KEY (IdProyecto) REFERENCES Proyecto(Id) ON DELETE CASCADE
);

-- ===========================================
-- Tabla: DistribucionPresupuesto
-- ===========================================
CREATE TABLE DistribucionPresupuesto (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    IdPresupuestoPadre INT NOT NULL,
    IdProyectoHijo INT NOT NULL,
    MontoAsignado DECIMAL(15,2) NOT NULL,
    CONSTRAINT FK_Distribucion_Presupuesto FOREIGN KEY (IdPresupuestoPadre) REFERENCES Presupuesto(Id) ON DELETE CASCADE,
    CONSTRAINT FK_Distribucion_Proyecto FOREIGN KEY (IdProyectoHijo) REFERENCES Proyecto(Id) ON DELETE NO ACTION
);

-- ===========================================
-- Tabla: EjecucionPresupuesto
-- ===========================================
CREATE TABLE EjecucionPresupuesto (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    IdPresupuesto INT NOT NULL,
    Anio INT NOT NULL,
    MontoPlaneado DECIMAL(15,2) NULL,
    MontoEjecutado DECIMAL(15,2) NULL,
    Observaciones NVARCHAR(MAX) NULL,
    CONSTRAINT FK_Ejecucion_Presupuesto FOREIGN KEY (IdPresupuesto) REFERENCES Presupuesto(Id) ON DELETE CASCADE
);

-- ===========================================
-- Tabla: VariableEstrategica
-- ===========================================
CREATE TABLE VariableEstrategica (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Titulo NVARCHAR(255) NOT NULL,
    Descripcion NVARCHAR(MAX) NULL
);

-- ===========================================
-- Tabla: ObjetivoEstrategico
-- ===========================================
CREATE TABLE ObjetivoEstrategico (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    IdVariable INT NOT NULL,
    Titulo NVARCHAR(255) NOT NULL,
    Descripcion NVARCHAR(MAX) NULL,
    CONSTRAINT FK_ObjetivoEstrategico_Variable FOREIGN KEY (IdVariable) REFERENCES VariableEstrategica(Id) ON DELETE CASCADE
);

-- ===========================================
-- Tabla: MetaEstrategica
-- ===========================================
CREATE TABLE MetaEstrategica (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    IdObjetivo INT NOT NULL,
    Titulo NVARCHAR(255) NOT NULL,
    Descripcion NVARCHAR(MAX) NULL,
    CONSTRAINT FK_MetaEstrategica_Objetivo FOREIGN KEY (IdObjetivo) REFERENCES ObjetivoEstrategico(Id) ON DELETE CASCADE
);

-- ===========================================
-- Tabla: Meta_Proyecto (relación N:M)
-- ===========================================
CREATE TABLE Meta_Proyecto (
    IdMeta INT NOT NULL,
    IdProyecto INT NOT NULL,
    FechaAsociacion DATE NULL,
    PRIMARY KEY (IdMeta, IdProyecto),
    CONSTRAINT FK_MetaProyecto_Meta FOREIGN KEY (IdMeta) REFERENCES MetaEstrategica(Id) ON DELETE CASCADE,
    CONSTRAINT FK_MetaProyecto_Proyecto FOREIGN KEY (IdProyecto) REFERENCES Proyecto(Id) ON DELETE CASCADE
);

-- ========================================
-- 1. Tipo de Proyecto
-- ========================================
INSERT INTO TipoProyecto (Nombre, Descripcion)
VALUES ('Estratégico', 'Proyectos de carácter transversal institucional');

-- ========================================
-- 2. Responsables
-- ========================================
INSERT INTO Usuario (Email, Contrasena, RutaAvatar, Activo) VALUES
('laura.gomez@uni.edu', '123456', NULL, 1),
('carlos.ramirez@uni.edu', '123456', NULL, 1),
('ana.torres@uni.edu', '123456', NULL, 1);

INSERT INTO TipoResponsable (Titulo, Descripcion) VALUES
('Gerente de Proyecto', 'Responsable general del proyecto'),
('Líder de Microproyecto', 'Responsable de microproyectos'),
('Miembro del Equipo', 'Integrante de apoyo');

-- Responsables asociados a usuarios
INSERT INTO Responsable (IdTipoResponsable, IdUsuario, Nombre)
VALUES 
(1, 1, 'Laura Gómez'),
(2, 2, 'Carlos Ramírez'),
(2, 3, 'Ana Torres');

-- ========================================
-- 3. Proyecto Estratégico (Padre)
-- ========================================
INSERT INTO Proyecto (IdProyectoPadre, IdResponsable, IdTipoProyecto, Codigo, Titulo, Descripcion, FechaInicio, FechaFinPrevista, RutaLogo)
VALUES (
    NULL, 1, 1, 'PE-001',
    'Sistema de Analítica Universitaria',
    'Desarrollar un sistema institucional de análisis de datos académicos y administrativos.',
    '2025-02-01', '2026-12-31', NULL
);

-- ========================================
-- 4. Microproyectos (hijos del anterior)
-- ========================================
INSERT INTO Proyecto (IdProyectoPadre, IdResponsable, IdTipoProyecto, Codigo, Titulo, Descripcion, FechaInicio, FechaFinPrevista)
VALUES
(1, 2, 1, 'PE-001-M1', 'Diseño de la Arquitectura de Datos',
 'Definir la estructura de datos para la integración de sistemas académicos.',
 '2025-02-15', '2025-07-30'),

(1, 3, 1, 'PE-001-M2', 'Desarrollo del Dashboard Académico',
 'Construir un tablero de control para el seguimiento académico institucional.',
 '2025-08-01', '2026-02-28');

-- ========================================
-- 5. Estados y Seguimiento
-- ========================================
INSERT INTO Estado (Nombre, Descripcion) VALUES
('En ejecución', 'El proyecto está actualmente en desarrollo'),
('Finalizado', 'El proyecto ya fue completado');

INSERT INTO Estado_Proyecto (IdProyecto, IdEstado)
VALUES (1, 1);  -- Proyecto padre en ejecución

-- ========================================
-- 6. Productos y Entregables
-- ========================================
INSERT INTO TipoProducto (Nombre, Descripcion)
VALUES ('Software', 'Aplicaciones y sistemas informáticos');

INSERT INTO Producto (IdTipoProducto, Codigo, Titulo, Descripcion)
VALUES (1, 'P-001', 'Plataforma de Analítica de Datos', 'Sistema web para consulta de reportes institucionales');

INSERT INTO Proyecto_Producto (IdProyecto, IdProducto, FechaAsociacion)
VALUES (1, 1, GETDATE());

INSERT INTO Entregable (Codigo, Titulo, Descripcion, FechaInicio, FechaFinPrevista)
VALUES ('E-001', 'Módulo de Integración de Datos', 'Submódulo que conecta los sistemas académicos', '2025-03-20', '2025-06-30');

INSERT INTO Producto_Entregable (IdProducto, IdEntregable, FechaAsociacion)
VALUES (1, 1, GETDATE());

INSERT INTO Responsable_Entregable (IdResponsable, IdEntregable, FechaAsociacion)
VALUES (2, 1, GETDATE());

INSERT INTO Actividad (IdEntregable, Titulo, PorcentajeAvance)
VALUES (1, 'Revisión de calidad del modelo de datos', 0);

INSERT INTO Archivo (IdUsuario, Ruta, Nombre, Tipo, Fecha)
VALUES (2, '/repositorio/proyectos/analitica/modelo.pdf', 'ModeloDatos.pdf', 'PDF', GETDATE());

INSERT INTO Archivo_Entregable (IdArchivo, IdEntregable)
VALUES (1, 1);

-- ========================================
-- 7. Presupuesto
-- ========================================
INSERT INTO Presupuesto (IdProyecto, MontoSolicitado, Estado, PeriodoAnio, FechaSolicitud)
VALUES (1, 50000000, 'Pendiente', 2025, GETDATE());

-- ========================================
-- 8. Estrategia Institucional
-- ========================================
INSERT INTO VariableEstrategica (Titulo, Descripcion)
VALUES ('Innovación', 'Apuesta por la innovación institucional');

INSERT INTO ObjetivoEstrategico (IdVariable, Titulo, Descripcion)
VALUES (1, 'Fomentar el uso de tecnologías emergentes', NULL);

INSERT INTO MetaEstrategica (IdObjetivo, Titulo, Descripcion)
VALUES (1, 'Implementar plataforma de datos centralizada', NULL);

INSERT INTO Meta_Proyecto (IdMeta, IdProyecto, FechaAsociacion)
VALUES (1, 1, GETDATE());

-- Roles
INSERT INTO rol (nombre) VALUES 
('Administrador'),
('Vendedor'),
('Cajero'),
('Contador'),
('Cliente');

-- Usuarios con roles
EXEC crear_usuario_con_roles 'admin@correo.com', 'admin123', '[{"fkidrol":1}]';
EXEC crear_usuario_con_roles 'vendedor1@correo.com', 'vend123', '[{"fkidrol":2},{"fkidrol":3}]';
EXEC crear_usuario_con_roles 'jefe@correo.com', 'jefe123', '[{"fkidrol":1},{"fkidrol":3},{"fkidrol":4}]';
EXEC crear_usuario_con_roles 'cliente1@correo.com', 'cli123', '[{"fkidrol":5}]';

-- ==============================================================
-- PROCEDIMIENTO: Crear usuario con roles (CON TRANSACCIÓN)
-- ==============================================================
CREATE OR ALTER PROCEDURE crear_usuario_con_roles
    @p_email VARCHAR(100),
    @p_contrasena VARCHAR(100),
    @p_roles NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @ErrorMessage NVARCHAR(4000);
    DECLARE @ErrorSeverity INT;
    DECLARE @ErrorState INT;
    
    BEGIN TRY
        BEGIN TRANSACTION;
        
        -- Validar que el usuario no exista
        IF EXISTS (SELECT 1 FROM usuario WHERE email = @p_email)
        BEGIN
            THROW 50005, 'El usuario ya existe', 1;
        END
        
        -- Crear usuario
        INSERT INTO usuario (email, contrasena)
        VALUES (@p_email, @p_contrasena);
        
        -- Insertar roles desde JSON
        INSERT INTO rol_usuario (fkemail, fkidrol)
        SELECT 
            @p_email,
            fkidrol
        FROM OPENJSON(@p_roles)
        WITH (
            fkidrol INT '$.fkidrol'
        );
        
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        
        SELECT @ErrorMessage = ERROR_MESSAGE(),
               @ErrorSeverity = ERROR_SEVERITY(),
               @ErrorState = ERROR_STATE();
        
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END;
GO

-- ==============================================================
-- PROCEDIMIENTO: Actualizar usuario con roles (CON TRANSACCIÓN)
-- ==============================================================
CREATE OR ALTER PROCEDURE actualizar_usuario_con_roles
    @p_email VARCHAR(100),
    @p_contrasena VARCHAR(100),
    @p_roles NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @ErrorMessage NVARCHAR(4000);
    DECLARE @ErrorSeverity INT;
    DECLARE @ErrorState INT;
    
    BEGIN TRY
        BEGIN TRANSACTION;
        
        -- Validar que el usuario exista
        IF NOT EXISTS (SELECT 1 FROM usuario WHERE email = @p_email)
        BEGIN
            THROW 50006, 'El usuario no existe', 1;
        END
        
        -- Actualizar contraseña
        UPDATE usuario
        SET contrasena = @p_contrasena
        WHERE email = @p_email;
        
        -- Eliminar roles antiguos
        DELETE FROM rol_usuario WHERE fkemail = @p_email;
        
        -- Insertar nuevos roles desde JSON
        INSERT INTO rol_usuario (fkemail, fkidrol)
        SELECT 
            @p_email,
            fkidrol
        FROM OPENJSON(@p_roles)
        WITH (
            fkidrol INT '$.fkidrol'
        );
        
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        
        SELECT @ErrorMessage = ERROR_MESSAGE(),
               @ErrorSeverity = ERROR_SEVERITY(),
               @ErrorState = ERROR_STATE();
        
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END;
GO

-- ==============================================================
-- PROCEDIMIENTO: Eliminar usuario con roles (CON TRANSACCIÓN)
-- ==============================================================
CREATE OR ALTER PROCEDURE eliminar_usuario_con_roles
    @p_email VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @ErrorMessage NVARCHAR(4000);
    DECLARE @ErrorSeverity INT;
    DECLARE @ErrorState INT;
    
    BEGIN TRY
        BEGIN TRANSACTION;
        
        -- Validar que el usuario exista
        IF NOT EXISTS (SELECT 1 FROM usuario WHERE email = @p_email)
        BEGIN
            THROW 50006, 'El usuario no existe', 1;
        END
        
        -- Eliminar roles
        DELETE FROM rol_usuario WHERE fkemail = @p_email;
        
        -- Eliminar usuario
        DELETE FROM usuario WHERE email = @p_email;
        
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        
        SELECT @ErrorMessage = ERROR_MESSAGE(),
               @ErrorSeverity = ERROR_SEVERITY(),
               @ErrorState = ERROR_STATE();
        
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END;
GO

-- ==============================================================
-- FUNCIÓN: Consultar usuario con roles (retorna JSON)
-- ==============================================================
CREATE OR ALTER FUNCTION consultar_usuario_con_roles(@p_email VARCHAR(100))
RETURNS NVARCHAR(MAX)
AS
BEGIN
    DECLARE @resultado NVARCHAR(MAX);
    
    SELECT @resultado = (
        SELECT 
            u.email,
            (
                SELECT 
                    r.id AS idrol,
                    r.nombre
                FROM rol_usuario ru
                INNER JOIN rol r ON r.id = ru.fkidrol
                WHERE ru.fkemail = u.email
                FOR JSON PATH
            ) AS roles
        FROM usuario u
        WHERE u.email = @p_email
        FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
    );
    
    RETURN @resultado;
END;
GO

-- ==============================================================
-- FUNCIÓN: Listar usuarios con roles (retorna JSON)
-- ==============================================================
CREATE OR ALTER FUNCTION listar_usuarios_con_roles()
RETURNS NVARCHAR(MAX)
AS
BEGIN
    DECLARE @resultado NVARCHAR(MAX);
    
    SELECT @resultado = (
        SELECT 
            u.email,
            (
                SELECT 
                    r.id AS idrol,
                    r.nombre
                FROM rol_usuario ru
                INNER JOIN rol r ON r.id = ru.fkidrol
                WHERE ru.fkemail = u.email
                FOR JSON PATH
            ) AS roles
        FROM usuario u
        FOR JSON PATH
    );
    
    RETURN ISNULL(@resultado, '[]');
END;
GO
