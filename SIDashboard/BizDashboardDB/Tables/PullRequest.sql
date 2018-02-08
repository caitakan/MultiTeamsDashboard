CREATE TABLE [dbo].[PullRequest]
(
	[VSO] varchar(32) NOT NULL,
	[Project] varchar(32) NOT NULL,
	[Repository] varchar(128) NOT NULL,
	[PRId] INT NOT NULL,	
	[CountOfCommentNotFixed] INT DEFAULT 0,
	[CreationDate]  DATETIME NULL,
    PRIMARY KEY ([VSO], [Project], [Repository], [PRId])
)
