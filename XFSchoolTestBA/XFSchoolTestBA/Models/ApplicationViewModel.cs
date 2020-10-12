using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XFSchoolTestBA.Models
{
    class ApplicationViewModel
    {
        

        public User user { get; set; }
        public IEnumerable<Subjects> subjects { get; set; }
        public IEnumerable<Tests> tests { get; set; }
        public IEnumerable<Questions> questions { get; set; }
        public Tasks task { get; set; }
        public IEnumerable<Tasks> tasks { get; set; }
        public IEnumerable<TaskAnswers> taskAnswers { get; set; }
        public IEnumerable<Tests> TasksName { get; set; }
        public IEnumerable<Questions> taskAnswersQuestion { get; set; }
        public Tasks GetTasksIdForTaskAnswers { get; set; }
        public IEnumerable <Grade> Grades { get; set; }


        public async Task ChekUser(string login, string password)
        {
            UserService userService = new UserService();
            user = await userService.GetCheck(login, password);

        }

        public async Task GetSubjects (string login,string password)
        {
            SubjectsService subjectsService = new SubjectsService();
            subjects = await subjectsService.GetSubjects(login, password);

            
        }


        public async Task GetTests(string login, string password, int id)
        {
            TestsService testsService = new TestsService();
            tests = await testsService.GetTests(login, password, id);


        }


        public async Task GetQuestions(string login, string password, int id)
        {
            QuestionsService questionsService = new QuestionsService();
            questions = await questionsService.GetQuestions(login, password, id);


        }


        public async Task GetTasks(string login, string password)
        {
            TasksService tasksService = new TasksService();
            tasks = await tasksService.GetTasks(login, password);


        }


        public async Task GetTaskID(string login, string password,int id)
        {
            TasksService tasksService = new TasksService();
            task = await tasksService.GetTasksID(login, password, id);


        }



        public async Task AddTasks(Tasks task)
        {
            TasksService tasksService = new TasksService();
            GetTasksIdForTaskAnswers = await tasksService.AddTask(task);


        }


        public async Task GetTasksName(string login, string password)
        {
            TasksService tasksService = new TasksService();
            TasksName = await tasksService.GetTaskName(login, password);


        }




        public async Task GetTaskAnswers(string login, string password, int taskId)
        {
            TaskAnswersService taskAnswersService = new TaskAnswersService();
            taskAnswers = await taskAnswersService.GetTaskAnswers(login, password, taskId);


        }



        public async Task GetTaskAnswersQuestion(string login, string password, int taskId)
        {
            TaskAnswersService taskAnswersService = new TaskAnswersService();
            taskAnswersQuestion = await taskAnswersService.GetTaskAnswersQuestion(login, password, taskId);


        }


        public async Task AddTaskAnswers(TaskAnswers taskAnswers)
        {
            TaskAnswersService tasksService = new TaskAnswersService();
            await tasksService.AddTaskAnswers(taskAnswers);


        }

        public async Task getGrades ()
        {
            GradeService gradeService = new GradeService();
            Grades = await gradeService.Get();
        }


        public async Task<User> AddUser(User user)
        {
            UserService userService = new UserService();
            return await userService.Add(user);
        }


        public async Task AddStudentsGrade (StudentsGrade studentsGrade)
        {
            StudentsGradeService studentsGradeService = new StudentsGradeService();
            await studentsGradeService.Add(studentsGrade);
        }
        



    }
}
