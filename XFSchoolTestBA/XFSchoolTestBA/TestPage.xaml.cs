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
    public partial class TestPage : ContentPage
    {
        public TestPage(User user,IEnumerable<Tests> tests)
        {
            InitializeComponent();
            TableView tableView = new TableView();

            foreach (var test in tests)
            {
                TableSection tableSection = new TableSection();
                TextCell textCell = new TextCell();
                tableSection.Add(textCell);
                tableView.Root.Add(tableSection);
                textCell.Text = test.Name.ToString();
                textCell.Tapped += async delegate
                {
                    App.Current.Properties["Pass"] = test.QuantityPass;
                    App.Current.Properties["Start"] = DateTime.Now;
                    App.Current.Properties["TestID"] = test.Id;
                    ApplicationViewModel applicationViewModel = new ApplicationViewModel();
                    await applicationViewModel.GetQuestions(user.Login, user.Password, test.Id);
                    var QuestionPage = new QuestionPage(user,applicationViewModel.questions,0,new int [applicationViewModel.questions.Count()]);
                    await Navigation.PushModalAsync(QuestionPage);
                };

            }
            Content = tableView;
        }
    }
}