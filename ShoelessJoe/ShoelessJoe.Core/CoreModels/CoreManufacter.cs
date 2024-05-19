using ShoelessJoe.Core.CoreModels.PartialModels;

namespace ShoelessJoe.Core.CoreModels
{
    public class CoreManufacter : CoreManufacterDropDown
    {
        public int UserId { get; set; }
        public CoreUser User { get; set; }

        public List<CoreModel> Models { get; set; } = new List<CoreModel>();
    }
}
