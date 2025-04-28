using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EMS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EMS.Controllers;

public class HomeController : Controller
{
    private readonly EmsContext _context;
    private readonly ILogger<HomeController> _logger;
    //private static List<Student> students = new List<Student>();
    //private static int Count = 0;

    public HomeController(ILogger<HomeController> logger, EmsContext context)
    {
        _logger = logger;
        _context = context;
    }


    //Only Read
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    //Get All Data from Database
    [HttpGet]
    public async Task<IActionResult> GetAllData()
    {
        return Json(await _context.Students.ToListAsync());
    }

    //public bool CheeckData(Student data)
    //{
    //    if (!data.RollNo.HasValue ||
    //       !data.Fees.HasValue ||
    //       string.IsNullOrWhiteSpace(data.StudentName) ||
    //       string.IsNullOrWhiteSpace(data.StudentAddress) ||
    //       !data.StudentStandard.HasValue)
    //    {

    //        return true;
    //    }
    //    return false;
    //}

    [HttpPost]
    public IActionResult SaveData([FromBody] Student data)
    {
        // Validate required fields
        if (!ModelState.IsValid )
        {

            return Json(new { message = "There is Something Missing" });
        }

        try
        {
            _context.Students.Add(data);
            _context.SaveChanges();

            return Json(new { message = "Student Successfully added", data });
        }
        catch (Exception ex)
        {
            return Json(new { message = "Error occurred while saving data", error = ex.Message });
        }
    }


    [HttpGet]
    public IActionResult GetById(int id)
    {
        var checkStudent = _context.Students.FirstOrDefault(s => s.StudentId == id);
        //Console.WriteLine("Data is fatched");

        return Json(checkStudent);
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        var checkStudent = _context.Students.FirstOrDefault(s => s.StudentId == id);

        _context.Students.Remove(checkStudent);
        _context.SaveChanges();

        //Console.WriteLine("Data is Deleted");

        return Json(new { messege = "Delete Successfuly" });
    }

    [HttpPost]
    public IActionResult UpdateData([FromBody] Student updateData)
    {
        if (!ModelState.IsValid )
        {
            return Json(new { message = "There is Something Missing" });
        }

        var checkStudent = _context.Students.FirstOrDefault(s => s.StudentId == updateData.StudentId);
        //Console.WriteLine("Data is found for update");
        if (checkStudent != null)
        {
            checkStudent.StudentId = updateData.StudentId;
            checkStudent.StudentName = updateData.StudentName;
            checkStudent.StudentAddress = updateData.StudentAddress;
            checkStudent.RollNo = updateData.RollNo;
            checkStudent.Fees = updateData.Fees;
            checkStudent.StudentStandard = updateData.StudentStandard;

            _context.SaveChanges();
            return Json(new { messege = "Successfully updated ", checkStudent });
        }

        return Json(new { messege = "Please update prperly " });
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}


