namespace LicenseServer.Core.Interfaces.BO
{
    public interface ILicenseBO
    {
        bool IsAppValid(string appKey);
    }
}
