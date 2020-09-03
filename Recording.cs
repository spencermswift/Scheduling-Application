using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Customer_Scheduling_Application
{
    class Recording
    {
        private static DateTime? time;
        public static void setTime(DateTime? Time)
        {
            time = Time;
        }
        public static DateTime? getTime()
        {
            return time;
        }
        public static void reminder()
        {
            try
            {
                var list = Database.getNextAppointment();
                IDictionary<string, object> dictionary = list.ToDictionary(pair => pair.Key, pair => pair.Value);
                DateTime? currentTime = getTime();
                string type = dictionary["type"].ToString();
                DateTime? nextAppointment = Convert.ToDateTime(dictionary["start"]);
                string name = dictionary["name"].ToString();
                if (currentTime != null && nextAppointment != null)
                {
                    DateTime dateTime = currentTime.Value;
                    DateTime dateTime2 = nextAppointment.Value;
                    string dateString = nextAppointment.Value.ToString("h:mm tt");
                    TimeSpan difference = dateTime2.Subtract(dateTime);
                    if(difference.Minutes < 15)
                    {
                        MessageBox.Show("Reminder: You have a " + type + " appointment at " + dateString + " with " + name + "!");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        

        public static void logIn(string userName)
        {
            DateTime time = DateTime.Now.ToLocalTime();
            Dictionary<DateTime, string> dictionary = new Dictionary<DateTime, string>();
            dictionary.Add(time, userName);
            setTime(time);

            foreach (KeyValuePair<DateTime, string> keyValue in dictionary)
            {
                string record = string.Format("Login time = {0}, userName = {1}", keyValue.Key, keyValue.Value);
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(record + Environment.NewLine);
                File.AppendAllText(Application.StartupPath + "_access_records.txt", stringBuilder.ToString());
                stringBuilder.Clear();
            }
        }
        public static void logOut(string userName)
        {
            Dictionary<DateTime, string> dictionary = new Dictionary<DateTime, string>();
            dictionary.Add(DateTime.Now, userName);
            

            foreach (KeyValuePair<DateTime, string> keyValue in dictionary)
            {
                string record = string.Format("Logout time = {0}, userName = {1}", keyValue.Key, keyValue.Value);
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(record + Environment.NewLine);
                File.AppendAllText(Application.StartupPath + "_access_records.txt", stringBuilder.ToString());
                stringBuilder.Clear();
            }
        }
    }
}
