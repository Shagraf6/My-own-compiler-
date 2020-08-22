using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace la2
{
    class semantics
    {
        //        public int chk = 0;
        public int count = 0;
        public int counter = 0;
        stack st = new stack();
        public int createscope()
        {
            this.count++;
            st.push(this.count);
            return this.count;
        }
        public int destroyscope()
        {
            return st.pop();
        }
        //find dta in whole function and class table 
        //for assignmnt exprreson
        public String Lookup_scope(List<symbol_table> cc, List<fn_table> fn, string name, int scope, int chk2)
        {
            int s = scope;
            int count2 = 0;
            int chk = 0;
            string type = null;

            // Console.WriteLine(name+" => *"+scope);
            for (int j = 0; j < cc.Count; j++)
            {
                for (int m = 0; m < st.count; m++)
                {
                    //   st.display();

                    for (int i = 0; i < fn.Count; i++)
                    {
                        // Console.WriteLine(name + " -> name " + s + "=>scope");
                        if (fn[i].name == name && fn[i].scope == s)
                        {
                            chk = 1;
                            if (fn[i].assign != true && chk2 == 1)
                            {
                                chk = 1;
                                type = null;
                                Console.WriteLine(name + " -> variable is not assigned");
                                break;
                            }
                            else
                            {
                                type = fn[i].type;
                                chk = 1;
                                break;
                            }
                        }

                    }
                    if (chk == 1)
                        break;
                    else //else count2++; 
                    //     if (count2 == st.count)
                    {
                        //     s = st.arr[1];
                        s = st.arr[m];
                        //                     Console.WriteLine(s + "->" + name + "->scope" + count2);
                        //    count2++;
                    }

                }


                // else if(s==0)
                //   break;
            }
            if (chk != 1)
                type = Lookup(cc, name, chk2);
            // if (type == null)
            //   Console.WriteLine("not found");
            return type;
        }

        public String p_Lookup_scope(List<symbol_table> cc, List<fn_table> fn, string name, int scope, int chk2, string po)
        {
            int s = scope;
            int chk = 0;
            string type = null;
            // Console.WriteLine(name+" => *"+po);
            for (int j = 0; j < cc.Count; j++)
            {
                for (int i = 0; i < fn.Count; i++)
                {
                    if (fn[i].name == name && fn[i].scope == s)
                    {
                        // Console.WriteLine(fn[i].Po+"-> 7"+po);
                        if (fn[i].assign != true && chk2 == 1)
                        {
                            chk = 1;
                            type = null;
                            Console.WriteLine(counter + ") => " + name + " variable is not assigned");
                            break;
                        }
                        else
                        {
                            if (fn[i].Po == po)
                            {
                                type = fn[i].ret;
                                chk = 1;
                                break;
                            }
                            else
                            {
                                Console.WriteLine(counter + ") => invalid call to Pointer ");
                                counter++;
                                chk = 1;
                                break;

                            }
                        } break;
                    }
                }
                if (chk == 1)
                    break;
                if (st.count != 0)
                {
                    s = st.arr[st.count - 1];
                    // Console.WriteLine(s +"->"+name+ "->scope");
                }
                else if (type == null && chk != 1)
                {
                    if (cc[j].name == name)
                    {
                        if (cc[j].assign != true && chk2 == 1)
                        {
                            chk = 1;
                            type = null;
                            Console.WriteLine(counter + ") => " + name + " variable is not assigned");
                            break;
                        }
                        else
                        {
                            if (cc[j].PO == po)
                            {
                                type = cc[j].ret;
                                chk = 1;
                                break;
                            }
                            else
                            {
                                Console.WriteLine(counter + ") => invalid call to Pointer ");
                                counter++;
                                break;
                            }
                        } break;
                    }
                }
            } return type;
        }
        //find data in function table with scope
        //for decleration
        public string search(List<fn_table> fn, fn_table temp)
        {
            string type = null;
            for (int i = 0; i < fn.Count; i++)
            {
                if (fn[i].name == temp.name && fn[i].scope == temp.scope)
                {
                    type = fn[i].type;
                    //     Console.WriteLine("found in fn table-=?" + temp.name);
                    break;
                }
            }
            // if(type==null)
            //   Console.WriteLine("not found in fn table" + temp.name);
            return type;
        }
        //class dec lookup
        public bool Lookup(List<class_table> ct, class_table temp)
        {
            bool chk = false;
            for (int i = 0; i < ct.Count; i++)
            {
                if (ct[i].name == temp.name)
                {
                    //    Console.WriteLine(temp.name+"class chk");
                    chk = true;
                    break;
                }
            }
            return chk;
        }
        public string Lookup(List<class_table> ct, string Class, string name, int chk)
        {
            string type = null;
            int chk2 = 0;
            for (int i = 0; i < ct.Count; i++)
            {
                if (ct[i].name == Class)
                {
                    for (int j = 0; j < ct[i].CC.Count; j++)
                    {
                        if (ct[i].CC[j].name == name)
                        {
                            chk2 = 1;
                            //       Console.WriteLine("blaaaaaaaaa"+name);
                            if (chk == 2 && ct[i].CC[j].final == true)
                            {
                                Console.WriteLine(counter + " )" + ct[i].CC[j].name + " => Final data can not be changed");
                                counter++;
                            }
                            else if ((chk == 2 || chk == 3) && ct[i].CC[j].sta == true)
                            {
                                Console.WriteLine(counter + " )" + ct[i].CC[j].name + " => Static data can not be changed, It can be incremented only");
                                counter++;
                            }

                            else if ((chk == 1 || chk == 3) && ct[i].CC[j].assign != true)
                            {
                                Console.WriteLine(counter + " )" + ct[i].CC[j].name + " => use of unassigned variable");
                                counter++;
                                break;
                            }
                            else
                            {
                                chk = 1;
                                if (ct[i].CC[j].am != "Private")
                                    type = ct[i].CC[j].type;
                                else
                                {
                                    Console.WriteLine(counter + " )" + ct[i].CC[j].name + " =>Private data can not be accessed !!");
                                    counter++;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            if (type == null && chk2 == 0)
            {
                Console.WriteLine(counter + " ) " + name + "=>variable is not0 declared !!"); counter++;
            }
            //    Console.WriteLine(name + "*->" + type);  
            return type;
        }

        //class insertion table
        public void insertion(List<class_table> ct, class_table temp)
        {
            if (Lookup(ct, temp) == true)
            {
                Console.WriteLine(counter + " )" + temp.name + " => 1 redeclaration error");
                counter++;
                //return false;
            }
            else
            {
                ct.Add(new class_table(temp));

                //     ct[ct.Count - 1].CC[ct[ct.Count - 1].CC.Count].class_name = temp.name;
                // Console.WriteLine(ct[ct.Count - 1].CC[ct[ct.Count - 1].CC.Count - 1].class_name + "ct,  name =" + ct[ct.Count - 1].name);
            }
        }
        public String Lookup(List<symbol_table> cc, string name, int chk)
        {
            string type = null;
            int chk2 = 2;
            //      Console.WriteLine("skdk" + chk + "->" + name);

            for (int i = 0; i < cc.Count; i++)
            {
                if (cc[i].name == name)
                {
                    chk2 = 6;
                    if (chk != 0 && cc[i].final == true)
                    {
                        Console.WriteLine(counter + " )" + cc[i].name + " => Final data can not be changed");
                        counter++;
                        break;
                    }
                    if ((chk == 2 || chk == 3) && cc[i].sta == true)
                    {
                        Console.WriteLine(counter + " )" + cc[i].name + " => Static data can not be changed, It can be incremented only");
                        counter++;
                        break;
                    }
                    if ((chk == 1 || chk == 3) && cc[i].assign != true)
                    {
                        Console.WriteLine(counter + " ) " + cc[i].name + "=> Variable is not assigned");
                        counter++;
                        break;
                    }

                    else
                    {
                        if (cc[i].am != "Private")
                        {
                            //    Console.WriteLine("ka*kdk");
                            type = cc[i].type;
                            //chk2 = 6;
                            //break;
                        }
                        else
                        {
                            Console.WriteLine(counter + " )" + cc[i].name + " =>Private data can not be accessed !!");
                            counter++;
                            break;
                        }
                        break;
                    }
                    if (chk2 == 6)
                        break;
                }
            }
            if (type == null && chk2 != 6)
            {
                Console.WriteLine(counter + ") " + name + "=> variable is not* declared !! ");
                counter++;
                //break;
            }
            //   Console.WriteLine(name + "->" + type);  
            return type;
        }

        public string Lookup(List<class_table> ct, string name, int chk2)
        {
            string type = null;
            int chk = 0;
            for (int i = 0; i < ct.Count; i++)
            {
                if (ct[i].name == name)
                {
                    chk = 1;
                    //Console.WriteLine(name+"akkd");
                    if (ct[i].am != "Private")
                    {
                        if (chk2 == 1)
                        {
                            if (ct[i].final == true)
                            {
                                Console.WriteLine(counter + " ) =>Final class can't be extended !! ");
                                counter++;
                                break;
                            }
                            else
                            {
                                // Console.WriteLine("skda"+name+"-?"+ct[i].xtends);
                                type = ct[i].xtends;
                                break;
                            }
                            break;
                        }
                        else
                        {
                            if (chk2 == 0)
                            {
                                type = name;
                                //   Console.WriteLine("jsdja");
                            }
                            else
                                type = ct[i].xtends;
                            break;
                        }
                        break;
                    }

                    {
                        type = null;
                        Console.WriteLine(counter + ") => Private class can not be accessed !!");
                        counter++;
                        break;
                    }
                    break;
                }
            }
            if (chk == 0)
            {
                Console.WriteLine(counter + ")" + name + " => Class is not declared !!");
                counter++;
                chk = 0;
            } return type;
        }

        //       public string lookup_inh(List<class_table> ct, string name,)

        //class object dec
        public string Lookup_const(List<class_table> ct, symbol_table temp, string PL2)
        {
            string type = null;
            //    Console.WriteLine(PL2);
            for (int i = 0; i < ct.Count; i++)
            {
                if (ct[i].name == temp.class_name)
                {
                    for (int j = 0; j < ct[i].CC.Count; j++)
                    {
                        //          Console.WriteLine(temp.name + "nshm");
                        if (ct[i].CC[j].name == temp.name)
                        {
                            if (ct[i].CC[j].PL == PL2)
                            {
                                type = ct[i].CC[j].ret;
                                // Console.WriteLine(ct[i].CC[j].ret + " ->ret");
                                //Console.WriteLine(type + " *->ret");
                                break;
                            }
                            else
                            {
                                Console.WriteLine(counter + " ) => function not found !!");
                                counter++;
                                break;
                            }

                        }

                    }
                }
            }

            //            Console.WriteLine("return type :" + type);
            return type;
        }

        public string Lookup_fn(List<symbol_table> cc, symbol_table temp, string PL2)
        {
            string type = null;
            // Console.WriteLine(temp.name+ " ->ret" + PL2+ " -> PL");

            for (int i = 0; i < cc.Count; i++)
            {
                //        Console.WriteLine("lls" + cc[i].name);

                if (cc[i].name == temp.name)
                {
                    if (cc[i].PL == PL2)
                    {
                        type = cc[i].ret;
                        //              Console.WriteLine(cc[i].ret + " ->ret" + cc[i].PL + " ->PL");
                        break;
                    }
                    else
                    {
                        Console.WriteLine(counter + " ) => Function  not found !!");
                        counter++;
                        break;
                    }
                }
            }
            //   Console.WriteLine("return type :" + type);
            return type;
        }
        public bool Lookup_fn(List<symbol_table> cc, symbol_table temp)
        {
            bool chk = false;
            for (int i = 0; i < cc.Count; i++)
            {
                if (cc[i].type == temp.type && cc[i].name == temp.name && cc[i].am != "Priavte")
                {
                    //Console.WriteLine(cc[i].name+"   "+temp.name+"redecleration function type");
                    chk = true;
                    break;
                }
                else if (cc[i].type == temp.type && cc[i].name == temp.name && cc[i].am != "Priavte")
                {
                    Console.WriteLine(counter + ") =>Priavate Function cannot be accessed !!");
                    counter++;
                    break;
                }
            }
            return chk;
        }
        public void insertion_fn(List<symbol_table> cc, symbol_table temp)
        {
            if (Lookup_fn(cc, temp) == true)
            {
                Console.WriteLine(counter + ") =>Function->redecleration error ");
                counter++;
                // return false;
            }
            else
            {
                cc.Add(new symbol_table(temp));
                //              Console.WriteLine(cc.Count + "cc.count,  name =" + cc[cc.Count - 1].name);
                // return true;
            }
            //return false;
        }
        public void insertion_fn(List<fn_table> fn, fn_table temp)
        {
            if (search(fn, temp) != null)
            {
                Console.WriteLine(counter + " ) =>  fn redeclaration error");
                counter++;
            }
            else
            {
                fn.Add(new fn_table(temp));
                //      Console.WriteLine(fn.Count + "fn.count,  name =" + fn[fn.Count - 1].name);
            }
        }
        public bool search(List<symbol_table> cc, symbol_table temp)
        {
            bool chk = false;
            for (int i = 0; i < cc.Count; i++)
            {
                if (cc[i].name == temp.name)
                {
                    //                  Console.WriteLine(temp.name);
                    chk = true;
                    break;
                }
            }
            return chk;
        }

        public bool insertion(List<symbol_table> cc, symbol_table temp)
        {
            bool chk = false;
            if (search(cc, temp) != true)
            {
                cc.Add(new symbol_table(temp));
                chk = true;
                //Console.WriteLine(cc.Count + "cc.count,  name =" + cc[cc.Count - 1].name);
            }
            else
            {
                Console.WriteLine(counter + " ) =>redecleration error !!");
                counter++;
            } return chk;
        }

        public void chk_inh(class_table st)
        {
            if (st.final == true)
            {
                Console.WriteLine(counter + " ) =>Final class can't be extended !! ");
                counter++;
            }
        }
        public void setam(class_table ct, symbol_table st, string chk, string value)
        {
            if (chk == "class")
                ct.am = value;
            else
                st.am = value;
        }
        public void setst(class_table ct, symbol_table st, string chk, bool value)
        {
            if (chk == "class")
                ct.sta = value;
            else
                st.sta = value;
        }

        public void setfinal(class_table ct, symbol_table st, string chk, bool value)
        {
            if (chk == "class" && st.am != "Private")
                ct.final = value;
            else if (chk == "class" && st.am == "Private")
            {
                Console.WriteLine(counter + ") => Private class can not be extended");
                counter++;
            }
            else
                st.final = value;
        }
        public bool compatibility(List<class_table> ct, symbol_table temp, string temp_name, string PL2)
        {
            bool chk = false;
            int chk2 = 0;
            int chk3 = 0;
            //  Console.WriteLine(temp_name+ "8" + temp.class_name);

            for (int i = 0; i < ct.Count; i++)
            {
                if (temp.class_name == temp_name)
                {
                    if (ct[i].name == temp.class_name)
                    {
                        //    Console.WriteLine("ajskie");
                        for (int j = 0; j < ct[i].CC.Count; j++)
                        {
                            //    Console.WriteLine("jsaie" + ct[i].CC[j].chk_const);
                            if (ct[i].CC[j].chk_const == false && PL2 == "")
                            {
                                chk2 = 1;
                                //    Console.WriteLine("null constructor" + ct[i].CC[j].name);
                                chk = true;
                                break;
                            }
                            else if (ct[i].CC[j].chk_const != false)
                            {
                                //                   Console.WriteLine("skkska" + ct[i].CC[j].PL);
                                if (ct[i].CC[j].PL == PL2 && ct[i].CC[j].name == temp.class_name)
                                {
                                    chk = true;
                                    chk3 = 1;
                                    break;
                                }

                            }
                        }
                        if (ct[i].CC.Count == 0 && PL2 == "" && chk2 == 1)
                        {
                            //  Console.WriteLine("null constructor**" + ct[i].name);
                            chk = true;
                            break;
                        }
                        else if (chk3 != 1 && chk2 != 1)
                        {
                            Console.WriteLine(counter + ") =>Constructor not found !!");
                            counter++;
                            chk = false;
                            break;
                        }

                    }
                }

                else if (chk != true)
                {
                    bool type = polymorphism(ct, temp.class_name, temp_name);
                    if (type == true)
                    {
                        // Console.WriteLine("lsla");
                        temp.type = temp_name;
                        chk = true;
                    }
                    else
                    {
                        Console.WriteLine(counter + ") => Type Mismatch :" + temp.class_name + "->" + temp_name);
                        counter++;
                    } break;
                }
            }
            return chk;
        }
        public bool polymorphism(List<class_table> ct, string s_class, string subinstance)
        {
            bool chk = false;
            string type = null;
            string temp = null;
            if (s_class != subinstance)
            {
                type = Lookup(ct, subinstance, 1);

                // Console.WriteLine(s_class+"polymorphism ="+type);
                if (type != null && type != "object")
                {
                    if (type == s_class)
                        chk = true;
                    else
                    {
                        temp = type;

                        while (temp != "object")
                        {
                            type = Lookup(ct, type, 1);
                            //                    Console.WriteLine(type + " =poly type");
                            if (type == "object" && temp == s_class)
                            {
                                chk = true;
                                break;
                            }
                            else if (type == "object" && temp != s_class)
                            {
                                Console.WriteLine(counter + ") => Polymorphsim => class is not extended");
                                counter++;
                                break;
                            } temp = type;

                        }
                    }
                }
                else if (type == "object" && temp == s_class)
                {
                    Console.WriteLine(counter + ") => Polymorphsim => class is not extended");
                    counter++;
                }
            }
            return chk;

        }
        public bool compatibility(symbol_table temp)
        {
            bool chk = true;
            if (temp.class_name != temp.name)
            {
                Console.WriteLine(counter + " ) " + temp.name + " => method must have a return type !!");
                counter++;
                chk = false;
            }
            return chk;
        }
        public string compatibility(string Type, string op)
        {
            string type = "";
            if ((op == "++" || op == "--") && (type != "Alpha" && type != "Bool"))
                type = Type;
            return type;

        }
        public string compatibility(string type1, string type2, string op)
        {
            string type = "";
            //  Console.WriteLine("typ1 :"+type1+"typ2 :"+type2+"op:"+op);
            if ((op == "Relational_Operator") && (type1 != "Alpha" || type2 != "Alpha"))
                type = "Bool";
            else if ((type1 == "Alpha" || type2 == "Alpha") && op == "+")
                type = "Alpha";
            else
                if ((type1 == "Alpha" || type2 == "Alpha") && op != "+")
                {
                    Console.WriteLine(counter + " ) =>Operation can not performed on String !!");
                    counter++;
                }
                else
                    if (type1 == "Point" || type2 == "Point")
                        type = "Point";
                    else if (type1 == "Number" || type2 == "Number")
                        type = "Number";
                    else if (type2 == "")
                        type = type1;
            //    Console.WriteLine("final type=" + type);
            return type;
        }
        public bool chk_compatibility(string type1, string type2)
        {
            //Console.WriteLine("chk Type " + type1 + "  " + type2);
            bool type = false;
            if (type1 == type2)
                type = true;
            else if (type1 == "Point" && (type2 == "Number" || type2 == "Letter"))
                type = true;
            else
            {
                if (type2 != null)
                    Console.WriteLine(counter + ") => Type Mismatch " + type1 + " -> " + type2);
                else
                    Console.WriteLine(counter + ") => Type Mismatch ->" + type1);
                counter++;
            } return type;
        }
        //public bool chk_Pointer()
        public void chk_compatibility2(string type1, string type2)
        {
            Console.WriteLine("chk Type " + type1 + "  " + type2);
            bool type = false;
            if (type1 == type2)
                type = true;
            else if (type1 == "Point" && (type2 == "Number" || type2 == "Letter"))
                type = true;
            else
            {
                if (type2 != null)
                    Console.WriteLine(counter + ") => Type Mismatch " + type1 + " -> " + type2);
                else
                    Console.WriteLine(counter + ") => Type Mismatch ->" + type1);
                counter++;
            } //return type;
        }

        public void set_type(Etype type, string T)
        {
            //  Console.WriteLine("*type string-> " + T);
            if (type.Type1 == "")
            {
                type.Type1 = T;
                //     Console.WriteLine("**typee1-> " + type.Type1);
            }
            else if (type.Type1 != "" && type.type2 == "")
            {
                type.type2 = T;
                //    Console.WriteLine("**typee2-> " + type.type2);
            }
            else
            {
                type.resultant = type.Type1;
                //  Console.WriteLine("resulatnt type ->" + type.resultant);
            }
        }
        public string chk_st_fin(List<symbol_table> cc, string n)
        {
            return "";
        }
    }
}