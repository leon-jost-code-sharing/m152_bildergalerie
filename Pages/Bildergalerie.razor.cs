using m152_bildergalerie.Pocos;
using m152_bildergalerie.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace m152_bildergalerie.Pages
{
    public partial class Bildergalerie
    {
        [Inject]
        public HttpClient HttpClient { get; set; }
        [Inject]
        public NavigationManager NavManager { get; set; }
        [Inject]
        public IImageService ImageService { get; set; }

        // Image Upload 
        private bool Clearing = false;
        private static string DefaultDragClass = "relative rounded-lg border-2 border-dashed pa-4 mt-4 mud-width-full mud-height-full";
        private string DragClass = DefaultDragClass;
        private List<string> fileNames = new List<string>();
        private IList<IBrowserFile> _files = new List<IBrowserFile>();
        private bool _displayProgressBar = false;

        // Bildergalerie
        private string _imageKitBaseUrl = "https://ik.imagekit.io/leonjost/";


        protected override async Task OnInitializedAsync()
        {
            HttpResponseMessage response = await HttpClient.GetAsync("https://m152bildergalerierestapi.azurewebsites.net/api/images");
            ImageService.Images = await response.Content.ReadFromJsonAsync<List<Image>>();

            _carouselSource = ImageService.Images.Select(i => $"{_imageKitBaseUrl}{i.Name}?tr=w-600").ToList();
        }

        #region Image Upload

        private void OnInputFileChanged(InputFileChangeEventArgs e)
        {
            ClearDragClass();
            var files = e.GetMultipleFiles();
            foreach (var file in files)
            {
                if (file.Size > 1000000)
                {
                    Snackbar.Configuration.PositionClass = Defaults.Classes.Position.TopCenter;
                    Snackbar.Add($"Files with {file.Size} bytes size are not allowed", Severity.Error);
                }
                else
                {
                    fileNames.Add(file.Name);
                    _files.Add(file);
                }
            }
        }

        private async Task Clear()
        {
            Clearing = true;
            fileNames.Clear();
            ClearDragClass();
            await Task.Delay(100);
            Clearing = false;
        }

        private async void Upload()
        {
            using var content = new MultipartFormDataContent();

            _displayProgressBar = true;
            foreach (var file in _files)
            {
                var fileContent =
                    new StreamContent(file.OpenReadStream(maxAllowedSize: 1000000));

                fileContent.Headers.ContentType =
                    new MediaTypeHeaderValue(file.ContentType);

                content.Add(
                    content: fileContent,
                    name: "\"files\"",
                    fileName: file.Name);
            }

            await HttpClient.PostAsync("https://m152bildergalerierestapi.azurewebsites.net/api/images", content);
            _displayProgressBar = false;
            await Clear();
            StateHasChanged();
        }

        private void SetDragClass()
        {
            DragClass = $"{DefaultDragClass} mud-border-primary";
        }

        private void ClearDragClass()
        {
            DragClass = DefaultDragClass;
        }

        #endregion

        #region Bildergalerie

        private async Task NavigateToImageDetailsPage(string imageName)
        {
            NavManager.NavigateTo($"/m152_bildergalerie/imageDetails/{imageName}"); // TODO: add /m152_bildergalerie to start of url
        }

        #endregion

        #region Carousel

        private MudCarousel<string> _carousel;
        private bool _arrows = true;
        private bool _bullets = true;
        private bool _autocycle = true;
        private IList<string> _carouselSource;
        private int selectedIndex = 2;

        #endregion
    }
}
