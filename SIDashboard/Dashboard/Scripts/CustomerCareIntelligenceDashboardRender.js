var renderTable = function (config) {
    var categoriesCount = config.length;
    var tableElt = $("#result");
    for (var i = 0; i < categoriesCount; ++i) {
        var categoryConfig = dashboardMetricConfig[i]
        var metricsCount = categoryConfig.metrics.length;
        for (var j = 0; j < metricsCount; ++j) {
            var metricItem = categoryConfig.metrics[j];
            var rowElt = $("<tr></tr>");

            // Add category column
            if (j === 0) {
                var categoryColumn = $("<td></td>");
                categoryColumn.attr("rowspan", metricsCount);
                categoryColumn.attr("class", "categoryColumn");
                categoryColumn.text(categoryConfig.category);
                rowElt.append(categoryColumn)
            }

            // Add metric column
            var metricColumn = $("<td></td>");
            metricColumn.text(metricItem.metric);
            metricColumn.attr("title", metricItem.description);
            rowElt.append(metricColumn);

            // Add target setting column
            var goodColumn = $("<td></td>");
            goodColumn.text(metricItem.target.good);
            rowElt.append(goodColumn);

            var warningColumn = $("<td></td>");
            warningColumn.text(metricItem.target.yellow);
            rowElt.append(warningColumn);

            var failColumn = $("<td></td>");
            failColumn.text(metricItem.target.fail);
            rowElt.append(failColumn);

            // Add metric volume column
            var valueColumnCurrMonth = $("<td></td>");
            valueColumnCurrMonth.attr("id", metricItem.idPrefix + "CurrentMonth");
            rowElt.append(valueColumnCurrMonth);

            var valueColumnPrevMonth = $("<td></td>");
            valueColumnPrevMonth.attr("id", metricItem.idPrefix + "PreviousMonth");
            rowElt.append(valueColumnPrevMonth);

            var valueColumnOldMonth = $("<td></td>");
            valueColumnOldMonth.attr("id", metricItem.idPrefix + "OldMonth");
            rowElt.append(valueColumnOldMonth);

            // Add row to table
            tableElt.append(rowElt);
        }
    }
};

$(document).ready(
    function () {
        renderTable(window["CustomerCareIntelligenceDashboardConfig"]);
        var alertClass = ['NA', 'fail', 'warning', 'good'];

        var mapping = {
            "Code reviewed and signed off": "CodeReviewedAndSignedOff",
            "Official Build Failures": "OfficialBuildFailures",
            "Build Verfication Pass Rate": "BVTPassRate",
            "Service Up time - Ping Health": "ServiceUptime",
            "Test Coverage C#": "CodeCoverageWithTests",
            "Latency": "Latency",
            "End-To-End Response Time": "E2EResponseTime",
            "Throughput": "Throughput",
            "Critical Errors": "CriticalErrors",
            "Test Cases Identified & Enabled": "TestCasesIdentifiedEnabled",
            "Feature Verification Test Pass Rate": "FeatureVerificationTestPassRate",
            "Customer Incidents": "CustomerIncidents",
            "Number of Sev A Incidents": "NumOfSevAIncidents"
        };

        $.get('/api/metricsummary?SolutionName=SalesIntelligence', function (response) {
            $('#loading').css("background-color", "#92D050").text("Last updated on " + new Date());

            $.each(response, function (i, data) {
                var tableRowIdPrefix = mapping[data.MetricName];

                var currMonthId = tableRowIdPrefix + "CurrentMonth";
                var currMonthClass = alertClass[data.CurrentMonthAlertLevel];

                var prevMonthId = tableRowIdPrefix + "PreviousMonth";
                var prevMonthClass = alertClass[data.PreviousMonthAlertLevel];

                var oldMonthId = tableRowIdPrefix + "OldMonth";
                var oldMonthClass = alertClass[data.OlderMonthAlertLevel];

                $('#' + currMonthId).addClass(currMonthClass).text(data.CurrentMonthValue);
                $('#' + prevMonthId).addClass(prevMonthClass).text(data.PreviousMonthValue);
                $('#' + oldMonthId).addClass(oldMonthClass).text(data.OlderMonthValue);
            });

            // Arrange for the dashboard to be refreshed with the latest metrics.
            setTimeout(
                function () { window.location.href = window.location.href; },
                window["CustomerCareIntelligenceDashboardRefreshInterval"]);
        }, 'json');
    }
);