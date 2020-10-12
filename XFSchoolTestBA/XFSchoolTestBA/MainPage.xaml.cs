using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XFSchoolTestBA.Models;

namespace XFSchoolTestBA
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            Entry login = new Entry
            {
                Placeholder = "add login",

            };



            Entry password = new Entry
            {
                Placeholder = "add password",

            };
            password.IsPassword = true;


            Button Reg = new Button()
            {
                Text = "Create account"
            };


            Button In = new Button()
            {
                Text = "login"
            };




           


            In.Clicked += async delegate
              {
                  App.Current.Properties["SaveLogin"] = login.Text;
                  App.Current.Properties["SavePassword"] = password.Text;
                  ApplicationViewModel applicationViewModel = new ApplicationViewModel();
                  await applicationViewModel.ChekUser(login.Text, password.Text);
                  await applicationViewModel.GetSubjects(login.Text, password.Text);
                  
                  var StudentPage = new StudentPage(applicationViewModel.user,applicationViewModel.subjects);
                  await Navigation.PushModalAsync(StudentPage);

              };

            Reg.Clicked += async delegate
            {
                ApplicationViewModel applicationViewModel = new ApplicationViewModel();
                await applicationViewModel.getGrades();
                var registerPage = new RegisterPage(applicationViewModel.Grades);
                await Navigation.PushModalAsync(registerPage);
            };


            StackLayout stackLayout = new StackLayout();
            stackLayout.Children.Add(login);
            stackLayout.Children.Add(password);
            stackLayout.Children.Add(Reg);
            stackLayout.Children.Add(In);

           


            Content = stackLayout;

            try
            {
                login.Text = App.Current.Properties["SaveLogin"].ToString();
                password.Text = App.Current.Properties["SavePassword"].ToString();
            }
            catch
            {

            }
        }
    }
}
