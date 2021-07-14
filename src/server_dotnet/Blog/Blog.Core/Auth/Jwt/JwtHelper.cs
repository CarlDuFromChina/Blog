using Blog.Core.Config;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Auth
{
    public class JwtHelper
    {
        /// <summary>
        /// 创建AccessToken
        /// </summary>
        /// <param name="tokenModel"></param>
        /// <returns></returns>
        public static Token CreateAccessToken(JwtTokenModel tokenModel)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, tokenModel.Uid.ToString()),
                new Claim("Code", tokenModel.Code),
                new Claim("Name", tokenModel.Name),
           };

            // 可以将一个用户的多个角色全部赋予；
            claims.AddRange(tokenModel.Role.Split(',').Select(s => new Claim(ClaimTypes.Role, s)));

            return CreateToken(claims);
        }

        /// <summary>
        /// 创建Token（AccessToken、RefreshToken）
        /// </summary>
        /// <param name="tokenModel"></param>
        /// <returns></returns>
        public static ComplexToken CreateToken(JwtTokenModel tokenModel)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, tokenModel.Uid.ToString()),
                new Claim("Code", tokenModel.Code),
                new Claim("Name", tokenModel.Name)
           };

            // 可以将一个用户的多个角色全部赋予；
            claims.AddRange(tokenModel.Role.Split(',').Select(s => new Claim(ClaimTypes.Role, s)));

            return new ComplexToken() { AccessToken = CreateToken(claims), RefreshToken = CreateToken(claims, TokenType.RefreshToken) };
        }

        /// <summary>
        /// 颁发JWT字符串
        /// </summary>
        /// <param name="tokenModel"></param>
        /// <returns></returns>
        private static Token CreateToken(List<Claim> claims, TokenType tokenType = TokenType.AccessToken)
        {
            var now = DateTime.Now;
            var expires = tokenType == TokenType.AccessToken ? now.AddSeconds(JwtConfig.Config.ExpireSeconds) : now.AddHours(JwtConfig.Config.RefreshExpireHours);
            claims.Add(new Claim("type", tokenType.ToString()));

            string iss = JwtConfig.Config.Issuer;
            string aud = JwtConfig.Config.Audience;
            string secret = JwtConfig.Config.SecretKey;

            var jwt = new JwtSecurityToken(
                issuer: iss,
                claims: claims,
                audience: aud,
                expires: expires,
                notBefore: now,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)), SecurityAlgorithms.HmacSha256));

            var jwtHandler = new JwtSecurityTokenHandler();
            var encodedJwt = jwtHandler.WriteToken(jwt);

            return new Token()
            {
                TokenContent = encodedJwt,
                Expires = expires.ToLocalTime()
            };
        }

        /// <summary>
        /// 解析
        /// </summary>
        /// <param name="jwtStr"></param>
        /// <returns></returns>
        public static JwtTokenModel SerializeJwt(string jwtStr)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            if (!jwtHandler.CanReadToken(jwtStr))
            {
                return null;
            }
            string iss = JwtConfig.Config.Issuer;
            JwtSecurityToken jwtToken = jwtHandler.ReadJwtToken(jwtStr);
            if (jwtToken.Issuer != iss)
            {
                return null;
            }
            if (jwtToken.ValidTo.ToLocalTime() < DateTime.Now)
            {
                return null;
            }

            jwtToken.Payload.TryGetValue(ClaimTypes.Role, out var role);
            jwtToken.Payload.TryGetValue("Name", out var name);
            jwtToken.Payload.TryGetValue("Code", out var code);

            var tm = new JwtTokenModel
            {
                Uid = jwtToken.Id.ToString(),
                Role = role?.ToString(),
                Code = code?.ToString(),
                Name = name?.ToString()
            };

            return tm;
        }
    }

    public class Token
    {
        public string TokenContent { get; set; }
        public DateTime Expires { get; set; }
    }

    public class ComplexToken
    {
        public Token AccessToken { get; set; }
        public Token RefreshToken { get; set; }
    }

    public enum TokenType
    {
        AccessToken,
        RefreshToken
    }
}
