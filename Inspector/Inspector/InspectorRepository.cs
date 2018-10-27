using System.Collections.Generic;

namespace WonderTools.Inspector
{
    public class InspectorRepository
    {
        private Dictionary<string,string> _dictionary = new Dictionary<string, string>();

        public Dictionary<string, string> GetDictionary()
        {
            return _dictionary;
        }

        public void AddKeyValue(string key, string value)
        {
            if (_dictionary.ContainsKey(key))
            {
                _dictionary[key] = value;
            }
            else
            {
                _dictionary.Add(key,value);
            }
        }
    }
}