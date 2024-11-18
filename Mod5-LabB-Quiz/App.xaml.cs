using System.IO;

namespace Mod5_LabB_Quiz
{
    public partial class App : Application
    {
        public static QuestionRepository QuestionRepo { get; private set; }

        public App()
        {
            InitializeComponent();

            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "quiz.db3");
            QuestionRepo = new QuestionRepository(dbPath);

            MainPage = new AppShell();
        }
    }
}