using m152_bildergalerie.Pocos;

namespace m152_bildergalerie.Services
{
    public class ImageService : IImageService
    {
        public List<Image> Images { get; set; }

        public string ReplaceLastOccurrence(string Source, string Find, string Replace)
        {
            int place = Source.LastIndexOf(Find);

            if (place == -1)
                return Source;

            string result = Source.Remove(place, Find.Length).Insert(place, Replace);
            return result;
        }
    }
}
