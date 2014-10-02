using System;
using System.IO;
using System.Security.Cryptography;
using ProtoBuf;

namespace InvisibleDiary
{
    public static class EncryptionHelper
    {
        public static byte[] GetKey(string passphrase)
        {
            var salt = new byte[] { 4, 132, 84, 19, 231, 32, 49, 74, 211, 2, 62 };

            using (var derive = new Rfc2898DeriveBytes(passphrase, salt, 50000))
                return derive.GetBytes(32);
        }

        public static byte[] GetIV(string passphrase)
        {
            var salt = new byte[] { 41, 12, 182, 20, 21, 122, 49, 172, 115, 5, 30 };

            using (var derive = new Rfc2898DeriveBytes(passphrase, salt, 50000))
                return derive.GetBytes(16);
        }

        public static RSACryptoServiceProvider DecryptPrivateKey(byte[] bytes, string passphrase)
        {
            var rij = Rijndael.Create();
            var rsa = new RSACryptoServiceProvider();

            using (var cs = new CryptoStream(new MemoryStream(bytes), rij.CreateDecryptor(GetKey(passphrase),GetIV(passphrase)), CryptoStreamMode.Read))
            {
                rsa.ImportCspBlob(ReadFully(cs));    
            }
            
            return rsa;
        }

        public static DiaryRecord DecryptEntry(EncryptedDiaryRecord record, RSACryptoServiceProvider rsa)
        {
            var decryptedChunk = rsa.Decrypt(record.Header, false);

            var key = new byte[32];
            var iv = new byte[16];

            var rij = Rijndael.Create();

            Buffer.BlockCopy(decryptedChunk,  0, key, 0, 32);
            Buffer.BlockCopy(decryptedChunk, 32,  iv, 0, 16);

            using (var cs = new CryptoStream(new MemoryStream(record.Body), rij.CreateDecryptor(key, iv), CryptoStreamMode.Read))
            {
                return Serializer.Deserialize<DiaryRecord>(cs);
            }
        }

        public static EncryptedDiaryRecord EncryptRecord(DiaryRecord record, RSACryptoServiceProvider rsa)
        {
            var rij = Rijndael.Create();
            rij.KeySize = 256;
            rij.GenerateKey();
            rij.GenerateIV();

            var encryptedRecord = new EncryptedDiaryRecord();

            var header = new MemoryStream();

            header.Write(rij.Key, 0, rij.Key.Length);
            header.Write(rij.IV,  0, rij.IV.Length);
            
            encryptedRecord.Header = rsa.Encrypt(header.ToArray(), false);
            
            var encryptedBodyStream = new MemoryStream();

            using (var body = new CryptoStream(encryptedBodyStream, rij.CreateEncryptor(), CryptoStreamMode.Write))
            {
                Serializer.Serialize(body, record);
            }
            
            encryptedRecord.Body = encryptedBodyStream.ToArray();

            return encryptedRecord;
        }

        private static byte[] ReadFully(Stream input)
        {
            var buffer = new byte[4 * 1024];
            using (var ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }


        public static DiaryHeader CreateHeader(RSACryptoServiceProvider rsa, string passphrase)
        {
            var header = new DiaryHeader();

            header.PublicKey = rsa.ExportCspBlob(false);
            var privateBlob = rsa.ExportCspBlob(true);

            var rij = Rijndael.Create();
            
            var encryptedStream = new MemoryStream();

            using (var cs = new CryptoStream(encryptedStream, rij.CreateEncryptor(GetKey(passphrase), GetIV(passphrase)), CryptoStreamMode.Write))
            {
                cs.Write(privateBlob, 0, privateBlob.Length);
            }

            header.PrivateKeyEncrypted = encryptedStream.ToArray();
            
            return header;
        }
    }
}
