using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ModelLibrary.Model;
using Newtonsoft.Json;

namespace RestConsumer
{
    class Worker
    {
        private const string URI = "http://localhost:54977/api/Hotels"; 

        public Worker()
        {
            
        }



        public void Start()
        {
            
            PrintAll();
            

            Console.WriteLine();

            Console.WriteLine("Hent hotel 8");
            Console.WriteLine($"Hotel ::{GetOne(8)}");

            //Console.WriteLine(Post(new Hotel(10, "Some Hotel", "Somewhere")));
            Console.WriteLine();

            //Console.WriteLine(Delete(10));
            Console.WriteLine();

            //Console.WriteLine(Put(10, new Hotel(10, "Some other Hotel", "Best Address")));

            Console.WriteLine();

            
            PrintAll();

        }

        private void PrintAll()
        {
            Console.WriteLine("Printer alle Hoteller");
            GetAll().ForEach(h => Console.WriteLine($"Hotel: {h}"));
            Console.WriteLine();
        }

        private List<Hotel> GetAll()
        {
            List<Hotel> Hotels = new List<Hotel>();

            using (HttpClient client = new HttpClient())
            {
                Task<string> task = client.GetStringAsync(URI);
                string JsonString = task.Result;

                Hotels = JsonConvert.DeserializeObject<List<Hotel>>(JsonString);
            }
            return Hotels;
        }

        private Hotel GetOne(int id)
        {
            Hotel hotel = new Hotel();

            using (HttpClient client = new HttpClient())
            {
                Task<string> task = client.GetStringAsync($"{URI}/{id}");
                string JsonString = task.Result;

                hotel = JsonConvert.DeserializeObject<Hotel>(JsonString);
            }

            return hotel;
        }

        private bool Delete(int id)
        {
            bool status;

            using (HttpClient client = new HttpClient())
            {
                Task < HttpResponseMessage > responseTask = client.DeleteAsync($"{URI}/{id}");

                HttpResponseMessage response = responseTask.Result;

                if (response.IsSuccessStatusCode)
                {
                    string jsonString = response.Content.ReadAsStringAsync().Result;
                    status = JsonConvert.DeserializeObject<bool>(jsonString);
                }
                else
                {
                    status = false;
                }

            }

            return status;
        }

        private bool Post(Hotel hotel)
        {
            bool status;

            using (HttpClient client = new HttpClient())
            {
                string jsonString = JsonConvert.SerializeObject(hotel);
                StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");

                Task<HttpResponseMessage> responseTask = client.PostAsync(URI, content);

                HttpResponseMessage response = responseTask.Result;

                if (response.IsSuccessStatusCode)
                {
                    string jsonStringRead = response.Content.ReadAsStringAsync().Result;
                    status = JsonConvert.DeserializeObject<bool>(jsonStringRead);
                }
                else
                {
                    status = false;
                }

            }

            return status;
        }

        private bool Put(int id, Hotel hotel)
        {
            bool status;

            using (HttpClient client = new HttpClient())
            {
                string jsonString = JsonConvert.SerializeObject(hotel);
                StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");

                Task<HttpResponseMessage> responseTask = client.PutAsync($"{URI}/{id}", content);

                HttpResponseMessage response = responseTask.Result;

                if (response.IsSuccessStatusCode)
                {
                    string jsonStringRead = response.Content.ReadAsStringAsync().Result;
                    status = JsonConvert.DeserializeObject<bool>(jsonStringRead);
                }
                else
                {
                    status = false;
                }

            }

            return status;
        }


    }
}
