using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pruebaPinPad
{
    
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }

    public class clasePuertos
    {
        public resultado prueba3;
        public string nombrePuerto = string.Empty;
        SerialPort puerto;
        public char stx, etx,fs;
        public clasePuertos() {
            byte aux = 02;
            byte aux2 = 03;
            byte aux3 = 28;
            stx = Convert.ToChar(aux);
            etx = Convert.ToChar(aux2);
            fs = Convert.ToChar(aux3);
            puerto = new SerialPort();
            var puertos = SerialPort.GetPortNames();
            foreach (var item in puertos)
            {
                puerto.PortName = item;
                if (!puerto.IsOpen)
                {
                    var variable = puerto.PortName;
                    break;
                }
            }
            puerto.Open();
            puerto.BaudRate = 19200;
            puerto.Parity = Parity.None;
            puerto.DataBits = 8;
            puerto.StopBits = StopBits.One;
        }    
        public byte LRC(string msg)
        {
            byte LRC;
            LRC = Convert.ToByte(0);
            var caracteres = msg.ToCharArray();

            foreach (char ch in caracteres)
            {
                LRC = Convert.ToByte(LRC ^ Convert.ToByte(ch));
            }
            return LRC;
        }

        internal void imprimir(string inputSome)
        {
            string aux = inputSome;
            var variable = inputSome.ToCharArray();
            var bien = Encoding.ASCII.GetString(Encoding.ASCII.GetBytes(inputSome));
            string sndComando = "C59";
            string mensaje = sndComando + "A" + bien;
            var variable2 = aux.ToCharArray();
            string longitud = longitudCadena(mensaje);
            string output = stx + longitud + mensaje + etx;
            output += Convert.ToChar(LRC(output));
            puerto.Write(output); // Va a imprimir pero no necesita un sleep porque eso hace el pinpad.
            Thread.Sleep(1000);
            string recibo = puerto.ReadExisting();
            prueba3(recibo);
        }
        public void informacionPinPad() {
            if (!puerto.IsOpen)
            {
                puerto.Open();
            }
            string ComandoSnd = "C56";
            string longitudMensaje = "LLL"; //Se puede mandar de esta manera con puro LLL
            string formatoMensaje = "A00";
            string mensaje = "00";
            string cadenaAux = stx + longitudMensaje + ComandoSnd+"AABOUT" + etx;
            cadenaAux = cadenaAux +Convert.ToChar(LRC(cadenaAux));
            puerto.Write(cadenaAux);
            Thread.Sleep(1000);
            string aux = puerto.ReadExisting();
            string[] arreglo = {"Marca","Modelo","Numero serie","version","Bandera EMV "};
            prueba3(aux);
        }

        private string voucherPinPad(string mensaje)
        {
            string ComandoSnd = "C59";
            string longitudMensaje = "LLL"; //Se puede mandar de esta manera con puro LLL
            string formatoMensaje = "A@lnn";
            string saltosLinea = "@br@br@br@br@br@br";
            string cadenaAux = stx + longitudMensaje +ComandoSnd+ formatoMensaje + mensaje +saltosLinea+ etx;
            cadenaAux = cadenaAux + Convert.ToChar(LRC(cadenaAux));
            
            return cadenaAux;
        }

        internal void inicializacionDukpt()
        {
            DateTime fecha1 = DateTime.Now;
            string fecha = fecha1.ToString("ddMMyy");
            string hora2 = fecha1.ToString("HHmm");
            string sndComando = "C91";
            string mensaje = sndComando + "A" +fecha+fs+"B"+hora2;
            string longitud = longitudCadena(mensaje);
            string output = stx + longitud + mensaje + etx;
            output += Convert.ToChar(LRC(output));
            puerto.Write(output);
            Thread.Sleep(1500);
            string resultado = puerto.ReadExisting();
            prueba3(resultado);   
        }

        public void emisionDeBips(string segundo) {
            string sndComando = "C80";
            string mensaje = sndComando+"A4";
            string longitud = longitudCadena(mensaje);
            string output = stx + longitud + mensaje + etx;
            output += Convert.ToChar(LRC(output));
            puerto.Write(output);
            Thread.Sleep(1500);
            string resultado = puerto.ReadExisting();
            prueba3(resultado);
        }

        public void mostrarLogo() {
            // STXLLLC58ABANCOETXLRC
            string ComandoSnd = "C58";
            string mensaje = "ASANTANDER";
            
            mensaje = stx + "015" + ComandoSnd+ mensaje + etx;
            mensaje += Convert.ToChar(LRC(mensaje));
            puerto.Write(mensaje);
            Thread.Sleep(2000);
            string result = puerto.ReadExisting();
            prueba3(result);
        }

        public bool tarjetaConectada() {
            string sndComando = "C82";
            string mensaje = sndComando;
            string longitud = longitudCadena(mensaje);
            string output = stx + longitud + mensaje + etx;
            output += Convert.ToChar(LRC(output));
            puerto.Write(output);
            Thread.Sleep(1500);
            string resultado = puerto.ReadExisting();
            return true;   
        }

        public void mostrarDisplay(string mensajeaux) {
            string sndComando = "C50";
            string mensaje = sndComando+"A"+mensajeaux;
            string longitud = longitudCadena(mensaje);
            string output = stx + longitud + mensaje + etx;
            output += Convert.ToChar(LRC(output));
            puerto.Write(output);
        }

        public void capturarInformacion() {

            string sndComando = "C60";
            string mensaje = sndComando + "ACaptureInformacion"+fs+"B5";
            string longitud = longitudCadena(mensaje);
            string output = stx + longitud + mensaje + etx;
            output += Convert.ToChar(LRC(output));
            puerto.Write(output);
            string auxiliar = puerto.ReadExisting();
        }

        public void pedirFirmaPanel() { 
         string sndComando = "C61";
            string mensaje = sndComando + "A00"+fs+"BFirma panel"+fs+"C15"+fs+"DPNG";
            string longitud = longitudCadena(mensaje);
            string output = stx + longitud + mensaje + etx;
            output += Convert.ToChar(LRC(output));
            puerto.Write(output);
            string auxiliar = puerto.ReadExisting();
        }

        public string longitudCadena(string cadena) {
            int longitud = cadena.Length;
            string aux = "";
            if (longitud < 10)
            {
                aux = "00" + Convert.ToString(longitud);
            }
            else if (longitud <  100)
            {
                aux = "0" + Convert.ToString(longitud);
            }
            else {
                aux = Convert.ToString(longitud);
            }
            return aux;
        }

       
    }
}
