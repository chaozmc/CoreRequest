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

        //public void SaveTo(string Folder, string Name)
        //{
        //    if (!System.IO.Directory.Exists(Folder))
        //    {
        //        System.IO.Directory.CreateDirectory(Folder);
        //    }

        //    if (System.IO.File.Exists(Folder + System.IO.Path.DirectorySeparatorChar + Name + ".json"))
        //    {
        //            System.IO.File.Delete(Folder + System.IO.Path.DirectorySeparatorChar + Name + ".json");
        //    }

        //    System.IO.FileStream fileStream =
        //        new System.IO.FileStream(
        //            Folder + System.IO.Path.DirectorySeparatorChar + Name + ".json",
        //            System.IO.FileMode.CreateNew,
        //            System.IO.FileAccess.Write);

        //    System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(fileStream);

            
        //    streamWriter.Write(System.Text.Json.JsonSerializer.Serialize(this));
        //    streamWriter.Close();

        //}

    }

    public class DNSRequest : Request
    {
        public System.Collections.Generic.List<DNSRecord> Records;
        public string Action;

        public DNSRequest(string Action, System.Collections.Generic.List<DNSRecord> Records)
        {
            this.Records = Records;
            this.Action = Action;
        }

        public DNSRequest(string Action)
        {
            this.Action = Action;
            this.Records = new System.Collections.Generic.List<DNSRecord>();
        }

        private DNSRequest()
        {
            this.Action = "";
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
        private string recordType;
        private string recordName;
        private string recordValue;
        private string zone;

        public DNSRecord (string RecordType, string RecordName, string RecordValue, string Zone)
        {
            this.recordName = RecordName;
            this.recordType = RecordType;
            this.recordValue = RecordValue;
            this.zone = Zone;
        }

        public string RecordType
        {
            get { return this.recordType; }
            set { this.recordType = value; }
        }

        public string RecordName
        {
            get { return this.recordName; }
            set { this.recordName = value; }
        }

        public string RecordValue
        {
            get { return this.recordValue; }
            set { this.recordValue = value; }
        }

        public string Zone
        {
            get { return this.zone; }
            set { this.zone = value; }
        }

        private DNSRecord()
        {
            this.recordName = "";
            this.recordType = "";
            this.recordValue = "";
            this.zone = "";
        }
    }


    public class KeyTabRequest : Request
    {

    }

    public class DHCPRequest : Request
    {

    }
}
