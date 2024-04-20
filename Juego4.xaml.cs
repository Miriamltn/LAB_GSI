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
using System.Windows.Shapes;
using System.IO;

namespace LAB_GSI
{
    public partial class Juego4 : Window
    {
        private List<KeyValuePair<string, string>> imagenes;
        private int indiceActual = 0;
        private int aciertos = 0;
        private int fallos = 0;

        public Juego4()
        {
            InitializeComponent();
            InicializarImagenes();
            MostrarImagen();
        }

        private void InicializarImagenes()
        {
            imagenes = new List<KeyValuePair<string, string>>()
    {
        new KeyValuePair<string, string>("/imagenes/perro.jpg", "perro"),
        new KeyValuePair<string, string>("/imagenes/gato.jpg", "gato"),
        new KeyValuePair<string, string>("/imagenes/coche.jpg", "coche"),
        new KeyValuePair<string, string>("/imagenes/mariposa.jpg", "mariposa"),
        new KeyValuePair<string, string>("/imagenes/camiseta.jpg", "camiseta"),
        new KeyValuePair<string, string>("/imagenes/corazon.jpg", "corazon"),
        new KeyValuePair<string, string>("/imagenes/hipopotamo.jpg", "hipopótamo"),
        new KeyValuePair<string, string>("/imagenes/hospital.jpg", "hospital"),
        new KeyValuePair<string, string>("/imagenes/cine.jpg", "cine"),
        new KeyValuePair<string, string>("/imagenes/cama.jpg", "cama")
    };
        }

        private async void MostrarImagen()
        {
            if (indiceActual >= imagenes.Count)
            {
                indiceActual = 0; // Volver al principio de la lista si se ha alcanzado el final
            }

            KeyValuePair<string, string> imagenActual = imagenes[indiceActual];
            string rutaImagen = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "imagenes", imagenActual.Key);
            Uri uriImagen = new Uri(rutaImagen, UriKind.RelativeOrAbsolute);
            imgImagen.Source = new BitmapImage(uriImagen);

            await Task.Delay(3000); // Mostrar la imagen durante 3 segundos
            imgImagen.Source = null; // Limpiar la imagen después de 3 segundos

            indiceActual++; // Pasar a la siguiente imagen en la lista
        }

        private void Comprobar_Click(object sender, RoutedEventArgs e)
        {
            string respuesta = txtRespuesta.Text.ToLower().Trim();

            if (respuesta == imagenes[indiceActual - 1].Value.ToLower())
            {
                MessageBox.Show("¡Correcto!");
                aciertos++;
            }
            else
            {
                MessageBox.Show("Incorrecto. Intenta de nuevo.");
                fallos++;
            }

            txtAciertos.Text = "Aciertos: " + aciertos.ToString();
            txtFallos.Text = "Fallos: " + fallos.ToString();

            MostrarImagen(); // Mostrar la siguiente imagen después de la respuesta
        }
    }
}