using System;
using System.IO.Ports;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using HidLibrary;
using System.Management;
using Microsoft.Win32;

namespace UartComManager
{
    public partial class MainWindow : Window
    {
        SerialPort serial = new SerialPort();
        HidDevice? dev = null;

        bool portCon = false;
        bool hidCon = false;

        DispatcherTimer timer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();

            serial.DataReceived += Serial_DataReceived;

            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();

            Loaded += MainWindow_Loaded;
        }

        // ================= UI =================

        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void Min_Click(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;

        private void Max_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        }

        private void Close_Click(object sender, RoutedEventArgs e) => Close();

        // ================= LOAD =================

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ListCOMPorts();

            cbBaud.ItemsSource = new int[] { 9600, 19200, 38400, 57600, 115200 };
            cbBaud.SelectedIndex = 0;
        }

        // ================= UART =================

        private void ListCOMPorts()
        {
            cbPort.ItemsSource = SerialPort.GetPortNames();
            if (cbPort.Items.Count > 0)
                cbPort.SelectedIndex = 0;
        }

        private void BtnConnect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!serial.IsOpen)
                {
                    serial.PortName = cbPort.Text;
                    serial.BaudRate = int.Parse(cbBaud.Text);
                    serial.Open();

                    portCon = true;
                    ShowInfo("Port bağlandı");
                    btnConnect.Content = "Bağlantıyı Kes";
                    tabPanel.BorderBrush = System.Windows.Media.Brushes.Green;
                    txtReceiver.BorderBrush = System.Windows.Media.Brushes.Green;
                }
                else
                {
                    serial.Close();
                    portCon = false;
                    ShowInfo("Port kapandı");
                    btnConnect.Content = "Bağlan";
                    tabPanel.BorderBrush = System.Windows.Media.Brushes.Gray;
                    txtReceiver.BorderBrush = System.Windows.Media.Brushes.Gray;
                }
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
        }

        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            if (!serial.IsOpen) return;

            try
            {
                serial.Write(txtSend.Text);
                txtSend.Clear();
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            txtReceiver.Clear();
        }

        private void Serial_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string data = serial.ReadExisting();

            Dispatcher.Invoke(() =>
            {
                txtReceiver.AppendText(data);
                if(chkAutoScroll.IsChecked == true)
                    txtReceiver.ScrollToEnd();
            });
        }

        // ================= HID =================

        private void ListHIDDevices()
        {
            var devices = HidDevices.Enumerate().ToArray();
            cbDevice.ItemsSource = devices.Select(d =>
                $"VID:{d.Attributes.VendorHexId} PID:{d.Attributes.ProductHexId} - {d.Description}");

            if (cbDevice.Items.Count > 0)
                cbDevice.SelectedIndex = 0;
        }

        // ================= TIMER =================
        int sayac = 0;
        private void Timer_Tick(object? sender, EventArgs e)
        {
            if (portCon && !serial.IsOpen)
            {
                ShowWarning("Port koptu");
                portCon = false;
                ListCOMPorts();
            }
            if (infos.Text.Length > 0)
            {
                sayac++;
                if (sayac >= 5)
                    infos.Text = "";
            }
            else sayac = 0;
        }

        // ================= MESSAGE =================

        void ShowError(string msg)
        {
            MessageBox.Show(msg, "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        void ShowInfo(string msg)
        {
            infos.Text = msg;
        }

        void ShowWarning(string msg)
        {
            MessageBox.Show(msg, "Uyarı", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}