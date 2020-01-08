<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Web_Application_Higher_Institution.Login" EnableEventValidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="author" content="Ajax">
    <!-- Mobile Specific Meta -->
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
	<meta name="description" content="A powerful school application for all your school activities, Software for school result, generate PIN, check result student report card online with PIN."/>
    <meta name="keywords" content="School HR, School E-Portal, School Result Application, Check result, parent, staff, teacher, principal, School Result, Result, Result different template, Report card, Report sheet, Generate PIN for result" />
    
    <!-- Stylesheets -->
    <link rel="stylesheet" href="./Welcome To Your School E-Portal_files/bootstrap.min.css">
    <link rel="stylesheet" href="./Welcome To Your School E-Portal_files/font-awesome.min.css">
    <link rel="stylesheet" href="./Welcome To Your School E-Portal_files/slick.css">
    <link rel="stylesheet" href="./Welcome To Your School E-Portal_files/slick-theme.css">
    <link rel="stylesheet" href="./Welcome To Your School E-Portal_files/jquery.fancybox.css">
    <link rel="stylesheet" href="./Welcome To Your School E-Portal_files/style.css">
    
	<link rel="icon" type="image/png" href="https://kingston.schooleportal.com/appAssets/login/images/icons/favicon.ico">
	<link rel="apple-touch-icon" href="https://kingston.schooleportal.com/appAssets/login/images/icons/favicon.ico">
	<link rel="apple-touch-icon" sizes="72x72" href="https://kingston.schooleportal.com/appAssets/login/images/icons/favicon.ico">
	<link rel="apple-touch-icon" sizes="114x114" href="https://kingston.schooleportal.com/appAssets/login/images/icons/favicon.ico">
<style type="text/css">.fancybox-margin{margin-right:17px;}</style><style id="fit-vids-style">.fluid-width-video-wrapper{width:100%;position:relative;padding:0;}.fluid-width-video-wrapper iframe,.fluid-width-video-wrapper object,.fluid-width-video-wrapper embed {position:absolute;top:0;left:0;width:100%;height:100%;}</style>
</head>
<body>
    <form id="form1" runat="server">
    <!--##############################################################################################################################################-->
        <div class="parallax-mirror" style="visibility: visible; z-index: -100; position: fixed; top: 0px; left: 0px; overflow: hidden; transform: translate3d(0px, 0px, 0px); height: 678px; width: 1349px;"><img class="parallax-slider loaded" src="./Welcome To Your School E-Portal_files/img1.jpg" style="transform: translate3d(0px, 0px, 0px); position: absolute; top: -68px; left: 0px; height: 758px; width: 1349px; max-width: none;"></div>
    
    <!-- #body-wrap -->
    <div id="body-wrap">
        
        <!-- #header -->
        <nav id="navigation_affix" class="scrollspy"><div class="container"><div class="navbar-brand"><a href="#"><img src="./images1/logo.jpg" alt="Logo"/></a></div><ul class="nav navbar-nav">
            
                            <li>
                                <a href="#" class="smooth-scroll">ESCAE Online Student-Admin Portal</a>
                            </li>
                            
                        </ul></div></nav><nav id="navigation_mobile"><div class="nav-menu-links"><ul>

                            <li>
                                <a href="#" class="smooth-scroll">ESCAE Online Student-Admin Portal</a>
                            </li>
                            
                        </ul></div><div class="nav-menu-button"><button class="nav-menu-toggle"><i class="fa fa-navicon"></i></button></div></nav><header id="header" data-parallax="scroll" data-speed="0.2" data-natural-width="1920" data-natural-height="1080" data-image-src="https://kingston.schooleportal.com/welcomeAssets/images/content/bg/1.jpg">
            <!-- .header-overlay --> 
            <div class="header-overlay">
                <!-- #navigation -->
                <nav id="navigation" class="navbar scrollspy">
                    <!-- .container -->
                    <div class="container">
                        <div class="navbar-brand">
                            <a href="#"><img src="./images1/logo.jpg" style="float:right; width: 100px; height: 65px;" alt="Logo"/></a>
                        </div>
                        <ul class="nav navbar-nav">

                            <li>
                                <a href="#" class="smooth-scroll">ESCAE Online Student-Admin Portal</a>
                            </li>
                            
                        </ul>
                    </div>
                    <!-- .container end -->
                </nav>
                <!-- #navigation end -->
                
                <!-- .header-content -->
                <div class="header-content">
                    <!-- .container -->
                    <div class="container">
                         <!-- .row -->
                         <div class="row" style="margin-top:-70px;">

                                <div class="col-sm-5 col-md-4">
                                    <div class="header-form">
                                        <div class="header-form-heading">
                                            <p class="text-center"> <i class="fa fa-key"></i> 
                                                Enter Login Credential for Accessibility
                                            </p>
                                        </div>
                                        <div class="header-form-body">
                                            <div class="submit-status"></div>
                                            <asp:Label ID="output" runat="server" Text=""></asp:Label> 
                                            <form method="#" action="#">
                                            <input type="hidden" name="_token" value="7ffLADOm4Pk4ew3VOwjklOUxqTrBKiNolENDbwMT">  
                                                <label class = "control-label ">Matric No/ User Name</label> <br/>                                            
                                                 <asp:TextBox ID="matric_user_name" runat="server" required="" name="pinCode" placeholder="Matric No/ User Name" style="width:100%"></asp:TextBox>
                                               <label class = "control-label ">Password</label> <br/>
                                                 <asp:TextBox ID="password" runat="server" required="" name="pinCode" placeholder="Password" style="width:100%" TextMode="Password"></asp:TextBox>
                                
                                               <!-- <asp:DropDownList ID="DropDownList2" runat="server" required="" placeholder="Select School Term" style="width:100%">
                                                    <asp:ListItem Selected="True">Select Year/ Level</asp:ListItem>
                                                    <asp:ListItem>100L</asp:ListItem>
                                                    <asp:ListItem>200L</asp:ListItem>
                                                    <asp:ListItem>300L</asp:ListItem>
                                                    <asp:ListItem>400L</asp:ListItem>
                                                    <asp:ListItem>500L</asp:ListItem>
                                                    <asp:ListItem>600L</asp:ListItem>
                                                </asp:DropDownList>
                                                                                                                                                                                                                                                             </select>
                                                 <br/><br/>
                                                <asp:DropDownList ID="DropDownList1" runat="server" required="" placeholder="Select School Term" style="width:100%">
                                                    <asp:ListItem Selected="True">Select Semester</asp:ListItem>
                                                    <asp:ListItem>Semester 1</asp:ListItem>
                                                    <asp:ListItem>Semester 2</asp:ListItem>
                                                </asp:DropDownList>
                                               -->
                                                <br/><br/>
                                                <asp:Button ID="Button1" runat="server" Text="Login to Continue" OnClick="Button1_Click"  />
                                               
                                            </form>
                                            <p class="txt-desc">Developed for ESCAE University. Benin Republic.</p>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-sm-7 col-md-8 col-lg-7 col-lg-offset-1 col-md-mt-100">
                                    <div valign="center" class="header-heading-title">
                                            <h3>
                                                <!--<a href="#"><img src="./Welcome To Your School E-Portal_files/logo.jpg" alt="Logo"></a>-->
                                                <b class="header-heading-title">ESCAE Univerity- Benin Republic</b>
                                            </h3>
                                    </div>
                                    <div valign="center" class="header-heading-title">
                                        
                                    </div>
                                    <div valign="center" class="header-heading-title">
                                        <h1>Official Staff-Student Information Portal</h1>
                                    </div>
                                    <div valign="center" class="header-heading-title">
                                        <a href="#" target="_blank" class="btn btn-danger" style="padding-top:13px;">
                                            <h6>For technical assistance, send an email to info@escaeuniversity.com or call 08024138532</h6>
                                        </a>
                                    </div>
                                </div>
                            </div>
                            <!-- .row end -->
                    </div>
                    <!-- .container end -->
                </div>
                <!-- .header-content end -->
            </div>
            <!-- .header-overlay end -->
        </header>
        <!-- #header end -->
            <!-- #counter -->
            <div id="counter" class="bg-img">
                <!-- .bg-overlay -->
                <div class="bg-overlay wrap-container8040">
                    
                    <!-- .container -->
                    <div class="container">
                        
                        <!-- .row -->
                        <div class="row">
                            
                            <div class="col-sm-3"> <!-- 1 -->
                                <div class="affa-counter-txt">
                                    <i class="fa fa-smile-o"></i>
                                    <h4> <asp:Label ID="visitor" runat="server" Text="0"></asp:Label></h4>
                                    <p>Visitors</p>
                                </div>
                            </div>
                            
                            <div class="col-sm-3"> <!-- 2 -->
                                <div class="affa-counter-txt">
                                    <i class="fa fa-eye"></i>
                                    <h4> <asp:Label ID="result_view" runat="server" Text="0"></asp:Label></h4>
                                    <p>Result Viewers</p>
                                </div>
                            </div>
                            
                            <div class="col-sm-3"> <!-- 3 -->
                                <div class="affa-counter-txt">
                                    <i class="fa fa-users"></i>
                                    <h4> <asp:Label ID="student" runat="server" Text="0"></asp:Label></h4>
                                    <p>Students</p>
                                </div>
                            </div>
                            
                            <div class="col-sm-3"> <!-- 4 -->
                                <div class="affa-counter-txt">
                                    <i class="fa fa-users"></i>
                                    <h4> <asp:Label ID="staff" runat="server" Text="0"></asp:Label></h4>
                                    <p>Staff</p>
                                </div>
                            </div>
                        </div>
                        <!-- .row end -->
                    </div>
                    <!-- .container end -->
                </div>
                <!-- .bg-overlay end -->
                <div class="bg-img-base in" style="background-image:url(https://kingston.schooleportal.com/welcomeAssets/images/content/bg/2.jpg);"></div> <!-- background image -->
            </div>
            <!-- #counter end -->
            
           
            <!-- #footer -->
            <footer id="footer">
                <!-- .container -->
                <div class="container">
                    <p class="copyright-txt">©2019 <a>Powered by Complesol Nigeria</a> - All rights reserved.</p>
                    <div class="socials">
                        <a href="https://kingston.schooleportal.com/#" title="Facebook" class="link-facebook"><i class="fa fa-facebook"></i></a>
                        <a href="https://kingston.schooleportal.com/#" title="Twitter" class="link-twitter"><i class="fa fa-twitter"></i></a>
                        <a href="https://kingston.schooleportal.com/#" title="Google Plus" class="link-google-plus"><i class="fa fa-google-plus"></i></a>
                    </div>
                </div>
                <!-- .container end -->
            </footer>
            <!-- #footer end -->
        </div>
        <!-- #main-wrap end -->
    
    <!-- #body-wrap end -->
  
    <!-- JavaScripts -->
	<script type="text/javascript" src="./Welcome To Your School E-Portal_files/jquery-1.11.3.min.js.download"></script>
    <script type="text/javascript" src="./Welcome To Your School E-Portal_files/jquery-migrate-1.2.1.min.js.download"></script>
    <script type="text/javascript" src="./Welcome To Your School E-Portal_files/bootstrap.min.js.download"></script>
    <script type="text/javascript" src="./Welcome To Your School E-Portal_files/jquery.easing.min.js.download"></script>
    <script type="text/javascript" src="./Welcome To Your School E-Portal_files/smoothscroll.js.download"></script>
	<script type="text/javascript" src="./Welcome To Your School E-Portal_files/response.min.js.download"></script>
	<script type="text/javascript" src="./Welcome To Your School E-Portal_files/jquery.placeholder.min.js.download"></script>
	<script type="text/javascript" src="./Welcome To Your School E-Portal_files/jquery.fitvids.js.download"></script>
	<script type="text/javascript" src="./Welcome To Your School E-Portal_files/jquery.imgpreload.min.js.download"></script>
	<script type="text/javascript" src="./Welcome To Your School E-Portal_files/waypoints.min.js.download"></script>
    <script type="text/javascript" src="./Welcome To Your School E-Portal_files/slick.min.js.download"></script>
    <script type="text/javascript" src="./Welcome To Your School E-Portal_files/jquery.fancybox.pack.js.download"></script>
    <script type="text/javascript" src="./Welcome To Your School E-Portal_files/jquery.fancybox-media.js.download"></script>
    <script type="text/javascript" src="./Welcome To Your School E-Portal_files/jquery.counterup.min.js.download"></script>
    <script type="text/javascript" src="./Welcome To Your School E-Portal_files/parallax.min.js.download"></script>
	<script type="text/javascript" src="./Welcome To Your School E-Portal_files/gmaps.js.download"></script>
	<script type="text/javascript" src="./Welcome To Your School E-Portal_files/script.js.download"></script>
    
    <script type="text/javascript">
	var map;
	
	map = new window.GMaps({
		div: '#companyMap',
		lat: -12.0411925,
		lng: -77.0282043,
		scrollwheel: false,
		zoomControl: false,
		disableDoubleClickZoom: false,
		disableDefaultUI: true
	});
	
	map.addMarker({
		lat: -12.042,
		lng: -77.028333,
		title: 'Complesol Nigeria',
		infoWindow: {
			content: 'Complesol Nigeria'
		}
	});
    </script>
<!--#######################################################################################################################################################3-->
    </form>
</body>
</html>
