﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DateApp.API.Data;
using DateApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DateApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public DataContext Context { get; }

        public ValuesController(DataContext context)
        {
            Context = context;
        }
        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var values = await Context.Values.ToListAsync();
            return Ok(values);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var value = await Context.Values.FirstOrDefaultAsync(w=> w.ID == id);
            return Ok(value);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
