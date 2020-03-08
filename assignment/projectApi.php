<?php
require('DbConnection.php');

if(isset($_REQUEST["action"]))
{
    if($_REQUEST["action"]=="homeFolders")
    {
        $fid=$_REQUEST["fid"];
        if($fid=="null")
        {
            $sql="SELECT * FROM folderTable where parentFolderId IS NULL";
        }
        else{
            $sql="SELECT * FROM folderTable where parentFolderId='$fid'";
        }
       
        $res=mysqli_query($connection,$sql);
        $records=mysqli_num_rows($res);
        $Folders=array();
            while($row=mysqli_fetch_assoc($res))
            {
                array_push($Folders,array("fid"=>$row["folderId"],"fname"=>$row["folderName"]));
            }
            $result["data"]=$Folders;    

        echo json_encode($result);
    }
    else if($_REQUEST["action"]=="createFolders")
    {
       
        $arr1=array();
        $sql="";
        $flag="false";
        $fid="";

        $parent=$_REQUEST["fid"];
         $child=$_REQUEST["fname"];

            if($parent=="")
            {
                $sql1="SELECT * FROM folderTable where folderName='$child' and parentFolderId is NULL";
                $result1=mysqli_query($connection,$sql1);
                if($result1==true)
                {
                    $recs=mysqli_num_rows($result1);
                    if(!$recs>0)
                    {
                        $sql="INSERT INTO folderTable (folderName) VALUES ('$child')";
                    }
                }  
            }
            else
            {
                $sql1="SELECT * FROM folderTable where folderName='$child' and parentFolderId='$parent'";
                $result1=mysqli_query($connection,$sql1);
                if($result1==true)
                {
                    $recs=mysqli_num_rows($result1);
                    if(!$recs>0)
                    {
                        $sql="INSERT INTO foldertable (folderName,parentFolderId) VALUES ('$child','$parent')";
                    }
                }  
               
            }
            if($sql!="" && mysqli_query($connection,$sql)==true)
            {
                $query="SELECT * FROM folderTable ORDER BY folderId DESC LIMIT 1";
                $result=mysqli_query($connection,$query);
                $records=mysqli_num_rows($result);
               
                while($row=mysqli_fetch_assoc($result))
                {
                    $fid=$row["folderId"];
                }
                $flag="true";
                array_push($arr1,array("id"=>$fid,"flag"=>$flag));
                $output["data"]=$arr1;

            }
            else{
                array_push($arr1,array("id"=>"","flag"=>$flag));
                $output["data"]=$arr1;
            }
          
            echo json_encode($output);
    }
}

?>