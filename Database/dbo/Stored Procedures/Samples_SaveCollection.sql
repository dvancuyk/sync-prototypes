CREATE PROCEDURE [dbo].[Samples_SaveCollection]
	@samples SampleType READONLY
AS
BEGIN

	-- Deletes?


	MERGE ConnectSample WITH (HOLDLOCK) AS c
	USING @samples AS s
	ON s.[Name] = c.[Name]
	WHEN MATCHED THEN
		UPDATE SET
			[Description] = s.[Description]
	WHEN NOT MATCHED THEN
		INSERT ([Name], [Description]) VALUES (s.[Name], s.[Description]);

END
