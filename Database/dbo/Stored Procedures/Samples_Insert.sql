CREATE PROCEDURE [dbo].[Samples_Insert]
	@samples SampleType READONLY
AS
BEGIN

	INSERT INTO [dbo].[ConnectSample] ([Name], [Description])
	SELECT [Name], [Description] FROM @samples

END