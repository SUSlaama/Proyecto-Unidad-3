using System;
using System.IO.Ports;
using System.Windows.Forms;

namespace Proyecto_Unidad_3
{
    public partial class Form1 : Form
    {
        private SerialPort serialPort;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            serialPort = new SerialPort();
            serialPort.BaudRate = 9600;
            serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

            foreach (string port in SerialPort.GetPortNames())
            {
                comboBoxPorts.Items.Add(port);
            }
        }

        private void btnConnect_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxPorts.SelectedItem != null)
                {
                    serialPort.PortName = comboBoxPorts.SelectedItem.ToString();
                    serialPort.Open();
                    MessageBox.Show("Conectado a " + serialPort.PortName);
                }
                else
                {
                    MessageBox.Show("Por favor, seleccione un puerto COM.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al conectar: " + ex.Message);
            }
        }

        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            string inData = serialPort.ReadLine();

            this.Invoke(new MethodInvoker(delegate
            {
                richTextBoxReceivedData.AppendText(inData + Environment.NewLine + "\n");

                if (!string.IsNullOrEmpty(inData))
                {
                    try
                    {
                        string trimmedData = inData.Trim();
                        if (trimmedData.Length == 8)
                        {
                            int charCode = Convert.ToInt32(trimmedData, 2);
                            char letra = (char)charCode;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al convertir los datos: " + ex.Message);
                    }
                }
            }));
        }

        private void Form1_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            MessageBox.Show("Usted ha salido del programa. \n" + "Puerto " + serialPort.PortName + " desconectado.");
            if (serialPort.IsOpen)
            {
                serialPort.Close();
            }
        }

        private void btnDesconnected_Click(object sender, EventArgs e)
        {
            if (serialPort.IsOpen)
            {
                MessageBox.Show("Puerto " + serialPort.PortName + " desconectado.");
                serialPort.Close();
                richTextBoxReceivedData.Text = "";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (serialPort.IsOpen)
            {
                serialPort.Close();
            }
            this.Close();
        }

        private void comboBoxPorts_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
