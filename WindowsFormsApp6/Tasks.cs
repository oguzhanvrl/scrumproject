//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WindowsFormsApp6
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tasks
    {
        public int task_id { get; set; }
        public Nullable<int> story_id { get; set; }
        public Nullable<int> worker_id { get; set; }
        public string taskName { get; set; }
        public string details { get; set; }
        public Nullable<System.DateTime> commitmentDate { get; set; }
        public Nullable<System.DateTime> endingDate { get; set; }
        public string state { get; set; }
        public Nullable<int> deadline { get; set; }
    }
}