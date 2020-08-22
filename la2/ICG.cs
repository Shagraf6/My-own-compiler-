using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace la2
{
    class C_S
    {
        string stop = "";
        string cont = "";
        
    }
    class ICG
    {
        public stack stack2 = new stack();
 public       int count = 0;
        public string code;
        public int t_count = 0;
        public string type = "";
        public string cont = "";
        public string stop = "";
        public stack_s cs_stack = new stack_s();
        public int stack_count = 1;
        public string Create_label2()
        {
            int temp = stack2.pop();
            return "L" + temp+":";
        }
        public string jump_label2()
        {
            int temp = stack2.peek();
            return "JMP L" + temp;
        }
        public string jump_label1()
        {
            int temp = stack2.peek();
            temp = temp + 1;
            return "JMP L" + temp;
        }
        public string create_type()
        {
            t_count++;
            return "T" + t_count;
        }
        public string Create_label()
        {
            ++this.count;
            return "L" + this.count+":";        }
        public void Write2(string Label, string temp,string op)
        {
            //    Console.WriteLine(temp + "=" + Label);
            if (op == "+=")
                op = "="+temp+"+";
            if (op == "-=")
                op = "=" + temp + "-";
            if (op == "/=")
                op = "=" + temp + "/";
            if (op == "*=")
                op = "=" + temp + "*";
            if (op == "%=")
                op = "=" + temp + "%";
          
            add_string(temp + op + Label);
        }
       public void write3(string temp)
        {
            add_string("if ( " + temp + " == false)");
        }
        public void Write(string Label,string temp )
        {
        //    Console.WriteLine(temp + "=" + Label);
            add_string(temp + "=" + Label);
        }
        public void write(symbol_table temp,string pL)
        {
            add_string(temp.class_name + "_" + temp.name + "_" + pL+" PROC");
        }
        public void push(int chk)
        {
            if (chk == 1)
                this.stack2.push(this.count);
            else
            {
                this.stack2.push(this.count);
                this.stack2.push(this.count - 1);
            }
        }
        public void push()
        {
                this.cs_stack.push("L"+this.count);
                this.cs_stack.push("L"+(this.count - 1));
        }
      
        public string pop_stack(stack_s s, stack_s s2)
        {
            for(int i=0;i<s.count+1;i++)
            add_string("PARAM " + s.pop());
            string st = s2.pop();
            for (int i = 0; i < s2.count; i++)
                st = s2.pop()+"_"+st;
            return st+",";
        }
        public string jmp_label()
        {
            ++this.count;
            return "JMP L" + this.count;
        }
        public string jmp2_label()
        {
          //  Console.WriteLine(stack2.peek()+"->");
                return "JMP L" + stack2.pop();
        }
       
        public void add_string(string line)
        {
            this.code = this.code +"" + line;
            this.code = this.code+"\n"+"\r\n";
          // Console.WriteLine("\n\n");
           //Console.WriteLine(this.code);
        }
        
    }
}
