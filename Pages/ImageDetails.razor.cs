using m152_bildergalerie.Pocos;
using m152_bildergalerie.Services;
using Microsoft.AspNetCore.Components;

namespace m152_bildergalerie.Pages
{
    public partial class ImageDetails
    {
        [Inject]
        public IImageService ImageService { get; set; }

        private string _imageKitBaseUrl = "https://ik.imagekit.io/leonjost/";
        private Dictionary<string, string> _imageSizeUrls;
        private Image _image;

        private string _imageName;
        [Parameter]
        public string ImageName
        {
            get
            {
                return _imageName;
            }

            set
            {
                _imageName = value;

                _image = ImageService.Images.Find(i => i.Name == _imageName);

                _imageSizeUrls = new Dictionary<string, string>()
                {
                    { $"Original: {_image.Width}x{_image.Height}", $"{_imageKitBaseUrl}{_image.Name}?tr=w-auto" },
                    { $"Transformed: 300xScaledToKeepAspectRatio", $"{_imageKitBaseUrl}{_image.Name}?tr=w-300" },
                    { $"Transformed: 600xScaledToKeepAspectRatio", $"{_imageKitBaseUrl}{_image.Name}?tr=w-600" },
                    { $"Transformed: 1000xScaledToKeepAspectRatio", $"{_imageKitBaseUrl}{_image.Name}?tr=w-1000" }
                };
            }
        }
    }
}
