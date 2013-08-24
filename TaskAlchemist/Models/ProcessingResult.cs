using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskAlchemist.Models
{
    public class ProcessingResult
    {

        public string Message { get; set; } 
        public string Description { get; set; } 
        public string AlertType { get; set; } 
        public long TimeElapsed { get; set; } 
        public string MethodType { get; set; } 
        public int ThreadsUsed { get; set; }

        public string Result { get; set; } 

    }
}