using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
//using ServiceStack;

namespace Okuma_Monitor_Tools.Testing
{
   public class TestLathe:ITestLathe
   {
       //public ITestAPI GenericTestAPI { get; set; }

        //public TestLathe(ITestAPI GenericAPI)
        public TestLathe()
        {
            //GenericTestAPI = GenericAPI;
        }




        public void Dispose()
        {
            MessageBox.Show("TestLatheAPI Closing");
        }

        public void CreateMessage(string message)
        {
            MessageBox.Show(message, "TestLatheAPI");
        }
    }
}
