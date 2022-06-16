namespace Api_EtudeDeCas_INSTIC.Models
{
    public class Reservation
    {
        public string Id_Compte { get; set; }

        public string Id_Chambre { get; set; }

        public DateTime Date_de_debut { get; set; }

        public DateTime Date_de_fin { get; set; }
    }
}
