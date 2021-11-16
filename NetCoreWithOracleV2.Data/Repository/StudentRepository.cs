using NetCoreWithOracleV2.Business.IRepository;
using NetCoreWithOracleV2.Common.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;


namespace NetCoreWithOracleV2.Data.Repository
{
    public class StudentRepository : DataServiceBase,  IStudentRepository 
    {
       
        public IEnumerable<Student> GetAllStudentAsList()
        {
            List<Student> StudentsList = new List<Student>();
            DataSet ds = GetAllStudentAsDataSet();

            var tempList = ds.Tables[0].AsEnumerable()
           .Select(dataRow => new Student
               {
               Id = (Int32)dataRow.Field<Int64>("Id"),
               Name = dataRow.Field<string>("Name")
               }).ToList();
            StudentsList = tempList;
            return StudentsList;
        }

       

        public Student GetStudentById(Student student)
        {
            List<Student> StudentsList = new List<Student>();
            DataSet ds = GetStudentAsDataSet(student);

            var tempList = ds.Tables[0].AsEnumerable()
           .Select(dataRow => new Student
           {
               Id = (Int32)dataRow.Field<Int64>("Id"),
               Name = dataRow.Field<string>("Name")
           }).ToList();
            StudentsList = tempList;
            return StudentsList.FirstOrDefault();
        }

        public void AddStudent(Student student)
        {
            ExecuteNonQuery("CREATESUDENTPROC", CreateParameter("p_NAME", OracleDbType.Varchar2 , student.Name , ParameterDirection.Input));
        }

        public bool DeleteStudent(Student student)
        {
            try
            {
                ExecuteNonQuery("DELETESTUDENTPROC", CreateParameter("P_ID", OracleDbType.Int32, student.Id, ParameterDirection.Input));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void EditStudent(Student student)
        {
            ExecuteNonQuery("UPDATESUDENTPROC",
                CreateParameter("p_ID", OracleDbType.Int32, student.Id, ParameterDirection.Input),
                CreateParameter("p_NAME", OracleDbType.Varchar2, student.Name, ParameterDirection.Input)
                );

        }


        public DataSet GetAllStudentAsDataSet()
        {
            DataSet ds;
            OracleCommand cmd = null;
            ds = ExecuteDataSet("GETALLSTUDENTPROC", CreateParameter("O_STUDNET", OracleDbType.RefCursor, ParameterDirection.Output));

            return ds;
        }

        
        public DataSet GetStudentAsDataSet(Student student)
        {
            DataSet ds;
            OracleCommand cmd = null;
            ds = ExecuteDataSet("GETSTUDENTBYIDPROC", CreateParameter("P_ID" , OracleDbType.Int32 , student.Id , ParameterDirection.Input),
                CreateParameter("O_STUDNET", OracleDbType.RefCursor, ParameterDirection.Output));

            return ds;
        }


    }
}
