using Heirloom;

namespace Space_Invaders;

public class Marcia: ObjectePantallaD
{
    private readonly Image _imatge;

    public Marcia(Rectangle posicioInicial ,int _velocitat, Vector _direccio, Image imatgeMarcia) : base(posicioInicial,_velocitat,_direccio, imatgeMarcia)
    {
        _imatge = imatgeMarcia;
    }

    public Rectangle PosicioMarcia()
    {
        return Posicio;
    }
    
    public void CanviarDireccio()
    {
        _direccio *= -1;
    }
    
    public void Mou(Rectangle finestra)
    {
        var f = Posicio;
        f.Offset(Velocitat * _direccio);
        Posicio = f;
    }
    
    public Bala Dispara()
    {
        var balaImatge = new Image("Imatges/BalaMarcia.png");
        
        var balaPosicio = new Rectangle(Posicio.Center.X - balaImatge.Width /2, Posicio.Bottom, balaImatge.Width, balaImatge.Height);
        
        return new Bala(balaImatge,balaPosicio,Velocitat, _direccio);
    }
    
    public void Pinta(GraphicsContext gfx)
    {
        gfx.DrawImage(_imatge, Posicio);
    }
}