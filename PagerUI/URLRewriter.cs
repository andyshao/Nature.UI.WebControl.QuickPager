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
 * function: 分页事件的参数类型的定义
 * history:  created by 金洋  
 * 2011-01-28 简单整理
 * ***********************************************/

using System.Text;
using System.Web.UI.HtmlControls;

namespace Nature.UI.WebControl.QuickPager.PagerUI
{
    /// <summary>
    /// URL 重写
    /// </summary>
    public class URLRewriter : PageUI
    {
        /// <summary>
        /// URL 重写
        /// </summary>
        /// <param name="pager"></param>
        public URLRewriter(QuickPager pager)
            : base(pager)
        {
        }
        /// <summary>
        /// 保存 a标签的属性
        /// </summary>
        private string _myHref = "";
        /// <summary>
        /// 设置a的href
        /// </summary>
        /// <param name="pageIndex">页号</param>
        /// <returns></returns>
        public override string GetAHref(string pageIndex)
        {
            if (_myHref.Length == 0)
            {
                string query = GetQuery();

                string url = MyPager.UrlRewritePattern;

                _myHref = " href=\"" + url + query + "\" >";
            }

            return string.Format(_myHref, pageIndex);

        }

        /// <summary>
        /// Go的样子
        /// </summary>
        /// <param name="str"></param>
        /// <param name="txt"></param>
        public override void GetGoText(StringBuilder str, HtmlInputText txt)
        {
            string query = GetQuery();

            string url = MyPager.UrlRewritePattern + query;

            str.Append("\n<a id=\"P_GO\" ");

            str.Append("onclick=\"QPGo('");
            str.Append(txt.ClientID);
            str.Append("','");
            str.Append(url);
            str.Append("')\" style=\"cursor:hand;\">");

            str.Append(MyPager.PageUIGO);
            str.Append("</a>");

        }

        private string GetQuery()
        {
            string query = "";
            if (MyPager.Page.Request.Url.Query.Length > 0)
            {
                //有参数，追加
                query = MyPager.Page.Request.Url.Query;
                string[] arrQuery = query.TrimStart('?').Split('&');

                query = "?";
                foreach (string s in arrQuery)
                {
                    if (s.IndexOf("page=", System.StringComparison.Ordinal) == -1)
                        query += s + "&";
                }
                query = query.TrimEnd('&');
                 
            }

            if (query == "?")
                query = "";

            return query;
        }
    }
}
