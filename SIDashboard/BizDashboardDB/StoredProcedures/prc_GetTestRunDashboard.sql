CREATE PROCEDURE [dbo].[prc_GetTestRunDashboard]
AS
	DECLARE @now DATE = GETDATE();
	DECLARE @currYear INT = YEAR(@now);
	DECLARE @currMonth INT = MONTH(@now);
	DECLARE @currMonthStart DATE = DATEFROMPARTS(@currYear, @currMonth, 1)
	DECLARE @numDaysInCurrMonth FLOAT = DATEDIFF(DAY, @currMonthStart, @now) + 1
	
	DECLARE @prevMonthDate DATE = DATEADD(MONTH, -1, @now)
	DECLARE @prevMonthYear INT = YEAR(@prevMonthDate);
	DECLARE @prevMonthMonth INT = MONTH(@prevMonthDate);
	DECLARE @prevMonthStart DATE = DATEFROMPARTS(@prevMonthYear,@prevMonthMonth,1)
	DECLARE @numDaysInPrevMonth FLOAT = DATEDIFF(DAY, @prevMonthStart, @currMonthStart)
		
	DECLARE @minStart DATE;
	SELECT @minStart = MIN(CreationDate) FROM TestRun
	DECLARE @numDaysInOldMonth FLOAT = DATEDIFF(DAY, @minStart, @prevMonthStart);

	WITH AllTestRun AS
	(
		SELECT BuildOrReleaseId, SUM(TotalTestNum) AS TotalTestNum, MIN(CreationDate) AS CreationDate
		FROM TestRun
		WHERE TriggeredBy = 'Build'
		GROUP BY BuildOrReleaseId
	),
	OldTestRun AS
	(
		SELECT AVG(TotalTestNum) As AvgTestCount
		FROM AllTestRun
		WHERE CreationDate < @prevMonthStart
	),
	PrevTestRun AS
	(
		SELECT AVG(TotalTestNum) As AvgTestCount
		FROM AllTestRun
		WHERE CreationDate >= @prevMonthStart AND CreationDate < @currMonthStart
	),
	CurrTestRun AS
	(
		SELECT AVG(TotalTestNum) As AvgTestCount
		FROM AllTestRun
		WHERE CreationDate >= @currMonthStart
	)	
	SELECT 'N-2Month' AS DateLabel, * FROM OldTestRun UNION
	SELECT 'PrevMonth' AS DateLabel, * FROM PrevTestRun UNION 
	SELECT 'CurrMonth' AS DateLabel, * FROM CurrTestRun 
GO
