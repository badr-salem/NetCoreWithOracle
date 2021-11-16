using NetCoreWithOracleV2.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreWithOracleV2.Business.IRepository
{
    public interface IStudentRepository
    {
        IEnumerable<Student> GetAllStudentAsList();

        Student GetStudentById(Student student);

        void AddStudent(Student student);

        bool DeleteStudent(Student student);

        void EditStudent(Student student);
    }
}
