using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Lógica de interacción para Juego5.xaml
    /// </summary>
    public partial class Juego5 : Window
    {
        public JuegoViewModel ViewModel { get; set; }

        public Juego5()
        {
            InitializeComponent();
            ViewModel = new JuegoViewModel();
            DataContext = ViewModel;
        }

        private void ConceptsListBox_Drop(object sender, DragEventArgs e)
        {
            HandleDrop(sender, e, "ConceptsListBox");
        }

        private void AnimalsListBox_Drop(object sender, DragEventArgs e)
        {
            HandleDrop(sender, e, "AnimalsListBox");
        }

        private void FurnitureListBox_Drop(object sender, DragEventArgs e)
        {
            HandleDrop(sender, e, "FurnitureListBox");
        }

        private void LandscapeListBox_Drop(object sender, DragEventArgs e)
        {
            HandleDrop(sender, e, "LandscapeListBox");
        }

        private void HandleDrop(object sender, DragEventArgs e, string containerName)
        {
            if (e.Data.GetDataPresent(DataFormats.StringFormat))
            {
                string concept = e.Data.GetData(DataFormats.StringFormat) as string;
                if (concept != null)
                {
                    var listBox = sender as ListBox;
                    var container = listBox.ItemsSource as ObservableCollection<string>;
                    container.Add(concept);

                    // Si la gota proviene del contenedor general, eliminamos el elemento de la lista general
                    if (containerName == "ConceptsListBox")
                    {
                        var sourceContainer = ConceptsListBox.ItemsSource as ObservableCollection<string>;
                        sourceContainer.Remove(concept);
                    }
                }
            }
        }


        private void Item_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is TextBlock textBlock && textBlock.DataContext is string concept)
            {
                DragDrop.DoDragDrop(textBlock, concept, DragDropEffects.Copy);
            }
        }

        public class JuegoViewModel : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;

            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

            public ObservableCollection<string> Concepts { get; } = new ObservableCollection<string>
        {
            "Perro", "Mesa", "Silla", "Gato", "Montaña", "Lago"
        };

            public ObservableCollection<string> Animals { get; } = new ObservableCollection<string>();
            public ObservableCollection<string> Furniture { get; } = new ObservableCollection<string>();
            public ObservableCollection<string> Landscape { get; } = new ObservableCollection<string>();

            public ICommand CheckCommand { get; }

            public JuegoViewModel()
            {
                CheckCommand = new RelayCommand(CheckSolution);
            }

            private void CheckSolution(object parameter)
            {
                if (Animals.Count == 2 && Furniture.Count == 2 && Landscape.Count == 2 &&
                    Animals.Contains("Perro") && Animals.Contains("Gato") &&
                    Furniture.Contains("Mesa") && Furniture.Contains("Silla") &&
                    Landscape.Contains("Montaña") && Landscape.Contains("Lago"))
                {
                    MessageBox.Show("¡Correcto! Has completado el juego.");
                }
                else
                {
                    MessageBox.Show("Incorrecto. Por favor, intenta nuevamente.");
                }
            }
        }

        public class RelayCommand : ICommand
        {
            private readonly Action<object> _execute;
            private readonly Predicate<object> _canExecute;

            public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
            {
                _execute = execute ?? throw new ArgumentNullException(nameof(execute));
                _canExecute = canExecute;
            }

            public event EventHandler CanExecuteChanged
            {
                add { CommandManager.RequerySuggested += value; }
                remove { CommandManager.RequerySuggested -= value; }
            }

            public bool CanExecute(object parameter)
            {
                return _canExecute == null || _canExecute(parameter);
            }

            public void Execute(object parameter)
            {
                _execute(parameter);
            }
        }
    }
}