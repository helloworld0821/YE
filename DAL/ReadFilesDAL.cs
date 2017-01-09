using SCH.Model;
using SCH.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SCH.DAL
{

    class ReadFilesDAL
    {
        public static List<double> lstHD = new List<double>();
        public static int INTX = 0;
        public static int INTY = 0;
        public static int INTK = 0;
        public static void ReadHD(string path)
        {
            List<double> lstDZ = new List<double>();
            List<double> lstNTG = new List<double>();
            lstHD.Clear();
            INTX = 0;
            INTY = 0;
            INTK = 0;
            bool isDz = false;
            bool isNtg = false;
            FileStream fs = new FileStream(path, FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            string strReadLine = sr.ReadLine();
            while (strReadLine != null)
            {
                strReadLine = strReadLine.Trim();
                if (strReadLine.Equals("BOX"))
                {
                    strReadLine = sr.ReadLine().Trim();
                    string[] strArray = Regex.Split(strReadLine, @"\s+");
                    INTX = Convert.ToInt32(strArray[1]);
                    INTY = Convert.ToInt32(strArray[3]);
                    INTK = Convert.ToInt32(strArray[5]);
                }

                if (strReadLine.Equals("DZ"))
                {
                    strReadLine = sr.ReadLine();
                    isDz = true;
                }
                if (strReadLine.Equals("NTG"))
                {
                    strReadLine = sr.ReadLine();
                    isNtg = true;
                }
                if (isDz)
                {
                    strReadLine = strReadLine.Trim();
                    if (strReadLine.Equals("/"))
                    {
                        isDz = false;
                    }
                    else
                    {
                        string[] strArray = Regex.Split(strReadLine, @"\s+");
                        for (int i = 0; i < strArray.Length; i++)
                        {
                            lstDZ.Add(Convert.ToDouble(strArray[i]));
                        }
                    }
                }

                if (isNtg)
                {
                    strReadLine = strReadLine.Trim();
                    if (strReadLine.Equals("/"))
                    {
                        isNtg = false;
                    }
                    else
                    {
                        string[] strArray = Regex.Split(strReadLine, @"\s+");
                        for (int i = 0; i < strArray.Length; i++)
                        {
                            lstNTG.Add(Convert.ToDouble(strArray[i]));
                        }
                    }
                }
                strReadLine = sr.ReadLine();
            }
            sr.Close();
            fs.Close();


            for (int i = 0; i < lstDZ.Count; i++)
            {
                lstHD.Add(lstDZ[i] * lstNTG[i]);
            }
        }
        public void ReadSJL(string inputPath, string outputPath)
        {
            double dblSumHD = 0;//单井当前层段总厚度
            List<WellModel> lstWM = new List<WellModel>();
            DataTable dtZB = ListToDataTableUtil.ListToDataTable(lstWM);
            DataTable dtWM = dtZB.Clone(); ;
            FileStream fs = new FileStream(inputPath, FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            string strReadLine = sr.ReadLine();
            while (strReadLine != null)
            {
                string trim = strReadLine.Trim();
                if (trim.Equals("TSTEP"))
                {
                    break;
                }
                if (trim.Equals("WELSPECS"))
                {
                    strReadLine = sr.ReadLine().Trim();
                    string[] strArray = Regex.Split(strReadLine, @"\s+");
                    DataRow drZB = dtZB.NewRow();
                    drZB["JH"] = strArray[0].Substring(1, strArray[0].Length - 2);
                    drZB["X"] = Convert.ToInt32(strArray[2]);
                    drZB["Y"] = Convert.ToInt32(strArray[3]);
                    drZB["LB"] = strArray[5].Substring(1, strArray[5].Length - 2);
                    dtZB.Rows.Add(drZB);
                }
                if (trim.Equals("COMPDAT"))
                {
                    strReadLine = sr.ReadLine().Trim();
                    string[] strArray = Regex.Split(strReadLine, @"\s+");

                    DataRow[] rows = dtZB.Select("JH = '" + strArray[0].Substring(1, strArray[0].Length - 2) + "'");
                    if (strArray[0].Substring(1, strArray[0].Length - 2).Equals("Z511310"))
                    {
                    }
                    if (rows.Count() > 0)
                    {
                        DataRow drWM = dtWM.NewRow();

                        int k1 = Convert.ToInt32(strArray[2]);
                        int k2 = Convert.ToInt32(strArray[3]);
                        int x = Convert.ToInt32(rows[0]["X"]);
                        int y = Convert.ToInt32(rows[0]["Y"]);
                        drWM["JH"] = rows[0]["JH"];
                        drWM["X"] = x;
                        drWM["Y"] = y;
                        drWM["K1"] = k1;
                        drWM["K2"] = k2;
                        drWM["LB"] = rows[0]["LB"];
                        for (int i = k1; i <= k2; i++)
                        {
                            int index = (i - 1) * INTX * INTY + ((y - 1) * INTX + x) - 1;
                            dblSumHD += lstHD[index];
                        }
                        drWM["HD"] = dblSumHD;
                        drWM["ZRL"] = dblSumHD * MainFrom.dblZRL;
                        dblSumHD = 0;
                        dtWM.Rows.Add(drWM);
                    }

                }

                strReadLine = sr.ReadLine();

            }
            sr.Close();
            fs.Close();

            DataTable dtGroupInfo = GroupInfo(dtWM);


            //DataTable dtYjInfo = dtGroupInfo.Clone();
            //var query =
            //    from rYJ in dtYJ.AsEnumerable()
            //    join rGI in dtGroupInfo.AsEnumerable()
            //    on new { JH = rYJ.Field<string>("JH") } equals new { JH = rGI.Field<string>("JH") }

            //    select rGI.ItemArray.Concat(rYJ.ItemArray.Skip(1));

            //foreach (var obj in query)
            //{
            //    DataRow drYjInfo = dtYjInfo.NewRow();
            //    drYjInfo.ItemArray = obj.ToArray();
            //    dtYjInfo.Rows.Add(drYjInfo);
            //}

            //DataTable dtSjInfo = dtGroupInfo.Clone();
            //query =
            //    from rSJ in dtSJ.AsEnumerable()
            //    join rGI in dtGroupInfo.AsEnumerable()
            //    on new { JH = rSJ.Field<string>("JH") } equals new { JH = rGI.Field<string>("JH") }

            //    select rGI.ItemArray.Concat(rSJ.ItemArray.Skip(1));

            //foreach (var obj in query)
            //{
            //    DataRow drSjInfo = dtSjInfo.NewRow();
            //    drSjInfo.ItemArray = obj.ToArray();
            //    dtSjInfo.Rows.Add(drSjInfo);
            //}

            double dblSumZRL = Convert.ToDouble(dtGroupInfo.Compute("sum(ZRL)", "LB = 'WATER'"));//总注入量
            double dblSumYJHD = Convert.ToDouble(dtGroupInfo.Compute("sum(HD)", "LB = 'OIL'"));//总油井厚度
            double ratio = dblSumZRL / dblSumYJHD;
            DataRow[] r = dtGroupInfo.Select("LB = 'OIL'");
            foreach (DataRow drYjInfo in r)
            {
                drYjInfo["ZRL"] = Convert.ToDouble(drYjInfo["HD"]) * ratio;
            }
            Print(inputPath, outputPath, dtGroupInfo);
        }
        private DataTable GroupInfo(DataTable dtWM)
        {
            DataTable dtGroupInfo = dtWM.Clone();
            var query =
                from rG in dtWM.AsEnumerable()
                group rG by new { JH = rG.Field<string>("JH"), LB = rG.Field<string>("LB"), X = rG.Field<int>("X"), Y = rG.Field<int>("Y") } into m

                select new
                {
                    JH = m.Key.JH,
                    LB = m.Key.LB,
                    X = m.Key.X,
                    Y = m.Key.Y,
                    HD = m.Sum(n => n.Field<double>("HD")),
                    ZRL = m.Sum(n => n.Field<double>("ZRL"))
                };

            foreach (var nobj in query)
            {
                dtGroupInfo.Rows.Add(nobj.JH, nobj.LB, nobj.X, nobj.Y, nobj.HD, nobj.ZRL, null, null);
            }
            return dtGroupInfo;
        }
        private void Print(string inputPath, string outputPath, DataTable dtGroupInfo)
        {
            FileStream fs = new FileStream(inputPath, FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            FileStream fw = new FileStream(outputPath, FileMode.Append);
            StreamWriter sw = new StreamWriter(fw);
            string str = "";
            string strJH = "";
            string strReadLine = sr.ReadLine();
            while (strReadLine != null)
            {
                if (strReadLine.Equals("WCONINJE") || strReadLine.Equals("WCONPROD"))
                {
                    sw.WriteLine(strReadLine);
                    strReadLine = sr.ReadLine();
                    string[] strArray = Regex.Split(strReadLine, @"\s+");
                    strJH = strArray[0].Substring(1, strArray[0].Length - 2);
                    DataRow[] dr = dtGroupInfo.Select("JH = '" + strJH + "'");
                    if (dr.Count() > 0)
                    {
                        strArray[4] = Convert.ToDouble(dr[0]["ZRL"]).ToString("f3");
                    }
                    for (int i = 0; i < strArray.Count(); i++)
                    {
                        str += strArray[i] + " ";
                    }
                    sw.WriteLine(str);
                    strReadLine = sr.ReadLine();
                    str = "";
                    continue;

                }
                sw.WriteLine(strReadLine);
                strReadLine = sr.ReadLine();
            }
            sr.Close();
            fs.Close();
            sw.Close();
            fw.Close();
        }


    }

}
