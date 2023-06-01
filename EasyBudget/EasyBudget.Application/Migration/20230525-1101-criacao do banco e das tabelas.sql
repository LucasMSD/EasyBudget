CREATE DATABASE EasyBudgetDb;
GO

USE EasyBudgetDb;
GO

CREATE TABLE Category
(
    id INT IDENTITY,
    name NVARCHAR(50) NOT NULL,
    type tinyint NOT NULL CHECK (type = 1 OR type = 2),
    created datetime2(0) NOT NULL DEFAULT GETDATE(),
    updated datetime2(0),
    CONSTRAINT Pk_Category_id PRIMARY KEY (id)
);
GO

CREATE TABLE Movement
(
    id INT IDENTITY,
    title NVARCHAR(50) NOT NULL,
    amount DECIMAL(18,2) NOT NULL,
    type TINYINT NOT NULL CHECK (type = 1 OR type = 2),
    date DATE NOT NULL,
    category_id INT NOT NULL,
    description NVARCHAR(200),
    created datetime2(0) NOT NULL DEFAULT GETDATE(),
    updated datetime2(0)
    CONSTRAINT Pk_Movement_id PRIMARY KEY (id),
    CONSTRAINT Fk_Movement_Category_id FOREIGN KEY (category_id) REFERENCES Category (id)
);

GO

CREATE TRIGGER dbo.Category_updated
ON Category
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Category
        SET updated = GETDATE()
    FROM Category c
        JOIN inserted i
            ON c.id = i.id;

END;
GO

CREATE TRIGGER dbo.Movement_updated
ON Movement
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Movement
        SET updated = getdate()
    FROM Movement c
        JOIN inserted i
            ON c.id = i.id;

END;