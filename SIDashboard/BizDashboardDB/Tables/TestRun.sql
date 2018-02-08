CREATE TABLE [dbo].[TestRun]
(
	[VSO] varchar(32) NOT NULL,
	[Project] varchar(32) NOT NULL,
	[BuildOrReleaseId] INT NOT NULL,
	[RunId] INT NOT NULL,
	[PassedTestNum] INT DEFAULT 0,
	[TotalTestNum] INT DEFAULT 0,
	[CreationDate] DATETIME,
	[TriggeredBy] VARCHAR(16)

	PRIMARY KEY([VSO], [Project], [BuildOrReleaseId], [RunId], [TriggeredBy])
)
GO;
