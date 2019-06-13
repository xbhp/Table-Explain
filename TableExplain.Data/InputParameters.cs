using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace IP_Boss.Dss.Data
{
    public class InputParameters
    {
        [JsonProperty("s_date")]
        public DateTime? StartTime { get; set; }

        [JsonProperty("e_date")]
        public DateTime? EndTime { get; set; }

        [JsonProperty("level")]
        public int? ReportLevel { get; set; }

        [JsonProperty("common_id")]
        public int? CommonID { get; set; }

        [JsonProperty("IsAggregate")]
        public int? IsAggregate { get; set; }


        [JsonProperty("DepartmentID")]
        public int? DepartmentID { get; set; }

        [JsonProperty("UserID")]
        public int? UserID { get; set; }

        [JsonProperty("CallType")]
        public int? CallType { get; set; }
        [JsonProperty("FunnelRow")]
        public string  FunnelRow{ get; set; }

        [JsonProperty("ROISearchType")]
        public string ROISearchType { get; set; }

        [JsonProperty("btid")]
        public int BusinessLevel { get; set; }

        [JsonProperty("InfoLevel")]
        public int? InfoLevel { get; set; }
        [JsonProperty("SourceId")]
        public int? SourceId { get; set; }
    }
}
