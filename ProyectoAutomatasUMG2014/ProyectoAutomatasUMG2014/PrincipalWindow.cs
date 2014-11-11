using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;
using System.Text.RegularExpressions;

using ProyectoAutomatasUMG.Clases;
using ProyectoAutomatasUMG.AppFunction;

namespace ProyectoAutomatasUMG
{
    public partial class PrincipalWindow : Form
    {

        private List<TransicionAFD> funcionesTransicionAutomata;
        private List<TransicionAFD> nuevosEstadosAutomata;
        private String estadoInicialAutomataFinitoDeterminista;
        private List<String> simbolosAutomataND;

        public PrincipalWindow()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            // Asigna la información correspondiente al desarrollador y al proyecto dentro de un tool tip, para que se muestre
            // como información dentro del programa.
            this.toolTip.SetToolTip(this.label1, "Universidad Mariano Gálvez de Guatemala" + Char.ConvertFromUtf32(13) + 
                                                 "Facultad de Ingeniería en Sistemas" + Char.ConvertFromUtf32(13) + 
                                                 "Sección \"A\" Diaria Vespertina" + Char.ConvertFromUtf32(13) + 
                                                 "Lenguajes Formales y Autómatas" + Char.ConvertFromUtf32(13) + 
                                                 "Ing. Corina Pérez" + Char.ConvertFromUtf32(13) +
                                                 "Proyecto Final Autómatas - Última Fase" + Char.ConvertFromUtf32(13) + 
                                                 "David Fernando Mencos García" + Char.ConvertFromUtf32(13) +
                                                 "090-13-9241");
        }

        private void InicializarPrograma(String contenidoArchivo)
        {
            RegexHelper regexHelper = new RegexHelper();
            int size;
            int index = 0;

            // Obtiene toda la información contenida en el archivo y la asigna a las variables que representarán la quíntupla del AFN.
            contenidoArchivo = Regex.Replace(contenidoArchivo, @"\t|\n|\r|\s", "");
            List<String> estadosAutomataND = regexHelper.getValues(contenidoArchivo, @"Q:\{(?<valores>[\d\w]+,?)+\}");
            simbolosAutomataND = regexHelper.getValues(contenidoArchivo, @"F:\{(?<valores>[\d\w]+,?)+\}");
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
            this.funcionesTransicionAutomata = automata.ConvertirAFNaAFD(estadosAutomataND, simbolosAutomataND, estadoInicialAutomataND, estadosAceptacionAutomataND, listaFuncTransicionAutomataND, ref this.nuevosEstadosAutomata);

            // Arma la tabla e imprime en el grid las funciones de transición generadas del Autómata Finito Determinista.

            DataTable table = new DataTable();
            table.Columns.Add("ESTADOS", typeof(String));

            // Arma el listado de elementos del alfabeto de las funciones de transición.
            foreach (String estado in simbolosAutomataND)
            {
                table.Columns.Add(estado, typeof(String));
            }
            table.Columns.Add("COMPONENTES", typeof(String));
            table.Columns.Add("ACEPTACION", typeof(String));

            // Variables para armar los Strings que contendrán las especificaciones de cada elemento de la quíntupla del nuevo AFD.
            String AFDestados = String.Empty;
            String AFDelementos = String.Empty;
            String AFDinicial = String.Empty;
            String AFDAceptacion = String.Empty;
            String AFDFuncionesTransicion = String.Empty;

            // Recorre cada una de las funciones de transición del nuevo AFD, concatenando el estado, hacia donde
            // se dirige con cada elemento del alfabeto y la composición de subestados de cada uno de los nuevos estados
            // para agregar cada uno de esos elementos a un row que será añadido a la tabla desplegada por el dataGridView.
            foreach (TransicionAFD estado in nuevosEstadosAutomata)
            {
                List<String> elementosTabla = new List<String>();
                elementosTabla.Add(estado.estado);
                AFDestados += "," + estado.estado;
                if (estado.aceptacion)
                {
                    AFDAceptacion += "," + estado.estado;
                }

                // Concatena cada uno de los estados destino para cada elemento del alfabeto.
                foreach (TransicionAFD transicion in funcionesTransicionAutomata.Where(f => f.estado == estado.estado).ToList())
                {
                    elementosTabla.Add(transicion.proximoEstado);
                    AFDFuncionesTransicion += String.Format(",({0},{1},{2})", transicion.estado, transicion.simbolo, transicion.proximoEstado);
                }
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
                // Si el estado es un estado de aceptación, lo indica.
                elementosTabla.Add(compEstados);
                if (estado.aceptacion)
                {
                    elementosTabla.Add("Aceptación");
                }
                else
                {
                    elementosTabla.Add("-");
                }
                table.Rows.Add(elementosTabla.ToArray());
            }

            // Establece las propiedades del Grid.
            this.dataGridView1.DataSource = table;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.RowHeadersVisible = false;
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            // Formatea la quíntupla del AFD.
            AFDestados = AFDestados.Remove(0, 1);
            AFDestados = String.Format("Q:\t{{{0}}}", AFDestados);
            AFDAceptacion = AFDAceptacion.Remove(0, 1);
            AFDAceptacion = String.Format("A:\t{{{0}}}", AFDAceptacion);
            AFDFuncionesTransicion = AFDFuncionesTransicion.Remove(0, 1);
            AFDFuncionesTransicion = String.Format("W:\t{{{0}}}", AFDFuncionesTransicion);
            foreach(String simbolo in simbolosAutomataND)
            {
                AFDelementos += "," + simbolo;
            }
            AFDelementos = AFDelementos.Remove(0, 1);
            AFDelementos = String.Format("F:\t{{{0}}}", AFDelementos);
            AFDinicial = nuevosEstadosAutomata.FirstOrDefault().estado;
            estadoInicialAutomataFinitoDeterminista = AFDinicial;
            AFDinicial = String.Format("i:\t{0}", AFDinicial);
            
            // Arma el string de la quíntupla para mostrarla en pantalla.
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(AFDestados);
            sb.AppendLine(AFDelementos);
            sb.AppendLine(AFDinicial);
            sb.AppendLine(AFDAceptacion);
            sb.AppendLine(AFDFuncionesTransicion);
            this.txtAFD.Text = sb.ToString();
            this.txtCadena.Enabled = true;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();

            // Abre un explorador para ubicar el archivo .txt, devolviendo su ruta física.            
            OpenFileDialog fileDialog = new OpenFileDialog();
            String rutaArchivo = String.Empty;
            String contenidoArchivo = String.Empty;

            fileDialog.Title = "Abrir Archivo de Texto";
            fileDialog.Filter = "TXT files|*.txt";
            fileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                rutaArchivo = fileDialog.FileName.ToString();
                this.txtPathFile.Text = rutaArchivo;
            }

            // Intenta leer el contenido del archivo
            try
            {
                contenidoArchivo = File.ReadAllText(rutaArchivo);

                // Imprime el Automata finito no determinista original en el textbox correspondiente
                String contenidoAFND = contenidoArchivo;
                contenidoAFND = contenidoAFND.Replace("Q:{", "Q:\t{");
                contenidoAFND = contenidoAFND.Replace("F:{", "F:\t{");
                contenidoAFND = contenidoAFND.Replace("i:", "i:\t");
                contenidoAFND = contenidoAFND.Replace("A:{", "A:\t{");
                contenidoAFND = contenidoAFND.Replace("W:{", "W:\t{");
                contenidoAFND = contenidoAFND.Replace("w:{", "W:\t{");
                this.txtAFND.Text = contenidoAFND;

                this.InicializarPrograma(contenidoArchivo);
            }
            catch { }
        }

        private void btnCompCadena_Click(object sender, EventArgs e)
        {
            if (this.funcionesTransicionAutomata == null)
            {
                errorProvider1.Clear();
                this.lblResultado.Text = "";
                errorProvider1.SetError(this.txtCadena, "Debe generar un AFD.");
            }
            //else if(String.IsNullOrEmpty(this.txtCadena.Text))
            //{
            //    errorProvider1.Clear();
            //    this.lblResultado.Text = "";
            //    errorProvider1.SetError(this.txtCadena, "Debe ingresar una cadena.");
            //}
            else
            {
                errorProvider1.Clear();
                String contenidoCadenaValidar = this.txtCadena.Text.Trim().Replace(@"\s", "");
                //List<String> cadenaParaValidar = this.txtCadena.Text.Split("".ToCharArray(), StringSplitOptions.) .ToList();
                TransicionAFD status = new TransicionAFD();
                status = this.validarCadena(contenidoCadenaValidar, status);
                if(status != null && nuevosEstadosAutomata.Where(s => s.estado == status.proximoEstado).FirstOrDefault().aceptacion)
                {
                    this.lblResultado.ForeColor = System.Drawing.Color.Green;
                    this.lblResultado.Text = "ACEPTADA";
                }
                else if (status == null)
                {
                    this.lblResultado.ForeColor = System.Drawing.Color.Red;
                    this.lblResultado.Text = "CARACTERES INVALIDOS";
                }
                else
                {
                    this.lblResultado.ForeColor = System.Drawing.Color.Red;
                    this.lblResultado.Text = "RECHAZADA";
                }
            }
        }

        /// <summary>
        /// Valida que la cadena ingresada sea de aceptación o no.
        /// </summary>
        /// <param name="cadenaValidar">Cadena a validar.</param>
        /// <param name="status">Estado resultante al terminar de recorrer cada elemento de la cadena a validar.</param>
        /// <param name="index">Indice del elemento de la cadena que se está evaluando.</param>
        /// <returns></returns>
        private TransicionAFD validarCadena(String cadenaValidar, TransicionAFD status)
        {
            // Se utiliza como flag para que el primer elemento de la cadena inicie su recorrido desde el primer estado el autómata.
            int contador = 0;

            // Si se recibe una cadena vacía (lambda) y el primer estado es de aceptación, retorna ese primer estado como estado resultante.
            if (String.IsNullOrEmpty(cadenaValidar) && funcionesTransicionAutomata.Where(s => s.estado == estadoInicialAutomataFinitoDeterminista).FirstOrDefault().aceptacion)
            {
                status = funcionesTransicionAutomata.Where(s => s.estado == estadoInicialAutomataFinitoDeterminista).FirstOrDefault();
                return status;
            }
            else if (String.IsNullOrEmpty(cadenaValidar))
            {
                return null;
            }

            // Recorre cada elemento de la cadena, posición por posición, y devuelve el estado resultante al terminar de recorrer la cadena.
            foreach(Char elemento in cadenaValidar){

                // Si el elemento que se evalúa no pertenece al alfabeto original, retorna un estado nulo y sale del método.
                if (simbolosAutomataND.Contains(elemento.ToString()))
                {

                    // Si es el primer elemento de la cadena, se posiciona en el estado inicial del autómata y parte de ese estado.
                    // De lo contrario, sigue recorriendo con el estado en el que se quedó.
                    if (contador == 0)
                    {
                        status = this.funcionesTransicionAutomata.Where(f => f.simbolo == elemento.ToString() && f.estado == this.estadoInicialAutomataFinitoDeterminista).FirstOrDefault();
                    }
                    else
                    {
                        status = this.funcionesTransicionAutomata.Where(f => f.estado == status.proximoEstado && f.simbolo == elemento.ToString()).FirstOrDefault();
                    }
                }
                else
                {
                    return null;
                }

                contador = 1;

            }

            return status;
        }
    }
}
