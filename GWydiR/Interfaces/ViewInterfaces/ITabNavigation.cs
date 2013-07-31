using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GWydiR.Interfaces.ViewInterfaces
{
    public interface ITabNavigation
    {
        void RegisterNext(EventHandler nextHandler,int tabNumber);
        void RegisterPrevious(EventHandler previousHandler,int tabNumber);
        void DeRegisterNext(EventHandler nextHandler, int tabNumber);
        void DeRegisterPrevious(EventHandler previousHandler, int tabNumber);
        void ChangeTab(int i);
    }
}
