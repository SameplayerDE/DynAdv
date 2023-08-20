using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatrickAssFucker
{
    public interface IInventoryHolder
    {
        public List<char> Contents { get; }

        public void AddItem(char c);
        public void RemoveItem(char c);
    }
}
