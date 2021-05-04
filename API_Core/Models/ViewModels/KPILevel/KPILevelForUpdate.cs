﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Models.ViewModels.KPILevel
{
   public class KPILevelForUpdate
    {

        public int ID { get; set; }
        public string KPILevelCode { get; set; }
        public string UserCheck { get; set; }
        public int KPIID { get; set; }
        public int LevelID { get; set; }
        public int? TeamID { get; set; }
        public string Period { get; set; }

        public int? Weekly { get; set; }
        public string Monthly { get; set; }
        public string Target { get; set; }
        public string HalfYear { get; set; }
        public string Score { get; set; }
        public string Quarterly { get; set; }
        public string Yearly { get; set; }

        public bool? Checked { get; set; }
        //public bool? IsConversion { get; set; }

        public bool? WeeklyChecked { get; set; }
        public bool? MonthlyChecked { get; set; }
        public bool? HalfYearChecked { get; set; }
        public bool? QuarterlyChecked { get; set; }
        public bool? YearlyChecked { get; set; }

        public bool? CheckedPeriod { get; set; }

        public bool? WeeklyPublic { get; set; }
        public bool? MonthlyPublic { get; set; }
        public bool? HalfYearPublic { get; set; }
        public bool? QuarterlyPublic { get; set; }
        public bool? YearlyPublic { get; set; }

        public DateTime? TimeCheck { get; set; }
        public DateTime CreateTime { get; set; }
        public int LevelNumber { get; set; }

        public int WeeklyStandard { get; set; }
        public int MonthlyStandard { get; set; }
        public int HalfYearStandard { get; set; }
        public int QuarterlyStandard { get; set; }
        public int YearlyStandard { get; set; }

        public int WeeklyTarget { get; set; }
        public int MonthlyTarget { get; set; }
        public int HalfYearTarget { get; set; }
        public int QuarterlyTarget { get; set; }
        public int YearlyTarget { get; set; }


        public string ModifyBy { get; set; }
    }
}
