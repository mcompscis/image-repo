using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageRepo.Models
{
    public class ImageModel
    {
        public int Id { get; private set; }
        public string Name { get; set; }
        public byte[] ProductImage { get; set; }
    }
}
