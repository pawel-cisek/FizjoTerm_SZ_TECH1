using FizjoTerm;
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
using System.Windows.Shapes;
using TabMenu2.Models;

namespace TabMenu2
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {

        public ApplicationDbContext dbcontext = new ApplicationDbContext();
        public LoginWindow()
        {
            InitializeComponent();
            dbcontext.Physiotherapists.Load();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(TbLogin.Text == "admin" && TbPassword.Password == "admin")
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            else if (dbcontext.Physiotherapists.Local.Any<Physiotherapist>(p => p.Surname.Equals(TbLogin.Text) && p.Npwz.ToString().Equals(TbPassword.Password)))
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                mainWindow.TiPhysio.Visibility = Visibility.Collapsed;
                mainWindow.TiReport.Visibility = Visibility.Collapsed;
                this.Close();
            }
            else
            {
                LbValidation.Visibility = Visibility.Visible;
            }
        }
    }
}
