using POKOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public static class DAO
    {

        public static int FolderCreation(String cfolder,int pfolder)
        {
            String mysql = "";
            String mysql1 = "";
            if (pfolder == 0)
            {
                mysql = String.Format("SELECT * FROM foldertable where folderName='{0}' and parentFolderId is NULL", cfolder);
            }
            else
            {
                mysql = String.Format("SELECT * FROM foldertable where folderName='{0}' and parentFolderId='{1}'", cfolder, pfolder);
            }
            using (DBConnection conn = new DBConnection())
            {
                var reader = conn.ExcueteReader(mysql);
                if (!reader.Read())
                {
                    using (DBConnection conn1 = new DBConnection())
                    {
                        if (pfolder == 0)
                        {
                            mysql1 = String.Format("INSERT INTO foldertable (folderName) VALUES ('{0}')", cfolder);
                        }
                        else
                        {
                            mysql1 = String.Format("INSERT INTO foldertable (folderName,parentFolderId) VALUES ('{0}','{1}')", cfolder, pfolder);
                        }

                        int result = conn1.ExcueteQuery(mysql1);

                        if (result == 1)
                        {
                            FolderPoko folder = new FolderPoko();
                            folder.folderName = cfolder;
                            folder.parentFolderId = pfolder;
                            String query = "SELECT folderId FROM eadproject.folders ORDER BY folderId DESC LIMIT 1";
                            var record = conn1.ExcueteScalar(query);
                            int fid = Int32.Parse(record.ToString());
                            folder.folderId = fid;
                            return folder.folderId;
                        }
                    }
                }
                else
                {
                    return 0;
                }
            }

            return 0;
        }


        public static List<FolderPoko> GetFolders(int parent)
        {
            String mysql= "";
            List<FolderPoko> List = new List<FolderPoko>();
            if (parent == 0)
            {
                mysql = String.Format("SELECT * FROM foldertable where parentFolderId IS NULL");
            }
            else
            {
                mysql = String.Format("SELECT * FROM foldertable where parentFolderId='{0}'" ,parent);
            }
            using (DBConnection connection = new DBConnection())
            {
                var reader = connection.ExcueteReader(mysql);
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        FolderPoko folder = new FolderPoko();
                        folder.folderId = reader.GetInt32(0);
                        folder.folderName = reader.GetString(1);
                        if (parent == 0)
                        {
                            folder.parentFolderId = 0;
                        }
                        else
                        {
                            folder.parentFolderId = reader.GetInt32(2);
                        }
                        List.Add(folder);
                    }
                }
                else
                {
                    List = null;
                }
                return List;
            }

        }


        public static int SaveUser(UserPoko userPoko)
        {
            String mysql = String.Format("INSERT INTO usertable(name,password,login) VALUES('{0}', '{1}', '{2}')", userPoko.name, userPoko.password, userPoko.login);
            mysql = mysql + "; Select LAST_INSERT_ID()";
            using (DBConnection conn = new DBConnection())
            {
                int userId = 0;
                if (userPoko.userId == 0)
                {
                    try
                    {
                        var result = conn.ExcueteScalar(mysql);
                        String ans = result.ToString();
                        userId = Int32.Parse(ans);
                    }
                    catch (Exception exception)
                    {
                        userId = 0;
                    }
                }
                return userId;
            }

        }

        public static UserPoko UserValidation(String login, String password)
        {
            String query = String.Format("SELECT * FROM usertable where login='{0}' and password='{1}'", login, password);
            using (DBConnection connection = new DBConnection())
            {
                UserPoko user = new UserPoko();
                var reader = connection.ExcueteReader(query);
                if (reader.Read())
                {
                    user.userId = reader.GetInt32(0);
                    user.name = reader.GetString(1);
                    user.password = reader.GetString(2);
                    user.login = reader.GetString(3);
                }
                else
                {
                    user = null;
                }
                return user;
            }
        }
    }
}
