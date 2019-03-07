using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            Worker worker = new Worker();
            FacilitetsWorker fWorker = new FacilitetsWorker();
            //worker.Start();
            Console.WriteLine();
            Console.WriteLine("Her Starter Faciliteter");
            fWorker.Start();


            Console.ReadLine();
        }
    }
}
