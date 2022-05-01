using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class Product // all the properties for the product entity
    {
        public int Id { get; set; } // quick create with 'prop' -> tab
        public string Name { get; set; }
        public string Description { get; set; } 
        public long Price { get; set; }
        public string  PictureUrl { get; set; }
        public string Type { get; set; }
        public string Brand { get; set; }
        public int QuantityInStock { get; set; }    
    }
}