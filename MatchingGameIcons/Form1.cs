/* Summary:
 * Creates an example matching program to build the following competencies:
 *
•Store objects, such as icons, in a List object.
•Use a foreach loop in Visual C# or a For Each loop in Visual Basic to iterate through items in a list.
•Keep track of a form's state by using reference variables.
•Build an event handler to respond to events that you can use with multiple objects.
•Make a timer that counts down and then fires an event exactly once after being started.
 */



using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MatchingGameIcons
{
    public partial class Form1 : Form
    {
        //this Label object holds the first clicked label
        Label firstClicked = null;

        //this Label object holds the second clicked label
        Label secondClicked = null;

        //use this random object to choose random icons
        Random random = new Random();

        //each letter is a unique icon in the webdings font,
        //and each letter will appear twice
        List<string> icons = new List<string>()
        {
            "!", "!", "N", "N", ",", ",", "k", "k",
        "b", "b", "v", "v", "w", "w", "z", "z"
        };

        //assigns the random icons from the icon list to a square on the form

        private void AssignIconToSquares()
        {
            //the form has 16 squares and there are 16 icons, so the list grabs one randomly from the list and puts it in the form

            foreach (Control control in tableLayoutPanel1.Controls) 
            {
                Label iconLabel = control as Label;
                if (iconLabel != null)
                {
                    int randomNumber = random.Next(icons.Count);
                    iconLabel.Text = icons[randomNumber];
                    iconLabel.ForeColor = iconLabel.BackColor;
                    icons.RemoveAt(randomNumber);
                }

            }



        }

        public Form1()
        {
            InitializeComponent();

            AssignIconToSquares();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //every label that is clicked is handled by this method
        //param: name="sender", the label that was clicked
        //param: name="e", the 'event' that occured
        private void label_Click(object sender, EventArgs e)
        {

            //ignore any clicks by the user if the timer is already started
            if (timer1.Enabled == true)
                return;
            
            Label clickedLabel = sender as Label;

            if (clickedLabel!=null)
            {
                //if the label clicked is already black, ignore the click
                if (clickedLabel.ForeColor == Color.Black)
                    return;

                //if firstClicked is null, this sets the clicked label as firstClicked
                if (firstClicked==null)
                {
                    firstClicked = clickedLabel;
                    firstClicked.ForeColor = Color.Black;
                    return;
                }

                //if the code gets this far then the timer isnt running and firstClicked isnt null,
                //so so this must be the second icon clicked by the user.
                //sets secondClicked equal to black
                secondClicked = clickedLabel;
                secondClicked.ForeColor = Color.Black;

                //check to see if the player won
                CheckForWinner();

                //if the two clicked icons are equal, sets firstClicked and secondClicked
                //equal to null so that they do not dissapear
                if (firstClicked.Text==secondClicked.Text)
                {
                    firstClicked = null;
                    secondClicked = null;
                    return;
                }

                //player has now clicked both icons, so here starts the timer 
                //(which calls the 'Start' method after 3/4 seconds
                timer1.Start();
            }

        }

        //this timer activates after the second icon has been clicked and if the two icons dont match.
        //after 3/4 of a second both icons are hidden
        private void timer1_Tick(object sender, EventArgs e)
        {
            //stop the timer
            timer1.Stop();

            //hides both icons
            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;

            //resets the firstClicked and secondClicked variables
            firstClicked = null;
            secondClicked = null;

        }

        //method to check that all icons match, and informs the player that they have won
        private void CheckForWinner()
        {
            //go through each label in the List 'icon' to see if they are matched 
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;

                if (iconLabel != null)
                {
                    if (iconLabel.ForeColor == iconLabel.BackColor)
                        return;
                }

                //if the loop found no unmatched icons then the player won!
                MessageBox.Show("You matched all of the icons!", "Congratulations!");
                Close();
            }


        }




    }
}
