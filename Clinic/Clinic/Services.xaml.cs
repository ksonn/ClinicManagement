using Clinic.Models;
using Clinic.Repository;
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

namespace Clinic
{
    /// <summary>
    /// Interaction logic for Services.xaml
    /// </summary>
    public partial class Services : Window
    {
        public IClinicRepo clinicRepo;

        public Services(IClinicRepo clinic)
        {
            InitializeComponent();
            clinicRepo = clinic;
            Loaded += Window_Loaded;
            LoadListService();
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
            MessageBox.Show("Hiện đang ở màn hình quản lí dịch vụ");
        }

        private void ManageMoney_Click(object sender, RoutedEventArgs e)
        {
            IClinicRepo _clinicRepo = new ClinicRepo();
            Money m = new Money(_clinicRepo);
            m.Show();
            this.Close();
        }

        private void Shutdown_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Service s = new Service()
            {
                Sid = (tbSID.Text == null || tbSID.Text.Length == 0) ? -1 : int.Parse(tbSID.Text),
                Sname = (tbSName.Text == null || tbSName.Text.Length == 0) ? "-1" : tbSName.Text,
                Price = (tbPrice.Text == null || tbPrice.Text.Length == 0) ? -1 : double.Parse(tbPrice.Text),
            };
            if (s.Sid != -1 || s.Sname != "-1" || s.Price != -1)
            {
                clinicRepo.AddService(s);
                LoadListService();
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (clinicRepo.GetService((tbSID.Text == null || tbSID.Text.Length == 0) ? -1 : int.Parse(tbSID.Text)) != null)
            {
                Service s = new Service()
                {
                    Sid = (tbSID.Text == null || tbSID.Text.Length == 0) ? -1 : int.Parse(tbSID.Text),
                    Sname = (tbSName.Text == null || tbSName.Text.Length == 0) ? "-1" : tbSName.Text,
                    Price = (tbPrice.Text == null || tbPrice.Text.Length == 0) ? -1 : double.Parse(tbPrice.Text),
                };
                if (s.Sid != -1 || s.Sname != "-1" || s.Price != -1)
                {
                    clinicRepo.EditService(s);
                    LoadListService();
                }
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (clinicRepo.GetService((tbSID.Text == null || tbSID.Text.Length == 0) ? -1 : int.Parse(tbSID.Text)) != null)
            {
                Service s = new Service()
                {
                    Sid = (tbSID.Text == null || tbSID.Text.Length == 0) ? -1 : int.Parse(tbSID.Text),
                    Sname = (tbSName.Text == null || tbSName.Text.Length == 0) ? "-1" : tbSName.Text,
                    Price = (tbPrice.Text == null || tbPrice.Text.Length == 0) ? -1 : double.Parse(tbPrice.Text),
                };
                if (s.Sid != -1 || s.Sname != "-1" || s.Price != -1)
                {
                    MessageBoxResult result = MessageBox.Show("Bạn có chắc muốn xoá dịch vụ này?", "Xác thực xoá dữ liệu", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                            clinicRepo.DeleteService(s);
                            LoadListService();
                        
                    }
                }
            }
        }



        private void lvService_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Service s = (Service)lvService.SelectedItem != null ? (Service)lvService.SelectedItem : null;
            if (s != null)
            {
                tbSID.Text = s.Sid.ToString();
                tbSName.Text = s.Sname.ToString();
                tbPrice.Text = s.Price.ToString();
            }
        }
        private void LoadListService()
        {
            lvService.ItemsSource = clinicRepo.GetListService();
        }

        private void btnSearchDichVu_Click(object sender, RoutedEventArgs e)
        {
            List<Service> services = clinicRepo.GetListService() as List<Service>;
            string name = tbSTen.Text.ToLower().Trim();
            List<Service> rs = services.Where(s => s.Sname.ToLower().Contains(name)).ToList();
            lvService.ItemsSource = rs;
        }
    }
}
