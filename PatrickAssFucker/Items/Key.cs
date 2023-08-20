using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatrickAssFucker.Items
{
    public class Key : Item
    {

        public int DoorCode;

        public Key(string name) : base(Material.KEY, name)
        {
        }
    }
}
