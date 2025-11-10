using Microsoft.IdentityModel.Tokens;
using ScreenSound.BD;
using ScreenSound.Menus;
using ScreenSound.Modelos;

internal class MenuMostrarMusicasPorAno : Menu
{
    public override void Executar(DAL<Artista> dal)
    {
        base.Executar(dal);
        Console.WriteLine("Digite o ano da música");
        int ano = int.Parse(Console.ReadLine()!);

        if (ano < 0)
        {
            Console.WriteLine("Ano inválido");
        }
        else
        {
            var musicaDal = new DAL<Musica>(new ScreenSoundContext());

            var musicasDoAno = musicaDal.BuscarPor(m => m.AnoLancamento == ano).ToList();

            if (musicasDoAno.IsNullOrEmpty())
            {
                Console.WriteLine($"Não há músicas cadastradas para o ano de {ano}");
            }
            else
            {
                Console.WriteLine($"Músicas do ano de {ano}:");

                foreach (var musica in musicasDoAno)
                {
                    musica.ExibirFichaTecnica();
                }
                Console.WriteLine();
            }
        }
    }
}
   