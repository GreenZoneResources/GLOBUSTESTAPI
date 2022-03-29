using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlobusCustomerAPI.Models
{
    public class datas
    {
        public int Id { get; set; }
        public string name { get; set; }
    }
    public class LGADetails
    {
            public List<datas> data { get; set; }
        
    }
}
