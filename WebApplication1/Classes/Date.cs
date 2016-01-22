using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Classes
{
    public class Date
    {
        public string FormatPassedDate(string Date)
        {
            string passedDate = Date.ToString();
            TimeSpan span = DateTime.Now.Subtract(Convert.ToDateTime(Date));

            if (span.TotalSeconds < 0)
            {
                passedDate = "0 seconds ago ";
            }

            if (span.TotalSeconds < 60 && span.TotalSeconds > 0)
            {
                passedDate = Convert.ToInt16(span.TotalSeconds).ToString() + " seconds ago ";
            }

            if (span.TotalSeconds > 60 && span.TotalSeconds < 3600)
            {
                passedDate = Convert.ToInt16(span.TotalMinutes).ToString() + " minutes ago ";
            }

            if (span.TotalSeconds > 3600 && span.TotalSeconds < 86400)
            {
                passedDate = Convert.ToInt16(span.TotalHours).ToString() + " hours ago ";
            }

            if (span.TotalSeconds > 86400 && span.TotalSeconds < 604800)
            {
                passedDate = Convert.ToInt16(span.TotalDays).ToString() + " days ago ";
            }

            return passedDate;
        }

        public string FormatRemainedDate(string Date)
        {
            string remainedTime = Date.ToString();
            TimeSpan span = Convert.ToDateTime(Date).Subtract(DateTime.Now);

            if (span.TotalHours < 24)
            {
                remainedTime = "in " + Convert.ToInt16(span.TotalHours).ToString() + " hours";
            }
            else
            {
                remainedTime = "in " + Convert.ToInt16(span.TotalDays).ToString() + " days";
            }

            return remainedTime;
        }
    }
}