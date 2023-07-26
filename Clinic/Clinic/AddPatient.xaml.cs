using Clinic.Models;
using Clinic.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace Clinic
{
    /// <summary>
    /// Interaction logic for AddPatient.xaml
    /// </summary>
    public partial class AddPatient : Window
    {
        public IClinicRepo repo;
        private List<Service> services = new List<Service>();
        public AddPatient(IClinicRepo _repo)
        {
            InitializeComponent();
            Loaded += Window_Loaded_1;
            repo = _repo;
            LoadListService();
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
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
            IClinicRepo _repo = new ClinicRepo();
            List<Service> s = (List<Service>)lvCS.ItemsSource != null ? (List<Service>)lvCS.ItemsSource : new List<Service>();
            
            Patient p = new Patient
            {
                Pid = int.Parse((tbAddID.Text == null || tbAddID.Text.Length == 0)?"-1":tbAddID.Text),
                Pname = (tbAddName.Text == null || tbAddName.Text.Length == 0) ? "-1" : tbAddName.Text,
                Phone = (tbAddPhone.Text == null || tbAddPhone.Text.Length == 0) ? "-1" : tbAddPhone.Text,
                Address = (tbAAddress.Text == null || tbAAddress.Text.Length == 0) ? "-1" : tbAAddress.Text,
                Dob = DateTime.Parse("1/1/" + tbAddDOB.Text),
                Dayadd = DateTime.Now,
                Reason = (tbReason.Text == null || tbReason.Text.Length == 0) ? "-1" : tbReason.Text,
                Cmnd = "0",
                Job = "0",
                Gender = rbgender.IsChecked == true ? true : false,
                Sids = s.Count == 0 ? new List<Service>() : s
            };
            Treatment t = new Treatment
            {
                Tid = int.Parse((tbAddID.Text == null || tbAddID.Text.Length == 0) ? "-1" : tbAddID.Text),
                Diagnose = (tbDiagnose.Text == null || tbDiagnose.Text.Length == 0) ? "-1" : tbDiagnose.Text,
                Solution = (tbSolution.Text == null || tbSolution.Text.Length == 0) ? "-1" : tbSolution.Text
            };
            if (s.Count > 0)
            {
                var payment = new Payment(_repo, p);
                bool? rs = payment.ShowDialog();
                if(rs == true && payment.isApproved == true)
                {
                    repo.AddPatient(p, t);
                    ResetInfor();
                    services = new List<Service>();
                    LoadListSelectService();
                }
            }
            else
            {
                MessageBox.Show("Vui Lòng Chọn Dịch Vụ Khám");
            }
        }

        private void ResetInfor()
        {
            tbAddID.Text = null;
            tbAddName.Text = null;
            tbAddPhone.Text = null;
            tbAAddress.Text = null;
            tbAddDOB.Text = null;
            tbReason.Text = null;
            tbDiagnose.Text = null;
            tbSolution.Text = null;
            rbgender.IsChecked = false;
        }

        private void LoadListSelectService()
        {
            lvCS.ItemsSource = null;
            lvCS.ItemsSource = services;
        }

        private void Shutdown_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
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
            IClinicRepo _clinicRepo = new ClinicRepo();
            Money m = new Money(_clinicRepo);
            m.Show();
            this.Close();
        }

        private void AddPatient_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Hiện đang ở màn hình nhập hồ sơ");
        }

        private void LoadListService()
        {
            lvS.ItemsSource = repo.GetListService();
        }

        private void lvS_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Service s = (Service)lvS.SelectedItem != null ? (Service)lvS.SelectedItem : null;
            if (s != null)
            {
                // neu da ton tai return ve false
                if (checkServiceExist(s))
                {
                    services.Add(s);
                }
                LoadListSelectService();
            }
        }

        private bool checkServiceExist(Service s)
        {
            for (int i = 0; i < services.Count; i++)
            {
                if (services[i] == s)
                {
                    return false;
                }
            }
            return true;
        }

        private void lvCS_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<Service> s = services;
            Service service = (Service)lvCS.SelectedItem != null ? (Service)lvCS.SelectedItem : null;
            for(int i = 0; i < s.Count; i++)
            {
                if (s[i] == service)
                s.Remove(service);
            }
            LoadListSelectService();
        }
    }
}
