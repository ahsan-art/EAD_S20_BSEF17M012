<!DOCTYPE html>
<html lang="en">

<head>
    <title>Login V8</title>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="icon" type="image/png" href="images/icons/favicon.ico" />

    <link rel="stylesheet" type="text/css" href="vendor/bootstrap/css/bootstrap.min.css">

    <link rel="stylesheet" type="text/css" href="fonts/font-awesome-4.7.0/css/font-awesome.min.css">

    <link rel="stylesheet" type="text/css" href="vendor/animate/animate.css">

    <link rel="stylesheet" type="text/css" href="vendor/css-hamburgers/hamburgers.min.css">

    <link rel="stylesheet" type="text/css" href="vendor/animsition/css/animsition.min.css">

    <link rel="stylesheet" type="text/css" href="vendor/select2/select2.min.css">

    <link rel="stylesheet" type="text/css" href="vendor/daterangepicker/daterangepicker.css">
    <script src="vendor/jquery/jquery-3.2.1.min.js"></script>

    <script src="vendor/animsition/js/animsition.min.js"></script>

    <script src="vendor/bootstrap/js/popper.js"></script>
    <script src="vendor/bootstrap/js/bootstrap.min.js"></script>

    <script src="vendor/select2/select2.min.js"></script>

    <script src="vendor/daterangepicker/moment.min.js"></script>
    <script src="vendor/daterangepicker/daterangepicker.js"></script>

    <script src="vendor/countdowntime/countdowntime.js"></script>

    <script src="js/main.js"></script>

    <link rel="stylesheet" type="text/css" href="css/util.css">
    <link rel="stylesheet" type="text/css" href="css/main.css">

    <?php
     require('DBConnection.php');
     session_start();
        $errorMsg="";
        if(isset($_REQUEST["loginButton"]))
{
    $userId=$_REQUEST["userId"];
    $password=$_REQUEST["pass"];
    $sql="SELECT * FROM usertable where login='$userId' and password='$password'";
    $result=mysqli_query($connection,$sql);
    $recs=mysqli_num_rows($result);
    
        if($recs==1)
        {
            $_SESSION["userId"]=$userId;
            header('Location:homePage.php');
        }
        else{
            $errorMsg="Wrong Login or Password";
        }
    }
    ?>
</head>

<body>

    <div class="limiter">
        <div class="container-login100">
            <div class="wrap-login100">
                <form class="login100-form validate-form p-l-55 p-r-55 p-t-178" action="loginPage.php" method="POST">
                    <span class="login100-form-title">
						LOGIN IN
					</span>

                    <div class="wrap-input100 validate-input m-b-16" data-validate="Please enter username">
                        <input class="input100" type="text" name="userId" placeholder="Username" required>
                        <span class="focus-input100"></span>
                    </div>

                    <div class="wrap-input100 validate-input" data-validate="Please enter password">
                        <input class="input100" type="password" name="pass" placeholder="Password" required>
                        <span class="focus-input100"></span>
                    </div>

                    <div class="text-right p-t-13 p-b-23">
                    </div>

                    <div class="container-login100-form-btn">
                        <button class="login100-form-btn" type="submit" name="loginButton"> 
							LOGIN IN
						</button>
                    </div>
                    <div id="msgDiv" style="color:red;">
                       <b> <?php echo $errorMsg;?><b>
                    <div>

                    <div class="flex-col-c p-t-50 p-b-40">
                        

                        <a href="signupPage.php" class="txt3">
							Sign up now
						</a>
                    </div>
                </form>
            </div>
        </div>
    </div>
</body>

</html>