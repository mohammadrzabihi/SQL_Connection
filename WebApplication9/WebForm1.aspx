<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebApplication9.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>مدیریت بازیگر</title>
    <!-- Bootstrap CSS -->
    <link href="Content/Styles/css/Bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <!-- Optional: Font Awesome for icons -->
    <link href="Content/Styles/css/rtl/css/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/Styles/css/Overall/all.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container bordered-container">
            <h2>مدیریت بازیگر</h2>

            <h3>اضافه کردن بازیگر جدید</h3>
            <!-- Name Input Group -->
            <div class="form-group">
                <div class="input-group">
                    <span class="input-group-addon" id="basic-addon1">نام<i class="fas fa-user"></i></span>
                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="نام" aria-describedby="basic-addon1"></asp:TextBox>
                </div>
            </div>

            <!-- Username Input Group -->
            <div class="form-group">
                <div class="input-group">
                    <span class="input-group-addon" id="basic-addon2">نام کاربری<i class="fas fa-at"></i></span>
                    <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" placeholder="نام کاربری" aria-describedby="basic-addon2"></asp:TextBox>
                </div>
            </div>

            <!-- Password Input Group -->
            <div class="form-group">
                <div class="input-group">
                    <span class="input-group-addon" id="basic-addon3">رمز عبور<i class="fas fa-lock"></i></span>
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="رمز عبور" aria-describedby="basic-addon3"></asp:TextBox>
                </div>
            </div>

            <!-- Gender Dropdown -->
            <div class="form-group">
                <div class="input-group">
                    <span class="input-group-addon" id="basic-addon4">جنسیت<i class="fas fa-venus-mars"></i></span>
                    <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-control" aria-describedby="basic-addon4">
                        <asp:ListItem Value="">انتخاب جنسیت</asp:ListItem>
                        <asp:ListItem Value="1">مرد</asp:ListItem>
                        <asp:ListItem Value="2">زن</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>

            <!-- Military Service Dropdown -->
            <div class="form-group">
                <div class="input-group">
                    <span class="input-group-addon" id="basic-addon5">وضعیت خدمت<i class="fas fa-shield-alt"></i></span>
                    <asp:DropDownList ID="ddlMilitaryService" runat="server" CssClass="form-control" aria-describedby="basic-addon5" disabled="disabled">
                        <asp:ListItem Value="">انتخاب وضعیت</asp:ListItem>
                        <asp:ListItem Value="3">مشول</asp:ListItem>
                        <asp:ListItem Value="4">غایب</asp:ListItem>
                         <asp:ListItem Value="5">معاف</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>

            <asp:Button ID="btnAdd" runat="server" Text="اضافه کردن بازیگر" OnClick="btnAdd_Click" CssClass="btn btn-primary" /><br /><br />

            <h3>بازیگران موجود</h3>
            <asp:GridView ID="gvActors" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" CssClass="table table-bordered table-hover"
                OnRowEditing="gvActors_RowEditing" OnRowCancelingEdit="gvActors_RowCancelingEdit"
                OnRowUpdating="gvActors_RowUpdating" OnRowDeleting="gvActors_RowDeleting">
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="شناسه" ReadOnly="True" Visible="false" />
                    <asp:BoundField DataField="Name" HeaderText="نام" />
                    <asp:BoundField DataField="Username" HeaderText="نام کاربری" />
                    <asp:TemplateField HeaderText="رمز عبور">
                        <ItemTemplate>
                            <%# string.IsNullOrEmpty(Eval("Password").ToString()) ? string.Empty : "********" %>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtPasswordEdit" runat="server" Text='<%# Bind("Password") %>' TextMode="Password" CssClass="form-control"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="IdGenderType" HeaderText="جنسیت" />
                    <asp:BoundField DataField="IdMilitaryServiceType" HeaderText="وضعیت خدمت" />
                    <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" CancelText="انصراف" DeleteText="حذف" EditText="ویرایش" UpdateText="به روز رسانی">
                        <ControlStyle CssClass="btn btn-sm" />
                    </asp:CommandField>
                </Columns>
            </asp:GridView>
            <asp:Label ID="lblErrorMessage" runat="server" ForeColor="Red" CssClass="text-danger"></asp:Label>
            <asp:Label ID="lblSuccessMessage" runat="server" ForeColor="Green" CssClass="text-success"></asp:Label>
        </div>
    </form>
    <script src="Content/Scripts/JSS/jquery-3.5.1.min.js"></script>
    <script src="Content/Scripts/JSS/popper.min.js"></script>
    <script src="Content/Scripts/JSS/bootstrap.min.js"></script>
    <!-- Optional: Font Awesome for icons -->
    <script src="https://kit.fontawesome.com/a076d05399.js" crossorigin="anonymous"></script>
    <script src="Content/Scripts/jquery.js"></script>
</body>
</html>