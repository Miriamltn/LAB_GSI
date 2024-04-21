using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Lógica de interacción para Juego.xaml
    /// </summary>
    public partial class Juego : Window
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private List<string> animalNames = new List<string>
        {
            "Perro", "Gato", "Pájaro", "Elefante",
            "León", "Cebra", "Oso", "Tigre"
        };

        private List<Card> cards;
        private Card selectedCard;
        private bool isBusy;

        public ICommand CardClickedCommand { get; }

        public ObservableCollection<Card> Cards
        {
            get => new ObservableCollection<Card>(cards);
        }

        public Juego()
        {
            InitializeComponent();
            DataContext = this;

            CardClickedCommand = new RelayCommand<Card>(CardClicked, () => !isBusy);
            AllCardsMatched = false;
            InitializeGame();
        }

        private void InitializeGame()
        {
            cards = new List<Card>();
            var randomColors = GenerateRandomColors(animalNames.Count / 2);

            for (int i = 0; i < animalNames.Count; i++)
            {
                cards.Add(new Card(animalNames[i]));
                cards.Add(new Card(animalNames[i]));
            }

            Shuffle(cards);
        }

        private void Shuffle<T>(IList<T> list)
        {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        private List<Color> GenerateRandomColors(int count)
        {
            List<Color> colors = new List<Color>();
            Random rand = new Random();

            for (int i = 0; i < count; i++)
            {
                colors.Add(Color.FromRgb((byte)rand.Next(256), (byte)rand.Next(256), (byte)rand.Next(256)));
            }

            return colors;
        }

        private void CardClicked(Card clickedCard)
        {
            if (isBusy)
                return;

            if (clickedCard.IsFlipped || clickedCard.IsMatched)
                return;

            clickedCard.IsFlipped = true;

            if (selectedCard == null)
            {
                selectedCard = clickedCard;
                return;
            }

            isBusy = true;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CardClickedCommand)));

            if (selectedCard.Animal == clickedCard.Animal)
            {
                selectedCard.IsMatched = true;
                clickedCard.IsMatched = true;
                selectedCard = null;
                isBusy = false;

                // Verificar si todas las cartas están emparejadas
                if (cards.All(card => card.IsMatched))
                {
                    AllCardsMatched = true;
                    MessageBox.Show("¡Ganaste!");
                }
            }
            else
            {
                Task.Delay(TimeSpan.FromSeconds(1)).ContinueWith(_ =>
                {
                    selectedCard.IsFlipped = false;
                    clickedCard.IsFlipped = false;
                    selectedCard = null;
                    isBusy = false;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CardClickedCommand)));
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Cards)));
        }

        private bool allCardsMatched;
        public bool AllCardsMatched
        {
            get => allCardsMatched;
            set
            {
                allCardsMatched = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AllCardsMatched)));
            }
        }
    }

    public class Card : INotifyPropertyChanged
    {
        private string animal;
        private bool isFlipped;
        private bool isMatched;

        public string Animal
        {
            get => animal;
            set
            {
                animal = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Animal)));
            }
        }

        public bool IsFlipped
        {
            get => isFlipped;
            set
            {
                isFlipped = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsFlipped)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DisplayValue))); // Notify DisplayValue change
            }
        }

        public bool IsMatched
        {
            get => isMatched;
            set
            {
                isMatched = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsMatched)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DisplayValue))); // Notify DisplayValue change
            }
        }

        public string DisplayValue => IsFlipped || IsMatched ? Animal : "🂠";

        public Color BackgroundColor { get; internal set; }

        public Card(string animal)
        {
            Animal = animal;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T> execute;
        private readonly Func<bool> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action<T> execute, Func<bool> canExecute = null)
        {
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return canExecute == null || canExecute();
        }

        public void Execute(object parameter)
        {
            execute((T)parameter);
        }
    }
}
