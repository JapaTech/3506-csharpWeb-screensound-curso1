using ScreenSound.BD;
using ScreenSound.Modelos;

namespace ScreenSound.Menus;

internal class MenuSair : Menu
{
    public override void Executar(DAL<Artista> dal)
    {
        Console.WriteLine("Tchau tchau :)");
    }
}
