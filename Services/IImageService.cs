using m152_bildergalerie.Pocos;

namespace m152_bildergalerie.Services
{
    public interface IImageService
    {
        public List<Image> Images { get; set; }
        public string ReplaceLastOccurrence(string Source, string Find, string Replace);
    }
}
