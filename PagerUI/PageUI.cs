/**
 * 自然框架之QuickPager分页控件
 * http://www.natureFW.com/
 *
 * @author
 * 金洋（金色海洋jyk）
 * 
 * @copyright
 * Copyright (C) 2005-2013 金洋.
 *
 * Licensed under a GNU Lesser General Public License.
 * http://creativecommons.org/licenses/LGPL/2.1/
 *
 * 自然框架之QuickPager分页控件 is free software. You are allowed to download, modify and distribute 
 * the source code in accordance with LGPL 2.1 license, however if you want to use 
 * 自然框架之QuickPager分页控件 on your site or include it in your commercial software, you must  be registered.
 * http://www.natureFW.com/registered
 */

/* ***********************************************
 * author :  金色海洋（金洋）
 * email  :  jyk0011@live.cn 
 * website:  www.natureFW.com
 * function: 分页控件的绘制页面部分
 * history:  created by 金洋  
 * 2011-01-28 简单整理
 * ***********************************************/

using System;
using System.Globalization;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace Nature.UI.WebControl.QuickPager.PagerUI
{
    /// <summary>
    /// 负责绘制分页控件的显示内容
    /// </summary>
    public class PageUI
    {
        /// <summary>
        /// 分页控件的实例，在产生PageUI的实例的时候设置。
        /// </summary>
        protected QuickPager MyPager ;
        /// <summary>
        /// 初始化，设置分页控件的实例
        /// </summary>
        public PageUI(QuickPager pager)
        {
            MyPager = pager;
        }

        /// <summary>
        /// 添加UI的步骤，以后需要灵活处理
        /// </summary>
        public virtual void AddPageUI()
        {
            AddPageDataInfo();  //数据信息
            AddPageNavi();      //页号导航
            AddPagePrev();      //首页、上一页
            AddPageNext();      //下一页、末页、Go
        }

        #region 添加记录数的显示
        /// <summary>
        /// 添加记录数等的显示
        /// </summary>
        public  virtual void AddPageDataInfo()
        {
            string recordCount = MyPager.RecordCount.ToString(CultureInfo.InvariantCulture);
            Int32 pageCount = MyPager.PageCount;

            string span1 = "<span class=\"" + MyPager.CssClass + "n\"> ";
            const string span2 = "</span>";
            //总记录数 共{0}条记录
            string str = string.Format(MyPager.PageUIAllCount, recordCount);
            var lc1 = new LiteralControl {ID = "p_data1", Text = span1 + str + span2};
            MyPager.Controls.Add(lc1);

            //第几/n页 第{0}/{1}页
            str = string.Format(MyPager.PageUIAllPage, MyPager.PageIndex, pageCount);
            lc1 = new LiteralControl {ID = "p_data2", Text = span1 + str + span2};
            MyPager.Controls.Add(lc1);
            //MyPager.Controls.Add(new LiteralControl("&nbsp; &nbsp;"));

            //一页的记录数 每页{0}条记录
            str = string.Format(MyPager.PageUIPageSize, MyPager.PageSize);
            lc1 = new LiteralControl {ID = "p_data3", Text = span1 + str + span2};
            MyPager.Controls.Add(lc1);

        }
        #endregion


        //=======================================
        #region 添加首页、上一页
        /// <summary>
        /// 添加首页、上一页 
        /// </summary>
        public virtual void AddPagePrev()
        {
            var str = new StringBuilder();

            Int32 tmpPageIndex = MyPager.PageIndex;
            //Int32 pageCount = MyPager.PageCount;

            #region 首页

            if (tmpPageIndex == 1)
            {
                str.Append("<span id=\"P_First\" class=\"disabled\">");
                str.Append(MyPager.PageUIFirst);
                str.Append("</span> ");
            }
            else
            {
                str.Append("<a id=\"P_First\"  class=\"" + MyPager.CssClass + "p\" ");
                str.Append(GetAHref("1"));
                str.Append(MyPager.PageUIFirst);
                str.Append("</a> ");
            }

            var lc1 = new LiteralControl {ID = "p_First", Text = str.ToString()};
            MyPager.Controls.Add(lc1);
            str.Length = 0;

            #endregion

            #region 上一页

            if (tmpPageIndex == 1)
            {
                str.Append("<span id=\"P_Prev\" class=\"disabled\">");
                str.Append(MyPager.PageUIPrev);
                str.Append("</span> ");
            }
            else
            {
                str.Append("<a id=\"P_Prev\" class=\"");
                str.Append(MyPager.CssClass + "p\" ");
                str.Append(GetAHref((tmpPageIndex - 1).ToString(CultureInfo.InvariantCulture)));
                str.Append(MyPager.PageUIPrev);
                str.Append("</a> ");
            }

            lc1 = new LiteralControl {ID = "P_Prev", Text = str.ToString()};
            MyPager.Controls.Add(lc1);
            str.Length = 0;

            #endregion

        }
        #endregion

        #region 添加下一页、末页、GO
        /// <summary>
        /// 添加下一页、末页、GO
        /// </summary>
        public virtual void AddPageNext()
        {
            var str = new StringBuilder();

            Int32 tmpPageIndex = MyPager.PageIndex;
            Int32 pageCount = MyPager.PageCount;

            #region 下一页
            if (tmpPageIndex == pageCount)
            {
                str.Append("<span id=\"P_Next\" class=\"disabled\">");
                str.Append(MyPager.PageUINext);
                str.Append("</span> ");
            }
            else
            {
                str.Append("<a id=\"P_Next\" class=\"" + MyPager.CssClass + "p\" ");
                str.Append(GetAHref((tmpPageIndex + 1).ToString(CultureInfo.InvariantCulture)));
                str.Append(MyPager.PageUINext);
                str.Append("</a> ");
            }
            
           
            var lc1 = new LiteralControl {ID = "P_Next", Text = str.ToString()};
            MyPager.Controls.Add(lc1);
            str.Length = 0;
            #endregion

            #region 末页
            if (tmpPageIndex == pageCount)
            {
                str.Append("<span id=\"P_Last\" class=\"disabled\">");
                str.Append(MyPager.PageUILast);
                str.Append("</span> ");
            }
            else
            {
                str.Append("<a id=\"P_Last\" class=\"" + MyPager.CssClass + "p\" ");
                str.Append(GetAHref(pageCount.ToString(CultureInfo.InvariantCulture)));
                str.Append(MyPager.PageUILast);
                str.Append("</a> ");
            }
            
          
            lc1 = new LiteralControl {ID = "P_Last", Text = str.ToString()};
            MyPager.Controls.Add(lc1);
            str.Length = 0;
            #endregion

            #region 文本框、GO
            if (MyPager.PageUIGO.Length > 0)
            {
                //BaseTextBox txt = new BaseTextBox();
                var txt = new HtmlInputText {Name = "QuickPagerTxtGo", ID = "Txt_GO"};
                //txt.Columns = 1;
                txt.Attributes.Add("size", "1");
                txt.Attributes.Add("class","cssTxt");
                MyPager.Controls.Add(txt);

                lc1 = new LiteralControl {ID = "P_Go"};
                GetGoText(str, txt);
                lc1.Text = str.ToString();
                MyPager.Controls.Add(lc1);
            }
            str.Length = 0;
            #endregion
        }
        #endregion

        #region 处理Go
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="txt"></param>
        public virtual void GetGoText(StringBuilder str, HtmlInputText txt)
        {
            //如果没有设置，则不显示GO的功能
            str.Append("  <a id=\"P_GO\"  class=\"" + MyPager.CssClass + "g\" "); 
            str.Append(GetAHref("document.getElementById('" + txt.ClientID + "').value"));
            str.Append(MyPager.PageUIGO);
            str.Append("</a>");
           
        }
        #endregion

        //导航
        #region 页面导航
        /// <summary>
        /// navigation
        /// </summary>
        public virtual void AddPageNavi()
        {
            string str = GetNavi();

            var lc1 = new LiteralControl {ID = "p_no", Text = str};
            MyPager.Controls.Add(lc1);

        }
        #endregion

        #region 生成导航的html
        /// <summary>
        /// 生成导航的html
        /// </summary>
        /// <returns></returns>
        private string GetNavi()
        {
            var str = new StringBuilder();
            
            Int32 tmpPageIndex = MyPager.PageIndex;

            Int32 noCount = MyPager.NaviCount;   //一组页号的数量

            if (noCount == 0) //0：不显示页面导航。
                return "";

            Int32 tmpMod = tmpPageIndex % noCount;  //余数;

            if (tmpMod == 0) tmpMod = noCount;

            if (tmpPageIndex > noCount )
            {
                //前导页
                str.Append("<a id=\"P_aa\" class=\"" + MyPager.CssClass + "N1\" ");
                str.Append(GetAHref((tmpPageIndex - tmpMod).ToString(CultureInfo.InvariantCulture)));
                str.Append("...</a> ");
            }

            Int32 i ;
            Int32 pCount = MyPager.PageCount;

            //Int32 cp = tmpPageIndex / noCount * noCount;


            for (i = tmpPageIndex - tmpMod + 1; i <= tmpPageIndex - tmpMod + noCount; i++)
            {
                if (tmpPageIndex == i)
                {
                    //当前页
                    str.Append("<a id=\"P_b" + i + "\"  class=\"current\" ");
                    str.Append(GetAHref((i).ToString(CultureInfo.InvariantCulture)));
                    str.Append(i);
                    str.Append("</a>");

                    //str.Append("<span class=\"current\">");
                    //str.Append(i);
                    //str.Append("</span>");
                }
                else
                {
                    //其他页
                    str.Append("<a id=\"P_b" + i + "\"  class=\"" + MyPager.CssClass + "N3\" ");
                    str.Append(GetAHref((i).ToString(CultureInfo.InvariantCulture)));
                    str.Append(i);
                    str.Append("</a>");
                }

                if (i >= pCount)
                {
                    i = Int32.MaxValue -2 ;
                }
            }

            if (i <= pCount)
            {
                //后导页
                str.Append("<a id=\"P_zz\"  class=\"" + MyPager.CssClass + "N4\" ");
                str.Append(GetAHref((i).ToString(CultureInfo.InvariantCulture)));
                str.Append("...</a> ");
            }
            return str.ToString();
        }


        #endregion

        //==========================================

        #region 修改UI
        /// <summary>
        /// 修改UI
        /// </summary>
        public virtual void UpdatePageUI()
        {
            UpdatePageDataInfo();  //数据信息
            UpdatePageNavi();      //页号导航
            UpdatePageText();      //修改上一页

        }

        #region 修改记录数的显示
        /// <summary>
        /// 修改记录数等的显示
        /// </summary>
        public virtual void UpdatePageDataInfo()
        {
            //总记录数 共{0}条记录
            string str = string.Format(MyPager.PageUIAllCount, MyPager.RecordCount);
            var lc1 = (LiteralControl)MyPager.FindControl("p_data1");
            lc1.Text = str;
           
            //第几/n页 第{0}/{1}页
            str = string.Format(MyPager.PageUIAllPage, MyPager.PageIndex, MyPager.PageCount);
            lc1 = (LiteralControl)MyPager.FindControl("p_data2");
            lc1.Text = str;
           
        }
        #endregion

        #region 修改首页、上一页、下一页、末页
        /// <summary>
        /// 修改首页、上一页、下一页、末页
        /// </summary>
        public virtual void UpdatePageText()
        {
            var str = new StringBuilder();

            Int32 tmpPageIndex = MyPager.PageIndex;
            Int32 pageCount = MyPager.PageCount;

            #region 首页
            str.Append("<a id=\"P_First\" class=\"" + MyPager.CssClass + "d\" ");
            str.Append(tmpPageIndex == 1 ? ">" : GetAHref("1"));

            str.Append(MyPager.PageUIFirst);
            str.Append("</a> ");

            var lc1 = (LiteralControl)MyPager.FindControl("p_First");
            lc1.Text = str.ToString();
            str.Length = 0;
            #endregion

            #region 上一页
            str.Append("<a id=\"P_Prev\"  class=\"" + MyPager.CssClass + "d\" ");
            str.Append(tmpPageIndex == 1 ? ">" : GetAHref((tmpPageIndex - 1).ToString(CultureInfo.InvariantCulture)));

            str.Append(MyPager.PageUIPrev);
            str.Append("</a> ");

            lc1 = (LiteralControl)MyPager.FindControl("P_Prev");
            lc1.Text = str.ToString();
            str.Length = 0;
            #endregion

            #region 下一页
            str.Append("<a id=\"P_Next\"  class=\"" + MyPager.CssClass + "d\" ");
            str.Append(tmpPageIndex == pageCount
                           ? ">"
                           : GetAHref((tmpPageIndex + 1).ToString(CultureInfo.InvariantCulture)));

            str.Append(MyPager.PageUINext);
            str.Append("</a> ");

            lc1 = (LiteralControl)MyPager.FindControl("P_Next");
            lc1.Text = str.ToString();
            str.Length = 0;
            #endregion

            #region 末页
            str.Append("<a id=\"P_Last\"  class=\"" + MyPager.CssClass + "d\" ");
            str.Append(tmpPageIndex == pageCount ? ">" : GetAHref(pageCount.ToString(CultureInfo.InvariantCulture)));

            str.Append(MyPager.PageUILast);
            str.Append("</a> ");

            lc1 = (LiteralControl)MyPager.FindControl("P_Last");
            lc1.Text = str.ToString();
            str.Length = 0;
            #endregion
             
        }
        #endregion

        #region 修改页面导航
        /// <summary>
        /// navigation
        /// </summary>
        public virtual void UpdatePageNavi()
        {
            string str = GetNavi();

            var lc1 = (LiteralControl)MyPager.FindControl("p_no");
            lc1.Text = str;

        }
        #endregion

        #endregion

        //==========================================
        #region 获取a 的 href 属性。
        /// <summary>
        /// 获取a 的 href 和 calss 属性。
        /// </summary>
        /// <returns></returns>
        public virtual string GetAHref(string pageIndex)
        {
            return "";      //在子类里面实现
        }
        #endregion

        #region 获取获取URL和参数，参数里去掉page=。
        /// <summary>
        /// 获取获取URL和参数，参数里去掉page=。
        /// </summary>
        /// <returns></returns>
        public virtual string GetURL()
        {
            string url = MyPager.Page.Request.Url.LocalPath;
            if (MyPager.Page.Request.Url.Query.Length > 0)
            {
                //有参数，追加
                string query = MyPager.Page.Request.Url.Query;
                string[] arrQuery = query.TrimStart('?').Split('&');

                query = "?";
                foreach (string s in arrQuery)
                {
                    if (s.IndexOf("page=", StringComparison.Ordinal) == -1)
                        query += s + "&";
                }
                url += query + "page=";
            }
            else
            {
                //没有参数
                url += "?page=";
            }

            return url;
        }
        #endregion

        #region 输出URL分页里go的js函数
        /// <summary>
        /// 输出URL分页里go的js函数
        /// </summary>
        /// <returns></returns>
        public string WriteGoJS()
        {
            string str = "";

            switch (MyPager.PagerTurnKind)
            {
                case PagerTurnKind.URLRewriter:
                    str = @"<script language='javascript'>
                           function QPGo(id, url) {
                               var me = document.getElementById(id);
                               var p = me.value; location.href = url.replace('{0}',p);
                           }</script> ";
                    break;

                case PagerTurnKind.URL:
                    str = @"<script language='javascript'>
                           function QPGo(id, url) {
                               var me = document.getElementById(id);
                               var p = me.value; location.href = url + p;
                           }</script> ";
                    break;

            }
            return str;


            //StringBuilder builder = new StringBuilder(500);
            //builder.Append("<script language=\"javascript\">\n\r");
            //builder.Append("function QPGo(id, url) {");
            //builder.Append("    var me = document.getElementById(id);");
            //builder.Append("    var p = me.value; location.href = url + p;");
            //builder.Append("}\n\r</script>\n\r");
          
            //return builder.ToString();
        }
        #endregion

        #region 输出js 
        /// <summary>
        /// 
        /// </summary>
        public string  WritePageJS()
        {
            var builder = new StringBuilder();
            builder.Append("<script language=\"javascript\">");
            builder.Append("function page(){");
            builder.Append("switch (event.srcElement.type){");
            builder.Append("\tcase \"text\": case \"hidden\":case \"password\":case \"file\":");
            builder.Append("\tcase \"textarea\": case \"checkbox\":case \"radio\":case \"select-one\":");
            builder.Append("\tcase \"select-multiple\": return;}\t");
            builder.Append("ikeyCode=window.event.keyCode;");
            builder.Append("switch(ikeyCode){");
            builder.Append("\tcase 37: case 33:");
            string clientID = MyPager.ClientID;
            builder.Append("\t\t__doPostBack('','-3');break;");
            builder.Append("\tcase 39: case 34:");
            builder.Append("\t\tdocument.getElementById(\"");
            builder.Append(clientID);
            builder.Append("_btn_Next\").click();break;");
            builder.Append("\tcase 36:");
            builder.Append("\t\tdocument.getElementById(\"");
            builder.Append(clientID);
            builder.Append("_btn_First\").click();break;");
            builder.Append("\tcase 35:");
            builder.Append("\t\tdocument.getElementById(\"");
            builder.Append(clientID);
            builder.Append("_btn_Last\").click();break;");
            builder.Append("}");
            builder.Append("txt = document.getElementById(\"");
            builder.Append(clientID);
            builder.Append("_txt_No\");");
            builder.Append("go = document.getElementById(\"");
            builder.Append(clientID);
            builder.Append("_btn_GO\");");
            builder.Append(" if (txt) {");
            builder.Append("  if (go) {");
            builder.Append("    switch(ikeyCode){");
            builder.Append("\t  case 49: case 97:txt.value = \"1\";go.click();break;");
            builder.Append("\t\tcase 50: case 98:txt.value = \"2\";go.click();break;");
            builder.Append("\t\tcase 51: case 99:txt.value = \"3\";go.click();break;");
            builder.Append("\t\tcase 52: case 100:txt.value = \"4\";go.click();break;");
            builder.Append("\t\tcase 53: case 101:txt.value = \"5\";go.click();break;");
            builder.Append("\t\tcase 54: case 102:txt.value = \"6\";go.click();break;");
            builder.Append("\t\tcase 55: case 103:txt.value = \"7\";go.click();break;");
            builder.Append("\t\tcase 56: case 104:txt.value = \"8\";go.click();break;");
            builder.Append("\t\tcase 57: case 105:txt.value = \"9\";go.click();break;");
            builder.Append("\t\tcase 48: case 96:txt.value = \"10\";go.click();break;");
            builder.Append("}}}");
            builder.Append("}document.body.focus();document.body.onkeydown=page;</script>\n\r");
            
            return builder.ToString();
        }
        #endregion

    }
}
