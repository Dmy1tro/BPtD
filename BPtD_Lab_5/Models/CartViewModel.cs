using System.Collections.Generic;

namespace BPtD_Lab_5.Models
{
    public class CartViewModel
    {
        public string Data { get; set; }

        public string Signature { get; set; }

        public ICollection<Item> Cart { get; set; }
    }
}
