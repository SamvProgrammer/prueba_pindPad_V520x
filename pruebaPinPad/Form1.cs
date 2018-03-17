using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pruebaPinPad
{
    public delegate void resultado(string mensaje);
    public partial class Form1 : Form
    {
        clasePuertos puertos;
        resultado prueba;
        public Form1()
        {
            InitializeComponent();
            puertos = new clasePuertos();
            puertos.prueba3 = mensaje;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string cadena = " @logo1@br@cnn VENTA@br@cnn RETAIL PRUEBAS@cnn (001) RETAIL PRUEBAS@cnn CORREGIDORA 92 @cnn COL. MIGUEL HIDALGO 1A SECCIÃN, DF@br@cnn 2563418 PRUEBAS RETAIL@cnn G374PPPC0 @br@lnn No.Tarjeta: xxxxxxxxxxxx3107@lnn Vence:05/22@br@lnn CREDITO/BBVA BANCOMER/Visa@br@cnb -C-O-M-E-R-C-I-O-@br@lnn APROBADA@lnn IMPORTE @cnb $ 1.00 MXN  @br@lnn Oper.:     000564126@lnn Ref.:      pruebaitijisfj @lnn@lnn AID:       07A0000000032010@lnn Aut.:      546483@lnn  @br@lnn Fecha: 20/05/2017 18:30:00 @br@br@br@cnn @cnn VALIDADO CON FIRMA ELECTRONICA@br@cnn  ME OBLIGO EN LOS TERMINOS DADOS @cnn  AL REVERSO DE ESTE DOCUMENTO@br@logo3br@br@cnn CP-D App.Major.App.Minor.App.Revision@br@bc 000564126@br ";
            puertos.imprimir(cadena);
        }
        public void mensaje(string mensaje) {
            txtResult.Text = mensaje;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            puertos.informacionPinPad();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            puertos.inicializacionDukpt();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            puertos.mostrarLogo();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            puertos.emisionDeBips("samv");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            puertos.tarjetaConectada();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if(txtMensaje.Text == ""){
                MessageBox.Show("Se debe mostrar el mensaje");
                return;
            }

            puertos.mostrarDisplay(txtMensaje.Text);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            puertos.capturarInformacion();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            puertos.pedirFirmaPanel();
        }
    }
}
