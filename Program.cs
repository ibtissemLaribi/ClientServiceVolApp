using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientServiceVolApp.ServiceReference1;



namespace ClientServiceVolApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            WebService1SoapClient proxy = new WebService1SoapClient();

            // Demandez à l'utilisateur de saisir la date au format "AAAA-MM-JJ"
            Console.WriteLine("Veuillez saisir la date de départ (AAAA-MM-JJ) : ");
            string dateString = Console.ReadLine();

            // Demandez à l'utilisateur de saisir la destination
            Console.WriteLine("Veuillez saisir la destination : ");
            string destination = Console.ReadLine();

            // Convertissez la date saisie en un objet DateTime
            if (DateTime.TryParseExact(dateString, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out DateTime dateDepart))
            {
                // Appelez le service web avec la date et la destination fournies
                Vol[] resultatArray = proxy.GetVols(dateDepart, destination);

                // Convertissez le tableau en une liste
                List<Vol> resultatList = new List<Vol>(resultatArray);

                // Affichez les résultats
                foreach (var vol in resultatList)
                {
                    Console.WriteLine($"Date de départ: {vol.DepartureDate}, Destination: {vol.Destination}, Compagnie: {vol.Company}, Vol: {vol.VolNumber}, Heure de départ: {vol.DepartureTime}, Commentaires: {vol.Comments}");
                }
            }
            else
            {
                Console.WriteLine("Format de date invalide. Assurez-vous d'utiliser le format AAAA-MM-JJ.");
            }

            // Fermez le proxy du service
            proxy.Close();

            Console.WriteLine("Appuyez sur une touche pour quitter...");
            Console.ReadKey();
        }
    }
}
