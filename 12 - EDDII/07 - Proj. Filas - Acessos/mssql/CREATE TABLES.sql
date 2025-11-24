CREATE TABLE Usuario (
    id INT IDENTITY(1,1) PRIMARY KEY,
    nome VARCHAR(100) NOT NULL
);

CREATE TABLE Ambiente (
    id INT IDENTITY(1,1) PRIMARY KEY,
    nome VARCHAR(100) NOT NULL
);

-- Permissões de acesso
CREATE TABLE UsuarioAmbiente (
    id_usuario INT NOT NULL,
    id_ambiente INT NOT NULL,
    PRIMARY KEY (id_usuario, id_ambiente),
    FOREIGN KEY (id_usuario) REFERENCES Usuario(id) ON DELETE CASCADE,
    FOREIGN KEY (id_ambiente) REFERENCES Ambiente(id) ON DELETE CASCADE
);

-- Logs
CREATE TABLE LogAcesso (
    id_log INT IDENTITY(1,1) PRIMARY KEY,
    id_usuario INT NOT NULL,
    id_ambiente INT NOT NULL,
    dt_acesso DATETIME NOT NULL,
    tipo_acesso BIT NOT NULL,

    FOREIGN KEY (id_usuario) REFERENCES Usuario(id),
    FOREIGN KEY (id_ambiente) REFERENCES Ambiente(id)
);
