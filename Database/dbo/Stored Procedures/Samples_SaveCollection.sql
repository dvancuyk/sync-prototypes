﻿CREATE PROCEDURE [dbo].[Samples_SaveCollection]
	@samples SampleType READONLY
AS
BEGIN

	DELETE FROM dbo.ConnectSample WHERE Id IN
		(SELECT ConnectSample.Id FROM dbo.ConnectSample JOIN @samples AS S ON S.Id = ConnectSample.Id  WHERE s.[Delete] = 1)

	MERGE ConnectSample WITH (HOLDLOCK) AS c
	USING @samples AS s
	ON s.[Id] = c.[Id]
	WHEN MATCHED THEN
		UPDATE SET
			[Description] = s.[Description]
	WHEN NOT MATCHED THEN
		INSERT ([Name], [Description]) VALUES (s.[Name], s.[Description]);

END
