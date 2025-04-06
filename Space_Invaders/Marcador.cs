using Heirloom;

namespace Space_Invaders;

public class Marcador
{
    public Vector Posicio { get; private set; }
    private int Punts;
    private int Vida;
    
    public Marcador(Vector posicio, int punts, int vida)
    {
        Posicio = posicio;
        Punts = punts;
        Vida = vida;
    }

    public string Iniciar()
    {
        return $"Press Enter to Start";
    }
    
    public string PlayAgain()
    {
        return $"Puntuació total:{Punts} \n Press Enter to try again";
    }

    public string Win()
    {
        return $"Puntuació total:{Punts} \n Press Enter to play again";
    }
    
    public void actualitzacio(int punts, int vida)
    {
        Punts = punts;
        Vida = vida;
    }
    
    public string Puntuacio()
    {
        return $"Puntuació: {Punts} Vida: {Vida}";
    }
}