using QLGVFunction2.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLGVFunction2.DAO
{
    public class ReportCourseDAO
    {
        private static ReportCourseDAO instance;
        public static ReportCourseDAO Instance
        {
            get { if (instance == null) instance = new ReportCourseDAO(); return instance; }
            private set { instance = value; }
        }
        private ReportCourseDAO() { }
        public string GetUserid(string courseId)
        {
            string userId = "";
            string query = string.Format("select UserId from Course where courseId  = N'{0}'", courseId);
            DataTable table = DataProvider.Instance.ExecuteQuery(query);

            if (table.Rows.Count > 0)
            {
                DataRow dr = table.Rows[0];
                ReportCourse c = new ReportCourse(dr);
                userId = c.UserId;
            }

            return userId;
        }
        public bool CheckCourseId(string courseId)
        {
            string query = string.Format("select * from ReportCourse where courseId  = N'{0}'", courseId);
            DataTable result = DataProvider.Instance.ExecuteQuery(query);
            return result.Rows.Count > 0;
        }
        public void AddReportCourse(string userId, string courseId, string AbsentDate, string rescheduleDay)
        {
            string query = string.Format("exec addAbsent '{0}','{1}' ,'{2}','{3}' ", userId,courseId,AbsentDate,rescheduleDay);
            DataProvider.Instance.ExecuteNonQuery(query);
        }
    }
}
