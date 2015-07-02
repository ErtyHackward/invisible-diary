using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using ProtoBuf;

namespace InvisibleDiary
{
    public class Diary : IDisposable
    {
        private RSACryptoServiceProvider _rsa;
        private FileStream _fileStream;
        private DiaryHeader _header;
        private int _headerLength;

        /// <summary>
        /// Indicates if we can read the records from the dictionary (if it is unlocked)
        /// </summary>
        public bool IsLocked { get; set; }

        /// <summary>
        /// Indicates if the dictionary file is open and public key is loaded
        /// </summary>
        public bool IsOpen { get; set; }

        public void OpenDiary(string filePath)
        {
            _fileStream = File.Open(filePath, FileMode.Open);

            _header = ReadHeader(_fileStream, out _headerLength);

            _rsa = new RSACryptoServiceProvider();
            _rsa.ImportCspBlob(_header.PublicKey);

            IsLocked = true;
            IsOpen = true;
        }

        private DiaryHeader ReadHeader(FileStream fs, out int headerLength)
        {
            var reader = new BinaryReader(fs);

            headerLength = reader.ReadInt32();
            var headerBytes = reader.ReadBytes(headerLength);

            if (headerBytes.Length != _headerLength)
            {
                throw new InvalidDataException();
            }

            var headerStream = new MemoryStream(headerBytes);

            return Serializer.Deserialize<DiaryHeader>(headerStream);
        }

        public void Unlock(string passphrase)
        {
            _rsa = EncryptionHelper.DecryptPrivateKey(_header.PrivateKeyEncrypted, passphrase);
            IsLocked = false;
        }

        public bool CanReadMoreRecords
        {
            get { return !IsLocked && _fileStream.Position > _headerLength + 4; }
        }

        public void RewindBack()
        {
            if (!IsOpen || IsLocked)
                throw new InvalidOperationException("You need to open and unlock the diary before trying to read it");

            _fileStream.Seek(0, SeekOrigin.End);
        }

        public DiaryRecord ReadPrevious()
        {
            var reader = new BinaryReader(_fileStream);

            _fileStream.Seek(-4, SeekOrigin.Current);
            var messageLength = reader.ReadInt32();
            _fileStream.Seek(-(messageLength + 4), SeekOrigin.Current);
            var recordBytes = reader.ReadBytes(messageLength);

            if (recordBytes.Length != messageLength)
                throw new InvalidDataException("The record could not be read");

            var encryptedDiaryRecord = new EncryptedDiaryRecord();

            var headerLength = _rsa.KeySize / 8;

            encryptedDiaryRecord.Header = new byte[headerLength];
            encryptedDiaryRecord.Body = new byte[recordBytes.Length - headerLength];

            Buffer.BlockCopy(recordBytes, 0, encryptedDiaryRecord.Header, 0, headerLength);
            Buffer.BlockCopy(recordBytes, headerLength, encryptedDiaryRecord.Body, 0, encryptedDiaryRecord.Body.Length);

            var record = EncryptionHelper.DecryptEntry(encryptedDiaryRecord, _rsa);
            _fileStream.Seek(-(messageLength + 4), SeekOrigin.Current);

            return record;
        }

        public void CreateNew(string fileName, string passphrase)
        {
            _rsa = new RSACryptoServiceProvider(2048);
            
            if (File.Exists(fileName))
                File.Delete(fileName);

            if (_fileStream != null)
                _fileStream.Dispose();

            _fileStream = File.Open(fileName, FileMode.CreateNew);

            _header = EncryptionHelper.CreateHeader(_rsa, passphrase);

            var memoryStream = new MemoryStream();
            Serializer.Serialize(memoryStream, _header);

            var headerBytes = memoryStream.ToArray();
            var writer = new BinaryWriter(_fileStream);

            writer.Write(headerBytes.Length);
            writer.Write(headerBytes);
            writer.Flush();

            IsOpen = true;
            IsLocked = false;
        }

        public void AddRecord(DiaryRecord record)
        {
            if (!IsOpen)
                throw new InvalidOperationException("Please open the diary before writing to it");

            _fileStream.Seek(0, SeekOrigin.End);

            WriteRecords(new[] { record });
        }

        private void WriteRecords(IEnumerable<DiaryRecord> records)
        {
            var writer = new BinaryWriter(_fileStream);
            foreach (var diaryRecord in records)
            {
                var encryptedRecord = EncryptionHelper.EncryptRecord(diaryRecord, _rsa);
                
                var length = encryptedRecord.Body.Length + encryptedRecord.Header.Length;
                writer.Write(length);
                writer.Write(encryptedRecord.Header);
                writer.Write(encryptedRecord.Body);
                writer.Write(length);
            }
            writer.Flush();
        }

        public List<DiaryRecord> ReadAllRecords()
        {
            if (!IsOpen)
                throw new InvalidOperationException("Please open the diary before reading it");

            RewindBack();

            // read all our records
            var records = new List<DiaryRecord>();
            while (CanReadMoreRecords)
            {
                records.Add(ReadPrevious());
            }

            records.Reverse();

            return records;
        }

        public void Merge(string otherFilePath)
        {

            var ourRecords = ReadAllRecords();
            
            // read all other records
            var otherDirary = new Diary();

            otherDirary.OpenDiary(otherFilePath);
            
            if (!_header.Equals(otherDirary._header))
                throw new ApplicationException("Different headers, cannot merge");


            otherDirary._rsa = _rsa;
            otherDirary.IsLocked = false;

            var otherRecords = otherDirary.ReadAllRecords();

            var merged = ourRecords.Concat(otherRecords).Distinct().OrderBy(r => r.Created);

            _fileStream.Seek(_headerLength, SeekOrigin.Begin);

            WriteRecords(merged);

            _fileStream.SetLength(_fileStream.Position);

        }

        public void Dispose()
        {
            if (_fileStream != null)
            {
                _fileStream.Dispose();
                _fileStream = null;
            }
        }
    }
}
