using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Private Members
        /// <summary>
        /// Holds the current results of cells in the active game
        /// </summary>
        private MarkType[] mResults;

        /// <summary>
        /// True if it is Player 1's turn (X) or Player 2's turn (O)
        /// </summary>
        private bool mPlayer1Turn;

        /// <summary>
        /// True if the game has ended
        /// </summary>
        private bool mGameEnded;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            NewGame();
        }


        #endregion
        
        /// <summary>
        /// Starts a new game and clears all values back to the start
        /// </summary>
        private void NewGame()
        {
            // Create a new blank array of free cells
            mResults = new MarkType[9];

            // Explicitly setting MarkType to free
            for (var i = 0; i < mResults.Length; i++)
            {
                mResults[i] = MarkType.Free;
            }
            // Make sure Player 1 starts the game
            mPlayer1Turn = true;

            // Interate every button on the grid
            Container.Children.Cast<Button>().ToList().ForEach(button => 
            {
                // Change background, foreground and content to default values
                button.Content = string.Empty;
                button.Background = Brushes.Magenta;
                button.Foreground = Brushes.Aquamarine;
            });

            //Make sure the game hasn't finished
            mGameEnded = false;
        }

        /// <summary>
        /// Handles a button click event
        /// </summary>
        /// <param name="sender">The button that was clicked </param>
        /// <param name="e">The events of the click </param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // start a new game on the click after it finished
            if (mGameEnded)
            {
                NewGame();
                return;
            }
            // casting to type button(explict casting)
            var button = (Button)sender;

            // Find the buttons position in the array
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            var index = column + (row * 3);

            //Don't do anything if the cell already has a value in it
            if (mResults[index] != MarkType.Free)
                return;

            // Set the cell value based on which player's turn it is(set result value)
            mResults[index] = mPlayer1Turn ? MarkType.Cross : MarkType.Nought;

            // Set button text to the result
            button.Content = mPlayer1Turn ? "X" : "O";

            if (!mPlayer1Turn)
                button.Foreground = Brushes.Gray;

            //Bitwise operators (inverts boolean) Toggle players turn
            mPlayer1Turn ^= true;

            // Check for a winner
            CheckForWinner();
        }

        /// <summary>
        /// Checks if there is a winner of a 3 line straight
        /// </summary>
        private void CheckForWinner()
        {
            #region Horizontal Wins
            // Check for horizontal wins
            //
            //  - Row 0
            //
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[1] & mResults[2]) == mResults[0])
            {
                //Game ends
                mGameEnded = true;

                // Highlight winning cells in purple
                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Purple;
            }
            //
            //  - Row 1
            //
            if (mResults[3] != MarkType.Free && (mResults[3] & mResults[4] & mResults[5]) == mResults[3])
            {
                //Game ends
                mGameEnded = true;

                // Highlight winning cells in purple
                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Purple;
            } 
            //
            //  - Row 2
            //
            if (mResults[6] != MarkType.Free && (mResults[6] & mResults[7] & mResults[8]) == mResults[6])
            {
                //Game ends
                mGameEnded = true;

                // Highlight winning cells in purple
                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Purple;
            }
            #endregion

            #region Vertical Wins
            // Check for vertical wins
            //
            //  - Column 0
            //
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[3] & mResults[6]) == mResults[0])
            {
                //Game ends
                mGameEnded = true;

                // Highlight winning cells in purple
                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.Purple;
            }
            //
            //  - Column 1
            //
            if (mResults[1] != MarkType.Free && (mResults[1] & mResults[4] & mResults[7]) == mResults[1])
            {
                //Game ends
                mGameEnded = true;

                // Highlight winning cells in purple
                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.Purple;
            }
            //
            //  - Column 2
            //
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[5] & mResults[8]) == mResults[2])
            {
                //Game ends
                mGameEnded = true;

                // Highlight winning cells in purple
                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.Purple;
            }
            #endregion

            #region Diagonal Wins
            //
            //  - Top Left Bottom Right
            //
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[4] & mResults[8]) == mResults[0])
            {
                //Game ends
                mGameEnded = true;

                // Highlight winning cells in purple
                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Purple;
            }
            //
            //  - Top Right Bottom Left
            //
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[4] & mResults[6]) == mResults[2])
            {
                //Game ends
                mGameEnded = true;

                // Highlight winning cells in purple
                Button2_0.Background = Button1_1.Background = Button0_2.Background = Brushes.Purple;
            }
            
            #endregion

            #region No Winners
            // Check for no winner and full board
            if (!mResults.Any(result => result == MarkType.Free))
            {
                mGameEnded = true;

                //turn all cells PaleVioletRed
                Container.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    
                    button.Background = Brushes.PaleVioletRed;
                });
            }
            #endregion
        }
    }
}
