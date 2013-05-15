<%@ Page Title="" Language="C#" MasterPageFile="~/Content.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ MasterType VirtualPath="~/Content.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="Server">
    <form id="articles" runat="server">
    <asp:HiddenField ID="Top" runat="server" />
    <asp:ObjectDataSource ID="odsArticles" runat="server" SelectMethod="GetAll" TypeName="CPDM.LucasD.Midterm.BLL.ArticleDbAccess">
        <SelectParameters>
            <asp:ControlParameter ControlID="Top" Name="filter" PropertyName="ID" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ListView ID="lvArticles" runat="server" DataSourceID="odsArticles" OnItemCommand="lvArticles_OnItemCommand">
        <EmptyDataTemplate>
            No Articles
        </EmptyDataTemplate>
        <LayoutTemplate>
            <div class="article_horizontal">
                <div class="row" runat="server">
                    <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                </div>
            </div>
        </LayoutTemplate>
        <ItemTemplate>
            <div class="col gu1">
                <div class="center"><asp:Image ID="imgThumbnailImage" runat="server" ImageUrl='<%# Eval("thumbnailimageurl") %>' />
                <h5><asp:Literal ID="ltlTitle" runat="server" Text='<%# Eval("title") %>' /></h5>
                <asp:Literal ID="ltlSummary" runat="server" Text='<%# Eval("summary") %>' />
                <asp:LinkButton ID="btnReadMore" runat="server" Text=" Read More..." CommandName="OpenArticle"
                    CommandArgument='<%#Eval("articleid") %>' /></div>
            </div>
        </ItemTemplate>
    </asp:ListView>
    </form>
</asp:Content>
