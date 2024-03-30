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
            DataTable result = DataProvider.Instance.ExecuteQuery($"exec GetCalendar '{ValidateService.ConvertToDateFormat(date.ToString())}' , {userId} ");
            return result;
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
        public bool CheckCourseId(string courseId)
        {
            string query = string.Format("select * from Course where courseId  = N'{0}'", courseId);
            DataTable result = DataProvider.Instance.ExecuteQuery(query);
            return result.Rows.Count > 0;

        }
    }
}
