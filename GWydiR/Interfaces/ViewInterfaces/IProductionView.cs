﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GWydiR.Interfaces.ViewInterfaces
{
    public interface IProductionView
    {
        int GetInstanceCount();
        void SetVmSizes(List<string> VmSizes);
        string GetVmSize();
    }
}
