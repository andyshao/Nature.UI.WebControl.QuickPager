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
 * function: UI_AjaxForWebControl
 * history:  created by 金洋  
 * 2011-01-28 简单整理
 * ***********************************************/


namespace Nature.UI.WebControl.QuickPager.PagerUI
{
    /// <summary>
    /// ajax for服务器控件。
    /// </summary>
    public class AjaxForWebControl  : PageUI
    {
        /// <summary>
        /// 基于服务器控件的Ajax
        /// </summary>
        /// <param name="pager"></param>
        public AjaxForWebControl(QuickPager pager)
            : base(pager)
        {
        }

        /// <summary>
        /// 保存 a标签的属性
        /// </summary>
        private string _myHref = "";

        /// <summary>
        /// 设置a标签
        /// </summary>
        /// <param name="pageIndex">页号</param>
        /// <returns></returns>
        public override string GetAHref(string pageIndex)
        {
            if (_myHref.Length == 0)
            {
                string urlQuery = GetURL();
                string url = MyPager.Page.Request.Url.LocalPath;
                //myHref = "class=\"" + myPage.CssClass + "\" href=\"" + urlQuery + "{0}\" onclick=\"ajaxPager('" + url + "',{0},'" + this.myPage.ClientID + "Page');return false;\">";
                _myHref = " href=\"" + urlQuery + "{0}\" onclick=\"ajaxPager('" + url + "',{0},'" + MyPager.ClientID + "Page');return false;\">";
            }

            return string.Format(_myHref, pageIndex);

        }

        #region 获取获取URL和参数，参数里去掉page=。
        /// <summary>
        /// 获取获取URL和参数，参数里去掉page=。
        /// </summary>
        /// <returns></returns>
        public override string GetURL()
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
                    if (s.IndexOf("page=", System.StringComparison.Ordinal) == -1 && s.IndexOf("pagerKind=", System.StringComparison.Ordinal) == -1)
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

         
    }
}
