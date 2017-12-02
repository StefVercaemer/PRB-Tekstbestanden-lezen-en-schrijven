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
        int index1, index2;

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
            Write schrijfBewerking = new Write();
            schrijfBewerking.SchrijfListVanArrays(personen2, leesBewerking.rootPad + "Personen.txt", "|");
            HaalInfoOp(leesBewerking.rootPad + "Personen.txt");
            txtNaam.Text = "";
            txtVoornaam.Text = "";
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
