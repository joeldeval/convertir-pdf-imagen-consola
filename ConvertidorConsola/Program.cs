using ImageMagick;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ConvertidorConsola
{
    class Program
    {
        static void Main(string[] args)
        {
            // ruta de la carpeta donde se encuentra(n) su(s) pdf(s) 
            string rutaDirectorio = "C:\\Users\\Bioxor\\Documents\\Proyectos\\pdf";

            // arreglo de archivos con la extension PDF
            string[] archivos = System.IO.Directory.GetFiles(rutaDirectorio, "*.pdf");

            // contador de archivos
            int numeroImagen = 1;

            // ruta de archivo
            string rutaPDF = null;

            // recorre el arreglo de archivos para convertirlos 1 por 1
            foreach (string nombreArchivo in archivos)
            {
                // se declara una variable de tipo FileInfo que nos permite obtener un archivo
                FileInfo archivo = null;

                try
                {
                    // obtenemos el archivo
                    archivo = new FileInfo(nombreArchivo);

                    // obtenemos ruta del archivo a convertir
                    rutaPDF = archivo.Directory + "\\" + archivo.Name;

                    // muestra en consola el archivo a leer
                    Console.WriteLine("Leyendo archivo " + rutaPDF);

                }
                catch (System.IO.FileNotFoundException e)
                {
                    // en caso de error lo mostramos en consola
                    Console.WriteLine(e.Message);
                    continue;
                }

                // ESTO ES MAGICK
                // Instancia de un objeto para asignar propiedades a la imagen a crear
                MagickReadSettings propiedadesImagen = new MagickReadSettings();
                // asignamos una densidad de 300
                propiedadesImagen.Density = new Density(300);
                
                // se usa una coleccion de imagenes
                using (MagickImageCollection imagenes = new MagickImageCollection())
                {
                    // Lee el archivo PDF y asigna las propiedades antes escritas
                    imagenes.Read(rutaPDF, propiedadesImagen);

                    try
                    {
                        // Crea una imagen nueva la cual anexa las paginas horizontalmente
                        using (MagickImage imagenACrear = imagenes.AppendHorizontally())
                        {
                            // Guarda el resultado como un jpg
                            // dentro de la misma Carpeta que obtuvo el PDF
                            imagenACrear.Write(rutaDirectorio + "\\imagen" + numeroImagen + ".jpg");

                            // imprime en consola
                            Console.WriteLine("Exportó a " + rutaDirectorio + "\\imagen" + numeroImagen + ".jpg");

                        }
                    }
                    catch (Exception ex)
                    {
                        // en caso de error lo mostramos en consola
                        Console.WriteLine(ex.Message);
                    }
                    // limpia la coleccion de imagenes
                    imagenes.Clear();
                }
                numeroImagen++;
            }

        }
    }
}
