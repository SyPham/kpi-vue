﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace ProModes
{
    public partial class Comments
    {
        public int Id { get; set; }
        public string CommentMsg { get; set; }
        public DateTime CommentedDate { get; set; }
        public int UserId { get; set; }
        public int DataId { get; set; }
        public string Link { get; set; }
        public string Title { get; set; }
    }
}