using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TCS2010PPTG4.Data;

namespace TCS2010PPTG4.Controllers
{
    public class ReportController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ReportController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int departmentId = -1)
        {
            var studentIds = await _context.UserRoles.Where(u => u.RoleId == "Student")
                                                     .Select(u => u.UserId)
                                                     .ToListAsync();
           var students = await _context.Users.Where(u => studentIds.Contains(u.Id)).ToListAsync();
            var studentsOfDepartment = students.Where(s => s.DepartmentId == departmentId).ToList();
            ViewData["TotalStudentOfDept"] = studentsOfDepartment.Count();
            ViewData["TotalStudent"] = studentIds.Count();
            ViewData["TotalDepartment"] = await _context.Department.CountAsync();
            return View();
        }
    }
}
