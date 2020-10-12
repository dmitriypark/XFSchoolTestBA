
using CarouselView.FormsPlugin.Abstractions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XFSchoolTestBA.Models;

namespace XFSchoolTestBA
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class QuestionPage : ContentPage
	{
		public QuestionPage (User user,IEnumerable<Questions> questions,int pageNumber,int [] answers)
		{
			InitializeComponent ();
            
            var qs = questions.ToList();
            var count = qs.Count();
            //int[] answers =  new int[count];
            //for (int i=0; i<count;i++)
            //{
            //    answers[i] = 0;
            //}
            //Application.Current.Properties["answers"] = answers;
            

            StackLayout stackLayout = new StackLayout();
            Entry answer = new Entry();
            Label progress = new Label();
            Label question = new Label();
            question.Text = qs[pageNumber].Content;
            Button next = new Button();
            next.Text = "next";
            Button previous = new Button();
            previous.Text = "previous";
            Button end = new Button();
            end.Text = "end";
            end.HorizontalOptions = LayoutOptions.End;

            answer.Placeholder = "enter answer";
            if (answers[pageNumber]!=0)
            {
                answer.Text = answers[pageNumber].ToString();
            }
            progress.Text = (pageNumber+1).ToString() + "/" + count.ToString();
            





           

            Grid grid = new Grid
            {
                RowDefinitions =
                {
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }                   
                },
                    ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }                    
                }
            };

            
            



            if (pageNumber != 0)
            {
                grid.Children.Add(previous, 0, 0);
            }

            if (pageNumber != count - 1)
            {
                grid.Children.Add(next, 1, 0);
            }


            



            stackLayout.Children.Add(progress);
            stackLayout.Children.Add(grid);
            stackLayout.Children.Add(answer);
            stackLayout.Children.Add(question);

            //if (pageNumber != 0)
            //{
            //    stackLayout.Children.Add(previous);
            //}

            //if (pageNumber != count-1)
            //{
            //    stackLayout.Children.Add(next);
            //}

            stackLayout.Children.Add(end);


            next.Clicked += delegate
              {
                  try
                  {
                      answers[pageNumber] = int.Parse(answer.Text);
                  }
                  catch
                  {

                  }
                  var QuestionPage = new QuestionPage(user, questions, pageNumber + 1,answers);
                  Navigation.PushModalAsync(QuestionPage);
              };

            previous.Clicked += delegate
            {
                var QuestionPage = new QuestionPage(user, questions, pageNumber - 1, answers);
                Navigation.PushModalAsync(QuestionPage);
            };


            end.Clicked += async delegate
             {

                 var displayAlert =await DisplayAlert("Confirm the action", "Did you want end test?", "Yes", "No" );
                 if (displayAlert==true)
                 {
                     Tasks taskToSave = new Tasks();
                     taskToSave.Test = int.Parse(App.Current.Properties["TestID"].ToString());
                     taskToSave.Start = DateTime.Parse(App.Current.Properties["Start"].ToString());
                     taskToSave.Finish = DateTime.Now;
                     int sum = 0;
                     int i = 0;

                     foreach (var q in questions)
                     {
                         if (q.Answer == answers[i])
                         {
                             sum++;
                         }
                         i++;
                     }
                     taskToSave.Sum = sum;
                     taskToSave.User = user.Id;
                     taskToSave.Pass = int.Parse(App.Current.Properties["Pass"].ToString());





                     ApplicationViewModel applicationViewModel = new ApplicationViewModel();
                     await applicationViewModel.AddTasks(taskToSave);

                     int j = 0;
                     foreach (var q in questions)
                     {
                         TaskAnswers taskAnswers = new TaskAnswers();
                         taskAnswers.Task = applicationViewModel.GetTasksIdForTaskAnswers.Id;
                         taskAnswers.StudentAnswer = answers[j];
                         if (answers[j] == q.Answer)
                         {
                             taskAnswers.CorrectAnswer = 1;
                         }
                         else
                         {
                             taskAnswers.CorrectAnswer = 0;
                         }

                         taskAnswers.Question = q.Id;
                         j++;
                         await applicationViewModel.AddTaskAnswers(taskAnswers);
                     }


                     await applicationViewModel.ChekUser(user.Login, user.Password);
                     await applicationViewModel.GetSubjects(user.Login, user.Password);
                     var StudentPage = new StudentPage(user, applicationViewModel.subjects);
                     await Navigation.PushModalAsync(StudentPage);
                 }
                 
             };


            Content = stackLayout;

            
        }
	}
}