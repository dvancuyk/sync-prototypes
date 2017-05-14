<<<<<<< HEAD
﻿CREATE PROCEDURE [dbo].[Samples_Delete]
	@samples SampleType READONLY
AS
BEGIN

	DELETE FROM dbo.ConnectSample WHERE Id IN
		(SELECT ConnectSample.Id FROM dbo.ConnectSample LEFT JOIN @samples AS S ON S.Id = ConnectSample.Id  WHERE S.Id IS NOT NULL)

END

=======
﻿CREATE PROCEDURE [dbo].[Samples_Delete]
	@samples SampleType READONLY
AS
BEGIN

	DELETE FROM dbo.ConnectSample WHERE Id IN
		(SELECT ConnectSample.Id FROM dbo.ConnectSample LEFT JOIN @samples AS S ON S.Id = ConnectSample.Id  WHERE S.Id IS NOT NULL)

END

>>>>>>> phase-2
