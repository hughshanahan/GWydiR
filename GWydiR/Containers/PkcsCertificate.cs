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
    public class PkcsCertificate
    {
        public string Password { get; set; }
        public AsymmetricCipherKeyPair Keypair { get; set; }
        public Org.BouncyCastle.X509.X509Certificate X509Certificate { get; set; }
        public Pkcs12Store PkcsStore { get; set; }

        public PkcsCertificate(AsymmetricCipherKeyPair keypair, Org.BouncyCastle.X509.X509Certificate cert, Pkcs12Store store)
        {
            this.Password = "";
            this.Keypair = keypair;
            this.X509Certificate = cert;
            this.PkcsStore = store;
            // this is not the best place to do this
        }

        public PkcsCertificate(string password ,AsymmetricCipherKeyPair keypair, Org.BouncyCastle.X509.X509Certificate cert, Pkcs12Store store)
        {
            this.Password = password;
            this.Keypair = keypair;
            this.X509Certificate = cert;
            this.PkcsStore = store;
            // this is not the best place to do this
        }

        public X509CertificateEntry GetX509CertificateEntry()
        {
            return new X509CertificateEntry(this.X509Certificate);
        }

        public MemoryStream GetCertificateStream()
        {
            MemoryStream returnStream = new MemoryStream();
            PkcsStore.Save(returnStream,this.Password.ToCharArray(),new Org.BouncyCastle.Security.SecureRandom());
            return returnStream;
        }

        public X509Certificate2 GetX509Certificate2(X509KeyStorageFlags flags)
        {
            return new X509Certificate2(GetCertificateStream().ToArray(), this.Password, flags);
        }

    }
}
