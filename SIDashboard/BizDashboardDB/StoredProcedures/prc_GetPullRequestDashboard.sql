
CREATE PROCEDURE [dbo].[prc_GetPullRequestDashboard]
AS
	DECLARE @now DATE = GETDATE();
	DECLARE @currYear INT = YEAR(@now);
	DECLARE @currMonth INT = MONTH(@now);
	DECLARE @prevMonthDate DATE = DATEADD(MONTH, -1, @now)

	DECLARE @prevMonthYear INT = YEAR(@prevMonthDate);
	DECLARE @prevMonthMonth INT = MONTH(@prevMonthDate);

	DECLARE @prevMonthStart DATE = DATEFROMPARTS(@prevMonthYear,@prevMonthMonth,1)
	
	;WITH AllPR AS
	(
		SELECT YEAR(CreationDate) AS [Year],
			   MONTH(CreationDate) AS [Month],
			   COUNT(*) AS [AllPRCount]
		FROM dbo.PullRequest
		GROUP BY  YEAR(CreationDate), MONTH(CreationDate)
	),
	GoodPR AS
	(
		SELECT YEAR(CreationDate) AS [Year],
			   MONTH(CreationDate) AS [Month],
			   COUNT(*) AS [GoodPRCount]
		FROM dbo.PullRequest
		WHERE CountOfCommentNotFixed <= 0
		GROUP BY  YEAR(CreationDate), MONTH(CreationDate)
	),
	CurrMonthData AS
	(
		SELECT	A.Year, 
				A.Month,
				A.AllPRCount,
				B.GoodPRCount
		FROM AllPR AS A
			LEFT JOIN GoodPR AS B ON A.Year = B.Year and A.Month = B.Month
		WHERE A.year = @currYear AND A.Month = @currMonth
	),
	PrevMonthData AS
	(
		SELECT	A.Year, 
				A.Month,
				A.AllPRCount,
				B.GoodPRCount
		FROM AllPR AS A
			LEFT JOIN GoodPR AS B ON A.Year = B.Year and A.Month = B.Month
		WHERE A.year = @prevMonthYear AND A.Month = @prevMonthMonth
	),
	OlderMonthAllPRData AS
	(
		SELECT YEAR(CreationDate) AS [Year],
			   MONTH(CreationDate) AS [Month],
			   COUNT(*) AS [AllPRCount]
		FROM dbo.PullRequest
		WHERE CreationDate < @prevMonthStart
		GROUP BY  YEAR(CreationDate), MONTH(CreationDate)
	),
	OlderMonthGoodPRData AS
	(
		SELECT YEAR(CreationDate) AS [Year],
			   MONTH(CreationDate) AS [Month],
			   COUNT(*) AS [GoodPRCount]
		FROM dbo.PullRequest
		WHERE CreationDate < @prevMonthStart
				AND CountOfCommentNotFixed <= 0
		GROUP BY  YEAR(CreationDate), MONTH(CreationDate)
	),
	OlderMonthData AS
	(
		SELECT	A.Year, 
				A.Month,
				A.AllPRCount,
				B.GoodPRCount
		FROM OlderMonthAllPRData AS A
			LEFT JOIN OlderMonthGoodPRData AS B ON A.Year = B.Year and A.Month = B.Month
	),
	OlderMonthDataAgg AS
	(
		SELECT	-1 AS Year, 
				-1 AS Month,
				SUM(AllPRCount) AS AllPRCount,
				SUM(GoodPRCount) AS GoodPRCount
		FROM OlderMonthData
	)
	SELECT 'N-2Month' AS DateLabel, * FROM OlderMonthDataAgg UNION
	SELECT 'PrevMonth' AS DateLabel,* FROM PrevMonthData UNION 
	SELECT 'CurrMonth' AS DateLabel, * FROM CurrMonthData

GO
