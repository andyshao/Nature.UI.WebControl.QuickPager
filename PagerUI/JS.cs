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
 * function: UI_JS
 * history:  created by 金洋  
 * 2011-01-28 简单整理
 * ***********************************************/


namespace Nature.UI.WebControl.QuickPager.PagerUI
{
    /// <summary>
    /// 一种简单的，触发js事件的方式。只是现在还没有实现功能。
    /// </summary>
    public class JS : PageUI
    {
        /// <summary>
        /// 触发js事件的分页方式
        /// </summary>
        /// <param name="pager"></param>
        public JS(QuickPager pager)
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
                _myHref = "class=\"" + MyPager.CssClass + "\" href=\"javascript:jsClick('{0}')\">";
            }

            return string.Format(_myHref, pageIndex);
        }

    }
}
