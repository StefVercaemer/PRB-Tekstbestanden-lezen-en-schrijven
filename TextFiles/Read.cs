using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Win32;
using System.Text;
using System.Threading.Tasks;

namespace TextFiles
{


    public class Read
    {
        
        public string rootPad, myDocs;

        public Read()
        {
            rootPad = AppDomain.CurrentDomain.BaseDirectory;
            myDocs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }

        /// <summary>
        /// Opent een dialoogvenster om een bestand te selecteren. Standaard: tekstbestanden
        /// </summary>
        /// <returns>string met volledige pad van het gekozen bestand</returns>
        /// vb. filter: "Text documents (.txt)|*.txt|Comma seperated values (.csv)|*.csv"
        public string OpenBestand(string filter)
        {
            string gekozenBestandsPad = "";

            OpenFileDialog kiesBestand = new OpenFileDialog();
            //Enkel de bestanden met de doorgegeven extensie(s) worden getoond
            kiesBestand.Filter = filter;

            // Toon het dialoogvenster
            Nullable<bool> result = kiesBestand.ShowDialog();
            gekozenBestandsPad = kiesBestand.FileName;
            return gekozenBestandsPad;
        }

        /// <summary>
        /// Zet een tekstbetand om naar en list van strings[], waarbij elke regel uit het bestand een element van de list wordt
        /// In de parameter bestandsPad wordt het volledige pad van het tekstbestand meegegeven
        /// Ook het scheidingsteken tussen de verschillende elementen in het tekstbestand wordt meegegeven
        /// </summary>
        /// <param name="bestandsPad"></param>
        /// <param name="scheidingsteken"></param>
        /// <returns></returns>
        public List<string[]> ToStringArray_List(string bestandsPad, char scheidingsteken)
        {
            List<string> omzettingLijnen = new List<string>();
            List<string[]> omgezet = new List<string[]>();
            //Elke lijn uit het bestand wordt omgezet naar een item in de list
            omzettingLijnen = ToStringList(bestandsPad);
            //elk item in de omzettingLijnen wordt in een string[]
            foreach (string item in omzettingLijnen)
            {
                omgezet.Add(item.Split(scheidingsteken));
            }
            return omgezet;
        }

        /// <summary>
        /// Zet een tekstbetand om naar en list van strings, waarbij elke regel uit het bestand een element van de list wordt
        /// In de parameter bestandsPad wordt het volledige pad van het tekstbestand meegegeven
        /// </summary>
        /// <param name="bestandsPad">Plaats en naam het het bestand</param>
        /// <param name="scheidingsteken">scheidingsteken dat de verschillende gegevens onderscheidt</param>
        /// <returns></returns>
        public List<string> ToStringList(string path)
        {
            string[] temp;

            using (var sr = new StreamReader(path, System.Text.Encoding.Default, true))
            {
                temp = sr.ReadToEnd().Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            }

            return temp.ToList();
        }


        /// <summary>
        /// Een list met strings waarin de gegevens  wordt omgezet naar een list van string-arrays
        /// </summary>
        /// <param name="charSeperatedString">List met strings waarin de gegevens telkens door een scheidingsteken onderscheiden worden</param>
        /// <param name="scheidingsteken">Het scheidingsteken dat de gegevens in elke string onderscheidt</param>
        /// <returns></returns>
        List<string[]> CharSeperatedListNaarArrayList(List<string> charSeperatedString, char scheidingsteken)
        {
            //Declaratie van een array van strings. 
            //De elementen uit charSeperatedString zullen telkens omgezet worden naar een array van strings
            string[] arrayRecord;
            //Er wordt een instance aangemaakt van een List<string[]>. Hierin komen de arrayRecords in terecht
            List<string[]> omgezet = new List<string[]>();
            //csvStrings wordt overlopen van het 0de tot en met het laatste element
            foreach (string record in charSeperatedString)
            {
                //Elke csv-string wordt omgezet naar een array.
                //De elementen worden van elkaar gescheiden door een ;
                arrayRecord = record.Split(scheidingsteken);
                //Het aldus omgezette element wordt toegevoegd aan de list
                omgezet.Add(arrayRecord);
            }
            return omgezet;
        }




    }
}
