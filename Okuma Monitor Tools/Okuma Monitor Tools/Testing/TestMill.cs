using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Okuma_Monitor_Tools.Testing
{
  public  class TestMill:ITestMill
    {
        public ITestAPI GenericTestAPI { get; set; }

        //public TestLathe(ITestAPI GenericAPI)
        public TestMill()
        {
            //GenericTestAPI = GenericAPI;
        }

        public void Dispose()
        {
            MessageBox.Show("TestMillAPI Closing");
        }

        public void CreateMessage(string message)
        {
            MessageBox.Show(message,"TestMill");
        }
    }
}
