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

namespace SixDiceProbability
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //setting up the classes
        WPFMessagesClass TheMessagesClass = new WPFMessagesClass();

        //setting up the data
        DiceBreakDownDataSet TheDiceBreakDownDataSet = new DiceBreakDownDataSet();
        RollProbabilityDataSet TheRollProbabilityDataSet = new RollProbabilityDataSet();

        int gintProbCounter;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void expCloseProgram_Expanded(object sender, RoutedEventArgs e)
        {
            expCloseProgram.IsExpanded = false;
            TheMessagesClass.CloseTheProgram();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            int intDice1;
            int intDice2;
            int intDice3;
            int intDice4;
            int intDice5;
            int intDice6;
            int intTotal = 0;
            double douTotal;
            double douProbability;
            int intCounter;
            int intNumberOfRecords;
            int intProbCounter;
            int intDiceRoll;
            bool blnItemFound;
            decimal decProbability;

            try
            {

                for (intDice1 = 1; intDice1 < 7; intDice1++)
                {
                    for (intDice2 = 1; intDice2 < 7; intDice2++)
                    {
                        for (intDice3 = 1; intDice3 < 7; intDice3++)
                        {
                            for (intDice4 = 1; intDice4 < 7; intDice4++)
                            {
                                for (intDice5 = 1; intDice5 < 7; intDice5++)
                                {
                                    for (intDice6 = 1; intDice6 < 7; intDice6++)
                                    {
                                        intTotal = intDice1 + intDice2 + intDice3 + intDice4 + intDice5 + intDice6;

                                        DiceBreakDownDataSet.dicebreakdownRow NewDiceRoll = TheDiceBreakDownDataSet.dicebreakdown.NewdicebreakdownRow();

                                        NewDiceRoll.Dice1 = intDice1;
                                        NewDiceRoll.Dice2 = intDice2;
                                        NewDiceRoll.Dice3 = intDice3;
                                        NewDiceRoll.Dice4 = intDice4;
                                        NewDiceRoll.Dice5 = intDice5;
                                        NewDiceRoll.Dice6 = intDice6;
                                        NewDiceRoll.Total = intTotal;

                                        TheDiceBreakDownDataSet.dicebreakdown.Rows.Add(NewDiceRoll);
                                    }
                                }
                            }
                        }
                    }
                }

                dgrBreakDown.ItemsSource = TheDiceBreakDownDataSet.dicebreakdown;

                douTotal = 6 * 6 * 6 * 6 * 6 * 6;

                douProbability = 1 / douTotal;

                decProbability = Convert.ToDecimal(douProbability);

                decProbability = Math.Round(decProbability, 8);

                gintProbCounter = 0;

                intNumberOfRecords = TheDiceBreakDownDataSet.dicebreakdown.Rows.Count;

                for(intCounter = 0; intCounter < intNumberOfRecords; intCounter++)
                {
                    intDiceRoll = TheDiceBreakDownDataSet.dicebreakdown[intCounter].Total;
                    blnItemFound = false;

                    if(gintProbCounter > 0)
                    {
                        for(intProbCounter = 0; intProbCounter < gintProbCounter; intProbCounter++)
                        {
                            if(intDiceRoll == TheRollProbabilityDataSet.rollprobability[intProbCounter].DiceRoll)
                            {
                                blnItemFound = true;
                                TheRollProbabilityDataSet.rollprobability[intProbCounter].Probability += decProbability;
                            }
                        }
                    }

                    if(blnItemFound == false)
                    {
                        RollProbabilityDataSet.rollprobabilityRow NewProbRow = TheRollProbabilityDataSet.rollprobability.NewrollprobabilityRow();

                        NewProbRow.DiceRoll = intDiceRoll;
                        NewProbRow.Probability = decProbability;

                        TheRollProbabilityDataSet.rollprobability.Rows.Add(NewProbRow);

                        gintProbCounter++;
                    }
                }

                dgrProbability.ItemsSource = TheRollProbabilityDataSet.rollprobability;
                
            }
            catch (Exception Ex)
            {
                TheMessagesClass.ErrorMessage(Ex.ToString());
            }
        }
    }
}
