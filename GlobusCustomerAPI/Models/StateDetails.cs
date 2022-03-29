using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlobusCustomerAPI.Models
{
    public class data
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string alias { get; set; }
    }
    public class StateDetails
    {
        public List<data> data { get; set; }
    }
}
