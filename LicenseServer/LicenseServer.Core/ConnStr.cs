namespace LicenseServer.Core
{
    public class ConnStr
    {

        public string ConnectionString { get; set; }
        public string Driver { get; set; }
        public string Host { get; set; }
        public string Port { get; set; }
        public string Catalog { get; set; }

        public override string ToString()
        {
            return string.Format("{0}://{1}:{2}/{3}", Driver, Host, Port, Catalog);
        }
    }
}