using Heirloom;
using Heirloom.Desktop;

namespace Space_Invaders;

class Program
{
    private const int Marciansinicials = 20;
    private static Window _finestra = null!;
    private static Joc _joc = null!;
    static void Main()
    {
        Application.Run(() =>
        {
            _finestra = new Window("La finestra", (1920, 1055));
            _finestra.Maximize();
            _joc = new Joc(_finestra, Marciansinicials);
            _joc.Inicialitzar();     
            var loop = GameLoop.Create(_finestra.Graphics, OnUpdate,60);
            loop.Start();
        });
    }

    private static void OnUpdate(GraphicsContext gfx, float dt)
    {
        _joc.Pinta(gfx);
        _joc.Mouobjectes();
    }
}