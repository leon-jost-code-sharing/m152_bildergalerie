using MudBlazor;

namespace m152_bildergalerie.Pages
{
    public partial class Bildergalerie
    {
        private MudCarousel<string> _carousel;
        private bool _arrows = true;
        private bool _bullets = true;
        private bool _autocycle = true;
        private IList<string> _source = new List<string>() { "images/riften_pier.jpg", "images/solitude_landscape.jpg", "images/whiterun_castle.jpg" };
        private int selectedIndex = 2;
    }
}
