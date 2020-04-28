using BAL;
using POKOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;

namespace Web_Api.Controllers
{
    public class ValuesController : ApiController
    {
        [Authorize]
        [HttpPost]
        public Object FolderCreation()
        {
            String fname = HttpContext.Current.Request["fname"];
            int fid = Int32.Parse(HttpContext.Current.Request["fid"]);
            var identity = User.Identity as ClaimsIdentity;
            Object data = null;
          
            if (identity != null)
            {
                int id = 0;
                bool flag = false;
                int FID=BO.FolderCreation(fname, fid);
                if (FID!= 0)
                {
                    id = FID;
                    flag = true;
                }
                data = new
                {
                    success = flag,
                    folderId = id
                };
            }
            return data;
        }


        [HttpPost]
        [Authorize]
        public Object GetFolders()
        {
            int fid = Int32.Parse(HttpContext.Current.Request["fid"]);
            var identity = User.Identity as ClaimsIdentity;
            Object data = null;
            if (identity != null)
            {

                List<FolderPoko> foldersList = BO.GetFolders(fid);
                data = new
                {
                    folders = foldersList,
                };
            }
            return data;
        }


    }
}
