using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLGVFunction2.DTO
{
    public  class ReportCourse
    {
        string userId;
        string courseId;
        DateTime AbsentDate;
        DateTime RescheduleDate;
        public ReportCourse(string userId, string courseId, DateTime absentDate, DateTime rescheduleDate)
        {
            this.userId = userId;
            this.courseId = courseId;
            this.AbsentDate = absentDate;
            this.RescheduleDate = rescheduleDate;

        }
        public ReportCourse(DataRow row)
        {
            this.userId = (string)row["userId"];
            this.courseId = (string)row["courseId"];
            this.AbsentDate = (DateTime)row["AbsentDate"];
            this.RescheduleDate = (DateTime)row["rescheduleDate"];
        }

        public string UserId { get => userId; set => userId = value; }
        public string CourseId { get => courseId; set => courseId = value; }
        public DateTime AbsentDate1 { get => AbsentDate; set => AbsentDate = value; }
        public DateTime RescheduleDate1 { get => RescheduleDate; set => RescheduleDate = value; }
    }
}
