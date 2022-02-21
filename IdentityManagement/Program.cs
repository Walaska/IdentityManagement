using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            Identify id = new Identify("visma-identity://sign?source=vismasign&documentid=47ed9186-2ba0-4e8b-b9e2-7123575fdd5b");
            Console.WriteLine(id.ToString());
            Console.ReadLine();
        }
    }
}
