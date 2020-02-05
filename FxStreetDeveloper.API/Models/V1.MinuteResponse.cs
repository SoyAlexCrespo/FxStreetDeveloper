using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FxStreetDeveloper.API.Models
{
    public static partial class V1
    {
        public class MinuteResponse
        {
            public string Name { get; set; }
            public int Id { get; set; }            
            public int Total { get; set; }
        }
    }
}
