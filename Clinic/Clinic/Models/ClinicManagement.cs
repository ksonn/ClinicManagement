using Clinic.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Xml.Linq;

namespace Clinic.Models
{
    public class ClinicManagement
    {
        private static ClinicManagement instance = null;

        private static readonly object instanceLock = new object();
        private ClinicManagement() { }
        public static ClinicManagement Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ClinicManagement();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<Patient> GetPatientPayed()
        {
            List<Patient> list;
            try
            {
                var db = new Clinic11Context();
                list = db.Patients.ToList();
                foreach (var pat in list)
                {
                    string sql = "SELECT s.sid" +
                        "  ,s.sname" +
                        "  ,s.price" +
                        "  FROM [Service] s join Patient_Service ps on s.sid = ps.sid\n" +
                        "join [Patient] p on ps.pid= p.pid\n" +
                        "where p.pid = @Pid";
                    List<Service> s = db.Services.FromSqlRaw(sql,
                        new SqlParameter("@Pid", pat.Pid)).ToList();
                    if (s != null)
                    {
                        pat.Sids = s;
                    }
                    else
                    {
                        list.Remove(pat);
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

        public IEnumerable<Patient> GetPatientList()
        {
            List<Patient> list;
            try
            {
                var db = new Clinic11Context();
                list = db.Patients.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

        public Patient GetPatient(int id)
        {
            Patient patient;
            try
            {
                var db = new Clinic11Context();
                patient = db.Patients.SingleOrDefault(patient => patient.Pid == id);
                if (patient != null)
                {
                    return patient;
                }
                else
                {
                    MessageBox.Show("Bệnh Nhân Chưa Tồn Tại!");
                    return null;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return null;
        }

        public Treatment GetTreatment(int id)
        {
            try
            {
                var db = new Clinic11Context();
                Treatment t = db.Treatments.SingleOrDefault(patient => patient.Tid == id);
                if (t != null)
                {
                    return t;
                }
                else
                {
                    MessageBox.Show("Không thể tìm được Phương Pháp Chữa Trị cho bệnh nhân này!", "Alert");
                    return null;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return null;
        }

        public void AddPatient(Patient patient, Treatment t)
        {
            try
            {
                if (GetPatient(patient.Pid) == null)
                {
                    var db = new Clinic11Context();
                    int Pid = patient.Pid;
                    List<Service> s = patient.Sids.ToList();
                    patient.Sids.Clear();
                    foreach (Service se in patient.Sids)
                    {
                        MessageBox.Show(se.Sname);
                    }

                    db.Patients.Add(patient);
                    db.Treatments.Add(t);
                    db.SaveChanges();

                    for (int i = 0; i < s.Count; i++)
                    {
                        string sql = "INSERT INTO [Patient_Service]" +
                        "           ([pid]" +
                        "           ,[sid])" +
                        "     VALUES" +
                        "          (@Pid" +
                        "           ,@Sid)\n";
                        int rowsAffected = db.Database.ExecuteSqlRaw(sql,
                        new SqlParameter("@Pid", patient.Pid),
                        new SqlParameter("@Sid", s[i].Sid));
                        if (rowsAffected > 0)
                        {
                            // da them oke
                        }
                        else
                        {
                            MessageBox.Show("Thêm Dịch Vụ Sử Dụng " + s[i].Sname + " Thất Bại");
                        }
                    }
                    MessageBox.Show("Thêm Hồ Sơ Bệnh Án Thành Công", "Thêm Bệnh Án");

                }
                else
                {
                    MessageBox.Show("Bệnh Nhân Đã Tồn Tại", "Alert");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void EditPatient(Patient patient, Treatment t)
        {
            try
            {
                if (GetPatient(patient.Pid) != null)
                {
                    var db = new Clinic11Context();
                    db.Patients.Update(patient);
                    db.Treatments.Update(t);
                    db.SaveChanges();
                    MessageBox.Show("Chỉnh sửa Hồ Sơ Bệnh Án Thành Công", "Sửa Bệnh Án");
                }
                else
                {
                    MessageBox.Show("Bệnh Nhân Không Tồn Tại", "Alert");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeletePatient(Patient patient, Treatment t)
        {
            try
            {
                if (GetPatient(patient.Pid) != null)
                {
                    var db = new Clinic11Context();
                        // xoa o bang quan he truoc
                        string sql = "DELETE FROM [Patient_Service]" +
                            "      WHERE pid = @Pid\n";
                        int rowsAffected = db.Database.ExecuteSqlRaw(sql,
                        new SqlParameter("@Pid", patient.Pid));

                        db.Treatments.Remove(t);
                        db.Patients.Remove(patient);
                        db.SaveChanges();
                        MessageBox.Show("Xoá Hồ Sơ Bệnh Án Thành Công", "Xoá Bệnh Án");

                }
                else
                {
                    MessageBox.Show("Bệnh Nhân Không Tồn Tại", "Alert");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Patient> SearchListPatient(string name, string dob, string gender)
        {
            List<Patient> list = new List<Patient>();
            try
            {
                using (var db = new Clinic11Context())
                {
                    String sql = "SELECT [pid]" +
                        ",[pname]" +
                        ",[gender]" +
                        ",[dob]" +
                        ",[job]" +
                        ",[address]" +
                        ",[phone]" +
                        ",[cmnd]" +
                        ",[dayadd]" +
                        ",[reason]" +
                        ",[photo]" +
                        "FROM [Patient]\n";

                    if (gender != "-1" || name != "1" || dob != "1")
                    {
                        sql += "Where ";
                    }

                    if (gender != "-1")
                    {
                        gender = gender == "Nam" ? "1" : "0";
                        sql += "[gender] =" + gender;
                    }

                    if (gender != "-1" && dob != "1")
                    {
                        sql += " AND YEAR(dob) AS YearOnly=" + dob;
                    }
                    else if (name != "1" && dob != "1")
                    {
                        sql += "YEAR(dob) AS YearOnly=" + dob + " AND pname LIKE 'N%" + name + "'%";
                    }
                    else if (dob != "1")
                    {
                        sql += "YEAR(dob) AS YearOnly=" + dob;
                    }

                    if (name != "1" && gender != "-1")
                    {
                        sql += " AND pname LIKE N'%" + name + "%'\n";
                    }
                    else if (name != "1")
                    {
                        sql += " pname LIKE N'%" + name + "%'\n";
                    }
                    var query = db.Patients.FromSqlRaw(sql);
                    if (query != null)
                    {
                        List<Patient> lits = query.ToList();
                        list.AddRange(lits);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return list;
        }

        public LoaiThuoc GetLoaiThuoc(int id)
        {
            try
            {
                var db = new Clinic11Context();
                LoaiThuoc t = db.LoaiThuocs.SingleOrDefault(patient => patient.LoaiId == id);
                if (t != null)
                {
                    return t;
                }
                else
                {
                    MessageBox.Show("Không thể tìm được loại thuốc này", "Alert");
                    return null;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return null;
        }


        public void AddLoaiThuoc(LoaiThuoc thuoc)
        {
            try
            {
                if (GetLoaiThuoc(thuoc.LoaiId) == null)
                {
                    var db = new Clinic11Context();
                    db.LoaiThuocs.Add(thuoc);
                    db.SaveChanges();
                    MessageBox.Show("Thêm Loại Thuốc Thành Công", "Thêm Loại Thuốc");
                }
                else
                {
                    MessageBox.Show("Loại Thuốc Đã Tồn Tại", "Alert");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void EditLoaiThuoc(LoaiThuoc thuoc)
        {
            try
            {
                if (GetLoaiThuoc(thuoc.LoaiId) != null)
                {
                    var db = new Clinic11Context();
                    db.LoaiThuocs.Update(thuoc);
                    db.SaveChanges();
                    MessageBox.Show("Sửa Loại Thuốc Thành Công", "Sửa Loại Thuốc");
                }
                else
                {
                    MessageBox.Show("Loại Thuốc Này Không Tồn Tại", "Alert");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void DeleteLoaiThuoc(LoaiThuoc thuoc)
        {
            try
            {
                if (GetLoaiThuoc(thuoc.LoaiId) != null)
                {
                    var db = new Clinic11Context();
                    List<TuThuoc> tt = db.TuThuocs.Where(t => t.LoaiId == thuoc.LoaiId).ToList();
                    db.TuThuocs.RemoveRange(tt);
                    db.LoaiThuocs.Remove(thuoc);
                    db.SaveChanges();
                    MessageBox.Show("Xoá Loại Thuốc Thành Công", "Xoá Loại Thuốc");
                }
                else
                {
                    MessageBox.Show("Loại Thuốc Này Không Tồn Tại", "Alert");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<LoaiThuoc> GetLoaiThuocs()
        {
            List<LoaiThuoc> list;
            try
            {
                var db = new Clinic11Context();
                list = db.LoaiThuocs.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

        public IEnumerable<TuThuoc> GetTuThuocs()
        {
            List<TuThuoc> list;
            try
            {
                var db = new Clinic11Context();
                list = db.TuThuocs.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

        public IEnumerable<TuThuoc> GetListThuocTheoLoai(LoaiThuoc lt)
        {
            List<TuThuoc> list = null;
            try
            {
                var db = new Clinic11Context();
                list = db.TuThuocs.Where(t => t.LoaiId == lt.LoaiId).Include(t => t.Loai).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

        public TuThuoc GetThuoc(int id)
        {
            try
            {
                var db = new Clinic11Context();
                TuThuoc t = db.TuThuocs.SingleOrDefault(patient => patient.ThuocId == id);
                if (t != null)
                {
                    return t;
                }
                else
                {
                    MessageBox.Show("Không thể tìm được thuốc này", "Alert");
                    return null;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return null;
        }

        public void AddThuoc(TuThuoc thuoc)
        {
            try
            {
                if (GetThuoc(thuoc.ThuocId) == null)
                {
                    var db = new Clinic11Context();
                    db.TuThuocs.Add(thuoc);

                    db.SaveChanges();
                    MessageBox.Show("Thêm Thuốc Vào Tủ Thành Công", "Thêm Thuốc");
                }
                else
                {
                    MessageBox.Show("Thuốc Đã Tồn Tại", "Alert");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void EditThuoc(TuThuoc thuoc)
        {
            try
            {
                if (GetThuoc(thuoc.ThuocId) != null)
                {
                    var db = new Clinic11Context();
                    db.TuThuocs.Update(thuoc);
                    db.SaveChanges();
                    MessageBox.Show("Sửa Thuốc Trong Tủ Thành Công", "Sửa Thuốc");
                }
                else
                {
                    MessageBox.Show("Thuốc Không Tồn Tại", "Alert");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteThuoc(TuThuoc thuoc)
        {
            try
            {
                if (GetLoaiThuoc(thuoc.ThuocId) != null)
                {
                    var db = new Clinic11Context();
                    db.TuThuocs.Remove(thuoc);
                    db.SaveChanges();
                    MessageBox.Show("Xoá Thuốc Trong Tủ Thành Công", "Xoá Thuốc");
                }
                else
                {
                    MessageBox.Show("Thuốc Chưa Tồn Tại", "Alert");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void AddService(Service s)
        {
            try
            {
                if (GetService(s.Sid) == null)
                {
                    using (var db = new Clinic11Context())
                    {
                        String sql = "INSERT INTO [Service]" +
                            "           ([sid]" +
                            "           ,[sname]" +
                            "          ,[price])" +
                            "     VALUES" +
                            "          ( @sId" +
                            "          , @Sname" +
                            "           , @Price)";
                        int rowsAffected = db.Database.ExecuteSqlRaw(sql,
                            new SqlParameter("@sId", s.Sid),
                            new SqlParameter("@Sname", s.Sname),
                            new SqlParameter("@Price", s.Price));
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Thêm Dịch Vụ Thành Công", "Thêm Dịch Vụ");
                        }
                        else
                        {
                            MessageBox.Show("Thêm Thất Bại");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Dịch Vụ Đã Tồn Tại", "Alert");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Service GetService(int id)
        {
            try
            {
                var db = new Clinic11Context();
                Service t = db.Services.SingleOrDefault(s => s.Sid == id);
                if (t != null)
                {
                    return t;
                }
                else
                {
                    MessageBox.Show("Không thể tìm được dịch vụ này", "Alert");
                    return null;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return null;
        }

        public void EditService(Service s)
        {
            try
            {
                if (GetService(s.Sid) != null)
                {
                    using (var db = new Clinic11Context())
                    {
                        String sql = "UPDATE [Service]" +
                            "   SET [sid] = @sId" +
                            "      ,[sname] = @Sname" +
                            "     ,[price] = @Price" +
                            "\n WHERE [sid] = @sID";
                        int rowsAffected = db.Database.ExecuteSqlRaw(sql,
                            new SqlParameter("@sId", s.Sid),
                            new SqlParameter("@Sname", s.Sname),
                            new SqlParameter("@Price", s.Price),
                            new SqlParameter("@sID", s.Sid));
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Sửa Dịch Vụ Thành Công", "Sửa Dịch Vụ");
                        }
                        else
                        {
                            MessageBox.Show("Sửa Thất Bại");
                        }
                    }

                }
                else
                {
                    MessageBox.Show("Dịch Vụ Chưa Tồn Tại", "Alert");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteService(Service s)
        {
            try
            {
                if (GetService(s.Sid) != null)
                {
                    var db = new Clinic11Context();
                        // xoa o bang quan he truoc
                        string sql = "DELETE FROM [Patient_Service]" +
                            "      WHERE [sid] = @Sid\n";
                        int rowsAffected = db.Database.ExecuteSqlRaw(sql,
                        new SqlParameter("@Sid", s.Sid));
                        db.Services.Remove(s);
                        db.SaveChanges();
                        MessageBox.Show("Xoá Dịch Vụ Thành Công", "Xoá Dịch Vụ");

                }
                else
                {
                    MessageBox.Show("Dịch Vụ Chưa Tồn Tại", "Alert");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<Service> GetListService()
        {
            List<Service> list;
            try
            {
                var db = new Clinic11Context();
                list = db.Services.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }
    }
}
