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
 * function: 分页控件的事件处理和控件绑定
 * history:  created by 金洋  
 * 2011-01-28 简单整理
 * ***********************************************/

using System;
using System.ComponentModel;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nature.Common;

namespace Nature.UI.WebControl.QuickPager
{
    /// <summary>
    /// 分页控件的事件处理
    /// PageGetData.cs 负责提取数据
    /// PageSQL.cs 负责生成SQl语句
    /// PageUI.cs 负责分页控件的页面内容
    /// </summary>
    public partial class QuickPager          //INamingContainer
    {
        #region ==============================事件==============================

        #region 定义委托
        /// <summary>
        /// 定一个委托，翻页的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void EventPageChange(Object sender, PageArgs e);

        /// <summary>
        /// 事件用
        /// </summary>
        protected static readonly object EventChange = new object();

        /// <summary>
        /// 事件用
        /// </summary>
        protected static readonly object EventBind = new object();

        /// <summary>
        /// 事件用
        /// </summary>
        protected static readonly object EventPreBind = new object();

        #endregion

        #region 定义事件
        #region 用户单击页号后，触发的事件，在绑定显示数据的控件之前触发
        /// <summary>
        /// 用户单击页号后，触发的事件，在绑定显示数据的控件之前触发
        /// </summary>
        [Description("页号改变的时候触发，绑定控件前触发")]
        public event EventPageChange PageChanged
        {
            add
            {
                Events.AddHandler(EventChange, value);
            }
            remove
            {
                Events.RemoveHandler(EventChange, value);
            }
        }
        #endregion

        #region 用户单击页号后，并且绑定显示数据的控件之后触发
        /// <summary>
        /// 用户单击页号后，并且绑定显示数据的控件之后触发
        /// </summary>
        [Description("在自动提取数据的方式下，绑定控件后触发")]
        public event EventPageChange GridBinded
        {
            add
            {
                Events.AddHandler(EventBind, value);
            }
            remove
            {
                Events.RemoveHandler(EventBind, value);
            }
        }
        #endregion

        #region 用户单击页号后，并且绑定显示数据的控件之后触发
        /// <summary>
        /// 用户单击页号后，并且绑定显示数据的控件之后触发
        /// </summary>
        [Description("在自动提取数据的方式下，获取记录后，绑定控件前触发")]
        public event EventPageChange PreGridBind
        {
            add
            {
                Events.AddHandler(EventPreBind, value);
            }
            remove
            {
                Events.RemoveHandler(EventPreBind, value);
            }
        }
        #endregion
        #endregion

        #region 内部调用
        /// <summary>
        /// 用户单击页号后，触发的事件，在绑定显示数据的控件之前触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnPageChange(object sender, PageArgs e)
        {
            var hd = (EventPageChange)Events[EventChange];
            if (hd != null)
                hd(sender, e);
        }

        /// <summary>
        /// 在绑定数据显示控件之后触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnGridBinded(object sender, PageArgs e)
        {
            var hd = (EventPageChange)Events[EventBind];
            if (hd != null)
                hd(sender, e);
        }

         /// <summary>
        /// 在提取数据后，绑定数据显示控件之前触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnPreGridBind(object sender, PageArgs e)
        {
            var hd = (EventPageChange)Events[EventPreBind];
            if (hd != null)
                hd(sender, e);
        }
        #endregion

        #region 分页控件的回发事件
        /// <summary>
        /// 分页控件的回发事件 
        /// </summary>
        /// <param name="pageIndex">要翻到的页号</param>
        public void RaisePostBackEvent(string pageIndex)
        {
            Int32 tmpPageIndex = 1; //不是数字，显示第一页
            if (Functions.IsInt(pageIndex))
                tmpPageIndex = Int32.Parse(pageIndex);

            PagerClick(tmpPageIndex);
        }
        #endregion

        #region 响应分页事件
        /// <summary>
        /// 响应分页事件
        /// </summary>
        private void PagerClick(Int32 tmpPageIndex)
        {
            //判断页号是否超出有效范围
            if (tmpPageIndex < 0)
                tmpPageIndex = 1;

            if (tmpPageIndex > PageCount)
                tmpPageIndex = PageCount;

            //定义一个事件里的参数
            var e = new PageArgs {OldPageIndex = PageIndex, CurrentPageIndex = tmpPageIndex};

            PageIndex = tmpPageIndex;

            //触发（调用）外部的事件
            OnPageChange(this, e);


            if (PagerRunKind == PagerRunKind.Auto)
            {
                //自动提取数据的方式
                if (tmpPageIndex == 1)
                {
                    //统计总记录数和总页数
                    ComputeRecordCount();
                }
                
                //绑定控件
                DataBind(tmpPageIndex,e);
                
            }

            //显示UI
            ManagerPageUI.AddPageUI();


        }
        #endregion

        #endregion

        #region ==============================函数==============================

        /// <summary>
        /// 计算总记录数，和页数
        /// </summary>
        private void ComputeRecordCount()
        {
            //计算总记录数，和页数
            string sql = PagerSql.GetRecordCountSQL;            //获取SQL语句
            string strRecordCount = Dal.ExecuteString(sql);     //执行SQL语句
            if (Dal.ErrorMessage.Length > 0)
            {
                Functions.MsgBox("计算总记录数时出现异常，请查看属性设置情况！", true);
                return ;
            }

            int intRecordCount = Int32.Parse(strRecordCount);   
            RecordCount = intRecordCount;                  //设置总记录数，必须先设置，否则无法计算总页数
            PagerSql.ComputePageCount();     //计算页数int intPageCount = 

            
            //this.PagerSQL.PageCount = intPageCount;             //设置总页数

        }

        /// <summary>
        /// 显示第一页的数据，可以用于添加记录
        /// </summary>
        public void BindFirstPage()
        {
            //生成SQL语句和获取记录总数
            //this.ManagerPageSQL.CreateSQL();

            ComputeRecordCount();

            //绑定第一页的数据
            DataBind(1,new PageArgs());

            ManagerPageUI.UpdatePageUI();  //修改导航

        }

        /// <summary>
        /// 显示当前页的数据，重新计算记录数，可以用于添加、删除数据
        /// </summary>
        public void BindThisPageForAddDelete()
        {
            //生成SQL语句和获取记录总数
            //this.ManagerPageSQL.CreateSQL();

            ComputeRecordCount();

            //绑定当前页的数据
            DataBind(PageIndex, new PageArgs());

            ManagerPageUI.UpdatePageUI();  //修改导航
        }

        /// <summary>
        /// 显示当前页的数据，不重新计算记录数，可以用于修改记录
        /// </summary>
        public void BindThisPage()
        {
            //绑定第一页的数据
            DataBind(PageIndex, new PageArgs());

            ManagerPageUI.UpdatePageUI();  //修改导航
        }
        #endregion

        #region 自定义获取数据的时候，重新显示PagerUI的时候调用的函数。
        /// <summary>
        /// 自定义获取数据的时候，或者使用URL分页的时候，重新显示PagerUI的时候调用的函数。
        /// </summary>
        public void SetPagerUI()
        {
            ManagerPageUI.UpdatePageUI();  //修改导航
        }
        #endregion

        #region 绑定控件
        /// <summary>
        /// 绑定控件
        /// </summary>
        public void DataBind(Int32 tmpPageIndex, PageArgs e)
        {
            //查找显示数据的控件

            PageIndex = tmpPageIndex;

            if (tmpPageIndex < 0)
                tmpPageIndex = 0;

            //获取提取记录的SQL
            string sql = PagerSql.GetSQLByPageIndex(tmpPageIndex);
            //this.ManagerPageSQL.GetPageSQL = sql;

            //获取DataTable
            DataTable dt = Dal.ExecuteFillDataTable(sql);

            //调用外部事件
            OnPreGridBind(this, e);

            if (ShowDataControl == null)
            {
                //没有设置控件实例，通过ID查找控件，然后绑定
                string ctrlIDs = ShowDataControlIDs;
                if (ctrlIDs == null) //没有设置显示数据的控件的ID值
                    return;

                //一个分页控件可以绑定多个显示数据的控件
                string[] ctrlID = ctrlIDs.Split(',');
                foreach (string a in ctrlID)
                {
                    DataBind( base.Page.FindControl(a), dt);
                }

            }
            else
            {
                //设置了 显示数据的对象，直接绑定
                DataBind(ShowDataControl, dt);
            }

            //触发（调用）外部的事件
            OnGridBinded(this, e);

            //this.ManagerPageUI.AddPageUI();  //添加页面显示
            
        }
        #endregion

        #region 绑定显示数据的控件
        /// <summary>
        /// 绑定显示数据的控件
        /// </summary>
        /// <param name="control">显示数据的控件</param>
        /// <param name="dt">数据源，目前只支持DataTable和DataSet</param>
        private void DataBind(Control control, MarshalByValueComponent dt)
        {
            if (control == null)
                return;

            //为什么没有一个统一的DataSource呢？
            //GridView等 
            var tmpBaseDataBoundControl = control as BaseDataBoundControl;
            if (tmpBaseDataBoundControl != null)
            {
                tmpBaseDataBoundControl.DataSource = dt;
                tmpBaseDataBoundControl.DataBind();
                return;
            }
            
            //Repeater
            var tmpRepeater = control as Repeater;
            if (tmpRepeater != null)
            {
                tmpRepeater.DataSource = dt;
                tmpRepeater.DataBind();
                return;
            }
            
            //DataGrid、DataList
            var tmpBaseDataList = control as BaseDataList;
            if (tmpBaseDataList != null)
            {
                tmpBaseDataList.DataSource = dt;
                tmpBaseDataList.DataBind();
            }
         
            //不在判断范围内，退出

        }
        #endregion

    }

}