using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.core.Entities
{
    public class Product :BaseEntity
    {//.Net 6 
     
        public string Name { get; set; }

        public string Description { get; set; }
        public string PictureUrl { get; set; }

        public decimal Price { get; set; }

    
        public int ProductBrandId { get; set; }
        //one
        public ProductBrand ProductBrand { get; set; }


        public int ProductTypeId { get; set; }
        //one
        public ProductType ProductType { get; set; }






    }
}
