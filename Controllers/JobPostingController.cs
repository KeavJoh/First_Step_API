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
            var jobPostingFromDb = _context.JobPostings.Where(x => x.OwnerUsername == User.Identity.Name).ToList();
            return View(jobPostingFromDb);
        }

        public IActionResult CreateOrEditJobPosting(int id)
        {
            if (id != null)
            {
                var jobPostingFromDb = _context.JobPostings.SingleOrDefault(x => x.Id == id);

                if (jobPostingFromDb != null)
                {
                    return View(jobPostingFromDb);
                }
                else
                {
                    return NotFound();
                }
            }
            return View();
        }

        public IActionResult CreateEditJob(JobPosting jobPosting, IFormFile file)
        {
            jobPosting.OwnerUsername = User.Identity.Name;
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
                jobFromDb.OwnerUsername = jobPosting.OwnerUsername;
            }

            _context.SaveChanges();

            // write jobposting to db


            return RedirectToAction("Index");
        }
    }
}
