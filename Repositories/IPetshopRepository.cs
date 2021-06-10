using System.Collections.Generic;
using DogsAndPeople.Entities;
using DogsAndPeople.ViewModel;

namespace DogsAndPeople.Repositories
{
    public interface IPetshopRepository
    {
        void Inserir(Dono dono, Cao cao);
        List<RelatorioCaesDonos> RelatorioCaesDonos();
        List<Cao> FiltrarPorRaca(string raca);
        Dono CadastrarDono(Dono dono);
        Cao CadastrarCao(int idDono, Cao cao);
    }
}