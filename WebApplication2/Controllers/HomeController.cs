using System.Data;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using Npgsql;

namespace WebApplication2.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Readtable()
    {
        string Constr = "Host=localhost;Port=5432;Database=test;Username=postgres;Password=shatha1425";
        NpgsqlConnection conn = new NpgsqlConnection(Constr);
        NpgsqlCommand comm = new NpgsqlCommand("select * from phone", conn);
        conn.Open();
        string mm = "";

        NpgsqlDataReader reader = comm.ExecuteReader();
        while (reader.Read())
        {
            mm += reader["username"].ToString();
            mm += " "+(string)reader["usernumber"].ToString() + " ";
        }

        reader.Close();
        conn.Close();

        ViewData["result"] = mm;
        return View();
    }

    public IActionResult GetSingleVal()
    {
        string ConStr = "Host=localhost;Port=5432;Database=test;Username=postgres;Password=shatha1425";
        NpgsqlConnection conn = new NpgsqlConnection(ConStr);
        NpgsqlCommand comm = new NpgsqlCommand("select count(id) from phone where username!= NULL", conn);
        conn.Open();

        int count = (int)comm.ExecuteScalar();
        string mm = Convert.ToString(count);
        
        conn.Close();
        ViewData["result"] = mm;
        return View();
    }
    
    public IActionResult GetdatafromSystem()
    {
        DateTime d1 , d2;
        d1 = DateTime.Now;
        d2 = DateTime.Today;
        ViewBag.Now = d1;
        ViewBag.Today = d2;
        ViewBag.FullDateYime = d1.ToString("yyyy-MM-dd HH:mm:ss");
        ViewBag.OnlyDate = d2.ToString("yyyy-MM-dd");
        ViewBag.PastMonthDate = DateTime.Today.AddDays(-30).ToString();
        
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}