﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;

namespace Nkgjjm.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistBlocksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DistBlocksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/DistBlocks
        
        public List<DistBlock> BlockList { get; set; }

       
        public async Task<ActionResult<DistBlock>> GetDistBlock(int id)
        {
          
            BlockList = await _context.TblBlock.Where(Did=>Did.District==id).ToListAsync();
            return Ok(BlockList);
        }

       
    }
}
