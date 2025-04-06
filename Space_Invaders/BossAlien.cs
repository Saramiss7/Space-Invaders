using Heirloom;

namespace Space_Invaders;

public class BossAlien
{
    private Rectangle _finestra;
    private List<Marcia> _marcia;

    public BossAlien(Rectangle finestra, List<Marcia> marcians)
    {
        _finestra = finestra;
        _marcia = marcians;
    }

    public void Comprova()
    {
        if (!_marcia.Any())
        {
            return;
        }
        var primer = _marcia[0].PosicioMarcia();
        var ultim = _marcia[_marcia.Count-1].PosicioMarcia();

        if (!_finestra.Contains(ultim) || !_finestra.Contains(primer))
        {
            foreach (var marci in _marcia)
            {
                marci.CanviarDireccio();
            }
        }
    }
}