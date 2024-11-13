namespace Mod5_LabB_Quiz
{
    public partial class MainPage : ContentPage
    {
        private int _currentQuestionIndex = 0;
        private int _score = 0;

        private List<(string Question, string ImagePath, bool Answer)> _questions = new List<(string, string, bool)>
        {
            ("Do you enjoy adventure?", "adventure.png", true),
            ("Do you prefer quiet places?", "quiet.png", true),
            ("Is strength more important than wisdom?", "strength.png", true),
            ("Do you enjoy puzzles?", "puzzles.png", true),
            ("Would you help a stranger in need?", "help.png", true),
        };

        public MainPage()
        {
            InitializeComponent();
            LoadQuestion();
        }

        private void LoadQuestion()
        {
            if(_currentQuestionIndex < _questions.Count)
            {
                var (question, imagePath, _) = _questions[_currentQuestionIndex];
                QuestionLabel.Text = question;
                QuestionImage.Source = imagePath;
            }
            else
            {
                ShowResults();
            }
        }

       private void OnAnswerClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            bool userAnswer = button.Text == "True";
            bool correctAnswer = _questions[_currentQuestionIndex].Answer;

            if (userAnswer == correctAnswer)
            {
                _score++;
            }

            _currentQuestionIndex++;
            LoadQuestion();
        }

        private void ShowResults()
        {
            string result;
            string imagePath;

            if (_score <= 2)
            {
                result = "You are like Frodo: brave and determined!";
                imagePath = "frodo.png"; 
            }
            else if (_score <= 4)
            {
                result = "You are like Sam: loyal and strong!";
                imagePath = "sam.png"; 
            }
            else
            {
                result = "You are like Gandalf: wise and powerful!";
                imagePath = "gandalf.png";
            }

            QuizContent.IsVisible = false;
            // Set the image source and make it visible
            ResultImage.Source = imagePath;
            ResultImage.IsVisible = true;

            ResetButton.IsVisible = true;
            // Show the result message in an alert
            DisplayAlert("Quiz Result", result, "OK");
        }

        private void OnSwiped(object sender, SwipedEventArgs e)
        {
            switch (e.Direction)
            {
                case SwipeDirection.Left:
                    // Handle left swipe: 
                    _currentQuestionIndex++;
                    if (_currentQuestionIndex >= _questions.Count)
                        _currentQuestionIndex = 0; 
                    LoadQuestion();
                    break;

                case SwipeDirection.Right:
                    // Handle right swipe: 
                    _currentQuestionIndex--;
                    if (_currentQuestionIndex < 0)
                        _currentQuestionIndex = _questions.Count - 1; 
                    LoadQuestion();
                    break;
            }
        }
        private void OnResetClicked(object sender, EventArgs e)
        {
            // Reset the quiz state
            _currentQuestionIndex = 0;
            _score = 0;

            // Hide the result image and Reset button
            ResultImage.IsVisible = false;
            ResetButton.IsVisible = false;

            // Show the quiz content
            QuizContent.IsVisible = true;

            // Load the first question
            LoadQuestion();
        }
    }

}
