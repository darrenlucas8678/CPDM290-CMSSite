<%@ Page Title="" Language="C#" MasterPageFile="~/Content.master" AutoEventWireup="true"
    CodeFile="Archive.aspx.cs" Inherits="Archive" %>

<%@ MasterType VirtualPath="~/Content.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="content" runat="Server">
    <form id="articles" runat="server">
    <asp:HiddenField ID="Archived" runat="server" />
    <asp:ObjectDataSource ID="odsArticles" runat="server" SelectMethod="GetAll" TypeName="CPDM.LucasD.Midterm.BLL.ArticleDbAccess">
        <SelectParameters>
            <asp:ControlParameter ControlID="Archived" Name="filter" PropertyName="ID" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ListView ID="lvArticles" runat="server" DataSourceID="odsArticles" OnItemCommand="lvArticles_OnItemCommand">
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
            <asp:Button ID="btnReadMore" runat="server" Text="Read More" CommandName="OpenArticle"
                CommandArgument='<%#Eval("articleid") %>' />
        </ItemTemplate>
        <ItemSeparatorTemplate>
            <hr />
        </ItemSeparatorTemplate>
    </asp:ListView>
    </form>
    </form>
</asp:Content>
