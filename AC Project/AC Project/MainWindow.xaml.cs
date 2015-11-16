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
using AC_Project.Classes;
using AC_Project.Properties;
using AC_Project.Algorithms;

namespace AC_Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow 
    {
        Automata tool;
        double[,] table;
        List<TransitionTable> transitiontables = new List<TransitionTable>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoadAutomata_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".txt";
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                string line;
                int states=0;
                int[] alphabet= new int[0];

                int counter = 0 ;
                System.IO.StreamReader file = new System.IO.StreamReader(filename);

                while ((line = file.ReadLine()) != null)
                {
                    if (counter == 0)
                    {
                        states = Int32.Parse(line);
                    }                    if(counter==1)
                    {
                        alphabet = new int[line.Length];
                        for (int i=0; i<line.Length; i++)
                        {
                            alphabet[i] = (int)Char.GetNumericValue(line[i]);
                        }
                    }
                    if(counter==2)
                    {
                        table = new double[states, states];

                        for (int z = 0; z < alphabet.Length; z++)
                        {
                            for (int i = 0; i < states; i++)
                            {
                                for (int j = 0; j < states; j++)
                                {
                                    table[j, i] = (int)Char.GetNumericValue(line[i * states + j + states*states*z]);
                                }
                            }
                            TransitionTable t = new TransitionTable(states, table);
                            transitiontables.Add(t);
                        }
                      
                    }
                    counter++;
                }
                tool = new Automata(states, alphabet, transitiontables);
                MessageBox.Show("hello world");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int[] alphabet = { 0, 1 };
            
            Automata ideal = new Automata(4,alphabet,transitiontables);
            PSOAlgorithm a = new PSOAlgorithm();
            a.ComputePSO(ideal, alphabet, 100);
        }
    }
}
