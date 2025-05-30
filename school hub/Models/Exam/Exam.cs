﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace school_hub.Models
{
    public class Exam
    {
        public short ExamId { get; set; }
        public short LessonId { get; set; }
        public byte PassingScore { get; set; }
        public byte ExamTime { get; set; }//minutes

        //public byte CountOfQuestion { get; set; }
        public Lesson Lesson { get; set; }
        public ICollection<Question> Questions { get; set; }
        public ICollection<StudentExam> StudentExams { get; set; }
    }

}
