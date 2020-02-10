using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Helpdesk.Cache;
using Helpdesk.Filters;
using Helpdesk.Model.Security;
using Helpdesk.Security.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Helpdesk.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ResponseFilter()]
    public class JwtController : ControllerBase
    {
        #region Properties and Member Variables
        private readonly ITokenProvider _tokenProvider;
        private readonly ICache serverCache;
        private bool IsAuthenticated { get; set; }

        //Todo: please move this method to a helper class and use in base class too.
        private string IPAddress
        {
            get
            {
                var connection = HttpContext.Connection;
                if (connection.RemoteIpAddress != null && connection.LocalIpAddress != null)
                {
                    return connection.RemoteIpAddress.Equals(connection.LocalIpAddress) ?
                        connection.LocalIpAddress.ToString() : connection.RemoteIpAddress.ToString();
                }
                return connection.LocalIpAddress.ToString();
            }
        }
        #endregion

        #region Constructor
        public JwtController(ITokenProvider tokenProvider, ICache cacheObject)
        {
            _tokenProvider = tokenProvider;
            serverCache = cacheObject;
        }
        #endregion

        #region Public Methods
        [HttpPost("token")]
        [AllowAnonymous]
        public async Task<IActionResult> GetToken([FromBody] IdentityUserModel identityUser)
        {
            identityUser.IPAddress = IPAddress;
            return await CreateTokenWithClaim(identityUser);
        }
        #endregion

        #region Private Methods 
        private async Task<IActionResult> CreateTokenWithClaim(IdentityUserModel identityUser)
        {
            TokenModel tokenModel = await _tokenProvider.ValidateAndCreateToken(identityUser);
            serverCache.ClearAll(identityUser.UserId);
            return GetTokenResponse(tokenModel);
        }

        private IActionResult GetTokenResponse(TokenModel tokenModel)
        {
            if (tokenModel == null || (tokenModel != null && !
                string.IsNullOrEmpty(tokenModel.ErrorMessage)))
            {
                return BadRequest(tokenModel.ErrorMessage);
            }

            // return the response
            var response = new
            {
                access_token = tokenModel.AccessToken,
                expires_in = (int)tokenModel.ExpiresIn
            };
            return new OkObjectResult(response);
        }

        #endregion
    }
}
