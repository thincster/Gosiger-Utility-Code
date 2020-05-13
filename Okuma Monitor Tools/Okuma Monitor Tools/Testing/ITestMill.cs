using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okuma_Monitor_Tools.Testing
{
   public interface ITestMill: IDisposable
   {
     
        void CreateMessage(string message);
   }
}
