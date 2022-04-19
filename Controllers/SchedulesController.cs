using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Schedulling.Interfaces;
using Schedulling.Modal;
using Schedulling.Modal.Database_Modal;
using Schedulling.MyData;

namespace Schedulling.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchedulesController : ControllerBase
    {
        private readonly DatabaseContexts _context;
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IRecurringJobManager _recurringJobManager;
        private readonly IJobTestService _jobTestService;

        public SchedulesController(DatabaseContexts context, IBackgroundJobClient backgroundJobClient, IJobTestService jobTestService, IRecurringJobManager recurringJobManager)
        {
            _context = context;
            _recurringJobManager = recurringJobManager;
            _jobTestService = jobTestService;
            _backgroundJobClient = backgroundJobClient;
        }

        // GET: api/Schedules
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Schedules>>> GetSchedules()
        {
            return await _context.Schedules.ToListAsync();
        }

        // GET: api/Schedules/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Schedules>> GetSchedules(int id)
        {
            var schedules = await _context.Schedules.FindAsync(id);

            if (schedules == null)
            {
                return NotFound();
            }

            return schedules;
        }

        // PUT: api/Schedules/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSchedules(int id, Schedules schedules)
        {
            if (id != schedules.Id)
            {
                return BadRequest();
            }

            _context.Entry(schedules).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SchedulesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        //Post: api/Schedule/Monthly
        [HttpPost("yearly")]
        public async Task<ActionResult<Schedules>> ScheduleYearly(RequestModal request)
        {
            Schedules schedules = new();
            try
            {
                schedules.Message = request.Message;
                schedules.Label = request.Label;
                schedules.Date = DateTime.Now;
                _context.Schedules.Add(schedules);
                await _context.SaveChangesAsync();

                foreach (string item in request.Phones)
                {
                    Phones phones = new()
                    {
                        Phone = item
                    };
                    var value = _context.Schedules.Find(schedules.Id);
                    _context.Phones.Add(phones);
                    value.Phones.Add(phones);
                    _context.Schedules.Update(value);
                    _context.SaveChanges();
                }
                _recurringJobManager.AddOrUpdate(schedules.Id + "", () => new JobTestServices(_context).ReccuringJob(schedules.Id), Cron.Yearly(request.Month,request.Day,request.Hour,request.minutes));
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //Post: api/Schedule/Monthly
        [HttpPost("monthly")]
        public async Task<ActionResult<Schedules>> ScheduleMonthly(RequestModal request)
        {
            Schedules schedules = new();
            try
            {
                schedules.Message = request.Message;
                schedules.Label = request.Label;
                schedules.Date = DateTime.Now;
                _context.Schedules.Add(schedules);
                await _context.SaveChangesAsync();

                foreach (string item in request.Phones)
                {
                    Phones phones = new()
                    {
                        Phone = item
                    };
                    var value = _context.Schedules.Find(schedules.Id);
                    _context.Phones.Add(phones);
                    value.Phones.Add(phones);
                    _context.Schedules.Update(value);
                    _context.SaveChanges();
                }
                _recurringJobManager.AddOrUpdate(schedules.Id + "", () => new JobTestServices(_context).ReccuringJob(schedules.Id), Cron.Monthly(request.Day,request.Hour,request.minutes));
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //POST: api/Schedules/daily
        [HttpPost("daily")]
        public async Task<ActionResult<Schedules>> ScheduleDaily(RequestModal request)
        {
            Schedules schedules = new();
            try
            {
                schedules.Message = request.Message;
                schedules.Label = request.Label;
                schedules.Date = DateTime.Now;
                _context.Schedules.Add(schedules);
                await _context.SaveChangesAsync();

                foreach (string item in request.Phones)
                {
                    Phones phones = new()
                    {
                        Phone = item
                    };
                    var value = _context.Schedules.Find(schedules.Id);
                    _context.Phones.Add(phones);
                    value.Phones.Add(phones);
                    _context.Schedules.Update(value);
                    _context.SaveChanges();
                }
                _recurringJobManager.AddOrUpdate(schedules.Id+"",() => new JobTestServices(_context).ReccuringJob(schedules.Id), Cron.Daily(request.Hour,request.minutes));
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // POST: api/Schedules
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("once")]
        public async Task<ActionResult<Schedules>> PostScheduleOnce(RequestModal request)
        {
            Schedules schedules = new();
            try
            {
                TimeSpan span = new TimeSpan(days: request.Day, hours: request.Hour, minutes: request.minutes, seconds: 0);

                schedules.Message = request.Message;
                schedules.Label = request.Label;
                schedules.Date = DateTime.Now;
                _context.Schedules.Add(schedules);
                await _context.SaveChangesAsync();

                foreach (string item in request.Phones)
                {
                    Phones phones = new()
                    {
                        Phone = item
                    };
                   var value =  _context.Schedules.Find(schedules.Id);
                    _context.Phones.Add(phones);
                    value.Phones.Add(phones);
                    _context.Schedules.Update(value);
                    _context.SaveChanges();
                }
                var job = BackgroundJob.Schedule(() => new JobTestServices(_context).ReccuringJob(schedules.Id), span);
                return Ok(job);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Schedules/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchedules(int id)
        {
            var schedules = await _context.Schedules.FindAsync(id);
            if (schedules == null)
            {
                return NotFound();
            }

            _context.Schedules.Remove(schedules);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool SchedulesExists(int id)
        {
            return _context.Schedules.Any(e => e.Id == id);
        }
    }
}
