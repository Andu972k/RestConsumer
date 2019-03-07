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
    class FacilitetsWorker
    {

        private const string URI = "http://localhost:54977/api/Facilitets";

        public FacilitetsWorker()
        {
            PrintAll();

            PrintOne(6);

            //Console.WriteLine(Post(new Facilitet("Basket Ball", 50)));

            PrintAll();

            //Put(6, new Facilitet("Internet", 30));

            PrintAll();

            //Console.WriteLine(Delete(12));


        }


        public void Start()
        {
            PrintAll();
        }

        private void PrintAll()
        {
            Console.WriteLine("Printer nu alle faciliteter");
            GetAll().ForEach(f => Console.WriteLine($"Facilitet: {f}"));
            Console.WriteLine();
        }

        private void PrintOne(int id)
        {
            Console.WriteLine($"Henter nu hotel nummer {id}");
            GetOne(id);
            Console.WriteLine();

        }

        private List<Facilitet> GetAll()
        {
            List<Facilitet> facilitets = new List<Facilitet>();

            using (HttpClient client = new HttpClient())
            {
                Task<string> task = client.GetStringAsync(URI);
                string jsonString = task.Result;

                facilitets = JsonConvert.DeserializeObject<List<Facilitet>>(jsonString);

            }

            return facilitets;
        }

        private Facilitet GetOne(int id)
        {
            Facilitet facilitet = new Facilitet();

            using (HttpClient client = new HttpClient())
            {
                Task<string> task = client.GetStringAsync($"{URI}/{id}");
                string jsonString = task.Result;

                facilitet = JsonConvert.DeserializeObject<Facilitet>(jsonString);
            }

            return facilitet;
        }

        private bool Post(Facilitet facilitet)
        {
            bool status;

            using (HttpClient client = new HttpClient())
            {
                string jsonString = JsonConvert.SerializeObject(facilitet);
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

        private bool Put(int id, Facilitet facilitet)
        {
            bool status;

            using (HttpClient client = new HttpClient())
            {
                string jsonString = JsonConvert.SerializeObject(facilitet);
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

        private bool Delete(int id)
        {
            bool status;

            using (HttpClient client = new HttpClient())
            {
                Task<HttpResponseMessage> responseTask = client.DeleteAsync($"{URI}/{id}");

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
