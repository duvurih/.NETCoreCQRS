using System;

namespace Dot.Net.Core.Common.DTO
{
    public class ObservationDTO : BastDTO
    {
        public int Id { get; set; }
        public string Discussion { get; set; }
        public DateTime DiscussionDate { get; set; }
        public string DiscussionLocation { get; set; }
        public string DiscussionWith { get; set; }
        public string Subject { get; set; }
        public string Outcome { get; set; }
        public string RecordedBy { get; set; }
        public DateTime RecordedDate { get; set; }
    }
}
