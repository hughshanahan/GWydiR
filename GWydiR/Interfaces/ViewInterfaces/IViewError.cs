using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GWydiR.Interfaces.ViewInterfaces
{
    public interface IViewError
    {
        void NotifyOfError(Exception e);
    }
}
