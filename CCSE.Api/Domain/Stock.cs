using System;

namespace CCSE.Api.Domain
{
    public class Stock
    {

        public int id { get; set; }
        public string name { get; set; }
        public string currency { get; set; }
        public string symbol  { get; set; }
        public DateTime createDate { get; set; }

        public bool isActive { get; set; }



    }
}
