using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace la2
{
    class operators
    {
        int left;
        int right;
        string text;
        // int count;
        public operators(int left, int right, string text)
        {
            this.left = left;
            this.right = right;
            this.text = text;
        }
        public void ifoperator(char left, char right)
        {
            string temp = "";
            /* wordbreaker wd = new wordbreaker();
             /* switch (left)
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
                              if (count == text.Length)
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
                              if (count == text.Length)
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
                              if (count == text.Length)
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
                          temp = left.ToString();
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
                              if (count == text.Length)
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
                              if (count == text.Length)
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
                              if (count == text.Length)
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
                              if (count == text.Length)
                                  break;
                              left = text[count];
                              if (count + 1 != length)
                                  right = text[count + 1];
                          } if (right == '=')
                          {
                              temp = "" + left + right;
                              this.count++;
                              // Console.WriteLine(count);
                              if (count == text.Length)
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
                              if (count == text.Length)
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
                              if (count == text.Length)
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
                              if (count == text.Length)
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
                              if (count == text.Length)
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
              */
           // return temp;
        }
    }
        
    
}
