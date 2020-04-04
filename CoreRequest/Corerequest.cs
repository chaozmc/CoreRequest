using System;

namespace CoreRequest
{
    public class Request
    {
        protected string requestorAccountName;
        protected string requestorSid;
        protected bool implemented;
        protected bool approved;
        protected string approvedByAccountName;
        protected string approvedByAccountSid;
        protected string implementedByAccountName;
        protected string implementedByAccountSid;
        protected string requestReference;

        public Request()
        {
            this.implementedByAccountName = "";
            this.implementedByAccountSid = "";
            this.requestorAccountName = "Nobody";
            this.requestorSid = "";
            this.implemented = false;
            this.approved = false;
            this.approvedByAccountName = "";
            this.approvedByAccountSid = "";
            this.requestReference = "";
        }

        public string RequestorAccountName 
        {
            get { return this.requestorAccountName; }
            set { this.requestorAccountName = value; }
        }

        public string RequestorSid
        {
            get { return this.requestorSid; }
            set { this.requestorSid = value; }
        }

        public bool Implemented
        {
            get { return this.implemented; }
            set { this.implemented = value; }
        }

        public string ImplementorName
        {
            get { return this.implementedByAccountName; }
            set { this.implementedByAccountName = value; }

        }

        public string ImplementorSid
        {
            get { return this.implementedByAccountSid; }
            set { this.implementedByAccountSid = value; }
        }

        public string ApproverName
        {
            get { return this.approvedByAccountName; }
            set { this.approvedByAccountName = value; }
        }

        public string ApproverSid
        {
            get { return this.implementedByAccountSid; }
            set { this.implementedByAccountSid = value; }
        }

        public bool Approved
        {
            get { return this.approved; }
            set { this.approved = value; }
        }

        public string RequestReference
        {
            get { return this.requestReference; }
            set { this.requestReference = value; }
        }

        public void Approve(string ApproverName)
        {
            this.approved = true;
            this.approvedByAccountName = ApproverName;
        }

        public void Implement(string ImplementorName)
        {
            this.implemented = true;
            this.implementedByAccountName = ImplementorName;
        }
    }

    public class DNSRequest : Request
    {
        public System.Collections.Generic.List<DNSRecord> Records;

        public DNSRequest()
        {
            this.Records = new System.Collections.Generic.List<DNSRecord>();
        }

        public void AddRecordToCollection(DNSRecord Record)
        {
            this.Records.Add(Record);
        }

        public void RemoveRecordFromCollection(DNSRecord Record)
        {
            this.Records.Remove(Record);
        }

        public DNSRecord[] AllDnsRecordItems()
        {
            return this.Records.ToArray();
        }

        public void SaveTo(string Folder)
        {
            if (this.requestReference.Length <= 2) { throw new Exception("No RequestReference set. Can't save"); }

            if (!System.IO.Directory.Exists(Folder))
            {
                System.IO.Directory.CreateDirectory(Folder);
            }

            if (System.IO.File.Exists(Folder + System.IO.Path.DirectorySeparatorChar + this.requestReference + ".xml"))
            {
                System.IO.File.Delete(Folder + System.IO.Path.DirectorySeparatorChar + this.requestReference + ".xml");
            }

            System.IO.FileStream fileStream =
                new System.IO.FileStream(
                    Folder + System.IO.Path.DirectorySeparatorChar + this.requestReference + ".xml",
                    System.IO.FileMode.CreateNew,
                    System.IO.FileAccess.Write);

            System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(fileStream);
            System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(DNSRequest));
            xmlSerializer.Serialize(streamWriter, this);
            streamWriter.Close();
        }

        public void SaveTo(string Folder, string FileName)
        {
            if (!FileName.EndsWith(".xml")) { _ = FileName + ".xml"; }

            if (!System.IO.Directory.Exists(Folder))
            {
                System.IO.Directory.CreateDirectory(Folder);
            }

            if (System.IO.File.Exists(Folder + System.IO.Path.DirectorySeparatorChar + FileName))
            {
                System.IO.File.Delete(Folder + System.IO.Path.DirectorySeparatorChar + FileName);
            }

            System.IO.FileStream fileStream =
                new System.IO.FileStream(
                    Folder + System.IO.Path.DirectorySeparatorChar + FileName,
                    System.IO.FileMode.CreateNew,
                    System.IO.FileAccess.Write);

            System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(fileStream);
            System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(DNSRequest));
            xmlSerializer.Serialize(streamWriter, this);
            streamWriter.Close();
        }

        public static DNSRequest Open(string Folder, string FileName)
        {

            if (!FileName.EndsWith(".xml")) { _ = FileName + ".xml"; }

            if (!System.IO.File.Exists(Folder + System.IO.Path.DirectorySeparatorChar + FileName))
            {
                throw new Exception("File does not exist. Can't open.");
            }

            System.IO.FileStream fileStream =
                new System.IO.FileStream(
                    Folder + System.IO.Path.DirectorySeparatorChar + FileName,
                    System.IO.FileMode.Open,
                    System.IO.FileAccess.Read);

            System.IO.StreamReader streamReader = new System.IO.StreamReader(fileStream);
            System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(DNSRequest));
            DNSRequest temp = new DNSRequest();
            temp =  (DNSRequest)xmlSerializer.Deserialize(streamReader);
            streamReader.Close();
            return temp;
        }
    }


    public class DNSRecord
    {
        public string RecordOperation { get; set; }
        public string RecordType { get; set; }
        public string RecordName { get; set; }
        public string RecordValue { get; set; }
        public string Zone { get; set; }

        private DNSRecord()
        {
            this.RecordOperation = "";
            this.RecordName = "";
            this.RecordType = "";
            this.RecordValue = "";
            this.Zone = "";
        }

        public DNSRecord (string RecordOperation, string RecordType, string RecordName, string RecordValue, string Zone)
        {
            this.RecordOperation = RecordOperation;
            this.RecordName = RecordName;
            this.RecordType = RecordType;
            this.RecordValue = RecordValue;
            this.Zone = Zone;
        }
    }


    public class KeyTabRequest : Request
    {
        public System.Collections.Generic.List<KeyTabConfig> KeyTabConfigs;
        
        public KeyTabRequest()
        {
            KeyTabConfigs = new System.Collections.Generic.List<KeyTabConfig>();
        }

        public void AddConfigToCollection(KeyTabConfig keyTabConfigItem)
        {
            this.KeyTabConfigs.Add(keyTabConfigItem);
        }

        public void RemoveConfigFromCollection(KeyTabConfig keyTabConfigItem)
        {
            this.KeyTabConfigs.Remove(keyTabConfigItem);
        }

        public KeyTabConfig[] AllKeyTabConfigItemsAsArray()
        {
            return this.KeyTabConfigs.ToArray();
        }

        public void SaveTo(string Folder)
        {
            if (this.requestReference.Length <= 2) { throw new Exception("No RequestReference set. Can't save"); }

            if (!System.IO.Directory.Exists(Folder))
            {
                System.IO.Directory.CreateDirectory(Folder);
            }

            if (System.IO.File.Exists(Folder + System.IO.Path.DirectorySeparatorChar + this.requestReference + ".xml"))
            {
                System.IO.File.Delete(Folder + System.IO.Path.DirectorySeparatorChar + this.requestReference + ".xml");
            }

            System.IO.FileStream fileStream =
                new System.IO.FileStream(
                    Folder + System.IO.Path.DirectorySeparatorChar + this.requestReference + ".xml",
                    System.IO.FileMode.CreateNew,
                    System.IO.FileAccess.Write);

            System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(fileStream);
            System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(KeyTabRequest));
            xmlSerializer.Serialize(streamWriter, this);
            streamWriter.Close();
        }

        public void SaveTo(string Folder, string FileName)
        {
            if (!FileName.EndsWith(".xml")) { _ = FileName + ".xml"; }

            if (!System.IO.Directory.Exists(Folder))
            {
                System.IO.Directory.CreateDirectory(Folder);
            }

            if (System.IO.File.Exists(Folder + System.IO.Path.DirectorySeparatorChar + FileName))
            {
                System.IO.File.Delete(Folder + System.IO.Path.DirectorySeparatorChar + FileName);
            }

            System.IO.FileStream fileStream =
                new System.IO.FileStream(
                    Folder + System.IO.Path.DirectorySeparatorChar + FileName,
                    System.IO.FileMode.CreateNew,
                    System.IO.FileAccess.Write);

            System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(fileStream);
            System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(KeyTabRequest));
            xmlSerializer.Serialize(streamWriter, this);
            streamWriter.Close();
        }

        public static KeyTabRequest Open(string Folder, string FileName)
        {

            if (!FileName.EndsWith(".xml")) { _ = FileName + ".xml"; }

            if (!System.IO.File.Exists(Folder + System.IO.Path.DirectorySeparatorChar + FileName))
            {
                throw new Exception("File does not exist. Can't open.");
            }

            System.IO.FileStream fileStream =
                new System.IO.FileStream(
                    Folder + System.IO.Path.DirectorySeparatorChar + FileName,
                    System.IO.FileMode.Open,
                    System.IO.FileAccess.Read);

            System.IO.StreamReader streamReader = new System.IO.StreamReader(fileStream);
            System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(KeyTabRequest));
            KeyTabRequest temp = new KeyTabRequest();
            temp = (KeyTabRequest)xmlSerializer.Deserialize(streamReader);
            streamReader.Close();
            return temp;
        }
    }

    public class KeyTabConfig
    {
        public bool UseServiceAccount { get; set; }
        public string AccountName { get; set; }
        public string EncryptionType { get; set; }
        public bool UseUpn { get; set; }
        public string Upn { get; set; }
        public bool UseSpn { get; set; }
        public string Spn { get; set; }
        public string ExportDirectory { get; set; }

        public KeyTabConfig(bool useServiceAccount, string accountName, 
            string encryptionType, bool useUpn, string upn, 
            bool useSpn, string spn, string exportDirectory)
        {
            this.UseServiceAccount = useServiceAccount;
            this.AccountName = accountName;
            this.EncryptionType = encryptionType;
            this.UseUpn = useUpn;
            this.Upn = upn;
            this.UseSpn = useSpn;
            this.Spn = spn;
            this.ExportDirectory = exportDirectory;
        }

        private KeyTabConfig()
        {
            this.UseServiceAccount = false;
            this.AccountName = "";
            this.EncryptionType = "";
            this.UseUpn = false;
            this.Upn = "";
            this.UseSpn = true;
            this.Spn = "";
            this.ExportDirectory = "";
        }

        public static class KeyTabEncryption
        {
            public static string Aes128Aes256 => "24";
            public static string Rc4Aes128 => "18";
            public static string Rc4Aes256 => "20";
            public static string Rc4Aes128Aes256 => "34";
            public static string Rc4 => "4";
            public static string Aes128 => "8";
            public static string Aes256 => "16";

        }
    }

    

    public class DHCPRequest : Request
    {

    }
}
