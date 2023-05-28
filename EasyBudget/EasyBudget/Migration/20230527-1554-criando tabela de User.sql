CREATE TABLE [User]
(
    id INT IDENTITY,
    first_name NVARCHAR(20) NOT NULL,
    last_name NVARCHAR(50) NOT NULL,
    email NVARCHAR(320) NOT NULL,
    password NVARCHAR(50) NOT NULL,
    birth DATE NOT NULL,
    created datetime2(0) NOT NULL DEFAULT GETDATE(),
    updated datetime2(0),
    CONSTRAINT Pk_User_id PRIMARY KEY (id),
    CONSTRAINT Ak_email UNIQUE (email),
)

GO

CREATE TRIGGER dbo.User_updated
ON [User]
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    
    UPDATE [User]
    SET updated = GETDATE()
    FROM [User] u
    JOIN inserted i
        ON u.id = i.id
END