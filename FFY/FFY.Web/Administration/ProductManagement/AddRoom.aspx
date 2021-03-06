﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddRoom.aspx.cs" Inherits="FFY.Web.Administration.ProductManagement.AddRoom" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-horizontal">
        <h3>Room Addition</h3>
        <hr />
        <asp:Label ID="ErrorMessage" AssociatedControlID="ErrorMessage" runat="server"></asp:Label>
        <asp:ValidationSummary runat="server" CssClass="text-danger" />
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Name" CssClass="col-md-2 control-label">Name</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Name" CssClass="form-control" />  
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Name"
                    CssClass="text-danger" ErrorMessage="Name field is required." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Image" CssClass="col-md-offset-2 col-md-3 btn btn-default">Browse</asp:Label>
                <asp:FileUpload runat="server" ID="Image" CssClass="form-control"></asp:FileUpload>
        </div>
        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button Width="150px" runat="server" OnClick="AddRoomClick" Text="Create" CssClass="btn btn-primary" />
            </div>
        </div>
    </div>
</asp:Content>
