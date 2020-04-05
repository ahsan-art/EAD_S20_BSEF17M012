using Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public static class DO
    {
        public static UserPoko fillPoko(SqlDataReader dataReader)
        {
            UserPoko user = new UserPoko();
            user.UserID = dataReader.GetInt32(0);
            user.Name = dataReader.GetString(1);
            user.Login = dataReader.GetString(2);
            user.Password = dataReader.GetString(3);
            user.Email = dataReader.GetString(4);
            user.Gender = dataReader.GetString(5);
            user.Address = dataReader.GetString(6);
            user.Age = dataReader.GetInt32(7);
            user.NIC = dataReader.GetString(8);
            user.DOB = dataReader.GetDateTime(9);
            user.isCricket = dataReader.GetBoolean(10);
            user.isHockey = dataReader.GetBoolean(11);
            user.Chess = dataReader.GetBoolean(12);
            user.ImageName = dataReader.GetString(13);
            user.createdOn = dataReader.GetDateTime(14);
            user.createdBy = dataReader.GetString(15);
            user.modifiedOn = dataReader.GetDateTime(16);
            user.modifiedBy = dataReader.GetString(17);
            user.isActive = dataReader.GetBoolean(18);
            return user;
        }
        public static int validateAdmin(AdminPoko admin)
        {
            String Query = String.Format("Select count(*) from [dbo].[Admin] where Login='{0}' and Password='{1}'", admin.Login, admin.Password);
            using (Connection dBConnection = new Connection())
            {
                var obj = dBConnection.ExcueteScalar(Query);
                String records = obj.ToString();
                int count = int.Parse(records);
                return count;
            }
        }

        public static DataTable getAllUsersPoko()
        {
           
            DataTable table = new DataTable();
            String Query = "Select * from dbo.Users";
            using (Connection conn = new Connection())
            {
                var reader = conn.ExcueteReader(Query);
                table.Load(reader);
                return table;
            }
        }

        public static int Save(UserPoko user)
        { 
            String Query;
            if (user.UserID > 0)
            {
                Query = String.Format("update [dbo].[Users]  set Name='{0}',Login='{1}',Password='{2}',Email='{3}', Gender='{4}',Address='{5}',Age='{6}',NIC='{7}',DOB='{8}',IsCricket='{9}',Hockey='{10}',Chess='{11}',ImageName='{12}',CreatedOn='{13}',CreatedBy='{14}',ModifiedOn='{15}',ModifiedBy='{16}',isActive='{17}' where UserID='{18}'", user.Name, user.Login, user.Password, user.Email, user.Gender, user.Address, user.Age, user.NIC, user.DOB, user.isCricket, user.isHockey, user.Chess, user.ImageName, user.createdOn, user.createdBy, user.modifiedOn, user.modifiedBy, user.isActive, user.UserID);
            }
            else
            {
                Query = String.Format("insert into [dbo].[Users]  (Name,Login,Password,Email,Gender,Address,Age,NIC,DOB,IsCricket,Hockey,Chess,ImageName,CreatedOn,CreatedBy,ModifiedOn,ModifiedBy,isActive)  Values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}')", user.Name, user.Login, user.Password, user.Email, user.Gender, user.Address, user.Age, user.NIC, user.DOB, user.isCricket, user.isHockey, user.Chess, user.ImageName, user.createdOn, user.createdBy, user.modifiedOn, user.modifiedBy, user.isActive);
                Query = Query + ";Select Scope_Identity()";
            }

            using (Connection conn = new Connection())
            {
                int userId = 0;
                if (user.UserID > 0)
                {
                   conn.ExcueteQuery(Query);
                }
                else
                {
                    var recs =conn.ExcueteScalar(Query);
                    String res = recs.ToString();
                    userId = int.Parse(res);
                }
                return userId;
            }

        }

        public static UserPoko validateUser(String login, String password, String email, int uid, int flag)
        {
            UserPoko user = null;
            String Query = "";
            if (flag == 1)
            {
                Query = String.Format("Select * from [dbo].[Users] where Login='{0}' and Password='{1}'", login, password);
            }
            else if (flag == 2)
            {
                Query = String.Format("Select * from [dbo].[Users] where Login='{0}'or Email='{1}'", login, email);
            }
            else
            {
                Query = String.Format("Select * from [dbo].[Users] where (Login='{0}' or Email='{1}') and UserID!='{2}'", login, email, uid);
            }
            using (Connection conn = new Connection())
            {
                var reader =conn.ExcueteReader(Query);
                if (reader.Read())
                {
                    user = fillPoko(reader);
                }
                return user;
            }
        }

   

        public static int validateUserWithEmail(String email)
        {
            String Query = String.Format("Select count(*) from [dbo].[Users] where Email='{0}'", email);
            using (Connection conn = new Connection())
            {
                var recs = conn.ExcueteScalar(Query);
                String res = recs.ToString();
                int count = Int32.Parse(res);
                return count;
            }
        }


        public static UserPoko getUserPokoById(int userId)
        {
            String query = String.Format("Select * from [dbo].[Users] where UserID='{0}'", userId);
            using (Connection conn = new Connection())
            {
                var reader = conn.ExcueteReader(query);
                UserPoko user = null;
                if (reader.Read())
                {
                    user = fillPoko(reader);
                }
                return user;
            }
        }

        public static UserPoko getUserPokoByEmail(String email)
        {
            String query = String.Format("Select * from [dbo].[Users] where Email='{0}'", email);
            using (Connection conn = new Connection())
            {
                var reader = conn.ExcueteReader(query);
                UserPoko user = null;
                if (reader.Read())
                {
                    user = fillPoko(reader);
                }
                return user;
            }
        }

       
    }
}
