using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Org.BouncyCastle.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Pkcs;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace GWydiR.Containers
{
    /// <summary>
    /// This class represents an intermediate form of an X509 ceritifcate. Since such a certificate can be in multiple forms
    /// containing different ammounts of data about the certificte and it might be such that any of those forms is required, this class
    /// provides (provided appropriate data) the means to build certificate related data into thos require formats of certificate.
    /// These include:
    /// <list type="bullet">
    ///     <item>
    ///         <name>Pkcs12 (.pfx)</name>
    ///         <description>This type of certificate stores the end user certificate, the certificate validation chain and private key</description>
    ///     </item>
    ///     <item>
    ///         <name>X509 (Exportable Private Key)</name>
    ///         <description>This type of certificate stores the private key </description>
    ///     </item>
    ///     <item>
    ///         <name>X509 (.cer)</name>
    ///         <description>This type of certificate provides only the public key for use encrypting or signing messages to the holder of a certificate with the private key</description>
    ///     </item>
    /// </list>
    /// </summary>
    public class PkcsCertificate
    {
        /// <summary>
        /// <value>A string representing the password to a .pfx file.</value>
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// The object holding and alowing the manipulation or, the public key and private key.
        /// </summary>
        public AsymmetricCipherKeyPair Keypair { get; set; }

        /// <summary>
        /// This is a easy to manipulate version of an X509 certificate, simple to convert to System.Security.Cryptography objects
        /// needs example
        /// </summary>
        public Org.BouncyCastle.X509.X509Certificate X509Certificate { get; set; }

        /// <summary>
        /// Since a Pkcs12 certificate can contain a chain of certificates and key associations, a Pkcs store is required to hold
        /// these in a manner that makes it easy to work with
        /// needs example
        /// </summary>
        public Pkcs12Store PkcsStore { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public PkcsCertificate() { }

        /// <summary>
        /// Public Constructor for a Pkcs certificate, takes in values to descibe the state of a certificate.
        /// The password of the certificte is set to an empty string.
        /// </summary>
        /// <param name="keypair">An object allowing the manipulation of the public and private key of a certificate</param>
        /// <param name="cert">An easy to manipulate version of the X509 certificate</param>
        /// <param name="store">A PkcsStore object representing certificate chains and key associations</param>
        public PkcsCertificate(AsymmetricCipherKeyPair keypair, Org.BouncyCastle.X509.X509Certificate cert, Pkcs12Store store)
        {
            this.Password = "";
            this.Keypair = keypair;
            this.X509Certificate = cert;
            this.PkcsStore = store;
            // this is not the best place to do this
        }

        /// <summary>
        /// Public constructor for a Pkcs certificate, takes in values to describe it's state.
        /// </summary>
        /// <param name="password">The value used to secure (encrypt) the private key with the certificate</param>
        /// <param name="keypair">An object used for manipulating/accessing the public and private key of a certificate</param>
        /// <param name="cert">An easy to manipulate version of an X509 certificate</param>
        /// <param name="store">A store of Pkcs12 certificates used to store and manipulate certificat chains and key associations</param>
        public PkcsCertificate(string password ,AsymmetricCipherKeyPair keypair, Org.BouncyCastle.X509.X509Certificate cert, Pkcs12Store store)
        {
            this.Password = password;
            this.Keypair = keypair;
            this.X509Certificate = cert;
            this.PkcsStore = store;
            // this is not the best place to do this
        }

        /// <summary>
        /// Method to return an X509 certificate entry to a pkcs certificate store. It's responcibility is easing the need to add 
        /// the certificate an instancce of this object to a Pkcs certificate store
        /// </summary>
        /// <returns>X509CertificateEntry object</returns>
        public X509CertificateEntry GetX509CertificateEntry()
        {
            return new X509CertificateEntry(this.X509Certificate);
        }

        /// <summary>
        /// Method to save the Pkcs store an instance of this class represents to a memory stream for writing to files.
        /// </summary>
        /// <returns>Memory stream representing the bytes that make up the X509certificate instance of this class</returns>
        public virtual MemoryStream GetCertificateStream()
        {
            MemoryStream returnStream = new MemoryStream();
            PkcsStore.Save(returnStream,this.Password.ToCharArray(),new Org.BouncyCastle.Security.SecureRandom());
            return returnStream;
        }

        /// <summary>
        /// This method creates an instance of the X509Certificate2 with the password provided to secure the private key
        /// provided the correct flags are set
        /// </summary>
        /// <param name="flags">Flags that denote values that can be given when creating a X509Certificate2 object</param>
        /// <returns>An X509Certificate2 object</returns>
        public virtual X509Certificate2 GetX509Certificate2(X509KeyStorageFlags flags)
        {
            return new X509Certificate2(GetCertificateStream().ToArray(), this.Password, flags);
        }

    }
}
