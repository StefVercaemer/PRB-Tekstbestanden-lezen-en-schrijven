using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TextFiles
{
    public class Write
    {
        /// <summary>
        /// Een list van arrays van string wordt weggeschreven naar een tekstbestand
        /// Een List<string[]> wordt omgezet naar een List<string>. 
        /// Elke string is een omzetting van de string[] waarbij de elementen gescheiden worden door een meegegeven separator.
        /// Elk element van de List<string> wordt weggeschreven naar een bestand waarvan het bestandspad en naam worden meegegeven.
        /// </summary>
        /// <param name="listVanArrays"></param>
        /// <param name="bestandsPad"></param>
        /// <param name="separator"></param>
        public void SchrijfListVanArrays(List<string[]> listVanArrays, string bestandsPad, string separator)
        {
            List<string> wegTeSchrijven = ArrayListNaarCharacterSeparatedList(listVanArrays, separator);
            SchrijfListVanStrings(wegTeSchrijven, bestandsPad);
        }

        /// <summary>
        /// Schrijft een tekstbestand weg op basis van een list van strings
        /// Elk element van de list wordt een lijn in het tekstbestand
        /// </summary>
        /// <param name="data"></param>
        /// <param name="bestandsPad"></param>
        /// <returns>boolean die aanduidt of het gelukt is om het bestand op te slaan</returns>
        public bool SchrijfListVanStrings(List<string> data, string bestandsPad)
        {
            //in de bool gelukt zal opgeslagen of het opslaan geslaagd is
            bool gelukt = false;
            //Via een instance van een Streamwriter wordt een bestand aangemaakt met een opgegeven pad
            using (StreamWriter sw = new StreamWriter(
                new FileStream(bestandsPad, FileMode.Create, FileAccess.ReadWrite), System.Text.Encoding.UTF8))
            {
                //Elke element van de List<string> data wordt overlopen
                foreach (string tekstLijn in data)
                {
                    //Het ingelezen element wordt toegevoegd aan het tekstbestand op een nieuwe lijn.
                    sw.WriteLine(tekstLijn);
                }
                sw.Close();
                gelukt = true;
            }
            return gelukt;
        }

        /// <summary>
        /// Een list met string-arrays wordt omgezet naar een list van character seperated strings
        /// </summary>
        /// <param name="stringArrays"></param>
        /// <returns>list van csv-strings</returns>
        List<string> ArrayListNaarCharacterSeparatedList(List<string[]> stringArrays, string scheidingsteken)
        {
            string characterSeperatedString;
            //Er wordt een instance aangemaakt van een List<string> waarin de omgezet arrays opgeslagen worden
            List<string> omgezet = new List<string>();
            //Alle arrays in de list worden één voor één overlopen
            foreach (string[] record in stringArrays)
            {
                //Elke array wordt omgezet naar een  csv-string.
                //Tussen elk element wordt een ; geplaatst
                characterSeperatedString = String.Join(scheidingsteken, record);
                //De aldus bekomen string wordt toegevoegd aan de list
                omgezet.Add(characterSeperatedString);
            }
            return omgezet;
        }


    }
}
