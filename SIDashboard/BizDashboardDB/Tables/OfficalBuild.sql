CREATE TABLE [dbo].[OfficialBuild]
(
	[VSO] varchar(32) NOT NULL,
	[Project] varchar(32) NOT NULL,
	[BuildId] INT NOT NULL,	
	[Result] BIT DEFAULT 0,
	[SourceBranch] varchar(256),
	[CreationDate]  DATETIME NULL

	 PRIMARY KEY([VSO], [Project], [BuildId])
)

GO;

CREATE INDEX OfficialBuild_Index_Result ON dbo.[OfficialBuild] ([Result]);