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
using System.Data.OleDb;
using System.Data;
using System.Threading;

namespace repairParts
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            myConnection = new OleDbConnection(connString);
            myConnection.Open();
        }

        public static string connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=repairDB.mdb;";
        private OleDbConnection myConnection;

        private async void LoadTable()
        {
            await this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, (ThreadStart)delegate ()
           {
               string query = "SELECT * FROM Pairs";
               OleDbCommand command = new OleDbCommand(query, myConnection);
               OleDbDataAdapter da = new OleDbDataAdapter(command);
               DataSet ds = new DataSet();
               da.Fill(ds, query);
               dg.ItemsSource = ds.Tables[0].DefaultView;
               
           });
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddDetail aDet = new AddDetail();
            aDet.Show();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await Task.Run(() => LoadTable());
        }

        private string queryToSeek;

        private void seek(int record)
        {
            try
            {
                if (record == 0) /*поиск по парт № детали*/
                {
                    string param = searchTxtBox.Text;
                    queryToSeek = "SELECT * FROM Pairs Where IndexPair ='" + param + "' ";
                }
                if (record == 1) /*поиск по номеру детали*/
                {
                    int param = Convert.ToInt32(searchTxtBox.Text);
                    string query = "SELECT * FROM Pairs Where id ='" + param + "' ";
                }
                if (record == 2) /*поиск по фирме*/
                {
                    string param = searchTxtBox.Text;
                    queryToSeek = "SELECT * FROM Pairs Where NameFirm ='" + param + "'";
                }
                seekRecor();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void seekRecor()
        {
            try
            {
                dg.ItemsSource = null;                
                OleDbCommand command = new OleDbCommand(queryToSeek, myConnection);
                OleDbDataAdapter da = new OleDbDataAdapter(command);
                DataSet ds = new DataSet();
                da.Fill(ds, queryToSeek);
                dg.ItemsSource = ds.Tables[0].DefaultView;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;           
            }
        }

       

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            myConnection.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            seek(searchCmbBox.SelectedIndex);
        }
    }
}
