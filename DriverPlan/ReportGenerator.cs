using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using DriverPlan.viewmodel;

namespace DriverPlan
{
    static class ReportGenerator
    {
        public static IEnumerable<string> CreateReport(IEnumerable<DriverPlanEntryViewModel> _Entries)
        {
            var hEntriesByDate = new Dictionary<DateTime, List<DriverPlanEntryViewModel>>();
            
            foreach (var hEntry in _Entries)
            {
                if (hEntriesByDate.ContainsKey(hEntry.DeliveryDate))
                {
                    hEntriesByDate[hEntry.DeliveryDate].Add(hEntry);
                }
                else
                {
                    hEntriesByDate.Add(hEntry.DeliveryDate, new List<DriverPlanEntryViewModel>() {hEntry});
                }
            }

            var hListReport = new List<string>();
            foreach (var hEntries in hEntriesByDate)
            {
                hListReport.Add(hEntries.Key.ToLongDateString() + " " + hEntries.Key.ToLongTimeString());
                hListReport.AddRange(hEntries.Value.Select(_ => _.Driver ));
            }

            return hListReport;
        }
    }
}