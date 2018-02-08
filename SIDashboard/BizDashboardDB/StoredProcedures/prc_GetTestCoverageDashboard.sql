CREATE PROCEDURE [dbo].[prc_GetTestCoverageDashboard]
AS
	DECLARE @now DATE = GETDATE();
	DECLARE @currYear INT = YEAR(@now);
	DECLARE @currMonth INT = MONTH(@now);
	DECLARE @prevMonthDate DATE = DATEADD(MONTH, -1, @now)

	DECLARE @prevMonthYear INT = YEAR(@prevMonthDate);
	DECLARE @prevMonthMonth INT = MONTH(@prevMonthDate);

	DECLARE @prevMonthStart DATE = DATEFROMPARTS(@prevMonthYear,@prevMonthMonth,1)
	
	;WITH AllTestCoverage AS
	(
		SELECT T.BuildId AS BuildId,
			   B.CreationDate AS CreationDate,
			   SUM(T.LinesCovered) AS LinesCovered,
			   SUM(T.LinesNotCovered) AS LinesNotCovered,
			   SUM(T.LinesCovered) / CAST(SUM(T.LinesNotCovered)+SUM(T.LinesCovered) AS FLOAT) AS Ratio
		FROM dbo.TestCoverage AS T
			JOIN dbo.OfficialBuild AS B ON T.BuildId = B.BuildId AND T.VSO = B.VSO AND T.Project = B.Project
		WHERE ModuleName LIKE 'microsoft%'
		GROUP BY  T.VSO, T.Project, T.BuildId, YEAR(CreationDate), MONTH(CreationDate), B.CreationDate
	),
	CurrMonthTestCoverage AS
	(
		SELECT SUM(A.Ratio) / CAST(COUNT(*) AS FLOAT) AS AvgTestCoverage
		FROM AllTestCoverage AS A
		WHERE YEAR(A.CreationDate) = @currYear AND MONTH(A.CreationDate) = @currMonth
	),
	PrevMonthTestCoverage AS
	(
		SELECT SUM(A.Ratio) / CAST(COUNT(*) AS FLOAT) AS AvgTestCoverage
		FROM AllTestCoverage AS A
		WHERE YEAR(A.CreationDate) = @prevMonthYear AND MONTH(A.CreationDate) = @prevMonthMonth
	),
	OldMonthTestCoverage AS
	(
		SELECT SUM(A.Ratio) / CAST(COUNT(*) AS FLOAT) AS AvgTestCoverage
		FROM AllTestCoverage AS A
		WHERE A.CreationDate < @prevMonthStart
	)	
	SELECT 'N-2Month' AS DateLabel, * FROM OldMonthTestCoverage UNION
	SELECT 'PrevMonth' AS DateLabel, * FROM PrevMonthTestCoverage UNION 
	SELECT 'CurrMonth' AS DateLabel, * FROM CurrMonthTestCoverage 
GO