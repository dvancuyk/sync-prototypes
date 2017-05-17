CREATE PROCEDURE [dbo].[Smpls_SaveCollection]
	@samples SampleType READONLY
AS
BEGIN

	MERGE ClientSmpl WITH (HOLDLOCK) AS c
	USING @samples AS s
	ON s.[Name] = c.[Name]
	WHEN MATCHED THEN
		UPDATE SET
			[Description]		= s.[Description],
			[IsActive]			= s.[IsActive],
			[Token]				= s.[Token],
			[AddressLine1]		= s.[AddressLine1],
			[AddressLine2]		= s.[Token],
			[City]				= s.[City],
			[State]				= s.[State],
			[ZipCode]			= s.[ZipCode],
			[BuildingNumber]	= s.[BuildingNumber],
			[Legal1]			= s.[Legal1],
			[Legal2]			= s.[Legal2],
			[SquareFootage]		= s.[SquareFootage],
			[AssessedValue]		= s.[AssessedValue],
			[OwnerRatio]		= s.[OwnerRatio],
			[GroupOwner_Number] = s.[GroupOwner_Number],
			[GlCostCenter]		= s.[GlCostCenter],
			[Latitude]			= s.[Latitude],
			[Longitude]			= s.[Longitude],
			[GeocodeProvider]	= s.[GeocodeProvider],
			[GeocodeAccuracy]	= s.[GeocodeAccuracy],
			[ModifiedDate]		= s.[ModifiedDate],
			[PROASSMTCATG]		= s.[PROASSMTCATG],
			[PROASSMTAMT]		= s.[PROASSMTAMT],
			[PROASSESSEDVAL]	= s.[PROASSESSEDVAL]
	WHEN NOT MATCHED THEN
		INSERT 
		(
			[Name], 
			[Description],
			[IsActive],
			[Token],
			[AddressLine1],
			[AddressLine2],
			[City],
			[State],
			[ZipCode],
			[BuildingNumber],
			[Legal1],
			[Legal2],
			[SquareFootage],
			[AssessedValue],
			[OwnerRatio],
			[GroupOwner_Number],
			[GlCostCenter],
			[Latitude],
			[Longitude],
			[GeocodeProvider],
			[GeocodeAccuracy],
			[ModifiedDate],
			[PROASSMTCATG],
			[PROASSMTAMT],
			[PROASSESSEDVAL]
		) 
		
		VALUES 
		(
			s.[Name], 
			s.[Description],
			s.[IsActive],
			s.[Token],
			s.[AddressLine1],
			s.[AddressLine2],
			s.[City],
			s.[State],
			s.[ZipCode],
			s.[BuildingNumber],
			s.[Legal1],
			s.[Legal2],
			s.[SquareFootage],
			s.[AssessedValue],
			s.[OwnerRatio],
			s.[GroupOwner_Number],
			s.[GlCostCenter],
			s.[Latitude],
			s.[Longitude],
			s.[GeocodeProvider],
			s.[GeocodeAccuracy],
			s.[ModifiedDate],
			s.[PROASSMTCATG],
			s.[PROASSMTAMT],
			s.[PROASSESSEDVAL]
		);

END
