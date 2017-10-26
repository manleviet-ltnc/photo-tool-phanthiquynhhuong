using System;
using System.IO;
using System.Text;



namespace Manning.MyPhotoAlbum
{
    class CryptoReader : StreamReader
    {
        private CryptoTextBase _base;
        private CryptoTextBase cryptoBase
        {
            get { return _base; }
        }
        public CryptoReader(string path,string password): base(path)
        {
            if (path == null || path.Length == 0)
                throw new ArgumentNullException("path");
            if (password == null || path.Length == 0)
                throw new ArgumentNullException("password");

            _base = new CryptoTextBase(password);
        }
        public override string ReadLine()
        {
            string encrypted = base.ReadLine();
            if (encrypted == null || encrypted.Length == 0)
                return encrypted;
            else
                return cryptoBase.ProcessText(encrypted, false);
         
        }
        public string ReadUnencryptedLine()
        {
            return base.ReadLine();
        }
    }
}
