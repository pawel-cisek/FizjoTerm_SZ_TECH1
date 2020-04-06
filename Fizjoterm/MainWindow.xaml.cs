
using FizjoTerm.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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


namespace FizjoTerm
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ApplicationDbContext dbcontext = new ApplicationDbContext();

        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void BtAdd_Click(object sender, RoutedEventArgs e)
        {
            Patient p1 = new Patient();
            p1.Name = TbName.Text;
            p1.Surname = TbSurname.Text;
            p1.Adress = TbAdress.Text;
            p1.Pesel = TbPesel.Text;
            p1.Phone = TbPhone.Text;
            Patient.AddPatient(p1, dbcontext);
            patientDataGrid.Items.Refresh();
            TbName.Clear(); TbAdress.Clear(); TbPesel.Clear(); TbPhone.Clear(); TbSurname.Clear();
        }

        private void BtDelete_Click(object sender, RoutedEventArgs e)
        {
            Patient p1 = (Patient)patientDataGrid.SelectedItem;
            Patient.DeletePatient(p1, dbcontext);
            patientDataGrid.Items.Refresh();
        }

        private void BtSearch_Click(object sender, RoutedEventArgs e)
        {
            Patient p1 = new Patient();
            p1.Name = TbName.Text;
            p1.Surname = TbSurname.Text;
            p1.Adress = TbAdress.Text;
            p1.Pesel = TbPesel.Text;
            p1.Phone = TbPhone.Text;

            patientDataGrid.ItemsSource = Patient.SearchPatient(p1, dbcontext);
        }

        private void BtViewAll_Click(object sender, RoutedEventArgs e)
        {
            patientDataGrid.ItemsSource = dbcontext.Patients.Local;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            TabMenu2.DefaultConnDataSet defaultConnDataSet = ((TabMenu2.DefaultConnDataSet)(this.FindResource("defaultConnDataSet")));
            // Load data into the table Patient. You can modify this code as needed.
            TabMenu2.DefaultConnDataSetTableAdapters.PatientTableAdapter defaultConnDataSetPatientTableAdapter = new TabMenu2.DefaultConnDataSetTableAdapters.PatientTableAdapter();
            defaultConnDataSetPatientTableAdapter.Fill(defaultConnDataSet.Patient);
            //System.Windows.Data.CollectionViewSource patientViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("patientViewSource")));
            //patientViewSource.View.MoveCurrentToFirst();
            dbcontext.Patients.Load();
            CollectionViewSource patientViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("patientViewSource")));
            patientViewSource.Source = dbcontext.Patients.Local;
            patientDataGrid.SelectedIndex = 0;
            patientDataGrid.Focus();

            CbPatient.ItemsSource = dbcontext.Patients.Local;
            // Load data into the table Referral. You can modify this code as needed.
            TabMenu2.DefaultConnDataSetTableAdapters.ReferralTableAdapter defaultConnDataSetReferralTableAdapter = new TabMenu2.DefaultConnDataSetTableAdapters.ReferralTableAdapter();
            defaultConnDataSetReferralTableAdapter.Fill(defaultConnDataSet.Referral);
            System.Windows.Data.CollectionViewSource referralViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("referralViewSource")));
            referralViewSource.View.MoveCurrentToFirst();
        }

        private void BtAddReferral_Click(object sender, RoutedEventArgs e)
        {
            Referral r1 = new Referral();
            Patient p1 = CbPatient.SelectedItem as Patient;
            r1.Diagnosis = TbDiagnosis.Text;
            r1.Icd10 = TbIcd10.Text;
            int NbOfDays; 
            bool success = int.TryParse(TbNbOfDays.Text, out NbOfDays);
            if (success)
                r1.NbOfDays = NbOfDays;
            else
                MessageBox.Show("Podaj poprawną liczbę wizyt!");



        }
    }
}
