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
                    XmlNodeList sintomasNodes = seccionSintomas.SelectNodes("texto/item");

                    if (sintomasNodes != null && sintomasNodes.Count > 0)
                    {
                        StringBuilder textoSintomas = new StringBuilder();
                        foreach (XmlNode sintomaNode in sintomasNodes)
                        {
                            textoSintomas.AppendLine($"- {sintomaNode.InnerText}");
                        }
                        tb_sintomas.Text = textoSintomas.ToString().Trim();
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
                    XmlNodeList etapasNodes = seccionFases.SelectNodes("etapa");

                    if (etapasNodes != null && etapasNodes.Count > 0)
                    {
                        StringBuilder textoFases = new StringBuilder();
                        foreach (XmlNode etapaNode in etapasNodes)
                        {
                            string nombreEtapa = etapaNode.SelectSingleNode("nombre").InnerText;
                            string descripcionEtapa = etapaNode.SelectSingleNode("descripcion").InnerText;

                            textoFases.AppendLine($"{nombreEtapa}: {descripcionEtapa}");
                        }
                        tb_fases.Text = textoFases.ToString().Trim();
                    }
                    else
                    {
                        tb_fases.Text = "No se encontraron fases en el archivo XML.";
                    }
                }

                // Obtener la sección "Tratamientos"
                XmlNode seccionTratamientos = xmlDoc.SelectSingleNode("/informacion/seccion[@nombre='Tratamientos']");

                if (seccionTratamientos != null)
                {
                    XmlNodeList tratamientosNodes = seccionTratamientos.SelectNodes("etapa");

                    if (tratamientosNodes != null && tratamientosNodes.Count > 0)
                    {
                        StringBuilder textoTratamientos = new StringBuilder();
                        foreach (XmlNode tratamientoNode in tratamientosNodes)
                        {
                            string nombreTratamiento = tratamientoNode.SelectSingleNode("nombre").InnerText;
                            string descripcionTratamiento = tratamientoNode.SelectSingleNode("descripcion").InnerText;

                            textoTratamientos.AppendLine($"{nombreTratamiento}: {descripcionTratamiento}");
                        }
                        tb_avances.Text = textoTratamientos.ToString().Trim();
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

                // Obtener la sección "Nutricion"
                XmlNode seccionNutricion = xmlDoc.SelectSingleNode("/informacion/seccion[@nombre='Nutricion']");

                if (seccionNutricion != null)
                {
                    XmlNodeList textoNodes = seccionNutricion.SelectNodes("texto");
                    XmlNodeList itemNodes = seccionNutricion.SelectNodes("item");

                    if (textoNodes != null && textoNodes.Count > 0 && itemNodes != null && itemNodes.Count > 0)
                    {
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

                        tb_nutricion.Text = textoNutricion.ToString().Trim();
                    }
                    else
                    {
                        tb_nutricion.Text = "No se encontró la información de nutrición en la sección 'Nutricion' del archivo XML.";
                    }
                }
                else
                {
                    tb_nutricion.Text = "Sección 'Nutricion' no encontrada en el archivo XML.";
                }

                // Obtener la sección "Aceptación de la enfermedad"
                XmlNode seccionActuar = xmlDoc.SelectSingleNode("/informacion/seccion[@nombre='Aceptacion de la enfermedad']");

                if (seccionActuar != null)
                {
                    string textoActuar = seccionActuar.SelectSingleNode("texto").InnerText;
                    tb_comoActuar.Text = textoActuar;
                }
                else
                {
                    tb_comoActuar.Text = "Sección 'Aceptacion de la enfermedad' no encontrada en el archivo XML.";
                }

                // Obtener la sección "Alteraciones fisicas"
                XmlNode seccionFisica = xmlDoc.SelectSingleNode("/informacion/seccion[@nombre='Alteraciones fisicas']");

                if (seccionFisica != null)
                {
                    string textoFisico = seccionFisica.SelectSingleNode("texto").InnerText;
                    tb_fisicas.Text = textoFisico;
                }
                else
                {
                    tb_fisicas.Text = "Sección 'Alteraciones fisicas' no encontrada en el archivo XML.";
                }

                // Obtener la sección "Causas"
                XmlNode seccionCausas = xmlDoc.SelectSingleNode("/informacion/seccion[@nombre='Causas']");

                if (seccionCausas != null)
                {
                    string textoCausas = seccionCausas.SelectSingleNode("texto").InnerText;
                    tb_causas.Text = textoCausas;
                }
                else
                {
                    tb_causas.Text = "Sección 'Causas' no encontrada en el archivo XML.";
                }

                // Obtener la sección "Esperanza de vida"
                XmlNode seccionEsperanza = xmlDoc.SelectSingleNode("/informacion/seccion[@nombre='Esperanza de vida']");

                if (seccionEsperanza != null)
                {
                    string textoEsperanza = seccionEsperanza.SelectSingleNode("texto").InnerText;
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
                tb_avances.Text = "Error al cargar los datos desde el archivo XML: " + ex.Message;
                tb_nutricion.Text = "Error al cargar los datos desde el archivo XML: " + ex.Message;
                tb_comoActuar.Text = "Error al cargar los datos desde el archivo XML: " + ex.Message;
                tb_fisicas.Text = "Error al cargar los datos desde el archivo XML: " + ex.Message;
                tb_causas.Text = "Error al cargar los datos desde el archivo XML: " + ex.Message;
                tb_Esperanza.Text = "Error al cargar los datos desde el archivo XML: " + ex.Message;
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
            List<Expander> allExpanders = new List<Expander> { Expander1, Expander2, Expander3, Expander4, Expander5, 
                Expander6, Expander6, Expander8, Expander9,Expander10, Expander11, Expander12, Expander13, 
                Expander14, Expander15, Expander16, Expander17, Expander18, Expander19, Expander20, Expander21, Expander22, 
                Expander23, Expander24, Expander25, Expander26, Expander27, Expander28, Expander29, Expander30, Expander31, Expander32 };

            // Cierra todos los Expanders excepto el que se expandió
            foreach (Expander expander in allExpanders)
            {
                if (expander != expanded)
                {
                    expander.IsExpanded = false;
                }
            }
        }


        // Metodo para iniciar links
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string enlaceYouTube = "https://www.youtube.com/watch?v=hIOWIJYVtXg";

            // Usa Process.Start para abrir el enlace en el navegador predeterminado del sistema.
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(enlaceYouTube) { UseShellExecute = true });
        }

        private void MostrarImagen_Click(object sender, RoutedEventArgs e)
        {
            FotoNutricion ventanaNutricion = new FotoNutricion();
            ventanaNutricion.Show();
        }

        private void AbrirTablaEjercicios(object sender, RoutedEventArgs e)
        {
            var tablaEjercicios = new TablaEjercicios();

            tablaEjercicios.Show();
        }

        // Metodos para abrir los links de las asociaciones 

        private void AbrirAiudo(object sender, RoutedEventArgs e)
        {
            string enlace = "https://aiudo.es/";

            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(enlace) { UseShellExecute = true });
        }

        private void AbrirFundacionEspañola(object sender, RoutedEventArgs e)
        {
            string enlace = "https://alzfae.org/";

            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(enlace) { UseShellExecute = true });
        }

        private void AbrirConfederacion(object sender, RoutedEventArgs e)
        {
            string enlace = "https://www.ceafa.es/es";

            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(enlace) { UseShellExecute = true });
        }

        private void AbrirAsociacion(object sender, RoutedEventArgs e)
        {
            string enlace = "https://www.alz.org/?lang=es-MX";

            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(enlace) { UseShellExecute = true });
        }

        private void AbrirASPE(object sender, RoutedEventArgs e)
        {
            string enlace = "https://asociaciones.aspe.es/asociacion/aspe-contra-el-alzheimer/";

            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(enlace) { UseShellExecute = true });
        }
        
        private void AbrirDonativos(object sender, RoutedEventArgs e)
        {
            string enlace = "https://fpmaragall.org/donativos/";

            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(enlace) { UseShellExecute = true });
        }

        // Para abrir los links de los libros 
        private void AbrirLibro1(object sender, RoutedEventArgs e)
        {
            string enlace = "https://www.casadellibro.com/libro-pe-las-amapolas-del-olvido-un-hogar-tres-generaciones-y-un-viaje-al-alzheimer/9788484609797/1841214";

            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(enlace) { UseShellExecute = true });
        }

        private void AbrirLibro2(object sender, RoutedEventArgs e)
        {
            string enlace = "https://www.casadellibro.com/ebook-el-dia-menos-pensado-ebook/9789877389227/12998490";

            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(enlace) { UseShellExecute = true });
        }

        private void AbrirLibro3(object sender, RoutedEventArgs e)
        {
            string enlace = "https://www.casadellibro.com/ebook-siempre-alice-ebook/9788490199954/2661461";

            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(enlace) { UseShellExecute = true });
        }

        private void AbrirLibro4(object sender, RoutedEventArgs e)
        {
            string enlace = "https://www.casadellibro.com/ebook-vitaminas-para-no-olvidar-ebook/9786070752452/7689615";

            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(enlace) { UseShellExecute = true });
        }

        private void AbrirLibro1(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }
    }
}
