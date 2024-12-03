using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akosdolgozata
{
    public class Szerzo
    {
        public string KeresztNev { get; private set; }
        public string VezetekNev { get; private set; }
        public string GUID { get; private set; }

        public Szerzo(string teljesnev)
        {
            var nevek = teljesnev.Split(' ');
            if (nevek.Length != 2 || nevek[0].Length < 3 || nevek[1].Length < 3 || nevek[0].Length > 32 || nevek[1].Length > 32)
            {
                throw new ArgumentException("A névnek 3-32 karakter hosszú kereszt- és vezetéknevet kell tartalmaznia.");
            }

            KeresztNev = nevek[0];
            VezetekNev = nevek[1];
            GUID = Guid.NewGuid().ToString();
        }
    }
}
