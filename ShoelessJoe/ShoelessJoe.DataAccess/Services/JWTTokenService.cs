using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ShoelessJoe.Core.CoreModels;
using ShoelessJoe.Core.Interfaces;
using ShoelessJoe.DataAccess.AppSettings;
using ShoelessJoe.DataAccess.DataModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ShoelessJoe.DataAccess.Services
{
    public class JWTTokenService : ServiceHelper, IJWTService
    {
        private readonly JWTAppSettings _jwtAppSettings;
        private readonly UserAppSettings _userAppSettings;

        #region Constructors
        public JWTTokenService(ShoelessJoeContext context, IConfiguration configuration) : base(context, configuration)
        {
            _jwtAppSettings = new JWTAppSettings(_configuration);
            _userAppSettings = new UserAppSettings(_configuration);
        }
        #endregion

        #region Public Proprties
        public string RefreshTokenNotFoundMessage => _jwtAppSettings.RefreshTokenNotFoundMessage;

        public string RefreshTokenExpiredMessage => _jwtAppSettings.RefreshTokenExpiredMessage;
        #endregion

        #region Public Methods
        public string CreateToken(IEnumerable<Claim> claims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtAppSettings.JWTKey));

            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescripton = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),//Add jwt,
                SigningCredentials = signIn
            };

            var token = tokenHandler.CreateToken(tokenDescripton);

            return tokenHandler.WriteToken(token);
        }

        public List<Claim> GetClaims(CoreUser coreUser)
        {
            return new List<Claim>
            {
                new Claim(_userAppSettings.UserId, coreUser.UserId.ToString()),
                new Claim(_userAppSettings.Email, coreUser.Email),
                new Claim(_userAppSettings.PhoneNumb, coreUser.PhoneNumb),
                new Claim(_userAppSettings.FirstName, coreUser.FirstName),
                new Claim(_userAppSettings.LastName, coreUser.LastName)
            };
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtAppSettings.JWTKey)),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            //if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature, StringComparison.InvariantCultureIgnoreCase))
            //    throw new SecurityTokenException("Invalid token");

            return principal;
        }
        #endregion
    }
}
