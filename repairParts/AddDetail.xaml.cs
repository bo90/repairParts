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
using System.Windows.Shapes;
using System.Data;
using System.Data.OleDb;
using System.Data.Odbc;

namespace repairParts
{
    /// <summary>
    /// Логика взаимодействия для AddDetail.xaml
    /// </summary>
    public partial class AddDetail : Window
    {
        /*строка подключения*/
        public static string connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=repairDB.mdb;";
        private OleDbConnection myConnection;

        public AddDetail()
        {
            InitializeComponent();
            myConnection = new OleDbConnection(connString);
            myConnection.Open();
        }

        private void ComboBox_DropDownClosed(object sender, EventArgs e)
        {

        }

        private void LoadingNameDetail()
        {
            try
            {
                string query = "SELECT namePair FROM Pairs";
                OleDbCommand comm = new OleDbCommand(query, myConnection);
                OleDbDataAdapter da = new OleDbDataAdapter(comm);
                DataSet ds = new DataSet();
                da.Fill(ds, query);
                nameDateil.ItemsSource = ds.Tables[0].DefaultView;

                query = "SELECT ID_Pair FROM Pairs";
                comm = new OleDbCommand(query, myConnection);
                da = new OleDbDataAdapter(comm);
                ds = new DataSet();
                da.Fill(ds, query);
                partNumber.ItemsSource = ds.Tables[0].DefaultView;

                //firmaComBox
                query = "SELECT NameFirm FROM Pairs";
                comm = new OleDbCommand(query, myConnection);
                da = new OleDbDataAdapter(comm);
                ds = new DataSet();
                da.Fill(ds, query);
                firmaComBox.ItemsSource = ds.Tables[0].DefaultView;

                //IndexFirm
                query = "SELECT IndexFirm FROM Firms";
                comm = new OleDbCommand(query, myConnection);
                da = new OleDbDataAdapter(comm);
                ds = new DataSet();
                da.Fill(ds, query);
                firmIDComBox.ItemsSource = ds.Tables[0].DefaultView;

                //NameFirm
                query = "SELECT NameFirm FROM Firms";
                comm = new OleDbCommand(query, myConnection);
                da = new OleDbDataAdapter(comm);
                ds = new DataSet();
                da.Fill(ds, query);
                fullNmaeComBox.ItemsSource = ds.Tables[0].DefaultView;

                //Mark
                query = "SELECT Mark FROM CarTable";
                comm = new OleDbCommand(query, myConnection);
                da = new OleDbDataAdapter(comm);
                ds = new DataSet();
                da.Fill(ds, query);
                markcomBox.ItemsSource = ds.Tables[0].DefaultView;

                //PetName
                query = "SELECT PetName FROM CarTable";
                comm = new OleDbCommand(query, myConnection);
                da = new OleDbDataAdapter(comm);
                ds = new DataSet();
                da.Fill(ds, query);
                ModelComBox.ItemsSource = ds.Tables[0].DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadingNameDetail();
        }

        
    }
}
