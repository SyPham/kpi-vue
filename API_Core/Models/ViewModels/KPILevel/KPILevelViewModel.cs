﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Models.ViewModels.KPILevel
{
   public class KPILevelViewModel
    {
        public int ID { get; set; }
        public string KPILevelCode { get; set; }
        public string UserCheck { get; set; }
        public int KPIID { get; set; }
        public int LevelID { get; set; }
        public string Period { get; set; }

        public string Lang { get; set; }
        public string Name { get; set; }
        public string Kind { get; set; }

        public int? Weekly { get; set; }
        public DateTime? Monthly { get; set; }
        public DateTime? Quarterly { get; set; }
        public DateTime? HalfYear { get; set; }
        public DateTime? Yearly { get; set; }

        public bool? Checked { get; set; }
        //public bool? IsConversion { get; set; }
        public bool? WeeklyChecked { get; set; }
        public bool? MonthlyChecked { get; set; }
        public bool? HalfYearChecked { get; set; }
        public bool? QuarterlyChecked { get; set; }
        public bool? YearlyChecked { get; set; }
        public bool? CheckedPeriod { get; set; }

        public DateTime? TimeCheck { get; set; }

        public DateTime? CreateTime { get; set; }

        public int CategoryID { get; set; }
        public string KPIName { get; set; }
        public string KPICode { get; set; }
        public int LevelNumber { get; set; }
        public string LevelCode { get; set; }

        public bool StatusUploadDataW { get; set; }
        public bool StatusUploadDataM { get; set; }
        public bool StatusUploadDataH { get; set; }
        public bool StatusUploadDataQ { get; set; }
        public bool StatusUploadDataY { get; set; }

        public bool StatusEmptyDataW { get; set; }
        public bool StatusEmptyDataM { get; set; }
        public bool StatusEmptyDataH { get; set; }
        public bool StatusEmptyDataQ { get; set; }
        public bool StatusEmptyDataY { get; set; }

        public string Unit { get; set; }

        public bool CheckCategory { get; set; }
        public string ModifyBy { get; set; }

    }
}
