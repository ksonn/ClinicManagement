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
    /// Interaction logic for Drags.xaml
    /// </summary>
    public partial class Drags : Window
    {
        public IClinicRepo clinicRepo;
        public Drags(IClinicRepo clinic)
        {
            InitializeComponent();
            clinicRepo = clinic;
            Loaded += Window_Loaded;
            cbType.ItemsSource = clinicRepo.GetLoaiThuocs();
            LoadLVLThuoc();
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
            MessageBox.Show("Hiện đang ở màn hình quản lí tủ thuốc");
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

        private void Shutdown_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LoaiThuoc thuoc = new LoaiThuoc
            {
                LoaiId = int.Parse( tbLTID.Text),
                TenLoai = tbLTName.Text
            };
            clinicRepo.AddLoaiThuoc(thuoc);
            cbType.ItemsSource = clinicRepo.GetLoaiThuocs();
            LoadLVLThuoc();
            lvT.ItemsSource = clinicRepo.GetListThuocTheoLoai(thuoc);
        }

        private void lvLT_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoaiThuoc t = (LoaiThuoc)lvLT.SelectedItem != null ? (LoaiThuoc)lvLT.SelectedItem : null;
            if (t != null)
            {
                tbLTID.Text = t.LoaiId.ToString();
                tbLTName.Text = t.TenLoai;
                List<TuThuoc> list = (List<TuThuoc>)clinicRepo.GetListThuocTheoLoai(t);
                foreach (TuThuoc th in list)
                {
                    th.Loai = clinicRepo.GetLoaiThuoc((int)th.LoaiId);
                }
                lvT.ItemsSource = list;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(clinicRepo.GetLoaiThuoc(int.Parse(tbLTID.Text)) != null)
            {
                LoaiThuoc thuoc = new LoaiThuoc
                {
                    LoaiId = int.Parse(tbLTID.Text),
                    TenLoai = tbLTName.Text
                };
                clinicRepo.EditLoaiThuoc(thuoc);
                cbType.ItemsSource = clinicRepo.GetLoaiThuocs();
                LoadLVLThuoc();
                lvT.ItemsSource = clinicRepo.GetListThuocTheoLoai(thuoc);
            }
        }


        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn có chắc muốn xoá loại thuốc này?", "Xác thực xoá dữ liệu", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                if (clinicRepo.GetLoaiThuoc(int.Parse(tbLTID.Text)) != null)
                {
                    LoaiThuoc thuoc = new LoaiThuoc
                    {
                        LoaiId = int.Parse(tbLTID.Text),
                        TenLoai = tbLTName.Text
                    };
                    clinicRepo.DeleteLoaiThuoc(thuoc);
                    cbType.ItemsSource = clinicRepo.GetLoaiThuocs();
                    LoadLVLThuoc();
                    lvT.ItemsSource = clinicRepo.GetListThuocTheoLoai(thuoc);
                }
            }
            else
            {
                // Người dùng chọn No hoặc đóng hộp thoại, không thực hiện xoá dữ liệu
            }
            
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            TuThuoc t = new TuThuoc()
            {
                ThuocId = int.Parse(tbTID.Text),
                TenThuoc = tbTName.Text,
                HangSanXuat = tbTNhaSX.Text,
                NgaySx = (DateTime)nsx.SelectedDate,
                HanSx = (DateTime)hsd.SelectedDate,
                LoaiId = ((LoaiThuoc)cbType.SelectedItem).LoaiId,
                //Loai = (LoaiThuoc)cbType.SelectedItem
            };
            clinicRepo.AddThuoc(t);
            lvT.ItemsSource = clinicRepo.GetListThuocTheoLoai(clinicRepo.GetLoaiThuoc((int)t.LoaiId));
        }
        
        private void LoadLVLThuoc()
        {
            lvLT.ItemsSource = clinicRepo.GetLoaiThuocs();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if (clinicRepo.GetThuoc(int.Parse((tbTID.Text == null || tbTID.Text.Length == 0) ? "0" : tbTID.Text)) != null)
            {

                TuThuoc t = new TuThuoc()
                {
                    ThuocId = int.Parse(tbTID.Text),
                    TenThuoc = tbTName.Text,
                    HangSanXuat = tbTNhaSX.Text,
                    NgaySx = (DateTime)nsx.SelectedDate,
                    HanSx = (DateTime)hsd.SelectedDate,
                    LoaiId = ((LoaiThuoc)cbType.SelectedItem).LoaiId
                };
                clinicRepo.EditThuoc(t);
                lvT.ItemsSource = clinicRepo.GetListThuocTheoLoai(clinicRepo.GetLoaiThuoc((int)t.LoaiId));
            }
            else
            {
                MessageBox.Show("Sửa Thất Bại");
            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn có chắc muốn xoá thuốc này?", "Xác thực xoá dữ liệu", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                if (clinicRepo.GetThuoc(int.Parse((tbTID.Text == null || tbTID.Text.Length == 0) ? "0" : tbTID.Text)) != null)
                {
                    TuThuoc t = new TuThuoc()
                    {
                        ThuocId = int.Parse(tbTID.Text),
                        TenThuoc = tbTName.Text,
                        HangSanXuat = tbTNhaSX.Text,
                        NgaySx = (DateTime)nsx.SelectedDate,
                        HanSx = (DateTime)hsd.SelectedDate,
                        LoaiId = ((LoaiThuoc)cbType.SelectedItem).LoaiId
                    };
                    clinicRepo.DeleteThuoc(t);
                    lvT.ItemsSource = clinicRepo.GetListThuocTheoLoai(clinicRepo.GetLoaiThuoc((int)t.LoaiId));
                }
                else
                {
                    MessageBox.Show("Xoá Thất Bại");
                }
            }
            else { }
        }

        /*private void lvT_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TuThuoc t = (TuThuoc)lvT.SelectedItem != null ? (TuThuoc)lvT.SelectedItem : null;
            if (t != null)
            {
                tbTID.Text = t.ThuocId.ToString();
                tbTName.Text = t.TenThuoc;
                tbTNhaSX.Text = t.HangSanXuat;
                nsx.SelectedDate = t.NgaySx;
                hsd.SelectedDate = t.HanSx;
                t.Loai = clinicRepo.GetLoaiThuoc((int)t.LoaiId);
                cbType.SelectedItem = t.Loai;
            }
            
        }*/
    }
}
