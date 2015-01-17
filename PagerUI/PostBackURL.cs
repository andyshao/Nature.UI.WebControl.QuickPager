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
 * function: UI_PostBackURL
 * history:  created by 金洋  
 * 2011-01-28 简单整理
 * ***********************************************/


namespace Nature.UI.WebControl.QuickPager.PagerUI
{
    /// <summary>
    /// 本质是PostBack分页，但是要伪装成URL分页，以便于搜索引擎。
    /// PostBack分页方式里的 a 的 href 属性
    /// </summary>
    public class PostBackURL : PageUI
    {
        /// <summary>
        /// 伪URL分页，Postback版
        /// </summary>
        /// <param name="pager"></param>
        public PostBackURL(QuickPager pager)
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
                string url = base.GetURL();
                _myHref = " href=\"" + url + "{0}\" onclick=\"__doPostBack('" + MyPager.ClientID + "',{0});return false;\">";
            }

            return string.Format(_myHref, pageIndex);

        }

         
    }
}
