<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SSSWebFormASP.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <%--start login form--%>
    <div class="login-page bg-light">
        <div class="container">
            <div class="row">
                <div class="col-lg-10 offset-lg-1">
                    <%--<h3 class="mb-3">Login Now</h3>--%>
                    <div class="bg-white shadow rounded">
                        <div class="row">

                            <div class="col-md-5 ps-0 d-none d-md-block">                         
                                    <div class="form-right h-100 bg-primary text-white text-center pt-5">
                                    <%--<i class="bi bi-bootstrap"></i>--%>
                                    <h2 class="fs-3">Siam Smart Solutions Co., Ltd.</h2>
                                </div>
                            </div>

                            <div class="col-md-7 pe-0">
                                <div class="form-left h-100 py-5 px-5">
                                    <form action="" class="row g-1">
                                        <div class="col-12">
                                            <label>Username<span class="text-danger">*</span></label>
                                            <div class="input-group">
                                                <div class="input-group-text"><i class="bi bi-person-fill"></i></div>
                                                <input id="txt_username" name="username" runat="server" type="number" class="form-control" placeholder="Enter Username" required oninvalid="this.setCustomValidity('กรุณากรอกรหัสพนักงาน')" oninput="this.setCustomValidity('')">
                                            </div>
                                        </div>
                                        <br />

                                        <div class="col-12">
                                            <label>Password<span class="text-danger">*</span></label>
                                            <div class="input-group">
                                                <div class="input-group-text"><i class="bi bi-lock-fill"></i></div>
                                                <input id="txt_password" name="password" runat="server" type="password" class="form-control" placeholder="Enter Password" required oninvalid="this.setCustomValidity('กรุณากรอกรหัสผ่าน')" oninput="this.setCustomValidity('')">
                                            </div>
                                        </div>
                                        <br />

                                        <%--<div class="col-sm-6">
                                            <div class="form-check">
                                                <input class="form-check-input" type="checkbox" id="inlineFormCheck">
                                                <label class="form-check-label" for="inlineFormCheck">Remember me</label>
                                            </div>
                                        </div>

                                        <div class="col-sm-6">
                                            <a href="#" class="float-end text-primary">Forgot Password?</a>
                                        </div> <br />--%>

                                        <div class="col-12">
                                            <%--  <button type="submit" class="btn btn-primary px-4 float-end mt-4">login</button>--%>
                                            <asp:Button ID="btn_login" type="submit" runat="server" class="btn btn-outline-primary" Text="Login" OnClick="btn_login_Click" />
                                        </div>
                                    </form>
                                </div>
                            </div>
                        </div>
                    </div>
                    <%-- <p class="text-end text-secondary mt-3">Bootstrap 5 Login Page Design</p>--%>
                </div>
            </div>
        </div>
    </div>
    <%--end login form--%>

</asp:Content>
