using First_Step_API.Data;
using First_Step_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace First_Step_API.Controllers
{
    public class JobPostingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JobPostingController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateOrEditJobPosting(int id)
        {
            return View();
        }

        public IActionResult CreateEditJob(JobPosting jobPosting, IFormFile file)
        {
            // convert image to bytes
            if (file != null)
            {
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    var bytes = ms.ToArray();
                    jobPosting.CompanyImage = bytes;
                }
            }

            if (jobPosting.Id == 0)
            {
                // add new job if not editing
                _context.JobPostings.Add(jobPosting);
            } else
            {
                // edit exists job
                var jobFromDb = _context.JobPostings.FirstOrDefault(x => x.Id == jobPosting.Id);
                //_context.JobPostings.Update(jobPosting);

                if (jobFromDb == null)
                {
                    return NotFound();
                }

                jobFromDb.StartDate = jobPosting.StartDate;
                jobFromDb.Salary = jobPosting.Salary;
                jobFromDb.ContactPhone = jobPosting.ContactPhone;
                jobFromDb.ContactMail = jobPosting.ContactMail;
                jobFromDb.ContactWebsite = jobPosting.ContactWebsite;
                jobFromDb.JobTitle = jobPosting.JobTitle;
                jobFromDb.CompanyImage = jobPosting.CompanyImage;
                jobFromDb.CompanyName = jobPosting.CompanyName;
                jobFromDb.JobDescription = jobPosting.JobDescription;
                jobFromDb.JobLocation = jobPosting.JobLocation;
            }

            _context.SaveChanges();

            // write jobposting to db


            return RedirectToAction("Index");
        }
    }
}
