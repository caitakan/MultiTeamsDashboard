CREATE PROCEDURE [dbo].[prc_GetAlertErrorDashboard]
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
	DECLARE @numDaysInOldMonth FLOAT = DATEDIFF(DAY, @minStart, @prevMonthStart);

	WITH OldAlert AS
	(
		SELECT COUNT(*) / @numDaysInOldMonth AS AvgAlertPerDay
		FROM dbo.VSOWorkItem			
		WHERE Tags = 'Critical Error'
			AND CreatedDate < @prevMonthStart
	),
	PrevAlert AS
	(
		SELECT COUNT(*) / @numDaysInPrevMonth AS AvgAlertPerDay
		FROM dbo.VSOWorkItem		
		WHERE Tags = 'Critical Error'
			AND CreatedDate >= @prevMonthStart AND CreatedDate < @currMonthStart
	),
	CurrAlert AS
	(
		SELECT COUNT(*) / @numDaysInCurrMonth AS AvgAlertPerDay
		FROM dbo.VSOWorkItem		
		WHERE Tags = 'Critical Error'
			AND CreatedDate >= @currMonthStart
	)
	SELECT 'N-2Month' AS DateLabel, * FROM OldAlert UNION
	SELECT 'PrevMonth' AS DateLabel, * FROM PrevAlert UNION 
	SELECT 'CurrMonth' AS DateLabel, * FROM CurrAlert 

GO
