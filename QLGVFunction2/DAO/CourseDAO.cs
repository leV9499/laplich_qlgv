using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLGVFunction2.DTO;
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
            DataTable result = DataProvider.Instance.ExecuteQuery($"exec GetCalendar '{ValidateService.ConvertToDateFormat(date.ToString())}' , '{userId}' ");
            
            return result;
        }
        public bool CheckGetCalendar(string userId)
        {
            DataTable result = DataProvider.Instance.ExecuteQuery("select * from Course where UserId = @userId ", new object[] { userId });
            return result.Rows.Count > 0;
        }
        public DataTable GetAbsentCalendar(DateTime date, string userId)
        {
            DataTable result = DataProvider.Instance.ExecuteQuery("exec GetReportCourse @date , @userid ", new object[] { ValidateService.ConvertToDateFormat(date.ToString()), userId });
            return result;
        }


        public string GetUserId(string courseId)
        {
            string userId = "";
            string query = string.Format("select * from Course where courseId  = N'{0}'", courseId);
            DataTable table = DataProvider.Instance.ExecuteQuery(query);

            if (table.Rows.Count > 0)
            {
                DataRow dr = table.Rows[0];
                Course c = new Course(dr);
                userId = c.UserId;
            }

            return userId;
        }

     
        public bool CheckGetAbsentCalendar(string userId)
        {
            DataTable result = DataProvider.Instance.ExecuteQuery("select * from ReportCourse where userId = @userId",new object[] {userId});
            return result.Rows.Count > 0;
        }
        public void AddCourse(string courseId, string userId, string teachingDay, string startingTime, string location, DateTime calenderStart, DateTime calenderEnd)

       
        {
            DataProvider.Instance.ExecuteNonQuery($" exec addTeaching '{courseId}' , '{userId}' , '{teachingDay}' , '{startingTime}' , '{location}' , '{calenderStart}' ,  '{calenderEnd}' ", new object[] { });
        }



        public void AddTeaching(string courseId, string userId, string teachingDay, string startingTime, string location, DateTime calenderStart, DateTime calenderEnd, float money)

        {
            DataProvider.Instance.ExecuteNonQuery($" insert into course values ('{courseId}' , '{userId}' , '{teachingDay}' , '{startingTime}' , '{location}' , '{ValidateService.ConvertToDateFormat( calenderStart.ToString())}' ,  '{ValidateService.ConvertToDateFormat(calenderEnd.ToString())}' , {money})");
        }
        public bool CheckCourseId(string id)
        {
            string query = "select* from Course where courseId = @id";

            DataTable table = DataProvider.Instance.ExecuteQuery(query, new object[] { id });

            return table.Rows.Count > 0;
        }
        public DataTable ShowJob(string user)
        {
            string query = "select courseId as N'Mã lớp', Teachingday as N'Thứ',Location as N'Địa điểm', Price as N'Giá tiền' ,Startingtime as N'Thời gian bắt đầu dạy', calendarStart as N'Thời gian bắt đầu lớp', calendarEnd as N'Thời gian kết thúc lớp'from Course Where UserId= @user";
            DataTable table = DataProvider.Instance.ExecuteQuery(query, new object[] {user});

            return table;

        }
        public void DeleteJob(string id)
        {
            string query = "delete from Course where courseId= @id";

          
            DataProvider.Instance.ExecuteNonQuery(query, new object[] { id });
        }
        public void DeleteJobReport(string id)
        {
           
            string query1 = "delete from ReportCourse where courseId= @id";
            DataProvider.Instance.ExecuteNonQuery(query1, new object[] { id });
    
        }
        public void EditJob(string courseId, string userId, string teachingDay, string startingTime, string location, DateTime calenderStart, DateTime calenderEnd, float money)
        {
            string query = "exec UpdateCourse @courseId , @userId , @teachingDay , @startingTime , @location , @calenderStart , @calenderEnd , @money";
            DataProvider.Instance.ExecuteNonQuery(query, new object[] { courseId, userId,  teachingDay,  startingTime,  location,  calenderStart,  calenderEnd, money });
        }
    }
}
