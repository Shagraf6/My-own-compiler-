using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace la2
{
    class strng
    {
        char left;
        char right;
        public strng(char left,char right)
        {
            this.left = left;
            this.right = right;
        }
       /* public string str()
        {
            string temp="";
            int chk = 0;
            while (chk != 1)
            {
                wordbreaker wd = new wordbreaker();
                temp = "" + left + right;
                wd.count++;
                if (wd.count + 1 != wd.length)
                    right = wd.text[wd.count + 1];
                left = wd.text[wd.count];
                //if(left=='\\')
                if (right == ';' || (left == '\\' && right == '\\' || left == '\"') || wd.count == wd.length) 
                    break;

                    if (wd.count + 1 != wd.text.Length)
                        right = wd.text[wd.count + 1];
                    left = wd.text[wd.count];
                //if(left==
            } return temp;

         */   
        }
    
}
