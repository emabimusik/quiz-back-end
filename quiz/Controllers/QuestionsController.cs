using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using quiz.Model;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace quiz.Controllers
{   [Produces("application/json")]
    [Route("api/Questions")]
    public class QuestionsController : Controller
    {
          readonly QuizContext _context;

        public QuestionsController(QuizContext context){
            this._context = context;
        }


        // GET: Questions/values
        [HttpGet]
        public IEnumerable<Model.Questions> Get()
        {
            return _context.questions.ToList();
        }

        [HttpGet("{quizId}")]
        public IEnumerable<Model.Questions> Get([FromRoute] int quizId)
        {
            return _context.questions.Where(q => q.quizId == quizId);
        }


        // POST api/questions
        [HttpPost]
        public  async Task<ActionResult> Post([FromBody] Model.Questions question)

        {
            var quiz = _context.quizes.SingleOrDefault(q => q.Id == question.quizId);
            /*
            var quiz = _context.quizes.SingleOrDefault(q => q.Id == question.quizId);
            if(quiz==null){
                return NotFound();  
            }
            if(! ModelState.IsValid){
                return BadRequest();
            }
            */
            if(quiz == null){
                return NotFound();
            }
            _context.Add(question);
            await _context.SaveChangesAsync();
            return Ok(question);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody]Questions question )
        {
            if(id != question.Id){
                return BadRequest();
            }
            _context.Entry(question).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(question);

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
