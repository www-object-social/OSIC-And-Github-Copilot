CREATE TABLE [dbo].[Binding]
(
	[ID] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Expires] DATETIME NOT NULL, 
    [Created] DATETIME NOT NULL, 
    [ProjectSoftware] INT NOT NULL, 
    [ProjectType] INT NOT NULL, 
    [UnitType] INT NOT NULL, 
    [UnitTwoLetterISORegionName] INT NOT NULL, 
    [UnitTwoLetterISOLanguageName] INT NOT NULL, 
    [UnitTimeZone] INT NOT NULL, 
)
