using ShoelessJoe.App.Models.PostModels;

namespace ShoelessJoe.App.Models
{
    public class ShoeViewModel : PostShoeViewModel
    {
        public string ManufacterName { get; set; }
        public string ModelName { get; set; }

        public int OwnerId { get; set; }
        public string OwnerDisplayName { get; set; }

        public List<PotentialBuyViewModel> PotentialBuys { get; set; }
    }
}
