CREATE TYPE dbo.PullRequestParamType AS TABLE 
(
	[VSO] varchar(32) NOT NULL,
	[Project] varchar(32) NOT NULL,
    [Repository] varchar(128) NOT NULL,
	[PRId] INT NOT NULL,	
	[CountOfCommentNotFixed] INT DEFAULT 0,
	[CreationDate]  DATETIME NULL
)

GO;

CREATE PROC [dbo].[prc_MergePullRequest]
(
	@newPullRequestInfo AS dbo.PullRequestParamType READONLY
)
AS
BEGIN
	MERGE dbo.PullRequest AS TARGET  
    USING @newPullRequestInfo AS SOURCE
        ON (TARGET.VSO = SOURCE.VSO AND TARGET.Project = SOURCE.Project AND TARGET.PRId = SOURCE.PRId AND TARGET.Repository LIKE SOURCE.Repository )  
    WHEN MATCHED THEN   
        UPDATE SET TARGET.CountOfCommentNotFixed = SOURCE.CountOfCommentNotFixed,
				   TARGET.CreationDate = SOURCE.CreationDate
    WHEN NOT MATCHED THEN  
        INSERT (VSO, Project, Repository, PRId, CountOfCommentNotFixed, CreationDate)  
        VALUES (SOURCE.VSO, SOURCE.Project, SOURCE.Repository, SOURCE.PRId, SOURCE.CountOfCommentNotFixed, SOURCE.CreationDate);
END

GO;