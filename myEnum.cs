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
 * function: 分页方式、运行方式、记录集的枚举定义
 * history:  created by 金洋  
 * 2011-01-28 整理
 * ***********************************************/

namespace Nature.UI.WebControl.QuickPager
{
    #region enum_分页控件的分页方式
    /// <summary>
    /// 分页控件的分页方式
    /// </summary>
    public enum PagerTurnKind
    {
        /// <summary>
        /// PostBack方式分页
        /// </summary>
        PostBack = 1,

        /// <summary>
        /// 伪URL分页Postback版。 
        /// </summary>
        PostBackURL = 12,

        /// <summary>
        /// URL的方式分页
        /// </summary>
        URL = 2,

        /// <summary>
        /// URL 重写的方式
        /// </summary>
        URLRewriter = 21,

        /// <summary>
        /// 触发js事件的方式分页。暂未实现功能 
        /// </summary>
        JS = 3,

        /// <summary>
        /// XMLHttp的方式分页，json格式传递数据。暂未实现功能
        /// </summary>
        Ajax = 4,

        /// <summary>
        /// XMLHttp的方式分页，for 服务器控件 。
        /// </summary>
        AjaxForWebControl = 41

    }
    #endregion

    #region enum_分页控件的运行方式
    /// <summary>
    /// 分页控件的运行方式
    /// </summary>
    public enum PagerRunKind
    {
        /// <summary>
        /// 自动，好比全自动洗衣机，加水、浸泡、洗涤、漂洗、甩干、烘干，自动运行。
        /// </summary>
        Auto = 1,

        /// <summary>
        /// 手动，自行设置如何获取数据、如何绑定控件等。
        /// </summary>
        Customer = 2

    }
    #endregion

    #region enum_数据载体类型
    /// <summary>
    /// 数据载体类型
    /// </summary>
    public enum DataSourceKind
    {
         
        /// <summary>
        /// 提取数据到DataTable
        /// </summary>
        DataTable = 0,

        /// <summary>
        /// 提取数据到WebList1，无需反射
        /// </summary>
        WebList1 = 1,

        /// <summary>
        /// 提取数据到WebList2，无需反射
        /// </summary>
        WebList2 = 2,

        /// <summary>
        /// 提取数据到任意实体类。通过反射填充属性值。暂未实现
        /// </summary>
        Entity = 3

    }
    #endregion
}
