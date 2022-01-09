using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Ensek.Models;
using Ensek.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic.FileIO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;

namespace Ensek.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MeterReadingController : ControllerBase
    {
        private readonly MeterReadingContext _context;
        private MeterReadingService meterReadingService = new MeterReadingService();
        
        private readonly ILogger<MeterReadingController> _logger;

        public MeterReadingController(MeterReadingContext context, ILogger<MeterReadingController> logger)
        {
            _context = context;
            _logger = logger;
        }

        

        // GET: api/Meter
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MeterReadingItem>>> GetTodoItems()
        {
            return await _context.MeterReadingItems.ToListAsync();
        }

        // TODO: Refactor to add logic to the services file as opposed to controller
        // Minimise how clunky this looks
        [HttpPost("meter-reading-uploads")]
        public async Task<IActionResult> OnPostUploadAsync(IFormFile formFile)
        {    
            var setOfItems = new HashSet<MeterReadingItem>();
            int success = 0;
            int total = 0;
            if (formFile.Length > 0)
            {
                var stream = formFile.OpenReadStream();
                    
                using (var parser = new TextFieldParser(stream))
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(",");

                    string[] line;
                    while (!parser.EndOfData)
                    {
                        total++; 
                        try
                        {
                            line = parser.ReadFields();
                            if (!long.TryParse(line[0], out var accountId)) continue;
                            var dateTime = line[1];
                            if (!long.TryParse(line[2], out var meterReadingValue)) continue;

                            if (!meterReadingService.CheckValidUser(accountId)) continue;

                            var meterReadingItem = new MeterReadingItem(accountId, dateTime, meterReadingValue);
                            setOfItems.Add(meterReadingItem);
                            _context.MeterReadingItems.Add(meterReadingItem);
                            await _context.SaveChangesAsync();
                            success++;

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            throw;
                        }
                    }
                }                                        
            }

            _logger.LogInformation("Final Set is " + string.Join("\n", setOfItems) + "\nLength of set is: " + setOfItems.Count);

            return Ok(new {success, failure = total - success - 1});
        }
    }
}
