﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Models.ViewModels.Comment
{
   public class CommentViewModel
    {
        public int CommentID { get; set; }
        public int UserID { get; set; }
        public string CommentMsg { get; set; }
        public string KPILevelCode { get; set; }
        public DateTime CommentedDate { get; set; }
        public string Period { get; set; }
        public string FullName { get; set; }
        public bool? Read { get; set; }
        public bool? IsHasTask { get; set; }
        public bool Task { get; set; }
        public bool Pin { get; set; }
        public bool IsOwner { get; set; }
    }
}
