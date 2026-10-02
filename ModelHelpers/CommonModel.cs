using System.Data;

namespace RCMS.ModelHelpers
{
    public class CommonModel
    {
        public string? Code { get; set; }
        public string? Status { get; set; }

        public string? Msg { get; set; }

        public DataRow? DTRowDetails { get; set; }

        public DataTable? DTDetails { get; set; }
    }

    public class EncDecCModel
    {
        public Boolean Status { get; set; }
        public string? EncDecData { get; set; }

        public string? Msg { get; set; }

        public string? ErrorMsg { get; set; }
    }

    public class RegistrationModel
    {
        public string Type { get; set; }
        public string Input1 { get; set; }
        public string Input2 { get; set; }
        public string Input3 { get; set; }
        public string Input4 { get; set; }
        public string Input5 { get; set; } 
        public string? Input6 { get; set; }
    }

    public class SecureRequest
    {
        public string EncryptedKey { get; set; }
        public string IV { get; set; }
        public string CipherText { get; set; }
        public string Hmac { get; set; }
    }
}