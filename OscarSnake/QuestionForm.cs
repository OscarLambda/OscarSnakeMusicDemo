using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OscarSnake
{
    public partial class QuestionForm : Form
    {
        List<(string question, string answer)> questions = new List<(string question, string answer)>();
        public static int index = 0;
        bool DidAnswerQuestion = false;
        public QuestionForm()
        {
            InitializeComponent();

            questions.Add(("What is your name?", "Oscar"));
            questions.Add(("What is your age?", "17"));
            questions.Add(("What is your sister's name?", "Lucy"));

            this.FormClosed += QuestionForm_FormClosed;
        }

        private void QuestionForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(DidAnswerQuestion == false)
            {
                DialogResult = DialogResult.Abort;
            }
            else
            {
                DialogResult = DialogResult.Yes;
            }
        }

        private void QuestionForm_Load(object sender, EventArgs e)
        {
            QuestionLabel.Text = questions[index].question;
        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            //Compare the answer from the text to the current question's answer
            if (questions[index].answer == AnswerTextBox.Text)
            {
                DidAnswerQuestion = true;
                index++;
                index %= questions.Count;
            }
            this.Close();
            
        }
    }
}
