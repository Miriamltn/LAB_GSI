using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LAB_GSI
{
    /// <summary>
    /// Lógica de interacción para Juego3.xaml
    /// </summary>
    public partial class Juego3 : Window
    {
        private List<string> questions = new List<string>
        {
        "¿Cuál es la capital de Francia?",
        "¿Cuál es el río más largo del mundo?",
        "¿Cuál es el océano más grande?",
        "¿Quién escribió Romeo y Julieta?",
        "¿Cuál es el planeta más grande del sistema solar?",
        "¿Cuál es el hueso más largo del cuerpo humano?",
        "¿Quién pintó La última cena?",
        "¿Qué idioma se habla en Brasil?",
        "¿Cuál es la montaña más alta del mundo?",
        "¿Qué metal es líquido a temperatura ambiente?",
        "¿Cuál es el país más grande del mundo?",
        "¿Qué tipo de animal es la ballena?",
        "¿Quién fue el primer presidente de Estados Unidos?",
        "¿Cuál es el país más poblado del mundo?",
        "¿Quién escribió Don Quijote de la Mancha?"
        };

        private List<List<string>> answers = new List<List<string>>
        {
        new List<string> { "Londres", "Madrid", "París", "Roma" },
        new List<string> { "Nilo", "Amazonas", "Mississippi", "Yangtsé" },
        new List<string> { "Pacífico", "Atlántico", "Índico", "Ártico" },
        new List<string> { "William Shakespeare", "Charles Dickens", "Jane Austen", "Herman Melville" },
        new List<string> { "Júpiter", "Saturno", "Urano", "Neptuno" },
        new List<string> { "Fémur", "Húmero", "Radio", "Fíbula" },
        new List<string> { "Leonardo da Vinci", "Pablo Picasso", "Vincent van Gogh", "Michelangelo" },
        new List<string> { "Portugués", "Inglés", "Español", "Francés" },
        new List<string> { "Monte Everest", "Mont Blanc", "K2", "Annapurna" },
        new List<string> { "Mercurio", "Plomo", "Hierro", "Sodio" },
        new List<string> { "Rusia", "Canadá", "Estados Unidos", "China" },
        new List<string> { "Mamífero", "Reptil", "Ave", "Anfibio" },
        new List<string> { "George Washington", "Thomas Jefferson", "Abraham Lincoln", "John Adams" },
        new List<string> { "China", "India", "Estados Unidos", "Indonesia" },
        new List<string> { "Miguel de Cervantes", "Federico García Lorca", "Gabriel García Márquez", "Pablo Neruda" }
        };

        private List<string> correctAnswers = new List<string>
        {
        "París",
        "Amazonas",
        "Pacífico",
        "William Shakespeare",
        "Júpiter",
        "Fémur",
        "Leonardo da Vinci",
        "Portugués",
        "Monte Everest",
        "Mercurio",
        "Rusia",
        "Mamífero",
        "George Washington",
        "China",
        "Miguel de Cervantes"
        };

        private List<string> selectedAnswers = new List<string>(); // Lista para almacenar las respuestas seleccionadas

        private int currentQuestionIndex = -1;
        private int correctAnswersCount = 0;
        private int incorrectAnswersCount=0;

        public Juego3()
        {
            InitializeComponent();

            // Mostrar la primera pregunta
            ShowNextQuestion();
        }

        private void ShowNextQuestion()
        {
            // Mostrar la próxima pregunta
            currentQuestionIndex++;
            if (currentQuestionIndex < questions.Count)
            {
                questionTextBlock.Text = questions[currentQuestionIndex];
                optionsStackPanel.Children.Clear();
                foreach (string option in answers[currentQuestionIndex])
                {
                    RadioButton radioButton = new RadioButton();
                    radioButton.Content = option;
                    optionsStackPanel.Children.Add(radioButton);
                }
                checkButton.IsEnabled = true;
                resultTextBlock.Visibility = Visibility.Collapsed;
                nextButton.Visibility = Visibility.Collapsed;
                checkAllButton.Visibility = Visibility.Collapsed;
            }
            else
            {
                // Se acabaron las preguntas
                questionTextBlock.Text = "¡Fin del juego!";
                optionsStackPanel.Children.Clear();
                checkButton.IsEnabled = false;
                resultTextBlock.Visibility = Visibility.Collapsed;
                nextButton.Visibility = Visibility.Collapsed;
                checkAllButton.Visibility = Visibility.Visible;
            }
        }

        private void CheckAnswer()
        {
            // Verificar la respuesta actual
            if (currentQuestionIndex >= 0 && currentQuestionIndex < questions.Count)
            {
                string correctAnswer = correctAnswers[currentQuestionIndex];
                bool isAnswerCorrect = false; // Variable para verificar si la respuesta es correcta o incorrecta
                foreach (RadioButton radioButton in optionsStackPanel.Children.OfType<RadioButton>())
                {
                    if (radioButton.IsChecked == true)
                    {
                        string selectedAnswer = radioButton.Content.ToString();
                        selectedAnswers.Add(selectedAnswer); // Guardar la respuesta seleccionada
                        if (selectedAnswer == correctAnswer)
                        {
                            resultTextBlock.Text = "¡Respuesta correcta!";
                            resultTextBlock.Visibility = Visibility.Visible;
                            correctAnswersCount++;
                            isAnswerCorrect = true; // La respuesta es correcta
                        }
                        else
                        {
                            resultTextBlock.Text = "¡Respuesta incorrecta!";
                            resultTextBlock.Visibility = Visibility.Visible;
                        }
                        break;
                    }
                }

                // Incrementar el contador de respuestas incorrectas si la respuesta es incorrecta
                if (!isAnswerCorrect)
                {
                    incorrectAnswersCount++;
                }
            }
        }



        private void CheckButton_Click(object sender, RoutedEventArgs e)
        {
            CheckAnswer();
            nextButton.Visibility = Visibility.Visible;
        }

        private void CheckAllButton_Click(object sender, RoutedEventArgs e)
        {
            // Limpiar la lista de respuestas seleccionadas
            selectedAnswers.Clear();

            // Mostrar el resumen de respuestas
            string summary = "Resumen de respuestas:\n";
            int totalQuestions = questions.Count;
            int incorrectAnswersCount = totalQuestions - correctAnswersCount; // Calcular el recuento de respuestas incorrectas
            for (int i = 0; i < totalQuestions; i++)
            {
                string selectedOption = selectedAnswers.ElementAtOrDefault(i) ?? "Ninguna"; // Obtener la respuesta seleccionada
                summary += $"Pregunta {i + 1}: {questions[i]}\n  - Respuesta seleccionada: {selectedOption}\n";
                summary += $"  - Respuesta correcta: {correctAnswers[i]}\n"; // Mostrar la respuesta correcta
            }
            summary += $"\nTotal de respuestas correctas: {correctAnswersCount}\nTotal de respuestas incorrectas: {incorrectAnswersCount}";
            MessageBox.Show(summary, "Resumen de respuestas");
        }


        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            ShowNextQuestion();
        }
    }
}