using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ShoppingListService.Contracts
{
    [DataContract]
    public class Drink
    {
        public Drink() 
            : this("Random name")
        {
        }

        public Drink(string name)
        {
            this.Name = name;
        }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int Number { get; set; }
    }
}