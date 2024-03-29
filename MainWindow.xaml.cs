using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Xml;

namespace LAB_GSI
{
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

                // Obtener la sección "Fases"
                XmlNode seccionFases = xmlDoc.SelectSingleNode("/informacion/seccion[@nombre='Fases']");

                if (seccionFases != null)
                {
                    // Obtener todos los nodos "etapa" dentro de "Fases"
                    XmlNodeList etapasNodes = seccionFases.SelectNodes("etapa");

                    if (etapasNodes != null && etapasNodes.Count > 0)
                    {
                        // Construir el texto con las fases en líneas separadas
                        string textoFases = "";
                        foreach (XmlNode etapaNode in etapasNodes)
                        {
                            string nombreEtapa = etapaNode.SelectSingleNode("nombre").InnerText;
                            string descripcionEtapa = etapaNode.SelectSingleNode("descripcion").InnerText;

                            textoFases += $"{nombreEtapa}: {descripcionEtapa}" + Environment.NewLine + Environment.NewLine;
                        }

                        // Mostrar las fases en el TextBox tb_fases
                        tb_fases.Text = textoFases.Trim();  // Trim para quitar el espacio adicional al final
                    }
                    else
                    {
                        tb_fases.Text = "No se encontraron fases en el archivo XML.";
                    }
                }
                XmlNode seccionTratamientos = xmlDoc.SelectSingleNode("/informacion/seccion[@nombre='Tratamientos']");

                if (seccionTratamientos != null)
                {
                    // Obtener los elementos 'tratamiento' dentro de 'Tratamientos'
                    XmlNodeList tratamientosNodes = seccionTratamientos.SelectNodes("etapa");

                    if (tratamientosNodes != null && tratamientosNodes.Count > 0)
                    {
                        // Construir el texto con los tratamientos en líneas separadas
                        string textoTratamientos = "";
                        foreach (XmlNode tratamientoNode in tratamientosNodes)
                        {
                            string nombreTratamiento = tratamientoNode.SelectSingleNode("nombre").InnerText ;
                            string descripcionTratamiento = tratamientoNode.SelectSingleNode("descripcion").InnerText;

                            textoTratamientos += $"{nombreTratamiento}: {descripcionTratamiento}" + Environment.NewLine + Environment.NewLine;
                        }

                        // Mostrar los tratamientos en el TextBox tb_avances
                        tb_avances.Text = textoTratamientos.ToString().Trim();  // Trim para quitar el espacio adicional al final
                    }
                    else
                    {
                        tb_avances.Text = "No se encontraron tratamientos en la sección 'Tratamientos' del archivo XML.";
                    }
                }
                else
                {
                    tb_avances.Text = "Sección 'Tratamientos' no encontrada en el archivo XML.";
                }
            }
            catch (Exception ex)
            {
                tb_definicion.Text = "Error al cargar los datos desde el archivo XML: " + ex.Message;
                tb_sintomas.Text = "Error al cargar los datos desde el archivo XML: " + ex.Message;
                tb_fases.Text = "Error al cargar los datos desde el archivo XML: " + ex.Message;
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

        private void Expander1_Expanded(object sender, RoutedEventArgs e)
        {
            // Aquí puedes poner el código que desees cuando el Expander1 se expanda
        }

        private void Expander2_Expanded(object sender, RoutedEventArgs e)
        {
            // Aquí puedes poner el código que desees cuando el Expander2 se expanda
        }
    }
}
