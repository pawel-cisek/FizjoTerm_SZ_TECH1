using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
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
using System.Windows.Xps.Packaging;
using TabMenu2.Models;

namespace FizjoTerm
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ApplicationDbContext dbcontext = new ApplicationDbContext();
        //CollectionViewSource visitViewSource = new CollectionViewSource();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            TabMenu2.DefConnDataSet defConnDataSet = ((TabMenu2.DefConnDataSet)(this.FindResource("defConnDataSet")));
            // Load data into the table Patient. You can modify this code as needed.
            TabMenu2.DefConnDataSetTableAdapters.PatientTableAdapter defConnDataSetPatientTableAdapter = new TabMenu2.DefConnDataSetTableAdapters.PatientTableAdapter();
            defConnDataSetPatientTableAdapter.Fill(defConnDataSet.Patient);
            //System.Windows.Data.CollectionViewSource patientViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("patientViewSource")));
            //patientViewSource.View.MoveCurrentToFirst();
            dbcontext.Patients.Load();
            CollectionViewSource patientViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("patientViewSource")));
            patientViewSource.Source = dbcontext.Patients.Local;

            // Load data into the table Referral. You can modify this code as needed.
            TabMenu2.DefConnDataSetTableAdapters.ReferralTableAdapter defConnDataSetReferralTableAdapter = new TabMenu2.DefConnDataSetTableAdapters.ReferralTableAdapter();
            defConnDataSetReferralTableAdapter.Fill(defConnDataSet.Referral);
            //System.Windows.Data.CollectionViewSource referralViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("referralViewSource")));
            //referralViewSource.View.MoveCurrentToFirst();
            dbcontext.Referrals.Load();
            CollectionViewSource referralViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("referralViewSource")));
            referralViewSource.Source = dbcontext.Referrals.Local;
            CbPatient.ItemsSource = dbcontext.Patients.Local;

            // Load data into the table Physiotherapist. You can modify this code as needed.
            TabMenu2.DefConnDataSetTableAdapters.PhysiotherapistTableAdapter defConnDataSetPhysiotherapistTableAdapter = new TabMenu2.DefConnDataSetTableAdapters.PhysiotherapistTableAdapter();
            defConnDataSetPhysiotherapistTableAdapter.Fill(defConnDataSet.Physiotherapist);
            //System.Windows.Data.CollectionViewSource physiotherapistViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("physiotherapistViewSource")));
            //physiotherapistViewSource.View.MoveCurrentToFirst();
            dbcontext.Physiotherapists.Load();
            CollectionViewSource physiotherapistViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("physiotherapistViewSource")));
            physiotherapistViewSource.Source = dbcontext.Physiotherapists.Local;

            // Load data into the table Visit. You can modify this code as needed.
            TabMenu2.DefConnDataSetTableAdapters.VisitTableAdapter defConnDataSetVisitTableAdapter = new TabMenu2.DefConnDataSetTableAdapters.VisitTableAdapter();
            defConnDataSetVisitTableAdapter.Fill(defConnDataSet.Visit);
            //System.Windows.Data.CollectionViewSource visitViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("visitViewSource")));
            //visitViewSource.View.MoveCurrentToFirst();
            dbcontext.Visits.Load();
            CollectionViewSource visitViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("visitViewSource")));
            visitViewSource.Source = dbcontext.Visits.Local;
            CbPatient2.ItemsSource = dbcontext.Patients.Local;
            CbPhysiotherapist.ItemsSource = dbcontext.Physiotherapists.Local;

            RbAdding.IsChecked = true;

            List<string> hours = new List<string>() { "7:00", "7:30", "8:00", "8:30", "9:00", "9:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "14:30", "15:00", "15:30", "16:00", "16:30" };
            CbVisitTime.ItemsSource = hours;
            List<string> reportTypes = new List<string>() { "Dzienny", "Miesięczny", "Roczny" };
            CbReportType.ItemsSource = reportTypes;

            // Load data into the table Report. You can modify this code as needed.
            TabMenu2.DefConnDataSetTableAdapters.ReportTableAdapter defConnDataSetReportTableAdapter = new TabMenu2.DefConnDataSetTableAdapters.ReportTableAdapter();
            defConnDataSetReportTableAdapter.Fill(defConnDataSet.Report);
            //System.Windows.Data.CollectionViewSource reportViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("reportViewSource")));
            //reportViewSource.View.MoveCurrentToFirst();
            dbcontext.Reports.Load();
            CollectionViewSource reportViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("reportViewSource")));
            reportViewSource.Source = dbcontext.Reports.Local;

            reportDataGrid.SelectedIndex = -1;
            visitsFromReport.ItemsSource = null;
            LbPreviewReport.Content = "Wybierz raport aby zobaczyć podgląd";


        }

        private void BtAddPatient_Click(object sender, RoutedEventArgs e)
        {
            if (TbPatientName.Text != "" && TbPatientSurname.Text != "")
            {
                if (Patient.PeselValidation(TbPatientPesel.Text))
                {
                    MessageBoxResult result = MessageBox.Show("Czy zapisać pacjenta " + TbPatientName.Text + " " + TbPatientSurname.Text + "?", "Zapis", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        Patient.AddPatient(TbPatientName.Text, TbPatientSurname.Text, TbPatientPesel.Text, TbPatientAdress.Text, TbPatientPhone.Text, dbcontext);
                        patientDataGrid.Items.Refresh();
                        TbPatientName.Clear(); TbPatientAdress.Clear(); TbPatientPesel.Clear(); TbPatientPhone.Clear(); TbPatientSurname.Clear();
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

        private void BtDeletePatient_Click(object sender, RoutedEventArgs e)
        {
            Patient p1 = (Patient)patientDataGrid.SelectedItem;
            if (p1 != null)
            {
                MessageBoxResult result = System.Windows.MessageBox.Show("Czy usunąć pacjenta " + TbPatientName.Text + " " + TbPatientSurname.Text + "?", "Usuwanie pacjenta", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    Patient.DeletePatient(p1, dbcontext);
                }
            }
            patientDataGrid.Items.Refresh();
        }

        private void BtSearchPatient_Click(object sender, RoutedEventArgs e)
        {
            patientDataGrid.ItemsSource = Patient.SearchPatient(TbPatientName.Text, TbPatientSurname.Text, TbPatientPesel.Text, TbPatientAdress.Text, TbPatientPhone.Text, dbcontext);
        }

        private void BtViewAllPatient_Click(object sender, RoutedEventArgs e)
        {
            patientDataGrid.ItemsSource = dbcontext.Patients.Local;
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

        private void BtAddPhysio_Click(object sender, RoutedEventArgs e)
        {
            if (TbPhysioName.Text != "" && TbPhysioSurname.Text != "")
            {
                if (Physiotherapist.NwpzValidation(TbPhysioNpwz.Text) && TbPhysioNpwz.Text != "")
                {
                    MessageBoxResult result = MessageBox.Show("Czy zapisać fizjoterapeutę " + TbPhysioName.Text + " " + TbPhysioSurname.Text + "?", "Zapis", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        Physiotherapist.AddPhysiotherapist(TbPhysioName.Text, TbPhysioSurname.Text, TbPhysioAdress.Text, TbPhysioPhone.Text, TbPhysioEmail.Text , int.Parse(TbPhysioNpwz.Text), dbcontext);
                        physiotherapistDataGrid.Items.Refresh();
                        TbPhysioName.Clear(); TbPhysioAdress.Clear(); TbPhysioNpwz.Clear(); TbPhysioPhone.Clear(); TbPhysioSurname.Clear();
                    }
                }
                else
                {
                    MessageBox.Show("Podaj poprawny numer Numer Prawa Wykonywania Zawodu!");
                }

            }
            else
            {
                MessageBox.Show("Podaj imię i nazwisko!");
            }
        }

        private void BtDeletePhysio_Click(object sender, RoutedEventArgs e)
        {
            Physiotherapist p1 = (Physiotherapist)physiotherapistDataGrid.SelectedItem;
            if (p1 != null)
            {
                MessageBoxResult result = System.Windows.MessageBox.Show("Czy usunąć fizjoterapeutę " + TbPhysioName.Text + " " + TbPhysioSurname.Text + "?", "Usuwanie pacjenta", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    Physiotherapist.DeletePhysiotherapist(p1, dbcontext);
                }
            }
            physiotherapistDataGrid.Items.Refresh();
        }

        private void BtSearchPhysio_Click(object sender, RoutedEventArgs e)
        {
            physiotherapistDataGrid.ItemsSource = Physiotherapist.SearchPhysiotherapist(TbPhysioName.Text, TbPhysioSurname.Text, TbPhysioAdress.Text, TbPhysioPhone.Text, TbPhysioEmail.Text, TbPhysioNpwz.Text, dbcontext);
        }

        private void BtViewAllPhysio_Click(object sender, RoutedEventArgs e)
        {
            physiotherapistDataGrid.ItemsSource = dbcontext.Physiotherapists.Local;
        }

        private void BtAddVisit_Click(object sender, RoutedEventArgs e)
        {
            if (RbAdding.IsChecked == true)
            {
                if (CbPatient2.SelectedIndex < 0)
                    MessageBox.Show("Wybierz pacjenta!");
                else
                {
                    if (CbReferral.SelectedIndex < 0)
                        MessageBox.Show("Wybierz skierowanie!");
                    else
                    {
                        if (CbPhysiotherapist.SelectedIndex < 0)
                        {
                            MessageBox.Show("Wybierz fizjoterapeutę!");
                        }
                        else
                        {
                            if (CalendarVisit.SelectedDates.Count() < 1)
                                MessageBox.Show("Wybierz datę!");
                            else
                            {
                                foreach (var date in CalendarVisit.SelectedDates)
                                {
                                    Visit.AddVisit((Physiotherapist)CbPhysiotherapist.SelectedItem, (Referral)CbReferral.SelectedItem, date, CbVisitTime.SelectedItem.ToString(), dbcontext);
                                }
                                CbPhysiotherapist.SelectedIndex = -1; CbReferral.SelectedIndex = -1; CbPatient2.SelectedIndex = -1; CbVisitTime.SelectedIndex = -1; CalendarVisit.SelectedDates.Clear();
                                visitDataGrid.ItemsSource = dbcontext.Visits.Local.Where(v => v.VisitDate.Date.Equals(DateTime.Now.Date));
                                visitDataGrid.Items.Refresh();
                               
                            }
                        }
                    }
                }
            }
                
        }

        private void CbPatient2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<Referral> referrals = dbcontext.Referrals.Local.Where(r => r.Patient.Equals(CbPatient2.SelectedItem as Patient)).ToList();
            if (referrals.Count() > 0)
            {
                CbReferral.ItemsSource = dbcontext.Referrals.Local.Where(r => r.Patient.Equals(CbPatient2.SelectedItem as Patient));
            }
            else if (CbPatient2.SelectedIndex > -1 && RbAdding.IsChecked == true)
                MessageBox.Show("Brak zarejestrowanych skierowań dla pacjenta " + CbPatient2.SelectedItem.ToString());
        }

        private void BtDeleteVisit_Click(object sender, RoutedEventArgs e)
        {
            Visit v1 = (Visit)visitDataGrid.SelectedItem;
            if (v1 != null)
            {
                MessageBoxResult result = System.Windows.MessageBox.Show("Czy usunąć wizytę dla pacjenta " + v1.Referral.Patient.Name + " " + v1.Referral.Patient.Surname + " z dnia " + v1.VisitDate.ToShortDateString() + "?", "Usuwanie pacjenta", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    Visit.DeleteVisit(v1, dbcontext);
                }
            }
            physiotherapistDataGrid.Items.Refresh();
        }

        private void RbAdding_Checked(object sender, RoutedEventArgs e)
        {
            BtSearchVisit.IsEnabled = false;
            BtAddVisit.IsEnabled = true;
            CbVisitTime.IsEnabled = true;
            LabTodayVisits.Content = "Dzisiejsze wizyty:";
            CbReferral.IsEnabled = true;
            //visitViewSource.Source = dbcontext.Visits.Local.Where(v => v.VisitDate.Date.Equals(DateTime.Now.Date));
            visitDataGrid.ItemsSource = dbcontext.Visits.Local.Where(v => v.VisitDate.Date.Equals(DateTime.Now.Date));
            visitDataGrid.Items.Refresh();

        }

        private void RbSearching_Checked(object sender, RoutedEventArgs e)
        {
            BtSearchVisit.IsEnabled = true;
            BtAddVisit.IsEnabled = false;
            CbVisitTime.IsEnabled = false;
            LabTodayVisits.Content = "Znalezione wizyty:";
            CbReferral.IsEnabled = false;
            //visitViewSource.Source = dbcontext.Visits.Local;
            //visitDataGrid.Items.Refresh();
            visitDataGrid.ItemsSource = dbcontext.Visits.Local;

        }

        private void BtSearchVisit_Click(object sender, RoutedEventArgs e)
        {
            visitDataGrid.ItemsSource = Visit.SearchVisit(CbPatient2.SelectedItem as Patient, CbPhysiotherapist.SelectedItem as Physiotherapist, CalendarVisit.SelectedDate, dbcontext);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == System.Windows.WindowState.Normal)
            {
                this.WindowState = System.Windows.WindowState.Maximized;
            }
            else
            {
                this.WindowState = System.Windows.WindowState.Normal;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }

        private void CbPatient2_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Back || e.Key == Key.Delete)
            {
                CbPatient2.SelectedIndex = -1;
            }
        }

        private void CalendarVisit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back || e.Key == Key.Delete)
            {
                CalendarVisit.SelectedDate = null;
            }
        }

        private void BtGenerateReport_Click(object sender, RoutedEventArgs e)
        {
            if (CbReportType.SelectedIndex < 0)
            {
                MessageBox.Show("Wybierz typ raportu!");
            }
            else
            {
                switch (CbReportType.SelectedItem.ToString().Trim())
                {
                    case "Dzienny":
                        ICollection<Visit> dayResults = dbcontext.Visits.Local.Where(r => r.VisitDate.Equals(CalendarReport.SelectedDate)).ToList();
                        Report.AddReport("Raport_" + CbReportType.SelectedItem.ToString() + "_" + CalendarReport.SelectedDate.Value.ToShortDateString(), dayResults, dbcontext);
                        break;

                    case "Miesięczny":
                        ICollection<Visit> monthResults = dbcontext.Visits.Local.Where(r => r.VisitDate.Month.Equals(CalendarReport.SelectedDate.Value.Month) && r.VisitDate.Year.Equals(CalendarReport.SelectedDate.Value.Year)).ToList();
                        Report.AddReport("Raport_" + CbReportType.SelectedItem.ToString() + "_" + CalendarReport.SelectedDate.Value.Month + "." + CalendarReport.SelectedDate.Value.Year, monthResults, dbcontext);

                        break;

                    case "Roczny":
                        ICollection<Visit> yearResults = dbcontext.Visits.Local.Where(r => r.VisitDate.Year.Equals(CalendarReport.SelectedDate.Value.Year)).ToList();
                        Report.AddReport("Raport_" + CbReportType.SelectedItem.ToString() + "_" + CalendarReport.SelectedDate.Value.Year, yearResults, dbcontext);

                        break;

                }
            }
        }

        private void reportDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Report r1 = reportDataGrid.SelectedItem as Report;
            if (r1 != null)
            {
                visitsFromReport.ItemsSource = r1.Visits.ToList();
                LbPreviewReport.Content = "Podgląd raportu:";
            }
        }

        private void BtPrintReport_Click(object sender, RoutedEventArgs e)
        {

            // Create the print dialog object and set options
            PrintDialog pDialog = new PrintDialog();
            pDialog.PageRangeSelection = PageRangeSelection.AllPages;
            pDialog.UserPageRangeEnabled = true;

            // Display the dialog. This returns true if the user presses the Print button.
            Nullable<Boolean> print = pDialog.ShowDialog();
            if (print == true)
            {
                pDialog.PrintVisual(visitsFromReport, "Grid Printing.");
            }
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }


    }
}
