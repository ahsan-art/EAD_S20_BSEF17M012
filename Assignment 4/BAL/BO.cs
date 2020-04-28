using DAL;
using POKOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public static class BO
    {
        public static int FolderCreation(String cfolder, int pfolder)
        {
            return DAO.FolderCreation(cfolder, pfolder);
        }


        public static List<FolderPoko> GetFolders(int parent)
        {
            return DAO.GetFolders(parent);
        }


        public static int SaveUser(UserPoko userPoko)
        {
            return DAO.SaveUser(userPoko);
        }

        public static UserPoko UserValidation(String login, String password)
        {
            return DAO.UserValidation(login, password);
        }
    }
}
