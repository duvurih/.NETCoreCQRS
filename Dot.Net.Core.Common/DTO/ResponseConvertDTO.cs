using System;
using System.Collections.Generic;

namespace Dot.Net.Core.Common.DTO
{
    public class ResponseConvertDTO
    {
        public bool Success { get; set; }
        public string Timestamp { get; set; }
        public string Base { get; set; }
        public Dictionary<string, string> Rates { get; set; }
        public Dictionary<string, string> Query { get; set; }
        public Dictionary<string, string> Info { get; set; }
        public string Historical { get; set; }
        public DateTime Date { get; set; }
        public double Result { get; set; }
    }
}
