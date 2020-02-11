using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace NUnitTestProject2
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            string Response;
            string LogPath = "LOG.txt";
            using (var file = File.Create(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LOG.txt")))
            { }

            String url = "https://www.metaweather.com/api/location/search/?query=min";
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
            {
                Response = streamReader.ReadToEnd();
            }
            using (StreamWriter sw = new StreamWriter(LogPath, false, System.Text.Encoding.Default))
            {
                sw.WriteLine("1 задание");
                sw.WriteLine(Response);
            }


            //////////////////////////////////////////

            url = "https://www.metaweather.com/api/location/search/?query=Minsk";
            httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
            {
                Response = streamReader.ReadToEnd();
            }
            using (StreamWriter sw = new StreamWriter(LogPath, true, System.Text.Encoding.Default))
            {
                sw.WriteLine("2 задание");
                sw.WriteLine(Response);
            }

            /////////////////////////////////

            string ll = "53.90000,27.566670";
            url = "https://www.metaweather.com/api/location/search/?query=Minsk";
            httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
            {
                Response = streamReader.ReadToEnd();
                Response = Response.Remove(0, 1);
                Response = Response.Remove(Response.Length - 1, 1);
            }
            
            Class @class = new Class();
            @class = JsonConvert.DeserializeObject<Class>(Response);
            using (StreamWriter sw = new StreamWriter(LogPath, true, System.Text.Encoding.Default))
            {
                sw.WriteLine("3 задание");
                if (@class.latt_long.Equals(ll) == true)
                { sw.WriteLine("—овпадение"); }
                else { sw.WriteLine("Ќе совпадает"); }
            }

            /////////////////////////////////////////////
            

            url = ("https://www.metaweather.com/api/location/"+ @class.woeid +"/"+ DateTime.Today.Year +"/" + DateTime.Today.Month + "/" + DateTime.Today.Day + "/");
            httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
            {
                Response = streamReader.ReadToEnd();   
            }
            
            using (StreamWriter sw = new StreamWriter(LogPath, true, System.Text.Encoding.Default))
            {
                sw.WriteLine("4 задание");
                sw.WriteLine(Response);
            }

            //////////////////////////

            url = "https://www.metaweather.com/api/location/" + @class.woeid + "/" + DateTime.Today.AddYears(-3).Year + "/" + DateTime.Today.Month + "/" + DateTime.Today.Day + "/";
            httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
            {
                Response = streamReader.ReadToEnd();
                Response = Response.Remove(0, 1);
                Response = Response.Remove(Response.Length - 1, 1);
            }

            List<WSN> listOld = new List<WSN>();
            listOld = JsonExtensions.FromDelimitedJson<WSN>(new StringReader(Response)).ToList();
            
            using (StreamWriter sw = new StreamWriter(LogPath, true, System.Text.Encoding.Default))
            {
                sw.WriteLine("5 задание");
                //foreach (WSN w in listOld)
                //{
                //    sw.WriteLine(w.weather_state_name);
                //    sw.WriteLine(w.created);
                //}
            }

            url = ("https://www.metaweather.com/api/location/" + @class.woeid + "/" + DateTime.Today.Year + "/" + DateTime.Today.Month + "/" + DateTime.Today.Day +"/");
            httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
            {
                Response = streamReader.ReadToEnd();
                Response = Response.Remove(0, 1);
                Response = Response.Remove(Response.Length - 1, 1);
            }
            List<WSN> listToday = new List<WSN>();
            listToday = JsonExtensions.FromDelimitedJson<WSN>(new StringReader(Response)).ToList();
            using (StreamWriter sw = new StreamWriter(LogPath, true, System.Text.Encoding.Default))
            {
                for (int j = 0; j < 8; j++)
                {
                    string timetoday = listToday[j].created.Split('T')[1];
                    timetoday = timetoday.Split(':')[0];
                    for (int i = 0; i < 8; i++)
                    {
                        string timeold = listOld[i].created.Split('T')[1];
                        timeold = timeold.Split(':')[0];
                        if (timeold.Equals(timetoday) == true)
                        {
                            if (listToday[j].weather_state_name.Equals(listOld[i].weather_state_name) == true)
                            {
                                sw.WriteLine(timetoday + " час");
                                sw.WriteLine(listToday[j].weather_state_name);
                            }
                        }
                    }
                }
            }

            ////////////////////////////////////

            url = "https://www.metaweather.com/api/location/" + @class.woeid + "/2019/01/15/";
            httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
            {
                Response = streamReader.ReadToEnd();
                Response = Response.Remove(0, 1);
                Response = Response.Remove(Response.Length - 1, 1);
            }

            List<WSN> tempwinter = new List<WSN>();
            tempwinter = JsonExtensions.FromDelimitedJson<WSN>(new StringReader(Response)).ToList();
            using (StreamWriter sw = new StreamWriter(LogPath, true, System.Text.Encoding.Default))
            {
                sw.WriteLine("6 задание");
                float avgtemp = (tempwinter[0].max_temp + tempwinter[0].min_temp) / 2;
                if (avgtemp<0)
                { sw.WriteLine(" In winter 2019 average temp < 0"); }
                else { sw.WriteLine(" In winter 2019 average temp > 0"); }
                
            }


            url = "https://www.metaweather.com/api/location/" + @class.woeid + "/2019/07/15/";
            httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
            {
                Response = streamReader.ReadToEnd();
                Response = Response.Remove(0, 1);
                Response = Response.Remove(Response.Length - 1, 1);
            }

            List<WSN> tempsummer = new List<WSN>();
            tempsummer = JsonExtensions.FromDelimitedJson<WSN>(new StringReader(Response)).ToList();
            using (StreamWriter sw = new StreamWriter(LogPath, true, System.Text.Encoding.Default))
            {
                float avgtemp = (tempsummer[0].max_temp + tempsummer[0].min_temp) / 2;
                if (avgtemp < 0)
                { sw.WriteLine(" In summer 2019 average temp < 0"); }
                else { sw.WriteLine(" In summer 2019 average temp > 0"); }
            }


            url = "https://www.metaweather.com/api/location/" + @class.woeid + "/2019/04/15/";
            httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
            {
                Response = streamReader.ReadToEnd();
                Response = Response.Remove(0, 1);
                Response = Response.Remove(Response.Length - 1, 1);
            }

            List<WSN> tempspring = new List<WSN>();
            tempspring = JsonExtensions.FromDelimitedJson<WSN>(new StringReader(Response)).ToList();
            using (StreamWriter sw = new StreamWriter(LogPath, true, System.Text.Encoding.Default))
            {
                float avgtemp = (tempspring[0].max_temp + tempspring[0].min_temp) / 2;
                if (avgtemp < 8)
                { sw.WriteLine(" In spring 2019 average temp < 8"); }
                else { if (avgtemp > 3) { sw.WriteLine(" In spring 2019 average temp > 3"); } }
            }


            url = "https://www.metaweather.com/api/location/" + @class.woeid + "/2019/10/15/";
            httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
            {
                Response = streamReader.ReadToEnd();
                Response = Response.Remove(0, 1);
                Response = Response.Remove(Response.Length - 1, 1);
            }

            List<WSN> tempautumn = new List<WSN>();
            tempautumn = JsonExtensions.FromDelimitedJson<WSN>(new StringReader(Response)).ToList();
            using (StreamWriter sw = new StreamWriter(LogPath, true, System.Text.Encoding.Default))
            {
                float avgtemp = (tempautumn[0].max_temp + tempautumn[0].min_temp) / 2;
                if (avgtemp < 8)
                { sw.WriteLine(" In autumn 2019 average temp < 8"); }
                else { if (avgtemp > 3) { sw.WriteLine(" In autumn 2019 average temp > 3"); } }
            }
            Assert.Pass();
        }
    }
}