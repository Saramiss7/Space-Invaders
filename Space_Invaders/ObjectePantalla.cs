using Heirloom;

namespace Space_Invaders;

public class ObjectePantalla
{
    public Rectangle Posicio { get; protected set; }
    protected int Velocitat;

    public ObjectePantalla(Rectangle posicio, int velocitat, Image imatgeNau)
    {
        Posicio = posicio;
        Velocitat = velocitat;
    }
}