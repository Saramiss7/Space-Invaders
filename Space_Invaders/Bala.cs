using Heirloom;

namespace Space_Invaders;

public class Bala : ObjectePantallaD
{
    private readonly Image _imatge;
    private Rectangle _position;
    private readonly int _velocitat;
    private int _direccio;
    public Bala(Image imatgeBala,Rectangle posicioInicial, int velocitat, Vector _direccio) : base(posicioInicial, velocitat, _direccio, imatgeBala)
    {
        _imatge = imatgeBala;
        _position = posicioInicial;
    }
    public Rectangle PosicioBala()
    {
        return _position;
    }
    
    public void MouBalaNau(Rectangle finestra, int direccio)
    {
        _position.Y -= 2 * Velocitat;
        Posicio = _position;
    }

    public void MouBalaMarcia(Rectangle finestra, int direccio)
    {
        _position.Y += Velocitat;
        Posicio = _position;
    }

    public bool MarciaDispara()
    {
        Random random = new Random();
        var dispara = random.Next(1,100);
        if (dispara == 0)
        {
            return true;
        }
        return false;
    }
    
    public void Pinta(GraphicsContext gfx)
    {
        gfx.DrawImage(_imatge, _position);
    }
    
}