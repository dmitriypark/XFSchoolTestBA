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
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage(IEnumerable<Grade> grades)
        {
            InitializeComponent();
            StackLayout stackLayout = new StackLayout();
            Entry login = new Entry();
            login.Placeholder = "enter login";
            Entry name = new Entry();
            name.Placeholder = "enter full name and family";

            Entry password = new Entry
            {
                Placeholder = "add password",
            };
            password.IsPassword = true;


            Entry passwordVerify = new Entry
            {
                Placeholder = "verify password",
            };
            password.IsPassword = true;


            Picker gradeNumber = new Picker
            {
                Title = "grade"
            };
            foreach (var grade in grades)
            {
                gradeNumber.Items.Add(grade.Number.ToString());
            }

            Picker gradeName = new Picker();
            gradeName.Title = "grade name";




            gradeNumber.SelectedIndexChanged+=delegate
            {
                gradeName.Items.Clear();
                var gradeNames = grades.ToList().Where(g => g.Number == int.Parse(gradeNumber.Items[gradeNumber.SelectedIndex]));
                foreach (var grade in gradeNames)
                {
                    gradeName.Items.Add(grade.Name.ToString());
                }
            };


            Button confirm = new Button();
            confirm.Clicked += async delegate
             {
                 StudentsGrade studentsGrade = new StudentsGrade();
                 User user = new User();
                 if (password.Text==passwordVerify.Text)
                 {
                     App.Current.Properties["SaveLogin"] = login.Text;
                     App.Current.Properties["SavePassword"] = password.Text;
                     ApplicationViewModel applicationViewModel = new ApplicationViewModel();
                     var grade = grades.ToList().Where(g => g.Name == gradeName.Items[gradeName.SelectedIndex] && g.Number == int.Parse(gradeNumber.Items[gradeNumber.SelectedIndex])).FirstOrDefault();
                     user.Login = login.Text;
                     user.FullName = name.Text;
                     user.Password = password.Text;
                     user.Roles = 1;
                     studentsGrade.Grade = grade.Id;
                     var userForID = await applicationViewModel.AddUser(user);
                     studentsGrade.User = userForID.Id;
                     await applicationViewModel.AddStudentsGrade(studentsGrade);


                     await applicationViewModel.ChekUser(login.Text, password.Text);
                     await applicationViewModel.GetSubjects(login.Text, password.Text);

                     var StudentPage = new StudentPage(applicationViewModel.user, applicationViewModel.subjects);
                     await Navigation.PushModalAsync(StudentPage);



                 }
                 else
                 {
                     await DisplayAlert("password not coincide", "verify password","ok");
                 }
             };
            confirm.Text = "confirm";

            stackLayout.Children.Add(login);
            stackLayout.Children.Add(name);
            stackLayout.Children.Add(password);
            stackLayout.Children.Add(passwordVerify);
            stackLayout.Children.Add(gradeNumber);
            stackLayout.Children.Add(gradeName);
            stackLayout.Children.Add(confirm);

            Content = stackLayout;



        }
    }
}