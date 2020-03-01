<?php

require('DBConnection.php');
session_start();
if($_SESSION["userId"]==false)
{
    header('location:loginPage.php');
}
else{
    $uname=$_SESSION["userId"];
    echo "WELCOME $uname";
}


?>