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
    public partial class StudentPage : ContentPage
    {
        
        public StudentPage(User user, IEnumerable<Subjects> subjects)
        {

            InitializeComponent();
                                      
            


            TableView tableView = new TableView();

            foreach (var subject in subjects)
            {
                TableSection tableSection = new TableSection();
                TextCell textCell = new TextCell();
                tableSection.Add(textCell);
                tableView.Root.Add(tableSection);
                textCell.Text = subject.Name.ToString();
                textCell.Tapped += async delegate
                {
                    ApplicationViewModel applicationViewModel = new ApplicationViewModel();
                    await applicationViewModel.GetTests(user.Login, user.Password, subject.Id);
                    
                    var TestPage = new TestPage(user,applicationViewModel.tests);
                    await Navigation.PushModalAsync(TestPage);
                };

            }


            TableSection tableSectionPassed = new TableSection();
            TextCell textCellPassed = new TextCell();
            tableSectionPassed.Add(textCellPassed);
            tableView.Root.Add(tableSectionPassed);
            textCellPassed.Text = "Passed Tests";
            textCellPassed.TextColor = Color.Green;
            textCellPassed.Tapped += async delegate
            {
                ApplicationViewModel applicationViewModel = new ApplicationViewModel();
                await applicationViewModel.GetTasksName(user.Login, user.Password);
                await applicationViewModel.GetTasks(user.Login, user.Password);
                var passedTestPage  = new PassedTestPage(user, applicationViewModel.tasks, applicationViewModel.TasksName);
                await Navigation.PushModalAsync(passedTestPage);
            };



            //TableSection tableSection = new TableSection();
            //TextCell textCell = new TextCell();
            //tableSection.Add(textCell);
            //tableView.Root.Add(tableSection);
            //textCell.Text = user.FullName.ToString();
            //textCell.Tapped += TextCell_Tapped;

            Content = tableView;


        }

        
    }
}