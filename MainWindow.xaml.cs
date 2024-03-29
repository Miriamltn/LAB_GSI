using System;
using System.Collections.Generic;
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
            //stackPanel1.Visibility = Visibility.Collapsed;
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
                            string nombreTratamiento = tratamientoNode.SelectSingleNode("nombre").InnerText;
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
                XmlNode seccionNutricion = xmlDoc.SelectSingleNode("/informacion/seccion[@nombre='Nutricion']");

                if (seccionNutricion != null)
                {
                    // Obtener los elementos 'texto' y 'item' dentro de 'informacion'
                    XmlNodeList textoNodes = seccionNutricion.SelectNodes("informacion/texto");
                    XmlNodeList itemNodes = seccionNutricion.SelectNodes("informacion/item");

                    if (textoNodes != null && textoNodes.Count > 0 && itemNodes != null && itemNodes.Count > 0)
                    {
                        // Construir el texto con la información de nutrición
                        StringBuilder textoNutricion = new StringBuilder();
                        foreach (XmlNode textoNode in textoNodes)
                        {
                            textoNutricion.AppendLine(textoNode.InnerText.Trim());
                            textoNutricion.AppendLine();
                        }

                        textoNutricion.AppendLine("Algunos de los problemas comunes pueden ser:");

                        foreach (XmlNode itemNode in itemNodes)
                        {
                            textoNutricion.AppendLine($"- {itemNode.InnerText.Trim()}");
                        }

                        // Mostrar la información de nutrición en el TextBox tb_nutricion
                        tb_nutricion.Text = textoNutricion.ToString().Trim();
                    }
                    else
                    {
                        tb_nutricion.Text = "No se encontró la información de nutrición en el archivo XML.";
                    }
                }
                else
                {
                    tb_nutricion.Text = "Sección 'Nutricion' no encontrada en el archivo XML.";
                }

                XmlNode seccionActuar = xmlDoc.SelectSingleNode("/informacion/seccion[@nombre='Aceptación de la enfermedad']");

                if (seccionActuar != null)
                {
                    string textoActuar = seccionActuar.SelectSingleNode("texto").InnerText;

                    // Mostrar la definición en el TextBox tb_definicion
                    tb_comoActuar.Text = textoActuar;
                }
                else
                {
                    tb_comoActuar.Text = "Sección 'Aceptacion de la enfermedad' no encontrada en el archivo XML.";
                }

                XmlNode seccionFisica = xmlDoc.SelectSingleNode("/informacion/seccion[@nombre='Alteraciones fisicas']");

                if (seccionFisica != null)
                {
                    string textoFisico = seccionFisica.SelectSingleNode("texto").InnerText;

                    // Mostrar la definición en el TextBox tb_definicion
                    tb_fisicas.Text = textoFisico;
                }
                else
                {
                    tb_fisicas.Text = "Sección 'Alteraciones fisicas' no encontrada en el archivo XML.";
                }

                XmlNode seccionCausas = xmlDoc.SelectSingleNode("/informacion/seccion[@nombre='Causas']");

                if (seccionCausas != null)
                {
                    string textoCausas = seccionCausas.SelectSingleNode("texto").InnerText;

                    // Mostrar la definición en el TextBox tb_definicion
                    tb_causas.Text = textoCausas;
                }
                else
                {
                    tb_causas.Text = "Sección 'Causas' no encontrada en el archivo XML.";
                }

                XmlNode seccionEsperanza = xmlDoc.SelectSingleNode("/informacion/seccion[@nombre='Esperanza de vida']");

                if (seccionEsperanza != null)
                {
                    string textoEsperanza = seccionEsperanza.SelectSingleNode("texto").InnerText;

                    // Mostrar la definición en el TextBox tb_definicion
                    tb_Esperanza.Text = textoEsperanza;
                }
                else
                {
                    tb_Esperanza.Text = "Sección 'Esperanza de vida' no encontrada en el archivo XML.";
                }
            }
            catch (Exception ex)
            {
                tb_definicion.Text = "Error al cargar los datos desde el archivo XML: " + ex.Message;
                tb_sintomas.Text = "Error al cargar los datos desde el archivo XML: " + ex.Message;
                tb_fases.Text = "Error al cargar los datos desde el archivo XML: " + ex.Message;
            }
        }


        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Verificar si la pestaña seleccionada es "Dudas"
            if (P_Dudas.IsSelected)
            {
                //stackPanel1.Visibility = Visibility.Visible;
            }
            else
            {
                //stackPanel1.Visibility = Visibility.Collapsed;
            }
        }

        private void Expander_Expanded(object sender, RoutedEventArgs e)
        {
            // Obtén el Expander que se expandió
            Expander expanded = sender as Expander;

            // Crea una lista con todos tus Expanders
            List<Expander> allExpanders = new List<Expander> { Expander1, Expander2, Expander3, Expander4, Expander5, Expander6, Expander6, Expander8 };

            // Cierra todos los Expanders excepto el que se expandió
            foreach (Expander expander in allExpanders)
            {
                if (expander != expanded)
                {
                    expander.IsExpanded = false;
                }
            }
        }


    }
}
