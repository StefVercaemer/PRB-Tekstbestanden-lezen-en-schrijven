using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TextFiles;
using Personen;

namespace TextBestanden.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Read leesBewerking = new Read();
        List<string> personen1;
        List<string[]> personen2;
        List<Persoon> klasseMensen;
        Persoon aangekliktePersoon;
        int indexListStrings, indexListArrays, indexClass;

        public MainWindow()
        {
            InitializeComponent();
            HaalInfoOp(leesBewerking.rootPad + "Personen.txt");
        }

        void HaalInfoOp(string bestandsPad)
        {
            //Tekstbestand ==> List<string>
            lstLinks.Items.Clear();
            personen1 = leesBewerking.ToStringList(bestandsPad);
            foreach (string persoon in personen1)
            {
                lstLinks.Items.Add(persoon);
            }

            //Tekstbestand ==> List<string[]>
            lstMidden.Items.Clear();
            personen2 = leesBewerking.ToStringArray_List(bestandsPad, '|');
            foreach (string[] persoon in personen2)
            {
                lstMidden.Items.Add(String.Join(" - ", persoon));
            }

            //Tekstbestand ==> List<Persoon>
            klasseMensen = new List<Persoon>();
            //List<string[]> personen = leesBewerking.ToStringArray_List(leesBewerking.rootPad + "Personen.txt", '|');
            foreach (string[] persoon in personen2)
            {
                Persoon mens = new Persoon();
                mens.Familienaam = persoon[0];
                mens.Voornaam = persoon[1];
                mens.Woonplaats = persoon[2];
                mens.Land = persoon[3];
                if (persoon[4] == "M")
                {
                    mens.Geslacht = Persoon.Geslachten.M;
                }
                else
                {
                    mens.Geslacht = Persoon.Geslachten.V;
                }
                mens.Leeftijd = int.Parse(persoon[5]);
                klasseMensen.Add(mens);
            }
            ToonListVanClass();
        }

        void ToonListVanClass()
        {
            lstClass.Items.Clear();
            foreach (Persoon persoon in klasseMensen)
            {
                lstClass.Items.Add(persoon);
            }
        }

        void SlaPersonenOp(List<string[]> personenLijst)
        {
            Write schrijfBewerking = new Write();
            schrijfBewerking.SchrijfListVanArrays(personenLijst, leesBewerking.rootPad + "Personen.txt", "|");
            HaalInfoOp(leesBewerking.rootPad + "Personen.txt");
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            HaalInfoOp(leesBewerking.rootPad + "Personen.txt");
        }

        private void lstLinks_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            indexListStrings = lstLinks.SelectedIndex;
            txtPersoon.Text = lstLinks.SelectedValue.ToString();
        }

        private void lstMidden_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            indexListArrays = lstMidden.SelectedIndex;
            txtNaam.Text = personen2[indexListArrays][0];
            txtVoornaam.Text = personen2[indexListArrays][1];
        }

        private void btnSaveStringArray_Click(object sender, RoutedEventArgs e)
        {
            personen2[indexListArrays][0] = txtNaam.Text  ;
            personen2[indexListArrays][1] = txtVoornaam.Text;
            SlaPersonenOp(personen2);
            txtNaam.Text = "";
            txtVoornaam.Text = "";
        }

        private void lstClass_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            indexClass = lstClass.SelectedIndex;
            aangekliktePersoon = new Persoon();

            aangekliktePersoon = klasseMensen[indexClass];
            txtNaam_Class.Text = aangekliktePersoon.Familienaam;
            txtVoornaam_Class.Text = aangekliktePersoon.Voornaam;
            txtNieuweGemeente.Text = aangekliktePersoon.Woonplaats;
            txtNieuwLand.Text = aangekliktePersoon.Land;
        }

        private void txtNieuweGemeente_TextChanged(object sender, TextChangedEventArgs e)
        {
            klasseMensen[indexClass].Woonplaats = txtNieuweGemeente.Text;
        }

        private void txtNieuweGemeente_LostFocus(object sender, RoutedEventArgs e)
        {
            klasseMensen[indexClass].Verhuis(txtNieuweGemeente.Text, txtNieuwLand.Text);
            int huidigeIndex = indexClass;
            ClassListOpslaan();
            //ToonListVanClass();
            HaalInfoOp(leesBewerking.rootPad + "Personen.txt");
            indexClass = huidigeIndex;

        }

        private void btnSaveList_Class_Click(object sender, RoutedEventArgs e)
        {
            aangekliktePersoon.Familienaam = txtNaam_Class.Text ;
            aangekliktePersoon.Voornaam = txtVoornaam_Class.Text;
            ToonListVanClass();
            ClassListOpslaan();
            txtNaam_Class.Text = "";
            txtVoornaam_Class.Text = "";
        }

        private void btnVerjaar_Click(object sender, RoutedEventArgs e)
        {
            klasseMensen[indexClass].Verjaar();
            ClassListOpslaan();
            ToonListVanClass();
        }

        private void btnSaveWoonplaats_Click(object sender, RoutedEventArgs e)
        {
            klasseMensen[indexClass].Verhuis(txtNieuweGemeente.Text, txtNieuwLand.Text);
            ClassListOpslaan();
            HaalInfoOp(leesBewerking.rootPad + "Personen.txt");
        }

        void ClassListOpslaan()
        {
            List<string[]> personen3 = new List<string[]>();
            foreach (Persoon persoon in klasseMensen)
            {
                int i = 0;
                string[] dezePersoon = new string[6];
                dezePersoon[i++] = persoon.Familienaam;
                dezePersoon[i++] = persoon.Voornaam;
                dezePersoon[i++] = persoon.Woonplaats;
                dezePersoon[i++] = persoon.Land;
                dezePersoon[i++] = persoon.Geslacht.ToString();
                dezePersoon[i++] = persoon.Leeftijd.ToString();
                personen3.Add(dezePersoon);
            }
            SlaPersonenOp(personen3);
        }

        private void btnSaveString_Click(object sender, RoutedEventArgs e)
        {
            personen1[indexListStrings] = txtPersoon.Text;
            Write schrijfBewerking = new Write();
            schrijfBewerking.SchrijfListVanStrings(personen1,leesBewerking.rootPad + "Personen.txt");
            HaalInfoOp(leesBewerking.rootPad + "Personen.txt"); 

        }
    }
}
