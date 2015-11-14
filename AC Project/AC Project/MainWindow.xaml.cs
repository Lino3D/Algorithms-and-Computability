using AC_Project.Classes;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace AC_Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow 
    {
        Automata Tool;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoadAutomata_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                string file = openFileDialog.FileName;
                try
                {   
                    using (StreamReader sr = new StreamReader(file))
                    {
                        String line;
                        int i = 0;
                        while ((line = sr.ReadLine()) != null)
                        {

                        }
                    }
                }
                catch (Exception x)
                {
                    MessageBox.Show("File couldn't be streamed");
                }
            }
         
        }
    }
}
