-- Crear la base de datos
CREATE DATABASE PeliculasDB;
GO

-- Usar la base recién creada
USE PeliculasDB;
GO

-- Tabla: Usuarios
CREATE TABLE Usuarios (
    Id INT PRIMARY KEY IDENTITY(1,1),
    UserName NVARCHAR(100) NOT NULL,
    Password NVARCHAR(100) NOT NULL
);
GO

CREATE TABLE Actores (
    ActorId INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Apellido NVARCHAR(100) NOT NULL,
    FechaNacimiento DATE NULL,
    Nacionalidad NVARCHAR(50) NULL,
    Biografia NVARCHAR(MAX) NULL
);
GO
-- Tabla: Favoritos
CREATE TABLE Favoritos (
    Id INT PRIMARY KEY IDENTITY(1,1),
    UsuarioId INT NOT NULL,
    ItemId INT NOT NULL,
    Tipo NVARCHAR(50) NOT NULL, -- Puede ser "pelicula" o "serie"
    Comentario NVARCHAR(500) NULL,
    Calificacion INT NULL,
    FOREIGN KEY (UsuarioId) REFERENCES Usuarios(Id)
);
GO

-- Tabla: Watchlist
CREATE TABLE Watchlist (
    Id INT PRIMARY KEY IDENTITY(1,1),
    UsuarioId INT NOT NULL,
    ItemId INT NOT NULL,
    Tipo NVARCHAR(50) NOT NULL, -- Puede ser "pelicula" o "serie"
    Prioridad INT NULL,
    FOREIGN KEY (UsuarioId) REFERENCES Usuarios(Id)
);
GO

-- =============================================
-- sp_AgregarFavorito
-- =============================================
CREATE PROCEDURE sp_AgregarFavorito
    @UsuarioId INT,
    @ItemId INT,
    @Tipo NVARCHAR(50),
    @Comentario NVARCHAR(500),
    @Calificacion INT
AS
BEGIN
    INSERT INTO Favoritos (UsuarioId, ItemId, Tipo, Comentario, Calificacion)
    VALUES (@UsuarioId, @ItemId, @Tipo, @Comentario, @Calificacion);

    RETURN SCOPE_IDENTITY();
END;
GO

-- =============================================
-- sp_ActualizarFavorito
-- =============================================
CREATE PROCEDURE sp_ActualizarFavorito
    @Id INT,
    @Comentario NVARCHAR(500),
    @Calificacion INT
AS
BEGIN
    UPDATE Favoritos
    SET Comentario = @Comentario,
        Calificacion = @Calificacion
    WHERE Id = @Id;

    RETURN @@ROWCOUNT;
END;
GO

-- =============================================
-- sp_EliminarFavorito
-- =============================================
CREATE PROCEDURE sp_EliminarFavorito
    @Id INT
AS
BEGIN
    DELETE FROM Favoritos
    WHERE Id = @Id;

    RETURN @@ROWCOUNT;
END;
GO

-- =============================================
-- sp_AgregarWatchlist
-- =============================================
CREATE PROCEDURE sp_AgregarWatchlist
    @UsuarioId INT,
    @ItemId INT,
    @Tipo NVARCHAR(50),
    @Prioridad INT
AS
BEGIN
    INSERT INTO Watchlist (UsuarioId, ItemId, Tipo, Prioridad)
    VALUES (@UsuarioId, @ItemId, @Tipo, @Prioridad);

    RETURN SCOPE_IDENTITY();
END;
GO
-- =============================================
-- sp_ActualizarWatchlist
-- =============================================
CREATE PROCEDURE sp_ActualizarWatchlist
    @Id INT,
    @Prioridad INT
AS
BEGIN
    UPDATE Watchlist
    SET Prioridad = @Prioridad
    WHERE Id = @Id;

    RETURN @@ROWCOUNT;
END;
GO

-- =============================================
-- sp_EliminarWatchlist
-- =============================================
CREATE PROCEDURE sp_EliminarWatchlist
    @Id INT
AS
BEGIN
    DELETE FROM Watchlist
    WHERE Id = @Id;

    RETURN @@ROWCOUNT;
END;
GO

-- =============================================
-- sp_RegistrarUsuario
-- =============================================
CREATE PROCEDURE sp_RegistrarUsuario
    @UserName NVARCHAR(100),
    @Password NVARCHAR(100)
AS
BEGIN
    INSERT INTO Usuarios (UserName, Password)
    VALUES (@UserName, @Password);

    RETURN SCOPE_IDENTITY();
END;
GO

-- =============================================
-- sp_ObtenerUsuarioPorCredenciales
-- =============================================
CREATE PROCEDURE sp_ObtenerUsuarioPorCredenciales
    @UserName NVARCHAR(100),
    @Password NVARCHAR(100)
AS
BEGIN
    SELECT Id, UserName
    FROM Usuarios
    WHERE UserName = @UserName AND Password = @Password;

    RETURN @@ROWCOUNT;
END;
GO

-- =============================================
-- sp_ObtenerFavoritos
-- =============================================
CREATE PROCEDURE sp_ObtenerFavoritos
    @UsuarioId INT
AS
BEGIN
    SELECT *
    FROM Favoritos
    WHERE UsuarioId = @UsuarioId;

    RETURN @@ROWCOUNT;
END;
GO

-- =============================================
-- sp_ObtenerWatchlist
-- =============================================
CREATE PROCEDURE sp_ObtenerWatchlist
    @UsuarioId INT
AS
BEGIN
    SELECT *
    FROM Watchlist
    WHERE UsuarioId = @UsuarioId;

    RETURN @@ROWCOUNT;
END;
GO

-- =============================================
-- sp_ObtenerWatchlistPorId
-- =============================================
CREATE PROCEDURE sp_ObtenerWatchlistPorId
    @Id INT
AS
BEGIN
    SELECT *
    FROM Watchlist
    WHERE Id = @Id;

    RETURN @@ROWCOUNT;
END;
GO

-- =============================================
-- sp_ObtenerWatchlistPorUsuario
-- =============================================
CREATE PROCEDURE sp_ObtenerWatchlistPorUsuario
    @UsuarioId INT
AS
BEGIN
    SELECT *
    FROM Watchlist
    WHERE UsuarioId = @UsuarioId;

    RETURN @@ROWCOUNT;
END;
GO

-- Insertar un usuario de prueba
INSERT INTO Usuarios (UserName, Password)
VALUES ('ander.corrales', '1234');

-- Guardar el ID generado
DECLARE @UsuarioId INT = SCOPE_IDENTITY();

-- Insertar un favorito de prueba
INSERT INTO Favoritos (UsuarioId, ItemId, Tipo, Comentario, Calificacion)
VALUES (@UsuarioId, 550, 'pelicula', 'Obra maestra del cine.', 10);

-- Insertar un ítem en la watchlist
INSERT INTO Watchlist (UsuarioId, ItemId, Tipo, Prioridad)
VALUES (@UsuarioId, 603, 'pelicula', 1);