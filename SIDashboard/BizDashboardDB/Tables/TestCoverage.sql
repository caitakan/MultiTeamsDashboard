CREATE TABLE [dbo].[TestCoverage]
(
	[VSO] varchar(32) NOT NULL,
	[Project] varchar(32) NOT NULL,
	[BuildId] INT NOT NULL,
	[ModuleName] varchar(256) NOT NULL,
	[LinesCovered] INT DEFAULT 0,
	[LinesNotCovered]  INT DEFAULT 0,

	PRIMARY KEY([VSO], [Project], [BuildId], [ModuleName])
)
GO;
