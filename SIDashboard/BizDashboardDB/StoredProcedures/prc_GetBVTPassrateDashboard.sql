CREATE PROCEDURE [dbo].[prc_GetBVTPassrateDashboard]
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
	SELECT @minStart = MIN(CreatedDate) FROM VSOWorkItem
	DECLARE @numMonthInOldMonth FLOAT = DATEDIFF(MONTH, @minStart, @prevMonthStart);


	DECLARE @Trigger VARCHAR(32) = 'Build';

	WITH OldBVT AS
	(
		SELECT AVG ( PassedTestNum / CAST ( TotalTestNum AS FLOAT ) ) AS Passrate
		FROM dbo.TestRun
		WHERE TriggeredBy = @Trigger
			AND CreationDate < @prevMonthStart
	),
	PrevBVT AS
	(
		SELECT AVG ( PassedTestNum / CAST ( TotalTestNum AS FLOAT ) ) AS Passrate
		FROM dbo.TestRun		
		WHERE TriggeredBy = @Trigger
			AND CreationDate >= @prevMonthStart AND CreationDate < @currMonthStart
	),
	CurrBVT AS
	(
		SELECT AVG ( PassedTestNum / CAST ( TotalTestNum AS FLOAT ) ) AS Passrate
		FROM dbo.TestRun		
		WHERE TriggeredBy = @Trigger
			AND CreationDate >= @currMonthStart
	)
	SELECT 'N-2Month' AS DateLabel, * FROM OldBVT UNION
	SELECT 'PrevMonth' AS DateLabel, * FROM PrevBVT UNION 
	SELECT 'CurrMonth' AS DateLabel, * FROM CurrBVT 
GO
