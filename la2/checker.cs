using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;

namespace la2
{
    class Operators
    {
        public string operato;
        public string Class;

        
        public Operators(string op, string cls)
        {
            operato = op;
            Class = cls;
        }
    }
    class keyword
    {
        public string kword;
        public string Class;


        public keyword(string key, string cls)
        {
            kword = key;
            Class = cls;
        }
    }
    class Punctuators
    {
        public string punctuator;
        public string Class;


        public Punctuators(string punc, string cls)
        {
            this.punctuator = punc;
            this.Class = cls;
        }
    }
    class checker
    {   //keywords
        string temp;
        int line_no;
        int s_no;
        public checker(string temp, int line_no,int s_no)
        {
            this.temp = temp;
            this.line_no = line_no;
            this.s_no = s_no;
        }
        public string RegExpression(string input)
        {
            if (Regex.IsMatch(input, "([']([a-zA-Z]|[0-9]|[,.?;:=+-_()!@#$%^&*/`~]|\\\\'|\\\\(\\\\)|\\\\n|\\\\r|\\\\t|\\\\b|\\\\0|\\\\(\"))['])"))
                return "Character";

            else if (Regex.IsMatch(input, @"^[+|-]?[0-9]*[.][0-9]+((e|E)[+|-]?([0-9]+))?$"))
                return "Float";

            //  else if (Regex.IsMatch(input, @"^[A-Za-z]([A-Za-z]|[0-9])*$"))
            else if (Regex.IsMatch(input, @"^([A-Z]|[a-z]|_)([A-Z]|[a-z]|_|[0-9])*$"))
                return "identifier";

            else if (Regex.IsMatch(input, @"^([+]|[-])?[0-9]+$"))
                return "integer";

            // else if (Regex.IsMatch(input, "\"([A-Za-z0-9+=_!@#$%^&*() ~`,./;:-]|[0bfnrt']|\\\\([0bfnrt']|\\\\)|\\\")+\""))
            else if (Regex.IsMatch(input, "(\"([a-zA-Z]|[0-9]|[, . ? ; : [ ] { } = + - _ ! @ # $ % ^ & * ( ) | / ` ~ ']|\\\\'|\\\\(\\\\)|\\\\n|\\\\r|\\\\t|\\\\b|\\\\0)+\")"))


                return "string";

            else return "invalid ";



        }
        string integr(string temp)
        { string output="invalid";
        int count = 0;
        for (int i = 0; i < temp.Length; i++)
        {
            if (temp[i] == '0' || temp[i] == '1' || temp[i] == '2'
                || temp[i] == '3' || temp[i] == '4' || temp[i] == '5'
                || temp[i] == '6' || temp[i] == '7' || temp[i] == '8'
                || temp[i] == '9')
                count++;
        }
        if (count == temp.Length)
            output = "integer";
        return output;
        }
        bool str(string temp)
        {
            bool chk = false;
            if (temp[0] == '\"' && temp[temp.Length - 1] == '\"')
                chk = true;
            return chk;
        }
        string isRealNumber(string temp)
        {
            int i, len = temp.Length;
            string outpu="";
            int chk = 0;
            //if (len == 0)
              //  return (invalid);
            for (i = 0; i < len; i++)
            {
                if (integr(temp) == "integer")
                {
                    if (temp[i] == '.')
                        chk = 1;
                }
                else
                    outpu = "invalid";
            }
            if (chk == 1&&integr(temp)=="integer" )
                outpu = "float";
            return outpu;
        } 
        bool ident(string temp)
        {
            bool chk = false;

            for (int i = 0; i < temp.Length; i++)
            {
                //Console.WriteLine(temp[i]);
                if (validIdentifier(temp) != true)
                {
           //         Console.WriteLine("valid id");
                    break;
                }
                else if (temp[i] == '\'' || temp[i] == '\"' || temp[i] == '\\' || isOperator(temp[i]) == true || ispunctuator(temp[i]) == true )
                {
                    chk = false;
                    break;
                }
                else chk = true;



            }
           // if (chk != false)
             //   chk = true;
            //Console.WriteLine(chk);
            return chk;
        }
        bool validIdentifier(string str)
        {
            bool chk=false;
           
            for (int i = 0; i < str.Length; i++)
            {
                if (str[0] != '0' && str[0] != '1' && str[0] != '2' &&
               str[0] != '3' && str[0] != '4' && str[0] != '5' &&
               str[0] != '6' && str[0] != '7' && str[0] != '8' &&
               str[0] != '9' && isOperator(str[0]) != true && ispunctuator(str[0]) != true && str[0] != '.')
                {
//                    Console.WriteLine(str[0]);
                    if (isOperator(str[i]) == true && ispunctuator(str[i]) == true && str[i] == '.')
                    {
                        chk = false;
                        break;
                    }

                    else
                        chk = true;
            
                }}
            
            //if (chk != false)
              //  chk = true;
            return chk;
        }

        bool isOperator(char ch)
        {
            if (ch == '+' || ch == '-' || ch == '*' ||
                ch == '/' || ch == '>' || ch == '<' ||
                ch == '=' || ch == '%' || ch == '&' || ch == '!' || ch == '~' || ch == '|')
                return (true);
            return (false);
        }
        bool ispunctuator(char ch)
        {
            if (ch == ',' || ch == ';' || ch == '(' || ch == ')' ||
                ch == '[' || ch == ']' || ch == '{' || ch == '}' || ch == '.' || ch == ':')
                return (true);
            return (false);
        }
        public token check(string temp, int line)
        {
            List<keyword> words = new List<keyword>();
            words.Add(new keyword("LOOP", "LOOP"));//for
            words.Add(new keyword("Stop", "Stop"));//for break
            words.Add(new keyword("Cont", "Cont"));//continuo
            words.Add(new keyword("Ret", "Ret"));//return
            words.Add(new keyword("Bydefault", "Bydefault"));
            words.Add(new keyword("Agar", "Agar"));//if
            words.Add(new keyword("Warna", "Warna"));//else
            words.Add(new keyword("Select", "Select"));//switch
            words.Add(new keyword("CC", "CC"));//case
            words.Add(new keyword("Ref", "Ref"));//this
            words.Add(new keyword("Setter", "Setter"));//set
            words.Add(new keyword("Getter", "Getter"));//get
            words.Add(new keyword("Public", "Access Modifier"));
            words.Add(new keyword("Private", "Access Modifier"));
            words.Add(new keyword("Possesive", "Access Modifier"));//protected
            words.Add(new keyword("Number", "DataType"));//int
            words.Add(new keyword("Letter", "DataType"));//char
            words.Add(new keyword("Point", "DataType"));//float
            words.Add(new keyword("Bool", "DataType"));// bool
            words.Add(new keyword("Alpha", "DataType"));//string
            words.Add(new keyword("Room", "Room"));//class
            words.Add(new keyword("Po*", "Po*"));//pointer
            words.Add(new keyword("Const", "Const"));//constant
            words.Add(new keyword("While", "While"));//while
            words.Add(new keyword("Fixed", "Fixed"));//fixed
            words.Add(new keyword("True", "TF"));//true
            words.Add(new keyword("False", "TF"));//false
            words.Add(new keyword("Null", "Null"));//null
            words.Add(new keyword("St", "St"));//static
            words.Add(new keyword("Invoke", "Invoke"));//new
            words.Add(new keyword("Obj", "Obj"));//object
            words.Add(new keyword("Devoid", "Devoid"));//void
            words.Add(new keyword("Main", "Main"));//void
            words.Add(new keyword("Extend", "Extend"));//extends
            words.Add(new keyword("Super", "Super"));//extends

            words.Add(new keyword("LOOP\r", "LOOP"));//for
            words.Add(new keyword("Stop\r", "Stop"));//for break
            words.Add(new keyword("Cont\r", "Cont"));//continuo
            words.Add(new keyword("Ret\r", "Ret"));//return
            words.Add(new keyword("Default\r", "Default"));
            words.Add(new keyword("Agr\r", "Agr"));//if
            words.Add(new keyword("Wrna\r", "Wrna"));//else
            words.Add(new keyword("Select\r", "Select"));//switch
            words.Add(new keyword("CC\r", "CC"));//case
            words.Add(new keyword("Ref\r", "Ref"));//this
            words.Add(new keyword("Setter\r", "Setter"));//set
            words.Add(new keyword("Getter\r", "Getter"));//get
            words.Add(new keyword("Public\r", "Access Modifier"));
            words.Add(new keyword("Private\r", "Access Modifier"));
            words.Add(new keyword("Possesive\r", "Access Modifier"));//protected
            words.Add(new keyword("Number\r", "DataType"));//int
            words.Add(new keyword("Letter\r", "DataType"));//char
            words.Add(new keyword("Point\r", "DataType"));//float
            words.Add(new keyword("Bool\r", "DataType"));//int
            words.Add(new keyword("Alpha\r", "DataType"));//string
            words.Add(new keyword("Room\r", "Room"));//class
            words.Add(new keyword("Po*\r", "Po*"));//pointer
            words.Add(new keyword("Const\r", "Const"));//constant
            words.Add(new keyword("While\r", "While"));//while
            words.Add(new keyword("Fixed\r", "Fixed"));//fixed
            words.Add(new keyword("True\r", "TF"));//true
            words.Add(new keyword("False\r", "TF"));//false
            words.Add(new keyword("Null\r", "Null"));//null
            words.Add(new keyword("St\r", "St"));//static
            words.Add(new keyword("Invoke\r", "Invoke"));//new
            words.Add(new keyword("Obj\r", "Obj"));//object
            words.Add(new keyword("Devoid\r", "Devoid"));//void
            words.Add(new keyword("Main\r", "Main"));//void
            

            List<Punctuators> Pun = new List<Punctuators>();
            Pun.Add(new Punctuators(",", ","));
            Pun.Add(new Punctuators(";", ";"));
            Pun.Add(new Punctuators(":", ":"));
            Pun.Add(new Punctuators("[", "["));
            Pun.Add(new Punctuators("]", "]"));
            Pun.Add(new Punctuators("{", "{"));
            Pun.Add(new Punctuators("}", "}"));
            Pun.Add(new Punctuators("(", "("));
            Pun.Add(new Punctuators(")", ")"));
            Pun.Add(new Punctuators(".", "Dot"));


           /* Pun.Add(new Punctuators(",\r", ","));
            Pun.Add(new Punctuators(";\r", ";"));
            Pun.Add(new Punctuators(":\r", ":"));
            Pun.Add(new Punctuators("[\r", "["));
            Pun.Add(new Punctuators("]\r", "]"));
            Pun.Add(new Punctuators("{\r", "{"));
            Pun.Add(new Punctuators("}\r", "}"));
            Pun.Add(new Punctuators("(\r", "("));
            Pun.Add(new Punctuators(")\r", ")"));
            Pun.Add(new Punctuators(".\r", "Dot"));
            */


            List<Operators> Oper = new List<Operators>();
            Oper.Add(new Operators("=", "="));
            Oper.Add(new Operators("+", "PLUSMINUS"));
            Oper.Add(new Operators("-", "PLUSMINUS"));
            Oper.Add(new Operators("*", "MDM"));
            Oper.Add(new Operators("/", "MDM"));
            Oper.Add(new Operators("%", "MDM"));
            Oper.Add(new Operators("++", "IncDec_Operator"));
            Oper.Add(new Operators("--", "IncDec_Operator"));
            Oper.Add(new Operators("+=", "Assignment_Operator"));
            Oper.Add(new Operators("-=", "Assignment_Operator"));
            Oper.Add(new Operators("*=", "Assignment_Operator"));
            Oper.Add(new Operators("/=", "Assignment_Operator"));
            Oper.Add(new Operators("%=", "Assignment_Operator"));
            Oper.Add(new Operators(">>", "Shift_Operator"));
            Oper.Add(new Operators("<<", "Shift_Operator"));
            Oper.Add(new Operators("&&", "And"));
            Oper.Add(new Operators("||", "OR"));
            Oper.Add(new Operators("!", "!"));
            Oper.Add(new Operators("&", "&"));
            Oper.Add(new Operators("|", "|"));
            Oper.Add(new Operators("<", "Relational_Operator"));
            Oper.Add(new Operators(">", "Relational_Operator"));
            Oper.Add(new Operators("!=", "Relational_Operator"));
            Oper.Add(new Operators("<=", "Relational_Operator"));
            Oper.Add(new Operators(">=", "Relational_Operator"));
            Oper.Add(new Operators("==", "Relational_Operator"));


           /* Oper.Add(new Operators("=\r", "="));
            Oper.Add(new Operators("+\r", "PLUSMINUS"));
            Oper.Add(new Operators("-\r", "PLUSMINUS"));
            Oper.Add(new Operators("*\r", "MDM"));
            Oper.Add(new Operators("/\r", "MDM"));
            Oper.Add(new Operators("%\r", "MDM"));
            Oper.Add(new Operators("++\r", "IncDec_Operator"));
            Oper.Add(new Operators("--\r", "IncDec_Operator"));
            Oper.Add(new Operators("+=\r", "Assignment_Operator"));
            Oper.Add(new Operators("-=\r", "Assignment_Operator"));
            Oper.Add(new Operators("*=\r", "Assignment_Operator"));
            Oper.Add(new Operators("/=\r", "Assignment_Operator"));
            Oper.Add(new Operators("%=\r", "Assignment_Operator"));
            Oper.Add(new Operators(">>\r", "Shift_Operator"));
            Oper.Add(new Operators("<<\r", "Shift_Operator"));
            Oper.Add(new Operators("&&\r", "And"));
            Oper.Add(new Operators("||\r", "OR"));
            Oper.Add(new Operators("!\r", "!"));
            Oper.Add(new Operators("&\r", "&"));
            Oper.Add(new Operators("|\r", "|"));
            Oper.Add(new Operators("<\r", "Relational_Operator"));
            Oper.Add(new Operators(">\r", "Relational_Operator"));
            Oper.Add(new Operators("!=\r", "Relational_Operator"));
            Oper.Add(new Operators("<=\r", "Relational_Operator"));
            Oper.Add(new Operators(">=\r", "Relational_Operator"));
            Oper.Add(new Operators("==\r", "Relational_Operator"));
            */int chk = 0, chk2 = 1;

              Console.WriteLine("                          =>   TOKEN  [" + s_no + "]  : " + temp);
            //            Console.Write(temp);
           List<token> TOKEN = new List<token>();
            token tk = new token();
            while (chk != 1)
            {
                tk = new token();
              //  token tk = new token();
                // Console.WriteLine("= ?  "+temp);
                for (int i = 0; i < Oper.Count; i++)
                {
                    if (Oper[i].operato == temp)
                    {
                        tk.lineno = line_no;
                        tk.classname = Oper[i].Class;
                        tk.value = temp;
                        //   Console.WriteLine("oper[i].class" + Oper[i].Class);
              //          Console.WriteLine("class part oper:" + tk.classname);
                        chk = 1;
                        chk2 = 0;
                        break;
                    }
                }
                for (int i = 0; i < Pun.Count; i++)
                {
                    //Console.WriteLine("punctuator loop"+"punctuaor=="+temp+"|bla");
                    if (Pun[i].punctuator == temp)
                    {
                        tk.lineno = line_no;
                        tk.classname = Pun[i].Class;
                        tk.value = temp;
                        // Console.WriteLine("punc[i].class" + Pun[i].Class);
                //        Console.WriteLine("class part punc=" + tk.classname);
                        chk = 1;
                        chk2 = 0;
                        break;
                    }
                }
                //                chk = 1;
                if (chk2 != 0 && temp != null)
                {
                    string st = RegExpression(temp);
                    switch (st)
                    {
                        case "integer":
                            {

                                tk.lineno = line_no;
                                tk.classname = "Integer_constant";
                                tk.value = temp;
                  //              Console.WriteLine("class part int=" + tk.classname);
                                chk = 1;
                                break;
                            }
                        case "Float":
                            {

                                tk.lineno = line_no;
                                tk.classname = "Float_constant";
                                tk.value = temp;
                    //            Console.WriteLine("class part ff=" + tk.classname + "value" + temp);
                                chk = 1;
                                break;
                            }
                        case "Character":
                            {
                                string p = "";
                                for (int i = 1; i < temp.Length - 1; i++)
                                {
                                    p = p + temp[i];
                                }
                                tk.lineno = line_no;
                                tk.classname = "Char_constant";
                                tk.value = p;
                      //          Console.WriteLine("class part ch=" + tk.classname);
                                chk = 1;
                                break;
                            }
                        case "string":
                            {
                                //temp = "abc.text";
                        //        Console.WriteLine(temp[temp.Length - 1]+"jhg");
                                if (temp[temp.Length-1] == '\"')
                                {
                                    string p="";
                                    for (int i = 1; i < temp.Length - 2;i++ )
                                    {
                                        p = p + temp[i];
                                    }
                          //          Console.WriteLine(p);
                                        tk.lineno = line_no;
                                    tk.classname = "String_constant";
                                    tk.value = p;
                            //        Console.WriteLine("class part st=" + tk.classname);
                                    chk = 1;
                                    break;
                                }
                                else
                                {
                                    
                                tk.lineno = line_no;
                                tk.classname = "Invalid";
                                tk.value = temp;
                              //  Console.WriteLine("class part invakid=" + tk.classname);
                                chk = 1;
                                break;
                            }
                                break;
                            }
                        case "identifer":
                            {
                                //Console.WriteLine("id for loop");
                                chk2 = 1;
                                for (int i = 0; i < words.Count; i++)
                                {
                                    //   Console.WriteLine("class part word=  " + words[i].kword);
                                    if (words[i].kword == temp)
                                    {
                                        tk.lineno = line_no;
                                        tk.classname = words[i].Class;
                                        tk.value = temp;
                                  //      Console.WriteLine("class part = " + tk.classname + "value" + temp);
                                        chk = 1;
                                        chk2 = 0;
                                        break;
                                    }
                                }
                                if (chk2 == 1)
                                {
                                    tk.lineno = line_no;
                                    tk.classname = "ID";
                                    tk.value = temp;
                                    //Console.WriteLine("class part id=" + tk.classname + "value" + temp);
                                    chk = 1;
                                    break;
                                }
                                break;
                            }
                        default:
                            {
                                int chk3 = 1;
                                //Console.WriteLine(temp+"invalid case");
                                if (temp == "\"\"")
                                {
                                  //  Console.WriteLine(temp.Length);
                                    tk.lineno = line_no;
                                    tk.classname = "String_constant";
                                    tk.value = temp;
                                    //Console.WriteLine("class part st=" + tk.classname);
                                    chk = 1;
                                    break;
                                }
                                if(temp==" ")
                                {
                                    //Console.WriteLine(temp.Length);
                                    tk.lineno = line_no;
                                    tk.classname = "String_constant";
                                    tk.value = temp;
                                    //Console.WriteLine("class part st=" + tk.classname);
                                    chk = 1;
                                    break;
                                }else if(str(temp)==true)
                                {
                                    
                                        string p = "";
                                        for (int i = 1; i < temp.Length - 2; i++)
                                        {
                                            p = p + temp[i];
                                        }
                                      //  Console.WriteLine("inavld srrtrnf ");
                                        tk.lineno = line_no;
                                        tk.classname = "String_constant";
                                        tk.value = p;
                                        //Console.WriteLine("class part st=" + tk.classname);
                                        chk = 1;
                                        break;
                                }
                                else
                                if (integr(temp)=="integer")
                                {
                                    tk.lineno = line_no;
                                    tk.classname = "Integer_constant";
                                    tk.value = temp;
                                  //  Console.WriteLine("class part int=" + tk.classname);
                                    chk = 1;
                                    break;
                                }
                                else if (isRealNumber(temp) == "float")
                                {
                                    tk.lineno = line_no;
                                    tk.classname = "Float_constant";
                                    tk.value = temp;
                                    //Console.WriteLine("class part ff=" + tk.classname + "value" + temp);
                                    chk = 1;
                                    break;
                               }
                                    else if(temp=="\r")
                                {
                                }
                                else if(temp=="Po*"||temp=="Po*\r")
                                {
                                    tk.lineno = line_no;
                                    tk.classname = "Po*";
                                    tk.value = temp;
                                    //Console.WriteLine("class part = " + "Po*" + "value" + temp);
                                    chk = 1;
                                    chk2 = 0;
                                    break;
                                }
                                else if (ident(temp) == true)
                                {
                                    chk2 = 1;
                                    //Console.WriteLine("id invald case mai");
                                    for (int i = 0; i < words.Count; i++)
                                    {
                                        if (words[i].kword == temp)
                                        {
                                            tk.lineno = line_no;
                                            tk.classname = words[i].Class;
                                            tk.value = temp;
                                      //      Console.WriteLine("class part = " + tk.classname + "value" + temp);
                                            chk = 1;
                                            chk2 = 0;
                                            break;
                                            chk3=0;
                                        }
                                    }
                                    if (chk2 == 1)
                                    {
                                        tk.lineno = line_no;
                                        tk.classname = "ID";
                                        tk.value = temp;
                                        //Console.WriteLine("class part id=" + tk.classname + "value" + temp);
                                        chk = 1;
                                        chk3=0;
                                    }
                                    break;
                                }
                                else if(chk3!=0)
                                {
                                    tk.lineno = line_no;
                                    tk.classname = "Invalid";
                                    tk.value = temp;
                                    //Console.WriteLine("class part invalid=" + tk.classname);
                                    chk = 1;
                                    break;
                                }
                                break;
                            }
                    }
                }
                //System.IO.File.WriteAllText(@"C:\Users\aryanz\Documents\Visual Studio 2013\Projects\la2\write.txt", temp);
     
            }
           
                TOKEN.Add((new token(tk.value, tk.classname, tk.lineno)));
                chk2 = 1;
                // string output;OU
                string output = null;
                output = "                      " + s_no + "                  " + tk.classname + "       " + tk.value + "         " + tk.lineno.ToString();
                // Console.WriteLine("line filling===?" + temp);
                File.AppendAllText(@"C:\Users\aryanz\Documents\Visual Studio 2013\Projects\la2\write.txt", output + Environment.NewLine);
                // }
                //                Console.WriteLine("sno" + s_no);
                //              Console.WriteLine("token " + TOKEN[0].value);
                return tk;
            

        }
        
    }

}