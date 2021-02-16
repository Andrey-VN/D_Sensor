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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO.Ports;
using Modbus.Device;
using NationalInstruments.Restricted;
using System.Windows.Threading;

namespace Project1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        ModbusSerialMaster master;
        readonly byte slaveID = 1;
        ushort startAddress = 0;
        ushort numOfPoints = 0;
        int rezh = 0;
        SerialPort serialPort1;
        DispatcherTimer timer;

        public MainWindow()
        {
            InitializeComponent();

            serialPort1 = new SerialPort();
            string[] ports = SerialPort.GetPortNames();

            foreach (string comport in ports)
            {
                ComboBox1.Items.Add(comport);
            }
            master = ModbusSerialMaster.CreateRtu(serialPort1);
            master.Transport.ReadTimeout = 1000;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(150);
            timer.Tick += timer_Tick;



        }
        void timer_Tick(object sender, EventArgs e)
        {
            startAddress = 0;
            numOfPoints = 20;
            ushort[] holding_register = master.ReadHoldingRegisters(slaveID, startAddress, numOfPoints);
            if (serialPort1.IsOpen)
            {
                VKL.Content = "Включено";
            }
            switch (rezh)
            {
                case 0:
                    break;
                case 1:
                    UltraText.Text = holding_register[0].ToString() + " См";
                    //UltraText.Text = serialPort1.ReadLine() + " См";

                    break;
                case 2:
                    InfraText.Text = holding_register[1].ToString() + " См";
                    break;
                case 3:
                    Potenc.Text = holding_register[2].ToString() + " °";
                    break;
                case 4:
                    Encod.Text = ((short)holding_register[3]).ToString() + " Имп";
                    break;
                case 5:
                    if (holding_register[4] == 0)
                        Gerk.Text = "Сработал датчик";
                    else
                        Gerk.Text = "Исходное положение";
                    break;
                case 6:
                    Holl.Text = ((short)holding_register[5] /100 * 0.1).ToString() + " V";
                    break;
                case 7:
                    R0.Text = (holding_register[13] * 0.001).ToString() + "Ом";
                    Ratio.Text = (holding_register[14] * 0.001).ToString();
                    CO2.Text = holding_register[15].ToString() + " ppm";
                    break;
                case 8:
                    if (holding_register[16] == 0)
                        Dvizh.Text = "Сработал датчик";
                    else
                        Dvizh.Text = "Исходное положение";
                    break;
                case 9:
                    Vlag.Text = holding_register[17].ToString() + " %";
                    Temp.Text = holding_register[18].ToString() + " °C";
                    break;
                case 10:
                    int a = (short)holding_register[12];
                    Compas.Text = a.ToString() + " °";

                    break;
                case 11:
                    Gx.Text = ((short)holding_register[9]).ToString() + " °/с";
                    Gy.Text = ((short)holding_register[10]).ToString() + " °/с";
                    Gz.Text = ((short)holding_register[11]).ToString() + " °/с";
                    Ax.Text = ((short)holding_register[6] * 0.1).ToString() + " М/с^2";
                    Ay.Text = ((short)holding_register[7] * 0.1).ToString() + " М/с^2";
                    Az.Text = ((short)holding_register[8] * 0.1 - 0.23).ToString() + " М/с^2";
                    break;
                case 12:
                    UltraText.Text = default;
                    InfraText.Text = default;
                    Potenc.Text = default;
                    Encod.Text = default;
                    Gerk.Text = default;
                    Holl.Text = default;
                    R0.Text = default;
                    Ratio.Text = default;
                    CO2.Text = default;
                    Dvizh.Text = default;
                    Vlag.Text = default;
                    Temp.Text = default;
                    Compas.Text = default;
                    Ax.Text = default;
                    Ay.Text = default;
                    Az.Text = default;
                    Gx.Text = default;
                    Gy.Text = default;
                    Gz.Text = default;
                    break;

            }
            Console.WriteLine("dsd");
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ushort adress = 19;
                ushort value = 0;
                master.WriteSingleRegister(slaveID, adress, value);
                rezh = 0;
            }
            catch
            {
                MessageBox.Show("Ошибка подключения");
            }
        }

        private void UltStart_Click(object sender, RoutedEventArgs e)
        {
            //timer.Tick += timer_Tick;
            try
            {
                ushort adress = 19;
                ushort value = 1;
                master.WriteSingleRegister(slaveID, adress, value);
                rezh = 1;
            }
            catch
            {
                MessageBox.Show("Ошибка подключения");
            }

        }


        private void InfStart_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                ushort adress = 19;
                ushort value = 2;
                master.WriteSingleRegister(slaveID, adress, value);
                rezh = 2;
            }
            catch
            {
                MessageBox.Show("Ошибка подключения");
            }
        }

        private void PotStart_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                ushort adress = 19;
                ushort value = 3;
                master.WriteSingleRegister(slaveID, adress, value);
                rezh = 3;
            }
            catch
            {
                MessageBox.Show("Ошибка подключения");
            }
        }

        private void EncStart_Click(object sender, RoutedEventArgs e)
        {


            try
            {
                ushort adress = 19;
                ushort value = 4;
                master.WriteSingleRegister(slaveID, adress, value);
                rezh = 4;
            }
            catch
            {
                MessageBox.Show("Ошибка подключения");
            }
        }
        private void GerStart_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                ushort adress = 19;
                ushort value = 5;
                master.WriteSingleRegister(slaveID, adress, value);
                rezh = 5;
            }
            catch
            {
                MessageBox.Show("Ошибка подключения");
            }
        }


        private void HollStart_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                ushort adress = 19;
                ushort value = 6;
                master.WriteSingleRegister(slaveID, adress, value);
                rezh = 6;
            }
            catch
            {
                MessageBox.Show("Ошибка подключения");
            }
        }

        private void GirStart_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                ushort adress = 19;
                ushort value = 11;
                master.WriteSingleRegister(slaveID, adress, value);
                rezh = 11;
            }
            catch
            {
                MessageBox.Show("Ошибка подключения");
            }
        }

        private void CompStart_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                ushort adress = 19;
                ushort value = 10;
                master.WriteSingleRegister(slaveID, adress, value);
                rezh = 10;
            }
            catch
            {
                MessageBox.Show("Ошибка подключения");
            }

        }

        private void GazStart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ushort adress = 19;
                ushort value = 7;
                master.WriteSingleRegister(slaveID, adress, value);
                rezh = 7;
            }
            catch
            {
                MessageBox.Show("Ошибка подключения");
            }
        }

        private void DvizhStart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ushort adress = 19;
                ushort value = 8;
                master.WriteSingleRegister(slaveID, adress, value);
                rezh = 8;
            }
            catch
            {
                MessageBox.Show("Ошибка подключения");
            }
        }

        private void TempStart_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                ushort adress = 19;
                ushort value = 9;
                master.WriteSingleRegister(slaveID, adress, value);
                rezh = 9;
            }
            catch
            {
                MessageBox.Show("Ошибка подключения");
            }
        }
        private void Sbros_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                ushort adress = 19;
                ushort value = 0;
                master.WriteSingleRegister(slaveID, adress, value);
                rezh = 12;
            }
            catch
            {
                MessageBox.Show("Ошибка подключения");
            }
        }


        private void OpenPort_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                serialPort1.PortName = ComboBox1.SelectedItem.ToString();
                serialPort1.Open();

                timer.Start();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка подключения");
                //serialPort1.Close();

            }
        }
        public void MainWindow_Closed(object sender, EventArgs e)
        {
            ushort adress = 19;
            ushort value = 0;
            master.WriteSingleRegister(slaveID, adress, value);
            serialPort1.Close();
        }
    }
}
