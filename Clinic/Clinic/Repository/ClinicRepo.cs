
using Clinic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Repository
{
    public class ClinicRepo:IClinicRepo
    {
        public void EditPatient(Patient patient, Treatment t) => ClinicManagement.Instance.EditPatient(patient, t); 
        public void DeletePatient(Patient patient, Treatment t) => ClinicManagement.Instance.DeletePatient(patient, t); 
        public void AddPatient(Patient patient, Treatment t) => ClinicManagement.Instance.AddPatient(patient,t);

        public Patient GetByPatientId(int id) => ClinicManagement.Instance.GetPatient(id);

        public Treatment GetTreatmentById(int id) => ClinicManagement.Instance.GetTreatment(id);

        public List<Patient> SearchListPatient(string name, string dob, string gender) =>ClinicManagement.Instance.SearchListPatient(name, dob, gender);  

        IEnumerable<Patient> IClinicRepo.GetAll() => ClinicManagement.Instance.GetPatientList();

        public void AddLoaiThuoc(LoaiThuoc thuoc) => ClinicManagement.Instance.AddLoaiThuoc(thuoc);
        public LoaiThuoc GetLoaiThuoc(int id) => ClinicManagement.Instance.GetLoaiThuoc(id);
        public void EditLoaiThuoc(LoaiThuoc thuoc) => ClinicManagement.Instance.EditLoaiThuoc(thuoc);
        public void DeleteLoaiThuoc(LoaiThuoc thuoc) => ClinicManagement.Instance.DeleteLoaiThuoc(thuoc);
        IEnumerable<LoaiThuoc> IClinicRepo.GetLoaiThuocs() => ClinicManagement.Instance.GetLoaiThuocs();

        public IEnumerable<TuThuoc> GetTuThuocs() => ClinicManagement.Instance.GetTuThuocs();

        public TuThuoc GetThuoc(int id) => ClinicManagement.Instance.GetThuoc(id);

        public void AddThuoc(TuThuoc thuoc) => ClinicManagement.Instance.AddThuoc(thuoc);

        public void EditThuoc(TuThuoc thuoc) => ClinicManagement.Instance.EditThuoc(thuoc);

        public void DeleteThuoc(TuThuoc thuoc) => ClinicManagement.Instance.DeleteThuoc(thuoc);

        public IEnumerable<TuThuoc> GetListThuocTheoLoai(LoaiThuoc lt) => ClinicManagement.Instance.GetListThuocTheoLoai(lt);

        public void AddService(Service s) => ClinicManagement.Instance.AddService(s);

        public void DeleteService(Service s) => ClinicManagement.Instance.DeleteService(s);

        public Service GetService(int id) => ClinicManagement.Instance.GetService(id);

        public void EditService(Service s) => ClinicManagement.Instance.EditService(s);

        public IEnumerable<Service> GetListService() => ClinicManagement.Instance.GetListService();

        public IEnumerable<Patient> GetPatientPayed() => ClinicManagement.Instance.GetPatientPayed();

    }
}
