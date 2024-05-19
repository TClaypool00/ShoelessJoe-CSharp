using ShoelessJoe.Core.CoreModels;
using System.Security.Claims;

namespace ShoelessJoe.Core.Interfaces
{
    public interface IJWTService
    {
        #region Public Properites
        public string RefreshTokenNotFoundMessage { get; }
        public string RefreshTokenExpiredMessage { get; }
        #endregion

        #region Public Methods
        public string CreateToken(IEnumerable<Claim> claims);

        public List<Claim> GetClaims(CoreUser coreUser);

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        #endregion
    }
}
