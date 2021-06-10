using System.Collections.Generic;
using System.Data.SqlClient;
using DogsAndPeople.Entities;
using DogsAndPeople.ViewModel;

namespace DogsAndPeople.Repositories
{
    public class PetshopRepository : IPetshopRepository
    {
        private readonly SqlConnection sqlConnection;

        public PetshopRepository()
        {
            sqlConnection = new SqlConnection("Data Source=DESKTOP-8J62RD3\\SQLEXPRESS;Initial Catalog=DOGSANDPEOPLE;Integrated Security=True;Connect Timeout=30");
        }

        public void Inserir(Dono dono, Cao cao)
        {
            Inserir(dono);
            Inserir(cao);
            
            var caoId = ObterUltimoCao();
            var donoId = ObterUltimoDono();

            var comando = $"INSERT INTO CAES_DONOS (ID_DONO, ID_CAO) VALUES ({donoId.Id}, {caoId.Id})";

            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();

            sqlConnection.Close();
        }

        private void Inserir(Cao cao)
        {
            var comando = $"INSERT INTO CAES (NOME_CAO, RACA_CAO) VALUES ('{cao.Nome}', '{cao.Raca}')";

            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();

            sqlConnection.Close();
        }

        private void Inserir(Dono dono)
        {
            var comando = $"INSERT INTO DONOS (NOME_DONO) VALUES ('{dono.Nome}')";

            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();

            sqlConnection.Close();
        }

        public List<RelatorioCaesDonos> RelatorioCaesDonos()
        {
            var caesDonos = new List<RelatorioCaesDonos>();

            var comando = $"SELECT DONOS.ID_DONO, NOME_DONO, CAES.ID_CAO, NOME_CAO, RACA_CAO FROM DONOS, CAES, CAES_DONOS WHERE (DONOS.ID_DONO = CAES_DONOS.ID_DONO) AND (CAES.ID_CAO = CAES_DONOS.ID_CAO)";

            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            while(sqlDataReader.Read())
            {
                caesDonos.Add(new RelatorioCaesDonos
                {
                    IdDono = (int)sqlDataReader["ID_DONO"],
                    NomeDono = (string)sqlDataReader["NOME_DONO"],
                    IdCao = (int)sqlDataReader["ID_CAO"],
                    NomeCao = (string)sqlDataReader["NOME_CAO"],
                    Raca = (string)sqlDataReader["RACA_CAO"]
                });
            }

            sqlConnection.Close();

            return caesDonos;
        }

        public List<Cao> FiltrarPorRaca(string raca)
        {
            var caes = new List<Cao>();

            var comando = $"SELECT * FROM CAES WHERE RACA_CAO = '{raca}'";

            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            while(sqlDataReader.Read())
            {
                caes.Add(new Cao
                {
                    Id = (int)sqlDataReader["ID_CAO"],
                    Nome = (string)sqlDataReader["NOME_CAO"],
                    Raca = (string)sqlDataReader["RACA_CAO"]
                });
            }

            sqlConnection.Close();

            return caes;
        }

        public Dono CadastrarDono(Dono dono)
        {
            Inserir(dono);

            var novoDono = ObterUltimoDono();

            return novoDono;
        }

        public Cao CadastrarCao(int idDono, Cao cao)
        {
            Inserir(cao);

            var caoId = ObterUltimoCao();

            var comando = $"INSERT INTO CAES_DONOS (ID_DONO, ID_CAO) VALUES ({idDono}, {caoId.Id})";

            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();

            sqlConnection.Close();

            var novoCao = ObterUltimoCao();

            return novoCao;
        }

        private Cao ObterUltimoCao()
        {
            Cao cao = null;

            var comando = $"SELECT TOP 1 * FROM CAES ORDER BY ID_CAO DESC";

            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            while (sqlDataReader.Read())
            {
                cao = new Cao
                {
                    Id = (int)sqlDataReader["ID_CAO"],
                    Nome = (string)sqlDataReader["NOME_CAO"],
                    Raca = (string)sqlDataReader["RACA_CAO"]
                };
            }

            sqlConnection.Close();

            return cao;
        }

        private Dono ObterUltimoDono()
        {
            Dono dono = null;

            var comando = $"SELECT TOP 1 * FROM DONOS ORDER BY ID_DONO DESC";

            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            while(sqlDataReader.Read())
            {
                dono = new Dono
                {
                    Id = (int)sqlDataReader["ID_DONO"],
                    Nome = (string)sqlDataReader["NOME_DONO"]
                };
            }

            sqlConnection.Close();

            return dono;
        }
    }
}