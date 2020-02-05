using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FxStreetDeveloper.API.Models
{
    public static partial class V1
    {
        public class ManagerResponse : ManagerRequest
        {
            public int Id { get; set; }
        }
    }
}
