<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SummaryForecast.aspx.cs" Inherits="SSSWebFormASP.SummaryForecast" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="forecast">
        <div class="container-fluid" style="width: 100%; padding-right: 0; padding-left: 0;">
            <div style="padding: 0px 15px; border-radius: 10px;">
                <div class="row">
                    <div class="col-sm-12 background1" style="border-radius: 10px; margin-bottom: 5px;">
                        <h2 class="text-center fw-bold mb-2 text-uppercase">Summary Forecast</h2>
                    </div>
                    <span>
                        <%--<asp:Button ID="Btn_Excel" runat="server" Text="Excel"/>--%>
                        <asp:ImageButton ID="ibtn_excel" runat="server" src="./Images/excel.png" Height="32px" Width="32px" ImageAlign="Right"/>
                    </span>
                </div>
                <br />


                <%--Start (Repeater) Show Summary Forecast--%>
                <asp:Repeater ID="rpt_show_all" runat="server" OnItemDataBound="Repeater_show_all_ItemDataBound">
                    <ItemTemplate>
                        <asp:Label ID="lal_year" runat="server" Text="Label" Style="font-size: 30px; font-weight: bold;"></asp:Label>
                        <asp:GridView ID="gv_show_all" runat="server" CssClass="gv_design" AutoGenerateColumns="true" OnRowDataBound="gv_show_RowDataBound" ShowFooter="true">
                        </asp:GridView>
                        <br />
                    </ItemTemplate>
                </asp:Repeater>
                <%--End (Repeater) Show Summary Forecast--%>

                <%--Start (Repeater) Show Sum--%>
                <asp:Label ID="Label1" runat="server" Text="Sum" Style="font-size: 30px; font-weight: bold;"></asp:Label>
                <asp:GridView ID="show_sum" runat="server" AutoGenerateColumns="true" OnRowDataBound="gv_show_sum"></asp:GridView>
                <br />
                <%--End (Repeater) Show Sum--%>


            </div>
        </div>
    </div>
</asp:Content>
