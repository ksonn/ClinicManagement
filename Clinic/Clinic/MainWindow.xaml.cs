using Clinic.Models;
using Clinic.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Clinic
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public IClinicRepo clinicRepo;

        public MainWindow(IClinicRepo clinic)
        {
            InitializeComponent();
            clinicRepo = clinic;
            Loaded += Window_Loaded;
            cbsg.SelectedIndex = 0;
        }

        private void LoadListBN_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                lvBN.ItemsSource = clinicRepo.GetAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Danh Sách Bệnh Nhân");
            }
        }

        private void lvBN_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Patient p = (Patient)lvBN.SelectedItem != null ? (Patient)lvBN.SelectedItem : null;
            if (p != null)
            {
                lbPID.Content = p.Pid;
                tbName.Text = p.Pname;
                tbAddress.Text = p.Address;
                tbReason.Text = p.Reason;
                tbDob.Text = p.Dob.Year.ToString();
                tbSex.Text = p.Gender == true ? "Nam" : "Nữ";
                tbDateaad.Text = p.Dayadd.ToString();
                if (clinicRepo.GetTreatmentById(p.Pid) != null)
                {
                    tbDiagnostic.Text = clinicRepo.GetTreatmentById(p.Pid).Diagnose.ToString();
                }
                else
                {
                    tbDiagnostic.Text = "Trống";
                }
                if (clinicRepo.GetTreatmentById(p.Pid) != null)
                {
                    tbConclusion.Text = clinicRepo.GetTreatmentById(p.Pid).Solution.ToString();
                }
                else
                {
                    tbConclusion.Text = "Trống";
                }
            }
        }

        private void Shutdown_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void ManagePatient_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Hiện đang ở màn hình Quản lí bệnh nhân");
        }

        private void AddPatient_Click(object sender, RoutedEventArgs e)
        {
            IClinicRepo _clinicRepo = new ClinicRepo();
            AddPatient add = new AddPatient(_clinicRepo);
            add.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string Tenbn = (tbsn.Text == null || tbsn.Text.Length == 0) ? "1" : tbsn.Text;
            string DOB = (tbsa.Text == null || tbsa.Text.Length == 0) ? "1" : tbsa.Text;
            ComboBoxItem selectedItem = (ComboBoxItem)cbsg.SelectedItem;
            // validate
            string gender = selectedItem.Content.ToString() == "All" ? "-1" : selectedItem.Content.ToString();

            lvBN.ItemsSource = clinicRepo.SearchListPatient(Tenbn, DOB, gender);
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            Patient pa = clinicRepo.GetByPatientId(int.Parse(lbPID.Content.ToString()));
            if (pa != null)
            {
                Patient p = new Patient
                {
                    Pid = pa.Pid,
                    Pname = tbName.Text,
                    Phone = pa.Phone,
                    Address = tbAddress.Text,
                    Dob = DateTime.Parse("1/1/" + tbDob.Text),
                    Dayadd = DateTime.Now,
                    Reason = tbReason.Text,
                    Cmnd = "0",
                    Job = "0",
                    Gender = tbSex.Text.ToLower().Trim() == "nam" ? true : false
                };
                Treatment t = new Treatment
                {
                    Tid = pa.Pid,
                    Diagnose = tbDiagnostic.Text,
                    Solution = tbConclusion.Text
                };
                clinicRepo.EditPatient(p,t);
                LoadListBN_Click(sender, e);
            }

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Patient pa = clinicRepo.GetByPatientId(int.Parse(lbPID.Content.ToString()));
            if (pa != null)
            {
                
                MessageBoxResult cf = MessageBox.Show("Bạn có chắc muốn xoá bệnh án này?", "Xác thực xoá dữ liệu", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (cf == MessageBoxResult.Yes)
                {
                    Patient p = new Patient
                    {
                        Pid = pa.Pid,
                        Pname = pa.Pname,
                        Phone = pa.Phone,
                        Address = pa.Address,
                        Dob = pa.Dob,
                        Dayadd = pa.Dayadd,
                        Reason = pa.Reason,
                        Cmnd = pa.Cmnd,
                        Job = pa.Job,
                        Gender = pa.Gender
                    };
                    Treatment t = new Treatment
                    {
                        Tid = pa.Pid,
                        Diagnose = tbDiagnostic.Text,
                        Solution = tbConclusion.Text
                    };
                        clinicRepo.DeletePatient(p, t);
                        LoadListBN_Click(sender, e);
                }
                else
                {

                }
            }
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
    }
}
