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
 * function: 分页控件的属性
 * history:  created by 金洋  
 * 2011-01-28 简单整理
 * 2011-04-19 去掉了 FunctionID 属性
 ************************************************/

using System;
using System.ComponentModel;
using Nature.Common;

namespace Nature.UI.WebControl.QuickPager
{
    /// <summary>
    /// 分页控件
    /// PageGetData.cs 负责提取数据
    /// PageSQL.cs 负责生成SQl语句
    /// PageUI.cs 负责分页控件的页面内容
    /// </summary>
    public partial class QuickPager  
    {
        //属性

        #region 数据访问
        /// <summary>
        /// 访问数据库用的实例
        /// </summary>
        protected Data.IDal _Dal ;
        /// <summary>
        /// 访问数据库用的实例
        /// </summary>
        public Data.IDal Dal
        {
            get { return _Dal; }
            set { _Dal = value; }
        }
        #endregion

        #region 模块ID
        ///// <summary>
        ///// 模块ID
        ///// </summary>
        //[Bindable(true)]
        //[Category("配置信息")]
        //[Localizable(true)]
        //[Description("模块ID，用于提取配置信息")]
        //public string FunctionID
        //{
        //    set { ViewState["_FunctionID"] = value; }
        //    get { return (string)(ViewState["_FunctionID"] ?? ""); }
        //}
        #endregion

        #region 设置UI显示数据的控件
        /// <summary>
        /// 设置显示数据的控件
        /// </summary>
        public System.Web.UI.Control ShowDataControl { get; set; }
        #endregion

        #region 显示数据的控件的ID
        /// <summary>
        /// 显示数据的控件ID
        /// </summary>
        [Bindable(true)]
        [Category("显示数据的控件ID")]
        [DefaultValue("")]
        [Localizable(true)]
        public string ShowDataControlIDs
        {
            set { ViewState["ShowDataControlIDs"] = value; }
            get { return (string)ViewState["ShowDataControlIDs"]; }
        }
        #endregion


        #region 在用户控件里面使用的时候，需要设置。
        private System.Web.UI.UserControl _uc;
        /// <summary>
        /// 在用户控件里面使用的时候，需要设置。
        /// </summary>
        public System.Web.UI.UserControl UserControl
        {
            get { return _uc; }
            set { _uc = value; }
        }
        #endregion

        #region 运行方式的设置

        #region 分页控件采用的分页方式
        /// <summary>
        /// 分页控件采用的分页方式
        /// </summary>
        [Bindable(true)]
        [Category("运行方式")]
        [Localizable(true)]
        [Description("分页方式：PostBack；URL")]
        public PagerTurnKind PagerTurnKind
        {
            set { ViewState["SetPageTurnKind"] = value; }
            get { return (PagerTurnKind)(ViewState["SetPageTurnKind"] ?? PagerTurnKind.PostBack); }
        }
        #endregion

        #region 分页控件采用的运行方式
        /// <summary>
        /// 分页控件采用的运行方式
        /// </summary>
        [Bindable(true)]
        [Category("运行方式")]
        [Localizable(true)]
        [Description("数据提取方式：自动；手动")]
        public PagerRunKind PagerRunKind
        {
            set {  ViewState["SetRunKind"] = value; }
            get
            {
                if (ViewState["SetRunKind"] == null)
                    return PagerRunKind.Auto;
                return (PagerRunKind) ViewState["SetRunKind"];
            }
        }
        #endregion

        #region 分页控件采用的数据提取方式
        /// <summary>
        /// 分页控件采用的数据提取方式
        /// </summary>
        [Bindable(true)]
        [Category("提取数据的方式")]
        [Localizable(true)]
        [Description("数据提取方式：自动；自定义")]
        public bool IsBindControl
        {
            set { ViewState["IsBindControl"] = value; }
            get
            {
                if (ViewState["IsBindControl"] == null)
                {
                    ViewState["IsBindControl"] = true;
                    return true;
                }

                return (bool)ViewState["IsBindControl"];
            }
        }
        #endregion

        #endregion

        #region 总记录数
        /// <summary>
        /// 总记录数
        /// </summary>
        [Bindable(true)]
        [Category("记录集")]
        [DefaultValue("")]
        [Localizable(true)]
        [Description("设置总记录数")]
        public Int32 RecordCount
        {
            set {
                ViewState["RecordCount"] = value;
                if (PagerSql != null)
                    PagerSql.RecordCount = value; }
            get
            {
                //没有设置的话，返回 0 
                if (ViewState["RecordCount"] == null)
                {
                    if (PagerSql == null)
                        return 0;

                    return PagerSql.RecordCount;
                }

                return (Int32) ViewState["RecordCount"];
            }
        }
        #endregion

        #region 是否直接设置总记录数
        /// <summary>
        /// 直接设置总记录数，true：直接设置记录数；false：需要到数据库里面统计
        /// </summary>
        [Bindable(true)]
        [Category("记录集")]
        [DefaultValue("")]
        [Localizable(true)]
        [Description("是否直接设置总记录数")]
        public bool IsSetRecordCount
        {
            set { ViewState["IsSetRecordCount"] = value; }
            get { return (bool)(ViewState["IsSetRecordCount"] ?? false); }
        }
        #endregion

        #region 一页的记录数
        /// <summary>
        /// 一页的记录数
        /// </summary>
        [Bindable(true)]
        [Category("记录集")]
        [DefaultValue("")]
        [Localizable(true)]
        [Description("一页显示的记录数，默认20条")]
        public Int32 PageSize
        {
            set
            {
                if (PagerSql != null)
                    PagerSql.PageSize = value < 0 ? 20 : value;

                ViewState["PageSize"] = value;
            }
            get
            {
                //没有设置的话，使用默认值：一页20条记录
                if (ViewState["PageSize"] == null)
                {
                    if (PagerSql != null)
                    {
                        //没有设置外部的PageSize
                        ViewState["PageSize"] = PagerSql.PageSize;
                        return PagerSql.PageSize;
                    }

                    return 20;
                }

                return (Int32) ViewState["PageSize"];
            }
        }
        #endregion

        #region 总页数
        /// <summary>
        /// 总页数
        /// </summary>
        [Bindable(true)]
        [Category("记录集")]
        [DefaultValue("")]
        [Localizable(true)]
        [Description("总页数")]
        public Int32 PageCount
        {
            set
            {
                ViewState["PageCount"] = value;
                if (PagerSql != null)
                    PagerSql.PageCount = value;
            }
            get
            {
                //没有设置的话，返回 0 
                if (ViewState["PageCount"] == null)
                    return PagerSql == null ? 1 : PagerSql.PageCount;

                return (Int32) ViewState["PageCount"];
            }
        }
        #endregion

        #region 当前的页号
        /// <summary>
        /// 当前的页号
        /// </summary>
        [Bindable(true)]
        [Category("记录集")]
        [DefaultValue("")]
        [Localizable(true)]
        [Description("当前的页号")]
        public Int32 PageIndex
        {
            set {
                ViewState["PageIndex"] = value; 
                if (PagerSql != null)
                    PagerSql.PageIndex = value;
            }
            get
            {
                int pageIndex = 1;
                string index;

                switch (PagerTurnKind)
                {
                    case PagerTurnKind.URLRewriter:
                    case PagerTurnKind.URL:

                        #region URL 分页

                        index = System.Web.HttpContext.Current.Request.QueryString["page"];
                        if (Functions.IsInt(index))
                        {
                            //整数，是页号
                            pageIndex = int.Parse(index);
                        }
                        else
                        {
                            //默认为第一页。
                            if (PagerSql != null)
                                PagerSql.PageIndex = 1;

                            pageIndex = 1;
                        }

                        #endregion

                        break;

                    case PagerTurnKind.PostBack:
                    case PagerTurnKind.PostBackURL:
                    case PagerTurnKind.AjaxForWebControl:

                        #region postback

                        if (ViewState["PageIndex"] == null)
                        {
                            //判断URL
                            index = System.Web.HttpContext.Current.Request.QueryString["page"];
                            if (Functions.IsInt(index))
                            {
                                //整数，是页号
                                pageIndex = int.Parse(index);
                            }
                            else
                            {
                                if (PagerSql == null)
                                    pageIndex = 1;
                                else
                                {
                                    pageIndex = PagerSql.PageIndex;
                                    ViewState["PageIndex"] = pageIndex;
                                }
                            }
                        }
                        else
                        {
                            pageIndex = (Int32) ViewState["PageIndex"];
                        }

                        #endregion

                        break;
                }

                return pageIndex <= 0 ? 1 : pageIndex;
            }
        }
        #endregion

        #region URL重写时需要设置，显示的网页名称
        /// <summary>
        ///  URL重写时需要设置，显示的网页名称。
        ///  比如：list{0}.aspx
        /// </summary>
        [Bindable(true)]
        [Category("URL重写")]
        [DefaultValue("list{0}.aspx")]
        [Localizable(true)]
        [Description("重写时静态页的名称。list{0}.aspx，{0}会替换为页号")]
        public string UrlRewritePattern
        {
            set { ViewState["UrlRewritePattern"] = value; }
            //没有设置的话，使用默认值：list{0}.aspx
            get { return (string)(ViewState["UrlRewritePattern"] ?? "list{0}.aspx"); }
        }
        #endregion

        //UI
        #region css
        /// <summary>
        /// 设置默认的css样式名
        /// </summary>
        public override string CssClass
        {
            set { base.CssClass = value; }
            get
            {
                if (base.CssClass.Length == 0)
                    base.CssClass = "pager";
                return base.CssClass;
            }
        }
        #endregion

        #region 设置皮肤
        private string _skin = "default";   //默认文件夹
        /// <summary>
        /// 设置
        /// </summary>
        [Bindable(true)]
        [Category("页面显示")]
        [DefaultValue("default")]
        [Localizable(true)]
        public string Skin
        {
            set { _skin = value; }
            get { return _skin; }
        }
        #endregion

        #region 页面显示 上一页、下一页、页号导航

        #region 上一页
        private string _pPrevText = "上一页";   //上一页
        /// <summary>
        /// 上一页
        /// </summary>
        [Bindable(true)]
        [Category("页面显示")]
        [DefaultValue("上一页")]
        [Localizable(true)]
        public string PageUIPrev
        {
            get { return _pPrevText; }
            set { _pPrevText = value; }
        }
        #endregion

        #region 下一页
        private string _pNextText = "下一页";   //下一页
        /// <summary>
        /// 下一页
        /// </summary>
        [Bindable(true)]
        [Category("页面显示")]
        [DefaultValue("下一页")]
        [Localizable(true)]
        public string PageUINext
        {
            get { return _pNextText; }
            set { _pNextText = value; }
        }
        #endregion

        #region 首页
        private string _pFirstText = "首页";   //首页
        /// <summary>
        /// 首页 
        /// </summary>
        [Bindable(true)]
        [Category("页面显示")]
        [DefaultValue("首页")]
        [Localizable(true)]
        [Description("首页的显示的文字")]
        public string PageUIFirst
        {
            get { return _pFirstText; }
            set { _pFirstText = value; }
        }
        #endregion

        #region 末页
        private string _pLastText = "末页";   //末页
        /// <summary>
        /// 末页
        /// </summary>
        [Bindable(true)]
        [Category("页面显示")]
        [DefaultValue("末页")]
        [Localizable(true)]
        public string PageUILast
        {
            get { return _pLastText; }
            set { _pLastText = value; }
        }
        #endregion

        #region GO
        private string _pGoText = "GO";   //GO
        /// <summary>
        /// GO 去指定页号 的文字
        /// </summary>
        [Bindable(true)]
        [Category("页面显示")]
        [DefaultValue("")]
        [Localizable(true)]
        public string PageUIGO
        {
            get { return _pGoText; }
            set { _pGoText = value; }
        }
        #endregion
        
        #region 页面导航的数量
        /// <summary>
        /// 页面导航的数量
        /// </summary>
        public Int32 NaviCount
        {
            set { ViewState["NaviCount"] = value; }
            get { return (Int32)(ViewState["NaviCount"] ?? 10); }
        }
        #endregion
        
        #endregion

        #region 页面显示 记录数、页数

        #region 共{0}条记录
        /// <summary>
        /// 共{0}条记录
        /// </summary>
        [Bindable(true)]
        [Category("记录信息")]
        [DefaultValue("")]
        [Localizable(true)]
        public string PageUIAllCount
        {
            set { ViewState["PageUIAllCount"] = value; }
            get
            {
                if (ViewState["PageUIAllCount"] == null)
                    return "共<font style=\"color:Red;\">{0}</font>条记录";

                return ViewState["PageUIAllCount"].ToString();
            }
        }
        #endregion

        #region 第{0}/{1}页
        /// <summary>
        /// 第{0}/{1}页
        /// </summary>
        [Bindable(true)]
        [Category("记录信息")]
        [DefaultValue("")]
        [Localizable(true)]
        public string PageUIAllPage
        {
            set { ViewState["PageUIAllPage"] = value; }
            get
            {
                if (ViewState["PageUIAllPage"] == null)
                    return "第<font id=\"f_Index\" style=\"color:Red;\">{0}</font>/<font  id=\"f_PageCount\" style=\"color:Red;\">{1}</font>页";

                return ViewState["PageUIAllPage"].ToString();
            }
        }
        #endregion

        #region 每页{0}条记录
        /// <summary>
        /// 每页{0}条记录
        /// </summary>
        [Bindable(true)]
        [Category("记录信息")]
        [DefaultValue("")]
        [Localizable(true)]
        public string PageUIPageSize
        {
            set { ViewState["PageUIAllPageCount"] = value; }
            get
            {
                if (ViewState["PageUIAllPageCount"] == null)
                    return "每页<font style=\"color:Red;\">{0}</font>条记录";

                return ViewState["PageUIAllPageCount"].ToString();
            }
        }
        #endregion

        #endregion

   
    }
}