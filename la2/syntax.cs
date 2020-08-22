using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace la2
{
    class SA
    {
        public int i = 0;
        string sr = "";
        public List<token> Tokenset = new List<token>();
        public List<class_table> ct = new List<class_table>();
        public token tok = new token();
        class_table Ct_temp = new class_table();
        symbol_table st_temp = new symbol_table();
        symbol_table param_tep = new symbol_table();
        symbol_table st_temp2 = new symbol_table();
        public List<fn_table> fn = new List<fn_table>();
        public fn_table fn_temp = new fn_table();
        public ICG icg = new ICG();
        string rel_op = null;
        string rel_label = null;
        string rel_type = null;
        int icg_chk = 0;
        int super_chk = 0;
        Etype etype = new Etype();
        int scope = 0;
        int check_s = 0;
        int check_fn = 0;
        string Label = "";
        string temp = "";
        string pl3 = "";
        string stop = "";
        string cont = "";
        int inc_chk = 0;
        string loop_temp = "";
        string Po_type = "";
        int Po_chk = 0;
        string PL = "";
        string PL2 = "";
        string type = "";
        string Op = "";
        string temp_name = "";
        stack_s p_type = new stack_s();
        stack_s p_stack = new stack_s();
        semantics Semantic = new semantics();
        public SA()
        {
        }
        public SA(string text)
        {
            this.sr = text;
            init(sr);
        }
        public void init(string text)
        {
            wordbreaker wd = new wordbreaker(text);
            List<token> Tokenset = new List<token>();
            wd.get_token();
            for (int i = 0; i < wd.tok.Count; i++)
                this.Tokenset.Add(new token(wd.tok[i]));
            this.Tokenset.Add(new token("$", "$", Tokenset.Count));
            //Console.WriteLine(Tokenset.Count);
        }

        public bool
            ROOM()
        {
            bool chk = false;
            st_temp.clear();
            Ct_temp.clear();
            //Console.WriteLine(Tokenset[i].classname);
            if (Tokenset[i].classname == "Access Modifier" || Tokenset[i].classname == "Fixed" || Tokenset[i].classname == "Room")//first
            {
                if (AM(Ct_temp, st_temp, "class"))//first
                {
                    //        Console.WriteLine(Ct_temp.am+"value of am ");
                    //                    i++;
                    if (FIXED(Ct_temp, st_temp, "class"))//<NT>-> function
                    {
                        //          Console.WriteLine("room->" + Tokenset[i].classname);
                        if (Tokenset[i].classname == "Room")//<T>
                        {
                            Ct_temp.type = Tokenset[i].value;
                            i++;
                            if (Tokenset[i].classname == "ID")
                            {

                                Ct_temp.name = Tokenset[i].value;
                                i++;
                                if (INH(Ct_temp))
                                {
                                    Semantic.insertion(ct, Ct_temp);
                                    if (Tokenset[i].classname == "{")
                                    {
                                        i++;
                                        //       if (CLASSBODY())
                                        //    {
                                        if (Tokenset[i].classname == "Access Modifier")
                                        {
                                            st_temp.am = Tokenset[i].value;
                                            i++;
                                            if (Tokenset[i].classname == "St")
                                            {
                                                st_temp.sta = true;
                                                i++;
                                                if (Tokenset[i].classname == "Devoid")
                                                {
                                                    st_temp.type = Tokenset[i].value;
                                                    i++;
                                                    if (Tokenset[i].classname == "Main")
                                                    {
                                                        st_temp.name = Tokenset[i].value;
                                                        st_temp.class_name = Ct_temp.name;
                                                        i++;
                                                        if (Tokenset[i].classname == "(")
                                                        {
                                                            st_temp.type = "" + "->" + st_temp.type;
                                                            i++;
                                                            if (Tokenset[i].classname == ")")
                                                            {
                                                                i++;
                                                                scope = Semantic.createscope();
                                                                //                                                  Console.WriteLine("scope =" + scope);
                                                                if (Tokenset[i].classname == "{")
                                                                {

                                                                    //                                                  
                                                                    Semantic.insertion(ct[ct.Count - 1].CC, st_temp);
                                                                    icg.write(st_temp, "");
                                                                    //
                                                                    st_temp.class_name = Ct_temp.name;
                                                                    st_temp2.class_name = Ct_temp.name;

                                                                    i++;
                                                                    //                          
                                                                    if (MST(ct[ct.Count - 1].CC, scope))
                                                                    {
                                                                        //                                                    Console.WriteLine(Tokenset[i].classname + " MST is k bd");

                                                                        if (Tokenset[i].classname == "}")
                                                                        {
                                                                            //                                                      Console.WriteLine(Tokenset[i].classname + " } is k bd");
                                                                            i++;
                                                                            icg.add_string("END P");
                                                                            scope = Semantic.destroyscope();
                                                                            if (CLASSBODY(ct[ct.Count - 1].CC, scope))
                                                                            {
                                                                                //                                                        Console.WriteLine(Tokenset[i].classname + " classbody is k bd");

                                                                                if (Tokenset[i].classname == "}")
                                                                                {
                                                                                    i++;

                                                                                    if (CLASSDEF())
                                                                                    {
                                                                                        //                                                              Console.WriteLine("room true");
                                                                                        return true;

                                                                                    }
                                                                                }

                                                                            }
                                                                            //                                       }
                                                                        }

                                                                    }

                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                //                Console.WriteLine("room false");
                chk = false;
            }
            return chk;
        }
        public bool O_ELSE(List<symbol_table> cc, int scope)
        {
            bool chk = false;
            if (Tokenset[i].classname == "Warna")
            {
                icg.add_string(icg.jmp_label());
                icg.add_string(icg.Create_label2());
                icg.push(1);
                i++;
                if (Tokenset[i].classname == "{")
                {
                    i++;
                    scope = Semantic.createscope();
                    if (MST(cc, scope))
                    {
                        if (Tokenset[i].classname == "}")
                        {
                            icg.add_string(icg.Create_label2());
                            i++;
                            scope = Semantic.destroyscope();
                            //                          Console.WriteLine("else true");
                            return true;
                        }
                    }
                }
            }
            else
                if (Tokenset[i].classname == "While" || Tokenset[i].classname == "Agar" || Tokenset[i].classname == "LOOP" || Tokenset[i].classname == "Ret" || Tokenset[i].classname == "IncDec_Operator" ||
                Tokenset[i].classname == "ID" || Tokenset[i].classname == "DataType" || Tokenset[i].classname == "Super" || Tokenset[i].classname == "Select" || Tokenset[i].classname == "Stop" || Tokenset[i].classname == "Ref" ||
                Tokenset[i].classname == "Cont" || Tokenset[i].classname == "Po*" || Tokenset[i].classname == "}")
                {

                    icg.add_string(icg.Create_label2());
                    return true;
                }
                else
                {
                    //                Console.WriteLine("else false" + Tokenset[i].classname);
                    chk = false;
                }
            return chk;
        }
        //if
        public bool AGR_WARNA(List<symbol_table> cc, int scope)
        {
            bool chk = false;
            //      Console.WriteLine(Tokenset[i].classname);
            //if //(Tokenset[i].classname == "IncDec_Operator" || Tokenset[i].classname == "=" || Tokenset[i].classname == "," || Tokenset[i].classname == "Dot" || Tokenset[i].classname == "(" ||
            if (Tokenset[i].classname == "Agar")
            {
                if (Tokenset[i].classname == "Agar")
                {
                    i++;
                    if (Tokenset[i].classname == "(")
                    {
                        i++;
                        if (EXP(cc, scope))
                        {
                            if (Tokenset[i].classname == ")")
                            {
                                // Console.WriteLine(etype.resultant);
                                if (etype.resultant != "Bool")
                                {
                                    Console.WriteLine(Semantic.counter + ") IF/ELSE => Cannot convert " + etype.resultant + " to \"Bool\"");
                                    Semantic.counter++;
                                } //Console.WriteLine("else");

                                icg.write3(temp);
                                icg.add_string(icg.jmp_label());
                                icg.push(1);
                                i++;
                                if (Tokenset[i].classname == "{")
                                {
                                    i++;
                                    scope = Semantic.createscope();
                                    if (MST(cc, scope))
                                    {
                                        if (Tokenset[i].classname == "}")
                                        {
                                            i++;

                                            scope = Semantic.destroyscope();
                                            if (O_ELSE(cc, scope))
                                            {
                                                //                                        Console.WriteLine("argr true");
                                                return true;
                                            }
                                        }
                                    }
                                }

                            }
                        }
                    }
                }
            }
            else
            {
                //      Console.WriteLine("agr false");
                chk = false;
            }
            return chk;
        }

        //mst
        public bool MST(List<symbol_table> cc, int scope)
        {
            // Console.WriteLine("mst scopw->" + scope);
            //bool chk = true;//sst ka nfirst
            if (Tokenset[i].classname == "While" || Tokenset[i].classname == "Agar" || Tokenset[i].classname == "LOOP" || Tokenset[i].classname == "Ret"
                || Tokenset[i].classname == "LOOP" || Tokenset[i].classname == "IncDec_Operator" || Tokenset[i].classname == "DataType"
                || Tokenset[i].classname == "ID" || Tokenset[i].classname == "Select" || Tokenset[i].classname == "Super" || Tokenset[i].classname == "Ref" || Tokenset[i].classname == "Cont"
                || Tokenset[i].classname == "Po*" || Tokenset[i].classname == "Stop")//first
            {

                //Console.WriteLine(cont + "=> Mst");
                // icg.cont = icg.cs_stack.pop();
                //icg.stop = icg.cs_stack.pop();

                if (SST(cc, scope))//ssT
                {


                    //      Console.WriteLine(Tokenset[i].classname + "sst agr tru inmst");
                    if (MST(cc, scope))
                    {
                        //        Console.WriteLine("mst true");
                        return true;
                    }
                }
            }
            else if (Tokenset[i].classname == "}")//Follow
            {
                icg.stop = stop;
                icg.cont = cont;

                //                Console.WriteLine("MST ka folllow true" + Tokenset[i].classname);
                return true;
            }
            else
            {
                //              Console.WriteLine("mst false");

                return false;
                //                chk = false;
            }
            return false;
        }
        //classdef chk ni ki
        public bool CLASSDEF()
        {
            Ct_temp.clear();
            st_temp.clear();
            fn_temp.clear();
            PL = "";
            if (Tokenset[i].classname == "Access Modifier" || Tokenset[i].classname == "Fixed" || Tokenset[i].classname == "Room")//first
            {
                if (AM(Ct_temp, st_temp, "class"))
                {
                    if (FIXED(Ct_temp, st_temp, "class"))
                    {
                        //                    Console.WriteLine("room->" + Tokenset[i].classname);
                        if (Tokenset[i].classname == "Room")//<T>
                        {
                            Ct_temp.type = Tokenset[i].value;
                            i++;
                            if (Tokenset[i].classname == "ID")
                            {
                                Ct_temp.name = Tokenset[i].value;
                                i++;
                                if (INH(Ct_temp))
                                {
                                    //  Ct_temp.CC[Ct_temp.CC.Count-1].class_name = ct[ct.Count - 1].name;
                                    // Console.WriteLine(ct[ct.Count - 1].CC[ct[ct.Count - 1].CC.Count - 1].class_name + " -> class kanam class def mao");
                                    Semantic.insertion(ct, Ct_temp);
                                    st_temp.class_name = Ct_temp.name;
                                    st_temp2.class_name = Ct_temp.name;
                                    if (Tokenset[i].classname == "{")
                                    {
                                        i++;
                                        if (CLASSBODY(ct[ct.Count - 1].CC, scope))
                                        {
                                            //    Console.WriteLine(ct[ct.Count - 1].name+"  ->class name ");
                                            if (Tokenset[i].classname == "}")
                                            {
                                                i++;
                                                if (CLASSDEF())
                                                {
                                                    //                                              Console.WriteLine("class def true");
                                                    return true;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else if (Tokenset[i].classname == "$")//Follow
            {
                return true;
            }
            //else
            //{
            //    Console.WriteLine("class def true");
            //chk = false;
            //    }
            return false;
        }
        public bool BODY(List<symbol_table> cc, int scope, int flag, string s, string c)
        {
            bool chk = false;
            if (Tokenset[i].classname == ";" || Tokenset[i].classname == "While" || Tokenset[i].classname == "Agar" || Tokenset[i].classname == "LOOP" || Tokenset[i].classname == "Ret" || Tokenset[i].classname == "IncDec_Operator" ||
                Tokenset[i].classname == "ID" || Tokenset[i].classname == "DataType" || Tokenset[i].classname == "Super" || Tokenset[i].classname == "Select" || Tokenset[i].classname == "Stop" || Tokenset[i].classname == "Ref" ||
                Tokenset[i].classname == "Cont" || Tokenset[i].classname == "Po*" || Tokenset[i].classname == "{")
            {
                if (Tokenset[i].classname == ";")
                {
                    i++;
                    //          Console.WriteLine("BODY fun is true");
                    return true;
                }

                else if (SST(cc, scope))
                {
                    //        Console.WriteLine("BODY fun is true");
                    return true;
                }
                else if (Tokenset[i].classname == "{")
                {
                    i++;
                    scope = Semantic.createscope();
                    if (MST(cc, scope))
                    {
                        if (flag == 1)
                            icg.add_string(loop_temp);
                        icg.add_string(icg.jmp2_label());

                        if (Tokenset[i].classname == "}")
                        {

                            icg.add_string(icg.Create_label2());
                            i++;
                            scope = Semantic.destroyscope();
                            //               Console.WriteLine("BODY fun is true");
                            return true;
                        }
                    }
                }
            }
            else
            {    //Console.WriteLine("BODY fun is false");
                chk = false;
            }
            return chk;
        }//assignmntopp
        //chmge kia hai neachy
        public bool LOOP(List<symbol_table> cc, int scope)
        {
            bool chk = true;
            if (Tokenset[i].classname == "LOOP")
            {
                //   Console.WriteLine("looop k andr");
                i++;
                if (Tokenset[i].classname == "(")
                {
                    //     Console.WriteLine("(");
                    i++;
                    if (F1(cc, scope))
                    {
                        //       Console.WriteLine("f1");
                        if (F2(cc, scope))
                        {
                            //         Console.WriteLine(Tokenset[i].classname + "f2");
                            if (Tokenset[i].classname == ";")
                            {
                                i++;
                                //           Console.WriteLine(Tokenset[i].classname + " =>termin");
                                if (F3(cc, scope))
                                {
                                    //             Console.WriteLine(Tokenset[i].classname + "F3");

                                    if (Tokenset[i].classname == ")")
                                        i++;
                                    {
                                        stop = icg.stop;
                                        cont = icg.cont;
                                        if (BODY(cc, scope, 1, stop, cont))
                                        {
                                            icg.cont = cont;
                                            icg.stop = stop;

                                            //                      Label = icg.Create_label();
                                            //                        icg.add_string(Label);
                                            //                   Console.WriteLine("LOOP fun true");
                                            return true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }



            else
            {
                //                Console.WriteLine("LOOP fun false");
                chk = false;
            }
            return chk;
        }
        public bool F1(List<symbol_table> cc, int scope)
        {
            bool chk = false;
            if (Tokenset[i].classname == "DataType" || Tokenset[i].classname == "ID" || Tokenset[i].classname == "Po*" || Tokenset[i].classname == ";")
            {

                if (Tokenset[i].classname == "DataType")
                {
                    fn_temp.type = Tokenset[i].value;
                    //  check_fn = 1;
                    i++;
                    if (ID_PO(cc, scope))
                    {
                        //                      Console.WriteLine("F1 fun true");
                        return true;
                    }
                }
                else if (Tokenset[i].classname == "ID")
                {
                    //check_fn = 1;
                    fn_temp.type = Semantic.Lookup_scope(cc, fn, Tokenset[i].value, scope, 1);
                    fn_temp.name = Tokenset[i].value;
                    i++;
                    if (ASSIGNMNET_OPERATOR(cc, scope))
                    {
                        if (Tokenset[i].classname == ";")
                        {
                            i++;
                            //                        Console.WriteLine("F1 fun true");
                            return true;
                        }
                    }
                }
                else if (P(cc, scope))
                {
                    check_fn = 1;
                    //              Console.WriteLine("F1 fun true");
                    return true;

                }

                else if (Tokenset[i].classname == ";")
                {

                    i++;
                    //            Console.WriteLine("F1 fun true");
                    return true;
                }
            }
            else
            {
                //      Console.WriteLine("F1 fun false");
                chk = false;
            }
            return chk;

        }
        public bool F2(List<symbol_table> cc, int scope)
        {
            bool chk = false;
            Label = icg.Create_label();
            icg.add_string(Label);
            icg.cont = "JMP L" + icg.count;
            if (Tokenset[i].classname == "PLUSMINUS" || Tokenset[i].classname == "ID" || Tokenset[i].classname == "Integer_constant" || Tokenset[i].classname == "Float_constant" || Tokenset[i].classname == "String_constant"
               || Tokenset[i].classname == "Char_constant" || Tokenset[i].classname == "!" || Tokenset[i].classname == "IncDec_Operator" || Tokenset[i].classname == "TF"
                || Tokenset[i].classname == "Po*" || Tokenset[i].classname == "&" || Tokenset[i].classname == "(" || Tokenset[i].classname == "Super")
            {
                if (EXP(cc, scope))
                {
                    //        Console.WriteLine("F2 fun true");
                    if (etype.resultant != "Bool")
                    {
                        Console.WriteLine(Semantic.counter + ") FOR Loop=> Cannot convert " + etype.resultant + " to \"Bool\"");
                        Semantic.counter++;
                    }
                    icg.write3(temp);
                    icg.stop = icg.jmp_label();
                    icg.add_string(icg.stop);
                    icg.push(0);
                    //icg.push();
                    return true;
                }
            }
            else if (Tokenset[i].classname == ";")
            {
                //  Console.WriteLine("F2 fun true");
                return true;
            }
            else
            {
                //Console.WriteLine("F2 fun false");
                chk = false;
            }
            return chk;


        }
        public bool F3(List<symbol_table> cc, int scope)
        {
            bool chk = false;
            if (Tokenset[i].classname == "IncDec_Operator" || Tokenset[i].classname == "ID" || Tokenset[i].classname == "Po*")
            {
                if (Tokenset[i].classname == "IncDec_Operator")
                {
                    etype.op = Tokenset[i].value;
                    i++;
                    if (ID_P2(cc, scope))//chngre kia h
                    {
                        if (etype.op == "++")
                            type = Semantic.Lookup_scope(cc, fn, Tokenset[i].value, scope, 1);
                        else
                            type = Semantic.Lookup_scope(cc, fn, Tokenset[i].value, scope, 3);
                        loop_temp = fn_temp.name + "= " + fn_temp.name + "+1";
                        //                        i++;
                        Console.WriteLine("F3 fun true" + loop_temp);
                        return true;
                    }
                }
                else if (ID_P2(cc, scope))//chngef
                {
                    // i++;
                    if (F4(cc, scope))
                    {
                        //    Console.WriteLine("F3 fun true");
                        return true;
                    }
                }
                /*  else if (Tokenset[i].classname == "Po*")
                  {
                      i++;
                      if (Tokenset[i].classname == "ID")
                      {
                          i++;
                          if (Tokenset[i].classname == "=")
                          {
                              i++;
                              if (EXP())
                              {
                                  Console.WriteLine("F3 fun true");
                                  return true;
                              }
                          }
                      }
                  }*/
            }
            else if (Tokenset[i].classname == ")")
            {
                //                Console.WriteLine("F3 fun true");
                return true;
            }
            else
            {
                //              Console.WriteLine("F3 fun False");
                chk = false;
            }
            return chk;

        }
        public bool F4(List<symbol_table> cc, int scope)
        {
            bool chk = false;
            if (Tokenset[i].classname == "Assignment_Operator" || Tokenset[i].classname == "=" || Tokenset[i].classname == "IncDec_Operator")
            {
                //            Console.WriteLine("f4 k andr " + Tokenset[i].classname);
                if (ASSIGNMNET_OPERATOR(cc, scope))
                {
                    //              Console.WriteLine("F4 fun true");
                    return true;
                }
                else if (Tokenset[i].classname == "IncDec_Operator")
                {
                    loop_temp = fn_temp.name + "=" + fn_temp.name + "+1";
                    // loop_temp = fn_temp.name + "= " + fn_temp.name + "+1";
                    //                        i++;
                    //  Console.WriteLine("F3 fun true" + loop_temp);
                    //icg.add_string(Label);
                    i++;
                    //            Console.WriteLine("F4 fun true");
                    return true;
                }
            }
            else
            {
                //   Console.WriteLine("F4 fun false");
                chk = false;
            }
            return chk;
        }
        public bool EXP(List<symbol_table> cc, int scope)
        {
            bool chk = false;
            etype.clear();
            icg_chk = 0;
            st_temp2.clear();
            Label = "";
            //            Console.WriteLine(Tokenset[i].classname);
            if (Tokenset[i].classname == "PLUSMINUS" || Tokenset[i].classname == "ID" || Tokenset[i].classname == "Integer_constant" || Tokenset[i].classname == "Float_constant" || Tokenset[i].classname == "String_constant"
               || Tokenset[i].classname == "Char_constant" || Tokenset[i].classname == "!" || Tokenset[i].classname == "IncDec_Operator" || Tokenset[i].classname == "TF"
                || Tokenset[i].classname == "Po*" || Tokenset[i].classname == "&" || Tokenset[i].classname == "(" || Tokenset[i].classname == "Super" || Tokenset[i].classname == "Ref")
            {
                //        Console.WriteLine(Tokenset[i].classname + "intcost");
                if (OE(cc, scope))
                {
                    if (etype.op == "" && etype.type2 == "" && icg_chk == 0)
                    {
                        Label = temp;
                        temp = icg.create_type();
                        icg.Write(Label, temp);
                        //    Console.WriteLine("alld"+Label);
                    }
                    ////   Console.WriteLine(Label+"EXP type=>"+etype.resultant);
                    //  Console.WriteLine(Label + "EXP type=>" + etype.resultant);
                    if (rel_op != null)
                    {
                        etype.op = rel_op;
                        if (etype.Type1 != null)
                        {
                            etype.type2 = rel_type;
                            etype.resultant = Semantic.compatibility(etype.Type1, etype.type2, rel_op);
                            Label = rel_label + temp;
                            temp = icg.create_type();
                            icg.Write(Label, temp);
                            Label = "" + temp;
                            // Console.WriteLine("")
                            //      Console.WriteLine(Label + "EXP type=>" + etype.resultant);
                            rel_op = null;
                            rel_label = null;
                            rel_type = null;
                        }
                        //else if (etype.op == "" && rel_op != null)
                        //{
                        //    Console.WriteLine("kakd");
                        //    etype.type2 = rel_type;
                        //    etype.resultant = Semantic.compatibility(etype.Type1, etype.type2, rel_op);
                        //    Label = rel_label + Label;
                        //    temp = icg.create_type();
                        //    icg.Write(Label, temp);
                        //    Label = "" + temp;
                        //    rel_op = null;
                        //    rel_label = null;
                        //    rel_type = null;

                        //}


                    }              //}//                Console.WriteLine(Tokenset[i].classname + "oe cost");

                    Label = "";
                    return true;
                }
            }
            else
            {
                //        Console.WriteLine(Tokenset[i].classname + " ->EXP fun false");
                return false;
            }
            return false;
        }
        public bool OE(List<symbol_table> cc, int scope)
        {
            bool chk = false;

            //  Console.WriteLine(Tokenset[i].classname + "->OE");

            if (Tokenset[i].classname == "PLUSMINUS" || Tokenset[i].classname == "ID" || Tokenset[i].classname == "Integer_constant" || Tokenset[i].classname == "Float_constant" || Tokenset[i].classname == "String_constant"
               || Tokenset[i].classname == "Char_constant" || Tokenset[i].classname == "!" || Tokenset[i].classname == "IncDec_Operator" || Tokenset[i].classname == "TF"
                || Tokenset[i].classname == "Po*" || Tokenset[i].classname == "&" || Tokenset[i].classname == "(" || Tokenset[i].classname == "Super" || Tokenset[i].classname == "Ref")
            {

                ///    Console.WriteLine(Tokenset[i].classname + "OE k angr");
                if (AE(cc, scope))
                {

                    //     Console.WriteLine(Tokenset[i].classname + "AE k andr");
                    if (OE2(cc, scope))
                    {

                        //       Console.WriteLine(Tokenset[i].classname + "OE2 k andr ");

                        //     Console.WriteLine("OE fun true");
                        return true;

                    }
                }

            }
            else
            {
                //                Console.WriteLine("OE fun false");
                return false;
            }
            return false;
        }

        public bool OE2(List<symbol_table> cc, int scope)
        {
            bool chk = false;
            if (Tokenset[i].classname == "OR")
            {
                //                etype.op = Tokenset[i].classname;
                //              Label = Label + Tokenset[i].value;

                rel_op = Tokenset[i].classname;
                rel_label = Label + Tokenset[i].value;
                rel_type = etype.resultant;
                etype.clear();
                // temp = icg.create_type();
                Label = "";
                etype.Type1 = "";
                etype.resultant = "";
                icg_chk = 1; // Console.WriteLine(Label)

                i++;
                if (AE(cc, scope))
                {
                    if (OE2(cc, scope))
                    {
                        //                      Console.WriteLine("OE2 fun true");
                        return true;
                    }
                }
            }
            else if (Tokenset[i].classname == ";" || Tokenset[i].classname == ":" || Tokenset[i].classname == ")" || Tokenset[i].classname == ",")
            {
                //            Console.WriteLine("OE2 fun true");
                return true;
            }
            else
            {
                //          Console.WriteLine("OE2 fun false");
                return false;
            }
            return false;
        }

        public bool AE(List<symbol_table> cc, int scope)
        {
            bool chk = false;

            //    Console.WriteLine(Tokenset[i].classname + "AE constant");
            if (Tokenset[i].classname == "PLUSMINUS" || Tokenset[i].classname == "ID" || Tokenset[i].classname == "Integer_constant" || Tokenset[i].classname == "Float_constant" || Tokenset[i].classname == "String_constant"
               || Tokenset[i].classname == "Char_constant" || Tokenset[i].classname == "!" || Tokenset[i].classname == "IncDec_Operator" || Tokenset[i].classname == "TF"
                 || Tokenset[i].classname == "Po*" || Tokenset[i].classname == "&" || Tokenset[i].classname == "(" || Tokenset[i].classname == "Super" || Tokenset[i].classname == "Ref")
            {

                //      Console.WriteLine(Tokenset[i].classname + "AE ck andr");
                if (ROPE(cc, scope))
                {

                    //        Console.WriteLine(Tokenset[i].classname + "rope ck andr");
                    if (AE2(cc, scope))
                    {
                        //          Console.WriteLine(Tokenset[i].classname + "AE2 ck andr");

                        //        Console.WriteLine("AE fun true");
                        return true;
                    }
                }

            }
            else
            {
                //                Console.WriteLine("AE fun false");
                return false;
            }
            return false;
        }
        public bool AE2(List<symbol_table> cc, int scope)
        {
            bool chk = false;
            if (Tokenset[i].classname == "And")
            {
                //etype.op = Tokenset[i].classname;
                //Label = Label + Tokenset[i].value;
                rel_op = Tokenset[i].classname;
                rel_label = Label + Tokenset[i].value;
                rel_type = etype.resultant;
                etype.clear();
                // temp = icg.create_type();
                Label = "";
                etype.Type1 = "";
                etype.resultant = "";
                icg_chk = 1; // Console.WriteLine(Label)
                i++;
                if (ROPE(cc, scope))
                {
                    if (AE2(cc, scope))
                    {
                        //                      Console.WriteLine("AE2 fun true");
                        return true;
                    }
                }
            }
            else if (Tokenset[i].classname == ";" || Tokenset[i].classname == ")" || Tokenset[i].classname == ":" || Tokenset[i].classname == "," || Tokenset[i].classname == "OR")
            {
                //            Console.WriteLine("AE2 fun true");
                return true;
            }
            else
            {
                //          Console.WriteLine("AE2 fun false");
                return false;
            }
            return false;
        }
        public bool ROPE(List<symbol_table> cc, int scope)
        {
            bool chk = false;
            if (Tokenset[i].classname == "PLUSMINUS" || Tokenset[i].classname == "ID" || Tokenset[i].classname == "Integer_constant" || Tokenset[i].classname == "Float_constant" || Tokenset[i].classname == "String_constant"
               || Tokenset[i].classname == "Char_constant" || Tokenset[i].classname == "!" || Tokenset[i].classname == "IncDec_Operator" || Tokenset[i].classname == "TF"
                || Tokenset[i].classname == "Po*" || Tokenset[i].classname == "&" || Tokenset[i].classname == "(" || Tokenset[i].classname == "Super" || Tokenset[i].classname == "Ref")
            {
                if (E(cc, scope))
                {
                    if (ROPE2(cc, scope))
                    {
                        //                Console.WriteLine("ROPE fun true");
                        return true;
                    }
                }

            }
            else
            {
                //      Console.WriteLine("ROPE fun false");
                return false;
            }
            return false;
        }
        public bool ROPE2(List<symbol_table> cc, int scope)
        {
            bool chk = false;
            //  string temp = "";
            if (Tokenset[i].classname == "Relational_Operator")
            {
                // etype.op = Tokenset[i].classname;
                //Label = Label + Tokenset[i].value;
                rel_op = Tokenset[i].classname;
                rel_label = Label + Tokenset[i].value;
                rel_type = etype.resultant;
                etype.clear();
                // temp = icg.create_type();
                Label = "";
                etype.Type1 = "";
                etype.resultant = "";
                icg_chk = 1; // Console.WriteLine(Label)
                i++;

                // Console.WriteLine(rel_label+"->");
                if (E(cc, scope))
                {
                    if (ROPE2(cc, scope))
                    {

                        //            Console.WriteLine("ROPE2 fun true");
                        return true;
                    }
                }
            }
            else if (Tokenset[i].classname == "And" || Tokenset[i].classname == ";" || Tokenset[i].classname == ":" || Tokenset[i].classname == ")" || Tokenset[i].classname == "," || Tokenset[i].classname == "OR")
            {
                //  Console.WriteLine("ROPE2 fun true");
                Label = Label + temp;
                return true;
            }
            else
            {
                //Console.WriteLine("ROPE2 fun false");
                return false;
            }
            return false;
        }
        public bool E(List<symbol_table> cc, int scope)
        {
            bool chk = false;

            //          Console.WriteLine(Tokenset[i].classname + "e k andr");

            if (Tokenset[i].classname == "PLUSMINUS" || Tokenset[i].classname == "ID" || Tokenset[i].classname == "Integer_constant" || Tokenset[i].classname == "Float_constant" || Tokenset[i].classname == "String_constant"
               || Tokenset[i].classname == "Char_constant" || Tokenset[i].classname == "!" || Tokenset[i].classname == "IncDec_Operator" || Tokenset[i].classname == "TF"
                || Tokenset[i].classname == "Po*" || Tokenset[i].classname == "&" || Tokenset[i].classname == "(" || Tokenset[i].classname == "Super" || Tokenset[i].classname == "Ref")
            {
                //                Console.WriteLine("e k ande");
                if (T(cc, scope))
                {
                    if (E2(cc, scope))
                    {
                        //                  Console.WriteLine("e2 k ande");
                        //         Console.WriteLine("E fun true");
                        return true;
                    }
                }

            }
            else
            {
                //   Console.WriteLine("E falr=> " + Tokenset[i].classname);
                //    Console.WriteLine("E fun false");
                return false;
            }
            return false;
        }
        public bool E2(List<symbol_table> cc, int scope)
        {
            bool chk = false;
            // Console.WriteLine("plsminus k ansdr" + Tokenset[i].classname);
            if (Tokenset[i].classname == "PLUSMINUS")
            {
                etype.op = Tokenset[i].value;
                Label = Label + Tokenset[i].value;
                i++;
                //Console.WriteLine("plsminus k ansdr" + Tokenset[i].classname);
                if (T(cc, scope))
                {

                    //      Console.WriteLine("plsminus k T() k ansdr" + Tokenset[i].classname);
                    if (E2(cc, scope))
                    {
                        //            Console.WriteLine("E2 fun true");
                        return true;
                    }
                }
            }
            else if (Tokenset[i].classname == "Relational_Operator" || Tokenset[i].classname == "And" || Tokenset[i].classname == ";" || Tokenset[i].classname == ":" || Tokenset[i].classname == ")" || Tokenset[i].classname == "," || Tokenset[i].classname == "OR")
            {
                //            Console.WriteLine("E2 fun true");

                return true;
            }
            else
            {
                // Console.WriteLine("E2 fun false");
                return false;
            }
            return false;
        }
        public bool T(List<symbol_table> cc, int scope)
        {
            bool chk = false;
            // Console.WriteLine("T k ansdr" + Tokenset[i].classname);
            if (Tokenset[i].classname == "PLUSMINUS" || Tokenset[i].classname == "ID" || Tokenset[i].classname == "Integer_constant" || Tokenset[i].classname == "Float_constant" || Tokenset[i].classname == "String_constant"
               || Tokenset[i].classname == "Char_constant" || Tokenset[i].classname == "!" || Tokenset[i].classname == "IncDec_Operator" || Tokenset[i].classname == "TF"
            || Tokenset[i].classname == "Po*" || Tokenset[i].classname == "&" || Tokenset[i].classname == "(" || Tokenset[i].classname == "Super" || Tokenset[i].classname == "Ref")
            {
                if (F(cc, scope))
                {

                    // Console.WriteLine("etyp1 plum" + etype.Type1 + "Etyp2" + etype.type2);
                    if (etype.type2 != "")
                    {
                        icg_chk = 1;
                        temp = icg.create_type();
                        icg.Write(Label, temp);
                        Label = "" + temp;
                        etype.resultant = Semantic.compatibility(etype.Type1, etype.type2, etype.op);
                    }
                    else
                    {

                        etype.resultant = etype.Type1;
                    }
                    etype.set(etype, etype.resultant);

                    // Console.WriteLine(Label + "-> t mai label");
                    if (T2(cc, scope))
                    {
                        //                    Console.WriteLine("T fun true");
                        return true;
                    }
                }

            }
            else
            {
                //          Console.WriteLine("E fun false");
                if (etype.type2 != "")
                {
                    temp = icg.create_type();
                    icg.Write(Label, temp);
                    Label = "" + temp;
                    etype.resultant = Semantic.compatibility(etype.Type1, etype.type2, etype.op);
                }
                else
                {

                    etype.resultant = etype.Type1;
                }
                etype.set(etype, etype.resultant);
                return false;
            }
            return false;
        }


        public bool T2(List<symbol_table> cc, int scope)
        {
            bool chk = false;
            if (Tokenset[i].classname == "MDM")
            {
                if (Tokenset[i].classname == "MDM")
                {
                    etype.op = Tokenset[i].classname;
                    Label = Label + Tokenset[i].value;
                    i++;
                    if (F(cc, scope))
                    {
                        // Console.WriteLine("etyp1 mdm"+etype.Type1+"Etyp2"+etype.type2);
                        if (etype.type2 != "")
                        {
                            etype.resultant = Semantic.compatibility(etype.Type1, etype.type2, etype.op);
                            temp = icg.create_type();
                            icg.Write(Label, temp);
                            Label = "" + temp;
                            icg_chk = 1;
                        }
                        else
                        {
                            etype.resultant = etype.Type1;
                        }
                        etype.set(etype, etype.resultant);

                        if (T2(cc, scope))
                        {
                            //        Console.WriteLine("T2 fun true");
                            return true;
                        }
                    }
                }
            }
            else
                if (Tokenset[i].classname == "PLUSMINUS" || Tokenset[i].classname == ":" || Tokenset[i].classname == "Relational_Operator" || Tokenset[i].classname == "And" || Tokenset[i].classname == ";" || Tokenset[i].classname == ")" || Tokenset[i].classname == "," || Tokenset[i].classname == "OR")
                {  //     Console.WriteLine(Tokenset[i].classname + "T2");
                    //Console.WriteLine("T2 fun true");
                    // if (Label != "")
                    //{
                    //                        temp = icg.create_type();
                    //
                    temp = Label;
                    // if(etype.Type1==null)
                    //     icg.Write(Label, temp);
                    return true;
                }
                else
                {
                    //   Console.WriteLine("T2 fun falsw");

                    return false;
                }
            return false;
        }

        public bool CONST(List<symbol_table> cc, int scope)
        {
            // Console.WrciteLine("constant fiun->" + Tokenset[i].classname);
            if (Tokenset[i].classname == "Integer_constant")
            {
                Label = Label + Tokenset[i].value;

                type = "Number";
                Semantic.set_type(etype, type);
                i++;
                //      Console.WriteLine("CONSt fun true");
                return true;
            }
            else if (Tokenset[i].classname == "Float_constant")
            {
                Label = Label + Tokenset[i].value;

                type = "Point";
                Semantic.set_type(etype, type);
                i++;
                //Console.WriteLine("CONSt fun true point");
                return true;
            }
            else if (Tokenset[i].classname == "Char_constant")
            {
                Label = Label + Tokenset[i].value;

                type = "Letter";
                Semantic.set_type(etype, type);
                i++;
                //        Console.WriteLine("CONSt fun true");
                return true;
            }
            else if (Tokenset[i].classname == "String_constant")
            {
                Label = Label + Tokenset[i].value;

                type = "Alpha";
                Semantic.set_type(etype, type);
                i++;
                //      Console.WriteLine("CONSt fun true");
                return true;
            }

            else if (Tokenset[i].classname == "PLUSMINUS")
            {
                Label = Label + Tokenset[i].value;

                i++;
                if (INT_ID(cc, scope))
                {
                    //    Console.WriteLine("CONSt fun true ");
                    return true;
                }
            }

            else
            {
                //  Console.WriteLine("CONSt fun false");
                return false;
            }
            return false;
        }
        bool INT_ID(List<symbol_table> cc, int scope)
        {
            if (Tokenset[i].classname == "ID")
            {
                //Console.WriteLine(scope);
                Label = Label + Tokenset[i].value;

                type = Semantic.Lookup_scope(cc, fn, Tokenset[i].value, scope, 1);
                if (type != null)
                    Semantic.set_type(etype, type);
                else if (type == "")
                {
                    Console.WriteLine(Semantic.counter + " )  => Variable is not assigned");
                    Semantic.counter++;
                }
                i++;
                return true;
            }
            else if (Tokenset[i].classname == "Integer_constant")
            {
                Label = Label + Tokenset[i].value;

                type = "Number";
                Semantic.set_type(etype, type);
                i++;
                return true;
            }
            else if (Tokenset[i].classname == "Float_constant")
            {
                Label = Label + Tokenset[i].value;

                type = "Point";
                Semantic.set_type(etype, type);
                i++;
                return true;
            }

            else return false;
        }
        //chk
        //chnge

        //object dec CHK KRNI H
        public bool OBJECT2(List<symbol_table> cc, int scope)
        {
            bool chk = false;
            //Console.WriteLine(Tokenset[i].classname);
            if (Tokenset[i].classname == ",")
            {
                if (Tokenset[i].classname == ",")
                {
                    i++;
                    if (Tokenset[i].classname == "ID")
                    {
                        //                        Ct_temp.name = Tokenset[i].value;
                        st_temp.name = Tokenset[i].value;
                        fn_temp.name = Tokenset[i].value;
                        i++;
                        if (Tokenset[i].classname == "=")
                        {
                            i++;
                            if (Tokenset[i].classname == "Invoke")
                            {
                                i++;
                                if (Tokenset[i].classname == "ID")
                                {
                                    string temp_name2 = Tokenset[i].value;
                                    i++;
                                    if (Tokenset[i].classname == "(")
                                    {
                                        i++;
                                        if (PARM(cc, scope, p_stack, p_type))
                                        {
                                            if (Tokenset[i].classname == ")")
                                            {
                                                i++;
                                                if (Semantic.compatibility(ct, st_temp, temp_name2, PL2) == true)
                                                {
                                                    if (check_s == 1)
                                                    {
                                                        st_temp.assign = true;
                                                        st_temp.class_name = temp_name;
                                                        Semantic.insertion(cc, st_temp);
                                                    }
                                                    else
                                                        if (check_fn == 1)
                                                        {
                                                            fn_temp.assign = true;
                                                            Semantic.insertion_fn(fn, fn_temp);
                                                        }
                                                    check_s = check_fn = 0;
                                                }

                                                if (OBJECT2(cc, scope))
                                                {
                                                    //                                      Console.WriteLine("OBJECT2 fun is true");
                                                    return true;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                }
            }
            else if (Tokenset[i].classname == ";")
            {
                i++;
                //Console.WriteLine("OBJECT2 fun is true");
                return true;
            }

            else
            {
                //                Console.WriteLine("OBJECT2 fun is false");
                chk = false;
            }
            return chk;

        }
        //inh chlri h
        /*        public bool INH()
                {
                    bool chk = false;
                    if (Tokenset[i].classname == "Extend")//first
                    {
                        i++;
                        if (Tokenset[i].classname == "ID")
                        {
                            i++;
                            //                  Console.WriteLine("inh true");
                            return true;
                        }
                    }
                    else if (Tokenset[i].classname == "{")//Follow
                    {
                        //            Console.WriteLine("inh follow true");
                        return true;
                    }
                    else
                    {
                        //          Console.WriteLine("inh false");
                        chk = false;
                    }
                    return chk;
                }
                */
        public bool INH(class_table temp)
        {
            bool chk = false;
            if (Tokenset[i].classname == "Extend")//first
            {
                i++;
                if (Tokenset[i].classname == "ID")
                {
                    //  Semantic.chk_inh(temp);

                    if (Semantic.Lookup(ct, Tokenset[i].value, 1) != null)
                        Ct_temp.xtends = Tokenset[i].value;

                    i++;
                    //    Console.WriteLine("ajj"+Ct_temp.xtends);
                    return true;
                }
            }
            else if (Tokenset[i].classname == "{")//Follow
            {
                //            Console.WriteLine("inh follow true");
                return true;
            }
            else
            {
                //          Console.WriteLine("inh false");
                chk = false;
            }
            return chk;
        }
        public bool ID_P2(List<symbol_table> cc, int scope)
        {
            bool chk = false;
            fn_temp.scope = scope;

            if (Tokenset[i].classname == "ID")
            {
                //PL = PL +" "+ Tokenset[i].value;
                //   fn_temp.name = Tokenset[i].value;
                st_temp.name = Tokenset[i].value;
                fn_temp.name = Tokenset[i].value;
                //  Semantic.insertion_fn(fn, fn_temp);
                i++;
                return true;
            }
            else if (Tokenset[i].classname == "Po*")
            {
                PL = Tokenset[i].value + PL;
                //  fn_temp.type = Tokenset[i].value + fn_temp.type;

                st_temp.PO = Tokenset[i].value;
                fn_temp.Po = Tokenset[i].value;
                Po_chk = 1;
                i++;
                if (PO3(cc, scope, 1))
                {
                    if (Tokenset[i].classname == "ID")
                    {
                        PL = PL + Tokenset[i].value;
                        fn_temp.name = Tokenset[i].value;
                        //        Semantic.insertion_fn(fn, fn_temp);
                        i++;
                        return true;
                    }
                }
            }


            else
                chk = false;
            return chk;

        }
        public bool PO3(List<symbol_table> cc, int scope, int chk)
        {
            if (Tokenset[i].classname == "Po*")
            {
                if (chk == 0)
                {
                    st_temp2.type = Tokenset[i].value + st_temp.type;
                    st_temp2.PO = Tokenset[i].value + st_temp2.PO;
                }
                PL = Tokenset[i].value + PL;
                //   fn_temp.type = Tokenset[i].value + fn_temp.type;
                if (chk == 1)
                {
                    fn_temp.Po = Tokenset[i].value + fn_temp.Po;
                    st_temp.PO = Tokenset[i].value + st_temp.PO;
                }
                Label = Label + "+" + Tokenset[i].value;
                i++;
                return true;
            }
            else if (Tokenset[i].classname == "ID")
                return true;
            else return false;
            return false;
        }
        public bool INIT(List<symbol_table> cc, int scope)
        {
            bool chk = false;

            if (Tokenset[i].classname == "=")
            {
                i++;
                if (EXP(cc, scope))
                {
                    if (etype.resultant != "")
                    {
                        // Console.WriteLine(fn_temp.type+"->"+etype.resultant);
                        //  Console.WriteLine(check_fn + "*" + check_s+"etype = "+etype.resultant);
                        if (check_s == 1 && Po_chk == 1 && Semantic.chk_compatibility(st_temp.ret, etype.resultant) == true)
                        {
                            st_temp.assign = true;
                            st_temp.type = st_temp.PO + st_temp.ret;
                            Semantic.insertion(cc, st_temp);
                            icg.Write(temp, st_temp.name);
                        }
                        else if (check_s == 1 && Po_chk == 0 && Semantic.chk_compatibility(st_temp.type, etype.resultant) == true)
                        {
                            st_temp.assign = true;
                            Semantic.insertion(cc, st_temp);
                            icg.Write(temp, st_temp.name);
                        }
                        else if (check_fn == 1 && Po_chk == 1 && Semantic.chk_compatibility(fn_temp.ret, etype.resultant) == true)
                        {
                            fn_temp.assign = true;

                            fn_temp.type = fn_temp.Po + fn_temp.type;
                            Semantic.insertion_fn(fn, fn_temp);
                            icg.Write(temp, fn_temp.name);
                        }

                        else if (check_fn == 1 && Po_chk == 0 && Semantic.chk_compatibility(fn_temp.type, etype.resultant) == true)
                        {
                            fn_temp.assign = true;
                            Semantic.insertion_fn(fn, fn_temp);
                            icg.Write(temp, fn_temp.name);
                        }
                        //                        else
                        //                          Console.WriteLine("variable is not declared");
                    }
                    //   else
                    // {
                    //   Console.WriteLine(Semantic.counter + " -> variable : " + st_temp2.name + " is not declared or unassigned variable is used");
                    // Semantic.counter++;
                    //}

                }

                //                else Console.WriteLine(fn_temp.name + " -> variable is not declared or unassigned variable is used");
                etype.clear();//error
                //check_s = check_fn = Po_chk = 0;


                //Console.WriteLine("true hy"+check_fn+"0>"+fn_temp.assign+"->"+fn_temp.name);
                return true;
            }


            else if (Tokenset[i].classname == ";" || Tokenset[i].classname == ",")
            {


                return true;
            }
            else

                chk = false;
            return chk;


        }

        public bool LIST(List<symbol_table> cc, int scope)
        {
            bool chk = false;
            if (Tokenset[i].classname == ";")
            {
                i++;
                if (check_s == 1 && st_temp.assign != true)
                {
                    Semantic.insertion(cc, st_temp);
                }
                else if (check_fn == 1 && fn_temp.assign != true)
                {
                    Semantic.insertion_fn(fn, fn_temp);
                }

                check_s = check_fn = Po_chk = 0;
                //check_fn = 0;
                //check_s = 0;
                return true;
            }

            else if (Tokenset[i].classname == ",")
            {

                if (check_s == 1 && st_temp.assign != true)
                {
                    Semantic.insertion(cc, st_temp);
                }
                else if (check_fn == 1 && fn_temp.assign != true)
                {
                    Semantic.insertion_fn(fn, fn_temp);
                }
                st_temp.assign = false;
                fn_temp.assign = false;

                i++;
                if (ID_P2(cc, scope))
                {
                    if (INIT(cc, scope))
                    {
                        if (LIST(cc, scope))
                        {
                            return true;
                        }
                    }
                }
            }

            else

                chk = false;
            return chk;
        }
        public bool WHILE(List<symbol_table> cc, int scope)
        {
            bool chk = false;

            if (Tokenset[i].classname == "While")
            {
                Label = icg.Create_label();
                icg.cont = "JMP  L" + icg.count;
                icg.add_string(Label);
                i++;
                if (Tokenset[i].classname == "(")
                {
                    i++;
                    if (EXP(cc, scope))
                    {
                        if (Tokenset[i].classname == ")")
                        {
                            //   Console.WriteLine(etype.resultant + "->whie");
                            if (etype.resultant != "Bool")
                            {
                                Console.WriteLine(Semantic.counter + ") While Loop=> Cannot convert " + etype.resultant + " to \"Bool\"");
                                Semantic.counter++;
                            }
                            icg.write3(temp);
                            icg.stop = icg.jmp_label();
                            icg.add_string(icg.stop);
                            icg.push(0);

                            //        icg.push();
                            i++;
                            if (BODY(cc, scope, 0, icg.stop, icg.cont))
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            else

                chk = false;
            return chk;
        }

        public bool SELECT_CC(List<symbol_table> cc, int scope)
        {
            bool chk = false;
            if (Tokenset[i].classname == "Select")
            {
                string Label4 = icg.Create_label();
                icg.stop = "JMP L" + icg.count;

                i++;
                Console.WriteLine(Label);
                if (Tokenset[i].classname == "(")
                {
                    i++;
                    if (EXP(cc, scope))
                    {
                        if (etype.resultant == "Bool")
                        {
                            Console.WriteLine(Semantic.counter + ")  SWITCH_CASE=> Cannot convert TYPE To  \"Bool\"  ");
                            Semantic.counter++;
                        }

                        icg.add_string("JMP L" + icg.count);
                        //    Console.WriteLine("exp sseclct true");
                        if (Tokenset[i].classname == ")")
                        {
                            i++;
                            //      Console.WriteLine(") sseclct true"+Tokenset[i].classname);

                            if (Tokenset[i].classname == "{")
                            {
                                i++;

                                scope = Semantic.createscope();
                                if (CC(cc, scope))
                                {
                                    if (Tokenset[i].classname == "}")
                                    {
                                        i++;

                                        icg.add_string(Label4);
                                        scope = Semantic.destroyscope();
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            else
            {
                //               Console.WriteLine("Select fun in false");
                chk = false;
            }
            return chk;


        }
        public bool CC(List<symbol_table> cc, int scope)
        {
            bool chk = false;

            //          Console.WriteLine("CC ka ndr ->" + Tokenset[i].classname);
            if (Tokenset[i].classname == "CC")
            {
                i++;
                if (EXP(cc, scope))
                {
                    // icg.add_string(icg.Create_label());
                    icg.write3(temp);
                    icg.add_string(icg.jmp_label());
                    // icg.push(1);

                    //Console.WriteLine("ep in cc true"+Tokenset[i].classname);
                    if (Tokenset[i].classname == ":")
                    {
                        i++;
                        if (Tokenset[i].classname == "{")
                        {
                            i++;
                            if (MST_SST(cc, scope))
                            {
                                if (STOP())
                                {
                                    if (Tokenset[i].classname == "}")
                                    {
                                        i++;
                                        if (CC2(cc, scope))
                                        {
                                            //                                                   Console.WriteLine("CC fun is true");
                                            return true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {    //Console.WriteLine("CC fun is false");
                chk = false;
            }
            return false;

        }
        public bool CC2(List<symbol_table> cc, int scope)
        {
            bool chk = false;
            if (Tokenset[i].classname == "CC" || Tokenset[i].classname == "Bydefault")
            {
                if (Tokenset[i].classname == "CC")
                {
                    i++;
                    if (EXP(cc, scope))
                    {

                        icg.add_string("L" + icg.count + ":");
                        icg.write3(temp);
                        icg.add_string(icg.jmp_label());

                        if (Tokenset[i].classname == ":")
                        {
                            i++;
                            if (Tokenset[i].classname == "{")
                            {
                                i++;
                                if (MST_SST(cc, scope))
                                {
                                    if (STOP())
                                    {
                                        if (Tokenset[i].classname == "}")
                                        {
                                            i++;
                                            if (CC2(cc, scope))
                                            {
                                                //             Console.WriteLine("CC2 fun is true");
                                                icg.add_string("L" + icg.count + " :");

                                                return true;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                }
                else if (Tokenset[i].classname == "Bydefault")
                {
                    // Label = icg.jmp_label();
                    //icg.add_string(Label);

                    i++;
                    if (Tokenset[i].classname == ":")
                    {
                        i++;
                        if (Tokenset[i].classname == "{")
                        {
                            i++;
                            if (MST_SST(cc, scope))
                            {
                                if (STOP())
                                {
                                    if (Tokenset[i].classname == "}")
                                    {
                                        i++;
                                        ///   Console.WriteLine("CC2 fun is true");
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }

            }
            else if (Tokenset[i].classname == "}")
            {
                //                Console.WriteLine("CC2 fun is true");
                return true;
            }
            else
            {
                //              Console.WriteLine("CC2 fun is false");
                chk = false;
            }
            return false;

        }
        public bool MST_SST(List<symbol_table> cc, int scope)
        {
            bool chk = false;
            if (Tokenset[i].classname == "While" || Tokenset[i].classname == "Agar" || Tokenset[i].classname == "LOOP" || Tokenset[i].classname == "Ret" || Tokenset[i].classname == "IncDec_Operator" ||
                Tokenset[i].classname == "ID" || Tokenset[i].classname == "DataType" || Tokenset[i].classname == "Select" || Tokenset[i].classname == "Super" || Tokenset[i].classname == "Ref" ||
                Tokenset[i].classname == "Cont" || Tokenset[i].classname == "Po*")
            {
                if (SST_EB(cc, scope))
                {
                    if (MST_SST(cc, scope))
                    {
                        //           Console.WriteLine("MST_SST fun is true");
                        return true;
                    }
                }
            }
            else if (Tokenset[i].classname == "Stop")
            {
                //                Console.WriteLine("MST_SST fun is true");
                return true;
            }
            else
            {
                //              Console.WriteLine("MST_SST fun is false");
                chk = false;
            }
            return chk;

        }
        public bool STOP()
        {
            bool chk = false;
            if (Tokenset[i].classname == "Stop")
            {
                i++;
                if (Tokenset[i].classname == ";")
                {
                    icg.add_string(icg.stop);
                    //                    Console.WriteLine(Tokenset[i].classname);
                    i++;
                    //                  Console.WriteLine("STOP fun is true");
                    return true;
                }
            }

            else
            {
                //            Console.WriteLine("STOP fun is false");
                chk = false;
            }
            return chk;
        }
        public bool CONT()
        {
            //            bool chk = false;
            if (Tokenset[i].classname == "Cont")
            {
                i++;
                if (Tokenset[i].classname == ";")
                {
                    icg.add_string(icg.cont);
                    i++;
                    //                    Console.WriteLine("Cont fun is true");
                    return true;
                }
            }

            //        else
            //          {
            //          Console.WriteLine("Cont fun is false");

            return false;
        }
        public bool CLASSBODY(List<symbol_table> cc, int scope)
        {
            bool chk = false;
            if (Tokenset[i].classname == "Access Modifier" || Tokenset[i].classname == "Fixed" || Tokenset[i].classname == "St" || Tokenset[i].classname == "DataType"
                || Tokenset[i].classname == "Devoid" || Tokenset[i].classname == "ID")//first
            {
                if (SUPER_CLASS_MEMBER(cc))
                {
                    //                Console.WriteLine(Tokenset[i].classname + "->super class ka andrr");
                    if (CLASSBODY(cc, scope))
                    {
                        //                  Console.WriteLine("Class BODY true");
                        return true;
                    }
                }
            }
            else if (Tokenset[i].classname == "}")//Follow
            {
                //        Console.WriteLine("Class BODY true");
                return true;
            }
            else
            {
                //      Console.WriteLine(Tokenset[i].classname + "clas body flse token");
                //    Console.WriteLine("Class BODY false");
                chk = false;
            }
            return chk;
        }
        public bool STATIC(class_table ct, symbol_table st, string chk)
        {
            if (Tokenset[i].classname == "St")
            {
                Semantic.setst(ct, st, chk, true);
                i++;
                //    Console.WriteLine("St fun is true");
                return true;
            }
            else if (Tokenset[i].classname == "Fixed" || Tokenset[i].classname == "DataType" || Tokenset[i].classname == "Devoid")
            {
                Semantic.setst(ct, st, chk, false);

                //  Console.WriteLine("St fun is true");
                return true;
            }
            else
            {
                //Console.WriteLine("St fun is false");
                return false;
            }
            return false;
        }
        public bool ID_PO_FID_P(List<symbol_table> cc)
        {
            bool chk = false;
            if (Tokenset[i].classname == "ID" || Tokenset[i].classname == "Po*")
            {
                if (Tokenset[i].classname == "ID")
                {
                    st_temp.name = Tokenset[i].value;
                    i++;
                    if (ID2(cc))
                    {
                        //      Console.WriteLine("ID_PO_FID_P fun true" + Tokenset[i].classname);
                        return true;
                    }
                }
                else if (Tokenset[i].classname == "Po*")
                {
                    Po_chk = 1;
                    st_temp.PO = Tokenset[i].value;
                    st_temp.type = Tokenset[i].value + st_temp.type;
                    // Console.WriteLine("Po* k andr iD_PO_FID->" + Po_type);
                    i++;
                    if (PO2(cc))
                    {
                        return true;
                    }
                }
            }
            else
            {
                chk = false;
            }
            return chk;
        }
        public bool PO2(List<symbol_table> cc)
        {
            bool chk = false;

            if (Tokenset[i].classname == "Po*")
            {
                st_temp.PO = Tokenset[i].value;
                st_temp.type = Tokenset[i].value + st_temp.type;
                i++;
                if (Tokenset[i].classname == "ID")
                {
                    st_temp.ret = st_temp.type;
                    st_temp.name = Tokenset[i].value;
                    i++;
                    if (ID2(cc))
                    {
                        //                      Console.WriteLine("PO2 fun is true");
                        return true;
                    }
                }
            }
            else if (Tokenset[i].classname == "ID")
            {
                st_temp.name = Tokenset[i].value;
                i++;
                if (ID2(cc))
                {
                    //                Console.WriteLine("PO2 fun is true");
                    return true;
                }
            }

            else
            {
                //          Console.WriteLine("PO2 fun is false");
                chk = false;
            }
            return chk;
        }
        public bool ID2(List<symbol_table> cc)
        {
            bool chk = false;
            if (Tokenset[i].classname == "=" || Tokenset[i].classname == ";" || Tokenset[i].classname == ",")
            {
                check_s = 1;
                if (INIT(cc, scope))
                {
                    if (LIST(cc, scope))
                    {
                        //                Console.WriteLine("ID2 fun is true");
                        return true;
                    }
                }

            }
            else if (Tokenset[i].classname == "(")
            {
                i++;

                scope = Semantic.createscope();
                string temp2 = st_temp.name;
                if (PARAM(cc, scope))
                {
                    if (Tokenset[i].classname == ")")
                    {
                        st_temp.name = temp2;

                        icg.write(st_temp, pl3);

                        st_temp.PL = PL;
                        //                        st_temp.ret = st_temp.type;
                        st_temp.type = PL + "->" + st_temp.type;
                        Console.WriteLine(st_temp.class_name);
                        i++;
                        if (Tokenset[i].classname == "{")
                        {
                            Semantic.insertion_fn(cc, st_temp);
                            i++;
                            if (MST_SST2(cc, scope))
                            {
                                if (RET(cc, scope, st_temp.ret))
                                {
                                    if (Tokenset[i].classname == "}")
                                    {
                                        i++;
                                        scope = Semantic.destroyscope();
                                        icg.add_string("END P");
                                        //                              Console.WriteLine("ID2 fun is true");
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                //    Console.WriteLine("ID2 fun is false");
                chk = false;
            }
            return chk;
        }
        public bool MST_SST2(List<symbol_table> cc, int scope)
        {
            bool chk = false;
            if (Tokenset[i].classname == "While" || Tokenset[i].classname == "Agar" || Tokenset[i].classname == "LOOP" || Tokenset[i].classname == "Stop" || Tokenset[i].classname == "IncDec_Operator" ||
                Tokenset[i].classname == "ID" || Tokenset[i].classname == "DataType" || Tokenset[i].classname == "Super" || Tokenset[i].classname == "Select" || Tokenset[i].classname == "Ref" ||
                Tokenset[i].classname == "Cont" || Tokenset[i].classname == "Po*")
            {
                if (SST_ER(cc, scope))
                {
                    if (MST_SST2(cc, scope))
                    {
                        //          Console.WriteLine("MST_SST2 fun is true");
                        return true;
                    }
                }
            }
            else if (Tokenset[i].classname == "Ret")
            {
                //Console.WriteLine("MST_SST2 fun is true");
                return true;
            }
            else
            {
                //Console.WriteLine("MST_SST2 fun is false");
                chk = false;
            }
            return chk;

        }
        public bool RET(List<symbol_table> cc, int scope, string ret)
        {
            bool chk = false;
            // Console.WriteLine("Ret fun k anw" + Tokenset[i].classname);
            if (Tokenset[i].classname == "Ret")
            {
                i++;
                //   Console.WriteLine("Ret fun++" + st_temp.ret+"0"+etype.resultant);
                if (EXP(cc, scope))
                {
                    if (etype.resultant != null)
                    {
                        if (Semantic.chk_compatibility(st_temp.ret, etype.resultant) != true)
                        {
                            Console.WriteLine(Semantic.counter + ") => Cannot implicitly convert type " + ret + " to " + etype.resultant);
                            Semantic.counter++;
                        }
                    }
                    /*else
                    { 
                        Console.WriteLine(Semantic.counter+") => Variable is not declared or unassigned variable is used");
                        Semantic.counter++;
                    }*/
                    etype.clear();//error

                    if (Tokenset[i].classname == ";")
                    {
                        i++;
                        return true;
                        //         Console.WriteLine("Ret fun " + Tokenset[i].classname);
                    }

                }
            }
            else
            {
                //                Console.WriteLine("Ret fun is false" + Tokenset[i].classname);
                return false;
            }
            //          Console.WriteLine("CHK RET");
            return false;
        }
        public bool PARM(List<symbol_table> cc, int scope, stack_s p_stack, stack_s p_type)
        {
            bool chk = false;
            icg.stack_count = 1;
            if (Tokenset[i].classname == "PLUSMINUS" || Tokenset[i].classname == "ID" || Tokenset[i].classname == "Integer_constant" || Tokenset[i].classname == "Float_constant" || Tokenset[i].classname == "String_constant"
                || Tokenset[i].classname == "Char_constant" || Tokenset[i].classname == "!" || Tokenset[i].classname == "IncDec_Operator" || Tokenset[i].classname == "TF"
                  || Tokenset[i].classname == "Po*" || Tokenset[i].classname == "&" || Tokenset[i].classname == "(" || Tokenset[i].classname == "Super" || Tokenset[i].classname == "Ref")
            {
                if (EXP(cc, scope))
                {
                    PL2 = etype.resultant;
                    p_type.push(PL2);
                    p_stack.push(temp);
                    if (PARM1(cc, scope, p_stack, p_type))
                    {
                        //                    Console.WriteLine("PARM fun is true");
                        return true;
                    }
                }
            }
            else if (Tokenset[i].classname == ")")
            {
                PL2 = "";
                //          Console.WriteLine("PARM fun is true");
                return true;
            }
            else
            {
                //        Console.WriteLine("PARM fun is false");
                chk = false;
            }
            return chk;
        }

        public bool PARM1(List<symbol_table> cc, int scope, stack_s p_stack, stack_s p_type)
        {
            bool chk = false;
            if (Tokenset[i].classname == ",")
            {
                icg.stack_count++;
                i++;
                if (EXP(cc, scope))
                {
                    //  PL2 = ;
                    PL2 = PL2 + "," + etype.resultant;
                    p_type.push(etype.resultant);
                    p_stack.push(temp);
                    if (PARM1(cc, scope, p_stack, p_type))
                    {
                        //                 Console.WriteLine("PARM1 fun is true"+PL2);
                        return true;
                    }
                }
            }
            else if (Tokenset[i].classname == ")")
            {
                //    Console.WriteLine("PARM1 fun is true");
                return true;
            }
            else
            {
                //  Console.WriteLine("PARM fun is false");
                chk = false;
            }

            return chk;
        }
        //yaha sai first follow dalo
        bool A2(List<symbol_table> cc, int scope)
        {
            if (Tokenset[i].classname == "Dot")
            {
                if (M3(cc, scope))
                {
                    return true;
                }
            }
            else if (Tokenset[i].classname == "(")
            {
                i++;
                if (PARM(cc, scope, p_stack, p_type))
                {
                    if (Tokenset[i].classname == ")")
                    {
                        i++;
                        if (M5(cc, scope))
                        {
                            return true;
                        }
                    }
                }
            }
            else if (Tokenset[i].classname == "IncDec_Operator")
            {
                i++;
                if (Tokenset[i].classname == ";")
                {
                    i++;
                    return true;
                }
            }
            else
            {
                return false;
            }
            return false;
        }
        bool M5(List<symbol_table> cc, int scope)
        {
            if (Tokenset[i].classname == ";")
            {
                i++;
                return true;
            }
            else
                if (Tokenset[i].classname == "Dot")
                {
                    if (M3(cc, scope))
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            return false;
        }
        bool M3(List<symbol_table> cc, int scope)
        {
            if (Tokenset[i].classname == "Dot")
            {
                i++;
                if (Tokenset[i].classname == "ID")
                {
                    i++;
                    if (A2(cc, scope))
                    {
                        return true;
                    }
                }
            }
            else
            {
                return false;
            }
            return false;
        }
        bool REF(List<symbol_table> cc, int scope)
        {
            if (Tokenset[i].classname == "Ref")
            {
                i++;
                if (Tokenset[i].classname == "Dot")
                {
                    i++;
                    if (Tokenset[i].classname == "ID")
                    {
                        check_s = 1;
                        st_temp.type = Semantic.Lookup(cc, Tokenset[i].value, 0);
                        st_temp.name = Tokenset[i].value;

                        Label = st_temp.type + "." + st_temp.name;

                        i++;
                        if (COMMON_M(cc, scope))
                        {
                            return true;
                        }

                    }
                }
            }
            else
            {
                return false;
            }
            return false;

        }
        bool COMMON_M(List<symbol_table> cc, int scope)
        {
            // temp_name = st_temp.name;
            string l2 = Label;

            if (Tokenset[i].classname == "Dot")
            {
                st_temp.type = Semantic.Lookup_scope(cc, fn, st_temp.name, scope, 0);
                //  Console.WriteLine("SST  calling dot true" + st_temp.type);
                fn_temp.type = st_temp.type;

                if (fn_temp.type != null || st_temp.type != null)
                    Label = Label + st_temp.name;
                l2 = Label;
                //   Console.WriteLine("SST  COMMON_M Dot true"+Label);
                if (M6(cc, scope))
                {
                    return true;
                }
            }
            else if (Tokenset[i].classname == "=" || Tokenset[i].classname == "Assignment_Operator")
            {
                if (inc_chk != 1)
                    st_temp.type = Semantic.Lookup_scope(cc, fn, st_temp.name, scope, 0);
                // Console.WriteLine("SST  calling dot true" + st_temp.type);
                inc_chk = 0;
                fn_temp.type = st_temp.type;

                if (ASSIGNMENT(cc, scope))
                {
                    return true;
                }
            }
            else if (Tokenset[i].classname == "(")
            {
                // if (inc_chk != 1)
                //   st_temp.type = Semantic.Lookup_scope(cc, fn, st_temp.name, scope, 0);
                //// Console.WriteLine("SST  calling dot true" + st_temp.type);
                //inc_chk = 0;

                fn_temp.type = st_temp.type;
                i++;

                param_tep.name = st_temp.name;
                param_tep.type = st_temp.type;
                int temp2 = icg.stack_count;
                icg.stack_count = 1;

                if (PARM(cc, scope, p_stack, p_type))
                {
                    if (Tokenset[i].classname == ")")
                    {

                        p_stack.counter = icg.stack_count;
                        icg.stack_count = temp2;
                        Label = icg.pop_stack(p_stack, p_type);

                        //  Label = l2 + "(" + ")" + PL2;

                        Label = "CALL " + l2 + "(" + ")" + "_" + Label + p_stack.counter;

                        temp = icg.create_type();
                        icg.Write(Label, temp);
                        Label = "" + temp;

                        i++;
                        {
                            //Console.WriteLine(st_temp.name + "98ka");
                            //  st_temp.name = temp_name;
                            if (COMMON_M2(cc, scope))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            else if (Tokenset[i].classname == "IncDec_Operator")
            {
                //   Console.WriteLine(st_temp.type + " ->"+st_temp.name);
                etype.op = Tokenset[i].value;
                if (super_chk == 1)
                {
                    st_temp.type = Semantic.Lookup(ct, st_temp.type, st_temp.name, 0);
                    super_chk = 0;    // st_temp.name = Tokenset[i].value;
                }


                if (etype.op == "++" && inc_chk != 1)
                {
                    st_temp.type = Semantic.Lookup_scope(cc, fn, st_temp.name, scope, 1);
                    Label = Label + fn_temp.name;
                    //inc_chk = 0; 
                }
                else if (etype.op == "--" && inc_chk != 1)
                {
                    st_temp.type = Semantic.Lookup_scope(cc, fn, st_temp.name, scope, 3);
                    Label = Label + st_temp.name;
                }
                inc_chk = 0;


                // st_temp.type = Semantic.Lookup_scope(cc, fn, st_temp.name, scope,1);
                if (st_temp.type != null)
                {
                    etype.resultant = Semantic.compatibility(st_temp.type, etype.op);
                    Semantic.set_type(etype, etype.resultant);
                    //       Console.WriteLine(Label);
                    st_temp.name = Label;
                    Label = Label + " + 1";
                    icg.Write(Label, st_temp.name);

                }
                //   else if (type == null)
                // {
                //  Console.WriteLine(Semantic.counter + " )  => Variable is not assigned");
                // Semantic.counter++;
                //}

                i++;
                {
                    if (Tokenset[i].classname == ";")
                    {
                        i++;
                        return true;
                    }
                }
            }
            else
            {
                return false;
            }
            return false;
        }
        bool COMMON_M2(List<symbol_table> cc, int scope)
        {
            //Console.WriteLine(st_temp.name);
            //      Console.WriteLine(PL2 + " -> pl2 "+check_fn+"f , s ->"+check_s+st_temp.name);
            if (Tokenset[i].classname == ";")
            {
                if (check_s == 1)
                    st_temp.type = Semantic.Lookup_const(ct, st_temp, PL2);
                else if (check_fn == 1)
                    fn_temp.type = Semantic.Lookup_fn(cc, st_temp, PL2);

                //  if (st_temp.type == null)
                //   Console.WriteLine("function is not declared !!"+ st_temp.type+"name"+st_temp.name);
                //else
                if (st_temp.type != "Devoid" && fn_temp.type != "Devoid")
                {
                    Console.WriteLine(Semantic.counter + ") => Function type is not Devoid !" + st_temp.type);
                    Semantic.counter++;
                }
                check_s = check_fn = 0;
                i++;
                return true;
            }
            else if (Tokenset[i].classname == "Dot")
            {
                //
                if (st_temp.type != null)
                    st_temp.class_name = st_temp.type;
                st_temp.type = Semantic.Lookup(ct, st_temp.type, Tokenset[i].value, 0);
                if (st_temp.type != null)
                    st_temp.name = Tokenset[i].value;
                //  Console.WriteLine("type class2" + st_temp.type + "->" + Tokenset[i].value);

                check_fn = 0;
                check_s = 1;

                i++;
                if (Tokenset[i].classname == "ID")
                {
                    i++;
                    if (COMMON_M(cc, scope))
                    {
                        return true;
                    }
                }
            }
            else
            {
                return false;
            }
            return false;
        }
        bool M6(List<symbol_table> cc, int scope)
        {
            if (Tokenset[i].classname == "Dot")
            {
                inc_chk = 1;

                i++;
                if (Tokenset[i].classname == "ID")
                {
                    //  Console.WriteLine("type class" + st_temp.type +"->"+ Tokenset[i].value);
                    if (st_temp.type != null)
                        st_temp.class_name = st_temp.type;
                    st_temp.type = Semantic.Lookup(ct, st_temp.type, Tokenset[i].value, 2);
                    if (st_temp.type != null)
                    {
                        st_temp.name = Tokenset[i].value;
                        //  Console.WriteLine("type class2" + st_temp.type + "->" + Tokenset[i].value);
                    } check_fn = 0;
                    check_s = 1;

                    Label = Label + "." + Tokenset[i].value;


                    i++;
                    if (COMMON_M(cc, scope))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        bool ASSIGNMENT(List<symbol_table> cc, int scope)
        {
            if (Tokenset[i].classname == "=")
            {
                i++;
                if (ASSIGN(cc, scope))
                {
                    return true;
                }
            }
            else
                if (Tokenset[i].classname == "Assignment_Operator")
                {
                    Op = Tokenset[i].value;
                    i++;
                    if (EXP(cc, scope))
                    {
                        //if (etype.resultant == null)
                        //    Console.WriteLine("variable is not declared or unassigned variable is used");
                        //else if (Semantic.Lookup(ct, st_temp.class_name, st_temp.name, 2) != null)
                        //{

                        //    icg.Write2(temp, st_temp.name, Op);
                        //    Semantic.chk_compatibility2(fn_temp.type, etype.resultant);
                        //}
                        //etype.clear();//error

                        if (etype.resultant == null)
                            Console.WriteLine("variable is not declared or unassigned variable is used");
                        else
                        {
                            //          fn_temp.type = Semantic.Lookup_scope(cc, fn, st_temp.name, scope, 2);
                            if (fn_temp.type != null)
                            {
                                if (Semantic.chk_compatibility(fn_temp.type, etype.resultant) == true)
                                    icg.Write2(temp, st_temp.name, Op);
                                //     icg.Write(temp, st_temp.name);
                            }
                        }
                        etype.clear();//error


                        if (Tokenset[i].classname == ";")
                        {
                            i++;
                            return true;
                        }
                    }
                }
            return false;
        }
        bool ASSIGN(List<symbol_table> cc, int scope)
        {
            if (Tokenset[i].classname == "PLUSMINUS" || Tokenset[i].classname == "ID" || Tokenset[i].classname == "Integer_constant" || Tokenset[i].classname == "Float_constant" || Tokenset[i].classname == "String_constant"
             || Tokenset[i].classname == "Char_constant" || Tokenset[i].classname == "!" || Tokenset[i].classname == "IncDec_Operator" || Tokenset[i].classname == "TF"
              || Tokenset[i].classname == "Po*" || Tokenset[i].classname == "&" || Tokenset[i].classname == "(" || Tokenset[i].classname == "Super" || Tokenset[i].classname == "Ref")
            {
                //     Console.WriteLine("assign k andr=" + st_temp.name);

                if (EXP(cc, scope))
                {
                    //  Console.WriteLine(etype.resultant+"=>resultant");
                    if (etype.resultant == null)
                        Console.WriteLine("variable is not declared or unassigned variable is used");
                    else
                    {
                        //          fn_temp.type = Semantic.Lookup_scope(cc, fn, st_temp.name, scope, 2);
                        if (fn_temp.type != null)
                        {
                            if (Semantic.chk_compatibility(fn_temp.type, etype.resultant) == true)
                                icg.Write(temp, st_temp.name);
                        }
                    }
                    etype.clear();//error

                    if (Tokenset[i].classname == ";")
                    {
                        i++;
                        return true;
                    }
                }
            }
            else if (Tokenset[i].classname == "Invoke")
            {
                check_fn = 1;
                if (NEW(cc, scope))
                {
                    return true;
                }
            }
            else return false;
            return false;
        }
        public bool SST(List<symbol_table> cc, int scope)
        {
            fn_temp.clear();
            Label = "";
            pl3 = "";
            PL2 = "";
            PL = "";
            if (Tokenset[i].classname == "While" || Tokenset[i].classname == "Agar" || Tokenset[i].classname == "LOOP" || Tokenset[i].classname == "Ret" || Tokenset[i].classname == "IncDec_Operator" ||
                 Tokenset[i].classname == "ID" || Tokenset[i].classname == "DataType" || Tokenset[i].classname == "Super" || Tokenset[i].classname == "Select" || Tokenset[i].classname == "Stop" || Tokenset[i].classname == "Ref" ||
                 Tokenset[i].classname == "Cont" || Tokenset[i].classname == "Po*")
            {
                //              Console.WriteLine(Tokenset[i].classname + " *SST k andr->");
                if (Tokenset[i].classname == "While")
                {
                    icg.push();

                    if (WHILE(cc, scope))
                    {
                        icg.cont = "JMP " + icg.cs_stack.pop();
                        icg.stop = "JMP " + icg.cs_stack.pop();


                        //                    Console.WriteLine("SST while true");
                        return true;
                    }
                }
                else if (Tokenset[i].classname == "Agar")
                {
                    if (AGR_WARNA(cc, scope))
                    {
                        //                  Console.WriteLine("SST agr wrna true");
                        return true;
                    }
                }
                else if (Tokenset[i].classname == "LOOP")
                {
                    icg.push();
                    if (LOOP(cc, scope))
                    {
                        icg.cont = icg.cs_stack.pop();
                        icg.stop = icg.cs_stack.pop();

                        //                Console.WriteLine("SST loop true");
                        return true;
                    }
                }
                else if (Tokenset[i].classname == "Po*")
                {
                    if (P(cc, scope))
                    {
                        //              Console.WriteLine("SST po true");
                        return true;
                    }
                }
                else if (Tokenset[i].classname == "IncDec_Operator")
                {
                    etype.op = Tokenset[i].classname;
                    i++;
                    if (Tokenset[i].classname == "ID")
                    {
                        if (etype.op == "++")
                            type = Semantic.Lookup_scope(cc, fn, Tokenset[i].value, scope, 1);
                        else
                            type = Semantic.Lookup_scope(cc, fn, Tokenset[i].value, scope, 3);

                        if (type != null)
                        {
                            etype.resultant = Semantic.compatibility(type, etype.op);
                            Semantic.set_type(etype, etype.resultant);
                            Label = Label + Tokenset[i].value + " + 1";
                        }
                        //    else if (type == "")
                        //  {
                        //    Console.WriteLine(Semantic.counter + " )  => Variable is not assigned");
                        //  Semantic.counter++;
                        //}

                        i++;
                        if (Tokenset[i].classname == ";")
                        {

                            i++;
                            //                Console.WriteLine("SST_EB true");
                            return true;
                        }
                        //eror;

                    }
                }
                else if (Tokenset[i].classname == "Select")
                {
                    if (SELECT_CC(cc, scope))
                    {
                        //          Console.WriteLine("SST seclect cc true");
                        return true;
                    }
                }
                else if (Tokenset[i].classname == "Super")
                {
                    i++;
                    if (Tokenset[i].classname == "Dot")
                    {
                        i++;
                        if (Tokenset[i].classname == "ID")
                        {
                            check_s = 1;
                            st_temp.type = Semantic.Lookup(ct, st_temp.class_name, 2);
                            st_temp.name = Tokenset[i].value;
                            //        Console.WriteLine("super.type->" + st_temp.type);
                            super_chk = 1;
                            /* if (st_temp.type != "object")
                            {
                                st_temp.type = Semantic.Lookup(ct, st_temp.type, Tokenset[i].value, 0);
                                st_temp.name = Tokenset[i].value;
                            }
                            else
                                Console.WriteLine("Class is not extended !!");
                           */
                            i++;
                            if (COMMON_M(cc, scope))
                            {
                                return true;
                            }
                        }
                    }
                }
                else if (Tokenset[i].classname == "ID")
                {
                    check_fn = 1;
                    st_temp.name = Tokenset[i].value;
                    st_temp.ret = Tokenset[i].value;
                    st_temp.name = Tokenset[i].value;
                    st_temp.type = Tokenset[i].value;
                    temp_name = st_temp.class_name;
                    //  if(Semantic.Lookup(ct,Tokenset[i].value,0)!=null)
                    //st_temp.class_name = Tokenset[i].value;
                    fn_temp.type = st_temp.type;
                    fn_temp.name = Tokenset[i].value;
                    i++;
                    // Label = Tokenset[i].value;
                    //  Console.WriteLine("kakdka");
                    if (CALLING(cc, scope))
                    {
                        //      Console.WriteLine("SST  calling true");
                        return true;
                    }
                }

                else if (Tokenset[i].classname == "DataType")
                {
                    fn_temp.type = Tokenset[i].value;
                    fn_temp.ret = Tokenset[i].value;
                    Po_type = Tokenset[i].value;
                    i++;

                    if (ID_PO(cc, scope))
                    {   //    Console.WriteLine("SST idpo true");
                        return true;

                    }
                }
                else if (Tokenset[i].classname == "Stop")
                {
                    if (STOP())
                    {
                        //  Console.WriteLine("SST stop true");
                        return true;
                    }
                }
                else if (Tokenset[i].classname == "Cont")
                {
                    if (CONT())
                    {
                        //Console.WriteLine("SSTcont  true");
                        return true;
                    }
                }
                else if (Tokenset[i].classname == "Ref")
                {

                    if (REF(cc, scope))
                    {
                        //                        Console.WriteLine("SST ref true");
                        return true;
                    }
                }
                else if (Tokenset[i].classname == "Ret")
                {
                    if (RET(cc, scope, st_temp.ret))
                    {
                        //                      Console.WriteLine("Ret fun ;" + Tokenset[i].classname);
                        return true;
                    }
                }
                //  else return false;
            }
            else
                return false;
            //        Console.WriteLine("SST false");
            //     chk = false;

            return false;
        }


        public bool CALLING(List<symbol_table> cc, int scope)
        {

            // Console.WriteLine(st_temp.name);
            if (Tokenset[i].classname == "Dot" || Tokenset[i].classname == "=" || Tokenset[i].classname == "Assignment_Operator" || Tokenset[i].classname == "(" || Tokenset[i].classname == "IncDec_Operator")
            {
                //
                check_fn = 1;
                if (COMMON_M(cc, scope))
                {
                    //            Console.WriteLine("commo_m true");
                    return true;
                }
            }
            else if (Tokenset[i].classname == "ID")
            {
                st_temp.name = Tokenset[i].value;
                fn_temp.name = Tokenset[i].value;
                //Console.WriteLine("kskskdd");
                i++;
                check_fn = 1;
                if (N1(cc, scope))
                {
                    return true;
                }
            }
            else return false;

            return false;
        }
        //public bool N1()
        //{
        //    if (Tokenset[i].classname == ";")
        //    {
        //        i++;
        //        return true;
        //    }
        //    else if (Tokenset[i].classname == "=")
        //    {
        //        i++;
        //        if (NEW())
        //        {
        //            return true;
        //        }
        //    }
        //    else return false;
        //    return false;
        //}
        public bool NEW(List<symbol_table> cc, int scope)
        {
            bool chk = false;
            PL2 = "";
            if (Tokenset[i].classname == "Invoke")
            {
                st_temp.class_name = st_temp.type;
                i++;
                if (Tokenset[i].classname == "ID")
                {
                    string temp_name2 = Tokenset[i].value;
                    //  st_temp.name = Tokenset[i].value;
                    i++;
                    param_tep.name = st_temp.name;
                    param_tep.type = st_temp.type;
                    int temp2 = icg.stack_count;
                    icg.stack_count = 1;

                    if (Tokenset[i].classname == "(")
                    {
                        i++;
                        if (PARM(cc, scope, p_stack, p_type))
                        {
                            if (Tokenset[i].classname == ")")
                            {

                                p_stack.counter = icg.stack_count;
                                icg.stack_count = temp2;
                                Label = icg.pop_stack(p_stack, p_type);
                                Label = "CALL " + st_temp.name + "_" + Label + p_stack.counter;
                                temp = icg.create_type();
                                icg.Write(Label, temp);
                                Label = "" + temp;

                                i++;
                                //      Console.WriteLine(st_temp.name +" "+ st_temp.class_name + " "+st_temp.type+"->"+PL2);
                                if (Semantic.compatibility(ct, st_temp, temp_name2, PL2) == true)
                                {
                                    //     st_temp.name = temp_name;
                                    if (check_s == 1)
                                    {
                                        st_temp.assign = true;
                                        st_temp.class_name = temp_name;
                                        Semantic.insertion(cc, st_temp);
                                    }
                                    else
                                        if (check_fn == 1)
                                        {
                                            fn_temp.scope = scope;
                                            fn_temp.type = temp_name2;
                                            fn_temp.assign = true;
                                            Semantic.insertion_fn(fn, fn_temp);
                                        }

                                    check_s = check_fn = 0;
                                }

                                if (OBJECT2(cc, scope))
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            else return false;
            return false;
        }

        public bool P(List<symbol_table> cc, int scope)
        {
            bool chk = false;
            if (Tokenset[i].classname == "Po*")
            {
                // fn_temp.type = Tokenset[i].value + st_temp.type;
                fn_temp.Po = Tokenset[i].value;
                i++;
                if (PO3(cc, scope, 1))
                {
                    if (Tokenset[i].classname == "ID")
                    {
                        Semantic.p_Lookup_scope(cc, fn, Tokenset[i].value, scope, 0, st_temp2.PO);
                        fn_temp.name = Tokenset[i].value;
                        i++;
                        if (COMMON_M(cc, scope))
                        {
                            //                  Console.WriteLine("P fun is true");
                            return true;
                        }
                    }
                }
            }

            else if (Tokenset[i].classname == "MDM" || Tokenset[i].classname == "PLUSMINUS" || Tokenset[i].classname == "Relational_Operator" || Tokenset[i].classname == "And" || Tokenset[i].classname == ";" || Tokenset[i].classname == ")" || Tokenset[i].classname == "," || Tokenset[i].classname == "OR")
            {
                //    Console.WriteLine("CommonID fun true");
                return true;
            }
            else
            {
                //  Console.WriteLine("P fun is falsw");
                chk = false;
            }
            return chk;
        }
        /*        bool P2(List<symbol_table>cc,int scope)
                {
                    if (Tokenset[i].classname == "=" || Tokenset[i].classname == "Assignment_Operator")
                    {
                        if (ASSIGNMNET_OPERATOR(cc, scope))
                        {
                            if (Tokenset[i].classname == ";")
                            {
                                i++;
                                return true;
                            }
                        }
                    }
                    else if (Tokenset[i].classname == "IncDec_Operator")
                    {
                        i++;
                        if (Tokenset[i].classname == ";")
                        {
                            i++;
                            return true;
                        }
                    }
                    else
                    {
                        return false;
                    }
                    return false;
                }
                */

        public bool ASSIGNMNET_OPERATOR(List<symbol_table> cc, int scope)
        {
            bool chk = true;
            if (Tokenset[i].classname == "=")
            {

                i++;
                if (EXP(cc, scope))
                {
                    loop_temp = fn_temp.name + "=" + temp;

                    if (etype.resultant == null)
                        Console.WriteLine("variable is not declared or unassigned variable is used");
                    else
                    {
                        //          fn_temp.type = Semantic.Lookup_scope(cc, fn, st_temp.name, scope, 2);
                        if (fn_temp.type != null)
                        {
                            if (Semantic.chk_compatibility(fn_temp.type, etype.resultant) == true)
                                icg.Write(temp, st_temp.name);
                        }
                    }
                    etype.clear();//error

                    //    Console.WriteLine("Assignment_Operator fun is true");
                    return true;
                }

            }

            else if (Tokenset[i].classname == "Assignment_Operator")
            {
                Op = Tokenset[i].value;
                i++;
                if (EXP(cc, scope))
                {

                    loop_temp = fn_temp.name + Op + temp;

                    if (etype.resultant == null)
                        Console.WriteLine("variable is not declared or unassigned variable is used");
                    else
                    {
                        //          fn_temp.type = Semantic.Lookup_scope(cc, fn, st_temp.name, scope, 2);
                        if (fn_temp.type != null)
                        {
                            if (Semantic.chk_compatibility(fn_temp.type, etype.resultant) == true)
                                icg.Write(temp, st_temp.name);
                        }
                    }
                    etype.clear();//error

                    // Console.WriteLine("Assignment_Operator fun is true");
                    return true;
                }
            }
            else
            {
                chk = false;
                ///                Console.WriteLine("Assignment_Operator fun is false");
            }
            return chk;
        }
        public bool ID_PO(List<symbol_table> cc, int scope)
        {
            bool chk = true;
            if (Tokenset[i].classname == "ID" || Tokenset[i].classname == "Po*")
            {
                check_fn = 1;

                if (ID_P2(cc, scope))
                {
                    if (INIT(cc, scope))
                    {
                        if (LIST(cc, scope))
                        {
                            //                         Console.WriteLine("ID_PO fun is true");
                            return true;
                        }
                    }
                }
            }
            else
            {
                //           Console.WriteLine("ID_PO fun is false");
                return false;
            }
            return false;
        }

        public bool F(List<symbol_table> cc, int scope)
        {
            bool chk = false;
            if (Tokenset[i].classname == "ID" || Tokenset[i].classname == "PLUSMINUS" || Tokenset[i].classname == "Integer_constant" || Tokenset[i].classname == "Float_constant" || Tokenset[i].classname == "String_constant"
               || Tokenset[i].classname == "Char_constant" || Tokenset[i].classname == "!" || Tokenset[i].classname == "IncDec_Operator" || Tokenset[i].classname == "TF"
                || Tokenset[i].classname == "Po*" || Tokenset[i].classname == "&" || Tokenset[i].classname == "(" || Tokenset[i].classname == "Super" || Tokenset[i].classname == "Ref")
            {
                if (Tokenset[i].classname == "ID")
                {
                    st_temp2.name = Tokenset[i].value;
                    //   Console.WriteLine(st_temp2.name+" => nkl");
                    Label = Label + Tokenset[i].value;
                    //   Console.WriteLine(st_temp2.type + "   -> f mai type");
                    i++;
                    if (COMMONID(cc, scope))
                    {
                        //                 Console.WriteLine("F fun true");
                        return true;
                    }
                }
                else if (Tokenset[i].classname == "PLUSMINUS" || Tokenset[i].classname == "Integer_constant" || Tokenset[i].classname == "Float_constant" || Tokenset[i].classname == "String_constant" || Tokenset[i].classname == "Char_constant")
                {
                    //Console.WriteLine("F fun const k andr");
                    if (CONST(cc, scope))
                    {
                        //     Console.WriteLine("F fun true");
                        return true;
                    }
                }
                else if (Tokenset[i].classname == "!")
                {
                    param_tep.name = st_temp2.name;
                    param_tep.type = st_temp2.type;

                    i++;
                    if (F(cc, scope))
                    {
                        st_temp2.name = param_tep.name;
                        st_temp2.type = param_tep.type;
                        //             Console.WriteLine("F fun true");
                        return true;
                    }
                }
                else if (Tokenset[i].classname == "IncDec_Operator")
                {

                    etype.op = Tokenset[i].classname;
                    i++;
                    if (Tokenset[i].classname == "ID")
                    {
                        //Console.WriteLine("skd");
                        Label = Label + Tokenset[i].value + " + 1";
                        if (etype.op == "++")
                            type = Semantic.Lookup_scope(cc, fn, Tokenset[i].value, scope, 1);

                        else
                            type = Semantic.Lookup_scope(cc, fn, Tokenset[i].value, scope, 3);

                        if (type != null)
                        {
                            etype.resultant = Semantic.compatibility(type, etype.op);
                            Semantic.set_type(etype, etype.resultant);
                        }
                        else if (type == "")
                        {
                            Console.WriteLine(Semantic.counter + " )  => Variable is not assigned");
                            Semantic.counter++;
                        }
                        //                 Console.WriteLine("F fun true");
                        return true;
                    }
                }
                else if (Tokenset[i].classname == "TF")
                {
                    etype.resultant = "Bool";
                    Semantic.set_type(etype, etype.resultant);
                    Label = Label + Tokenset[i].value;
                    i++;
                    //     Console.WriteLine("F fun true");
                    return true;
                }
                else if (Tokenset[i].classname == "Po*")//chngr kra h
                {
                    st_temp2.type = Tokenset[i].value;
                    st_temp2.PO = Tokenset[i].value;

                    Label = Label + Tokenset[i].value;

                    i++;
                    if (PO3(cc, scope, 0))
                    {
                        if (Tokenset[i].classname == "ID")
                        {
                            st_temp2.name = Tokenset[i].value;
                            st_temp2.type = Semantic.p_Lookup_scope(cc, fn, Tokenset[i].value, scope, 1, st_temp2.PO);
                            if (Po_type != null)
                                etype.resultant = st_temp2.type;
                            Semantic.set_type(etype, etype.resultant);
                            Label = Label + Tokenset[i].value;
                            i++;
                            if (COMMONID(cc, scope))
                            {
                                return true;
                            }
                        }
                    }
                }
                else if (Tokenset[i].classname == "&")
                {
                    i++;
                    if (Tokenset[i].classname == "ID")
                    {
                        Label = Label + Tokenset[i].value;

                        type = Semantic.Lookup_scope(cc, fn, Tokenset[i].classname, scope, 1);
                        if (type != null)
                            Semantic.set_type(etype, type);
                        else if (type == "")
                        {
                            Console.WriteLine(Semantic.counter + " )  => Variable is not assigned");
                            Semantic.counter++;
                        }
                        i++;
                        //     Console.WriteLine("F fun true");
                        return true;
                    }
                }
                else if (Tokenset[i].classname == "(")
                {
                    i++;
                    param_tep.name = st_temp2.name;
                    param_tep.type = st_temp2.type;

                    if (EXP(cc, scope))
                    {
                        st_temp2.name = param_tep.name;
                        st_temp2.type = param_tep.type;

                        if (Tokenset[i].classname == ")")
                        {
                            i++;
                            //       Console.WriteLine("F fun true");
                            return true;
                        }
                    }
                }
                else if (Tokenset[i].classname == "Ref")
                {
                    //                    Console.WriteLine(Tokenset[i].classname + "ref k andr");
                    i++;
                    if (Tokenset[i].classname == "Dot")
                    {
                        i++;
                        if (Tokenset[i].classname == "ID")
                        {
                            Label = Label + Tokenset[i].value;

                            st_temp2.type = Semantic.Lookup(cc, Tokenset[i].value, 2);
                            st_temp2.name = Tokenset[i].value;
                            Label = st_temp2.type + "." + Label;
                            Semantic.set_type(etype, st_temp2.type);
                            i++;
                            if (COMMONID(cc, scope))
                            {
                                //                              Console.WriteLine("F fun true");
                                return true;
                            }

                        }
                    }

                }
                else if (Tokenset[i].classname == "Super")
                {
                    i++;
                    if (Tokenset[i].classname == "Dot")
                    {
                        i++;
                        if (Tokenset[i].classname == "ID")
                        {
                            Label = Label + Tokenset[i].value;

                            st_temp2.type = Semantic.Lookup(ct, st_temp2.class_name, 2);
                            st_temp2.name = Tokenset[i].value;
                            if (st_temp2.type != "object")
                            {
                                type = Semantic.Lookup(ct, st_temp2.type, st_temp2.name, 1);
                                Semantic.set_type(etype, type);
                                //        Console.WriteLine("super.type->" + st_temp2.type + " type=" + type);
                            }
                            else
                            {
                                Console.WriteLine(Semantic.counter + ")  => Class is not extended !!");
                                Semantic.counter++;
                            }
                            super_chk = 1;
                            i++;
                            if (COMMONID(cc, scope))
                            {
                                //                            Console.WriteLine("F fun true");
                                return true;
                            }

                        }
                    }

                }

            }
            else
            {
                //          Console.WriteLine("F fun false");
                chk = false;
            }
            return chk;
        }
        //chk
        public bool COMMONID(List<symbol_table> cc, int scope)
        {
            bool chk = false;
            if (Tokenset[i].classname == "(")
            {
                i++;
                //etype.clear();
                param_tep.name = st_temp2.name;
                param_tep.type = st_temp2.type;
                int temp2 = icg.stack_count;
                icg.stack_count = 1;
                if (PARM(cc, scope, p_stack, p_type))   //Expression wla
                {
                    //etype.clear();
                    if (Tokenset[i].classname == ")")
                    {
                        p_stack.counter = icg.stack_count;
                        icg.stack_count = temp2;
                        st_temp2.name = param_tep.name;
                        st_temp2.type = param_tep.type;
                        //   Console.WriteLine(st_temp2.name + "comn id => "+ PL2);
                        st_temp2.type = Semantic.Lookup_fn(cc, st_temp2, PL2);
                        if (st_temp2.type == null)
                        {
                            Console.WriteLine(Semantic.counter + ") =>function is not declared !!");
                            Semantic.counter++;
                        }
                        else if (st_temp2.type != "Devoid" && st_temp2.type == null)
                        {
                            Console.WriteLine(Semantic.counter + ") =>Function should not return anything !)");
                            Semantic.counter++;
                        }
                        Label = icg.pop_stack(p_stack, p_type);
                        Label = "CALL " + st_temp2.name + "_" + Label + p_stack.counter;
                        temp = icg.create_type();
                        icg.Write(Label, temp);
                        Label = "" + temp;

                        // temp = icg.create_type();
                        //icg.Write(Label, temp);
                        etype.set(etype, st_temp2.type);
                        //     Console.WriteLine(etype.resultant);
                        i++;
                        //  if()             Console.WriteLine("CommonID brat) fun true");
                        if (A4(cc, scope))
                        {
                            return true;
                        }
                    }
                }
            }
            else if (Tokenset[i].classname == "Dot")
            {
                //      Console.WriteLine("CommonID DOt true");
                if (MULTIEXP(cc, scope))
                {
                    return true;
                }
            }
            else if (Tokenset[i].classname == "IncDec_Operator")
            {

                etype.op = Tokenset[i].value;
                if (Tokenset[i].value == "++")
                    type = Semantic.Lookup_scope(cc, fn, st_temp2.name, scope, 1);
                else
                    type = Semantic.Lookup_scope(cc, fn, st_temp2.name, scope, 3);
                if (type != null)
                {
                    etype.resultant = Semantic.compatibility(type, etype.op);
                    Semantic.set_type(etype, etype.resultant);
                    Label = Label + "+1";
                }
                else if (type == "")
                {
                    Console.WriteLine(Semantic.counter + " )  => Variable is not assigned");
                    Semantic.counter++;
                }
                i++;
                //    Console.WriteLine("CommonID inc dec fun true");
                return true;
            }
            else if (Tokenset[i].classname == "MDM" || Tokenset[i].classname == "PLUSMINUS" || Tokenset[i].classname == ":" || Tokenset[i].classname == "Relational_Operator" || Tokenset[i].classname == "And" || Tokenset[i].classname == ";" || Tokenset[i].classname == ")" || Tokenset[i].classname == "," || Tokenset[i].classname == "OR")
            {
                //                Console.WriteLine("CommonID fun true");
                //etype.set(etype, st_temp2.type);
                if (super_chk != 1)
                    etype.resultant = Semantic.Lookup_scope(cc, fn, st_temp2.name, scope, 1);
                //   Console.WriteLine(st_temp2.name + "etype ->" + etype.resultant);
                if (etype.resultant != "")
                    Semantic.set_type(etype, etype.resultant);
                super_chk = 0;
                return true;
            }
            else
            {
                //              Console.WriteLine("CommonID fun false");
                chk = false;
            }
            return chk;
        }
        //chnge kra h
        public bool MULTIEXP(List<symbol_table> cc, int scope)
        {

            bool chk = false;
            if (Tokenset[i].classname == "Dot")
            {
                st_temp2.type = Semantic.Lookup_scope(cc, fn, st_temp2.name, scope, 1);
                //     if (st_temp2.type != null)
                //{
                //Console.WriteLine(Semantic.counter + ") " + st_temp2.name + "  => Variable is not assigned");
                //  Semantic.counter++;
                //}
                // else
                if (st_temp2.type != null)
                    Semantic.set_type(etype, st_temp2.type);
                Label = Label + Tokenset[i].value;
                i++;
                if (Tokenset[i].classname == "ID")
                {
                    Label = Label + Tokenset[i].value;
                    if (st_temp2.type != null)
                        st_temp2.type = Semantic.Lookup(ct, st_temp2.type, Tokenset[i].value, 1);
                    if (st_temp2.type != null)
                        Semantic.set_type(etype, st_temp2.type);
                    i++;
                    if (A1(cc, scope))
                    {
                        //                        Console.WriteLine("MultiExp fun true");
                        return true;
                    }
                }
            }
            else
            {
                //                Console.WriteLine("MultiExp fun false");
                chk = false;
            }
            return chk;
        }
        //chk
        bool A1(List<symbol_table> cc, int scope)
        {
            param_tep.name = st_temp2.name;
            param_tep.type = st_temp2.type;

            int temp2 = icg.stack_count;
            icg.stack_count = 1;


            if (Tokenset[i].classname == "(")
            {
                i++;
                if (PARM(cc, scope, p_stack, p_type))
                {

                    st_temp2.name = param_tep.name;
                    st_temp2.type = param_tep.type;
                    if (Tokenset[i].classname == ")")
                    {
                        p_stack.counter = icg.stack_count;
                        icg.stack_count = temp2;

                        st_temp2.type = Semantic.Lookup_const(ct, st_temp2, PL2);
                        if (st_temp2.type == null)
                        {
                            Console.WriteLine(Semantic.counter + ") =>Function " + st_temp2.name + " is not declared !!");
                            Semantic.counter++;
                        }
                        else if (st_temp2.type == "Devoid")
                        {
                            Console.WriteLine(Semantic.counter + ") =>Function  " + st_temp2.name + " type -> Devoid (do not return anything !)");
                            Semantic.counter++;
                        }

                        Label = icg.pop_stack(p_stack, p_type);
                        Label = "CALL " + st_temp2.name + "_" + Label + p_stack.counter;

                        temp = icg.create_type();
                        icg.Write(Label, temp);
                        Label = "" + temp;

                        //check_s = check_fn = 0;
                        i++;
                        if (A4(cc, scope))
                        {
                            return true;
                        }
                    }
                }
            }
            else if (Tokenset[i].classname == "IncDec_Operator")
            {
                etype.op = Tokenset[i].value;
                if (Tokenset[i].value != "++" && st_temp2.sta == true)
                {
                    Console.WriteLine(Semantic.counter + " )" + st_temp2.name + " => Static data can not be changed, It can be incremented only");
                    Semantic.counter++;
                }//  type = Semantic.Lookup_scope(cc, fn, st_temp2.name, scope);
                if (st_temp2.type != null)
                {
                    etype.resultant = Semantic.compatibility(st_temp2.type, etype.op);
                    Semantic.set_type(etype, etype.resultant);
                    Label = Label + " + 1";
                }
                i++;
                return true;
            }
            else if (Tokenset[i].classname == "Dot")
            {
                if (A4(cc, scope))
                {
                    return true;
                }
            }
            else if (Tokenset[i].classname == "MDM" || Tokenset[i].classname == "PLUSMINUS" || Tokenset[i].classname == ":" || Tokenset[i].classname == "Relational_Operator" || Tokenset[i].classname == "And" || Tokenset[i].classname == ";" || Tokenset[i].classname == ")" || Tokenset[i].classname == "," || Tokenset[i].classname == "OR")
            {
                return true;
            }

            else return false;
            return false;
        }
        bool A4(List<symbol_table> cc, int scope)
        {
            //      Console.WriteLine("A4 k andr" + Tokenset[i].classname);
            if (Tokenset[i].classname == "Dot")
            {
                if (MULTIEXP(cc, scope))
                {
                    return true;
                }
            }
            else if (Tokenset[i].classname == "MDM" || Tokenset[i].classname == "PLUSMINUS" || Tokenset[i].classname == ":" || Tokenset[i].classname == "Relational_Operator" || Tokenset[i].classname == "And" || Tokenset[i].classname == ";" || Tokenset[i].classname == ")" || Tokenset[i].classname == "," || Tokenset[i].classname == "OR")
            {
                return true;
            }
            return false;
        }

        //      }
        public bool SUPER1(List<symbol_table> cc, int scope)
        {
            bool chk = false;
            st_temp.clear();
            if (Tokenset[i].classname == "Super")
            {
                i++;
                if (SUPER2(cc, scope))
                {
                    return true;
                }
            }
            else
                if (Tokenset[i].classname == "While" || Tokenset[i].classname == "Agar" || Tokenset[i].classname == "LOOP" || Tokenset[i].classname == "Ret"
              || Tokenset[i].classname == "LOOP" || Tokenset[i].classname == "IncDec_Operator" || Tokenset[i].classname == "DataType"
              || Tokenset[i].classname == "ID" || Tokenset[i].classname == "Select" || Tokenset[i].classname == "Ref" || Tokenset[i].classname == "Cont"
              || Tokenset[i].classname == "Po*" || Tokenset[i].classname == "Stop" || Tokenset[i].classname == "}")//first
                {
                    //            Console.WriteLine("Super1 fun is true");
                    return true;
                }
                else
                {
                    //          Console.WriteLine("Super1 fun is false");
                    chk = false;
                }
            return chk;
        }
        bool SUPER2(List<symbol_table> cc, int scope)
        {
            if (Tokenset[i].classname == "(")
            {
                i++;
                if (PARM(cc, scope, p_stack, p_type))
                {
                    if (Tokenset[i].classname == ")")
                    {
                        st_temp.type = Semantic.Lookup(ct, st_temp.class_name, 2);
                        //Console.WriteLine("super.type->" + st_temp.type);
                        // st_temp.name
                        if (st_temp.type != "object")
                        {
                            st_temp.class_name = st_temp.type;
                            st_temp.name = st_temp.type;
                            st_temp.type = Semantic.Lookup_const(ct, st_temp, PL2);
                            //        Console.WriteLine("super.type->" + st_temp.type);
                        }
                        else
                        {
                            Console.WriteLine(Semantic.counter + ") =>Class is not extended !!");
                            Semantic.counter++;
                        }
                        i++;
                        if (Tokenset[i].classname == ";")
                        {
                            i++;
                            //                Console.WriteLine("Super1 fun is true");
                            return true;
                        }
                    }
                }
            }
            else if (Tokenset[i].classname == "Dot")
            {
                i++;
                if (Tokenset[i].classname == "ID")
                {
                    st_temp.type = Semantic.Lookup(ct, st_temp.class_name, 0);
                    //Console.WriteLine(st_temp.type+"=>name");
                    if (st_temp.type != "object")
                    {
                        st_temp.type = Semantic.Lookup(ct, st_temp.type, Tokenset[i].value, 0);
                        Console.WriteLine("super.type->" + st_temp.type);
                    }
                    else
                    {
                        Console.WriteLine(Semantic.counter + ") =>Class is not extended !!");
                        Semantic.counter++;
                    }
                    i++;
                    if (COMMON_M(cc, scope))
                    {
                        return true;
                    }
                }
            }
            else
                return false;
            return false;

        }

        public bool SST_EB(List<symbol_table> cc, int scope)
        {
            //            bool chk = false;
            fn_temp.clear();
            Label = "";
            pl3 = "";
            PL2 = "";
            PL = "";
            if (Tokenset[i].classname == "While" || Tokenset[i].classname == "Agar" || Tokenset[i].classname == "LOOP" || Tokenset[i].classname == "Ret" || Tokenset[i].classname == "IncDec_Operator" ||
                Tokenset[i].classname == "ID" || Tokenset[i].classname == "DataType" || Tokenset[i].classname == "Select" || Tokenset[i].classname == "Super" || Tokenset[i].classname == "Ref" ||
                Tokenset[i].classname == "Cont" || Tokenset[i].classname == "Po*")
            {
                if (Tokenset[i].classname == "While")
                {
                    icg.push();

                    if (WHILE(cc, scope))
                    {
                        icg.cont = "JMP " + icg.cs_stack.pop();
                        icg.stop = "JMP " + icg.cs_stack.pop();


                        //                    Console.WriteLine("SST while true");
                        return true;
                    }
                }
                else if (Tokenset[i].classname == "Agar")
                {
                    if (AGR_WARNA(cc, scope))
                    {
                        //                  Console.WriteLine("SST agr wrna true");
                        return true;
                    }
                }
                else if (Tokenset[i].classname == "LOOP")
                {
                    icg.push();
                    if (LOOP(cc, scope))
                    {
                        icg.cont = icg.cs_stack.pop();
                        icg.stop = icg.cs_stack.pop();

                        //                Console.WriteLine("SST loop true");
                        return true;
                    }
                }

                /*   else if (RET())
                   {
                       Console.WriteLine("SST_EB true");
                       return true;
                   }
                  */
                else if (Tokenset[i].classname == "IncDec_Operator")
                {
                    etype.op = Tokenset[i].classname;
                    i++;
                    if (Tokenset[i].classname == "ID")
                    {
                        if (etype.op == "++")
                            type = Semantic.Lookup_scope(cc, fn, Tokenset[i].value, scope, 1);
                        else
                            type = Semantic.Lookup_scope(cc, fn, Tokenset[i].value, scope, 3);

                        if (type != null)
                        {
                            etype.resultant = Semantic.compatibility(type, etype.op);
                            Semantic.set_type(etype, etype.resultant);
                            Label = Label + Tokenset[i].value + " + 1";
                        }
                        //    else if (type == "")
                        //  {
                        //    Console.WriteLine(Semantic.counter + " )  => Variable is not assigned");
                        //  Semantic.counter++;
                        //}

                        i++;
                        if (Tokenset[i].classname == ";")
                        {

                            i++;
                            //                Console.WriteLine("SST_EB true");
                            return true;
                        }
                        //eror;

                    }
                }


                else if (Tokenset[i].classname == "ID")
                {
                    check_fn = 1;
                    st_temp.name = Tokenset[i].value;
                    st_temp.ret = st_temp.type;
                    st_temp.name = Tokenset[i].value;
                    st_temp.type = Tokenset[i].value;
                    temp_name = st_temp.class_name;
                    //  if(Semantic.Lookup(ct,Tokenset[i].value,0)!=null)
                    //st_temp.class_name = Tokenset[i].value;
                    fn_temp.type = st_temp.type;
                    fn_temp.name = Tokenset[i].value;
                    i++;
                    //  Console.WriteLine("kakdka");
                    if (CALLING(cc, scope))
                    {
                        //      Console.WriteLine("SST  calling true");
                        return true;
                    }
                }
                else if (Tokenset[i].classname == "DataType")
                {
                    fn_temp.type = Tokenset[i].value;
                    fn_temp.ret = Tokenset[i].value;
                    Po_type = Tokenset[i].value;
                    i++;

                    if (ID_PO(cc, scope))
                    {   //    Console.WriteLine("SST idpo true");
                        return true;

                    }
                }
                else if (Tokenset[i].classname == "Select")
                {
                    if (SELECT_CC(cc, scope))
                    {
                        //          Console.WriteLine("SST seclect cc true");
                        return true;
                    }
                }
                else if (Tokenset[i].classname == "Super")
                {
                    i++;
                    if (Tokenset[i].classname == "Dot")
                    {
                        i++;
                        if (Tokenset[i].classname == "ID")
                        {
                            check_s = 1;
                            st_temp.type = Semantic.Lookup(ct, st_temp.class_name, 2);
                            st_temp.name = Tokenset[i].value;
                            //        Console.WriteLine("super.type->" + st_temp.type);
                            super_chk = 1;


                            //if (st_temp.type != "object")
                            //{
                            //    st_temp.type = Semantic.Lookup(ct, st_temp.type, Tokenset[i].value, 0);
                            //    st_temp.name = Tokenset[i].value;
                            //    //   Console.WriteLine("super.type->" + st_temp.type);
                            //}
                            //else
                            //    Console.WriteLine("Class is not extended !!");
                            //i++;
                            if (COMMON_M(cc, scope))
                            {
                                return true;
                            }
                        }
                    }
                }
                else if (Tokenset[i].classname == "Cont")
                {
                    if (CONT())
                    {
                        //Console.WriteLine("SSTcont  true");
                        return true;
                    }
                }
                else if (Tokenset[i].classname == "Ref")
                {

                    if (REF(cc, scope))
                    {
                        //                        Console.WriteLine("SST ref true");
                        return true;
                    }
                }
                else if (Tokenset[i].classname == "Ret")
                {
                    if (RET(cc, scope, st_temp.ret))
                    {
                        //                      Console.WriteLine("Ret fun ;" + Tokenset[i].classname);
                        return true;
                    }
                }

                else if (Tokenset[i].classname == "Po*")
                {
                    if (P(cc, scope))
                    {
                        //              Console.WriteLine("SST po true");
                        return true;
                    }
                }

            }
            else
            {
                //        Console.WriteLine("SST_EB false");
                return false;
            }
            return false;

        }

        public bool SST_ER(List<symbol_table> cc, int scope)
        {
            bool chk = false;
            fn_temp.clear();
            Label = "";
            pl3 = "";
            PL2 = "";
            PL = "";
            if (Tokenset[i].classname == "While" || Tokenset[i].classname == "Agar" || Tokenset[i].classname == "LOOP" || Tokenset[i].classname == "Stop" || Tokenset[i].classname == "IncDec_Operator" ||
                Tokenset[i].classname == "ID" || Tokenset[i].classname == "DataType" || Tokenset[i].classname == "Super" || Tokenset[i].classname == "Select" || Tokenset[i].classname == "Ref" ||
                Tokenset[i].classname == "Cont" || Tokenset[i].classname == "Po*")
            {
                if (Tokenset[i].classname == "While")
                {
                    icg.push();

                    if (WHILE(cc, scope))
                    {
                        icg.cont = "JMP " + icg.cs_stack.pop();
                        icg.stop = "JMP " + icg.cs_stack.pop();


                        //                    Console.WriteLine("SST while true");
                        return true;
                    }
                }
                else if (Tokenset[i].classname == "Agar")
                {
                    if (AGR_WARNA(cc, scope))
                    {
                        //                  Console.WriteLine("SST agr wrna true");
                        return true;
                    }
                }
                else if (Tokenset[i].classname == "LOOP")
                {
                    icg.push();
                    if (LOOP(cc, scope))
                    {
                        icg.cont = icg.cs_stack.pop();
                        icg.stop = icg.cs_stack.pop();

                        //                Console.WriteLine("SST loop true");
                        return true;
                    }
                }

                else if (Tokenset[i].classname == "Stop")
                {
                    if (STOP())
                    {
                        //  Console.WriteLine("SST stop true");
                        return true;
                    }
                }
                else if (Tokenset[i].classname == "IncDec_Operator")
                {
                    etype.op = Tokenset[i].classname;
                    i++;
                    if (Tokenset[i].classname == "ID")
                    {
                        if (etype.op == "++")
                            type = Semantic.Lookup_scope(cc, fn, Tokenset[i].value, scope, 1);
                        else
                            type = Semantic.Lookup_scope(cc, fn, Tokenset[i].value, scope, 3);

                        if (type != null)
                        {
                            etype.resultant = Semantic.compatibility(type, etype.op);
                            Semantic.set_type(etype, etype.resultant);
                            Label = Label + Tokenset[i].value + " + 1";
                        }
                        //    else if (type == "")
                        //  {
                        //    Console.WriteLine(Semantic.counter + " )  => Variable is not assigned");
                        //  Semantic.counter++;
                        //}

                        i++;
                        if (Tokenset[i].classname == ";")
                        {

                            i++;
                            //                Console.WriteLine("SST_EB true");
                            return true;
                        }
                        //eror;

                    }
                }

                else if (Tokenset[i].classname == "ID")
                {
                    check_fn = 1;
                    st_temp.name = Tokenset[i].value;
                    st_temp.ret = st_temp.type;
                    st_temp.name = Tokenset[i].value;
                    st_temp.type = Tokenset[i].value;
                    temp_name = st_temp.class_name;
                    //  if(Semantic.Lookup(ct,Tokenset[i].value,0)!=null)
                    //st_temp.class_name = Tokenset[i].value;
                    fn_temp.type = st_temp.type;
                    fn_temp.name = Tokenset[i].value;
                    i++;
                    //  Console.WriteLine("kakdka");
                    if (CALLING(cc, scope))
                    {
                        //      Console.WriteLine("SST  calling true");
                        return true;
                    }
                }

                else if (Tokenset[i].classname == "DataType")
                {
                    fn_temp.type = Tokenset[i].value;
                    fn_temp.ret = Tokenset[i].value;
                    Po_type = Tokenset[i].value;
                    i++;

                    if (ID_PO(cc, scope))
                    {   //    Console.WriteLine("SST idpo true");
                        return true;

                    }
                }

                else if (Tokenset[i].classname == "Super")
                {
                    i++;
                    if (Tokenset[i].classname == "Dot")
                    {
                        i++;
                        if (Tokenset[i].classname == "ID")
                        {
                            check_s = 1;
                            st_temp.type = Semantic.Lookup(ct, st_temp.class_name, 2);
                            st_temp.name = Tokenset[i].value;
                            //        Console.WriteLine("super.type->" + st_temp.type);
                            super_chk = 1;



                            //if (st_temp.type != "object")
                            //{
                            //    st_temp.type = Semantic.Lookup(ct, st_temp.type, Tokenset[i].value, 0);
                            //    st_temp.name = Tokenset[i].value;
                            //    //   Console.WriteLine("super.type->" + st_temp.type);
                            //}
                            //else
                            //    Console.WriteLine("Class is not extended !!");
                            //
                            i++;
                            if (COMMON_M(cc, scope))
                            {
                                return true;
                            }
                        }
                    }
                }
                else if (Tokenset[i].classname == "Select")
                {
                    if (SELECT_CC(cc, scope))
                    {
                        //          Console.WriteLine("SST seclect cc true");
                        return true;
                    }
                }

                else if (Tokenset[i].classname == "Cont")
                {
                    if (CONT())
                    {
                        //Console.WriteLine("SSTcont  true");
                        return true;
                    }
                }
                else if (Tokenset[i].classname == "Ref")
                {

                    if (REF(cc, scope))
                    {
                        //                        Console.WriteLine("SST ref true");
                        return true;
                    }
                }
                else if (Tokenset[i].classname == "Po*")
                {
                    if (P(cc, scope))
                    {
                        //              Console.WriteLine("SST po true");
                        return true;
                    }
                }
            }
            else
            {
                //        Console.WriteLine("SST_ER2 false");
                return false;
            }
            return false;

        }


        public bool SST2(List<symbol_table> cc, int scope)
        {
            Label = "";
            fn_temp.clear();
            Label = "";
            pl3 = "";
            PL2 = "";
            PL = "";
            if (Tokenset[i].classname == "While" || Tokenset[i].classname == "Agar" || Tokenset[i].classname == "LOOP" || Tokenset[i].classname == "Ret" || Tokenset[i].classname == "IncDec_Operator" ||
                 Tokenset[i].classname == "ID" || Tokenset[i].classname == "DataType" || Tokenset[i].classname == "Select" || Tokenset[i].classname == "Stop" || Tokenset[i].classname == "Ref" ||
                 Tokenset[i].classname == "Cont" || Tokenset[i].classname == "Po*")
            {
                //      Console.WriteLine(Tokenset[i].classname + " *SST k andr->");
                if (Tokenset[i].classname == "While")
                {
                    icg.push();

                    if (WHILE(cc, scope))
                    {
                        icg.cont = "JMP " + icg.cs_stack.pop();
                        icg.stop = "JMP " + icg.cs_stack.pop();


                        //                    Console.WriteLine("SST while true");
                        return true;
                    }
                }
                else if (Tokenset[i].classname == "Agar")
                {
                    if (AGR_WARNA(cc, scope))
                    {
                        //                  Console.WriteLine("SST agr wrna true");
                        return true;
                    }
                }
                else if (Tokenset[i].classname == "LOOP")
                {
                    icg.push();
                    if (LOOP(cc, scope))
                    {
                        icg.cont = icg.cs_stack.pop();
                        icg.stop = icg.cs_stack.pop();

                        //                Console.WriteLine("SST loop true");
                        return true;
                    }
                }
                else if (Tokenset[i].classname == "Po*")
                {
                    if (P(cc, scope))
                    {
                        //              Console.WriteLine("SST po true");
                        return true;
                    }
                }
                else if (Tokenset[i].classname == "IncDec_Operator")
                {
                    etype.op = Tokenset[i].classname;
                    i++;
                    if (Tokenset[i].classname == "ID")
                    {
                        if (etype.op == "++")
                            type = Semantic.Lookup_scope(cc, fn, Tokenset[i].value, scope, 1);
                        else
                            type = Semantic.Lookup_scope(cc, fn, Tokenset[i].value, scope, 3);

                        if (type != null)
                        {
                            etype.resultant = Semantic.compatibility(type, etype.op);
                            Semantic.set_type(etype, etype.resultant);
                            Label = Label + Tokenset[i].value + " + 1";
                        }
                        //    else if (type == "")
                        //  {
                        //    Console.WriteLine(Semantic.counter + " )  => Variable is not assigned");
                        //  Semantic.counter++;
                        //}

                        i++;
                        if (Tokenset[i].classname == ";")
                        {

                            i++;
                            //                Console.WriteLine("SST_EB true");
                            return true;
                        }
                        //eror;

                    }
                }
                else if (Tokenset[i].classname == "Select")
                {
                    if (SELECT_CC(cc, scope))
                    {
                        //          Console.WriteLine("SST seclect cc true");
                        return true;
                    }
                }
                else if (Tokenset[i].classname == "ID")
                {
                    check_fn = 1;
                    st_temp.name = Tokenset[i].value;
                    st_temp.ret = st_temp.type;
                    st_temp.name = Tokenset[i].value;
                    st_temp.type = Tokenset[i].value;
                    temp_name = st_temp.class_name;
                    //  if(Semantic.Lookup(ct,Tokenset[i].value,0)!=null)
                    //st_temp.class_name = Tokenset[i].value;
                    fn_temp.type = st_temp.type;
                    fn_temp.name = Tokenset[i].value;
                    i++;
                    // Label = Label + Tokenset[i].value + " + 1";
                    //   Console.WriteLine(Label+"->"+fn_temp.name);
                    if (CALLING(cc, scope))
                    {
                        //      Console.WriteLine("SST  calling true");
                        return true;
                    }
                }

                else if (Tokenset[i].classname == "DataType")
                {
                    fn_temp.type = Tokenset[i].value;
                    fn_temp.ret = Tokenset[i].value;
                    Po_type = Tokenset[i].value;
                    i++;

                    if (ID_PO(cc, scope))
                    {   //    Console.WriteLine("SST idpo true");
                        return true;

                    }
                }

                else if (Tokenset[i].classname == "Stop")
                {
                    if (STOP())
                    {
                        //  Console.WriteLine("SST stop true");
                        return true;
                    }
                }
                else if (Tokenset[i].classname == "Cont")
                {
                    if (CONT())
                    {
                        //Console.WriteLine("SSTcont  true");
                        return true;
                    }
                }
                else if (Tokenset[i].classname == "Ref")
                {

                    if (REF(cc, scope))
                    {
                        //                        Console.WriteLine("SST ref true");
                        return true;
                    }
                }
                else if (Tokenset[i].classname == "Ret")
                {
                    if (RET(cc, scope, st_temp.ret))
                    {
                        //                      Console.WriteLine("Ret fun ;" + Tokenset[i].classname);
                        return true;
                    }
                }

                //  else return false;
            }
            else
                return false;
            //        Console.WriteLine("SST false");
            //     chk = false;

            return false;
        }

        public bool MST2(List<symbol_table> cc, int scope)
        {
            //bool chk = true;//sst ka nfirst
            if (Tokenset[i].classname == "While" || Tokenset[i].classname == "Agar" || Tokenset[i].classname == "LOOP" || Tokenset[i].classname == "Ret"
                || Tokenset[i].classname == "LOOP" || Tokenset[i].classname == "IncDec_Operator" || Tokenset[i].classname == "DataType"
                || Tokenset[i].classname == "ID" || Tokenset[i].classname == "Select" || Tokenset[i].classname == "Ref" || Tokenset[i].classname == "Cont"
                || Tokenset[i].classname == "Po*" || Tokenset[i].classname == "Stop")//first
            {
                //Console.WriteLine(Tokenset[i].classname + "=> MST");
                if (SST2(cc, scope))//ssT
                {
                    //  Console.WriteLine(Tokenset[i].classname + "sst agr tru inmst");
                    if (MST2(cc, scope))
                    {
                        //    Console.WriteLine("mst true");

                        return true;
                    }
                }
            }
            else if (Tokenset[i].classname == "}")//Follow
            {
                //                Console.WriteLine("MST ka folllow true" + Tokenset[i].classname);
                return true;
            }
            else
            {
                //              Console.WriteLine("mst false");
                return false;
                //                chk = false;
            }
            return false;
        }
        bool P3()
        {
            if (Tokenset[i].classname == "IncDec_Operator")
            {
                i++;
                return true;
            }

            else if (Tokenset[i].classname == "MDM" || Tokenset[i].classname == "PLUSMINUS" || Tokenset[i].classname == ":" || Tokenset[i].classname == "Relational_Operator" || Tokenset[i].classname == "And" || Tokenset[i].classname == ";" || Tokenset[i].classname == ")" || Tokenset[i].classname == "," || Tokenset[i].classname == "OR")
            {
                //Console.WriteLine("CommonID fun true");
                return true;
            }

            else
                return false;
            return false;

        }
        public bool SUPER_CLASS_MEMBER(List<symbol_table> cc)
        {
            bool chk = false;
            st_temp.clear();
            Ct_temp.clear();
            check_s = 0;
            check_fn = 0;
            PL = "";
            if (Tokenset[i].classname == "Access Modifier" || Tokenset[i].classname == "Fixed" || Tokenset[i].classname == "St" || Tokenset[i].classname == "DataType"
             || Tokenset[i].classname == "Devoid" || Tokenset[i].classname == "ID")//first
            {

                if (AM(Ct_temp, st_temp, "symb"))
                {

                    if (St_final_DT_B_A_P(cc))
                    {
                        //            Console.WriteLine("Super class member true");
                        return true;
                    }
                }
            }
            else
            {
                //Console.WriteLine("Super class member false");
                chk = false;
            }
            return chk;
        }
        public bool St_final_DT_B_A_P(List<symbol_table> cc)
        {
            bool chk = false;
            // if (Tokenset[i].classname == "St" || Tokenset[i].classname == "Fixed" || Tokenset[i].classname == "DataType" || Tokenset[i].classname == "Devoid")
            pl3 = "";
            PL2 = "";
            PL = "";
            if (Tokenset[i].classname == "St")
            {
                st_temp.sta = true;
                i++;
                if (FIXED(Ct_temp, st_temp, "symb"))
                {
                    if (DT_B_A_P(cc))
                    {
                        return true;
                    }
                }
            }
            else if (Tokenset[i].classname == "Fixed")
            {
                st_temp.final = true;
                i++;
                if (DT_B_A_P(cc))
                {
                    return true;
                }
            }
            else if (Tokenset[i].classname == "DataType")
            {
                st_temp.type = Tokenset[i].value;
                st_temp.ret = st_temp.type;
                //   fn_temp.type = Tokenset[i].value;
                i++;
                if (ID_PO_FID_P(cc))
                {
                    return true;
                }
            }
            else if (Tokenset[i].classname == "Devoid")
            {
                st_temp.type = Tokenset[i].value;
                st_temp.ret = Tokenset[i].value;
                i++;
                if (Tokenset[i].classname == "ID")
                {
                    string temp2 = Tokenset[i].value;
                    st_temp.name = Tokenset[i].value;
                    i++;
                    if (Tokenset[i].classname == "(")
                    {
                        i++;
                        scope = Semantic.createscope();
                        if (PARAM(cc, scope))
                        {
                            if (Tokenset[i].classname == ")")
                            {
                                st_temp.name = temp2;
                                i++;
                                if (Tokenset[i].classname == "{")
                                {
                                    st_temp.PL = PL;
                                    st_temp.type = PL + "->" + st_temp.type;
                                    Semantic.insertion_fn(cc, st_temp);
                                    icg.write(st_temp, pl3);
                                    //   Console.WriteLine(st_temp.type + " stteno " + st_temp.name);
                                    i++;

                                    if (MST(cc, scope))
                                    {

                                        if (Tokenset[i].classname == "}")
                                        {
                                            i++;
                                            icg.add_string("END P");
                                            scope = Semantic.destroyscope();             //              Console.WriteLine("st_final_st fun true");
                                            return true;
                                        }
                                    }
                                }
                            }
                        }
                    }

                }
            }
            else if (Tokenset[i].classname == "ID")
            {
                st_temp.type = Tokenset[i].value;
                st_temp.ret = st_temp.type;
                st_temp.name = Tokenset[i].value;
                temp_name = st_temp.class_name;
                //  if(Semantic.Lookup(ct,Tokenset[i].value,0)!=null)
                st_temp.class_name = Tokenset[i].value;
                i++;
                if (OBI_CONST_DEC(cc))
                {
                    return true;
                }
            }
            else
            {
                //   Console.WriteLine("St_final_DT_B_A_P fun is true");
                chk = false;
            }
            return chk;
        }
        public bool OBI_CONST_DEC(List<symbol_table> cc)
        {
            bool chk = false;
            PL = "";
            if (Tokenset[i].classname == "ID")
            {
                st_temp.name = Tokenset[i].value;
                i++;
                check_s = 1;
                if (N1(cc, scope))//chnhge kia h
                {
                    //   Console.WriteLine("OBI_CONST_DEC fun true");
                    return true;
                }
            }
            else if (Tokenset[i].classname == "(")
            {
                i++;
                st_temp.type = "";
                st_temp.ret = st_temp.name;
                st_temp.chk_const = true;
                //       Console.WriteLine("class name in obj ->" + st_temp.class_name);
                scope = Semantic.createscope();
                if (PARAM(cc, scope))
                {
                    if (Tokenset[i].classname == ")")
                    {
                        st_temp.name = st_temp.ret;
                        st_temp.PL = PL;
                        st_temp.type = PL + "->" + st_temp.type;
                        //      Console.WriteLine("Pl:" + PL + "  type :" + st_temp.type);
                        if (Semantic.compatibility(st_temp) == true)
                        {
                            // Console.WriteLine(st_temp.name + "->" + st_temp.class_name);
                            Semantic.insertion_fn(cc, st_temp);
                            icg.write(st_temp, pl3);
                        }
                        i++;
                        if (Tokenset[i].classname == "{")
                        {
                            i++;
                            if (SUPER1(cc, scope))
                            {
                                //             Console.WriteLine(Tokenset[i].classname + "suepr 1 lbd");
                                if (MST2(cc, scope))
                                {
                                    if (Tokenset[i].classname == "}")
                                    {
                                        i++;
                                        icg.add_string("END P");
                                        scope = Semantic.destroyscope();
                                        //                         Console.WriteLine("OBI_CONST_DEC fun true");
                                        return true;
                                    }
                                }
                            }

                        }
                    }
                }
            }

            else
            {
                // Console.WriteLine("OBI_CONST_DEC fun false");

                chk = false;
            }
            return chk;

        }
        public bool N1(List<symbol_table> cc, int scope)
        {
            if (Tokenset[i].classname == ";")
            {
                i++;
                if (check_s == 1)
                {
                    Semantic.insertion(cc, st_temp);
                }
                else if (check_fn == 1)
                {
                    //   Console.WriteLine("n1 chk 1");
                    Semantic.insertion_fn(fn, fn_temp);
                }
                check_s = 0;
                check_fn = 0;
                return true;
            }
            else if (Tokenset[i].classname == "=")
            {
                i++;
                if (NEW(cc, scope))
                {
                    //if (Tokenset[i].classname == ";") // chnge kia hai
                    // {
                    //  i++;
                    return true;
                    //   }
                }
            }
            else if (Tokenset[i].classname == "(")
            {
                i++;
                scope = Semantic.createscope();
                if (PARAM(cc, scope))
                {
                    if (Tokenset[i].classname == ")")
                    {
                        i++;
                        st_temp.ret = st_temp.type;
                        st_temp.PL = PL;
                        st_temp.type = PL + "->" + st_temp.type;
                        if (Semantic.Lookup(ct, st_temp.ret, 0) != null)
                        {
                            //     Console.WriteLine("obj k asnkk -???? "+st_temp.type);
                            //    Semantic.insertion(cc, st_temp);
                            icg.write(st_temp, pl3);
                            Semantic.insertion_fn(cc, st_temp);
                        }
                        //    else 
                        //      Console.WriteLine("class is not declared !!");

                        if (Tokenset[i].classname == "{")
                        {
                            i++;
                            if (MST_SST2(cc, scope))
                            {
                                //             Console.WriteLine(Tokenset[i].classname + "suepr 1 lbd");
                                if (RET(cc, scope, st_temp.ret))
                                {
                                    if (Tokenset[i].classname == "}")
                                    {
                                        i++;
                                        icg.add_string("END P");
                                        scope = Semantic.destroyscope();
                                        //              Console.WriteLine("OBI_CONST_DEC fun true");
                                        return true;
                                    }
                                }
                            }

                        }
                    }
                }
            }
            else return false;
            return false;
        }
        public bool DT_B_A_P(List<symbol_table> cc)
        {
            bool chk = false;
            //if (Tokenset[i].classname == "DataType" || Tokenset[i].classname == "Devoid")
            //{
            if (Tokenset[i].classname == "DataType")
            {
                st_temp.type = Tokenset[i].value;
                Po_type = st_temp.type;
                st_temp.ret = st_temp.type;
                //     Label = "" + Tokenset[i].value;
                i++;
                if (ID_PO_FID_P(cc))
                {
                    //      Console.WriteLine("DT_B_A_P fun true");
                    return true;
                }
            }

            else if (Tokenset[i].classname == "Devoid")
            {
                st_temp.type = Tokenset[i].value;
                st_temp.ret = Tokenset[i].value;
                i++;
                if (Tokenset[i].classname == "ID")
                {
                    st_temp.name = Tokenset[i].value;
                    i++;
                    if (Tokenset[i].classname == "(")
                    {
                        i++;
                        scope = Semantic.createscope();
                        if (PARAM(cc, scope))
                        {
                            if (Tokenset[i].classname == ")")
                            {
                                i++;
                                if (Tokenset[i].classname == "{")
                                {
                                    st_temp.PL = PL;
                                    st_temp.type = PL + "->" + st_temp.type;
                                    Semantic.insertion_fn(cc, st_temp);
                                    icg.write(st_temp, pl3);
                                    //    Console.WriteLine("****devoid scope  :" + scope);
                                    i++;
                                    if (MST(cc, scope))
                                    {
                                        if (Tokenset[i].classname == "}")
                                        {
                                            i++;
                                            icg.add_string("END P");
                                            scope = Semantic.destroyscope();
                                            //                            Console.WriteLine("DT_B_A_P fun true");
                                            return true;
                                        }
                                    }
                                }
                            }
                        }
                    }

                }
            }
            else if (Tokenset[i].classname == "ID")
            {
                if (Semantic.Lookup(ct, Tokenset[i].value, 0) != Tokenset[i].classname)
                {
                    st_temp.type = Tokenset[i].value;
                    st_temp.ret = st_temp.type;
                }
                i++;
                if (Tokenset[i].classname == "ID")
                {
                    st_temp.name = Tokenset[i].value;
                    i++;
                    if (Tokenset[i].classname == "(")
                    {
                        i++;
                        scope = Semantic.createscope();
                        if (PARAM(cc, scope))
                        {
                            if (Tokenset[i].classname == ")")
                            {
                                st_temp.PL = PL;
                                st_temp.type = PL + "->" + Tokenset[i].value;
                                icg.write(st_temp, pl3);
                                Semantic.insertion_fn(cc, st_temp);
                                i++;
                                if (Tokenset[i].classname == "{")
                                {
                                    i++;
                                    if (MST_SST2(cc, scope))
                                    {
                                        if (RET(cc, scope, st_temp.ret))
                                        {
                                            if (Tokenset[i].classname == "}")
                                            {
                                                icg.add_string("END P");

                                                scope = Semantic.destroyscope();
                                                i++;
                                                //                                                     Console.WriteLine("DT_B_A_P fun true");
                                                return true;
                                            }
                                        }
                                    }
                                }
                            }
                        }

                    }
                }
            }

            else
            {
                //Console.WriteLine("DT_B_A_P fun fase");
                chk = false;
            }
            return chk;
        }
        public bool PARAM(List<symbol_table> cc, int scope)
        {
            bool chk = false;
            PL = "";
            fn_temp.clear();
            fn_temp.scope = scope;
            fn_temp.assign = true;
            if (Tokenset[i].classname == "DataType")
            {
                PL = Tokenset[i].value;
                pl3 = PL;
                fn_temp.type = Tokenset[i].value;
                //                st_temp.type = Tokenset[i].value + "->" + st_temp.type;
                i++;
                if (ID_P2(cc, scope))
                {
                    Semantic.insertion_fn(fn, fn_temp);

                    if (PARAM1(cc, scope))
                    {

                        return true;
                    }
                }
            }
            else if (Tokenset[i].classname == "ID")
            {
                PL = Tokenset[i].value;
                pl3 = PL;//  
                fn_temp.type = Tokenset[i].value;
                i++;
                if (Tokenset[i].classname == "ID")
                {
                    //  PL =PL+ Tokenset[i].value;
                    fn_temp.name = Tokenset[i].value;
                    Semantic.insertion_fn(fn, fn_temp);
                    //             
                    i++;
                    if (PARAM1(cc, scope))
                    {
                        return true;
                    }
                }
            }
            else if (Tokenset[i].classname == ")")//follow
                return true;
            else
                chk = false;
            return chk;

        }
        //no dhka
        public bool PARAM1(List<symbol_table> cc, int scope)
        {
            bool chk = false;
            if (Tokenset[i].classname == ",")
            {
                pl3 = PL + "_";
                PL = PL + Tokenset[i].value;
                i++;
                if (PARAM2(cc, scope))
                {
                    return true;
                }
            }
            else if (Tokenset[i].classname == ")")
                return true;
            else
                chk = false;
            return chk;
        }
        public bool PARAM2(List<symbol_table> cc, int scope)
        {
            bool chk = false;
            if (Tokenset[i].classname == "DataType")
            {
                PL = PL + Tokenset[i].value;
                pl3 = pl3 + Tokenset[i].value;
                fn_temp.type = Tokenset[i].value;
                i++;
                if (ID_P2(cc, scope))
                {
                    Semantic.insertion_fn(fn, fn_temp);
                    if (PARAM1(cc, scope))
                    {
                        return true;
                    }
                }
            }
            else if (Tokenset[i].classname == "ID")
            {
                PL = PL + Tokenset[i].value;
                pl3 = pl3 + Tokenset[i].value;

                i++;
                if (Tokenset[i].classname == "ID")
                {
                    PL = PL + Tokenset[i].value;
                    i++;
                    if (PARAM1(cc, scope))
                    {
                        return true;
                    }
                }
            }
            else
                chk = false;
            return chk;
        }/*
public bool AM()
        {
            if (Tokenset[i].classname == "Access Modifier")//first
            {
                i++;
                return true;
            }
            else
            {
                if (Tokenset[i].classname == "Fixed" || Tokenset[i].classname == "Room" || Tokenset[i].classname == "St" || Tokenset[i].classname == "DataType" || Tokenset[i].classname == "Devoid" || Tokenset[i].classname == "ID")//Follow
                   return true;
            }
            //else

            return false;
        }*/
        public bool AM(class_table ct, symbol_table st, string chk)
        {
            if (Tokenset[i].classname == "Access Modifier")//first
            {
                // st.am= Tokenset[i].value;
                Semantic.setam(ct, st, chk, Tokenset[i].value);
                i++;
                return true;
            }
            else
            {
                if (Tokenset[i].classname == "Fixed" || Tokenset[i].classname == "Room" || Tokenset[i].classname == "St" || Tokenset[i].classname == "DataType" || Tokenset[i].classname == "Devoid" || Tokenset[i].classname == "ID")//Follow
                    Semantic.setam(ct, st, chk, "Public");

                return true;
            }
            //else
            return false;
        }

        public bool FIXED()
        {
            if (Tokenset[i].classname == "Fixed")//first
            {
                i++;
                return true;
            }
            else
                if (Tokenset[i].classname == "Room" || Tokenset[i].classname == "DataType" || Tokenset[i].classname == "Devoid" || Tokenset[i].classname == "ID")//Follow
                {
                    //           Console.WriteLine("FIXED follow  true");
                    return true;
                }

                else
                {
                    // Console.WriteLine("FIXED fun is false");
                    return false;
                }
            return false;
        }
        public bool FIXED(class_table ct, symbol_table st, string chk)
        {
            if (Tokenset[i].classname == "Fixed")//first
            {
                Semantic.setfinal(ct, st, chk, true);
                i++;
                return true;
            }
            else
                if (Tokenset[i].classname == "Room" || Tokenset[i].classname == "DataType" || Tokenset[i].classname == "Devoid" || Tokenset[i].classname == "ID")//Follow
                {
                    //           Console.WriteLine("FIXED follow  true");
                    Semantic.setfinal(ct, st, chk, false);
                    return true;
                }

                else
                {
                    // Console.WriteLine("FIXED fun is false");
                    return false;
                }
            return false;
        }
    }
}

//}
