CREATE PROCEDURE [dbo].[Samples_Update]
	@samples SampleType READONLY
AS
BEGIN

	UPDATE [ConnectSample]
	SET
		[Name] = synced.[Name],
		[Description] = synced.[Description]
	FROM [dbo].[ConnectSample] 
	JOIN @samples AS synced ON synced.Id = [ConnectSample].[Id]
	

END
