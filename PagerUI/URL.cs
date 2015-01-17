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
 * function: UI_URL
 * history:  created by 金洋  
 * 2011-01-28 简单整理
 * ***********************************************/


using System.Text;
using System.Web.UI.HtmlControls;

namespace Nature.UI.WebControl.QuickPager.PagerUI
{
    /// <summary>
    /// URL 分页
    /// </summary>
    public class URL : PageUI
    {
        /// <summary>
        /// URL 分页
        /// </summary>
        /// <param name="pager"></param>
        public URL(QuickPager pager)
            : base(pager)
        {
        }
        /// <summary>
        /// 保存 a标签的属性
        /// </summary>
        private string _myHref = "";
        /// <summary>
        /// URL里获取a的href
        /// </summary>
        /// <param name="pageIndex">页号</param>
        /// <returns></returns>
        public override string GetAHref(string pageIndex)
        {
            if (_myHref.Length == 0)
            {
                string url = GetURL();

                _myHref = " href=\"" + url + "{0}\" >";
            }
            
            return string.Format(_myHref,pageIndex );
           
        }

        /// <summary>
        /// 设置Go的样子
        /// </summary>
        /// <param name="str"></param>
        /// <param name="txt"></param>
        public override void GetGoText(StringBuilder str, HtmlInputText txt)
        {
            string url = GetURL();

            str.Append("\n<a id=\"P_GO\" ");
            
            str.Append("onclick=\"QPGo('");
            str.Append(txt.ClientID);
            str.Append("','");
            str.Append(url );
            str.Append( "')\" style=\"cursor:hand;\">");

            str.Append(MyPager.PageUIGO);
            str.Append("</a>");
           
        }

       
    }
}
