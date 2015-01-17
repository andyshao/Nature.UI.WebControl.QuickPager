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

using System;

namespace Nature.UI.WebControl.QuickPager
{
    /// <summary>
    /// 分页控件用的参数
    /// </summary>
    [Serializable]
    public class PageArgs : EventArgs
    {
        /// <summary>
        /// 翻页前的页号，不知道这个有没有用
        /// </summary>
        public Int32 OldPageIndex;

        /// <summary>
        /// 想要翻到的页号
        /// </summary>
        public Int32 CurrentPageIndex;
        
    }
}
