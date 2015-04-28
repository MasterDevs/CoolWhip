using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MasterDevs.CoolWhip.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Assembly.GetExecutingAssembly().GetName().Version);
        }
    }
}
