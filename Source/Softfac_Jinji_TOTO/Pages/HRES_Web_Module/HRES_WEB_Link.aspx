<%@ Page Language="VB" AutoEventWireup="false" CodeFile="HRES_WEB_Link.aspx.vb" Inherits="Pages_HRES_WEB_Link" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Ad Hoc Reports Link</title>
</head>
<body>
    <form id="HRES_WEB_LINK" runat="server">
        <div>
        <asp:Image ID="Image1" runat="server" Width="120px" Height="120px" ImageUrl="~/Pages/HRES_Web_Module/Images/training.jpg" />
        <asp:HyperLink ID="HyperLink1" runat="server" Text="Training and Development System" NavigateUrl="~/Pages/HRES_Web_Module/Module/HRTD_WEB.exe"></asp:HyperLink>
        <br />
        <asp:Label ID="Label3" runat="server" Text="Its functionality enables user to keep track the training of employees record and choose the eligible employee to attend certain courses based on the training needs. Training calendar also available for user to view the training courses conducted for the whole year."></asp:Label>
        <br />
        <br />
        <asp:Image ID="Image2" runat="server" Width="120px" Height="120px" ImageUrl="~/Pages/HRES_Web_Module/Images/Performance.jpg" />
        <asp:HyperLink ID="HyperLink2" runat="server" Text="Performance System" NavigateUrl="~/Pages/HRES_Web_Module/Module/HRPM_WEB.exe"></asp:HyperLink>
        <br />
        <asp:Label ID="Label4" runat="server" Text="HRPA gauges work performance of individual employees and teams within an organization. It is also a tool to obtain better results from organizations and individuals by understanding and MANAGING PERFORMANCE within an agreed framework of goals, standards and competency requirements. HRPA System’s electronic format simplifies the reviewing of employee’s performance data. HRPA System identifies the employees Key Performance Area (KPA), enforces Management by Objective (MBO) and Key Results Area (KRA)."></asp:Label>
        <br />
        <br />
        <asp:Image ID="imgReport1" runat="server" Width="120px" Height="120px" ImageUrl="~/Pages/HRES_Web_Module/Images/EmpRelations.jpg"/>
        <asp:HyperLink ID="hyplnkREPORT1" runat="server" Text="Employee Relations Module" NavigateUrl="~/Pages/HRES_Web_Module/Module/Discipline_Web.exe"></asp:HyperLink>
        <br />
        <asp:Label ID="lblReport1" runat="server" Text="It is imperative for an organization to ensure industrial peace and harmony between the employees and the management for the purpose of optimising productivity. For this to happen, line managers must ensure that they do not infringe the rights of the employees and creates industrial unrest. With that,
        HRER System is used to keep DISPLINARY RECORDS and employment history of all employees in the organization.
       HRER tracks historical and present data of the following:
       Personnel Policies & Procedures
       Collective Agreement
       Occupational Safety & Health
       Industrial Relations
       Employee Activity Records
       Corporate Activity Records"></asp:Label>
        <br />
        <br />
        <asp:Image ID="imgReport2" runat="server" Width="120px" Height="120px" ImageUrl="~/Pages/HRES_Web_Module/Images/Medical_Safety.jpg"/>
        <asp:HyperLink ID="hyplnkREPORT2" runat="server" Text="Medical and Safety" NavigateUrl="~/Pages/HRES_Web_Module/Module/HRSH_WEB.exe"></asp:HyperLink>
        <br />
        <asp:Label ID="Label1" runat="server" Text="HRMS keeps records of all MEDICAL VISITS and INCIDENCES of employee’s individual health record. The ADVANTAGE of the system is that it is able to generate medical benefits claims and insurance claims processing electronically in accordance to the company’s policy and benefits entitlement. HRMS tracks the status of the safety equipment, indicating whether the equipment is on loan or purchased. HRMS also tracks the date and place of purchase, and information on equipment wear and tear."></asp:Label>
        <br />
        <br />
        <asp:Image ID="imgReport3" runat="server" Width="120px" Height="120px" ImageUrl="~/Pages/HRES_Web_Module/Images/gift_web.jpg" />
        <asp:HyperLink ID="hyplnkREPORT3" runat="server" Text="Gift System" NavigateUrl="~/Pages/HRES_Web_Module/Module/Gift_Web.exe"></asp:HyperLink>
        <br />
        <asp:Label ID="Label2" runat="server" Text="The Gift System is Control the employee’s Entitlement Benefit of individual Job Grade. The ADVANTAGE of the system is that it is able to processing electronically in accordance to the company’s policy and benefits entitlement."></asp:Label>
        <br />
        <br />
        <asp:Image ID="Image3" runat="server" Width="120px" Height="120px" ImageUrl="~/Pages/HRES_Web_Module/Images/Transport.gif" />
        <asp:HyperLink ID="HyperLink3" runat="server" Text="Transportation Module" NavigateUrl="~/Pages/HRES_Web_Module/Module/HRTransport_web.exe"></asp:HyperLink>
        <asp:HyperLink ID="HyperLink4" runat="server" Text="/ Unform Module" NavigateUrl="~/Pages/HRES_Web_Module/Module/Uniform_WEB.exe"></asp:HyperLink>
        <asp:HyperLink ID="HyperLink5" runat="server" Text="/ Locker Module" NavigateUrl="~/Pages/HRES_Web_Module/Module/HRLocker_Web.exe"></asp:HyperLink>
        <asp:HyperLink ID="HyperLink6" runat="server" Text="/ Hostel Module" NavigateUrl="~/Pages/HRES_Web_Module/Module/HRHM_WEB.exe"></asp:HyperLink>
        <br />
        <asp:Label ID="Label5" runat="server" Text="The HRESC consist of few modules, such as, Transportation, Hostel, Uniform and Shoes and Locker. The Transportation Modules assists in planning and scheduling of transportation, and the assignment of bus based on the needs and changing requirements such as rescheduling overtime especially in the manufacturing sector. 
        The Hostel Module stores and updates information pertaining to employees’ housing details, facilities provided and complaints to be handled by the maintenance department. Uniform and Shoes Module provide detailed transaction records of all suppliers and employees on the collection and replenishment of supplies. The Locker Module helps keep track of employee locker in assignment and application."></asp:Label>

    </div>
    </form>
</body>
</html>
