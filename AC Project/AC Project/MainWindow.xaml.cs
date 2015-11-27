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
    ///
    public partial class MainWindow 
    {
        Automata tool;
        double[,] table;
        List<TransitionTable> transitiontables = new List<TransitionTable>();
        List<Automata> automatas = new List<Automata>();
        List<Neighbours> Neighbours = new List<Neighbours>();

        int n=100;
        List<Neighbours> LocalBests = new List<Neighbours>();
        int[] alphabet;
        int[] EndingState;

        public MainWindow()
        {
            InitializeComponent();
            int[] _alphabet = { 0, 1 };
            alphabet = _alphabet;
            Random rand = new Random();
            for (int i = 0; i < n; i++)
            {
                //Generate 2-states random Automatons
                automatas.Add(Automata.GenerateParticle(2, alphabet, i, rand));
            }
        }

        private void LoadAutomataLines_Click(object sender, RoutedEventArgs e)
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
                int[] _alphabet= new int[0];

                int counter = 0 ;
                System.IO.StreamReader file = new System.IO.StreamReader(filename);

                while ((line = file.ReadLine()) != null)
                {
                    if (counter == 0)
                    {
                        states = Int32.Parse(line);
                    }                    if(counter==1)
                    {
                        _alphabet = new int[line.Length];
                        for (int i=0; i<line.Length; i++)
                        {
                            _alphabet[i] = (int)Char.GetNumericValue(line[i]);
                        }
                    }
                    if(counter==2)
                    {
                        

                        for (int z = 0; z < _alphabet.Length; z++)
                        {
                            table = new double[states, states];
                            for (int i = 0; i < states; i++)
                            {
                                for (int j = 0; j < states; j++)
                                {
                                    table[j, i] = (int)Char.GetNumericValue(line[i * states + j + states*states*z]);
                                }
                            }
                            TransitionTable t = new TransitionTable(states, table);
                            transitiontables.Add(t);
                            int d = 0;
                        }
                    }
                    counter++;
                }
                tool = new Automata(states, _alphabet, transitiontables, -1);
                alphabet = _alphabet;
                SetAutomataIntoWindow(tool);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            int _size = 0;
            for (int i = 0; i < 500; i++)
            {
                for (int j = i + 1; j < 500; j++)
                {
                    _size++;
                }
            }

            int[] _alphabet = { 0, 1 };
            //Hardcoded for easy testing, a sample from first AC classes 
            List<TransitionTable> SampleTable = new List<TransitionTable>();
            double[,] tmp1 = new double[4, 4] { { 0, 0, 0, 0 }, { 1, 0, 0, 0 }, { 0, 0, 1, 0 }, { 0, 1, 0, 1 } };
            double[,] tmp2 = new double[4, 4] { { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 1, 0, 1, 0 }, { 0, 1, 0, 1 } };
       //     double[,] tmp3 = new double[4, 4] { { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 1, 0, 1, 0 }, { 0, 1, 0, 1 } };

            SampleTable.Add(new TransitionTable(4, tmp1));
            SampleTable.Add(new TransitionTable(4, tmp2));
       //     SampleTable.Add(new TransitionTable(4, tmp3));

            Random rand = new Random();
            Automata ideal = new Automata(4,_alphabet,SampleTable, -1);
            Automata Particle = Automata.GenerateParticle(4, _alphabet, 1,rand);

            Word[] words = WordGenerator.GenerateWords(_alphabet, _alphabet.Count(), rand);

            SetAutomataIntoWindow(ideal);
         Automata solved = PSOAlgorithm.ComputePSO(ideal, automatas, alphabet, n, words, Neighbours, rand);
         if (solved != null)
             SetFoundAutomataIntoWindow(solved);

            
        }

        public void SetAutomataIntoWindow(Automata ideal )
        {
            string text;
            text = "" + ideal.getAlphabet()[0];
            foreach( var item in ideal.getAlphabet())
                if( item != ideal.getAlphabet()[0])
                    text += ", " + item;

            AlphabetTextbox.Text = text;
            StatesTextbox.Text = ideal.getStates().ToString();
            TabItemWindow.Content = "";
            for( int i = 0; i < ideal.getAlphabet().Count(); i++)
            {
                if (i == 0)
                {
                    text = "";
                    double[,] table = ideal.GetTransitionTable(i).GetTransitionMatrix();
                    for (int j = 0; j < ideal.GetTransitionTable(i).getSize(); j++)
                    {
                        for (int j2 = 0; j2 < ideal.GetTransitionTable(i).getSize(); j2++)
                            text += table[j2, j] + "  ";
                        TabItemWindow.Content += text;
                        text = "\n";
                    }
                    TabItemWindow.Header = ideal.getAlphabet()[i];

                }
                else
                {
                    TabItem NewTab = new TabItem { DataContext = TabItemWindow.DataContext };
                    text = "";
                    double[,] table = ideal.GetTransitionTable(i).GetTransitionMatrix();
                    for (int j = 0; j < ideal.GetTransitionTable(i).getSize(); j++)
                    {
                        for (int j2 = 0; j2 < ideal.GetTransitionTable(i).getSize(); j2++)
                            text += table[j2, j] + "  ";
                        NewTab.Content += text;
                        text = "\n";
                    }
                    NewTab.Header = ideal.getAlphabet()[i];
                    TabControlWindow.Items.Add(NewTab);
                }
            }
            
        }


        public void SetFoundAutomataIntoWindow(Automata ideal)
        {
            string text;
            text = "" + ideal.getAlphabet()[0];
            foreach (var item in ideal.getAlphabet())
                if (item != ideal.getAlphabet()[0])
                    text += ", " + item;

            AlphabetTextbox2.Text = text;
            StatesTextbox2.Text = ideal.getStates().ToString();
            TabItemWindow2.Content = "";
            ErrorTextBox.Text = ideal.getError().ToString();
            for (int i = 0; i < ideal.getAlphabet().Count(); i++)
            {
                if (i == 0)
                {
                    text = "";
                    double[,] table = ideal.GetTransitionTable(i).GetTransitionMatrix();
                    for (int j = 0; j < ideal.GetTransitionTable(i).getSize(); j++)
                    {
                        for (int j2 = 0; j2 < ideal.GetTransitionTable(i).getSize(); j2++)
                            text += table[j2, j] + "  ";
                        TabItemWindow2.Content += text;
                        text = "\n";
                    }
                    TabItemWindow2.Header = ideal.getAlphabet()[i];

                }
                else
                {
                    TabItem NewTab = new TabItem { DataContext = TabItemWindow2.DataContext };
                    text = "";
                    double[,] table = ideal.GetTransitionTable(i).GetTransitionMatrix();
                    for (int j = 0; j < ideal.GetTransitionTable(i).getSize(); j++)
                    {
                        for (int j2 = 0; j2 < ideal.GetTransitionTable(i).getSize(); j2++)
                            text += table[j2, j] + "  ";
                        NewTab.Content += text;
                        text = "\n";
                    }
                    NewTab.Header = ideal.getAlphabet()[i];
                    TabControlWindow2.Items.Add(NewTab);
                }
            }

        }
        private void LoaderComma_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".txt";
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                string line;
                int states = 0;
                int[] _alphabet = new int[0];

             //   int counter = 0;
                System.IO.StreamReader file = new System.IO.StreamReader(filename);
                 line= file.ReadToEnd();

                 List<string> stringautomata = line.Split(',').ToList();
                 if (stringautomata.Count() != 3)
                 {
                     MessageBox.Show("Wrong File Format");
                     return;
                 }
                 states = Int32.Parse(stringautomata[0]);
                       // string a = stringautomata[0];
                        _alphabet = new int[stringautomata[1].Count()];
                        for (int i = 0; i < stringautomata[1].Count(); i++)
                        {
                            _alphabet[i] = (int)Char.GetNumericValue(stringautomata[1][i]);
                        }
           

                        for (int z = 0; z < _alphabet.Length; z++)
                        {
                            table = new double[states, states];
                            for (int i = 0; i < states; i++)
                            {
                                for (int j = 0; j < states; j++)
                                {
                                    table[j, i] = (int)Char.GetNumericValue(stringautomata[2][i * states + j + states * states * z]);
                                }
                            }
                            TransitionTable t = new TransitionTable(states, table);
                            transitiontables.Add(t);
                        }
      
                tool = new Automata(states, _alphabet, transitiontables, -1);
                alphabet = _alphabet;
                SetAutomataIntoWindow(tool);

            }
            else
                MessageBox.Show("Something went very, very wrong");
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            Random rand = new Random();
            if (tool != null)
            {
                Word[] words = WordGenerator.GenerateWords(tool.getAlphabet(), tool.getAlphabet().Count(), rand);
                Automata solved = PSOAlgorithm.ComputePSO(tool, automatas, tool.getAlphabet(), n, words, Neighbours, rand);
                if (solved != null)
                    SetFoundAutomataIntoWindow(solved);
            }
        }


    }
}
