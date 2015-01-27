using System.Collections.Generic;
using System.Data.Entity;
using System.Net;
using MeldingAppX.Models;
using System.Threading.Tasks;
using System.Web.Http;

namespace MeldingAppX.Api.Controllers
{
    public class NoticeController : ApiController
    {
        public NoticeController()
        {
            _db = new MeldingAppContext();
        }

        public async Task<Notice> Get(int id)
        {
            return await _db.Notices.FindAsync(id);
        }

        public async Task<IEnumerable<Notice>> Get()
        {
            var notices = await _db.Notices.ToListAsync();

            return notices;
        }

        public async Task<Notice> Post([FromBody] Notice notice)
        {
            _db.Notices.Add(notice);
            await _db.SaveChangesAsync();

            return notice;
        }

        public async Task<IHttpActionResult> Delete(int id)
        {
            var notice = await _db.Notices.FindAsync(id);

            _db.Entry(notice).State = EntityState.Deleted;

            await _db.SaveChangesAsync();

            return Ok(notice);
        }

        public async Task<IHttpActionResult> Put(int id, [FromBody] Notice notice)
        {
            var current = await _db.Notices.FindAsync(id);
            
            _db.Entry(current).CurrentValues.SetValues(notice);
            await _db.SaveChangesAsync();
            
            return StatusCode(HttpStatusCode.NoContent);
        }

        private MeldingAppContext _db;
    }

}