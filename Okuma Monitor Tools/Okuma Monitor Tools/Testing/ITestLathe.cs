using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okuma_Monitor_Tools.Testing
{
   public interface ITestLathe: IDisposable
   {
       
       //ITestAPI GenericTestAPI { get; set; }
       void CreateMessage(string message);

   }
}
