
namespace FxStreetDeveloper.API.Models
{
    public static partial class V1
    {
        public class ManagerRequest
        {
            public string Name { get; set; }
            public string TeamName { get; set; }
            public int YellowCards { get; set; }
            public int RedCards { get; set; }
        }
    }
}
