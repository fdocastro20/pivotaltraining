using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PalTracker
{
    [Route("time-entries")]
    public class TimeEntryController : ControllerBase
    {
        private ITimeEntryRepository _inMemoryTimeEntryRepository;

        public TimeEntryController(ITimeEntryRepository inMemoryTimeEntryRepository)
        {
                _inMemoryTimeEntryRepository = inMemoryTimeEntryRepository;
        }

        [HttpGet("{id}", Name= "GetTimeEntry")]
        public IActionResult Read(long id)
        {
            TimeEntry timeEntry;

           if (_inMemoryTimeEntryRepository.Contains(id))
           {
               timeEntry=_inMemoryTimeEntryRepository.Find(id);
               
               return Ok(timeEntry);
           }
           else
           {
               return NotFound();
           }
        }

        [HttpGet]
        public IActionResult List()
        { 
            IEnumerable<TimeEntry> listTimeEntries;
            listTimeEntries = _inMemoryTimeEntryRepository.List();
           /*if (listTimeEntries == null || !listTimeEntries.GetEnumerator().MoveNext())
            {
                return NotFound();
            }*/

            return Ok(listTimeEntries);
        }

        [HttpPost]
        public IActionResult Create([FromBody] TimeEntry timeEntry)
        {
            TimeEntry timeEntryCreated;
            
            /*if (timeEntry.ProjectId == 0 || timeEntry.UserId == 0 || timeEntry.Date == null || timeEntry.Hours == 0)
            //if (!ModelState.IsValid)
            {
                return BadRequest();
            }*/

            timeEntryCreated = _inMemoryTimeEntryRepository.Create(timeEntry);
            return CreatedAtRoute("GetTimeEntry", new {id = timeEntryCreated.Id}, timeEntryCreated);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] TimeEntry timeEntry)
        {
            TimeEntry timeEntryUpdated;
            if (!_inMemoryTimeEntryRepository.Contains(id))
                return NotFound();

            timeEntryUpdated = _inMemoryTimeEntryRepository.Update(id, timeEntry);
            return new OkObjectResult(timeEntryUpdated);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            if (!_inMemoryTimeEntryRepository.Contains(id))
                return NotFound();

            _inMemoryTimeEntryRepository.Delete(id);
            return NoContent();
        }

    }
}


    