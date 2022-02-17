using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiController : Controller
    {

        private readonly sdgContext _SdgContext;
        public ApiController(sdgContext sdgContext) {
            _SdgContext = sdgContext; 
        }

        [HttpGet]
        [Route("v1/data/")]
        public IActionResult GetRegionList(string region)
        {
            return Ok(_SdgContext.ApiTable.ToList());
        }


        [HttpGet]
        [Route("v1/data/{region}")]
        public IActionResult GetRegion(string region)
        {
            return Ok(_SdgContext.ApiTable.Where(r => r.Region == region).FirstOrDefault());
        }

        [HttpGet]
        [Route("v1/data/{region}/m")]
        public IActionResult GetM(string region)
        {
            return Ok(_SdgContext.ApiTable.Where(r => r.Region == region).Select(r=>r.M).FirstOrDefault());

        }

        [HttpGet]
        [Route("v1/data/{region}/f")]
        public IActionResult GetF(string region)
        {
            return Ok(_SdgContext.ApiTable.Where(r => r.Region == region).Select(r => r.F).FirstOrDefault());

        }

        [HttpGet]
        [Route("v1/data/{region}/t")]
        public IActionResult GetT(string region)
        {
            return Ok(_SdgContext.ApiTable.Where(r => r.Region == region).Select(r => r.T).FirstOrDefault());

        }


        [HttpPost]
        [Route("v1/data/{region}")]
        public async Task<IActionResult> InsertRegion()
        {
            var address = "https://api.idescat.cat/pob/v1/geo.json?tipus=com&lang=es";
            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(address);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();

            var regionValues = JsonConvert.DeserializeObject<List<ApiTable>>(result);
            

            foreach (ApiTable member in regionValues) 
            {
                await _SdgContext.AddAsync(member);
            }

            return Ok("created");
        }


        //private async JObject GetResponse(string region)
        //{
        //    var address = "https://api.idescat.cat/pob/v1/geo.json?q=" + region;
        //    var client = new HttpClient();
        //    HttpResponseMessage response = await client.GetAsync(address);
        //    response.EnsureSuccessStatusCode();
        //    var result = await response.Content.ReadAsStringAsync();
        //    JObject o = JObject.Parse(result);
        //    return o;
        //}
    }
}
