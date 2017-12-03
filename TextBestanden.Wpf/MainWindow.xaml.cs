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
        int index1, index2, index3;

        public MainWindow()
        {
            InitializeComponent();
            HaalInfoOp(leesBewerking.rootPad + "Personen.txt");
        }

        void HaalInfoOp(string bestandsPad)
        {
            lstLinks.Items.Clear();
            personen1 = leesBewerking.ToStringList(bestandsPad);
            foreach (string persoon in personen1)
            {
                lstLinks.Items.Add(persoon);
            }

            lstRechts.Items.Clear();
            personen2 = leesBewerking.ToStringArray_List(bestandsPad, '|');
            foreach (string[] persoon in personen2)
            {
                lstRechts.Items.Add(String.Join(" - ", persoon));
            }
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
            index1 = lstLinks.SelectedIndex;
            txtPersoon.Text = lstLinks.SelectedValue.ToString();
        }

        private void lstRechts_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            index2 = lstRechts.SelectedIndex;
            txtNaam.Text = personen2[index2][0];
            txtVoornaam.Text = personen2[index2][1];
        }

        private void btnSaveStringArray_Click(object sender, RoutedEventArgs e)
        {
            personen2[index2][0] = txtNaam.Text  ;
            personen2[index2][1] = txtVoornaam.Text;
            SlaPersonenOp(personen2);
            txtNaam.Text = "";
            txtVoornaam.Text = "";
        }



        private void btnLoadClass_Click(object sender, RoutedEventArgs e)
        {
            klasseMensen = new List<Persoon>();
            List < string[] > personen = leesBewerking.ToStringArray_List(leesBewerking.rootPad + "Personen.txt", '|');
            foreach (string[] persoon in personen)
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

        private void lstClass_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            index3 = lstClass.SelectedIndex;
            aangekliktePersoon = new Persoon();

            aangekliktePersoon = klasseMensen[index3];
            txtNaam_Class.Text = aangekliktePersoon.Familienaam;
            txtVoornaam_Class.Text = aangekliktePersoon.Voornaam;
        }

        private void btnSaveList_Class_Click(object sender, RoutedEventArgs e)
        {
            aangekliktePersoon.Familienaam = txtNaam_Class.Text ;
            aangekliktePersoon.Voornaam = txtVoornaam_Class.Text;
            ToonListVanClass();
            List<string[]> personen3 = new List<string[]>();
            foreach (Persoon persoon in klasseMensen)
            {
                int i = 0;
                string[] dezePersoon = new string[6];
                dezePersoon[i] = persoon.Familienaam;
                dezePersoon[i++] = persoon.Voornaam;
                dezePersoon[i++] = persoon.Woonplaats;
                dezePersoon[i++] = persoon.Land;
                dezePersoon[i++] = persoon.Geslacht.ToString();
                dezePersoon[i++] = persoon.Leeftijd.ToString();
                personen3.Add(dezePersoon);
            }
            SlaPersonenOp(personen3);
            txtNaam_Class.Text = "";
            txtVoornaam_Class.Text = "";
        }

        private void btnSaveString_Click(object sender, RoutedEventArgs e)
        {
            personen1[index1] = txtPersoon.Text;
            Write schrijfBewerking = new Write();
            schrijfBewerking.SchrijfListVanStrings(personen1,leesBewerking.rootPad + "Personen.txt");
            HaalInfoOp(leesBewerking.rootPad + "Personen.txt"); 

        }
    }
}
