using Heirloom;
using Heirloom.Desktop;

namespace Space_Invaders;

public class Joc
{
    public Window Finestra { get; }

    private Nau _nau = null!;
    private BossAlien _bossAlien = null!;
    private Bala _bala = null!;
    private Marcia _marcia = null!;
    private Marcador _marcador = null!;
    private int _puntsnau = 0;
    private int _vidanau = 3;
    
    private readonly int _nummarcians;
    private readonly int _numbales;
    
    private readonly List<Marcia> _marcians = new();
    private readonly List<Bala> _balesNau = new();
    private readonly List<Bala> _balesMarcia = new();
    private enum Estat
    {
        Inici,
        Jugant,
        Win,
        GameOver
    }
    
    private Estat _estatactual = Estat.Inici;
    
    public Joc(Window finestra, int quantsmarcians)
    {
        Finestra = finestra;
        _nummarcians = quantsmarcians;
    }
    
    public void Inicialitzar()
    {
        var rectangleFinestra = new Rectangle((0,0), Finestra.Size);
        
        _bossAlien = new BossAlien(rectangleFinestra, _marcians);
        var imatgeNau = new Image("Imatges/Nave.jpg");
        
        var imatgeMarcia = new Image("Imatges/AlienYellow.jpg");
        var imatgeMarcia2 = new Image("Imatges/AlienOrange.jpg");
        var imatgeMarcia3 = new Image("Imatges/Alien_verde.jpg");
        var imatgeMarcia4 = new Image("Imatges/AlienCian.jpg");
        var imatgeMarcia5 = new Image("Imatges/AlienPurple.jpg");
        
        _nau = new Nau(new Rectangle(960, 850, imatgeNau.Width, imatgeNau.Height), -20, imatgeNau);
        
        var espaiEntreMarcians = 20;
        var x = espaiEntreMarcians;
        
        for (int i = 0; i < _nummarcians; i++)
        {
            var y = 50;
            _marcians.Add(new Marcia(new Rectangle(x,y,imatgeMarcia.Width,imatgeMarcia.Height),5, (1,0), imatgeMarcia));
            y = imatgeMarcia.Height + 90;
            _marcians.Add(new Marcia(new Rectangle(x,y,imatgeMarcia2.Width,imatgeMarcia2.Height),5, (1,0), imatgeMarcia2));
            y = imatgeMarcia2.Height + 180;
            _marcians.Add(new Marcia(new Rectangle(x,y,imatgeMarcia3.Width,imatgeMarcia3.Height),5, (1,0), imatgeMarcia3));
            y = imatgeMarcia.Height + 290;
            _marcians.Add(new Marcia(new Rectangle(x,y,imatgeMarcia4.Width,imatgeMarcia4.Height),5, (1,0), imatgeMarcia4));
            y = imatgeMarcia2.Height + 360;
            _marcians.Add(new Marcia(new Rectangle(x,y,imatgeMarcia5.Width,imatgeMarcia5.Height),5, (1,0), imatgeMarcia5));
            x +=(imatgeMarcia.Width + espaiEntreMarcians);
        }
        _marcador = new Marcador(new Vector(1790, 950), _puntsnau, _vidanau);
    }

    public void Mouobjectes()
    {
        switch (_estatactual)
        {
        case Estat.Inici:
            if (Input.CheckKey(Key.Enter, ButtonState.Down))
            {
                _estatactual = Estat.Jugant;
                Inicialitzar();
            }
            break;

        case Estat.GameOver:
            if (Input.CheckKey(Key.Enter, ButtonState.Down))
            {
                Inicialitzar();
                _estatactual = Estat.Jugant;
            }
            else if (Input.CheckKey(Key.Escape, ButtonState.Down))
            {
                _estatactual = Estat.Inici;
            }
            break;
        
        case Estat.Win:
            if (Input.CheckKey(Key.Enter, ButtonState.Down))
            {
                _estatactual = Estat.Inici;
            }
            break;

        case Estat.Jugant:
            var rectanglefinestra = new Rectangle((0,0), Finestra.Size);

            _nau.Mou(rectanglefinestra, _balesNau);
            _bossAlien.Comprova();
            
            foreach (var bala in _balesNau.ToList())
            {
                bala.MouBalaNau(rectanglefinestra, -1);
                if (!rectanglefinestra.Contains(bala.PosicioBala()))
                {
                    _balesNau.Remove(bala);
                }
            }
            
            for (int i = _marcians.Count - 1; i >= 0; i--)
            {
                var marcia = _marcians[i];
                marcia.Mou(rectanglefinestra);
                
                Random rnd = new Random();
                var num = rnd.Next(0, 100);
                if (num == 0 && _balesMarcia.Count < 10)
                {
                    var bala = marcia.Dispara();
                    if (bala != null)
                    {
                        _balesMarcia.Add(bala);
                    }
                }
                
                for (int j = _balesNau.Count - 1; j >= 0; j--)
                {
                    var bala = _balesNau[j];
                    if (bala.PosicioBala().Overlaps(marcia.PosicioMarcia()))
                    {
                        _marcians.RemoveAt(i);
                        _balesNau.RemoveAt(j);
                        _puntsnau += 10;
                        _marcador.actualitzacio(_puntsnau, _vidanau);
                        if (_marcians.Count == 0)
                        {
                            _estatactual = Estat.Win;
                            _marcians.Clear();
                        }
                        break;
                    }
                }
            }

            for (int i = _balesMarcia.Count - 1; i >= 0; i--)
            {
                var bala = _balesMarcia[i];
                bala.MouBalaMarcia(rectanglefinestra, -1);
                
                if (!rectanglefinestra.Contains(bala.PosicioBala()))
                {
                    _balesMarcia.RemoveAt(i);
                    continue;
                }
                
                if (bala.PosicioBala().Overlaps(_nau.PosicioNau()))
                {
                    _vidanau--;
                    _marcador.actualitzacio(_puntsnau, _vidanau);
                    _balesMarcia.RemoveAt(i);

                    if (_vidanau == 0)
                    {
                        _estatactual = Estat.GameOver;
                        _marcians.Clear();
                        _vidanau = 3;
                    }
                }
            }
            break;
        } 
    }

    public void Pinta(GraphicsContext gfx)
    {
        var rectanglefinestra = new Rectangle((0, 0), Finestra.Size);
        
        switch (_estatactual)
        {
            case Estat.Inici:
                var iniciPartida = new Image("Imatges/Inici.png");
                gfx.DrawImage(iniciPartida, rectanglefinestra);
                gfx.DrawText(_marcador.Iniciar(), new Vector(960 , 750), Font.Default, 60, TextAlign.Center);
                break;

            case Estat.GameOver:
                var finalPartida = new Image("Imatges/GameOver.png");
                gfx.DrawImage(finalPartida, rectanglefinestra);
                gfx.DrawText(_marcador.PlayAgain(), new Vector(960 , 750), Font.Default, 50, TextAlign.Center);
                break;
            
            case Estat.Win:
                var PartidaAcabada = new Image("Imatges/Win.png");
                gfx.DrawImage(PartidaAcabada, rectanglefinestra);
                gfx.DrawText(_marcador.Win(), new Vector(960 , 750), Font.Default, 40, TextAlign.Center);
                break;

            case Estat.Jugant:
                var background = new Image("Imatges/Galaxia.jpg");
                gfx.DrawImage(background, rectanglefinestra);

                _nau.Pinta(gfx);

                foreach (var bala in _balesMarcia)
                {
                    bala.Pinta(gfx);
                }

                foreach (var marcia in _marcians)
                {
                    marcia.Pinta(gfx);
                }

                foreach (var bala in _balesNau)
                {
                    bala.Pinta(gfx);
                }
                gfx.Color= Color.Yellow;
                gfx.DrawText(_marcador.Puntuacio(), _marcador.Posicio, Font.Default, 32, TextAlign.Top | TextAlign.Right);
                break;
        }
    }
}