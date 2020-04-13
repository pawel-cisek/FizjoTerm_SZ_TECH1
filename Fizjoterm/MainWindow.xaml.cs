﻿
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
            if (TbName.Text != "" && TbSurname.Text != "")
            {
                if (Patient.PeselValidation(TbPesel.Text))
                {
                    MessageBoxResult result = MessageBox.Show("Czy zapisać pacjenta " + TbName.Text + " " + TbSurname.Text + "?", "Zapis", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        Patient.AddPatient(TbName.Text, TbSurname.Text, TbPesel.Text, TbAdress.Text, TbPhone.Text, dbcontext);
                        patientDataGrid.Items.Refresh();
                        TbName.Clear(); TbAdress.Clear(); TbPesel.Clear(); TbPhone.Clear(); TbSurname.Clear();
                    }                    
                }
                else
                {
                    MessageBox.Show("Podaj poprawny numer PESEL!");
                }
                
            }
            else
            {
                MessageBox.Show("Podaj imię i nazwisko!");
            }                 
        }

        private void BtDelete_Click(object sender, RoutedEventArgs e)
        {
            Patient p1 = (Patient)patientDataGrid.SelectedItem;
            if (p1 != null)
            {
                MessageBoxResult result = System.Windows.MessageBox.Show("Czy usunąć pacjenta " + TbName.Text + " " + TbSurname.Text + "?", "Usuwanie pacjenta", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    Patient.DeletePatient(p1, dbcontext);
                }
            }            
            patientDataGrid.Items.Refresh();
        }

        private void BtSearch_Click(object sender, RoutedEventArgs e)
        {           
            patientDataGrid.ItemsSource = Patient.SearchPatient(TbName.Text, TbSurname.Text, TbPesel.Text, TbAdress.Text, TbPhone.Text, dbcontext);
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


            CbPatient.ItemsSource = dbcontext.Patients.Local;
            // Load data into the table Referral. You can modify this code as needed.
            TabMenu2.DefaultConnDataSetTableAdapters.ReferralTableAdapter defaultConnDataSetReferralTableAdapter = new TabMenu2.DefaultConnDataSetTableAdapters.ReferralTableAdapter();
            defaultConnDataSetReferralTableAdapter.Fill(defaultConnDataSet.Referral);
            dbcontext.Referrals.Load();
            CollectionViewSource referralViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("referralViewSource")));
            referralViewSource.Source = dbcontext.Referrals.Local;

        }

        private void BtAddReferral_Click(object sender, RoutedEventArgs e)
        {
            
            int nbOfDays;
            bool success; 
            if (CbPatient.SelectedItem != null && TbDiagnosis.Text != "" && TbIcd10.Text != "" && DpDateReferral.SelectedDate != null)
            {
                success = int.TryParse(TbNbOfDays.Text, out nbOfDays);
                if (success)
                {
                    MessageBoxResult result = MessageBox.Show("Czy zapisać dla pacjenta " + ((Patient)CbPatient.SelectedItem).Name + " " + ((Patient)CbPatient.SelectedItem).Surname + "?", "Zapis", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        Referral.AddReferral(TbDiagnosis.Text, TbIcd10.Text, nbOfDays, DpDateReferral.SelectedDate.Value, CbPatient.SelectedItem as Patient, dbcontext);
                        TbDiagnosis.Clear(); TbIcd10.Clear(); DpDateReferral.SelectedDate = null; TbNbOfDays.Clear(); CbPatient.SelectedItem = null;
                    }
                }
                else
                {
                    MessageBox.Show("Podaj poprawną liczbę wizyt!");
                }
            }
            else
            {
                MessageBox.Show("Uzupełnij wszystkie dane!");
            }            
            referralDataGrid.Items.Refresh();            
        }

        private void BtDeleteReferral_Click(object sender, RoutedEventArgs e)
        {
            Referral r1 = (Referral)referralDataGrid.SelectedItem;
            if (r1 != null)
            {
                MessageBoxResult result = MessageBox.Show("Czy usunąć skierowanie dla pacjenta " + r1.Patient.Name + " " + r1.Patient.Surname + "?", "Usuwanie pacjenta", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    Referral.DeleteReferral(r1, dbcontext);
                }
            }
            referralDataGrid.Items.Refresh();
        }

        private void BtSearchReferral_Click(object sender, RoutedEventArgs e)
        {
            int nbOfDays;
            int.TryParse(TbNbOfDays.Text, out nbOfDays);
                
                referralDataGrid.ItemsSource = Referral.SearchReferral(TbDiagnosis.Text, TbIcd10.Text, nbOfDays, DpDateReferral.SelectedDate.GetValueOrDefault(), CbPatient.SelectedItem as Patient, dbcontext);
        }

        private void BtShowAllReferrals_Click(object sender, RoutedEventArgs e)
        {
            referralDataGrid.ItemsSource = dbcontext.Referrals.Local;
           
        }

        private void BtClose_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
