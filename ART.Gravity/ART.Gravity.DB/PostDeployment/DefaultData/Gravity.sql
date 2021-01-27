BEGIN
	INSERT INTO dbo.tblGravity (Id, MassOne, MassTwo, Distance, ChangeDate, Force)
	VALUES
	(NEWID(), 300000, 400000, 1.5, GETDATE(), 1.772228)
END