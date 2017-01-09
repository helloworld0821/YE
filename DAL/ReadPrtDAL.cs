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
    class ReadPrtDAL
    {

        public static int cntDate = 0;
        public readonly double HSL = 0.9;
        private DataTable GetMinLayer(string strPath)
        {
            cntDate = 0;

            string strReadLine;
            string strJH = string.Empty;
            string strDate = string.Empty;
            bool isWaterWell = false;
            int isDate = 0, isRead = -1;
            string[] strArray;
            FileStream fs = new FileStream(strPath, FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            List<YJModel> lstOilM = new List<YJModel>();
            DataTable dtOil = ListToDataTableUtil.ListToDataTable(lstOilM);

            double dblHS = 0;
            strReadLine = sr.ReadLine().Replace(":", " ").Replace(",", " ");
            string trim = strReadLine.Trim();
            while (strReadLine != null)
            {
                trim = strReadLine.Trim();

                if (Regex.IsMatch(trim, @"STEP\s+\d.+REPT.+ITS\)"))
                {
                    strArray = Regex.Split(trim, @"\s+");
                    strDate = strArray[strArray.Length - 1];
                    cntDate++;

                    //if (strDate.Equals(strStartDate))
                    //{
                    //    isDate = 0;
                    //}
                    //if (strDate.Equals(strEndDate))
                    //{
                    //    isRead = -1;
                    //    isDate = -1;
                    //    break;
                    //}
                }

                if (trim.Equals("INJECTION  REPORT"))
                {
                    isRead = -1;
                    isWaterWell = true;
                }
                else if (trim.Equals("PRODUCTION REPORT"))
                {
                    isRead = 0;
                    isWaterWell = false;
                }
                else if (trim.Equals("CUMULATIVE PRODUCTION/INJECTION TOTALS"))
                {
                    isRead = -1;
                }

                if (isRead >= 0)
                {
                    strReadLine = strReadLine.Replace(":", " ").Replace(",", " ");

                    trim = strReadLine.Trim();
                    if (Regex.IsMatch(trim, @"FIELD.+"))
                    {
                        strArray = Regex.Split(trim, @"\s+");
                        dblHS = Convert.ToDouble(strArray[5]);
                        if (isDate == 1)
                        {
                            cntDate--;
                            break;
                        }
                           
                        if (isDate == 0)
                        {
                            if(dblHS > HSL)
                                isDate++;

                        }
                        
                    }
                    if (isDate == 1)
                    {
                        if (Regex.IsMatch(trim, ".+O PI"))
                        {
                            strArray = Regex.Split(trim, @"\s+");

                            if (strArray[0].Length > 0)
                            {
                                if (!strArray[0].Equals("BLOCK"))
                                {
                                    strJH = strArray[0];
                                }
                                else if (strArray[0].Equals("BLOCK"))
                                {

                                    DataRow drOil = dtOil.NewRow();
                                    drOil["ny"] = strDate;
                                    drOil["jh"] = strJH;
                                    drOil["x"] = Convert.ToInt32(strArray[1]);
                                    drOil["y"] = Convert.ToInt32(strArray[2]);
                                    drOil["k"] = Convert.ToInt32(strArray[3]);
                                    if (!strArray[4].Equals("SHUT"))
                                    {
                                        if (Convert.ToDouble(strArray[5]) < 0)
                                        {
                                            drOil["rcsl"] = 0;
                                        }
                                        else
                                        {
                                            drOil["rcsl"] = Convert.ToDouble(strArray[5]);
                                        }
                                        if (Convert.ToDouble(strArray[4]) < 0)
                                        {
                                            drOil["rcyl"] = 0;
                                        }
                                        else
                                        {
                                            drOil["rcyl"] = Convert.ToDouble(strArray[4]);
                                        }
                                        drOil["rcyl1"] = Convert.ToDouble(strArray[5]) + Convert.ToDouble(strArray[4]);

                                    }
                                    else
                                    {
                                        if (Convert.ToDouble(strArray[6]) < 0)
                                        {
                                            drOil["rcsl"] = 0;
                                        }
                                        else
                                        {
                                            drOil["rcsl"] = Convert.ToDouble(strArray[6]);
                                        }
                                        if (Convert.ToDouble(strArray[5]) < 0)
                                        {
                                            drOil["rcyl"] = 0;
                                        }
                                        else
                                        {
                                            drOil["rcyl"] = Convert.ToDouble(strArray[5]);
                                        }
                                        drOil["rcyl1"] = Convert.ToDouble(strArray[5]) + Convert.ToDouble(strArray[6]);
                                    }
                                    dtOil.Rows.Add(drOil);
                                }
                            }
                        }

                    }

                }
                strReadLine = sr.ReadLine();
                if (strReadLine != null)
                {
                    strReadLine = strReadLine.Replace(":", " ").Replace(",", " ");
                }

            }


            sr.Close();
            fs.Close();

            DataTable dtMinRCYL = GroupInfo(dtOil);
            DataTable dtShut = dtOil.Clone();
            var query =
               from rO in dtOil.AsEnumerable()
               join rM in dtMinRCYL.AsEnumerable()
               on new { JH = rO.Field<string>("JH"), NY = rO.Field<string>("NY"), RCYL1 = rO.Field<double>("RCYL1") } equals new { JH = rM.Field<string>("JH"), NY = rM.Field<string>("NY"), RCYL1 = rM.Field<double>("RCYL1") }

               select rO.ItemArray.Concat(rM.ItemArray.Skip(dtMinRCYL.Columns.Count));
            foreach (var obj in query)
            {
                DataRow drShut = dtShut.NewRow();
                drShut.ItemArray = obj.ToArray();
                dtShut.Rows.Add(drShut);
            }
            return dtShut;
        }

        private DataTable GroupInfo(DataTable dtOil)
        {
            DataTable dtGroupInfo = dtOil.Clone();
            var query =
                from rG in dtOil.AsEnumerable()
                group rG by new { JH = rG.Field<string>("JH"), NY = rG.Field<string>("NY") } into m

                select new
                {
                    JH = m.Key.JH,
                    NY = m.Key.NY,
                    RCYL1 = m.Min(n => n.Field<double>("RCYL1")),

                };

            foreach (var nobj in query)
            {
                dtGroupInfo.Rows.Add(nobj.JH, nobj.NY, null, null, null, null, null, nobj.RCYL1);
            }


            return dtGroupInfo;
        }
        public void Print(string strPRTPath, string strSJLPath, string outputPath)
        {
            DataTable dtShut = GetMinLayer(strPRTPath);
            DataTable dtShutJH = dtShut.DefaultView.ToTable(true, "JH");
            int cntStep = 0;
            List<WPDModel> lstWPD = new List<WPDModel>();
            DataTable dtWPD = ListToDataTableUtil.ListToDataTable(lstWPD);
            FileStream fs = new FileStream(strSJLPath, FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            FileStream fw = new FileStream(outputPath, FileMode.Append);
            StreamWriter sw = new StreamWriter(fw);
            string str = "";
            string strJH = "";
            string strReadLine = sr.ReadLine();
            while (strReadLine != null)
            {
                if (strReadLine.Equals("WCONPROD"))
                {
                    sw.WriteLine(strReadLine);

                    strReadLine = sr.ReadLine();

                    sw.WriteLine(strReadLine);
                    string[] strArray = Regex.Split(strReadLine, @"\s+");
                    DataRow drWPD = dtWPD.NewRow();
                    for (int i = 0; i < strArray.Count(); i++)
                    {
                        drWPD[i] = strArray[i];
                    }
                    dtWPD.Rows.Add(drWPD);
                    //strJH = strArray[0].Substring(1, strArray[0].Length - 2);
                    //DataRow[] dr = dtGroupInfo.Select("JH = '" + strJH + "'");
                    //if (dr.Count() > 0)
                    //{
                    //    strArray[4] = Convert.ToDouble(dr[0]["ZRL"]).ToString("f3");
                    //}
                    //for (int i = 0; i < strArray.Count(); i++)
                    //{
                    //    str += strArray[i] + " ";
                    //}
                    //sw.WriteLine(str);
                    strReadLine = sr.ReadLine();
                    //str = "";
                    continue;

                }
                else if (strReadLine.Equals("TSTEP"))
                {
                    cntStep++;
                    sw.WriteLine(strReadLine);
                    strReadLine = sr.ReadLine();
                    sw.WriteLine(strReadLine);

                    if (cntStep == cntDate)
                    {
                        for (int i = 0; i < dtShutJH.Rows.Count; i++)
                        {
                            DataRow[] drShut = dtShut.Select("JH = '" + dtShutJH.Rows[i]["JH"] + "'");
                            sw.WriteLine("COMPDAT");
                            sw.WriteLine("'" + drShut[0]["JH"] + "' 2* " + drShut[0]["K"] + " " + drShut[0]["K"] + " 'SHUT' 2* 0.2 3* 'Z' 1* /");
                            sw.WriteLine("/");
                            sw.WriteLine();
                        }
                        for (int i = 0; i < dtWPD.Rows.Count; i++)
                        {
                            sw.WriteLine("WCONPROD");
                            for (int j = 0; j < dtWPD.Columns.Count; j++)
                            {
                                if (j < dtWPD.Columns.Count - 1)
                                {
                                    if (j == 4)
                                    {
                                        dtWPD.Rows[i][j] = (Convert.ToDouble(dtWPD.Rows[i][j]) * FrmImportPRT.dblTimes).ToString("f3");
                                    }
                                    sw.Write(dtWPD.Rows[i][j] + " ");
                                }
                                
                                if (j == dtWPD.Columns.Count - 1)
                                {
                                    sw.WriteLine(dtWPD.Rows[i][j]);
                                }
                            }
                            sw.WriteLine("/");
                            sw.WriteLine();
                        }
                    }
                    strReadLine = sr.ReadLine();
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
