using CNF.Share.Domain.Services.Sys;
using CNF.Share.Infrastructure.Attributes;
using CNF.Share.Infrastructure.Caches;
using CNF.Share.Infrastructure.Common.CryptHelper;
using CNF.Share.Infrastructure.JsonWebToken;
using CNF.Share.Models.Configs;
using CNF.Share.Models.Dtos.Input.Sys;
using CNF.Share.Models.Dtos.Output.Sys;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Org.BouncyCastle.Crypto.Generators;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CNF.Admin.API.Controllers.Sys
{
    /// <summary>
    /// /用户控制器
    /// </summary>
    public class UserController : ApiControllerBase
    {
        readonly IOptions<JwtSetting> _jwtSetting;
        private readonly IUserService _userService;
        private readonly IR_User_RoleService _r_User_RoleService;
        //private readonly IMenuService _menuService;
        private readonly ICacheHelper _cacheHelper;
        private readonly ICurrentUserContext _currentUserContext;
        //private readonly IHubContext<ChatHub> _hubContext;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="jwtSetting"></param>
        /// <param name="userService"></param>
        /// <param name="r_User_RoleService"></param>
        /// <param name="menuService"></param>
        /// <param name="cacheHelper"></param>
        /// <param name="currentUserContext"></param>
        /// <param name="hubContext"></param>
        public UserController(IOptions<JwtSetting> jwtSetting, IUserService userService, IR_User_RoleService r_User_RoleService, /*IMenuService menuService,*/ ICacheHelper cacheHelper, ICurrentUserContext currentUserContext/*, IHubContext<ChatHub> hubContext*/)
        {
            _jwtSetting = jwtSetting;
            _userService = userService;
            _r_User_RoleService = r_User_RoleService;
            //_menuService = menuService;
            _cacheHelper = cacheHelper;
            _currentUserContext = currentUserContext;
            //_hubContext = hubContext;
        }


        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="userRegisterInput"></param>
        /// <returns></returns>
        [HttpPost, Authority(Module = nameof(User), Method = nameof(Button.Add))]
        public async Task<ApiResult> Register([FromBody] UserRegisterInput userRegisterInput)
        {
            return await _userService.RegisterAsync(userRegisterInput);
        }

        [HttpGet]
        public async Task<ApiResult> GetUser(int id)
        {
            return await _userService.GetUserAsync(id);
        }


        /// <summary>
        ///用户前后端分离的登录
        /// </summary>
        /// <returns></returns>
        [HttpPost("sign")]
        [AllowAnonymous]
        public async Task<ApiResult<LoginOutput>> SignIn([FromBody] LoginInput loginInput)
        {
            var rsaKey = _cacheHelper.Get<List<string>>(KeyHelper.User.loginRSACrypt + loginInput.NumberGuid);
            if (rsaKey == null)
            {
                return new ApiResult<LoginOutput>("登录失败，请刷新浏览器再次登录!");
            }
            //Ras解密密码
            var ras = new RSACrypt(rsaKey[0], rsaKey[1]);
            loginInput.Password = ras.Decrypt(loginInput.Password);
            var result = await _userService.LoginAsync(loginInput);
            var token = GetJwtToken(result.Data);
            if (string.IsNullOrEmpty(token))
            {
                return new ApiResult<LoginOutput>("生成的token字符串为空!");
            }
            result.Data.Token = token;
            return result;
        }

        private string GetJwtToken(LoginOutput loginOutput)
        {
            //如果是基于用户的授权策略，这里要添加用户;如果是基于角色的授权策略，这里要添加角色
            var claims = new List<Claim>
            {
                    new Claim(ClaimTypes.Name, loginOutput.LoginName),
                    new Claim(JwtRegisteredClaimNames.Sid, loginOutput.Id.ToString()),
                    new Claim(ClaimTypes.Expiration, DateTime.Now.AddSeconds(_jwtSetting.Value.ExpireSeconds).ToString(CultureInfo.InvariantCulture)),
                    new Claim(ClaimTypes.Role,"Type"),
                    new Claim("mobile",loginOutput.Mobile)
            };
            var token = JwtHelper.BuildJwtToken(claims.ToArray(), _jwtSetting);
            return token;
        }
    }
}
