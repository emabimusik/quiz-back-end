using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
namespace quiz.Model
{
    public class QuizContext : DbContext
    {
        public QuizContext(DbContextOptions<QuizContext> options) : base(options){}

        public DbSet<Questions> questions
        {
            get;
            set;
        }
        public DbSet<Quizzes> quizes
        {
            get;
            set;
        }
                                                         

    }
}
