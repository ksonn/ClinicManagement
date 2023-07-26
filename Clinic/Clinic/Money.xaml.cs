using Clinic.Models;
using Clinic.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Clinic
{
    /// <summary>
    /// Interaction logic for Money.xaml
    /// </summary>
    public partial class Money : Window
    {
        public IClinicRepo clinicRepo;
        public Money(IClinicRepo clinic)
        {
            InitializeComponent();
            clinicRepo = clinic;
            Loaded += Window_Loaded;
            
            lvPayed.ItemsSource = clinicRepo.GetPatientPayed();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void ManagePatient_Click(object sender, RoutedEventArgs e)
        {
            IClinicRepo _repo = new ClinicRepo();
            MainWindow window = new MainWindow(_repo);
            window.Show();
            this.Close();
        }

        private void AddPatient_Click(object sender, RoutedEventArgs e)
        {
            IClinicRepo _clinicRepo = new ClinicRepo();
            AddPatient add = new AddPatient(_clinicRepo);
            add.Show();
            this.Close();
        }

        private void ManageDrug_Click(object sender, RoutedEventArgs e)
        {
            IClinicRepo _clinicRepo = new ClinicRepo();
            Drags add = new Drags(_clinicRepo);
            add.Show();
            this.Close();
        }

        private void ManageService_Click(object sender, RoutedEventArgs e)
        {
            IClinicRepo _clinicRepo = new ClinicRepo();
            Services s = new Services(_clinicRepo);
            s.Show();
            this.Close();
        }

        private void ManageMoney_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Hiện đang ở màn hình quản lí doanh thu");
        }

        private void Shutdown_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DateTime from = DPfrom.SelectedDate.HasValue? (DateTime)DPfrom.SelectedDate : DateTime.Now;
            DateTime to = DPto.SelectedDate.HasValue? (DateTime)DPto.SelectedDate : DateTime.Now;
            List<Patient> patients = (List<Patient>)clinicRepo.GetPatientPayed();
            List<Patient> filterByDate = patients
            .Where(patient => patient.Dayadd >= from && patient.Dayadd <= to)
            .ToList();
            lvPayed.ItemsSource = filterByDate;
            tblSum.Text = patients.Where(patient => patient.Dayadd >= from && patient.Dayadd <= to).Sum(p => p.Sids.Sum(s => s.Price)).ToString();
        }
    }
}
