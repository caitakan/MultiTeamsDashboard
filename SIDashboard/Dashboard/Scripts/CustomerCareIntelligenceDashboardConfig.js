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
                "metric": "Number of Dependency Incidents",
                "idPrefix": "NumOfDependencyIncidents",
                "description": "Measure incidents against dependency components, per dependency team",
                "target": {
                    "good": "<= 1 / month",
                    "yellow": "2 / month",
                    "fail": "> 2 / month"
                }
            },
            {
                "metric": "Mean time to Detect",
                "idPrefix": "MeanTimeToDetect",
                "description": "Time measured from occurrence of incident to detection by our engineering team",
                "target": {
                    "good": "<= 5 min",
                    "yellow": "5 - 15 min",
                    "fail": "> 15 min"
                }
            },
            {
                "metric": "Mean time to Recovery",
                "idPrefix": "MeanTimeToRecovery",
                "description": "Time measured from occurrence to resolution of the incident (not necessarily long-term fix)",
                "target": {
                    "good": "<= 15 min",
                    "yellow": "15 - 30 min",
                    "fail": "> 30 min"
                }
            },
            {
                "metric": "Mean time to Root Cause Sev A",
                "idPrefix": "MeanTimeToRootCauseSevA",
                "description": "Time measured from occurrence to root causing the issue (may not necessarily gate recovery)",
                "target": {
                    "good": "4 hours",
                    "yellow": "4 hours - 1 day",
                    "fail": "> 1 days"
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
        "category": "Service Certification",
        "metrics": [
            {
                "metric": "Globalization Enabled",
                "idPrefix": "GlobalizationEnabled",
                "description": "Yes/No measure indicating whether the service is globalized across the board",
                "target": {
                    "good": "Yes",
                    "yellow": "",
                    "fail": "No"
                }
            },
            {
                "metric": "Localized Languages Enabled",
                "idPrefix": "LocalizedLanguagesEnabled",
                "description": "Absolute number of languages the service is available in",
                "target": {
                    "good": "Absolute #",
                    "yellow": "",
                    "fail": ""
                }
            },
            {
                "metric": "Microsoft Accessibility Standard",
                "idPrefix": "MicrosoftAccessibilityStandard",
                "description": "The standardized Microsoft Accessibility Standard (MAS) A/B/C/D/F grade",
                "target": {
                    "good": "B",
                    "yellow": "C",
                    "fail": "D,F"
                }
            },
            {
                "metric": "Compliance Certification",
                "idPrefix": "ComplianceCertification",
                "description": "Percentage of tasks identified by 1CS certification that are completed",
                "target": {
                    "good": "100% 1CS done",
                    "yellow": "",
                    "fail": "< 100% 1CS done"
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
            {
                "metric": "Acceptance Test Pass Rate",
                "idPrefix": "AcceptanceTestPassRate",
                "description": "Acceptance tests are the set of tests that certify full functionality and are the most comprehensive set",
                "target": {
                    "good": "100%",
                    "yellow": "80% - 100%",
                    "fail": "< 80%"
                }
            },
            {
                "metric": "Automated vs Manual Processes",
                "idPrefix": "AutomatedvsManualProcesses",
                "description": "Percentage of processes from commit to deployment that require no manual intervention",
                "target": {
                    "good": "100%",
                    "yellow": "98% - 100%",
                    "fail": "< 98%"
                }
            },
        ]
    },
    {
        "category": "Deployment",
        "metrics": [
            {
                "metric": "Check-In Velocity",
                "idPrefix": "Check-InVelocity",
                "description": "Average time between code commit and deployment to an environment",
                "target": {
                    "good": "<= 1 week",
                    "yellow": "1 - 2 weeks",
                    "fail": "> 2 weeks"
                }
            },
            {
                "metric": "Live Site Verification Pass Rate",
                "idPrefix": "LiveSiteVerificationPassRate",
                "description": "Tests that validate deployment is complete and accurate",
                "target": {
                    "good": "100%",
                    "yellow": "",
                    "fail": "< 100%"
                }
            },
            {
                "metric": "Stale Customer Deployments",
                "idPrefix": "StaleCustomerDeployments",
                "description": "Number of customers who are running builds older than a certain age",
                "target": {
                    "good": "<= 1 > 2 week old",
                    "yellow": "1 – 2 > 2 week old",
                    "fail": "> 2 > 2 week old"
                }
            }
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

window["CustomerCareIntelligenceDashboardConfig"] = dashboardMetricConfig;
window["CustomerCareIntelligenceDashboardRefreshInterval"] = 900000;  //  Fifteen minutes.
