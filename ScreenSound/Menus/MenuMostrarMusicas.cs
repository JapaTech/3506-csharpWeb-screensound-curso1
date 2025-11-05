using ScreenSound.BD;
using ScreenSound.Modelos;

namespace ScreenSound.Menus;

internal class MenuMostrarMusicas : Menu
{
    public override void Executar(ArtistaDAL artistaDAL)
    {
        base.Executar(artistaDAL);
        ExibirTituloDaOpcao("Exibir detalhes do artista");
        Console.Write("Digite o nome do artista que deseja conhecer melhor: ");
        string nomeDoArtista = Console.ReadLine()!;

        //var artistaRecuperado = artistaDAL.BuscarPorNome(nomeDoArtista);
        //foreach (var artista in artistaRecuperado)
        //{
        //    Console.WriteLine("\nDiscografia:");
        //    artista.ExibirDiscografia();
        //    Console.WriteLine("\nDigite uma tecla para voltar ao menu principal");
        //    Console.ReadKey();
        //    Console.Clear();
        //}

        var artistaRecuperado = artistaDAL.BuscarPorNomeExato(nomeDoArtista);
        if (artistaRecuperado is not null)
        {
            Console.WriteLine("\nDiscografia:");
            artistaRecuperado.ExibirDiscografia();
            Console.WriteLine("\nDigite uma tecla para voltar ao menu principal");
            Console.ReadKey();
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
