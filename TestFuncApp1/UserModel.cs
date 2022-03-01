using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure;

namespace TestFuncApp1
{
    public class UserModel
    {
        public Dictionary<string, object> _properties = new Dictionary<string, object>();

        public string Country { get; set; }  
        
        public string Mail { get; set; }    
        public string GUID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public string Sex { get; set; }
        public string Age { get; set; } 
    
        public object this[string name]
        {
            get => (ContainsProperty(name)) ? _properties[name] : null;
            set => _properties[name] = value;
        }
        public ICollection<string> PropertyNames => _properties.Keys;
        public int PropertyCount => _properties.Count;
        public bool ContainsProperty(string name) => _properties.ContainsKey(name);
    }

}
