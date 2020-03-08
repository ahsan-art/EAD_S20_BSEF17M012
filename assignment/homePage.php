<?php

require('DBConnection.php');
session_start();
if($_SESSION["userId"]==false)
{
    header('location:loginPage.php');
}
else{
    $uname=$_SESSION["userId"];
}

?>
<!DOCTYPE html>
<html lang="en">

<head>
<meta charset="utf-8">
  <meta name="viewport" content="width=device-width, initial-scale=1">
  <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css">
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script> 
 <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>
 <link href="home.css" rel="stylesheet" type="text/css" ></link>
 <script>
let folderId="";
let flag=true;
</script>
</head>



<body>

<script>
function homeDisplay() {
  var xhttp = new XMLHttpRequest();
  xhttp.onreadystatechange = function() {
    if (this.readyState == 4 && this.status == 200) {
       let arr=JSON.parse(this.responseText);
       let mdiv=$("<div></div>");
       mdiv.addClass("sdiv");
        for (let i = 0; i < arr.data.length; i++) {
            let div = $("<div></div").addClass("innerDivs");
            div.attr("fid",arr.data[i].fid);
            div.text(arr.data[i].fname);
            mdiv.append(div);
        }
        $(".folders-div").append(mdiv);

    }
  };
  xhttp.open("POST", "http://localhost/assignment/projectApi.php", true);
  xhttp.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
  xhttp.send("action=homeFolders&fid=null");
}

homeDisplay();
</script>

<nav class="navbar">
  <div class="container-fluid">
    <div class="navbar-header">
      <a class="navbar-brand" href="homePage.php">My Drive</a>
    </div>
    <ul class="nav navbar-nav">
      <li class="active"><a href="homePage.php">Home</a></li>
    </ul>
  </div>
</nav>


<script>

$(document).ready(function() {
      $("#create-folder").on("click",function(){
        let folderName=$("#inp").val();
        var xhttp = new XMLHttpRequest();
  xhttp.onreadystatechange = function() {
    if (this.readyState == 4 && this.status == 200) {
       let arr=JSON.parse(this.responseText);
       
        if(arr.data[0].flag=="true")
        {
            let div = $("<div></div>").addClass("innerDivs");
            div.attr("fid",arr.data[0].id);
            div.text(folderName);
            $(".folders-div .sdiv").append(div);
        }
    }
  };
  xhttp.open("POST", "http://localhost/assignment/projectApi.php", true);
  xhttp.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
  xhttp.send("action=createFolders&fname="+folderName+"&fid="+folderId);

      })

})

$(document).ready(function() {
  $(".modal").on("hidden.bs.modal", function() {
    $("#inp").val("");
  });
});

</script>   




<div class="container-fluid">
            <button type="button" class="btn" id="btn" data-toggle="modal" data-target="#myModal">
              CREATE FOLDER
            </button>

            <div class="modal modal-class" id="myModal">
                <div class="modal-dialog">
                    <div class="modal-content">


                        <div class="modal-header">
                            <span><b>MY DRIVE</b></span>
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                        </div>

                        <div class="modal-body mb">
                            <b><span style="color: black; ">ENTER YOUR FOLDER NAME</span></b>
                            <div>
                                <input type="text" name="child" class="input" id="inp">
                            </div>
                        </div>

                        <div class="modal-footer">
                            <button  class="btn btn1"  name="createbtn" id="create-folder">CREATE FOLDER</button>
                          
                          </div>

                    </div>
                </div>
            </div>


</div>

<script>
      $(document).ready(function() {
      $(".folders-div").on("dblclick",".innerDivs",function(){
          let fid=$(this).attr("fid");
          folderId=fid;
          var xhttp = new XMLHttpRequest();
  xhttp.onreadystatechange = function() {
    if (this.readyState == 4 && this.status == 200) {
       let arr=JSON.parse(this.responseText);
       let mdiv=$("<div></div>");
       mdiv.addClass("sdiv");
        for (let i = 0; i < arr.data.length; i++) {
            let div = $("<div></div").addClass("innerDivs");
            div.attr("fid",arr.data[i].fid);
            div.text(arr.data[i].fname);
            mdiv.append(div);
        }
        $(".folders-div").append(mdiv);
        $(".folders-div").children().not(':last').remove();
         $(".folders-div div div").addClass("innerDivs");

    }
  };
  xhttp.open("POST", "http://localhost/assignment/projectApi.php", true);
  xhttp.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
  xhttp.send("action=homeFolders&fid="+fid);

      })

})

</script>

<script>
    $(document).ready(function(){
        $(".folders-div").on("click",".innerDivs",function(){
           if(flag)
           {
            $(this).closest(".folders-div").find(".sc").css("border","0px");
            $(this).css("border","4px solid red").addClass("sc");
            flag=false;
           }
           else{
            $(this).css("border","0px")
            flag=true;
           }
        })
    })

    </script>


<div class="folders-div">


</div>



</body>


</html>