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
 * function: 分页控件，直接获取记录集
 * history:  created by 金洋 2010-8-19 13:25:55 
 * 2011-01-28 简单整理
 * ***********************************************/

using System;
using System.Collections.Generic;
using System.Data;
using Nature.Common;
using Nature.Data.Model;

namespace Nature.UI.WebControl.QuickPager
{
    public partial class QuickPager  
    {
        /// <summary>
        /// 获取当前页的记录集，用DataTable装载
        /// </summary>
        /// <returns></returns>
        public DataTable GetDataTable()
        {
            //初始化SQL
            SetSql();

            int tmpPageIndex = PageIndex ;

            //获取记录集
            string sql = PagerSql.GetSQLByPageIndex(tmpPageIndex);

            DataTable dt = Dal.ExecuteFillDataTable(sql);

            return dt;
        }

        /// <summary>
        /// 获取当前页的记录集，用WebList2装载
        /// </summary>
        /// <returns></returns>
        public IList<WebList2> GetWebList2(WebList2Format lstFormat)
        {
            //初始化SQL
            SetSql();

            int tmpPageIndex = PageIndex;

            //获取记录集
            string sql = PagerSql.GetSQLByPageIndex(tmpPageIndex);

            IList<WebList2> lst = Dal.ExecuteFillWebList2(sql, lstFormat);

            return lst;
        }

        /// <summary>
        /// SQL 语句的初始化
        /// </summary>
        private void SetSql()
        {
            //获取URL 里面的分页参数
            string tmpPageIndex = System.Web.HttpContext.Current.Request.QueryString["page"];

            Int32 intPageIndex = 1;
            if (!string.IsNullOrEmpty(tmpPageIndex))
            {
                if (Functions.IsInt(tmpPageIndex))
                    intPageIndex = Int32.Parse(tmpPageIndex);
            }

            PageIndex = intPageIndex;

            //生成SQL语句和获取记录总数
            PagerSql.CreateSQL();

            //统计总记录数和总页数
            ComputeRecordCount();

        }
    }
}
