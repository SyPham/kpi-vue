﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Models.ViewModels.ActionPlan
{
  public class ActionPlanParams
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int DataID { get; set; }
        public int CommentID { get; set; }

        public string Title { get; set; }
        public string KPILevelCodeAndPeriod { get; set; }
        public string Description { get; set; }
        public string Tag { get; set; }

        public string Deadline { get; set; }
        public string SubmitDate { get; set; }
        public string Subject { get; set; }
        public string Link { get; set; }
        public string DefaultLink { get; set; }
        public string Auditor { get; set; }
        public int CategoryID { get; set; }
        public string KPILevelCode { get; set; }
        public int KPILevelID { get; set; }
        public int OwnerID { get; set; }
        public string FileName { get; set; }
    }
}
