using Api_EtudeDeCas_INSTIC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Api_EtudeDeCas_INSTIC.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class CompteController : Controller
    {
        private MySqlConnection Connexion;
        // Data Source=TRAN-VMWARE\SQLEXPRESS;Initial Catalog=simplehr;Persist Security Info=True;User ID=sa;Password=12345
        public CompteController()
        {
            BDD.BDD BDD = new BDD.BDD();
            Connexion = BDD.ConnectionBuilder();
            try
            {
                Connexion.Open();
                Console.WriteLine("Connexion reussi");
            }
            catch (Exception ex)
            {
                throw new Exception("Impossible de requeter la BDD" + ex);
            }

        }

        // GET: Compte/index
        public ActionResult Index()
        {
            try
            {
                string requeteSQL = "SELECT * FROM Compte";
                MySqlCommand cmd = new MySqlCommand(requeteSQL, Connexion);
                MySqlDataReader rdr = cmd.ExecuteReader();

                var ListComptes = new List<Compte>();

                while (rdr.Read())
                {
                    var Compte = new Compte();

                    Compte.Id = rdr["ID"].ToString();
                    Compte.Prenom = rdr["Prenom"].ToString();
                    Compte.Nom = rdr["Nom"].ToString() ?? "";
                    Compte.Email = rdr["Email"].ToString() ?? "";
                    Compte.Password = rdr["Password"].ToString() ?? "";

                    ListComptes.Add(Compte);
                }
                return Json(ListComptes);
            }
            catch (Exception Ex)
            {
                throw new Exception("Erreur lors de la recuperation des Comptes" + Ex.Message);
            }
        }

        // GET: Compte/Details?id=5
        public ActionResult Details([FromQuery] string id)
        {
            try
            {
                string requeteSQL = "SELECT * FROM Compte WHERE Id = @Id";
                MySqlCommand cmd = new MySqlCommand(requeteSQL, Connexion);
                cmd.Parameters.AddWithValue("@Id", id);
                MySqlDataReader rdr = cmd.ExecuteReader();

                var ListComptes = new List<Compte>();

                while (rdr.Read())
                {
                    var Compte = new Compte();

                    Compte.Id = rdr["ID"].ToString();
                    Compte.Prenom = rdr["Prenom"].ToString();
                    Compte.Nom = rdr["Nom"].ToString() ?? "";
                    Compte.Email = rdr["Email"].ToString() ?? "0";
                    Compte.Password = rdr["Password"].ToString() ?? "";

                    ListComptes.Add(Compte);
                }
                return Json(ListComptes);
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la recuperation du details du comptes" + ex.Message);
            }
        }

        // POST: Compte/Create
        [HttpPost]
        public ActionResult Create([FromBody] JsonObject _Chambre)
        {
            try
            {
                var Compte = new Compte();

                Compte.Id = Guid.NewGuid().ToString();
                Compte.Prenom = _Chambre["Prenom"].ToString() ?? "";
                Compte.Nom = _Chambre["Nom"].ToString() ?? "";
                Compte.Email = _Chambre["Email"].ToString() ?? "";
                Compte.Password = _Chambre["Password"].ToString() ?? "";

                string requeteSQL = "INSERT INTO Compte(Id, Prenom, Nom, Email, Password) VALUES(@Id, @Prenom, @Nom, @Email, @Password)";
                MySqlCommand cmd = new MySqlCommand(requeteSQL, Connexion);
                cmd.Parameters.AddWithValue("@Id", Compte.Id);
                cmd.Parameters.AddWithValue("@Prenom", Compte.Prenom);
                cmd.Parameters.AddWithValue("@Nom", Compte.Nom);
                cmd.Parameters.AddWithValue("@Email", Compte.Email);
                cmd.Parameters.AddWithValue("@Password", Compte.Password);
                int rdr = cmd.ExecuteNonQuery();

                return Json(Compte);
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de l'insert du compte " + ex.Message);
            }
        }


        // POST: Compte/Edit/5
        [HttpPost]
        public ActionResult Edit([FromBody] JsonObject _Compte)
        {
            try
            {
                var Compte = new Compte();

                Compte.Id = (string)_Compte["Id"] ?? "";
                Compte.Prenom = (string)_Compte["Prenom"] ?? "";
                Compte.Nom = (string)_Compte["Nom"] ?? "";
                Compte.Email = (string)_Compte["Email"] ?? "";
                Compte.Password = (string)_Compte["Password"] ?? "";

                string requeteSQL = "Update Compte SET Prenom = @Prenom, Nom = @Nom, Email = @Email, Password = @Password Where Id = @Id";
                MySqlCommand cmd = new MySqlCommand(requeteSQL, Connexion);
                cmd.Parameters.AddWithValue("@Id", Compte.Id);
                cmd.Parameters.AddWithValue("@Prenom", Compte.Prenom);
                cmd.Parameters.AddWithValue("@Nom", Compte.Nom);
                cmd.Parameters.AddWithValue("@Email", Compte.Email);
                cmd.Parameters.AddWithValue("@Password", Compte.Password);
                int rdr = cmd.ExecuteNonQuery();

                return Json(Compte);
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de l'update du compte " + ex.Message);
            }
        }

        // POST: Compte/Login
        [HttpPost]
        public ActionResult Login([FromBody] JsonObject _Compte)
        {
            try
            {
                var Compte = new Compte();

                Compte.Email = (string)_Compte["Email"] ?? "";
                Compte.Password = (string)_Compte["Password"] ?? "";

                string requeteSQL = "SELECT * FROM Compte Where Email = @Email";
                MySqlCommand cmd = new MySqlCommand(requeteSQL, Connexion);
                cmd.Parameters.AddWithValue("@Email", Compte.Email);
                MySqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    if (rdr["Password"].ToString() == Compte.Password)
                    {
                        Compte.Id = rdr["ID"].ToString();
                        Compte.Prenom = rdr["Prenom"].ToString();
                        Compte.Nom = rdr["Nom"].ToString() ?? "";
                        Compte.Email = rdr["Email"].ToString() ?? "0";
                        Compte.Password = rdr["Password"].ToString() ?? "";

                        return Json(Compte);
                    }

                }

                return Json(null);


            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de l'update du compte " + ex.Message);
            }
        }

        // GET: Compte/Delete?id=5
        public ActionResult Delete([FromQuery] string id)
        {
            try
            {
                var Compte = new Compte();

                Compte.Id = id;

                string requeteSQL = "DELETE FROM Compte Where Id = @Id";
                MySqlCommand cmd = new MySqlCommand(requeteSQL, Connexion);
                cmd.Parameters.AddWithValue("@Id", Compte.Id);
                int rdr = cmd.ExecuteNonQuery();

                return Json(Compte);
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la suppression du compte " + ex.Message);
            }
        }
    }
}
