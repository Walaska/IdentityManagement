using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityManagement
{
    class Identify
    {
        private string retString;
        private string[] strs;
        private string[] parameter;
        public Identify(string uri)
        {
            bool checker = true;
            strs = uri.Split(':', '?');
            string action = strs[1].Remove(0, 2);
            if (validate(strs[0], action) == false)
            {
                checker = false;
            }
            if (checker)
            {
                if(action == "login")
                {
                    returnAction(strs[2]);
                }
                else if(action == "confirm")
                {
                    parameter = strs[2].Split('=');
                    returnAction(parameter[1], parameter[2]);
                }
                else if(action == "sign")
                {
                    Guid x = Guid.Parse(parameter[3]);
                    parameter = strs[2].Split('=');
                    returnAction(parameter[1], x);
                }
            }
        }
        private bool validate(string sch, string action)
        {
            string source;
            if (sch == "visma-identity")
            {
                if (action == "login")
                {
                    source = strs[2];
                    if(source == "source=severa")
                    {
                        return true;
                    }
                    err();
                    return false;
                }
                else if (action == "confirm")
                {
                    parameter = strs[2].Split('&', '=');
                    source = parameter[0] + "=" + parameter[1];
                    if (source == "source=netvisor" && parameter[2] == "paymentnumber")
                    {
                        return true;
                    }
                    err();
                    return false;
                }
                else if (action == "sign")
                {
                    parameter = strs[2].Split('&', '=');
                    source = parameter[0] + "=" + parameter[1];
                    if (source == "source=vismasign" && parameter[2] == "documentid")
                    {
                        return true;
                    }
                    err();
                    return false;
                }
                else
                {
                    err();
                    return false;
                }
            }
            else
            {
                err();
                return false;
            }
        }
        private void returnAction(string str)  //login
        {
            retString = "login?" + str;
        }
        private void returnAction(string str, string payment)  //confirm
        {
            retString = "confirm?source=" + str + "=" + payment;
        }
        private void returnAction(string str, Guid UUID)  //sign
        {
            retString = "sign?source=" + str + "=" + UUID; 
        }
        public override string ToString()
        {
            return retString;
        }
        private void err()
        {
            retString = "Something went wrong!";
        }
    }
}
