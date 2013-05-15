<%@ Page Title="" Language="C#" MasterPageFile="~/Content.master" AutoEventWireup="true"
    CodeFile="Login.aspx.cs" Inherits="Login" %>
    <%@ MasterType VirtualPath="~/Content.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="Server">
    <form id="frmContent" runat="server">
    <asp:ValidationSummary ID="LoginValidationSummary" runat="server" ValidationGroup="LoginValidationGroup" />
    <div class="login">
        <fieldset class="login">
            <legend>Account Information</legend>
            <p>
                <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Username:</asp:Label>
                <asp:TextBox ID="UserName" runat="server" CssClass="textEntry"></asp:TextBox>
                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                     ErrorMessage="User Name is required." ToolTip="User Name is required."
                    ValidationGroup="LoginValidationGroup">*</asp:RequiredFieldValidator>
            </p>
            <p>
                <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                <asp:TextBox ID="Password" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                     ErrorMessage="Password is required." ToolTip="Password is required."
                    ValidationGroup="LoginValidationGroup">*</asp:RequiredFieldValidator>
            </p>
            <p>
                <asp:CheckBox ID="RememberMe" runat="server" />
                <asp:Label ID="RememberMeLabel" runat="server" AssociatedControlID="RememberMe" CssClass="inline">Keep me logged in</asp:Label>
            </p>
        </fieldset>
        <p class="submitButton">
            <asp:Button CssClass="btn-med" ID="LoginButton" runat="server" CommandName="Login"
                Text="Log In" ValidationGroup="LoginValidationGroup" 
                onclick="LoginButton_Click" />
        </p>
        <asp:CustomValidator id="LoginFailed" runat="server" ErrorMessage="Login failed. Please check your username and password." ValidationGroup="LoginValidationGroup" visible = "false"></asp:CustomValidator>
    </div>
    </form>
</asp:Content>
