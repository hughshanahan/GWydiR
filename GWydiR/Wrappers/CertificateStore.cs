﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography.X509Certificates;

namespace GWydiR.Wrappers
{
    /// <summary>
    /// This class wraps the X509Store objects from System.Security.Cryptography.X509Certificates, the use of this is to
    /// provide a means to mock methods belonging to the wrapped object.
    /// </summary>
    public class CertificateStore
    {

        /// <summary>
        /// A refference to an object that is wrapped by an instancce of this class.
        /// </summary>
        public X509Store store { get; set; }

        public CertificateStore()
        {
        }

        public CertificateStore(X509Store store)
        {
            this.store = store;
        }

        public virtual void Open(OpenFlags flag)
        {
            store.Open(flag);
        }

        public virtual void Close()
        {
            store.Close();
        }

        public virtual X509Certificate2Collection GetCertificates()
        {
            return store.Certificates;
        }

        public virtual void Add(X509Certificate2 certificate)
        {
            store.Add(certificate);
        }

        public virtual void Remove(X509Certificate2 certificate)
        {
            store.Remove(certificate);
        }
    }
}
