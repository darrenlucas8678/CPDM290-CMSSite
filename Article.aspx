<%@ Page Title="" Language="C#" MasterPageFile="~/Content.master" AutoEventWireup="true" CodeFile="Article.aspx.cs" Inherits="Article" %>
<%@ MasterType VirtualPath="~/Content.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
Article Page
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="content" Runat="Server">
<form id="article" runat="server"> 
<asp:Literal ID="ltlTitle" runat="server" Text="<b>Breaking News Story</b>"/> <br />
<asp:Image ID="imgArticleImage" runat="server" /><br />
<asp:Literal ID="ltlCopy" runat="server" /><br />
</form>    
</asp:Content>

