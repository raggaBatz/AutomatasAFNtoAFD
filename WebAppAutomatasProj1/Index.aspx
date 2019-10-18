<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="WebAppAutomatasProj1.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    
    <title>R</title>
    <meta content="width=device-width, initial-scale=1.0" name="viewport"/>
    <meta content="" name="keywords"/>
    <meta content="" name="description"/>

    <!-- Google Fonts -->
    <link href="https://fonts.googleapis.com/css?family=Open+Sans:300,300i,400,400i,500,600,700,700i|Montserrat:300,400,500,600,700" rel="stylesheet"/>

    <!-- Bootstrap CSS File -->
    <link href="lib/bootstrap/css/bootstrap.min.css" rel="stylesheet"/>

    <!-- Libraries CSS Files -->
    <link href="lib/font-awesome/css/font-awesome.min.css" rel="stylesheet"/>
    <link href="lib/animate/animate.min.css" rel="stylesheet"/>
    <link href="lib/ionicons/css/ionicons.min.css" rel="stylesheet"/>
    <link href="lib/owlcarousel/assets/owl.carousel.min.css" rel="stylesheet"/>
    <link href="lib/lightbox/css/lightbox.min.css" rel="stylesheet"/>

    <!-- Main Stylesheet File -->
    <link href="css/style.css" rel="stylesheet"/>

    <link rel="icon" href="res/umg.ico" type="image/x-icon"/>
</head>
<body>
    <%--<header id="header">
        <div class="container">
            <div class="logo float-left">
                <h1 class="text-light"><a href="#" class="scrollto"><span>AUTOMATAS</span></a></h1>
            </div>
            <%--<nav class="main-nav float-right d-none d-lg-block">
            <nav class="navbar navbar-default">
                <ul>
                    <li class="active"><a href="#">ERICK RICARDO BATZ CUSCUL</a></li>
                    <li><a href="#">090 14 4920</a></li>
                    <li><a href="#">Sección A</a></li>
            
                </ul>
            </nav>
        </div>
    </header>--%>
    <nav class="navbar navbar-expand-lg navbar-light bg-light">
      <div class="container">
        <a class="navbar-brand" href="#">
              <img src="img/umg.png" height="75px" alt=""/>
        </a>
        <h1 class="text-light"><a href="#" class="scrollto"><span>AUTOMATAS</span></a></h1>
        <div class="collapse navbar-collapse" id="navbarResponsive">
            <ul class="navbar-nav ml-auto">
                <li class="nav-item active"><a href="#" class="nav-link text-info">ERICK RICARDO BATZ CUSCUL</a></li>
                <li class="nav-item"><a href="#" class="nav-link text-info">090 14 4920</a></li>
                <li class="nav-item"><a href="#" class="nav-link text-info">Sección A</a></li>
            </ul>
        </div>
      </div>
    </nav>
    <div id="main">
        <section id="services" class="container">
            <div class="container">
                <form id="form1" runat="server">
                    <div class="row">
                        <div class="col-sm-4">
                            <div class="custom-file">
                                <input type="file" class="custom-file-input" id="file" lang="es" runat="server"/>
                                <label class="custom-file-label" for="customFileLang">Seleccionar Archivo</label>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <button type="button" class="btn btn-info" id="btnLoadFile" runat="server" onserverclick="btnLoadFile_Click">Cargar archivo</button>
                        </div>
                    </div>
                    <br />
                    <div class="row" runat="server" id="divPrimary" visible="false">
                        <div class="col-sm-3">
                            <div class="md-form">
                                <i class="fa fa-file-o"></i>
                                <label>archivo</label>
                                <textarea id="taTXT" runat="server" class="md-textarea form-control" rows="10" readonly="readonly"></textarea>
                            </div>
                        </div>
                        <div class="col-sm-2">
                            <div class="md-form">
                                <i class="fa fa-quora"></i>
                                <label>estados</label>
                                <textarea id="taQ" runat="server" class="md-textarea form-control" rows="10" readonly="readonly"></textarea>
                            </div>
                        </div>
                        <div class="col-sm-2">
                            <div class="md-form">
                                <i class="fa fa-facebook"></i>
                                <label>alfabeto</label>
                                <textarea id="taF" runat="server" class="md-textarea form-control" rows="10" readonly="readonly"></textarea>
                            </div>
                        </div>
                        <div class="col-sm-2">
                            <div class="md-form">
                                <i class="fa fa-font"></i>
                                <label>aceptacion</label>
                                <textarea id="taA" runat="server" class="md-textarea form-control" rows="10" readonly="readonly"></textarea>
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="md-form">
                                <i class="fa fa-wikipedia-w"></i>
                                <label>AFN</label>
                                <asp:GridView ID="gvAFN" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="True" DataKeyNames="N" EmptyDataText="There are no data records to display.">
                                    <HeaderStyle CssClass="thead-custom"/>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <div class="row" runat="server" id="divSecondary" visible="false">
                        <div class="col-sm-3">
                            <div class="md-form">
                                <i class="fa fa-wikipedia-w"></i>
                                <label>AFD</label>
                                <asp:GridView ID="gvAFD" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="True" DataKeyNames="ESTADO" EmptyDataText="There are no data records to display.">
                                    <HeaderStyle CssClass="thead-custom"/>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </form>
            </div>
        </section>
    </div>
</body>
</html>
