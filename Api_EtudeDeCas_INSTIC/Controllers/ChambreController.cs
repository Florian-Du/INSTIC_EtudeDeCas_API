using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Api_EtudeDeCas_INSTIC.BDD;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using Api_EtudeDeCas_INSTIC.Models;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Api_EtudeDeCas_INSTIC.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ChambreController : Controller
    {
        private MySqlConnection Connexion;
        // Data Source=TRAN-VMWARE\SQLEXPRESS;Initial Catalog=simplehr;Persist Security Info=True;User ID=sa;Password=12345
        public ChambreController()
        {
            BDD.BDD BDD = new BDD.BDD();
            Connexion = BDD.ConnectionBuilder();
            try
            {
                
                Connexion.Open();
                Console.WriteLine("Connexion reussi");
             
                
            }
            catch(Exception ex)
            {
                throw new Exception("Impossible de requeter la BDD" + ex);
            }
            
        }
        // GET: ChambreController
        public ActionResult Index()
        {
            try
            {
                string requeteSQL = "SELECT * FROM chambre";
                MySqlCommand cmd = new MySqlCommand(requeteSQL, Connexion);
                MySqlDataReader rdr = cmd.ExecuteReader();

                var ListChambre = new List<Chambre>();

                while (rdr.Read())
                {
                    var Chambre = new Chambre();

                    Chambre.Id = rdr["ID"].ToString();
                    Chambre.ID_Appartement = rdr["ID_Appartement"].ToString();
                    Chambre.Nom = rdr["Nom"].ToString() ?? "";
                    Chambre.Prix = int.Parse(rdr["Prix"].ToString() ?? "0");
                    Chambre.Description = rdr["Description"].ToString() ?? "";

                    ListChambre.Add(Chambre);
                }
                return Json(ListChambre);
            }
            catch (Exception Ex)
            {
                throw new Exception("Erreur lors de la recuperation des chambres" + Ex.Message);
            }
            
        }

        // GET: Chambre/Details?id=5
        public ActionResult Details([FromQuery]string id)
        {
            try
            {
                string requeteSQL = "SELECT * FROM chambre WHERE Id = @Id";
                MySqlCommand cmd = new MySqlCommand(requeteSQL, Connexion);
                cmd.Parameters.AddWithValue("@Id", id);
                MySqlDataReader rdr = cmd.ExecuteReader();

                var ListChambre = new List<Chambre>();

                while (rdr.Read())
                {
                    var Chambre = new Chambre();

                    Chambre.Id = rdr["ID"].ToString();
                    Chambre.ID_Appartement = rdr["ID_Appartement"].ToString();
                    Chambre.Nom = rdr["Nom"].ToString() ?? "";
                    Chambre.Prix = int.Parse(rdr["Prix"].ToString() ?? "0");
                    Chambre.Description = rdr["Description"].ToString() ?? "";

                    ListChambre.Add(Chambre);
                }
                return Json(ListChambre);
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la recuperation du details de la chambre" + ex.Message);
            }
            
        }

        // POST: ChambreController/Create
        [HttpPost]
        public ActionResult Create([FromBody]JsonObject _Chambre)
        {
            try
            {
                var Chambre = new Chambre();

                Chambre.Id = Guid.NewGuid().ToString();
                Chambre.ID_Appartement = (string)_Chambre["ID_Appartement"];
                Chambre.Nom = (string)_Chambre["Nom"] ?? "";
                Chambre.Prix = int.Parse((string)_Chambre["Prix"]);
                Chambre.Description = (string)_Chambre["Description"] ?? "";

                string requeteSQL = "INSERT INTO chambre(ID, ID_Appartement, Nom, Prix, Description) VALUES(@ID, @ID_Appartement, @Nom, @Prix, @Description)";
                MySqlCommand cmd = new MySqlCommand(requeteSQL, Connexion);
                cmd.Parameters.AddWithValue("@ID", Chambre.Id);
                cmd.Parameters.AddWithValue("@ID_Appartement", Chambre.ID_Appartement);
                cmd.Parameters.AddWithValue("@Nom", Chambre.Nom);
                cmd.Parameters.AddWithValue("@Prix", Chambre.Prix);
                cmd.Parameters.AddWithValue("@Description", Chambre.Description);
                int rdr = cmd.ExecuteNonQuery();

                return Json(Chambre);
            }
            catch(Exception ex)
            {
                throw new Exception("Erreur lors de l'insert de la chambre " + ex.Message);
            }
        }

        // POST: ChambreController/Edit/5
        [HttpPost]
        public ActionResult Edit([FromBody] JsonObject _Chambre)
        {
            try
            {
                var Chambre = new Chambre();

                Chambre.Id = (string)_Chambre["Id"] ?? "";
                Chambre.Nom = (string)_Chambre["Nom"] ?? "";
                Chambre.Prix = int.Parse((string)_Chambre["Prix"]);
                Chambre.Description = (string)_Chambre["Description"] ?? "";

                string requeteSQL = "Update chambre SET Nom = @Nom, Prix = @Prix, Description = @Description Where ID = @Id";
                MySqlCommand cmd = new MySqlCommand(requeteSQL, Connexion);
                cmd.Parameters.AddWithValue("@Id", Chambre.Id);
                cmd.Parameters.AddWithValue("@Nom", Chambre.Nom);
                cmd.Parameters.AddWithValue("@Prix", Chambre.Prix);
                cmd.Parameters.AddWithValue("@Description", Chambre.Description);
                int rdr = cmd.ExecuteNonQuery();

                return Json(Chambre);
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de l'update du compte " + ex.Message);
            }
        }

        // GET: ChambreController/Delete?id=1
        public ActionResult Delete([FromQuery] string id)
        {
            try
            {
                var Chambre = new Chambre();

                Chambre.Id = id;

                string requeteSQL = "DELETE FROM Chambre Where ID = @ID";
                MySqlCommand cmd = new MySqlCommand(requeteSQL, Connexion);
                cmd.Parameters.AddWithValue("@ID", Chambre.Id);
                int rdr = cmd.ExecuteNonQuery();

                return Json(Chambre);
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la suppression de la chambre " + ex.Message);
            }
        }
    }
}
