using MudBlazor;

namespace m152_bildergalerie.Pages
{
    public partial class Bildergalerie
    {
        private MudCarousel<string> _carousel;
        private bool _arrows = true;
        private bool _bullets = true;
        private bool _autocycle = true;
        private IList<string> _source = new List<string>() { "images/tes6_teaser.png", "images/the-elder-scrolls-2560x1440.jpg", "images/whiterun_landscape.png" };
        private int selectedIndex = 2;
    }
}
