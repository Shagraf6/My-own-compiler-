using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace la2
{
    class token
    {
           public string value;
        public string classname;
        public int lineno;

        public token()
        {
            value = "";
            classname = "";
            lineno = 0;
        }
        public token(token tk)
        {
            value = tk.value;
            classname = tk.classname;
            lineno = tk.lineno;
        }
        public token(String v, String cn, int ln)
        {
            value = v;
            classname = cn;
            lineno = ln;
        }
    }
    }

