using Clinic.Models;
using Clinic.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
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
    /// Interaction logic for Payment.xaml
    /// </summary>
    public partial class Payment : Window
    {
        public IClinicRepo repo;
        public bool isApproved { get; private set; }
        Patient patient;
        public Payment(IClinicRepo _repo, Patient p)
        {
            InitializeComponent();
            Loaded += Window_Loaded;
            repo = _repo;
            patient = p;
            LoadPaymetInfor();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void LoadPaymetInfor()
        {
            tbNameP.Text = patient.Pname;
            lvS.ItemsSource = patient.Sids;
            tblSum.Text = patient.Sids.Sum(s => s.Price).ToString();
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            isApproved = true;
            this.DialogResult = true;
        }

    }
}
