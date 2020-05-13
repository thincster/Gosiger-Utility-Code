using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Okuma_Monitor_Tools.Enums;
using Okuma_Monitor_Tools.Okuma;

namespace Okuma_Monitor_Tools.Testing
{
   public class TestAPIFactory
   {
       private static ITestAPI _genericTestAPI;

       public static ITestAPI GetTestApi()
       {
           if (_genericTestAPI != null)
           {
               return _genericTestAPI;
           }

           _genericTestAPI = new TestAPISim();

           var machineType = "L";

           if (machineType == "L")
           {
               _genericTestAPI=new TestAPILathe();
           }

           return _genericTestAPI;
       }

       

       public static ITestLathe GetTestLathe()
       {
          //ITestLathe rtnAPI = new TestLathe(GetTestApi());
          ITestLathe rtnAPI = new TestLathe();

            return rtnAPI;
       }

       public static ITestMill GetTestMill()
       {
           //ITestLathe rtnAPI = new TestLathe(GetTestApi());
           ITestMill rtnAPI = new TestMill();

           return rtnAPI;
       }
    }
}
