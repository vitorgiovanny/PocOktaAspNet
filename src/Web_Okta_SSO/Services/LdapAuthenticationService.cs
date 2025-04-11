using System.DirectoryServices.Protocols;
using System.Net;

namespace Web_Okta_SSO.Services
{
    public class LdapAuthenticationService
    {
        private readonly string _ldapServer = "localhost";
        private readonly int _ldapPort = 389;
        private readonly string _ldapBaseDn = "dc=minhaempresa,dc=local";

        public bool Authenticate(string username, string password)
        {
            try
            {
                using var connection = new LdapConnection(new LdapDirectoryIdentifier(_ldapServer, _ldapPort));
                var userDn = $"uid={username},{_ldapBaseDn}";
                connection.AuthType = AuthType.Basic;
                connection.Bind(new NetworkCredential(userDn, password));

                return true;
            }
            catch (LdapException)
            {
                return false;
            }
        }
    }
}
