using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Photos
{
    public class PhotoDTO<TKey>
    {
        public TKey Id { get; set; }
        public string Url { get; set; }
    }
}