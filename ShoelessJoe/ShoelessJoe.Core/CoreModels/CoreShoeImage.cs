using Microsoft.AspNetCore.Http;

namespace ShoelessJoe.Core.CoreModels
{
    public class CoreShoeImage
    {
        private readonly MemoryStream _memoryStream;
        private readonly byte[] _shoeArray;

        private string _fileName;
        private IFormFile _formFile;

        public int ShoeImageId { get; set; }

        public string FileName
        {
            get
            {
                return _fileName;
            }
            set
            {
                _fileName = value;
            }
        }
        public IFormFile File
        {
            get
            {
                return _formFile;
            }
            set
            {
                _formFile = value;
            }
        }

        public byte[] ShoeArray
        {
            get
            {
                return _shoeArray;
            }
        }

        public int ShoeId { get; set; }
        public CoreShoe Shoe { get; set; }

        public CoreShoeImage()
        {

        }

        public CoreShoeImage(byte[] bytes, string fileName)
        {
            _shoeArray = bytes;
            _fileName = fileName;

            _memoryStream = new MemoryStream(bytes);

            File = new FormFile(_memoryStream, 0, _shoeArray.Length, "Name", _fileName);

            _memoryStream.Dispose();
        }

        public CoreShoeImage(IFormFile formFile)
        {
            _formFile = formFile;

            using (_memoryStream = new MemoryStream())
            {
                _formFile.CopyTo(_memoryStream);
                _shoeArray = _memoryStream.ToArray();
            }

            GenerateNewFile();
        }

        private void GenerateNewFile()
        {
            string justFileName = Path.GetFileNameWithoutExtension(_formFile.FileName);
            string extension = Path.GetExtension(_formFile.FileName);

            _fileName = $"{justFileName}-{Guid.NewGuid()}{extension}";
        }
    }
}
