<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true"
    CodeFile="ArticleEditor.aspx.cs" Inherits="Admin_ArticleEditor" %>

<%@ MasterType VirtualPath="~/Admin/Admin.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="content" runat="Server">
    <form id="frmArticleEditor" runat="server">
    <asp:HiddenField ID="hidIsPublished" runat="server" />
    <asp:HiddenField ID="hidPublishedDate" runat="server" />
    <asp:ValidationSummary ID="ArticleValidationSummary" runat="server" ValidationGroup="ArticleValidationGroup" />
    <asp:ScriptManager ID="smrArticleEditor" runat="server">
    </asp:ScriptManager>
    <fieldset>
        <legend>Article Information</legend>
        <div>
            <div>
                <asp:RequiredFieldValidator ID="TitleRequired" runat="server" ErrorMessage="Article Title is required"
                    ValidationGroup="ArticleValidationGroup" ToolTip="Article Tile is required" ControlToValidate="txtTitle"
                    Display="Dynamic" EnableClientScript="false">*</asp:RequiredFieldValidator>
            </div>
            <div>
                <asp:Label ID="lblTitle" runat="server" AssociatedControlID="txtTitle" CssClass="inline">Title:</asp:Label>
                <asp:TextBox ID="txtTitle" runat="server"></asp:TextBox>
            </div>
        </div>
        <div>
            <div>
                <asp:RequiredFieldValidator ID="SummaryRequired" runat="server" ErrorMessage="Article Summary is required"
                    ControlToValidate="txtSummary" ValidationGroup="ArticleValidationGroup" ToolTip="Article Summary is required"
                    Display="Dynamic" EnableClientScript="false">*</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="SummaryCharacterLimitValidator" runat="server"
                    ErrorMessage="Article Summary to be less than 100 characters" ControlToValidate="txtSummary"
                    ToolTip="Article Sumamry to be less than 100 characters" ValidationExpression="^([\S\s]{0,100})$"
                    ValidationGroup="ArticleValidationGroup" Display="Dynamic" EnableClientScript="false">*</asp:RegularExpressionValidator>
            </div>
            <div>
                <asp:Label ID="lblSummary" runat="server" AssociatedControlID="txtSummary">Summary (100 Characters Max):</asp:Label>
                <asp:TextBox ID="txtSummary" runat="server" TextMode="Multiline" Rows="4" Width="400"></asp:TextBox>
                <ajaxToolkit:HtmlEditorExtender ID="SummaryEditor" runat="server" TargetControlID="txtSummary"
                    EnableSanitization="false">
                    <Toolbar>
                        <ajaxToolkit:Bold />
                        <ajaxToolkit:Italic />
                        <ajaxToolkit:Underline />
                    </Toolbar>
                </ajaxToolkit:HtmlEditorExtender>
                <br />
            </div>
        </div>
        <div>
            <div>
                <asp:RegularExpressionValidator ID="CopyCharacterLimitValidator" runat="server" ErrorMessage="Article Copy to be less than 8000 characters"
                    ControlToValidate="txtCopy" ToolTip="Article Copy to be less than 8000 characters"
                    ValidationExpression="^([\S\s]{0,8000})$" ValidationGroup="ArticleValidationGroup"
                    Display="Dynamic" EnableClientScript="false">*</asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="CopyRequired" runat="server" ErrorMessage="Article Copy is required"
                    ToolTip="Article Copy is required" ControlToValidate="txtCopy" Display="Dynamic"
                    ValidationGroup="ArticleValidationGroup" EnableClientScript="false">*</asp:RequiredFieldValidator>
            </div>
            <div>
                <asp:Label ID="lblCopy" runat="server" AssociatedControlID="txtCopy">Copy (8000 Characters Max):</asp:Label>
                <asp:TextBox ID="txtCopy" runat="server" TextMode="MultiLine" Rows="10" Width="400"></asp:TextBox>
                <ajaxToolkit:HtmlEditorExtender ID="E" runat="server" TargetControlID="txtCopy" EnableSanitization="false">
                    <Toolbar>
                        <ajaxToolkit:Bold />
                        <ajaxToolkit:Italic />
                        <ajaxToolkit:Underline />
                    </Toolbar>
                </ajaxToolkit:HtmlEditorExtender>
                <br />
            </div>
        </div>
        <div>
            <div>
                <asp:CustomValidator ID="ThumbnailRequired" runat="server" ErrorMessage="Select a thumbnail image file to upload"
                    ValidationGroup="ArticleValidationGroup" Display="Dynamic">*</asp:CustomValidator>
                <asp:CustomValidator ID="ThumbnailImageFile" runat="server" ErrorMessage="Thumbnail Image must be an image file"
                    ValidationGroup="ArticleValidationGroup" Display="Dynamic">*</asp:CustomValidator>
                <asp:CustomValidator ID="ThumbnailImagePosted" runat="server" ErrorMessage="Thumbnail Image was not posted to the server"
                    ValidationGroup="ArticleValidationGroup" Display="Dynamic">*</asp:CustomValidator>
            </div>
            <div>
                <asp:Label ID="lblThumbnailImage" runat="server" AssociatedControlID="uploadThumbnailImage">Summary Thumbnail Image:</asp:Label><br />
                <asp:FileUpload ID="uploadThumbnailImage" runat="server" />
                <div>
                    <asp:Image ID="imgThumbnailImage" runat="server" Visible="false" />
                </div>
            </div>
        </div>
        <div>
            <div>
                <asp:CustomValidator ID="ArticleImageRequired" runat="server" ErrorMessage="Select an article image file to upload"
                    ValidationGroup="ArticleValidationGroup" Display="Dynamic">*</asp:CustomValidator>
                <asp:CustomValidator ID="ArticleImageFile" runat="server" ErrorMessage="Article Image must be an image file"
                    ValidationGroup="ArticleValidationGroup" Display="Dynamic">*</asp:CustomValidator>
                <asp:CustomValidator ID="ArticleImagePosted" runat="server" ErrorMessage="Article Image was not posted to the server"
                    ValidationGroup="ArticleValidationGroup" Display="Dynamic">*</asp:CustomValidator>
            </div>
            <div>
                <asp:Label ID="lblArticleImage" runat="server" AssociatedControlID="uploadArticleImage">Main Article Image</asp:Label><br />
                <asp:FileUpload ID="uploadArticleImage" runat="server" />
                <div>
                    <asp:Image ID="imgArticleImage" runat="server" Visible="false" />
                </div>
            </div>
        </div>
        <div>
            <div>
                <asp:RangeValidator ID="StartDateValidFormat" runat="server" ErrorMessage="Enter a valid date for Start Date"
                    ToolTip="Enter a valid date for Start Date" ControlToValidate="txtStartDate"
                    ValidationGroup="ArticleValidationGroup" Display="Dynamic" Type="Date" MinimumValue="1/1/2013"
                    MaximumValue="1/1/2020" EnableClientScript="false">*</asp:RangeValidator>
                <asp:RequiredFieldValidator ID="StartDateRequired" runat="server" ErrorMessage="Start Date is required"
                    ToolTip="Start Date is required" ControlToValidate="txtStartDate" ValidationGroup="ArticleValidationGroup"
                    Display="Dynamic" EnableClientScript="false">*</asp:RequiredFieldValidator>
            </div>
            <div>
                <asp:Label ID="lblStartDate" runat="server" AssociatedControlID="txtStartDate" CssClass="inline">Start Date:</asp:Label>
                <asp:TextBox ID="txtStartDate" runat="server"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="calendarStartDate" runat="server" TargetControlID="txtStartDate" />
            </div>
        </div>
        <div>
            <div>
                <asp:RangeValidator ID="EndDateValidFormat" runat="server" ErrorMessage="Enter a valid date for End Date"
                    ToolTip="Enter a valid date for End Date" ControlToValidate="txtEndDate" ValidationGroup="ArticleValidationGroup"
                    Display="Dynamic" Type="Date" MinimumValue="1/1/2013" MaximumValue="1/1/2020"
                    EnableClientScript="false">*</asp:RangeValidator>
                <asp:RequiredFieldValidator ID="EndDateRequired" runat="server" ErrorMessage="End Date is required"
                    ToolTip="End Date is required" ControlToValidate="txtEndDate" ValidationGroup="ArticleValidationGroup"
                    Display="Dynamic" EnableClientScript="false">*</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="EndDateAfterStartDate" runat="server" ControlToValidate="txtEndDate" ErrorMessage="End Date must be later than Start Date"
                    Display="Dynamic" ControlToCompare="txtStartDate" Operator="GreaterThan" Type="Date" EnableClientScript="false" ValidationGroup="ArticleValidationGroup">*</asp:CompareValidator>
            </div>
            <div>
                <asp:Label ID="lblEndDate" runat="server" AssociatedControlID="txtEndDate" CssClass="inline">End Date:</asp:Label>
                <asp:TextBox ID="txtEndDate" runat="server"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="calendarEndDate" runat="server" TargetControlID="txtEndDate" />
            </div>
        </div>
        <div>
            <asp:Button ID="btnSaveArticle" runat="server" Text="Save Article" OnClick="btnSaveArticle_Click"
                ValidationGroup="ArticleValidationGroup" />
        </div>
    </fieldset>
    </form>
</asp:Content>
