using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace la2
{
    class Etype
    {
        public string Type1;
        public string type2;
        public string resultant;
        public string op;
        public Etype()
        {
            this.Type1 = "";
            this.type2 = "";
            this.resultant = "";
            this.op = "";
        }
        public void clear()
        {
            this.Type1 = "";
            this.type2 = "";
            this.resultant = "";
            this.op = "";
        }
        public void set(Etype E, string res)
        {
            E.Type1 = res;
            E.type2 = "";
            E.resultant = res;
            this.op = "";
        }        
    }
    class fn_table
    {
        public string name = "";
        public string type = "";
        public int scope = 0;
        public string Po = "";
        public string ret = "";
        public bool assign = false;
        public fn_table()
        {
            this.name = "";
            this.type = "";
            this.scope = 0;
            this.assign = false;
        }
        public fn_table(fn_table fn)
        {
            this.name = fn.name;
            this.type = fn.type;
            this.scope = fn.scope;
            this.assign = fn.assign;
            this.Po = fn.Po;
            this.ret = fn.ret;
        }
        public void clear()
        {
            this.name = "";
            this.type = "";
            this.scope = 0;
            this.ret = "";
            this.Po = "";
            this.assign = false;
        }
    
    }
    class symbol_table
    {
       public string name = "";
       public string type = "";
       public string PL = "";
       public string ret = "";
       public string PO = "";
       public string am = "";
       public string class_name = "";
       public bool final = false;
       public bool sta = false;
       public bool assign = false;
       public bool chk_const = false;

       public symbol_table()
        {
           this.name = "";
            this.type = "";
            this.am = "";
            this.final = false;
      this.sta = false;
            this.assign = false;
            this.PL = "";
            this.PO = "";
            this.ret = "";
            this.chk_const = false;
        }
       public symbol_table(string name,string type,string am,bool sta,bool final,bool assign)
        {
            this.name = name;
            this.type = type;
            this.am = am;
            this.final = final;
            this.sta = sta; 
           this.PL = "";
            this.ret = "";
            this.assign = assign;
        }
      public symbol_table(symbol_table cc)
       {
           this.name = cc.name;
           this.type = cc.type;
           this.am = cc.am;
           this.final = cc.final;
           this.assign = cc.assign;
           this.PL = cc.PL;
           this.ret = cc.ret;
           this.sta = cc.sta;
           this.PO = cc.PO;
           this.chk_const = cc.chk_const;
       }
   public     void clear()
      {
          this.name = "";
          this.type = "";
          this.am = "";
          this.final = false;
          this.sta = false;
          this.assign = false;
          this.PL = "";
          this.ret = "";
          this.PO = "";
          this.chk_const = false;
      }
    }
    class class_table
    {
       public string name = "";
       public string type = "";
       public string am = "";
       public bool final = false;
       public bool sta = false;
       public string xtends = "object";
       public List<symbol_table> CC=new List<symbol_table>();
       public   class_table()
        {
            this.name = "";
            this.type = "";
            this.am = "";
            this.final = false;
            this.sta = false;
            this.xtends = "object";
        }
       public class_table(string name, string type, string am, bool final,bool sta, string xtends, List<symbol_table> cc)
        {
            this.name = name;
            this.type = type;
            this.am = am;
            this.final = final;
            this.xtends = xtends;
            this.sta = sta;
            this.CC = cc;
        }
       public class_table(class_table ct)
       {
           this.name = ct.name;
           this.type = ct.type;
           this.am = ct.am;
           this.final = ct.final;
           this.xtends = ct.xtends;
           this.sta = ct.sta;
           this.CC = ct.CC;
       }
       public void clear()
       {
           this.name = "";
           this.type = "";
           this.am = "";
           this.final = false;
           this.sta = false;
           this.xtends = "object";
   this.CC=new List<symbol_table>();
       }
}
    
}
