<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true"
    CodeFile="Articles.aspx.cs" Inherits="Articles" %>

<%@ MasterType VirtualPath="~/Admin/Admin.master" %>
<asp:Content ID="head" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="main" ContentPlaceHolderID="content" runat="Server">
    <form id="articlelist" runat="server">
    <asp:ScriptManager ID="smrArticleList" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="uplArticleList" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:LinkButton ID="lnkFilterAll" runat="server" OnClick="lnkFilterAll_Click">All</asp:LinkButton>
            <asp:LinkButton ID="lnkFilterActive" runat="server" OnClick="lnkFilterActive_Click">Active</asp:LinkButton>
            <asp:LinkButton ID="lnkFilterExpired" runat="server" OnClick="lnkFilterExpired_Click">Expired</asp:LinkButton>
            <asp:ObjectDataSource ID="odsArticles" runat="server" SelectMethod="GetAll" TypeName="CPDM.LucasD.Midterm.BLL.ArticleDbAccess"
                DeleteMethod="Delete">
                <SelectParameters>
                    <asp:SessionParameter SessionField="ListMode" Name="filter" Type="String" />
                    <asp:SessionParameter SessionField="CurrentUserRole" Name="userrole" Type="String" />
                </SelectParameters>
                <DeleteParameters>
                    <asp:Parameter Name="articleid" Type="Int32" />
                </DeleteParameters>
            </asp:ObjectDataSource>
            <asp:GridView ID="grdArticleList" runat="server" AllowPaging="True" DataSourceID="odsArticles"
                AutoGenerateColumns="False" OnRowCommand="grdArticleList_RowCommand" DataKeyNames="articleid"
                OnRowDeleting="grdArticleList_RowDeleting" PageSize="10">
                <Columns>
                    <asp:BoundField DataField="articleid" HeaderText="articleid" Visible="false" />
                    <asp:BoundField DataField="title" HeaderText="Title" />
                    <asp:BoundField DataField="startdate" HeaderText="Start Date" DataFormatString="{0:d}" />
                    <asp:BoundField DataField="enddate" HeaderText="End Date" DataFormatString="{0:d}" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Edit" CommandArgument='<%#Eval("articleid") %>'
                                Visible='<%# Master.PermissionEdit || Master.PermissionLimitedEdit %>'>Edit</asp:LinkButton>
                            <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" CommandArgument='<%#Eval("articleid") %>'
                                Visible='<%# Master.PermissionDelete %>'>Delete</asp:LinkButton>
                            <asp:LinkButton ID="lnkPublish" runat="server" CommandName="Publish" CommandArgument='<%#Eval("articleid") %>'
                                Visible='<%# Master.PermissionPublish && !(bool)Eval("IsPublished") %>'>Publish</asp:LinkButton>
                            <asp:LinkButton ID="lnkUnpublish" runat="server" CommandName="Unpublish" CommandArgument='<%#Eval("articleid") %>'
                                Visible='<%# Master.PermissionPublish && (bool)Eval("IsPublished") %>'>Unpublish</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</asp:Content>
