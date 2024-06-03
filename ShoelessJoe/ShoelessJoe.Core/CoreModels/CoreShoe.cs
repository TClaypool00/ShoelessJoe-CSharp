using Microsoft.AspNetCore.Http;

namespace ShoelessJoe.Core.CoreModels
{
    public class CoreShoe
    {
        private List<IFormFile> _files;

        public int ShoeId { get; set; }

        public double? RightSize { get; set; }

        public double? LeftSize { get; set; }

        public DateTime DatePosted { get; set; }

        public int ModelId { get; set; }
        public CoreModel Model { get; set; }

        public List<CoreShoeImage> ShoeImages { get; set; }

        public List<CorePotentialBuy> PotentialBuys { get; set; }

        public CoreShoe()
        {

        }

        public CoreShoe(List<IFormFile> files)
        {
            _files = files;

            if (_files is not null && _files.Count > 0)
            {
                ShoeImages = new List<CoreShoeImage>();

                for (int i = 0; i < _files.Count; i++)
                {
                    ShoeImages.Add(new CoreShoeImage(_files[i]));
                }
            }
        }
    }
}
