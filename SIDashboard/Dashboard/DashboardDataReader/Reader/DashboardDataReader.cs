using Dashboard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dashboard.DashboardDataReader
{
    public interface IDashboardDataReader
    {
        DashboardData read();
    }
}