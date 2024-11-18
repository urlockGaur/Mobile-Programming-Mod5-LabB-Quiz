using Mod5_LabB_Quiz.Models;
using System;
using System.Collections.Generic;

namespace Mod5_LabB_Quiz
{
    public partial class MainPage : ContentPage
    {
        private int _currentQuestionIndex = 0;
        private int _score = 0;
        private List<Question> _questions;

        public MainPage()
        {
            InitializeComponent();
            LoadQuestions();
        }

        private void LoadQuestions()
        {
            // Fetch questions from the database
            _questions = App.QuestionRepo.GetQuestions();

            if (_questions == null || _questions.Count == 0)
            {
                DisplayAlert("Error", "No questions found in the database.", "OK");
                StartQuizButton.IsEnabled = false;
            }
            else
            {
                StartQuizButton.IsEnabled = true;
            }
        }

        private void OnStartQuizClicked(object sender, EventArgs e)
        {
            _currentQuestionIndex = 0;
            _score = 0;
            QuizContent.IsVisible = true; // Show quiz content
            ResultImage.IsVisible = false; // Hide result image
            ResetButton.IsVisible = false; // Hide reset button
            StartQuizButton.IsVisible = false; // Hide start button
            DisplayCurrentQuestion();
        }

        private void DisplayCurrentQuestion()
        {
            if (_currentQuestionIndex < _questions.Count)
            {
                var question = _questions[_currentQuestionIndex];
                QuestionLabel.Text = question.Text;
                QuestionImage.Source = question.ImagePath;
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

           
            if (userAnswer)
            {
                _score++; 
            }

            _currentQuestionIndex++;
            DisplayCurrentQuestion();
        }
        private void ShowResults()
        {
            string result;
            string imagePath;

            if (_score <= 1)
            {
                result = "You are like Frodo: brave and determined!";
                imagePath = "frodo.png";
            }
            else if (_score <= 3)
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
            ResultImage.Source = imagePath;
            ResultImage.IsVisible = true;
            ResetButton.IsVisible = true;

            DisplayAlert("Quiz Result", result, "OK");
        }
        private void OnSwiped(object sender, SwipedEventArgs e)
        {
            if (_questions == null || _questions.Count == 0)
                return;

            switch (e.Direction)
            {
                case SwipeDirection.Left:
                    _currentQuestionIndex = (_currentQuestionIndex + 1) % _questions.Count;
                    break;
                case SwipeDirection.Right:
                    _currentQuestionIndex = (_currentQuestionIndex - 1 + _questions.Count) % _questions.Count;
                    break;
            }

            DisplayCurrentQuestion();
        }

        private void OnResetClicked(object sender, EventArgs e)
        {
            _currentQuestionIndex = 0;
            _score = 0;

            ResultImage.IsVisible = false;
            ResetButton.IsVisible = false;
            StartQuizButton.IsVisible = true;
            QuizContent.IsVisible = false;
        }
    }
}
