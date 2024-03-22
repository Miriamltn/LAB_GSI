using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LAB_GSI
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            toggleButton1.Checked += ToggleButton_Checked;
            toggleButton1.Unchecked += ToggleButton_Unchecked;
            stackPanel1.Visibility = Visibility.Collapsed;
        }

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            stackPanel1.Visibility = Visibility.Visible;
            // Aquí puedes realizar acciones adicionales cuando se muestra el contenido
        }

        private void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            stackPanel1.Visibility = Visibility.Collapsed;
            // Aquí puedes realizar acciones adicionales cuando se oculta el contenido
        }
      
        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Verificar si la pestaña seleccionada es "Dudas"
            if (P_Dudas.IsSelected)
            {
                stackPanel1.Visibility = Visibility.Visible;
            }
            else
            {
                stackPanel1.Visibility = Visibility.Collapsed;
            }
        }


    }
}
