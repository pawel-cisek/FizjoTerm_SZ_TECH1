using Domain.Models;
using Projekt1.Models;
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
using System.Data.Entity;
using System.Windows.Forms;

namespace TabMenu2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
       
        public MainWindow()
        {
            InitializeComponent();            
        }
        public void Window_Loaded(object sender, RoutedEventArgs e)
        {

            Projekt1.DefaultConnection2DataSet defaultConnection2DataSet = ((Projekt1.DefaultConnection2DataSet)(this.FindResource("defaultConnection2DataSet")));
            // Load data into the table Patients. You can modify this code as needed.
            Projekt1.DefaultConnection2DataSetTableAdapters.PatientsTableAdapter defaultConnection2DataSetPatientsTableAdapter = new Projekt1.DefaultConnection2DataSetTableAdapters.PatientsTableAdapter();
            defaultConnection2DataSetPatientsTableAdapter.Fill(defaultConnection2DataSet.Patients);
            //System.Windows.Data.CollectionViewSource patientsViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("patientsViewSource")));
            //patientsViewSource.View.MoveCurrentToFirst();
            dbcontext.Patients.Load();
            CollectionViewSource patientsViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("patientsViewSource")));
            patientsViewSource.Source = dbcontext.Patients.Local;
            patientsDataGrid.SelectedIndex = 0;
            patientsDataGrid.Focus();        
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Patient p1 = new Patient();
            p1.Name = TbName.Text;
            p1.Surname = TbSurname.Text;
            p1.Adress = TbAdress.Text;
            p1.Pesel = TbPesel.Text;
            p1.Phone = TbPhone.Text;

            
            MessageBoxResult result = System.Windows.MessageBox.Show("Czy zapisać pacjenta " + TbName.Text + " " + TbSurname.Text + "?", "Zapis", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                dbcontext.Patients.Add(p1);
                dbcontext.SaveChanges();
                patientsDataGrid.Items.Refresh();
                TbName.Clear(); TbAdress.Clear(); TbPesel.Clear(); TbPhone.Clear(); TbSurname.Clear();
            }
        }

       

        private void BtDelete_Click(object sender, RoutedEventArgs e)
        {
            Patient p1 = new Patient();
            p1 = (Patient)patientsDataGrid.SelectedItem;
            int index = patientsDataGrid.SelectedIndex;
            dbcontext.Patients.Remove(p1);
            dbcontext.SaveChanges();
            patientsDataGrid.ItemsSource = dbcontext.Patients.Local;            
        }

        private void BtSearch_Click(object sender, RoutedEventArgs e)
        {
            patientsDataGrid.ItemsSource = dbcontext.Patients.Local.Where(p => p.Name.Contains(TbName.Text) && p.Surname.Contains(TbSurname.Text) && p.Pesel.Contains(TbPesel.Text) && p.Phone.Contains(TbPhone.Text) && p.Adress.Contains(TbAdress.Text));
           // patientsDataGrid.ItemsSource = dbcontext.Patients.Local.Where(p => p.Surname.Contains(TbSurname.Text));
        }

        private void BtViewAll_Click(object sender, RoutedEventArgs e)
        {
            patientsDataGrid.ItemsSource = dbcontext.Patients.Local;
        }
    }
}
