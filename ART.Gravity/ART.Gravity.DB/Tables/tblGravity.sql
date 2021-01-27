CREATE TABLE [dbo].[tblGravity]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [ChangeDate] DATETIME NOT NULL, 
    [MassOne] FLOAT NOT NULL, 
    [MassTwo] FLOAT NOT NULL, 
    [Distance] FLOAT NOT NULL, 
    [Force] FLOAT NOT NULL
)
