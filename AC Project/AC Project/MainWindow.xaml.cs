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
using GraphVizWrapper.Queries;
using GraphVizWrapper;
using GraphVizWrapper.Commands;
using System.IO;
using System.Configuration;
using System.Text.RegularExpressions;
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
        Automata solved;
        int constant =4;
    
        List<Neighbours> LocalBests = new List<Neighbours>();
        int[] alphabet; 
        int[] EndingState;

        double aError = 0.005;
        int NumOfWords = 100;
        int LengthOfWordsFrom = 9;
        int LengthOfWordsTo = 100;
        int MaxIterations = 500;
        int n = 100;             //nr of particles 

        public MainWindow()
        {
            InitializeComponent();
            NumberOfWordsTextBox.Text = NumOfWords.ToString();
            nParticlesTextBox.Text = n.ToString();
            aErrorTextBox.Text = aError.ToString();
            LengthFromTextBox.Text = LengthOfWordsFrom.ToString();
            LengthToTextBox.Text = LengthOfWordsTo.ToString();
            MaxIterationsTextBox.Text = MaxIterations.ToString();
            ConstantTextBox.Text = constant.ToString();
            
        }

              //This is a Test function, we do not need it
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int[] alphabet = {0,1};
            Random rand = new Random();
            Word[] words = WordGenerator.GenerateWordsWithConstant(alphabet, alphabet.Count(), rand, 4);
            int abc = 2;
        }


        //Show transition matrices of ideal automata on screen
        public void SetAutomataIntoWindow(Automata ideal )
        {
            string text;
            text = "" + ideal.getAlphabet()[0];
            foreach( var item in ideal.getAlphabet())
                if( item != ideal.getAlphabet()[0])
                    text += ", " + item;
            AlphabetTextbox.Text = text;
            StatesTextbox.Text = ideal.getStates().ToString();
            TabItem TabItemWindow = new TabItem { DataContext = TabControlWindow.DataContext };
            for( int i = 0; i < ideal.getAlphabet().Count(); i++)
            {
                    TabItem NewTab = new TabItem { DataContext = TabControlWindow.DataContext };
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

        //show transition matrices of found automata onto screen.
        public void SetFoundAutomataIntoWindow(Automata found)
        {
            string text;
            text = "" + found.getAlphabet()[0];
            foreach (var item in found.getAlphabet())
                if (item != found.getAlphabet()[0])
                    text += ", " + item;

            AlphabetTextbox2.Text = text;
            StatesTextbox2.Text = found.getStates().ToString();

      
            for (int i = 0; i < found.getAlphabet().Count(); i++)
            {
                    TabItem NewTab = new TabItem { DataContext = TabControlWindow2.DataContext };
                    text = "";
                    double[,] table = found.GetTransitionTable(i).GetTransitionMatrix();
                    for (int j = 0; j < found.GetTransitionTable(i).getSize(); j++)
                    {
                        for (int j2 = 0; j2 < found.GetTransitionTable(i).getSize(); j2++)
                            text += table[j2, j] + "  ";
                        NewTab.Content += text;
                        text = "\n";
                    }
                    NewTab.Header = found.getAlphabet()[i];
                    TabControlWindow2.Items.Add(NewTab);
                
            }
        }
/*Function that will load the automata file presented on website
 * It splits it into multiple strings based on comas
 * Then it proceeds to create transition tables that are
 * readable by our automata class i.e 0s and 1s. 
 * 0 -> there is no transition, 1-> there's transition.
 * */

        private void LoaderComma_Click(object sender, RoutedEventArgs e)
        {
            automatas.Clear();
            try
            {
                if (tool.TransitionTables != null)
                    tool.TransitionTables.Clear(); }
            catch (Exception EX) { }
            TabControlWindow.Items.Clear();
            TabControlWindow2.Items.Clear();
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".txt";
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                string filename = dlg.FileName, line;
                int states = 0;
                int[] _alphabet = new int[0];
                System.IO.StreamReader file = new System.IO.StreamReader(filename);
                 line= file.ReadToEnd();
                 List<string> stringautomata = line.Split(',').ToList();
                        states = Int32.Parse(stringautomata[0]);
                        _alphabet = new int[int.Parse(stringautomata[1])];
                        for (int i = 0; i < _alphabet.Count(); i++)
                        {
                            _alphabet[i] = i; }
                        int nrofRows = states * states * _alphabet.Count();
                        double[,] onetable = new double[states,states];
                        for (int j = 0; j < _alphabet.Count(); j++)
                        {
                            onetable = new double[states, states];
                            int counter = 0;
                            for (int i = 2 + j; i < stringautomata.Count; i = i + _alphabet.Count())
                            {
                                int transition = int.Parse(stringautomata[i]);
                                for (int x = 0; x < states; x++)
                                {
                                    if (x == transition)
                                        onetable[x, counter] = 1;
                                    else
                                        onetable[x, counter] = 0;  }
                                counter++; }
                            TransitionTable t = new TransitionTable(states, onetable);
                            transitiontables.Add(t); }
                tool = new Automata(states, _alphabet, transitiontables, -1);
                alphabet = _alphabet;
                SetAutomataIntoWindow(tool);
                Start.IsEnabled = true;
            }
            else
                MessageBox.Show("Something went very, very wrong"); }

        /*
        Start click starts computing PSO for loaded automata.
         * 
         * */
        private void Start_Click(object sender, RoutedEventArgs e)
        {
            Random rand = new Random();
            int[] _alphabet = tool.getAlphabet();
            alphabet = _alphabet;
            for (int i = 0; i < n; i++)
            {
                //Generate 2-states random Automatons
                automatas.Add(Automata.GenerateParticle(2, alphabet, i, rand));
            }
            if (tool != null)
            {
                List<Word[]> subsets = WordGenerator.GenerateWords(tool.getAlphabet(), tool.getAlphabet().Count(), rand, NumOfWords, LengthOfWordsFrom, LengthOfWordsTo, constant);
                Word[] TrainingSet = subsets[0]; //0 contains whole testset
                Word[] lesserthanConstant = subsets[1];
                Word[] greaterthanConstant = subsets[2];
                Word[] TestSet = WordGenerator.GenerateTestWords(tool.getAlphabet(), tool.getAlphabet().Count(), rand, NumOfWords, constant+1, LengthOfWordsTo,TrainingSet); 

                //main solution
                solved = PSOAlgorithm.ComputePSO(tool, automatas, tool.getAlphabet(), n, TrainingSet, Neighbours, rand, MaxIterations, aError);
                //Place for comparsion between test and training set
                if (solved != null)
                {
                    ErrorTextBox.Text = solved.getError().ToString();
                    tool.ComputeAutomata(TestSet);
                    solved.ComputeAutomata(TestSet);
                   ErrorTestSetTextbox.Text = PSOAlgorithm.CalculateRelations(tool, solved).ToString();
                   TabControlWindow2.Items.Clear();
                    SetFoundAutomataIntoWindow(solved);
                    DrawAutomaton();

                    //less than c
                    tool.ComputeAutomata(lesserthanConstant);
                    solved.ComputeAutomata(lesserthanConstant);
                    ErrorCTextBox.Text = PSOAlgorithm.CalculateRelations(tool, solved).ToString();
                    //greater than c
                    tool.ComputeAutomata(greaterthanConstant);
                    solved.ComputeAutomata(greaterthanConstant);
                    ErrorgCTextBox.Text = PSOAlgorithm.CalculateRelations(tool, solved).ToString();

                }
            }
        }

        /*Based on GraphViz extension. Used to create images of the automaton.
         * We change dots into byte array and byte arary into png image.
         * */
        void DrawAutomaton()
        {
            var getStartProcessQuery = new GetStartProcessQuery();
            var getProcessStartInfoQuery = new GetProcessStartInfoQuery();
            var registerLayoutPluginCommand = new RegisterLayoutPluginCommand(getProcessStartInfoQuery, getStartProcessQuery);
            var wrapper = new GraphGeneration(getStartProcessQuery,
                                              getProcessStartInfoQuery,
                                              registerLayoutPluginCommand);
            byte[] original = wrapper.GenerateGraph(Generatedot(tool.GetTransitionTables()), Enums.GraphReturnType.Png);
            BitmapImage bm = LoadImage(original);
            toolAutomatonImage.Source = bm;
            byte[] found = wrapper.GenerateGraph(Generatedot(solved.GetTransitionTables()), Enums.GraphReturnType.Png);
            foundAutomatonImage.Source = LoadImage(found);


        }
        //This function reads our transition tables and creates a dot string for GraphViz to read.
        public string Generatedot(List<TransitionTable>automaton)
        {
            string dotString = "digraph{";
            for (int i = 0; i < automaton.Count(); i++)
            {
                double[,] Table = automaton[i].GetTransitionMatrix();
                for (int j = 0; j < Table.GetLength(0); j++)
                {
                    for (int k = 0; k < Table.GetLength(0); k++ )
                    {
                        if(Table[j,k]>0)
                        {
                            string stateToState = "" + k + " -> " +j + @" [label = """;
                            stateToState += i;
                          //  stateToState += ",";
                            stateToState += @"""] ;";
                            dotString += stateToState;
                        }
                    }
                }
            }
            dotString += "}";
            return dotString;
        }
        //This function reads byte array and transfers it into image for WPF display.
        private static BitmapImage LoadImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }

        // Use the DataObject.Pasting Handler 
        private void TextBoxPasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                String text = (String)e.DataObject.GetData(typeof(String));
                if (!IsTextAllowed(text))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }

        private static bool IsTextAllowed(string text)
        {
            Regex regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
            return !regex.IsMatch(text);
        }

        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void NumberOfWords_LostFocus(object sender, RoutedEventArgs e)
        {
            int tmp;
            Int32.TryParse(NumberOfWordsTextBox.Text, out tmp);
            tmp = Clamp(tmp, 1, 100000);
            NumOfWords = tmp;
            NumberOfWordsTextBox.Text = NumOfWords.ToString();
        }
        private int Clamp(int  val, int min, int  max) 
        {
            if (val.CompareTo(min) < 0) return min;
            else if (val.CompareTo(max) > 0) return max;
            else return val;
        }
        private void LengthToTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            int tmp;
            Int32.TryParse(LengthToTextBox.Text, out tmp);
            tmp = Clamp(tmp, LengthOfWordsFrom, 5000);
            LengthOfWordsTo = tmp;
            LengthToTextBox.Text = LengthOfWordsTo.ToString();
        }

        private void LenghtFromTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            int tmp;
            Int32.TryParse(LengthFromTextBox.Text, out tmp);
            tmp = Clamp(tmp, 1, LengthOfWordsTo);
            LengthOfWordsFrom = tmp;
            LengthFromTextBox.Text = LengthOfWordsFrom.ToString();
        }

        private void NumberOfIterationsTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            int tmp;
            Int32.TryParse(MaxIterationsTextBox.Text, out tmp);
            tmp = Clamp(tmp, 100, 1000000);
            MaxIterations = tmp;
            MaxIterationsTextBox.Text = MaxIterations.ToString();
        }

        private void nParticles_LostFocus(object sender, RoutedEventArgs e)
        {
            int tmp;
            Int32.TryParse(nParticlesTextBox.Text, out tmp);

            n = tmp;
            nParticlesTextBox.Text = n.ToString();
        }

        private void aErrorTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            double tmp;
            Double.TryParse(aErrorTextBox.Text, out tmp);
         
            aError = tmp;
           aErrorTextBox.Text = aError.ToString();
        }

        private void ConstantTextBox_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            int tmp;
            Int32.TryParse(ConstantTextBox.Text, out tmp);
    
            constant= tmp;
            ConstantTextBox.Text = constant.ToString();
        }
       
    }

     


}
