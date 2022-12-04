using AutoMapper;
using CNF.Share.Domain.Repository;
using CNF.Share.Infrastructure.Caches;
using CNF.Share.Infrastructure.Common;
using CNF.Share.Infrastructure.Common.CryptHelper;
using CNF.Share.Model.Entity.Sys;
using CNF.Share.Models.Configs;
using CNF.Share.Models.Dtos.Input.Sys;
using CNF.Share.Models.Dtos.Output.Sys;
using Dm;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Logging;
using Org.BouncyCastle.Crypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNF.Share.Domain.Services.Sys
{
    public interface IUserService : IBaseServer<User>
    {
        Task<ApiResult<LoginOutput>> LoginAsync(LoginInput loginInput);
        Task<ApiResult> RegisterAsync(UserRegisterInput userRegisterInput);
        Task<ApiResult> ModfiyAsync(UserModifyInput userModifyInput);
        Task<ApiResult> ModfiyPwdAsync(ModifyPwdInput modifyPwdInput);
        Task<ApiResult> GetUserAsync(int id);

    }

    public partial class UserService : BaseServer<User>, IUserService
    {
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;
        private readonly ICurrentUserContext _currentUserContext;
        private readonly ICacheHelper _cacheHelper;

        public Task<ApiResult> GetUserAsync(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="loginInput"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ApiResult<LoginOutput>> LoginAsync(LoginInput loginInput)
        {
            loginInput.Password = Md5Crypt.Encrypt(loginInput.Password);
            var loginModel = await GetModelAsync(d => d.Name.Equals(loginInput.LoginName) && d.Password.Equals(loginInput.Password));
            if (loginModel?.Id == 0)
            {
                //new LogHelper().Process(loginModel?.Name, LogEnum.Login.GetEnumText(), $"{loginModel?.Name}登陆失败，用户名或密码错误！", LogLevel.Info);
                return new ApiResult<LoginOutput>("用户名或密码错误", 500);
            }
            if (!loginInput.ConfirmLogin)
            {
                if (loginModel.IsLogin)
                {
                    return new ApiResult<LoginOutput>($"该用户[{loginInput.LoginName}]已经登录，此时强行登录，其他地方会被挤下线！", 200);
                }
            }
            string ip = _accessor.HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault() ?? _accessor.HttpContext.Connection.RemoteIpAddress.ToString();
            string address = IpParseHelper.GetAddressByIP(ip);
            var lastLoginTime = DateTime.Now;
            await UpdateAsync(d => new User()
            {
                LastLoginTime = lastLoginTime,
                Ip = ip,
                Address = address,
                IsLogin = true
            }, d => d.Id == loginModel.Id);
            var data = _mapper.Map<LoginOutput>(loginModel);
            //new LogHelper().Process(loginModel.Name, LogEnum.Login.GetEnumText(), $"{loginModel.Name}登陆成功！", LogLevel.Info);
            WebHelper.SendEmail("神牛系统用户登录", $"当前时间为{DateTime.Now}：有名为{loginModel.Name}的用户在{address}成功登录神牛系统", loginModel?.Name, loginModel?.Email);
            return new ApiResult<LoginOutput>(data);
        }

        public Task<ApiResult> ModfiyAsync(UserModifyInput userModifyInput)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult> ModfiyPwdAsync(ModifyPwdInput modifyPwdInput)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult> RegisterAsync(UserRegisterInput userRegisterInput)
        {
            throw new NotImplementedException();
        }
    }
}
