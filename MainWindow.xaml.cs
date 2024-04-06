using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
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

namespace WpfApp3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TcpListener listener;

        public MainWindow()
        {
            InitializeComponent();

            listener = new TcpListener(IPAddress.Any, 8888);
            listener.Start();

            Thread listenThread = new Thread(ListenForClients);
            listenThread.Start();
        }


        private void ListenForClients()
        {
            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();

                Thread clientThread = new Thread(HandleClient);
                clientThread.Start(client);
            }
        }

        private void HandleClient(object obj)
        {
            TcpClient client = (TcpClient)obj;
            NetworkStream stream = client.GetStream();

            byte[] imageData = new byte[1024]; 
            stream.Write(imageData, 0, imageData.Length);

            stream.Close();
            client.Close();
        }
    }
}
