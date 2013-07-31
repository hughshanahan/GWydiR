using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GWydiR.Handlers
{
    /// <summary>
    /// An object contraining delegate definitions of event handlers for the GWydiR GUI.
    /// </summary>
    public static class GWydiRHandlers
    {
        public delegate void NewDataEventHandler(object data);
        public delegate int ChangedSelectionHandler(int index);
    }
}
