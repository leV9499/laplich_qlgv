using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLGVFunction2.Service;
namespace QLGVFunction2.DAO
{
    public class CourseDAO
    {
        private static CourseDAO instance;
        public static CourseDAO Instance
        {
            get
            {
                if (instance == null) instance = new CourseDAO();
                return instance;
            }
        }
        public DataTable GetCalendar(DateTime date, string userId)
        {
            //DataTable result = DataProvider.Instance.ExecuteQuery("exec GetCalendar @dateinput , @userId ", new object[] { date,userId });
            DataTable result = DataProvider.Instance.ExecuteQuery($"exec GetCalendar '{ValidateService.ConvertToDateFormat(date.ToString())}' , {userId} ");
            return result;
        }
        public DataTable GetAbsentCalendar(DateTime date, string userId)
        {
            DataTable result = DataProvider.Instance.ExecuteQuery("exec GetReportCourse @date , @userid ",new object[] { ValidateService.ConvertToDateFormat(date.ToString()),userId});
            return result;
        }
        public void AddCourse(string courseId, string userId, string teachingDay, string startingTime, string location, string calenderStart, string calenderEnd)
       
        {
            DataProvider.Instance.ExecuteNonQuery($" exec addTeaching '{courseId}' , '{userId}' , '{teachingDay}' , '{startingTime}' , '{location}' , '{calenderStart}' ,  '{calenderEnd}' ", new object[] { });
        }
        public bool CheckCourseId(string id)
        {
            string query = "select* from Course where courseId= @id";

            DataTable table = DataProvider.Instance.ExecuteQuery(query, new object[] { id });

            return table.Rows.Count > 0;
        }
        public DataTable ShowJob()
        {
            string query = "select courseId as N'Mã lớp', Teachingday as N'Thứ',Location as N'Địa điểm', Startingtime as N'Thời gian bắt đầu dạy', calendarStart as N'Thời gian bắt đầu lớp', calendarEnd as N'Thời gian bắt đầu lớp'\r\nfrom Course";
            DataTable table = DataProvider.Instance.ExecuteQuery(query);
            return table;
        }
    }
}
