using Microsoft.AspNetCore.Components;

namespace m152_bildergalerie.Pages
{
    public partial class ImageDetails
    {
        private string _imageKitBaseUrl = "https://ik.imagekit.io/leonjost/";
        private Dictionary<string, string> _imageSizesUrls;

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

                _imageSizesUrls = new Dictionary<string, string>()
                {
                    { "300x300", $"{_imageKitBaseUrl}tr:w-300,h-300/{_imageName}" },
                    { "600x600", $"{_imageKitBaseUrl}tr:w-600,h-600/{_imageName}" },
                    { "1000x1000", $"{_imageKitBaseUrl}tr:w-1000,h-1000/{_imageName}" }
                };
            }
        }
    }
}
