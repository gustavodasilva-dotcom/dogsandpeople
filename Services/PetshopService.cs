using System;
using System.Collections.Generic;
using DogsAndPeople.Entities;
using DogsAndPeople.Repositories;
using DogsAndPeople.ViewModel;

namespace DogsAndPeople.Services
{
    public class PetshopService : IPetshopService
    {
        private readonly PetshopRepository petshopRepository = new PetshopRepository();

        public void Inserir()
        {
            Dono dono = new Dono();
            Cao cao = new Cao();

            string nomeDono, nomeCao, racaCao;

            Console.Write("Insira o nome do dono: ");
            nomeDono = Console.ReadLine();

            Console.Write("Insira o nome do cão: ");
            nomeCao = Console.ReadLine();

            Console.Write("Insira a raça do cão: ");
            racaCao = Console.ReadLine();

            dono.Nome = nomeDono;

            cao.Nome = nomeCao;
            cao.Raca = racaCao;

            petshopRepository.Inserir(dono, cao);

            Console.WriteLine($"Dono {dono.Nome} e cão {cao.Nome} cadastros com sucesso!");
        }

        public void RelatorioCaesDonos()
        {
            var relatorioCaesDonos = petshopRepository.RelatorioCaesDonos();

            if (relatorioCaesDonos.Count == 0)
            {
                Console.WriteLine("Não existem dados para geração deste relatório.");
                return;
            }
            
            foreach (var caoDono in relatorioCaesDonos)
            {
                Console.WriteLine($"Dono: #{caoDono.IdDono} {caoDono.NomeDono} | Cão: #{caoDono.IdCao} {caoDono.NomeCao} | Raça do cão: {caoDono.Raca}");
                Console.WriteLine();
            }
        }

        public void FiltrarPorRaca()
        {
            string raca;

            Console.Write("Insira a raça: ");
            raca = Console.ReadLine();

            var filtrarPorRaca = petshopRepository.FiltrarPorRaca(raca);

            if (filtrarPorRaca.Count == 0)
            {
                Console.WriteLine("Não existem cães cadastrados com essa raça.");
                return;
            }

            foreach (var cao in filtrarPorRaca)
            {
                Console.WriteLine($"#{cao.Id} {cao.Nome} | Raça: {cao.Raca}");
                Console.WriteLine();
            }
        }

        public void CadastrarDono()
        {
            string nome;
            
            Console.Write("Insira o nome do dono: ");
            nome = Console.ReadLine();

            Dono dono = new Dono();
            dono.Nome = nome;

            var entidadeDono = petshopRepository.CadastrarDono(dono);

            Console.WriteLine($"Dono cadastrado com sucesso! O ID de {entidadeDono.Nome} é {entidadeDono.Id}.");
        }

        public void CadastrarCao()
        {
            int idDono;
            string nome, raca;

            Console.Write("Insira o ID do dono: ");
            idDono = int.Parse(Console.ReadLine());

            Console.Write("Insira o nome do cão: ");
            nome = Console.ReadLine();

            Console.Write("Insira a raça do cão: ");
            raca = Console.ReadLine();

            Cao cao = new Cao();
            cao.Nome = nome;
            cao.Raca = raca;

            var entidadeCao = petshopRepository.CadastrarCao(idDono, cao);

            Console.WriteLine($"Cão {entidadeCao.Nome} com ID {entidadeCao.Id} cadastrado com sucesso!");
        }
    }
}