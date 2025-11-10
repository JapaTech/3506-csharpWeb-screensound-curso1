using Microsoft.IdentityModel.Tokens;
using ScreenSound.BD;
using ScreenSound.Menus;
using ScreenSound.Modelos;

internal class MenuListarMusicaPorAno : Menu
{
    public override void Executar(DAL<Musica> dal)
    {
        base.Executar(dal);
        ExibirTituloDaOpcao("Listando todas as músicas por ano de lançamento");

        var musicasOrdenadas = dal.ListarPorOrdem(m => m.AnoLancamento);
        if (musicasOrdenadas.IsNullOrEmpty())
        {
            Console.WriteLine("Não há músicas no banco");
        }
        else
        {
            foreach (var musica in musicasOrdenadas)
            {
                musica.ExibirFichaTecnica();
                Console.WriteLine();
            }
        }
    }
}
