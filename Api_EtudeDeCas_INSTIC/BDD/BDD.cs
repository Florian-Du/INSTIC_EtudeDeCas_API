using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace Api_EtudeDeCas_INSTIC.BDD

{
    public class BDD
    {
        public MySqlConnection ConnectionBuilder()
        {

            string ConnectionString = "SERVER=localhost;DATABASE=etudedecas;UID=root;PASSWORD=;";
            try
            {
                MySqlConnection command = new MySqlConnection(ConnectionString);
                return command;
            }
            catch ( Exception ex)
            {
                throw new Exception("Erreur lors de la connection a la BDD" + ex);
            }
            
        }
    }
}
