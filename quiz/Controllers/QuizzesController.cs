using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using quiz.Model;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace quiz.Controllers
{
    [Produces("application/json")]
    [Route("api/Quizzes")]
    public class QuizzesController : Controller
    {
        readonly QuizContext _context;

        public QuizzesController(QuizContext context){
            this._context = context;
        }
        // GET: api/quizzes
        [Authorize]
        [HttpGet]
        public IEnumerable<Quizzes> Get()
        {
             var userId = HttpContext.User.Claims.First().Value;
             return _context.quizes.Where(q => q.OwnerId == userId);
           // return _context.quizes;
        }
        //[Authorize]
        [HttpGet("all")]
        public IEnumerable<Quizzes> GetAllQuizzes()
        {
           return _context.quizes;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/quizzes
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Quizzes quiz)
        {
           
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
             var userId = HttpContext.User.Claims.First().Value;
             quiz.OwnerId = userId;
             _context.Add(quiz);
            await _context.SaveChangesAsync();
           // return CreatedAtAction("Get", new { id = quiz.Id }, quiz);
            return Ok(quiz);
        }

        // PUT api/quiz/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Quizzes quiz)
        {
            if(!ModelState.IsValid){
                return BadRequest();
            }
            _context.Entry(quiz).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(quiz);
           

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
