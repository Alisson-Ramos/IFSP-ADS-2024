GO
CREATE PROCEDURE proc_UsuarioInserir
    @nome VARCHAR(100)
AS
INSERT INTO Usuario(nome) VALUES (@nome);
END;
GO
CREATE PROCEDURE proc_UsuarioBuscar
    @id INT
AS
SELECT * FROM Usuario WHERE id = @id;
END;
GO
CREATE PROCEDURE proc_UsuarioRemover
    @id INT
AS
IF NOT EXISTS (SELECT 1 FROM UsuarioAmbiente WHERE id_usuario = @id)
BEGIN
    DELETE FROM Usuario WHERE id = @id;
    RETURN 1;
END
RETURN 0;
END;
GO
CREATE PROCEDURE proc_AmbienteInserir
    @nome VARCHAR(100)
AS
INSERT INTO Ambiente(nome) VALUES (@nome);
END;
GO
CREATE PROCEDURE proc_PermissaoConceder
    @id_usuario INT,
    @id_ambiente INT
AS
IF NOT EXISTS (
    SELECT 1 FROM UsuarioAmbiente 
    WHERE id_usuario = @id_usuario AND id_ambiente = @id_ambiente
)
INSERT INTO UsuarioAmbiente VALUES (@id_usuario, @id_ambiente);
END;
GO
CREATE PROCEDURE proc_PermissaoRevogar
    @id_usuario INT,
    @id_ambiente INT
AS
DELETE FROM UsuarioAmbiente 
WHERE id_usuario = @id_usuario AND id_ambiente = @id_ambiente;
END;
GO
CREATE PROCEDURE proc_LogRegistrar
    @id_usuario INT,
    @id_ambiente INT,
    @tipo BIT
AS
INSERT INTO LogAcesso(id_usuario, id_ambiente, dt_acesso, tipo_acesso)
VALUES (@id_usuario, @id_ambiente, GETDATE(), @tipo);

-- Mantém apenas os últimos 100 registros do ambiente
;WITH CTE AS (
    SELECT id_log,
           ROW_NUMBER() OVER (ORDER BY dt_acesso DESC) AS rn
    FROM LogAcesso
    WHERE id_ambiente = @id_ambiente
)
DELETE FROM LogAcesso WHERE id_log IN (
    SELECT id_log FROM CTE WHERE rn > 100
);
END;
GO
CREATE PROCEDURE proc_LogListar
    @id_ambiente INT,
    @tipo INT   -- 0 = negado, 1 = autorizado, 2 = todos
AS
IF @tipo = 2
    SELECT * FROM LogAcesso WHERE id_ambiente = @id_ambiente ORDER BY dt_acesso DESC;
ELSE
    SELECT * FROM LogAcesso WHERE id_ambiente = @id_ambiente AND tipo_acesso = @tipo ORDER BY dt_acesso DESC;
END;
