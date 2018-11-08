using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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

namespace WEBAPI_Wpf_Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static HttpClient client;
        public MainWindow()
        {
            InitializeComponent();
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:1479/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
                );
        }

        async Task<List<ChatEntry>> GetEntries()
        {
            List<ChatEntry> entries = null;
            HttpResponseMessage response = await client.GetAsync("Home/GetAll");
            if (response.IsSuccessStatusCode)
            {
                entries = await response.Content.ReadAsAsync<List<ChatEntry>>();
            }

            lb_entries.ItemsSource = entries;

            return entries;
            
        }

        async Task AddEntry(ChatEntry ce)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync
                ("/Home/AddEntry", ce);
            response.EnsureSuccessStatusCode();
            //return Task.CompletedTask;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ChatEntry ce = new ChatEntry(tb_username.Text, tb_message.Text);

            AddEntry(ce);

            GetEntries();
        }
    }
}
