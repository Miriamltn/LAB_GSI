﻿using System;
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
                XmlNode seccionCuidadosBasicos = xmlDoc.SelectSingleNode("/informacion/seccion[@nombre='Consejos de Cuidados Básicos']");
                if (seccionCuidadosBasicos != null)
                {
                    XmlNodeList etapasNodes = seccionCuidadosBasicos.SelectNodes("etapa");

                    if (etapasNodes != null && etapasNodes.Count > 0)
                    {
                        StringBuilder textoCuidadosBasicos = new StringBuilder();
                        foreach (XmlNode etapaNode in etapasNodes)
                        {
                            string nombreEtapa = etapaNode.SelectSingleNode("nombre").InnerText;
                            textoCuidadosBasicos.AppendLine($"{nombreEtapa}:");

                            XmlNodeList itemNodes = etapaNode.SelectNodes("item");
                            if (itemNodes != null && itemNodes.Count > 0)
                            {
                                foreach (XmlNode itemNode in itemNodes)
                                {
                                    textoCuidadosBasicos.AppendLine($"- {itemNode.InnerText.Trim()}");
                                }
                            }

                            textoCuidadosBasicos.AppendLine(); // Línea en blanco entre etapas
                        }

                        tb_cuidadosBasicos.Text = textoCuidadosBasicos.ToString().Trim();
                    }
                    else
                    {
                        tb_cuidadosBasicos.Text = "No se encontraron etapas de cuidados básicos en el archivo XML.";
                    }
                }
                else
                {
                    tb_cuidadosBasicos.Text = "Sección 'Consejos de Cuidados Básicos' no encontrada en el archivo XML.";
                }
                XmlNode seccionComunicacion = xmlDoc.SelectSingleNode("/informacion/seccion[@nombre='Consejos básicos sobre comunicacion']");

                if (seccionComunicacion != null)
                {
                    XmlNodeList etapasNodes = seccionComunicacion.SelectNodes("etapa");

                    if (etapasNodes != null && etapasNodes.Count > 0)
                    {
                        StringBuilder textoComunicacion = new StringBuilder();
                        foreach (XmlNode etapaNode in etapasNodes)
                        {
                            string nombreEtapa = etapaNode.SelectSingleNode("nombre").InnerText;
                            textoComunicacion.AppendLine($"{nombreEtapa}:");

                            XmlNodeList items = etapaNode.SelectNodes("item");
                            if (items != null && items.Count > 0)
                            {
                                foreach (XmlNode itemNode in items)
                                {
                                    textoComunicacion.AppendLine($"- {itemNode.InnerText.Trim()}");
                                }
                            }

                            textoComunicacion.AppendLine();
                        }

                        tb_comunicacion.Text = textoComunicacion.ToString().Trim();
                    }
                    else
                    {
                        tb_comunicacion.Text = "No se encontró la información sobre comunicación en el archivo XML.";
                    }
                }
                else
                {
                    tb_comunicacion.Text = "Sección 'Consejos básicos sobre comunicacion' no encontrada en el archivo XML.";
                }

                XmlNode seccionRecursosLegales = xmlDoc.SelectSingleNode("/informacion/seccion[@nombre='Consejos de recursos legales']");

                if (seccionRecursosLegales != null)
                {
                    // Obtener todas las etapas dentro de la sección
                    XmlNodeList etapasNodes = seccionRecursosLegales.SelectNodes("etapa");

                    if (etapasNodes != null && etapasNodes.Count > 0)
                    {
                        // Construir el texto con las etapas y sus items
                        StringBuilder textoRecursosLegales = new StringBuilder();
                        foreach (XmlNode etapaNode in etapasNodes)
                        {
                            string nombreEtapa = etapaNode.SelectSingleNode("nombre").InnerText;
                            textoRecursosLegales.AppendLine($"## {nombreEtapa}:");

                            // Obtener los elementos 'item' dentro de la etapa
                            XmlNodeList itemsNodes = etapaNode.SelectNodes("item");

                            if (itemsNodes != null && itemsNodes.Count > 0)
                            {
                                foreach (XmlNode itemNode in itemsNodes)
                                {
                                    textoRecursosLegales.AppendLine($"- {itemNode.InnerText.Trim()}");
                                }
                            }
                        }

                        // Mostrar el texto en el TextBlock tb_legal
                        tb_legal.Text = textoRecursosLegales.ToString().Trim();
                    }
                    else
                    {
                        tb_legal.Text = "No se encontraron etapas en la sección 'Consejos de recursos legales' del archivo XML.";
                    }
                }
                else
                {
                    tb_legal.Text = "Sección 'Consejos de recursos legales' no encontrada en el archivo XML.";
                }

                // Obtener la sección "Tratamiento y cuidados"
                XmlNode seccionTratamientoCuidados = xmlDoc.SelectSingleNode("/informacion/seccion[@nombre='Enfermeria']");

                if (seccionTratamientoCuidados != null)
                {
                    // Obtener todos los nodos "etapa" dentro de "Tratamiento y cuidados"
                    XmlNodeList etapasNodes = seccionTratamientoCuidados.SelectNodes("etapa");

                    if (etapasNodes != null && etapasNodes.Count > 0)
                    {
                        // Construir el texto con las etapas y consejos en líneas separadas
                        StringBuilder textoTratamientoCuidados = new StringBuilder();
                        foreach (XmlNode etapaNode in etapasNodes)
                        {
                            string nombreEtapa = etapaNode.SelectSingleNode("nombre").InnerText;
                            textoTratamientoCuidados.AppendLine($"## {nombreEtapa}");

                            // Obtener los elementos 'item' dentro de 'etapa'
                            XmlNodeList itemsNodes = etapaNode.SelectNodes("item");

                            if (itemsNodes != null && itemsNodes.Count > 0)
                            {
                                foreach (XmlNode itemNode in itemsNodes)
                                {
                                    textoTratamientoCuidados.AppendLine($"- {itemNode.InnerText}");
                                }
                            }

                            textoTratamientoCuidados.AppendLine(); // Separador entre etapas
                        }

                        // Mostrar el texto de tratamiento y cuidados en el TextBlock tb_enfermeria
                        tb_enfermeria.Text = textoTratamientoCuidados.ToString().Trim();
                    }
                    else
                    {
                        tb_enfermeria.Text = "No se encontraron etapas de tratamiento y cuidados en el archivo XML.";
                    }
                }
                else
                {
                    tb_enfermeria.Text = "Sección 'Tratamiento y cuidados' no encontrada en el archivo XML.";
                }

                XmlNode seccionEjercicioDudas = xmlDoc.SelectSingleNode("/informacion/seccion[@nombre='EjercicioDudas']");

                if (seccionEjercicioDudas != null)
                {
                    XmlNodeList etapasNodes = seccionEjercicioDudas.SelectNodes("etapa");

                    if (etapasNodes != null && etapasNodes.Count > 0)
                    {
                        StringBuilder textoEjercicioDudas = new StringBuilder();
                        foreach (XmlNode etapaNode in etapasNodes)
                        {
                            textoEjercicioDudas.AppendLine(etapaNode.InnerText.Trim());
                        }

                        tb_ejercicioDudas.Text = textoEjercicioDudas.ToString().Trim();
                    }
                    else
                    {
                        tb_ejercicioDudas.Text = "No se encontraron etapas en la sección 'EjercicioDudas' del archivo XML.";
                    }
                }
                else
                {
                    tb_ejercicioDudas.Text = "Sección 'EjercicioDudas' no encontrada en el archivo XML.";
                }

                XmlNode seccionCuidadosBano = xmlDoc.SelectSingleNode("/informacion/seccion[@nombre='Ducha']");

                if (seccionCuidadosBano != null)
                {
                    XmlNodeList etapasNodes = seccionCuidadosBano.SelectNodes("etapa");

                    if (etapasNodes != null && etapasNodes.Count > 0)
                    {
                        StringBuilder textoCuidadosBano = new StringBuilder();
                        foreach (XmlNode etapaNode in etapasNodes)
                        {
                            textoCuidadosBano.AppendLine(etapaNode.InnerText.Trim());
                            textoCuidadosBano.AppendLine();
                        }

                        tb_ducha.Text = textoCuidadosBano.ToString().Trim();
                    }
                    else
                    {
                        tb_ducha.Text = "No se encontraron etapas en la sección 'CuidadosBano' del archivo XML.";
                    }
                }
                else
                {
                    tb_ducha.Text = "Sección 'CuidadosBano' no encontrada en el archivo XML.";
                }

                XmlNode seccionApoyoEmocional = xmlDoc.SelectSingleNode("/informacion/seccion[@nombre='Salud']");

                if (seccionApoyoEmocional != null)
                {
                    XmlNodeList etapasNodes = seccionApoyoEmocional.SelectNodes("etapa");

                    if (etapasNodes != null && etapasNodes.Count > 0)
                    {
                        StringBuilder textoApoyoEmocional = new StringBuilder();
                        foreach (XmlNode etapaNode in etapasNodes)
                        {
                            textoApoyoEmocional.AppendLine(etapaNode.InnerText.Trim());
                            textoApoyoEmocional.AppendLine();
                        }

                        tb_salud.Text = textoApoyoEmocional.ToString().Trim();
                    }
                    else
                    {
                        tb_salud.Text = "No se encontraron etapas en la sección 'ApoyoEmocional' del archivo XML.";
                    }
                }
                else
                {
                    tb_salud.Text = "Sección 'ApoyoEmocional' no encontrada en el archivo XML.";
                }

                XmlNode seccionRutinasPrincipales = xmlDoc.SelectSingleNode("/informacion/seccion[@nombre='RutinasPrincipales']");

                if (seccionRutinasPrincipales != null)
                {
                    XmlNodeList etapasNodes = seccionRutinasPrincipales.SelectNodes("etapa");

                    if (etapasNodes != null && etapasNodes.Count > 0)
                    {
                        StringBuilder textoRutinasPrincipales = new StringBuilder();
                        foreach (XmlNode etapaNode in etapasNodes)
                        {
                            textoRutinasPrincipales.AppendLine(etapaNode.InnerText.Trim());
                            textoRutinasPrincipales.AppendLine();
                        }

                        tb_rutinasPrincipal.Text = textoRutinasPrincipales.ToString().Trim();
                    }
                    else
                    {
                        tb_rutinasPrincipal.Text = "No se encontraron etapas en la sección 'RutinasPrincipales' del archivo XML.";
                    }
                }
                else
                {
                    tb_rutinasPrincipal.Text = "Sección 'RutinasPrincipales' no encontrada en el archivo XML.";
                }

                XmlNode seccionEjercicioRelajacion = xmlDoc.SelectSingleNode("/informacion/seccion[@nombre='Ejercicio1']");

                if (seccionEjercicioRelajacion != null)
                {
                    XmlNodeList etapasNodes = seccionEjercicioRelajacion.SelectNodes("etapa");

                    if (etapasNodes != null && etapasNodes.Count > 0)
                    {
                        StringBuilder textoEjercicioRelajacion = new StringBuilder();
                        foreach (XmlNode etapaNode in etapasNodes)
                        {
                            textoEjercicioRelajacion.AppendLine(etapaNode.InnerText.Trim());
                            textoEjercicioRelajacion.AppendLine();
                        }

                        tb_ejercicio1.Text = textoEjercicioRelajacion.ToString().Trim();
                    }
                    else
                    {
                        tb_ejercicio1.Text = "No se encontraron etapas en la sección 'EjercicioRelajacion' del archivo XML.";
                    }
                }
                else
                {
                    tb_ejercicio1.Text = "Sección 'EjercicioRelajacion' no encontrada en el archivo XML.";
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
                tb_enfermeria.Text = "Error al cargar los datos desde el archivo XML " + ex.Message;
                tb_ducha.Text = "Error al cargar los datos desde el archivo XML" + ex.Message; 
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
                Expander23, Expander24, Expander25, Expander26, Expander27, Expander28, Expander29, Expander30, Expander31, Expander32, Expander33,
                Expander34, Expander35, Expander36, Expander37, Expander38, Expander39, Expander40, Expander41, Expander42, Expander43, Expander44};

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
