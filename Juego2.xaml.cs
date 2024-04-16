using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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
    /// Lógica de interacción para Juego2.xaml
    /// </summary>
    public partial class Juego2 : Window
    {
        private List<int> sequence = new List<int>(); // La secuencia de colores y sonidos
        private int currentIndex = 0; // Índice actual en la secuencia que el jugador debe recordar
        private bool awaitingInput = false; // Flag para esperar la entrada del jugador
        private int correctCount = 0; // Contador de aciertos
        private int wrongCount = 0; // Contador de fallos
        private Dictionary<int, SolidColorBrush> colorMappings = new Dictionary<int, SolidColorBrush>
        {
            { 0, Brushes.Red },
            { 1, Brushes.Blue },
            { 2, Brushes.Green },
            { 3, Brushes.Yellow }
        };

        private Dictionary<int, string> soundMappings = new Dictionary<int, string>
        {
            { 0, "red.wav" },
            { 1, "blue.wav" },
            { 2, "green.wav" },
            { 3, "yellow.wav" }
        };
        public Juego2()
        {
            InitializeComponent();
        }
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            StartGame();
            btn_empezar.IsEnabled = false; // Deshabilitar el botón Start
        }

        private void StartGame()
        {
            sequence.Clear();
            currentIndex = 0;
            awaitingInput = false;
            StatusLabel.Text = "Watch the sequence and repeat it.";

            // Generar una secuencia de 5 colores aleatorios
            Random random = new Random();
            for (int i = 0; i < 5; i++)
            {
                sequence.Add(random.Next(0, 4)); // 0 = Rojo, 1 = Azul, 2 = Verde, 3 = Amarillo
            }

            // Comenzar a reproducir la secuencia
            PlaySequence();
        }

        private async void PlaySequence()
        {
            awaitingInput = false;
            foreach (int colorIndex in sequence)
            {
                await HighlightButton(colorIndex);
                await Task.Delay(1000); // Esperar 1 segundo entre cada color
            }
            awaitingInput = true;

            // Mostrar botones de colores para que el jugador repita la secuencia
            ShowColorButtons();
        }

        private async Task HighlightButton(int colorIndex)
        {
            SolidColorBrush color = colorMappings[colorIndex];
            Button button = new Button
            {
                Background = color,
                Width = 100,
                Height = 100,
                Margin = new Thickness(5),
                Tag = colorIndex // Usamos el Tag para almacenar el color asociado al botón
            };

            button.Click += Button_Click; // Asociamos el evento Click

            GamePanel.Children.Add(button);
            PlaySound(soundMappings[colorIndex]);

            await Task.Delay(800); // Mantener resaltado durante 0.8 segundos
            GamePanel.Children.Remove(button);
        }

        private void ShowColorButtons()
        {
            ButtonPanel.Children.Clear();
            foreach (int colorIndex in colorMappings.Keys)
            {
                SolidColorBrush color = colorMappings[colorIndex];
                Button button = new Button
                {
                    Background = color,
                    Width = 60,
                    Height = 60,
                    Margin = new Thickness(5),
                    Tag = colorIndex
                };
                button.Click += ColorButton_Click; // Asociar evento Click para comprobar la secuencia
                ButtonPanel.Children.Add(button);
            }
        }

        private void PlaySound(string soundFile)
        {
            try
            {
                using (SoundPlayer player = new SoundPlayer(soundFile))
                {
                    player.Load();
                    player.Play();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al reproducir sonido: " + ex.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!awaitingInput) return;

            Button clickedButton = (Button)sender;
            int clickedColor = (int)clickedButton.Tag; // Obtenemos el color asociado al botón

            if (clickedColor == sequence[currentIndex])
            {
                currentIndex++;
                if (currentIndex == sequence.Count)
                {
                    // El jugador ha completado la secuencia correctamente
                    StatusLabel.Text = "¡Correcto! Siguiente nivel.";
                    correctCount++;
                    CorrectCount.Text = correctCount.ToString(); // Actualizar contador de aciertos
                    StartGame(); // Pasar al siguiente nivel
                }
            }
            else
            {
                // El jugador se equivocó, reiniciar el juego
                StatusLabel.Text = "¡Incorrecto! Inténtalo de nuevo.";
                wrongCount++;
                WrongCount.Text = wrongCount.ToString(); // Actualizar contador de fallos
                StartGame();
            }
        }

        private void ColorButton_Click(object sender, RoutedEventArgs e)
        {
            if (!awaitingInput) return;

            Button clickedButton = (Button)sender;
            int clickedColor = (int)clickedButton.Tag; // Obtenemos el color asociado al botón

            if (clickedColor == sequence[currentIndex])
            {
                currentIndex++;
                if (currentIndex == sequence.Count)
                {
                    // El jugador ha completado la secuencia correctamente
                    StatusLabel.Text = "¡Correcto! Siguiente nivel.";
                    correctCount++;
                    CorrectCount.Text = correctCount.ToString(); // Actualizar contador de aciertos
                    StartGame(); // Pasar al siguiente nivel
                }
            }
            else
            {
                // El jugador se equivocó, reiniciar el juego
                StatusLabel.Text = "¡Incorrecto! Inténtalo de nuevo.";
                wrongCount++;
                WrongCount.Text = wrongCount.ToString(); // Actualizar contador de fallos
                StartGame();
            }
        }
    }
}