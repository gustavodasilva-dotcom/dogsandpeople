using System;
using DogsAndPeople.Services;

namespace DogsAndPeople
{
    class Program
    {
        static void Main(string[] args)
        {
            PetshopService petshopService = new PetshopService();

            string opcaoUsuario;

            opcaoUsuario = MenuOpcaoUsuario();

            while (opcaoUsuario.ToUpper() != "X")
            {
                switch(opcaoUsuario)
                {
                    case "1":
                        petshopService.Inserir();
                        break;
                    case "2":
                        petshopService.CadastrarDono();
                        break;
                    case "3":
                        petshopService.CadastrarCao();
                        break;
                    case "4":
                        petshopService.RelatorioCaesDonos();
                        break;
                    case "5":
                        petshopService.FiltrarPorRaca();
                        break;
                    case "X":
                        break;
                    case "C":
                        Console.Clear();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                opcaoUsuario = MenuOpcaoUsuario();
            }
        }

        private static string MenuOpcaoUsuario()
        {
            string opcaoUsuario;

            Console.WriteLine();
            Console.WriteLine("Informe a opção desejada: ");

            Console.WriteLine("1 - Inserir dono e cão;");
            Console.WriteLine("2 - Cadastrar um novo dono;");
            Console.WriteLine("3 - Cadastrar um novo cão;");
            Console.WriteLine("4 - Gerar relatório de donos e cães;");
            Console.WriteLine("5 - Filtrar por raça de cães;");
            Console.WriteLine("C - Limpar console; e");
            Console.WriteLine("X - Sair.");
            Console.WriteLine();

            Console.Write("Opção: ");
            opcaoUsuario = Console.ReadLine().ToUpper();

            Console.WriteLine();

            return opcaoUsuario;
        }
    }
}
