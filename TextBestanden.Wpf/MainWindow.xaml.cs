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
        Read bestand = new Read();

        public MainWindow()
        {
            InitializeComponent();
            HaalInfoOp(bestand.rootPad + "Personen.txt");
        }

        void HaalInfoOp(string bestandsPad)
        {
            List<string> personen1 = bestand.ToStringList(bestandsPad);
            foreach (string persoon in personen1)
            {
                lstLinks.Items.Add(persoon);
            }

            List<string[]> personen2 = bestand.ToStringArray_List(bestandsPad, '|');
            foreach (string[] persoon in personen2)
            {
                lstRechts.Items.Add(String.Join(" - ", persoon));
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            HaalInfoOp(bestand.rootPad + "Personen.txt");
        }
    }
}
