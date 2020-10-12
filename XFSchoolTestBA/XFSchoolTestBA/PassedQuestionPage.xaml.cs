using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XFSchoolTestBA.Models;

namespace XFSchoolTestBA
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PassedQuestionPage : ContentPage
	{
		public PassedQuestionPage (User user, Tasks task,IEnumerable<Questions> questions,IEnumerable <TaskAnswers> taskAnswers)
		{
			InitializeComponent ();

            InitializeComponent();
            TableView tableView = new TableView();

            foreach (var question in questions)
            {
                TableSection tableSection = new TableSection();
                                                                                                                   
                TextCell textCellQuestion = new TextCell();
                TextCell textCellCorrect = new TextCell();
                tableSection.Add(textCellQuestion);
                
                textCellQuestion.Text = question.Content;
                if (taskAnswers.ToList().Where(t=>t.Question==question.Id).FirstOrDefault().CorrectAnswer==1)
                {
                    textCellCorrect.TextColor = Color.Green;
                    textCellCorrect.Text = "correct";
                }
                else
                {
                    textCellCorrect.TextColor = Color.Red;
                    textCellCorrect.Text = "incorrectly";
                }
                tableSection.Add(textCellCorrect);
                tableView.Root.Add(tableSection);
            }


            




            Content = tableView;


        }
	}
}