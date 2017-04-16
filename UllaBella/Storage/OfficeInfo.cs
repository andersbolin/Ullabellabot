using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UllaBella.Storage
{
    [Serializable]
    public class OfficeInfo
    {
        private Dictionary<string, string> information = new Dictionary<string, string>();

        public bool addInfo(string key, string value)
        {
            information.Add(key,value);

            bool test = false;
            test = information.ContainsKey(key);
            if (test)
            {
                test = information.ContainsValue(value);
            }
            return test;
        }

        public string getInfo(string key)
        {
            string value = "";
            if (information.TryGetValue(key, out value))
            {
                return value;
            }
            else
            {
                return "I have not been informed of this.";
            } 
        }

        private void removeInfo(string key)
        {
            information.Remove(key);
        }
    }
}