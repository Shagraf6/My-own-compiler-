using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace la2
{
    class wordbreaker
    {
         public string text;
         string temp;
        public int line = 1;
        public int count = 0;
        char left=' ', right=' ';
       public int lp=0;
        public int length;
        public List<token> tok = new List<token>();
        public token tk = new token();
       public wordbreaker(string text)
         {
             this.text = text;
             length = text.Length;
       }
        public wordbreaker()
       {
        }
        bool isRealNumber(char left)
        { bool hasDecimal = false;

            
                if (left != '0' &&left != '1' && left != '2'
                    && left!= '3' && left != '4' && left != '5'
                    && left != '6' && left != '7' && left != '8'
                    && left != '9' &&left != '.' ||
                    (left == '-' ))
                    return (false);
                if (left == '.')
                    hasDecimal = true;
            
            return (hasDecimal);
        } 
       bool ispunctuator(char ch)
       {
           if (ch == ',' || ch == ';' || ch == '(' || ch == ')' ||
               ch == '[' || ch == ']' || ch == '{' || ch == '}' || ch == ':')
               return (true);
           return (false);
       }
        public void newline(char ch)
       {
            if(ch=='\n')
            {
                line++;
            }
       }
       bool isOperator(char ch)
       {
           if (ch == '+' || ch == '-' || ch == '*' ||
               ch == '/' || ch == '>' || ch == '<' ||
               ch == '='|| ch == '%'|| ch == '&'|| ch == '!'|| ch == '~'|| ch == '|')
               return (true);
           return (false);
       }
       public string ifoperator(char left, char right)
       {
           string temp = "";
        //   wordbreaker wd = new wordbreaker();
           switch (left)
           {
               case '+':
                   {
                       if (right != '+' || right != '=')
                           temp = left.ToString();
                       if (right == '+' || right == '=')
                       {
                           temp = "" + left + right;
                           this.count++;
                           // Console.WriteLine(count);
                           if (count >= text.Length)
                               break;
                           left = text[count];
                           if (count + 1 != length)
                               right = text[count + 1];
                       }
                       else
                           break;
                       break;
                   }
               case '-':
                   {
                       if (right != '-' || right != '=')
                           temp = left.ToString();
                       if (right == '-' || right == '=')
                       {
                           temp = "" + left + right;
                           this.count++;
                           // Console.WriteLine(count);
                           if (count >= text.Length)
                               break;
                           left = text[count];
                           if (count + 1 != length)
                               right = text[count + 1];
                       }
                       else
                           break;
                       break;
                   }
               case '*':
                   {
                       if (right != '=')
                           temp = left.ToString();
                       if (right == '=')
                       {
                           temp = "" + left + right;
                           this.count++;
                           // Console.WriteLine(count);
                           if (count >= text.Length)
                               break;
                           left = text[count];
                           if (count + 1 != length)
                               right = text[count + 1];
                       }
                       else
                           break;
                       break;
                   }
               case '%':
                   {
                       if (right != '=')
                           temp = left.ToString();
                       if (right == '=')
                       {
                           temp = "" + left + right;
                           this.count++;
                           // Console.WriteLine(count);
                           if (count >= text.Length)
                               break;
                           left = text[count];
                           if (count + 1 != length)
                               right = text[count + 1];
                       }
                       else
                           break;
                       break;
                   }
               case '~':
                   {
                       temp = left.ToString();
                       break;
                   }
               case '/':
                   {
                       if (right != '=')
                           temp = left.ToString();
                       if (right == '=')
                       {
                           temp = "" + left + right;
                           this.count++;
                           // Console.WriteLine(count);
                           if (count >= text.Length)
                               break;
                           left = text[count];
                           if (count + 1 != length)
                               right = text[count + 1];
                       }
                       else
                           break;
                       break;
                   }
               case '!':
                   {
                       if (right != '=')
                           temp = left.ToString();
                       if (right == '=')
                       {
                           temp = "" + left + right;
                           this.count++;
                           // Console.WriteLine(count);
                           if (count >= text.Length)
                               break;
                           left = text[count];
                           if (count + 1 != length)
                               right = text[count + 1];
                       }
                       else
                           break;
                       break;
                   }
               case '=':
                   {
                       if (right != '=')
                           temp = left.ToString();
                       if (right == '=')
                       {
                           temp = "" + left + right;
                           this.count++;
                           // Console.WriteLine(count);
                           if (count >= text.Length)
                               break;
                           left = text[count];
                           if (count + 1 != length)
                               right = text[count + 1];
                       }
                       else
                           break;
                       break;
                   }
               case '<':
                   {
                       if (right != '=')
                           temp = left.ToString();
                       if (right == '<')
                       {
                           temp = "" + left + right;
                           this.count++;
                           // Console.WriteLine(count);
                           if (count >= text.Length)
                               break;
                           left = text[count];
                           if (count + 1 != length)
                               right = text[count + 1];
                       } if (right == '=')
                       {
                           temp = "" + left + right;
                           this.count++;
                           // Console.WriteLine(count);
                           if (count >= text.Length)
                               break;
                           left = text[count];
                           if (count + 1 != length)
                               right = text[count + 1];
                       }
                       else
                           break;
                       break;
                   }
               case '>':
                   {
                       if (right != '=')
                           temp = left.ToString();
                       if (right == '>')
                       {
                           temp = "" + left + right;
                           this.count++;
                           // Console.WriteLine(count);
                           if (count >= text.Length)
                               break;
                           left = text[count];
                           if (count + 1 != length)
                               right = text[count + 1];
                       }
                       if (right == '=')
                       {
                           temp = "" + left + right;
                           this.count++;
                           // Console.WriteLine(count);
                           if (count >= text.Length)
                               break;
                           left = text[count];
                           if (count + 1 != length)
                               right = text[count + 1];
                       }
                       else
                           break;
                       break;
                   }
               case '&':
                   {
                       if (right != '&')
                           temp = left.ToString();
                       if (right == '&')
                       {
                           temp = "" + left + right;
                           this.count++;
                           // Console.WriteLine(count);
                           if (count >= text.Length)
                               break;
                           left = text[count];
                           if (count + 1 != length)
                               right = text[count + 1];
                       }
                       else
                           break;
                       break;
                   }
               case '|':
                   {
                       if (right != '|')
                           temp = left.ToString();
                       if (right == '|')
                       {
                           temp = "" + left + right;
                           this.count++;
                           // Console.WriteLine(count);
                           if (count >= text.Length)
                               break;
                           left = text[count];
                           if (count + 1 != length)
                               right = text[count + 1];
                       }
                       else
                           break;
                       break;
                   }
               default:
                   break;
           }
          // count--;
           return temp;
       }
       public void backslash(char left, char right)
       {
           int chk = 0;
           if (left == '/' && right == '/')
           {
             //  Console.WriteLine("sinlge line comment");
               while (left != '\n')
               {
                   if (count >= text.Length)
                       break;
                   if (count + 1 != text.Length)
                       right = this.text[count + 1];
                   left = text[count];
                   newline(left);
                   count++;
               }
               count--;
           }
           if (left == '/' && right == '*')
           {
              // Console.WriteLine("multilinecomment");
               while (chk == 0)
               {
                   if (left == '*' && right == '/')
                       chk = 1;
                   if (count >= text.Length)
                       break;
                   if (count + 1 != text.Length)
                       right = this.text[count + 1];
                   left = text[count];
                   newline(left);
                   count++;
               }
              // Console.WriteLine("endmultilinecoment");
               count--;
           }
       }
        public void ischar(char left,char right)
       {
           int chk = count;
           //Console.WriteLine("ischr");
           temp = null;
           if (right == '\\')
           {
               while (count != (chk + 4))
               {
                   temp = "" + temp + left;
                   count++;
                   if (count >= text.Length || right == ';' || right == '\r' || right == '\n')
                       break;
                   left = text[this.count];
                   if (count + 1 != text.Length)
                       right = this.text[this.count + 1];
                   newline(left);
               }
           }
           else
           {
               while (count != chk + 3)
               {
                   temp = "" + temp + left;
                   count++;
                   if (count >= text.Length || right == ';' || right == '\r' || right == '\n')
                       break;
                   left = text[this.count];
                   if (count + 1 != text.Length)
                       right = this.text[this.count + 1];
                   newline(left);
               }
           }   
       count--;
         //  Console.WriteLine(temp);
       }

        string integr(char temp)
        {
            string output = "invalid";
          //  int count = 0;
                if (temp == '0' || temp == '1' || temp == '2'
                    || temp == '3' || temp == '4' || temp == '5'
                    || temp == '6' || temp== '7' || temp == '8'
                    || temp == '9')
                    output = "integer";
             //   Console.WriteLine(output);
            
            return output;
        }

        string integr(string temp)
        {
            string output = "invalid";
            int count = 1;
            for (int i = 0; i < temp.Length; i++)
            {
                if (temp[i] == '0' || temp[i] == '1' || temp[i] == '2'
                    || temp[i] == '3' || temp[i] == '4' || temp[i] == '5'
                    || temp[i] == '6' || temp[i] == '7' || temp[i] == '8'
                    || temp[i] == '9')
                    count = 0;
                else
                {
                    count = 1;
                break;}
            }
            if (count ==0 )
                output = "integer";
            return output;
        }
        public void Kw(char left, char right)
        {
            temp = null; bool flt=false;
            int chk3 = 1; int chk2 = 0;
            int count2 = 0;
            if (left != '\n'||left!='\r')
            {
                temp = "" + temp + left;                    
                while (chk3 != 0)
                {
                    if (left == '\n' || left == '\r' || left == '\t' )
                        break;
                    if (chk2 == 2 || right == ' ' || right == ';'  || right == ',' || (ispunctuator(right) == true && right != '.') || isOperator(right) == true || right == '\'' || right == '\"' || right == '\n' || right == '\r')
                    {

                        if (temp == "Po" && right == '*')
                        {
                            temp = "" + temp + right;
                            count++;
                            if (count >= text.Length)
                                break;
                            left = text[count];
                            if (count + 1 != text.Length)
                                right = text[count + 1];
                            newline(left);
                        }
                       if(flt==true )
                       {
                           for (int i = 0; i < temp.Length;i++ )
                           { 
                               if (temp[i] == '.')
                                   {
                                       for (int j = i+1; j < temp.Length; j++)
                                       {
                                           count2++;
                                       //    Console.WriteLine(count2 + "->flt ture"+"j->"+j+"temp.lenght"+temp.Length);
                                       }
                                       break;
                                   }
                           }
                           if (count2==2&&temp[temp.Length-1]==';')
                           {
                               string temp2="";
                               for (int i = 0; i < temp.Length-1; i++)
                               {
                                   temp2 = temp2+ temp[i];
                               }
                               temp = temp2;
                               count--;
                              if (count >= text.Length)
                                   break;
                               left = text[count];
                               if (count + 1 != text.Length)
                                   right = text[count + 1];
                           }
                       }
                        if (flt == true && left == 'e' && (right == '+' || right == '-'))
                        {
                            char temp2='a';
                            if (count + 1 != text.Length)
                                temp2 = text[count + 2];
                            if (integr(temp2) == "integer")
                            {
                                temp = "" + temp + right;
                                count++;
                                count++;
                                if (count >= text.Length)
                                    break;
                                left = text[count];
                                if (count + 1 != text.Length)
                                    right = text[count + 1];
                                while (true)
                                {
                                    temp = "" + temp + left;
                                    count++;
                                    if (count >= text.Length)
                                        break;
                                    left = text[count];
                                    if (count + 1 != text.Length)
                                        right = text[count + 1];
                                    if (integr(left) != "integer")
                                        break;
                                }
                            }
                            break;
                        }
                        break;
                    }count++;
                    if (count >= text.Length)
                        break;
                    left = text[count];
                    if (count + 1 != text.Length)
                        right = text[count + 1];
                   newline(left);
                    if(temp=="."&& (integr(left)!="integer"))
                    {
                      count--;
                    if (count >= text.Length)
                        break;
                    left = text[count];
                    if (count + 1 != text.Length)
                        right = text[count + 1];
                   newline(left);

                   break;
                    }
                   if ((integr(temp) != "integer") && left == '.' && (integr(right) != "integer"))
                   {
                    count--;
                       if (count >= text.Length)
                           break;
                       left = text[count];
                       if (count + 1 != text.Length)
                           right = text[count + 1];
                       break;
                   }
                   else
                   if ((integr(temp) != "integer") && left == '.')
                   {

//                       Console.WriteLine("case3");
                       count--;
                       if (count >= text.Length)
                           break;
                       left = text[count];
                       if (count + 1 != text.Length)
                           right = text[count + 1];

                       break;

                   }
                    else
                   if ((integr(temp) == "integer") && left == '.' && (integr(right) == "integer"))
                   {
//                      Console.WriteLine("case4");
                       flt = true;
//                       count2++;
                       temp = "" + temp + left + right;
                       count++;
                       count++;
                       if (count >= text.Length)
                           break;
                       left = text[count];
                       if (count + 1 != text.Length)
                           right = text[count + 1];
                       chk2++;
                   }
                   else
                       if ((integr(temp) == "integer") && left == '.' && (integr(right) != "integer"))
                       { 
                            //Console.WriteLine("case5");
                        break;
                       } if (chk2 == 1 && right == '.')
                   {
                       chk2 = 2;
                   }

                   temp = "" + temp + left;
                    
                    
                   
                }
            }
            
                 }

        public string str(char left ,char right)
        {

            temp = null;
            int chk = 0; int chk2 = 0;
            //temp = "" + temp + left;
            temp = "" + temp + left;
    
            while (chk != 1)
            {
//               Console.WriteLine("left-> "+left+"right-> "+ right);
                if (count == text.Length || (left != '\\' && right == '\"') || left == '\n' ||left == '\r' || chk2 == 1)
                {
                    if ((left != '\\' && right == '\"'))
                    {
                    temp = "" + temp + left+right;
                    count++;
                    count++;
                    if (this.count >= text.Length)
                    {
                        break;
                    } if (count + 1 != text.Length)
                        right = text[count + 1];
                    left = text[count];
                    newline(left);
                  chk2 = 1;
                    }
                    
                    chk = 1;
                    break;
                }                    count++;
                    if (count >= text.Length)
                        break;
                    if (count + 1 != text.Length)
                        right = text[count + 1];
                    left = text[count];
                    temp = "" + temp + left;

    }
        //   temp = "" + temp + left;
             
          /*
           if(left=='\n')
          {
              count--;
              if (count + 1 != text.Length)
                  right = text[count + 1];
              left = text[count];
          }*/
         // Console.WriteLine(temp);
        count--;
            return temp;
        }
        public void get_token()
        {
          //   Console.WriteLine("tokens");
             for (this.count = 0; count < text.Length; count++)
             {
                 temp = null;
                 if (count + 1 != text.Length)
                     right = this.text[count + 1];
                 left = this.text[count];
                 newline(left);
                 if ((left == '/' && right == '/') || (left == '/' && right == '*'))
                 {
                     // Console.WriteLine(left + "left" + "right" + right);
                     // if (left == '/' )
                     backslash(left, right);
                 }

                 else
                 {
                     if (left == '\'')
                         ischar(left, right);
                     else if (left == ' ')
                     {
                     }

                     else if (left == '\"')
                     {
                         temp = null;
                         //  temp = st.str();
                         temp = str(left, right);
                         //                          Console.WriteLine(temp+"=?liine no"+line);

                     }
                     else
                         if (ispunctuator(left) == true)
                         {
                             temp = null;
                             this.temp = left.ToString();
                         }

                         else if (isOperator(left) == true)
                         {
                             temp = null;
                             //operators opt = new operators(left, right, text);
                             this.temp = ifoperator(left, right);
                         }
                         else if (left != ' '|| left != ';' || left != '\r' || ispunctuator(left) != true || isOperator(left) != true || left != '\'' || left != '\"' || left != '\n')
                         {
                             Kw(left, right);

                         }
                     if (temp != null &&temp != "\t"&& temp != " " && temp != "\n" && temp != "\r")
                     {
                         lp++;
                       //  Console.WriteLine("mera naya temp length=> " + temp.Length);
                         checker ch = new checker(temp, line, lp);
                         tk = ch.check(temp, line);
                         tok.Add(new token(tk.value, tk.classname, tk.lineno));
                     }
                 }
                 temp = null;
             }
 }
                                    
    }
}
