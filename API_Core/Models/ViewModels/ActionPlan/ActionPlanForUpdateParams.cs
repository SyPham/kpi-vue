﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Models.ViewModels.ActionPlan
{
   public class ActionPlanForUpdateParams
    {
        public int ID { get; set; }
        public string Title { get; set; }

        public string DeadLine { get; set; }

        public string Description { get; set; }

        public string Tag { get; set; }
    }
}
