using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace JSON_Test
{
    class Scraper
    {
        public static void ScrapeXIVHuntAPI()
        {
            string urlAddress = "https://xivhunt.net/api/worlds";            
           
            try
            {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddress);
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        using (Stream receiveStream = response.GetResponseStream())
                        {
                            StreamReader readStream = null;
                            if (response.CharacterSet == null)
                                readStream = new StreamReader(receiveStream);
                            else
                                readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));

                            using (readStream)
                            {
                                while (!readStream.EndOfStream)
                                {
                                    // Pulls the text of the page and puts it into currentData
                                    string currentData = readStream.ReadLine();

                                    // This returns null and throws an exception.
                                    var worlds = JsonConvert.DeserializeObject<Rootobject>(currentData);

                                }
                            }
                        }
                    }
                    response.Close();

                   
                }
            }
            catch (Exception e)
            {
                File.AppendAllText("ErrorInfo.txt", $"{DateTime.Now} - {e.Message}\r\n");
                Console.WriteLine($"{DateTime.Now} - {e.Message}");
            }
        }
    }

    public class Rootobject
    {
        public Class1[] Property1 { get; set; }
    }

    public class Class1
    {
        public int id { get; set; }
        public Hunt[] hunts { get; set; }
    }

    public class Hunt
    {
        public int wId { get; set; }
        public int id { get; set; }
        public int r { get; set; }
        public DateTime lastReported { get; set; }
        public int instance { get; set; }
        public float x { get; set; }
        public float y { get; set; }
        public bool lastAlive { get; set; }
    }
}
