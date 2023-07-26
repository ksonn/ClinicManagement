
using Clinic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Repository
{
    public interface IClinicRepo
    {
        IEnumerable<Patient> GetPatientPayed();
        IEnumerable<Patient> GetAll();
        Patient GetByPatientId(int id);
        void AddPatient(Patient patient, Treatment t);
        void EditPatient(Patient patient, Treatment t);
        void DeletePatient(Patient patient, Treatment t);
        Treatment GetTreatmentById(int id);
        List<Patient> SearchListPatient(string name, string dob, string gender);
        void AddLoaiThuoc(LoaiThuoc thuoc);
        LoaiThuoc GetLoaiThuoc(int id);
        void EditLoaiThuoc(LoaiThuoc thuoc);
        void DeleteLoaiThuoc(LoaiThuoc thuoc);
        IEnumerable<LoaiThuoc> GetLoaiThuocs();

        IEnumerable<TuThuoc> GetTuThuocs();
        IEnumerable<TuThuoc> GetListThuocTheoLoai(LoaiThuoc lt);
        TuThuoc GetThuoc(int id);
        void AddThuoc(TuThuoc thuoc);
        void EditThuoc(TuThuoc thuoc);
        void DeleteThuoc(TuThuoc thuoc);

        void AddService(Service s);
        void DeleteService(Service s);
        Service GetService(int id);
        void EditService(Service s);
        IEnumerable<Service> GetListService();
    }
}
