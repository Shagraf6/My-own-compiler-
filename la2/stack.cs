using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace la2
{
    class stack
    {
        public int arraysize = 50;
        public int[] arr = new int[50];
        public int top = -1;
        public int count = 0;

        public int counter = 0;
        int p;
        public void push(int data)
        {
            if (count == arraysize)
                Console.WriteLine("stck is full");
            else
            {
                top++;
                arr[top] = data;
                p = data;
                count++;
                counter = count;
            }
        }
        public int pop()
        {
            int temp = 0;
            if (top == -1)
                Console.WriteLine("");
            else
            {
                temp = arr[top];
                top--;
                count--;
            }
            return temp;
        }
        public int peek()
        {
            return p;
        }
        public void display()
        {
            if (top == -1)
                Console.WriteLine("");
            else
            {
                int i;
                for (i = top; i >= 0; i--)
                    Console.WriteLine(arr[i]);
            }
        }

    }
    class stack_s
    {
        public int arraysize = 50;
        public string[] arr = new string[50];
        public int top = -1;
        public int count = 0;

        public int counter = 0;
        string p;
        public void push(string data)
        {
            if (count == arraysize)
                Console.WriteLine("stck is full");
            else
            {
                top++;
                arr[top] = data;
                p = data;
                count++;
                counter = count;
            }
        }
        public string pop()
        {
            string temp = "";
            if (top == -1)
                Console.WriteLine("");
            else
            {
                temp = arr[top];
                top--;
                count--;
            }
            return temp;
        }
        public string peek()
        {
            return p;
        }
        public void display()
        {
            if (top == -1)
                Console.WriteLine("");
            else
            {
                int i;
                for (i = top; i >= 0; i--)
                    Console.WriteLine(arr[i]);
            }
        }

    }


}

