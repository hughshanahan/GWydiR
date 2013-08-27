using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GWydiR.Models
{
    /// <summary>
    /// This class is responcible for binding data to the WPFviews.
    /// </summary>
    public class ViewModel
    {
        public AuthorisationModel Authorisation;

        public ViewModel()
        {
            Authorisation = new AuthorisationModel();
        }

    }
}
