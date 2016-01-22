using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace WebApplication1
{
    public partial class Blog : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Classes.Blog b = new Classes.Blog();
            DataTable dt = b.blogEntries();

            RepeaterBlog.DataSource = dt;
            RepeaterBlog.DataBind();
        }

        //protected string FormatPassedDate(string Date)
        //{
        //    string passedDate = Date.ToString();
        //    TimeSpan span = DateTime.Now.Subtract(Convert.ToDateTime(Date));

        //    if (span.TotalSeconds < 0)
        //    {
        //        passedDate = "0 seconds ago ";
        //    }

        //    if (span.TotalSeconds < 60 && span.TotalSeconds > 0)
        //    {
        //        passedDate = Convert.ToInt16(span.TotalSeconds).ToString() + " seconds ago ";
        //    }
        //    else if (span.TotalSeconds > 60 && span.TotalSeconds < 3600)
        //    {
        //        passedDate = Convert.ToInt16(span.TotalMinutes).ToString() + " minutes ago ";
        //    }
        //    else if (span.TotalSeconds > 3600 && span.TotalSeconds < 86400)
        //    {
        //        passedDate = Convert.ToInt16(span.TotalHours).ToString() + " hours ago ";
        //    }
        //    else if (span.TotalSeconds > 86400 && span.TotalSeconds < 604800)
        //    {
        //        passedDate = Convert.ToInt16(span.TotalDays).ToString() + " days ago ";
        //    }
        //    else if (span.TotalSeconds > 604800)
        //    {
        //        passedDate = Convert.ToDateTime(Date).Date.ToShortDateString();
        //    }

        //    return passedDate;
        //}
    }
}