using Heirloom;

namespace Space_Invaders;

public class Nau: ObjectePantalla
{
    private readonly Image _imatge;
    private Rectangle _position;
    private readonly int _velocitat;
    private static Bala _bala = null!;

    public Nau(Rectangle posicio, int velocitat, Image imatge) :base (posicio, velocitat, imatge)
    {
        _imatge = imatge;
        _position = posicio;
        _velocitat = velocitat;
    }

    public Rectangle PosicioNau()
    {
        var posicioNau = new Rectangle(_position.X, _position.Y, _imatge.Width, _imatge.Height);
        
        return posicioNau;
    }

    public void Mou(Rectangle finestra, List<Bala> balesNau)
    {
        var novaPosicio = _position;
        
        if(Input.CheckKey(Key.Left, ButtonState.Down))
        {
            novaPosicio.X += _velocitat;
        }
        
        if(Input.CheckKey(Key.Right, ButtonState.Down))
        {
            novaPosicio.X -= _velocitat;
        }
        
        if (Input.CheckKey(Key.Up, ButtonState.Down) && balesNau.Count < 5)
        {
            var balaImagen = new Image("Imatges/BalaNau.png");
            var balaPosicio = new Rectangle(_position.X + _imatge.Width / 2 - balaImagen.Width / 2, _position.Y - balaImagen.Height, balaImagen.Width, balaImagen.Height);
            balesNau.Add(new Bala(balaImagen, balaPosicio, 10, new Vector(0,-1)));
        }
        if (finestra.Contains(novaPosicio))
        {
            _position.X = novaPosicio.X;
        }
    }
    
    public void Pinta(GraphicsContext gfx)
    {
        gfx.DrawImage(_imatge, _position);
    }
}