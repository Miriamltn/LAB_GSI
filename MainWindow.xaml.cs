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
using System.Xml;

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
            CargarDefinicionDesdeXML();

        }
        private void CargarDefinicionDesdeXML()
        {
            string rutaArchivoXML = "../../Persistencia/datos.xml";

            try
            {
                // Crear un nuevo documento XML
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(rutaArchivoXML);

                // Obtener la sección "Definicion"
                XmlNode seccionDefinicion = xmlDoc.SelectSingleNode("/informacion/seccion[@nombre='Definicion']");

                if (seccionDefinicion != null)
                {
                    string textoDefinicion = seccionDefinicion.SelectSingleNode("texto").InnerText;

                    // Mostrar la definición en el TextBox tb_definicion
                    tb_definicion.Text = textoDefinicion;
                }
                else
                {
                    tb_definicion.Text = "Sección 'Definicion' no encontrada en el archivo XML.";
                }

                // Obtener la sección "Sintomas"
                XmlNode seccionSintomas = xmlDoc.SelectSingleNode("/informacion/seccion[@nombre='Sintomas']");

                if (seccionSintomas != null)
                {
                    // Obtener los elementos 'item' dentro de 'texto'
                    XmlNodeList sintomasNodes = seccionSintomas.SelectNodes("texto/item");

                    if (sintomasNodes != null && sintomasNodes.Count > 0)
                    {
                        // Construir el texto con los síntomas en líneas separadas
                        string textoSintomas = "";
                        foreach (XmlNode sintomaNode in sintomasNodes)
                        {
                            textoSintomas += sintomaNode.InnerText + Environment.NewLine;
                        }

                        // Mostrar los síntomas en el TextBox tb_sintomas
                        tb_sintomas.Text = textoSintomas.Trim();  // Trim para quitar el espacio adicional al final
                    }
                    else
                    {
                        tb_sintomas.Text = "Sección 'Sintomas' no encontrada en el archivo XML.";
                    }
                }
            }
            catch (Exception ex)
            {
                tb_definicion.Text = "Error al cargar los datos desde el archivo XML: " + ex.Message;
                tb_sintomas.Text = "Error al cargar los datos desde el archivo XML: " + ex.Message;
            }
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
