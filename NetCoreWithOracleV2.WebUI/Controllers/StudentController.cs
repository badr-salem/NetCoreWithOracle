using Microsoft.AspNetCore.Mvc;
using NetCoreWithOracleV2.Business.IRepository;
using NetCoreWithOracleV2.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWithOracleV2.WebUI.Controllers
{
    public class StudentController : Controller
    {
        IStudentRepository _studentRepository;
        public StudentController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Upsert(int? id)
        {
            Student student = new Student();
            if (id == null)
            {
                //this is for create
                return View(student);
            }
            else
            {
                //this is for edit
                student.Id = (int)id;
                student = _studentRepository.GetStudentById(student);
                if (student == null)
                {
                    return NotFound();
                }
                return View(student);
            }
           

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Student student)
        {
            if (ModelState.IsValid)
            {
                if (student.Id == 0)
                {
                    // Mean Create New
                    _studentRepository.AddStudent(student);

                }
                else
                {
                    // Mean Edit Exiting one
                    _studentRepository.EditStudent(student);

                }

                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }





        #region call function
        [HttpGet]
        public IActionResult GetAll()
        {
            var allObj = _studentRepository.GetAllStudentAsList();
            return Json(new { data = allObj });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Student student = new Student();
            student.Id = id;
            bool result = _studentRepository.DeleteStudent(student);

            if (result == true)
            {
                return Json(new { success = true, message = "تم الحذف بنجاح" });

            }
            else
            {
                return Json(new { success = false, message = "حدث خطأ أثناء الحذف" });

            }

        }

        #endregion
    }
}
