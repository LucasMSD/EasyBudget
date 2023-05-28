ALTER TABLE Movement ADD [user_id] INT NOT NULL;
GO

ALTER TABLE Movement ADD CONSTRAINT Fk_Movement_User_user_id FOREIGN KEY ([user_id]) REFERENCES [User] (id);
GO

ALTER TABLE Category ADD [user_id] INT NOT NULL;
GO

ALTER TABLE Category ADD CONSTRAINT Fk_Category_User_user_id FOREIGN KEY ([user_id]) REFERENCES [User] (id);
