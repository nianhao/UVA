using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UVA
{
    class Command
    {
        public static string READY_COMMAND(string ip, string port)
        {
           string command = string.Format( "ready;{0};{1}",ip,port);
            return command;
        }
    }
}
