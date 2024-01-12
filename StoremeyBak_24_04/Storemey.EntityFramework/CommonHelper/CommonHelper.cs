using Abp.UI;
using Storemey.EntityFramework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Hosting;

namespace Storemey.EntityFramework
{
    public static class CommonEntityHelper
    {


        public static string ConString = ConfigurationManager.AppSettings["dbConnectionString"];

        public static bool IsTenancyExists(string TenancyName)
        {
            if (StoremeyConsts.previousStorenameCheck == TenancyName)
            {
                return true;
            }
            string connectionString = ConString.Replace("[dbName]", TenancyName);
            SqlConnection dataConnection = new SqlConnection(connectionString);
            try
            {
                dataConnection.Open();
                dataConnection.Close();
                StoremeyConsts.previousStorenameCheck = TenancyName;
                return true;
            }
            catch (SqlException e)
            {
                return false;
            }
        }



        public static string TenancyConnectionString(string HostName)
        {
            string ConnectionString = string.Empty;

            if (!string.IsNullOrEmpty(HostName))
            {
                ConnectionString = ConString.Replace("[dbName]", HostName);
            }
            else
            {
                ConnectionString = ConString.Replace("[dbName]", "StoremeyMaster");
            }

            return ConnectionString;
        }

        public static void UpdateSQL()
        {
            try
            {   
                using (var context = new SimpleTaskSystemDbContext(StoremeyConsts.tenantName))
                {
                    String Countries = HostingEnvironment.ApplicationPhysicalPath + "/SQL_Query/country.txt";
                    String Currancy = HostingEnvironment.ApplicationPhysicalPath + "/SQL_Query/Currancy.txt";
                    String TimeZones = HostingEnvironment.ApplicationPhysicalPath + "/SQL_Query/Timezone_List.txt";
                    String States = HostingEnvironment.ApplicationPhysicalPath + "/SQL_Query/state_0.txt";
                    String States1 = HostingEnvironment.ApplicationPhysicalPath + "/SQL_Query/state_1.txt";
                    String States2 = HostingEnvironment.ApplicationPhysicalPath + "/SQL_Query/state_2.txt";
                    String States3 = HostingEnvironment.ApplicationPhysicalPath + "/SQL_Query/state_3.txt";
                    String States4 = HostingEnvironment.ApplicationPhysicalPath + "/SQL_Query/state_4.txt";

                    Countries = System.IO.File.ReadAllText(Countries);
                    Currancy = System.IO.File.ReadAllText(Currancy);
                    TimeZones = System.IO.File.ReadAllText(TimeZones);
                    States = System.IO.File.ReadAllText(States);
                    States1 = System.IO.File.ReadAllText(States1);
                    States2 = System.IO.File.ReadAllText(States2);
                    States3 = System.IO.File.ReadAllText(States3);
                    States4 = System.IO.File.ReadAllText(States4);




                    context.Database.ExecuteSqlCommand(Countries);
                    context.Database.ExecuteSqlCommand(Currancy);
                    context.Database.ExecuteSqlCommand(TimeZones);


                    //=====================================================================================

                    context.Database.ExecuteSqlCommand(States);
                    context.Database.ExecuteSqlCommand(States1);
                    context.Database.ExecuteSqlCommand(States2);
                    context.Database.ExecuteSqlCommand(States3);
                    context.Database.ExecuteSqlCommand(States4);

                    Countries = string.Empty;
                    States = string.Empty;
                    States1 = string.Empty;
                    States2 = string.Empty;
                    States3 = string.Empty;
                    States4 = string.Empty;



                    String Cities = HostingEnvironment.ApplicationPhysicalPath + "/SQL_Query/Cities_0.txt";


                    Cities = System.IO.File.ReadAllText(Cities);
                    context.Database.ExecuteSqlCommand(Cities);
                    Cities = string.Empty;


                    String Cities1 = HostingEnvironment.ApplicationPhysicalPath + "/SQL_Query/Cities_1.txt";

                    Cities1 = System.IO.File.ReadAllText(Cities1);
                    context.Database.ExecuteSqlCommand(Cities1);
                    Cities1 = string.Empty;

                    String Cities2 = HostingEnvironment.ApplicationPhysicalPath + "/SQL_Query/Cities_2.txt";

                    Cities2 = System.IO.File.ReadAllText(Cities2);
                    context.Database.ExecuteSqlCommand(Cities2);
                    Cities2 = string.Empty;


                    String Cities3 = HostingEnvironment.ApplicationPhysicalPath + "/SQL_Query/Cities_3.txt";

                    Cities3 = System.IO.File.ReadAllText(Cities3);
                    context.Database.ExecuteSqlCommand(Cities3);
                    Cities3 = string.Empty;

                    String Cities4 = HostingEnvironment.ApplicationPhysicalPath + "/SQL_Query/Cities_4.txt";


                    Cities4 = System.IO.File.ReadAllText(Cities4);
                    context.Database.ExecuteSqlCommand(Cities4);
                    Cities4 = string.Empty;

                    String Cities5 = HostingEnvironment.ApplicationPhysicalPath + "/SQL_Query/Cities_5.txt";

                    Cities5 = System.IO.File.ReadAllText(Cities5);
                    context.Database.ExecuteSqlCommand(Cities5);
                    Cities5 = string.Empty;


                    String Cities6 = HostingEnvironment.ApplicationPhysicalPath + "/SQL_Query/Cities_6.txt";

                    Cities6 = System.IO.File.ReadAllText(Cities6);
                    context.Database.ExecuteSqlCommand(Cities6);
                    Cities6 = string.Empty;

                    String Cities7 = HostingEnvironment.ApplicationPhysicalPath + "/SQL_Query/Cities_7.txt";

                    Cities7 = System.IO.File.ReadAllText(Cities7);
                    context.Database.ExecuteSqlCommand(Cities7);
                    Cities7 = string.Empty;

                    String Cities8 = HostingEnvironment.ApplicationPhysicalPath + "/SQL_Query/Cities_8.txt";

                    Cities8 = System.IO.File.ReadAllText(Cities8);
                    context.Database.ExecuteSqlCommand(Cities8);
                    Cities8 = string.Empty;

                    String Cities9 = HostingEnvironment.ApplicationPhysicalPath + "/SQL_Query/Cities_9.txt";

                    Cities9 = System.IO.File.ReadAllText(Cities9);
                    context.Database.ExecuteSqlCommand(Cities9);
                    Cities9 = string.Empty;

                    String Cities10 = HostingEnvironment.ApplicationPhysicalPath + "/SQL_Query/Cities_10.txt";

                    Cities10 = System.IO.File.ReadAllText(Cities10);
                    context.Database.ExecuteSqlCommand(Cities10);
                    Cities10 = string.Empty;

                    String Cities11 = HostingEnvironment.ApplicationPhysicalPath + "/SQL_Query/Cities_11.txt";

                    Cities11 = System.IO.File.ReadAllText(Cities11);
                    context.Database.ExecuteSqlCommand(Cities11);
                    Cities11 = string.Empty;

                    String Cities12 = HostingEnvironment.ApplicationPhysicalPath + "/SQL_Query/Cities_12.txt";

                    Cities12 = System.IO.File.ReadAllText(Cities12);
                    context.Database.ExecuteSqlCommand(Cities12);
                    Cities12 = string.Empty;


                    String Cities13 = HostingEnvironment.ApplicationPhysicalPath + "/SQL_Query/Cities_13.txt";
                    String Cities14 = HostingEnvironment.ApplicationPhysicalPath + "/SQL_Query/Cities_14.txt";
                    String Cities15 = HostingEnvironment.ApplicationPhysicalPath + "/SQL_Query/Cities_15.txt";
                    String Cities16 = HostingEnvironment.ApplicationPhysicalPath + "/SQL_Query/Cities_16.txt";
                    String Cities17 = HostingEnvironment.ApplicationPhysicalPath + "/SQL_Query/Cities_17.txt";
                    String Cities18 = HostingEnvironment.ApplicationPhysicalPath + "/SQL_Query/Cities_18.txt";
                    String Cities19 = HostingEnvironment.ApplicationPhysicalPath + "/SQL_Query/Cities_19.txt";
                    String Cities20 = HostingEnvironment.ApplicationPhysicalPath + "/SQL_Query/Cities_20.txt";
                    String Cities21 = HostingEnvironment.ApplicationPhysicalPath + "/SQL_Query/Cities_21.txt";
                    String Cities22 = HostingEnvironment.ApplicationPhysicalPath + "/SQL_Query/Cities_22.txt";
                    String Cities23 = HostingEnvironment.ApplicationPhysicalPath + "/SQL_Query/Cities_23.txt";
                    String Cities24 = HostingEnvironment.ApplicationPhysicalPath + "/SQL_Query/Cities_24.txt";
                    String Cities25 = HostingEnvironment.ApplicationPhysicalPath + "/SQL_Query/Cities_25.txt";
                    String Cities26 = HostingEnvironment.ApplicationPhysicalPath + "/SQL_Query/Cities_26.txt";
                    String Cities27 = HostingEnvironment.ApplicationPhysicalPath + "/SQL_Query/Cities_27.txt";
                    String Cities28 = HostingEnvironment.ApplicationPhysicalPath + "/SQL_Query/Cities_28.txt";
                    String Cities29 = HostingEnvironment.ApplicationPhysicalPath + "/SQL_Query/Cities_29.txt";
                    String Cities30 = HostingEnvironment.ApplicationPhysicalPath + "/SQL_Query/Cities_30.txt";
                    String Cities31 = HostingEnvironment.ApplicationPhysicalPath + "/SQL_Query/Cities_31.txt";
                    String Cities32 = HostingEnvironment.ApplicationPhysicalPath + "/SQL_Query/Cities_32.txt";
                    String Cities33 = HostingEnvironment.ApplicationPhysicalPath + "/SQL_Query/Cities_33.txt";
                    String Cities34 = HostingEnvironment.ApplicationPhysicalPath + "/SQL_Query/Cities_34.txt";
                    String Cities35 = HostingEnvironment.ApplicationPhysicalPath + "/SQL_Query/Cities_35.txt";
                    String Cities36 = HostingEnvironment.ApplicationPhysicalPath + "/SQL_Query/Cities_36.txt";
                    String Cities37 = HostingEnvironment.ApplicationPhysicalPath + "/SQL_Query/Cities_37.txt";
                    String Cities38 = HostingEnvironment.ApplicationPhysicalPath + "/SQL_Query/Cities_38.txt";
                    String Cities39 = HostingEnvironment.ApplicationPhysicalPath + "/SQL_Query/Cities_39.txt";
                    String Cities40 = HostingEnvironment.ApplicationPhysicalPath + "/SQL_Query/Cities_40.txt";
                    String Cities41 = HostingEnvironment.ApplicationPhysicalPath + "/SQL_Query/Cities_41.txt";
                    String Cities42 = HostingEnvironment.ApplicationPhysicalPath + "/SQL_Query/Cities_42.txt";
                    String Cities43 = HostingEnvironment.ApplicationPhysicalPath + "/SQL_Query/Cities_43.txt";
                    String Cities44 = HostingEnvironment.ApplicationPhysicalPath + "/SQL_Query/Cities_44.txt";
                    String Cities45 = HostingEnvironment.ApplicationPhysicalPath + "/SQL_Query/Cities_45.txt";
                    String Cities46 = HostingEnvironment.ApplicationPhysicalPath + "/SQL_Query/Cities_46.txt";
                    String Cities47 = HostingEnvironment.ApplicationPhysicalPath + "/SQL_Query/Cities_47.txt";
                    String Cities48 = HostingEnvironment.ApplicationPhysicalPath + "/SQL_Query/Cities_48.txt";
                    String Cities49 = HostingEnvironment.ApplicationPhysicalPath + "/SQL_Query/Cities_49.txt";
                    String Cities50 = HostingEnvironment.ApplicationPhysicalPath + "/SQL_Query/Cities_50.txt";
                    String Cities51 = HostingEnvironment.ApplicationPhysicalPath + "/SQL_Query/Cities_51.txt";
                    String Cities52 = HostingEnvironment.ApplicationPhysicalPath + "/SQL_Query/Cities_52.txt";
                    String Cities53 = HostingEnvironment.ApplicationPhysicalPath + "/SQL_Query/Cities_53.txt";


                    Cities13 = System.IO.File.ReadAllText(Cities13);
                    context.Database.ExecuteSqlCommand(Cities13);
                    Cities13 = string.Empty;

                    Cities14 = System.IO.File.ReadAllText(Cities14);
                    context.Database.ExecuteSqlCommand(Cities14);
                    Cities14 = string.Empty;

                    Cities15 = System.IO.File.ReadAllText(Cities15);
                    context.Database.ExecuteSqlCommand(Cities15);
                    Cities15 = string.Empty;

                    Cities16 = System.IO.File.ReadAllText(Cities16);
                    context.Database.ExecuteSqlCommand(Cities16);
                    Cities16 = string.Empty;

                    Cities17 = System.IO.File.ReadAllText(Cities17);
                    context.Database.ExecuteSqlCommand(Cities17);
                    Cities17 = string.Empty;

                    Cities18 = System.IO.File.ReadAllText(Cities18);
                    context.Database.ExecuteSqlCommand(Cities18);
                    Cities18 = string.Empty;

                    Cities19 = System.IO.File.ReadAllText(Cities19);
                    context.Database.ExecuteSqlCommand(Cities19);
                    Cities19 = string.Empty;

                    Cities20 = System.IO.File.ReadAllText(Cities20);
                    context.Database.ExecuteSqlCommand(Cities20);
                    Cities20 = string.Empty;

                    Cities21 = System.IO.File.ReadAllText(Cities21);
                    context.Database.ExecuteSqlCommand(Cities21);
                    Cities21 = string.Empty;

                    Cities22 = System.IO.File.ReadAllText(Cities22);
                    context.Database.ExecuteSqlCommand(Cities22);
                    Cities22 = string.Empty;

                    Cities23 = System.IO.File.ReadAllText(Cities23);
                    context.Database.ExecuteSqlCommand(Cities23);
                    Cities23 = string.Empty;

                    Cities24 = System.IO.File.ReadAllText(Cities24);
                    context.Database.ExecuteSqlCommand(Cities24);
                    Cities24 = string.Empty;

                    Cities25 = System.IO.File.ReadAllText(Cities25);
                    context.Database.ExecuteSqlCommand(Cities25);
                    Cities25 = string.Empty;

                    Cities26 = System.IO.File.ReadAllText(Cities26);
                    context.Database.ExecuteSqlCommand(Cities26);
                    Cities26 = string.Empty;

                    Cities27 = System.IO.File.ReadAllText(Cities27);
                    context.Database.ExecuteSqlCommand(Cities27);
                    Cities27 = string.Empty;

                    Cities28 = System.IO.File.ReadAllText(Cities28);
                    context.Database.ExecuteSqlCommand(Cities28);
                    Cities28 = string.Empty;

                    Cities29 = System.IO.File.ReadAllText(Cities29);
                    context.Database.ExecuteSqlCommand(Cities29);
                    Cities29 = string.Empty;

                    Cities30 = System.IO.File.ReadAllText(Cities30);
                    context.Database.ExecuteSqlCommand(Cities30);
                    Cities30 = string.Empty;

                    Cities31 = System.IO.File.ReadAllText(Cities31);
                    context.Database.ExecuteSqlCommand(Cities31);
                    Cities31 = string.Empty;

                    Cities32 = System.IO.File.ReadAllText(Cities32);
                    context.Database.ExecuteSqlCommand(Cities32);
                    Cities32 = string.Empty;

                    Cities33 = System.IO.File.ReadAllText(Cities33);
                    context.Database.ExecuteSqlCommand(Cities33);
                    Cities33 = string.Empty;

                    Cities34 = System.IO.File.ReadAllText(Cities34);
                    context.Database.ExecuteSqlCommand(Cities34);
                    Cities34 = string.Empty;

                    Cities35 = System.IO.File.ReadAllText(Cities35);
                    context.Database.ExecuteSqlCommand(Cities35);
                    Cities35 = string.Empty;

                    Cities36 = System.IO.File.ReadAllText(Cities36);
                    context.Database.ExecuteSqlCommand(Cities36);
                    Cities36 = string.Empty;

                    Cities37 = System.IO.File.ReadAllText(Cities37);
                    context.Database.ExecuteSqlCommand(Cities37);
                    Cities37 = string.Empty;

                    Cities38 = System.IO.File.ReadAllText(Cities38);
                    context.Database.ExecuteSqlCommand(Cities38);
                    Cities38 = string.Empty;

                    Cities39 = System.IO.File.ReadAllText(Cities39);
                    context.Database.ExecuteSqlCommand(Cities39);
                    Cities39 = string.Empty;

                    Cities40 = System.IO.File.ReadAllText(Cities40);
                    context.Database.ExecuteSqlCommand(Cities40);
                    Cities40 = string.Empty;

                    Cities41 = System.IO.File.ReadAllText(Cities41);
                    context.Database.ExecuteSqlCommand(Cities41);
                    Cities41 = string.Empty;

                    Cities42 = System.IO.File.ReadAllText(Cities42);
                    context.Database.ExecuteSqlCommand(Cities42);
                    Cities42 = string.Empty;

                    Cities43 = System.IO.File.ReadAllText(Cities43);
                    context.Database.ExecuteSqlCommand(Cities43);
                    Cities43 = string.Empty;

                    Cities44 = System.IO.File.ReadAllText(Cities44);
                    context.Database.ExecuteSqlCommand(Cities44);
                    Cities44 = string.Empty;

                    Cities45 = System.IO.File.ReadAllText(Cities45);
                    context.Database.ExecuteSqlCommand(Cities45);
                    Cities45 = string.Empty;

                    Cities46 = System.IO.File.ReadAllText(Cities46);
                    context.Database.ExecuteSqlCommand(Cities46);
                    Cities46 = string.Empty;

                    Cities47 = System.IO.File.ReadAllText(Cities47);
                    context.Database.ExecuteSqlCommand(Cities47);
                    Cities47 = string.Empty;

                    Cities48 = System.IO.File.ReadAllText(Cities48);
                    context.Database.ExecuteSqlCommand(Cities48);
                    Cities48 = string.Empty;

                    Cities49 = System.IO.File.ReadAllText(Cities49);
                    context.Database.ExecuteSqlCommand(Cities49);
                    Cities49 = string.Empty;

                    Cities50 = System.IO.File.ReadAllText(Cities50);
                    context.Database.ExecuteSqlCommand(Cities50);
                    Cities50 = string.Empty;

                    Cities51 = System.IO.File.ReadAllText(Cities51);
                    context.Database.ExecuteSqlCommand(Cities51);
                    Cities51 = string.Empty;

                    Cities52 = System.IO.File.ReadAllText(Cities52);
                    context.Database.ExecuteSqlCommand(Cities52);
                    Cities52 = string.Empty;

                    Cities53 = System.IO.File.ReadAllText(Cities53);
                    context.Database.ExecuteSqlCommand(Cities53);
                    Cities53 = string.Empty;



                    context.SaveChanges();


                }
            }
            catch (ThreadAbortException tex)
            {
                throw new UserFriendlyException("Ooppps! There is a problem!", "we are getting issue to save default entries.");
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException("Ooppps! There is a problem!", "we are getting issue to save default entries...");
            }

            Storemey.StoremeyConsts.defaultEntriesJobId = string.Empty;

        }

    }

}
