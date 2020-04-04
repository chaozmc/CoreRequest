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

        public void SaveTo(string Folder, string Name)
        {
            if (!System.IO.Directory.Exists(Folder))
            {
                System.IO.Directory.CreateDirectory(Folder);
            }

            if (System.IO.File.Exists(Folder + System.IO.Path.DirectorySeparatorChar + Name + ".json"))
            {
                    System.IO.File.Delete(Folder + System.IO.Path.DirectorySeparatorChar + Name + ".json");
            }

            System.IO.FileStream fileStream =
                new System.IO.FileStream(
                    Folder + System.IO.Path.DirectorySeparatorChar + Name + ".json",
                    System.IO.FileMode.CreateNew,
                    System.IO.FileAccess.Write);

            System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(fileStream);

            
            streamWriter.Write(System.Text.Json.JsonSerializer.Serialize(this));
            streamWriter.Close();

        }

    }

    public class DNSRequest : Request
    {

    }


    public class KeyTabRequest : Request
    {

    }

    public class DHCPRequest : Request
    {

    }
}
