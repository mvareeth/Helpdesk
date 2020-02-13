using Helpdesk.Management.Security;
using Helpdesk.Model.Security;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Helpdesk.Security.Token
{
    public class TokenProvider : ITokenProvider
    {
        #region Properties and Member Variables
        private readonly TokenProviderOptions _jwtOptions;
        private readonly ISecurityReadManager _securityReadManagement;

        private string ErrorMessage { get; set; }
        private string IPAddress { get; set; }
        #endregion

        public TokenProvider(IOptions<TokenProviderOptions> jwtOptions,
            ISecurityReadManager securityReadManagement)
        {
            _jwtOptions = jwtOptions.Value;
            _securityReadManagement = securityReadManagement;
            ThrowIfInvalidOptions(_jwtOptions);
        }

        #region Public Methods 
        /// <summary>
        /// validate the credential and create token
        /// </summary>
        /// <param name="identityUser"></param>
        /// <returns></returns>
        public async Task<TokenModel> ValidateAndCreateToken(IdentityUserModel identityUser)
        {
            IPAddress = identityUser.IPAddress;
            if (!ValidateCredentils(identityUser))
            {
                return new TokenModel { ErrorMessage = ErrorMessage };
            }

            var identity = await GetClaimsIdentity(identityUser);
            if (identity == null)
            {
                return new TokenModel { ErrorMessage = ErrorMessage };
            }

            return await CreateTokenWithClaim(identityUser, identity);
        }
        /// <summary>
        /// create claim using identity model
        /// </summary>
        /// <param name="identityUser"></param>
        /// <param name="identity"></param>
        /// <returns></returns>
        public async Task<TokenModel> CreateTokenWithClaim(IdentityUserModel identityUser, ClaimsIdentity identity)
        {
            // Create claims
            List<Claim> claims = await CreateClaim(identityUser, identity);

            // Create the JWT security token and encode it.
            var jwt = CreateToken(claims);
            // encode the token
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            // return the response
            var tokenResponse = new TokenModel
            {
                AccessToken = encodedJwt,
                ExpiresIn = (int)_jwtOptions.ValidFor.TotalSeconds
            };
            return tokenResponse;
        }
        #endregion

        #region Private Methods 
        /// <summary>
        /// validate credential using identity model
        /// </summary>
        /// <param name="identityUser"></param>
        /// <returns></returns>
        private bool ValidateCredentils(IdentityUserModel identityUser)
        {
            if (identityUser == null)
            {
                this.ErrorMessage = "You are not authorized to use the application";
                return false;
            }
            return ValidateCredentils(identityUser.UserName, identityUser.Password);
        }
        /// <summary>
        /// validate credential using username and password
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private bool ValidateCredentils(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                this.ErrorMessage = "The user name or password provided is incorrect";
                return false;
            }
            return true;
        }
        /// <summary>
        /// create claim using identity
        /// </summary>
        /// <param name="identityUser"></param>
        /// <param name="identity"></param>
        /// <returns></returns>
        private async Task<List<Claim>> CreateClaim(IdentityUserModel identityUser, ClaimsIdentity identity)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, identityUser.UserName),
                new Claim("clientId", GenerateClientId(IPAddress)),
                new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()),
                new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(),
                    ClaimValueTypes.Integer64)
            };

           claims.Add(new Claim(JwtRegisteredClaimNames.Sub, identityUser.UserId.ToString()));

            return claims;
        }
        /// <summary>
        /// create token using claim
        /// </summary>
        /// <param name="claims"></param>
        /// <returns></returns>
        private JwtSecurityToken CreateToken(List<Claim> claims)
        {
            // Create the JWT security token and encode it.
            var jwt = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                notBefore: _jwtOptions.NotBefore,
                expires: _jwtOptions.Expiration,
                signingCredentials: _jwtOptions.SigningCredentials);
            return jwt;
        }
        /// <summary>
        /// throw error based on validations
        /// </summary>
        /// <param name="options"></param>
        private void ThrowIfInvalidOptions(TokenProviderOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            if (options.ValidFor <= TimeSpan.Zero)
            {
                throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(TokenProviderOptions.ValidFor));
            }

            if (options.SigningCredentials == null)
            {
                throw new ArgumentNullException(nameof(TokenProviderOptions.SigningCredentials));
            }

            if (options.JtiGenerator == null)
            {
                throw new ArgumentNullException(nameof(TokenProviderOptions.JtiGenerator));
            }
        }

        /// <returns>Date converted to seconds since Unix epoch (Jan 1, 1970, midnight UTC).</returns>
        private static long ToUnixEpochDate(DateTime date)
          => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

        /// <summary>
        /// Validate the user whether it is valid or not and build the claims based on that.
        /// </summary>
        private Task<ClaimsIdentity> GetClaimsIdentity(IdentityUserModel identityUser)
        {
 
            string userName = identityUser.UserName;
            string password = identityUser.Password;

            identityUser.UserId = (_securityReadManagement.ValidateUserCredential(userName, password) ?? 0);
            if (identityUser.UserId > 0)  
            {
                // get roles to create the claim
                return Task.FromResult(new ClaimsIdentity(new GenericIdentity(identityUser.UserName, "Token"),
                new Claim[] { }));
            }

            // Credentials are invalid, or account doesn't exist or there is no permission
            this.ErrorMessage = "You are not authorized to use the application";
            return Task.FromResult<ClaimsIdentity>(null);
        }
        /// <summary>
        /// add list of roles as part of token for permission checking and authorization
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="portalContext"></param>
        /// <returns></returns>
        private IEnumerable<Claim> AddRoles(ClaimsIdentity identity, string portalContext)
        {
            return identity.FindAll(ClaimTypes.Role);
        }

        /// <summary>
        /// Usually this will come from the provider. Here we are handling the authentication and generate the client id
        /// </summary>
        /// <param name="remoteIpAddress"></param>
        /// <returns></returns>
        private string GenerateClientId(string remoteIpAddress)
        {
            string clientId = "zIUcWDiIPttjYLRgBl0pn9sOY5Xt5nhL";
            string ipAddress = remoteIpAddress;
            return clientId;
        }
        #endregion

    }
}
