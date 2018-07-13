using System;
namespace quiz.Model
{
    public class Questions
    {
        public Questions()
        {
        }
        public int Id {get;set;}
        public string Text { get; set; }
        public string CorrectAnswer{get; set; }
        public string Answer1 { get; set; }
        public string Answer2 { get; set; }
        public string Answer3 { get; set; }
        public int quizId
        {
            get;
            set;
        }

    }
}
