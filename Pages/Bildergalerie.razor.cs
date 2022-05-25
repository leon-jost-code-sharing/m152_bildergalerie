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

        // Image Upload 
        private bool Clearing = false;
        private static string DefaultDragClass = "relative rounded-lg border-2 border-dashed pa-4 mt-4 mud-width-full mud-height-full";
        private string DragClass = DefaultDragClass;
        private List<string> fileNames = new List<string>();
        private IList<IBrowserFile> _files = new List<IBrowserFile>();

        // Bildergalerie
        private string _imageKitBaseUrl = "https://ik.imagekit.io/leonjost/";
        private List<string> _imageNames;


        protected override async Task OnInitializedAsync()
        {
            HttpResponseMessage response = await HttpClient.GetAsync("https://localhost:44369/api/images");
            _imageNames = await response.Content.ReadFromJsonAsync<List<string>>();
        }

        #region Image Upload

        private void OnInputFileChanged(InputFileChangeEventArgs e)
        {
            ClearDragClass();
            var files = e.GetMultipleFiles();
            foreach (var file in files)
            {
                fileNames.Add(file.Name);
                _files.Add(file);
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

            foreach (var file in _files)
            {
                var fileContent =
                    new StreamContent(file.OpenReadStream());

                fileContent.Headers.ContentType =
                    new MediaTypeHeaderValue(file.ContentType);

                content.Add(
                    content: fileContent,
                    name: "\"files\"",
                    fileName: file.Name);

                var response = await HttpClient.PostAsync("https://localhost:44369/api/images", content);
            }
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
            NavManager.NavigateTo($"/imageDetails/{imageName}");
        }

        #endregion
    }
}
