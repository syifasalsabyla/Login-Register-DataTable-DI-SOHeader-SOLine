using latihanLogSig9.Data;
using latihanLogSig9.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace latihanLogSig9.Controllers.api
{
    [Route("api/GetData")]
    [ApiController]
    public class GetDataController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public GetDataController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/SOLine/5
        [HttpGet("{id}")]
        public IEnumerable<SOLine> GetSOLine([FromRoute] int id)
        {
            var sOLine = _context.SOLine
                .Include(x => x.Produk)
                .Include(x => x.SOHeader) //ini yang ditambah
                .Where(x => x.SOHeaderID.Equals(id)).ToList();

            return sOLine;
        }
    }
}
