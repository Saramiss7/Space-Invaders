using Heirloom;

namespace Space_Invaders;

public class ObjectePantallaD:ObjectePantalla
{
    protected Vector _direccio;

    public ObjectePantallaD(Rectangle posicioInicial, int velocitat, Vector direccio, Image imatge) : base (posicioInicial, velocitat,imatge)
    {
        _direccio = direccio;
    }
}