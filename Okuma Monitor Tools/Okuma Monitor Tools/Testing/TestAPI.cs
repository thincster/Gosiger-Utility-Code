using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Okuma_Monitor_Tools.Testing
{
    class TestAPI:ITestAPI
    {
        public void Dispose()
        {
            MessageBox.Show("TestAPI Closing");
        }

        public void CreateMessage(string message)
        {
            MessageBox.Show(message, "TestAPI");
        }
    }
}
