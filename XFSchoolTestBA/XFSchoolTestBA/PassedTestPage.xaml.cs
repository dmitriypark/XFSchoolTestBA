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
	public partial class PassedTestPage : ContentPage
	{
		public PassedTestPage (User user, IEnumerable<Tasks> tasks,IEnumerable<Tests> tasksName)
		{
			InitializeComponent ();
            TableView tableView = new TableView();
            int i = 0;
            List<Tasks> tasksList = tasks.ToList();
            foreach (var taskName in tasksName)
            {
                TableSection tableSection = new TableSection();
                TextCell textCell = new TextCell();
                
                tableSection.Add(textCell);
                
                tableView.Root.Add(tableSection);
                textCell.Text = taskName.Name;
                
                if (tasksList[i].Sum>= tasksList[i].Pass)
                {
                    textCell.TextColor = Color.Green;
                }
                else
                {
                    textCell.TextColor = Color.Red;
                }

                textCell.Tapped += async delegate
                {
                    ApplicationViewModel applicationViewModel = new ApplicationViewModel();
                    
                    await applicationViewModel.GetTaskAnswersQuestion(user.Login, user.Password, tasks.ToList().Where(t => t.Test == taskName.Id).FirstOrDefault().Id);
                    await applicationViewModel.GetTaskAnswers(user.Login, user.Password, tasks.ToList().Where(t => t.Test == taskName.Id).FirstOrDefault().Id);
                    var passedQuestionPage = new PassedQuestionPage(user, tasks.ToList().Where(t => t.Test == taskName.Id).FirstOrDefault(),applicationViewModel.taskAnswersQuestion, applicationViewModel.taskAnswers);
                    await Navigation.PushModalAsync(passedQuestionPage);
                };

            }

            Content = tableView;


        }
	}
}