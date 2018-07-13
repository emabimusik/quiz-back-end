using System;
namespace quiz.Model
{
    public class Quizzes
    {
        public Quizzes()
        {
        }
        public int Id {get;set;}
        public string Title { get; set; }
        public string OwnerId
        {
            get;
            set;
        }   


    }
}
