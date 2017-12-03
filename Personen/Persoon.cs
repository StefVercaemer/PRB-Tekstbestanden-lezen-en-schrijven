using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personen
{

    public class Persoon
    {
        public enum Geslachten { M, V }

        public string Familienaam { get; set; }
        public string Voornaam { get; set; }
        public string Woonplaats { get; set; }
        public string Land { get; set; }
        public Geslachten Geslacht { get; set; }
        public int Leeftijd { get; set; }

        public override string ToString()
        {
            string aanspreking;
            if (Geslacht == Geslachten.M)
            {
                aanspreking = "De heer"; 
            }
            else
            {
                aanspreking = "Mevrouw";
            }
            return $"{aanspreking} {Voornaam} {Familienaam} uit {Woonplaats} ({Land.Substring(0,1)}) {Leeftijd} jaar";
        }

        public void Verjaar()
        {
            Leeftijd++;
        }

        public void Verhuis(string nieuweGemeente, string nieuwLand)
        {
            Woonplaats = nieuweGemeente;
            Land = nieuwLand;
        }
    }
}
