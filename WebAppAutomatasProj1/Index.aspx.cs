﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppAutomatasProj1
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        
        protected void btnLoadFile_Click(object sender, EventArgs e)
        {
            divPrimary.Visible = true;
            divSecondary.Visible = true;
            if (file.PostedFile != null && file.PostedFile.ContentLength > 0)
            {
                if (!Directory.Exists(Server.MapPath("~/tmp/")))
                    Directory.CreateDirectory(Server.MapPath("~/tmp/"));

                file.PostedFile.SaveAs(String.Concat(Server.MapPath("~/tmp/"), file.PostedFile.FileName));
                parseInfo(File.ReadAllText(Server.MapPath("~/tmp/") + file.PostedFile.FileName));
            }
        }

        private void parseInfo(String text)
        {
            //PRIMERA PARTE
            String Q = text.Substring(0, text.IndexOf("F"));
            if (Q.Contains("\n"))
                Q = Q.Substring(0, (Q.Length - 1));
            if (Q.Contains("\r\n"))
                Q = Q.Substring(0, (Q.Length - 2));
            String F = text.Substring(text.IndexOf("F"), text.IndexOf("i") - text.IndexOf("F"));
            if (F.Contains("\r\n"))
                F = F.Substring(0, (F.Length - 2));
            if (F.Contains("\n"))
                F = F.Substring(0, (F.Length - 1));
            String I = text.Substring(text.IndexOf("i"), text.IndexOf("A") - text.IndexOf("i"));
            if (I.Contains("\r\n"))
                I = I.Substring(0, (I.Length - 2));
            if (I.Contains("\n"))
                I = I.Substring(0, (I.Length - 1));
            String A = text.Substring(text.IndexOf("A"), text.IndexOf("w") - text.IndexOf("A"));
            if (A.Contains("\r\n"))
                A = A.Substring(0, (A.Length - 2));
            if (A.Contains("\n"))
                A = A.Substring(0, (A.Length - 1));
            String W = text.Substring(text.IndexOf("w"), text.Length - text.IndexOf("w"));

            //Q
            taTXT.InnerText = Q + Environment.NewLine;
            process(Q);
            //F
            taTXT.InnerText += F + Environment.NewLine;
            process(F);
            //I
            taTXT.InnerText += I + Environment.NewLine;
            process(I);
            //A
            taTXT.InnerText += A + Environment.NewLine;
            process(A);
            //W
            taTXT.InnerText += W + Environment.NewLine;
            DataTable AFN = new DataTable();
            AFN = generarAFN(W, F);
            gvAFN.DataSource = AFN;
            gvAFN.DataBind();
            //SEGUNDA PARTE
            DataTable AFD = new DataTable();
            AFD = generarAFD(AFN, I);
            gvAFD.DataSource = AFD;
            gvAFD.DataBind();

        }

        private DataTable generarAFD(DataTable AFN, String inicial) {
            DataTable AFD = new DataTable();
            inicial = inicial.Substring((inicial.IndexOf("=") + 1), inicial.Length - (inicial.IndexOf("=") + 1));
            if (AFN.Rows[0]["N"].Equals(inicial))
            {
                AFD.Columns.Add("ESTADO");
                foreach (DataColumn column in AFN.Columns)
                {
                    if (!column.ColumnName.Equals("N") && !column.ColumnName.Equals("e")) {
                        AFD.Columns.Add(column.ColumnName);
                    }
                }
                AFD.Columns.Add("COMPOSICION");

                DataRow dr = AFD.NewRow();
                dr["ESTADO"] = "A";
                //String composicionA = ordenar((AFN.Rows[0]["N"].ToString() +","+ cerraduraE(AFN.Rows[0]["e"].ToString(), AFN)));
                //dr["COMPOSICION"] = composicionA;
                dr["COMPOSICION"] = ordenar((AFN.Rows[0]["N"].ToString() + "," + cerraduraE(AFN.Rows[0]["e"].ToString(), AFN)));
                AFD.Rows.Add(dr);

                AFD = armarComposiciones(AFD, AFN);
                AFD = armarComposiciones(AFD, AFN);
            }
            return AFD;
        }

        private DataTable armarComposiciones(DataTable AFD, DataTable AFN) {
            for(int i = 0; i<AFD.Rows.Count;i++)
            {
                foreach (DataColumn column in AFD.Columns)
                {
                    if (!column.ColumnName.Equals("ESTADO") && !column.ColumnName.Equals("COMPOSICION"))
                    {
                        //AFD.Columns.Add(column.ColumnName);
                        //String composicion2 = mover("A", "a", AFN, composicionA);
                        String composicion = ordenar(cerraduraE(mover(column.ColumnName, AFN, AFD.Rows[i]["COMPOSICION"].ToString()), AFN));
                        bool flag = false;
                        string lastLetter = "";
                        foreach (DataRow r in AFD.Rows)
                        {
                            if (r["COMPOSICION"].Equals(composicion))
                            {
                                flag = true;
                                //row[column.ColumnName] = r["ESTADO"].ToString();
                                AFD.Rows[i][column.ColumnName] = r["ESTADO"].ToString();
                            }
                            lastLetter = r["ESTADO"].ToString();
                        }
                        if (!flag)
                        {
                            DataRow dr = AFD.NewRow();
                            dr["ESTADO"] = nextLetter(lastLetter);
                            dr["COMPOSICION"] = composicion;
                            AFD.Rows.Add(dr);
                        }
                    }
                }
            }
            return AFD;
        }

        private string nextLetter(String value) {

            char letter = value[0];
            char next;

            if (letter == 'z')
                next = 'a';
            else if (letter == 'Z')
                next = 'A';
            else
                next = (char)(((int)letter) + 1);

            return next.ToString();
        }

        private string mover(String letra, DataTable AFN, String composicion) {
            string[] tmp = composicion.Split(',');
            String cadena = "";
            foreach (string item in tmp)
            {
                foreach (DataRow row in AFN.Rows)
                {
                    if (row["N"].Equals(item)) {
                        if (!row[letra].ToString().Equals(""))
                            if(cadena.Equals(""))
                                cadena = row[letra].ToString();
                            else
                                cadena += "," + row[letra].ToString();
                    }
                }
            }
            
            return cadena;
        }

        private String ordenar(String text) {
            string[] tmp = text.Split(',');
            int quantity = tmp.Count();
            //string numberStr = Console.ReadLine(); // "1 2 3 1 2 3 1 2 ...."
            //string[] splitted = numberStr.Split(' ');
            int[] nums = new int[tmp.Length];

            for (int i = 0; i < tmp.Length; i++)
            {
                nums[i] = int.Parse(tmp[i]);
            }

            Comparison<int> comparador = new Comparison<int>((numero1, numero2) => numero1.CompareTo(numero2));
            // Llamar a Array.Sort, pasando el arreglo a ordenar y el comparador
            Array.Sort<int>(nums, comparador);
            // Ahora simplemente imprimimos
            int pos = -1;
            tmp = new string[quantity];
            foreach (int numero in nums)
            {
                pos++;
                tmp[pos] = numero.ToString();
                
            }
            //Array.Sort(tmp);
            return String.Join(",",tmp);
        }

        private String cerraduraE(String cadena, DataTable dt) {

            String[] aux;

            if (cadena.Contains(","))
                aux = cadena.Split(',');
            else
                aux = new string[1]{cadena};

            foreach (string item in aux)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["N"].Equals(item)) {
                        if(!dr["e"].ToString().Equals(""))
                            if(cadena.Equals(""))
                                cadena = cerraduraE(dr["e"].ToString(), dt);
                            else
                                cadena += "," + cerraduraE(dr["e"].ToString(), dt);
                        break;
                    }
                }
            }
            return cadena;
        }
        //PRIMERA PARTE
        private DataTable generarAFN(String text, String alph)
        {
            text = text.Substring((text.IndexOf("{") + 1), text.IndexOf("}") - (text.IndexOf("{") + 1));
            char[] separators = { '(', ')' };
            String[] elements = text.Split(separators);
            var aux = new List<string>();
            foreach (var s in elements)
            {
                if (!(string.IsNullOrEmpty(s) || s.Equals(",")))
                {
                    aux.Add(s);
                }
            }
            elements = aux.ToArray();

            DataTable dt = new DataTable();
            dt.Columns.Add("N");
            foreach (string element in extract(alph))
            {
                dt.Columns.Add(element.ToString());
            }
            if (text.Contains("e"))
            {
                dt.Columns.Add("e");
            }
            foreach (String element in elements)
            {
                String[] line = element.Split(',');
                bool bandera = false;
                foreach (DataRow row in dt.Rows)
                {
                    if (line[0].Equals(row["N"]))
                    {
                        bandera = true;
                        row[line[1]] += "," + line[2];
                    }
                }
                if (!bandera)
                {
                    DataRow dr = dt.NewRow();
                    dr["N"] = line[0];
                    dr[line[1]] = line[2];
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }

        private void process(String t)
        {
            foreach (string element in extract(t))
            {
                if (t.Substring(0, 1).Equals("Q"))
                    taQ.InnerText += element.ToString() + Environment.NewLine;
                if (t.Substring(0, 1).Equals("F"))
                    taF.InnerText += element.ToString() + Environment.NewLine;
                //if (t.Substring(0, 1).Equals("i"))
                    //taI.InnerText = element.ToString() + Environment.NewLine;
                if (t.Substring(0, 1).Equals("A"))
                    taA.InnerText += element.ToString() + Environment.NewLine;
            }
        }

        private string[] extract(String text)
        {
            string[] c = null;
            if (text.Contains("{") && text.Contains("}"))
            {
                text = text.Substring((text.IndexOf("{") + 1), text.IndexOf("}") - (text.IndexOf("{") + 1));
                char[] separators = { '(', ')' };
                c = text.Split(',');
            } else if (text.Contains("="))
            {
                text = text.Substring((text.IndexOf("=") + 1), text.Length - (text.IndexOf("=") + 1));
                c = new string[1] {text};
            }
            return c;
        }

    }
}