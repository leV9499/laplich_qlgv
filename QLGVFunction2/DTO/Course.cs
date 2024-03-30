using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLGVFunction2.DTO
{
    public class Course
    {
        string courseId;
        string userId;
        string Teachingday;
        DateTime? startingTime;
        string Location;
        DateTime? calendarStart;
        DateTime? calendarEnd;


        public Course(DataRow row)
        {
            this.courseId = (string)row["courseId"];
            this.userId = (string)row["userId"];
            this.Teachingday = (string)row["Teachingday"];
            var starttime = row["Startingtime"];
            if (startingTime.ToString() != "")
            {
                this.StartingTime = (DateTime?)row["Startingtime"];
            }
            this.CalendarStart = (DateTime?)row["calendarStart"];
            this.Location = (string)row["Location"];
            this.CalendarEnd = (DateTime?)row["calendarEnd"];
        }

        public string CourseId { get => courseId; set => courseId = value; }
        public string UserId { get => userId; set => userId = value; }
        public string Teachingday1 { get => Teachingday; set => Teachingday = value; }
      
        public string Location1 { get => Location; set => Location = value; }
        public DateTime? StartingTime { get => startingTime; set => startingTime = value; }
        public DateTime? CalendarStart { get => calendarStart; set => calendarStart = value; }
        public DateTime? CalendarEnd { get => calendarEnd; set => calendarEnd = value; }
    }
}
