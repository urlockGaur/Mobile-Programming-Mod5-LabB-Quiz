using SQLite;
using Mod5_LabB_Quiz.Models;
using System.Collections.Generic;

namespace Mod5_LabB_Quiz
{
    public class QuestionRepository
    {
        private readonly SQLiteConnection _database;

        public QuestionRepository(string dbPath)
        {
            _database = new SQLiteConnection(dbPath);
            _database.CreateTable<Question>();

            // Seed some questions if the table is empty
            if (!_database.Table<Question>().Any())
            {
                _database.Insert(new Question { Text = "Do you enjoy adventure?", ImagePath = "adventure.png", Answer = true });
                _database.Insert(new Question { Text = "Do you prefer quiet places?", ImagePath = "quiet.png", Answer = true });
                _database.Insert(new Question { Text = "Is strength more important than wisdom?", ImagePath = "strength.png", Answer = false });
                _database.Insert(new Question { Text = "Do you enjoy puzzles?", ImagePath = "puzzles.png", Answer = true });
                _database.Insert(new Question { Text = "Would you help a stranger in need?", ImagePath = "help.png", Answer = true });
            }
        }

        public List<Question> GetQuestions()
        {
            return _database.Table<Question>().ToList();
        }
    }
}