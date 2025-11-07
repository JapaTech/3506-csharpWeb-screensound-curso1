using Microsoft.IdentityModel.Tokens;
using ScreenSound.BD;
using ScreenSound.Modelos;

namespace ScreenSound.Menus;

internal class MenuRegistrarMusica : Menu
{
    public override void Executar(DAL<Artista> dal)
    {
        base.Executar(dal);
        ExibirTituloDaOpcao("Registro de músicas");
        Console.Write("Digite o artista cuja música deseja registrar: ");
        string nomeDoArtista = Console.ReadLine()!;
        var artistaRecuperado = dal.BuscarObjetoExato(n => n.Nome.Equals(nomeDoArtista));
        if (artistaRecuperado is not null)
        {
            Console.Write("Agora digite o título da música: ");
            string tituloDaMusica = Console.ReadLine()!;
            Console.Write("Agora digite o ano de lançamento da música: ");
            int anoDaMusica = int.Parse(Console.ReadLine())!;
            artistaRecuperado.AdicionarMusica(new Musica(tituloDaMusica, anoDaMusica)); 
            Console.WriteLine($"A música {tituloDaMusica} - {anoDaMusica} de {nomeDoArtista} foi registrada com sucesso!");
            dal.Atualizar(artistaRecuperado);
            Thread.Sleep(4000);
            Console.Clear();
        }
        else
        {
            Console.WriteLine($"\nO artista {nomeDoArtista} não foi encontrado!");
            Console.WriteLine("Digite uma tecla para voltar ao menu principal");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
