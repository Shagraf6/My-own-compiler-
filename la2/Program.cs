using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace la2
{
    class Program
    {
        public static int index = 0;
           
    
        static void Main(string[] args)
        {
            string text = System.IO.File.ReadAllText(@"C:\Users\aryanz\Documents\Visual Studio 2013\Projects\la2\la.txt");
            int length=text.Length;
            Console.WriteLine("________________________________________________________________________________");
            Console.WriteLine("\n             .....*..... COMPILER CONSTRUCTION ( C C ).....*.....");
            Console.WriteLine("________________________________________________________________________________");
         
            Console.WriteLine("\n                       =>       SYNTAX :\n");
            System.Console.WriteLine("\n Contents of WriteText.txt = {0}", text);
            string temp = Console.ReadLine();
            Console.Clear();
            Console.WriteLine("________________________________________________________________________________");
            Console.WriteLine("\n             .....*..... COMPILER CONSTRUCTION ( C C ).....*.....");
            Console.WriteLine("________________________________________________________________________________");
         
            Console.WriteLine("\n\n________________________________________________________________________________");
            Console.WriteLine("\n              ......*..... LEXICAL ANALYZER  ( L A ).....*.....");
            Console.WriteLine("________________________________________________________________________________");
         
            Console.WriteLine("\n                        =>       TOKENS :\n");
          SA sa = new SA(text);
            string output = "________________________________________________________________________________________________________________________";
            File.AppendAllText(@"C:\Users\aryanz\Documents\Visual Studio 2013\Projects\la2\write.txt", output + Environment.NewLine);
             output = "\n                                                      T O K E N S";
            File.AppendAllText(@"C:\Users\aryanz\Documents\Visual Studio 2013\Projects\la2\write.txt", output + Environment.NewLine);
            output = "________________________________________________________________________________________________________________________";
            File.AppendAllText(@"C:\Users\aryanz\Documents\Visual Studio 2013\Projects\la2\write.txt", output + Environment.NewLine);
            output = "";
            File.AppendAllText(@"C:\Users\aryanz\Documents\Visual Studio 2013\Projects\la2\write.txt", output + Environment.NewLine);
           
            output = "\n\n                     S.No |" + "             " + "Token Class |" + " " + "Token Value |" + "  " + "Line No.";
            // Console.WriteLine("line filling===?" + temp);
            File.AppendAllText(@"C:\Users\aryanz\Documents\Visual Studio 2013\Projects\la2\write.txt", output + Environment.NewLine);
            output = "";
            File.AppendAllText(@"C:\Users\aryanz\Documents\Visual Studio 2013\Projects\la2\write.txt", output + Environment.NewLine);
            //List<token> TOKEN = new List<token>();
            temp = Console.ReadLine();
            Console.Clear();
            Console.WriteLine("________________________________________________________________________________");
            Console.WriteLine("\n             .....*..... COMPILER CONSTRUCTION ( C C ).....*.....");
            Console.WriteLine("________________________________________________________________________________");
         
            Console.WriteLine("\n________________________________________________________________________________");
            Console.WriteLine("\n                ......*..... SYNTAX ANALYZER ( S A ) .....*.....");
            Console.WriteLine("________________________________________________________________________________");
            Console.WriteLine("\n\n     => Semantic Error List \n\n");
            if (sa.ROOM() == true)
            {
                if (sa.Tokenset[sa.i].value == "$")
                    Console.WriteLine("\n\n                         =>   Valid Syntax....");
              else
              {
                  Console.WriteLine("\n\n                     =>   Error at line No. {0} !! ", sa.Tokenset[sa.i].lineno);
              }
            }
           else
           {
               Console.WriteLine("\n\n                     =>   Error at line No. {0} !! ", sa.Tokenset[sa.i].lineno);
           }

            temp = Console.ReadLine(); 
            Console.Clear();
            Console.WriteLine("________________________________________________________________________________");
            Console.WriteLine("\n             .....*..... COMPILER CONSTRUCTION ( C C ).....*.....");
            Console.WriteLine("________________________________________________________________________________");
            
            Console.WriteLine("\n\n________________________________________________________________________________");
            Console.WriteLine("\n                    ......*..... SEMANTICS .....*.....");
            Console.WriteLine("________________________________________________________________________________");
            Console.WriteLine("");
            Console.WriteLine("                              =>ClASS TABLE\n\n");
            Console.WriteLine("________________________________________________________________________________");
            Console.WriteLine("     NAME |" + "        AM |" + "   FINAL |" + "  TYPE |" + "    EXTENDS |" + "   STATIC |\n");
            Console.WriteLine("________________________________________________________________________________");
            for (int i = 0; i < sa.ct.Count; i++)
            {
                Console.WriteLine("  "+sa.ct[i].name + "     " + sa.ct[i].am + "      " +sa.ct[i].final + "       " + sa.ct[i].type + "       " + sa.ct[i].xtends + "       " + sa.ct[i].sta);
            }
            Console.WriteLine("");

            Console.WriteLine("\n\n");

            Console.WriteLine("                              =>SYMBOL TABLE\n\n");
            Console.WriteLine("________________________________________________________________________________");
            Console.WriteLine("   CLASS NAME     |" + " NAME |" + "    AM |" + "   FINAL |" + "  TYPE |" + "    ASSIGN |" + "   STATIC |\n");
            Console.WriteLine("________________________________________________________________________________");
            for (int i = 0; i < sa.ct.Count; i++)
            {
                for (int j = 0; j < sa.ct[i].CC.Count; j++)
                {
                  //  Console.WriteLine("j: " + j + " i:" + i + "class name :");
                    Console.WriteLine("  " + sa.ct[i].name + "   " + sa.ct[i].CC[j].name + "     " + sa.ct[i].CC[j].am + "      " + sa.ct[i].CC[j].final + "       " + sa.ct[i].CC[j].type + "       " + sa.ct[i].CC[j].assign + "       " + sa.ct[i].CC[j].sta);
                    Console.WriteLine("");
                }

            }
            Console.WriteLine("\n");

            Console.WriteLine("");

            Console.WriteLine("                          =>FUNCTION DATA TABLE\n\n");
            Console.WriteLine("           ______________________________________________________________");
            
            Console.WriteLine("                    NAME  |"  + "    ASSIGN  |" + "   SCOPE  | " +" TYPE |\n");
            Console.WriteLine("           _______________________________________________________________");
           
            for (int i = 0; i < sa.fn.Count; i++)
            {
                Console.WriteLine("                      " + sa.fn[i].name + "        " + sa.fn[i].assign + "        " + sa.fn[i].scope + "      " + sa.fn[i].Po + sa.fn[i].type);
            }

           /* string st="ab";
            char b = 'a';
            char f = 'a';
            int a = 1;
            float d = 1.2f;
           d=a*d;
           f = d + f;
            a = 5;*/
             output = "________________________________________________________________________________________________________________________";
             File.AppendAllText(@"C:\Users\aryanz\Documents\Visual Studio 2013\Projects\la2\IC.txt", output + Environment.NewLine);
             output = "\n                                                      TINTERMIDIATE CODE";
             File.AppendAllText(@"C:\Users\aryanz\Documents\Visual Studio 2013\Projects\la2\IC.txt", output + Environment.NewLine);
             output = "________________________________________________________________________________________________________________________";
             File.AppendAllText(@"C:\Users\aryanz\Documents\Visual Studio 2013\Projects\la2\IC.txt", output + Environment.NewLine);
             output = "";
             File.AppendAllText(@"C:\Users\aryanz\Documents\Visual Studio 2013\Projects\la2\IC.txt", sa.icg.code + Environment.NewLine);

             temp = Console.ReadLine(); Console.Clear();
             Console.WriteLine("________________________________________________________________________________");
             Console.WriteLine("\n             .....*..... COMPILER CONSTRUCTION ( C C ).....*.....");
             Console.WriteLine("________________________________________________________________________________");
             
            Console.WriteLine("\n\n________________________________________________________________________________");
             Console.WriteLine("\n              ......*..... INTERMIDIATE CODE GENERATOR .....*.....");
             Console.WriteLine("________________________________________________________________________________");
        
            Console.WriteLine(sa.icg.code);
            Console.ReadKey();
        }
    }
}
