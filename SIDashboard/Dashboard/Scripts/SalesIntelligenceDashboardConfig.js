var dashboardMetricConfig = [
    {
        "category": "Service Availability",
        "metrics": [
            {
                "metric": "Service Uptime",
                "idPrefix": "ServiceUptime",
                "description": "Percentage of time the service has been available to customers – 99.9 is < 45 min down per month",
                "target": {
                    "good": "99.9%",
                    "yellow": "99.8%",
                    "fail": "< 99.8%"
                }
            },
            {
                "metric": "Number of Customers out of SLA",
                "idPrefix": "NumOfCustomersOutOfSLA",
                "description": "Measures availability on a per customer basis to catch per customer outliers",
                "target": {
                    "good": "0",
                    "yellow": "1 - 2",
                    "fail": "> 2"
                }
            },
            {
                "metric": "Number of Sev A Incidents",
                "idPrefix": "NumOfSevAIncidents",
                "description": "Sev A/B/C measures are Azure and Office standards and more stringent than just outages",
                "target": {
                    "good": "<= 1 / month",
                    "yellow": "2 / month",
                    "fail": "> 2 / month"
                }
            },            
            {
                "metric": "Mean time to Fix Sev A",
                "idPrefix": "MeanTimeToFixSevA",
                "description": "Time measured from occurrence to fixing the root cause of the issue",
                "target": {
                    "good": "1 day",
                    "yellow": "1 - 2 days",
                    "fail": "> 2 days"
                }
            },
            {
                "metric": "Mean time to Deploy Sev A",
                "idPrefix": "MeanTimeToDeploySevA",
                "description": "Time measured from occurrence to deploying long term fix",
                "target": {
                    "good": "2 days",
                    "yellow": "2 - 3 days",
                    "fail": "> 3 days"
                }
            }
        ]
    },
    {
        "category": "Service Reliability",
        "metrics": [
            {
                "metric": "Customer Incidents",
                "idPrefix": "CustomerIncidents",
                "description": "Any incident causing disruption to customer usage",
                "target": {
                    "good": "<= 1 / month",
                    "yellow": "2 / month",
                    "fail": "> 2 / month"
                }
            },
            {
                "metric": "Critical Errors",
                "idPrefix": "CriticalErrors",
                "description": "Any errors critical to functioning of the service, does not need to have been noticed by the customer",
                "target": {
                    "good": "0 / day",
                    "yellow": "2 / day",
                    "fail": "> 2 / day"
                }
            }
        ]
    },    
    {
        "category": "Development",
        "metrics": [
            {
                "metric": "Code reviewed and signed off",
                "idPrefix": "CodeReviewedAndSignedOff",
                "description": "Number of code changes reviewed and committed with no outstanding unresolved comments",
                "target": {
                    "good": "100%",
                    "yellow": "98% - 100%",
                    "fail": "< 98%"
                }
            },
            {
                "metric": "Official Build Failures",
                "idPrefix": "OfficialBuildFailures",
                "description": "Failures of any kind requiring manual intervention in the official build environment",
                "target": {
                    "good": "0",
                    "yellow": "1",
                    "fail": "> 1"
                }
            }
        ]
    },
    {
        "category": "Validation",
        "metrics": [
            {
                "metric": "Build Verification Test Pass Rate",
                "idPrefix": "BVTPassRate",
                "description": "Build verification tests are the minimal set of tests indicating a viable build",
                "target": {
                    "good": "100%",
                    "yellow": "98% - 100%",
                    "fail": "< 98%"
                }
            },
            {
                "metric": "Feature Verification Test Pass Rate",
                "idPrefix": "FeatureVerificationTestPassRate",
                "description": "Feature verification tests are the set of test validating all major functionality in the product",
                "target": {
                    "good": "100%",
                    "yellow": "98% - 100%",
                    "fail": "< 98%"
                }
            },
            {
                "metric": "Code Coverage with Tests",
                "idPrefix": "CodeCoverageWithTests",
                "description": "Percentage of the code that is exercised by all validation tests",
                "target": {
                    "good": ">= 85%",
                    "yellow": "75% - 85%",
                    "fail": "< 75%"
                }
            },
            {
                "metric": "Test Cases Identified & Enabled",
                "idPrefix": "TestCasesIdentifiedEnabled",
                "description": "Absolute number of test cases identified and absolute number enabled and gating changes",
                "target": {
                    "good": "Absolute #/#",
                    "yellow": "",
                    "fail": ""
                }
            },            
        ]
    },
    
    {
        "category": "Service Responsiveness",
        "metrics": [
            {
                "metric": "Latency",
                "idPrefix": "Latency",
                "description": "Time measured from customer client request to first response to customer",
                "target": {
                    "good": "250 ms",
                    "yellow": "500 ms",
                    "fail": "> 500 ms"
                }
            },
            {
                "metric": "End-To-End Response Time",
                "idPrefix": "E2EResponseTime",
                "description": "Time measured from customer client request to completion of servicing the request",
                "target": {
                    "good": "2s",
                    "yellow": "2 - 5s",
                    "fail": "> 5s"
                }
            },
            {
                "metric": "Throughput",
                "idPrefix": "Throughput",
                "description": "Concurrent requests the service is able to handle per second",
                "target": {
                    "good": "100 rps",
                    "yellow": "80-100 rps",
                    "fail": "< 80rps"
                }
            }
        ]
    },
];

window["SalesIntelligenceDashboardConfig"]          = dashboardMetricConfig;
window["SalesIntelligenceDashboardRefreshInterval"] = 900000;  //  Fifteen minutes.
