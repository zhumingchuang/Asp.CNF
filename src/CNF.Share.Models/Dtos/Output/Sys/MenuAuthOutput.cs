using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNF.Share.Models.Dtos.Output.Sys
{
    public class MenuAuthOutput
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string NameCode { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string HttpMethod { get; set; }
        public string[] BtnCodeIds { get; set; }
        public string BtnCodeName { get; set; }
    }
}
