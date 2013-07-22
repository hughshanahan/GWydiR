using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GWydiR.Interfaces.ViewInterfaces
{
    public interface ITabNavigation
    {
        void RegisterNext(EventHandler nextHandler);
        void RegisterPrevious(EventHandler previousHandler);
        void DeRegisterNext(EventHandler nextHandler);
        void DeRegisterPrevious(EventHandler previousHandler);
    }
}
