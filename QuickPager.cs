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
 * function: 分页控件的自定义控件
 * history:  created by 金洋  
 * 2011-01-28 简单整理
 * ***********************************************/


using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nature.Common;
using Nature.UI.WebControl.QuickPager.PagerUI;
using Nature.UI.WebControl.QuickPagerSQL;

namespace Nature.UI.WebControl.QuickPager
{
    /// <summary>
    /// 分页控件
    /// PageGetData.cs 负责提取数据
    /// PageSQL.cs 负责生成SQl语句
    /// PageUI.cs 负责分页控件的页面内容
    /// </summary>
    [DefaultProperty("Text")]
    [ToolboxData("<Nature:QuickPager runat=server></Nature:QuickPager>")]
    //[assembly: WebResource(SpreadtrumControl1.UploadImgThumb.SetupJs, "text/javascript")] 
    public partial class QuickPager : System.Web.UI.WebControls.WebControl, IPostBackEventHandler      //INamingContainer
    {
        #region ==============================成员==============================
        
        /// <summary>
        /// 生成SQL语句的部分
        /// </summary>
        private PagerSQL _pagerSql;
       
        /// <summary>
        /// 提取数据的部分
        /// </summary>
        private PageUI _pagerUI ;
        #endregion

        #region ==============================管理部分==========================

        #region 分页算法的实例
        /// <summary>
        /// 分页算法的实例
        /// </summary>
        public PagerSQL PagerSql
        {
            set { _pagerSql = value; _pagerSql.Page = base.Page; }
            get
            {
                if (_pagerSql == null)
                {
                    if (PagerRunKind == PagerRunKind.Customer)
                        return null;        //自定义方式不提供分页算法

                    if (_pagerSql == null)
                    {
                        _pagerSql = new PagerSQL {Page = base.Page};
                    }
                }
                return _pagerSql;
            }
        }
        #endregion

        #region 分页方式的实例
        /// <summary>
        /// 分页方式的实例
        /// </summary>
        public PageUI ManagerPageUI
        {
            set { _pagerUI = value; }
            get
            {
                if (_pagerUI == null)
                {
                    switch (PagerTurnKind)
                    {
                        case PagerTurnKind.PostBack:
                            _pagerUI = new PostBack(this);
                            break;

                        case PagerTurnKind.PostBackURL:
                            _pagerUI = new PostBackURL(this);
                            break;

                        case PagerTurnKind.URLRewriter :
                            _pagerUI = new URLRewriter(this);
                            break;
                        
                        case PagerTurnKind.URL:
                            _pagerUI = new URL(this);
                            break;

                        case PagerTurnKind.JS :
                            _pagerUI = new JS(this);
                            break;
                        case PagerTurnKind.AjaxForWebControl :
                            _pagerUI = new AjaxForWebControl(this);
                            break;
                    }
                }

                return _pagerUI;
            }
        }
        #endregion

        #endregion

        //#region oninit事件
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="e"></param>
        //protected override void OnInit(EventArgs e)
        //{
        //    base.OnInit(e);
        //    //实例化PagerSQL
        //    //this.ManagerPageSQL.Page = base.Page;
        //}
        //#endregion

        #region 绘制UI CreateChildControls()
        /// <summary>
        /// 绘制UI
        /// </summary>
        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            //根据分页方式，做初始化设置
            SetOnIt();
            
           
            if (!Page.IsPostBack)
                //第一次访问
                QuickFirst();
            else
                //回发
                QuickPostBack();
             
            //PagerSQL.Page.ClientScript.RegisterStartupScript(PagerSQL.Page.GetType(), "a", this.ManagerPageUI.WritePageJS());

            base.Page.ClientScript.RegisterStartupScript(base.Page.GetType(), "b", ManagerPageUI.WriteGoJS());

            //输出引用css的代码
            base.Page.ClientScript.RegisterClientScriptInclude(Page.GetType(), "pagerJS", "~/aspnet_client/QuickPager/QuickPager-1.0.js");
            base.Page.ClientScript.RegisterStartupScript(Page.GetType(), "pagerCSS", "<link rel=\"stylesheet\" type=\"text/css\" href=\"/aspnet_client/QuickPager/skin/" + Skin + "/pager.css\">");

        }
        #endregion

        #region 根据分页方式，做初始化设置
        /// <summary>
        /// 根据分页方式，做初始化设置
        /// </summary>
        private void SetOnIt()
        {
            switch (PagerTurnKind)
            {
                case PagerTurnKind.JS:
                case PagerTurnKind.URL:
                case PagerTurnKind.URLRewriter:
                case PagerTurnKind.Ajax:
                case PagerTurnKind.AjaxForWebControl:
                    //不存储属性
                    if (PagerSql != null)
                        PagerSql.SaveLocation = SaveViewStateLocation.NoSave;
                    break;

                case PagerTurnKind.PostBack:
                case PagerTurnKind.PostBackURL:
                    //把属性存储到隐藏域里面，并且加密
                    PagerSql.SaveLocation = SaveViewStateLocation.Hidden;
                    
                    var btn = new LinkButton {ID = "Page_Button", Text = ""};
                    //btn.Click += new EventHandler(btn_Click);   //添加事件
                    Controls.Add(btn);
                    break;
            }
        }
        #endregion

        #region 第一次访问
        private void QuickFirst()
        {
            if (PagerRunKind == PagerRunKind.Auto)
            {
                //定义一个事件里的参数
                var e = new PageArgs();

                switch (PagerTurnKind)
                {
                    case PagerTurnKind.PostBack:
                        #region PostBack
                        e.OldPageIndex = 1;
                        e.CurrentPageIndex = 1;

                        //触发（调用）外部的事件
                        OnPageChange(this, e);

                        //自动提取数据的方式，显示第一页的数据
                        //生成SQL语句和获取记录总数
                        PagerSql.CreateSQL();

                        //统计总记录数和总页数
                        ComputeRecordCount();

                        //绑定第this.PageIndex页的数据
                        DataBind(PageIndex, e);

                        #endregion
                        break;

                    case PagerTurnKind.URLRewriter:
                    case PagerTurnKind.URL:
                        #region URL
                        //这是为不存储
                        PagerSql.SaveLocation = SaveViewStateLocation.NoSave;

                        if (IsBindControl)
                        {
                            //初始化SQL
                            SetSql();
                            //函数里实现下面几个功能
                            //获取URL 里面的分页参数
                            //生成SQL语句和获取记录总数
                            //统计总记录数和总页数

                            //绑定 数据
                            DataBind(PageIndex, e);
                        }
                        #endregion
                        break;

                    case PagerTurnKind.PostBackURL:
                    case PagerTurnKind.AjaxForWebControl:
                        #region PostBackURL

                        e.OldPageIndex = 1;
                        e.CurrentPageIndex = 1;

                        //触发（调用）外部的事件
                        OnPageChange(this, e);

                        //自动提取数据的方式，显示第一页的数据
                        //生成SQL语句和获取记录总数
                        PagerSql.CreateSQL();

                        //统计总记录数和总页数
                        ComputeRecordCount();

                        //绑定第this.PageIndex页的数据
                        DataBind(PageIndex, e);


                        #endregion
                        break;

                    //其他的分页方法暂时不处理
                }

                //判断是否ajax分页
                switch (PagerTurnKind)
                {
                    case PagerTurnKind.AjaxForWebControl:
                        //ajax
                        DataControltoHtml();
                        break;
                }

            }

            ManagerPageUI.AddPageUI();  //添加页面显示

        }
        #endregion

        #region 回发事件的处理
        private void QuickPostBack()
        {
            //回发的时候，如果不是分页控件触发的需要显示UI，否则不能保存状态
            string eventTarget = Page.Request.Form["__EVENTTARGET"];
            //System.Web.HttpContext.Current.Response.Write(EVENTTARGET);
            if (eventTarget == null || eventTarget != ClientID)
            {
                //this.Page.Response.Write(EVENTTARGET);
                ManagerPageUI.AddPageUI();  //添加页面显示
            }

            if (_uc != null)
            {
                //在UserConctrol里面使用的时候，目前无法得到事件，所以采用一个笨办法
                if (eventTarget == ClientID)
                {
                    string eventArgument = Page.Request.Form["__EVENTARGUMENT"];
                    PagerClick(Int32.Parse(eventArgument));
                }
            }
        }
        #endregion

        #region ajaxForWebcontrol，输出HTML
        private void DataControltoHtml()
        {
            //判断参数，必须有kind=ajax才可以输出数据列表的HTML
            string kind = HttpContext.Current.Request.QueryString["pagerKind"];
            if (kind != "ajax")
                return;

            var tw = new System.IO.StringWriter();
            var hw = new HtmlTextWriter(tw);

            if (ShowDataControl != null)
                ShowDataControl.EnableViewState = false;

            //Page page1 = new Page();
            //HtmlForm form = new HtmlForm();
            //base.Page.EnableEventValidation = false;
            //page1.DesignerInitialize();
            //page1.Controls.Add(form);
            //form.Controls.Add(this.ShowDataControl);

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Charset = "UTF-8";
            HttpContext.Current.Response.Buffer = true;
            //HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312"); //GB2312
            HttpContext.Current.Response.ContentType = "text/plain";	//application/ms-excel//image/JPEG;text/HTML;image/GIF;vnd.ms-excel/msword

            if (ShowDataControl != null) ShowDataControl.RenderControl(hw);


            HttpContext.Current.Response.Write(tw.ToString());
            HttpContext.Current.Response.Write("`");

            var tw2 = new System.IO.StringWriter();
            var hw2 = new HtmlTextWriter(tw2);

            ManagerPageUI.AddPageUI();  //添加页面显示

            //总记录数
            var lc1 = (LiteralControl)FindControl("p_data1");
            lc1.RenderControl(hw2);
            
            //第几页
            lc1 = (LiteralControl)FindControl("p_data2");
            lc1.RenderControl(hw2);

            //每页记录数
            lc1 = (LiteralControl)FindControl("p_data3");
            lc1.RenderControl(hw2);

            //页面导航
            lc1 = (LiteralControl)FindControl("p_no");
            lc1.RenderControl(hw2);

            //首页
            lc1 = (LiteralControl)FindControl("p_First");
            lc1.RenderControl(hw2);

            //上一页
            lc1 = (LiteralControl)FindControl("P_Prev");
            lc1.RenderControl(hw2);


            //下一页
            lc1 = (LiteralControl)FindControl("P_Next");
            lc1.RenderControl(hw2);


            //末页
            lc1 = (LiteralControl)FindControl("P_Last");
            lc1.RenderControl(hw2);
          
            //Go暂时不支持Go。
            //lc1 = (LiteralControl)this.FindControl("p_no");
            //lc1.RenderControl(hw2);

            
            HttpContext.Current.Response.Write(tw2.ToString());

            HttpContext.Current.Response.End();
        }
        #endregion

        #region 设计时支持
        /// <summary>
        /// 设计时支持
        /// </summary>
        /// <param name="output"></param>
        protected override void Render(HtmlTextWriter output)
        {
            if ((Site != null) && Site.DesignMode)
            {
                output.Write("<div style='TEXT-ALIGN: center;width:100%'>第1/100页 &nbsp; &nbsp; 首页&nbsp; &nbsp;上一页 &nbsp; &nbsp; [1][2][3] &nbsp;&nbsp; 下一页 &nbsp;&nbsp; 末页 &nbsp;&nbsp; 共1000条记录</div>");
            }
            else
            {
                //Page_Click();
                //base.Page.VerifyRenderingInServerForm(this);

                string div = "<div id=\"{0}Pager\" class=\"{1}\" >";
                div = string.Format(div, ClientID, CssClass);
                output.Write(div);
                base.Render(output);
                output.Write("</div>");
            }

        }
        #endregion

    }
}
