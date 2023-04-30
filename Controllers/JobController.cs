using JobPortal.DBContexts;
using JobPortal.DTOs;
using JobPortal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.ObjectPool;
using System.Linq;
using System.Reflection.Metadata;
using System.Text.Json.Serialization;


namespace JobPortal.Controllers
{
	public class JobController : Controller
	{
		private readonly JobDbContext _dbContext;

		public JobController(JobDbContext context)
		{
			this._dbContext = context;
		}


		public IActionResult Index()
		{

			string keywords = Request.Query["keywords"].ToString().Trim();
			string location = Request.Query["location"].ToString().Trim();
			

			if (!keywords.Equals("") && !location.Equals(""))
			{
				var jobsList = _dbContext.Jobs
			.Select(x => new ListJobDTO
			{
				id = x.id,
				Title = x.Title,
				Description = x.Description,
				Location = x.Location,
				Salary = x.Salary,
				CreationDate = x.creationDate,
				CompanyName = x.CompanyName

			})
			.
			ToList().OrderByDescending(job=> job.CreationDate);

				return View(jobsList);
			}


			
	
			var searchJobList = _dbContext.Jobs
				.Where(j =>
				(j.Title.Contains(keywords)) ||
					j.Location.Equals(location)
				)
			.Select(x => new ListJobDTO
			{
				id = x.id,
				Title = x.Title,
				Description = x.Description,
				Location = x.Location,
                Salary = x.Salary,
                CreationDate = x.creationDate,
                CompanyName = x.CompanyName

			})
			.
			ToList().OrderByDescending(job => job.CreationDate);
            return View(searchJobList);
		}

		public IActionResult Filter()
		{

            string location = Request.Query["location"].ToString().Trim();
            string jobType = Request.Query["jobType"].ToString().Trim();
            string salary = Request.Query["salary"].ToString().Trim();
            string sortBy = Request.Query["sortBy"].ToString().Trim();


			int fromSalary = 0;
			int toSalary = 0;

			if (!salary.Equals("") && !salary.Equals("100000"))
			{ 
                string[] salaryRange = salary.Split('-');
                 fromSalary = Convert.ToInt32(salaryRange[0]);
                 toSalary = Convert.ToInt32(salaryRange[1]);
            }



			IQueryable<Job> query = _dbContext.Jobs;


            if (!string.IsNullOrEmpty(jobType))
			{
                query = query.Where(a=>a.JobType == jobType);
            }

			if (!string.IsNullOrEmpty(location))
			{
				query = query.Where(a => a.Location.Equals(location));
			}
			

			if (fromSalary!=0 && toSalary!=0)
			{
				query = query.Where(a => a.Salary >= fromSalary && a.Salary <= toSalary);
			}
			if (salary.Equals("100000"))
			{
                query = query.Where(a => a.Salary >= 100000);
            }

			if (sortBy.Equals("date"))
			{
				query = query.OrderByDescending(job=>job.creationDate);
			}

            if (sortBy.Equals("salary"))
            {
				query = query.OrderByDescending(job=>job.Salary);
            }


			var jobList = query.Select(x => new ListJobDTO
			{
				id = x.id,
				Title = x.Title,
				Description = x.Description,
				Location = x.Location,
				Salary = x.Salary,
				CreationDate = x.creationDate,
				CompanyName = x.CompanyName

			}).ToList();

            return View("Index", jobList);
            }


		[Authorize]
		public IActionResult Details(int id)
		{
			var detailjob = _dbContext.Jobs
				.Include(job => job.Requirments)
				.Select(x => new DetailJobDTO
				{
					id = x.id,
					Title = x.Title,
					Description = x.Description,
					Location = x.Location,
					CompanyName = x.CompanyName,
					Qualifications = x.Qualifications,
					Salary = x.Salary,
					ApplyHelp = x.ApplyHelp,
					CreationDate = x.creationDate,
					JobType = x.JobType,
					requirements = x.Requirments

				})

				.FirstOrDefault(job => job.id == id);
			if (detailjob == null)
			{
				return NotFound("not record found");
			}
			return View(detailjob);

		}

		[Authorize]
		[HttpPost]
		public IActionResult CreateJob(CreateJobDto createJobDto)
		{
				Job newJob = new Job()
				{
					id = 0,
					Title = createJobDto.Title,
					CompanyName = createJobDto.CompanyName,
					Location = createJobDto.Location,
					Description = createJobDto.Description,
					Qualifications = createJobDto.Qualifications,
                    Salary = createJobDto.Salary,
					ApplyHelp = createJobDto.ApplyHelp,
					JobType = createJobDto.JobType.ToString(),
                    creationDate = DateTime.Now
                };
				_dbContext.Jobs.Add(newJob);

			Requirement requirement = new Requirement
			{
				//in future we will move to dynamic array
				Description = createJobDto.requirements[0].ToString(),
				job = newJob
			};
			_dbContext.JobRequirements.Add(requirement);

				_dbContext.SaveChanges();
				return Redirect("Index");
				//return View("Index");
		}

		[Authorize]
		[HttpGet]
		public IActionResult CreateJob()
		{
			
            return View();
        }


		[HttpGet]
		public IActionResult Edit(int id)
		{
			var job = _dbContext.Jobs
                .Include(job => job.Requirments)
                .Select(x => new EditJobDTO
                {
                    id = x.id,
                    Title = x.Title,
                    Description = x.Description,
                    Location = x.Location,
                    CompanyName = x.CompanyName,
                    Qualifications = x.Qualifications,
                    Salary = x.Salary,
                    ApplyHelp = x.ApplyHelp,

                    Requirements = x.Requirments

                })
                .FirstOrDefault(j=>j.id == id);
			if (job==null)
			{
				return NotFound();

            }
			
				//return edt view
				return View(job);
			}


		[HttpPost]
		public IActionResult Edit(EditJobDTO editJobDto)
		{
			var job = _dbContext.Jobs.FirstOrDefault(j => j.id == editJobDto.id);
			if (job==null)
			{
				return NotFound("no record found");
			}


			job.id = editJobDto.id;
            job.Title = editJobDto.Title;
            job.CompanyName = editJobDto.CompanyName;
            job.Location = editJobDto.Location;
            job.Description = editJobDto.Description;
            job.Qualifications = editJobDto.Qualifications;
            job.Salary = editJobDto.Salary;
            job.ApplyHelp = editJobDto.ApplyHelp;
			
			_dbContext.Jobs.Update(job);
			_dbContext.SaveChanges();
			return RedirectToAction("Details",editJobDto);
		}

	
		}

	
}
