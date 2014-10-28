using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;

using ProyectoAutomatasUMG2014.Helpers;
using ProyectoAutomatasUMG2014.Classes;

namespace ProyectoAutomatasUMG2014.AppFunction
{
    class Program
    {
        [STAThread]
        static void Main(String[] args)
        {

            // Abre un explorador para ubicar el archivo .txt, devolviendo su ruta física.            
            OpenFileDialog fileDialog = new OpenFileDialog();
            RegexHelper regexHelper = new RegexHelper();
            String rutaArchivo = String.Empty;
            String contenidoArchivo = String.Empty;
            
            fileDialog.Title = "Abrir Archivo de Texto";
            fileDialog.Filter = "TXT files|*.txt";
            fileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                rutaArchivo = fileDialog.FileName.ToString();
            }
            
            // Intenta leer el contenido del archivo
            try
            {
                contenidoArchivo = File.ReadAllText(rutaArchivo);
            }
            catch { }

            int size;
            int index = 0;

            // Obtiene toda la información contenida en el archivo y la asigna a las variables que representarán la quíntupla del AFN.
            contenidoArchivo = Regex.Replace(contenidoArchivo, @"\t|\n|\r|\s", "");
            List<String> estadosAutomataND = regexHelper.getValues(contenidoArchivo, @"Q:\{(?<valores>[\d\w]+,?)+\}");
            List<String> simbolosAutomataND = regexHelper.getValues(contenidoArchivo, @"F:\{(?<valores>[\d\w]+,?)+\}");
            String estadoInicialAutomataND = regexHelper.getValues(contenidoArchivo, @"i:(?<valores>[\d\w]+)A").FirstOrDefault();
            List<String> estadosAceptacionAutomataND = regexHelper.getValues(contenidoArchivo, @"A:\{(?<valores>[\d\w]+,?)+\}");
            List<String> funcionesTransicionAutomataND = regexHelper.getValues(contenidoArchivo, @"(w|W):\{(\((?<valores>[\d\w]+,?)+\),?)+\}");
            
            List<Transicion> listaFuncTransicionAutomataND = new List<Transicion>();
            size = funcionesTransicionAutomataND.Count / 3;

            // Reestructura las funciones de transición del AFN en base a la información obtenida del archivo TXT.
            for (int i = 0; i < size; i++)
            {
                listaFuncTransicionAutomataND.Add(new Transicion(funcionesTransicionAutomataND[index], funcionesTransicionAutomataND[index + 1], funcionesTransicionAutomataND[index + 2]));
                index += 3;
            }

            // Llama a los métodos que ejecutan el algoritmo de conversión de AFN a AFD.
            Automata automata = new Automata();
            List<TransicionAFD> nuevosEstadosAutomata = new List<TransicionAFD>();
            List<TransicionAFD> funcionesTransicionAutomata = automata.ConvertirAFNaAFD(estadosAutomataND, simbolosAutomataND, estadoInicialAutomataND, estadosAceptacionAutomataND, listaFuncTransicionAutomataND, ref nuevosEstadosAutomata);

            // Imprime en pantalla las funciones de transición generadas del Autómata Finito Determinista.

            // Arma el string de la cabecera para mostrarse, incluyendo el listado de elementos del alfabeto.
            String cabeceraEstados = String.Empty;
            foreach (String estado in simbolosAutomataND)
            {
                cabeceraEstados += estado + "  ";
            }
            String cabecera = String.Format("Estados\t  {0}\tComponentes\n\r", cabeceraEstados);
            Console.WriteLine(cabecera);

            // Recorre cada una de las funciones de transición del nuevo AFD, concatenando el estado, hacia donde
            // se dirige con cada elemento del alfabeto y la composición de subestados de cada uno de los nuevos estados.
            foreach (TransicionAFD estado in nuevosEstadosAutomata)
            {
                String transiciones = String.Empty;
                transiciones = "   " + estado.estado + "\t  ";
                
                // Concatena cada uno de los estados destino para cada elemento del alfabeto.
                foreach (TransicionAFD transicion in funcionesTransicionAutomata.Where(f => f.estado == estado.estado).ToList())
                {
                    transiciones += transicion.proximoEstado + "  ";
                }
                transiciones += "\t";
                String compEstados = String.Empty;
                
                // Arma la composición de subestados para cada uno de los nuevos estados del AFD.
                foreach (String e in estado.componentes)
                {
                    if (String.IsNullOrEmpty(compEstados))
                    {
                        compEstados += e;
                    }
                    else
                    {
                        compEstados += "," + e;
                    }
                }

                // Si el estado es un estado de aceptación, lo indica a la par.
                transiciones += compEstados;
                if (estado.aceptacion)
                {
                    transiciones += "\t[ESTADO_DE_ACEPTACION]";
                }
                Console.WriteLine(transiciones);
            }
            Console.WriteLine();
            Console.ReadLine();
        }
    }
}