<%@ Page Title="" Language="C#" MasterPageFile="~/Content.master" AutoEventWireup="true"
    CodeFile="Articles.aspx.cs" Inherits="Articles" %>

<%@ MasterType VirtualPath="~/Content.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="content" runat="Server">
    <form id="articles" runat="server">
    <asp:HiddenField ID="SortedPublished" runat="server" />
    <asp:ObjectDataSource ID="odsArticles" runat="server" SelectMethod="GetAll" TypeName="CPDM.LucasD.Midterm.BLL.ArticleDbAccess">
        <SelectParameters>
            <asp:ControlParameter ControlID="SortedPublished" Name="filter" PropertyName="ID" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ListView ID="lvArticles" runat="server" DataSourceID="odsArticles" OnItemCommand="lvArticles_OnItemCommand" OnDataBound="lvArticles_DataBound">
        <EmptyDataTemplate>
            No Articles
        </EmptyDataTemplate>
        <LayoutTemplate>
            <div>
                <div id="itemPlaceHolder" runat="server">
                </div>
            </div>
        </LayoutTemplate>
        <ItemTemplate>
            <asp:Literal ID="ltlTitle" runat="server" Text='<%# Eval("title") %>' /><br />
            <asp:Image ID="imgThumbnailImage" runat="server" ImageUrl='<%# Eval("thumbnailimageurl") %>' /><br />
            <asp:Literal ID="ltlSummary" runat="server" Text='<%# Eval("summary") %>' />
            <asp:LinkButton ID="btnReadMore" runat="server" Text=" Read More..." CommandName="OpenArticle"
                CommandArgument='<%#Eval("articleid") %>' />
        </ItemTemplate>
        <ItemSeparatorTemplate>
            <hr />
        </ItemSeparatorTemplate>
    </asp:ListView>
    <asp:DataPager ID="dpArticles" runat="server" PagedControlID="lvArticles" PageSize="5">
        <Fields>
            <asp:NextPreviousPagerField ShowNextPageButton="false" />
            <asp:NumericPagerField />
            <asp:NextPreviousPagerField ShowPreviousPageButton="false" />
        </Fields>
    </asp:DataPager>
    </form>
</asp:Content>
