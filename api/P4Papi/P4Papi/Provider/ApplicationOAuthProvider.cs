using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using P4Papi.Models;
using P4Papi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Cors;

namespace P4Papi.Provider
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly string _publicClientId;

        public ApplicationOAuthProvider(string publicClientId)
        {
            if (publicClientId == null)
            {
                throw new ArgumentNullException("publicClientId");
            }

            _publicClientId = publicClientId;
        }

        public override async Task GrantResourceOwnerCredentials
        (OAuthGrantResourceOwnerCredentialsContext context)
        {
            LoginModel loginModel = RepositoryFactory.LoginUserRepository.LoginUser(context.UserName, context.Password);
            if (loginModel == null)
            {
                context.SetError("invalid_grant",
                  "The user name or password is incorrect.");
                return;
            }

            ClaimsIdentity oAuthIdentity =
            new ClaimsIdentity(context.Options.AuthenticationType);
            ClaimsIdentity cookiesIdentity =
            new ClaimsIdentity(context.Options.AuthenticationType);

            AuthenticationProperties properties = CreateProperties(loginModel, context.Options.AccessTokenExpireTimeSpan);
            AuthenticationTicket ticket =
            new AuthenticationTicket(oAuthIdentity, properties);
            context.Validated(ticket);
            context.Request.Context.Authentication.SignIn(cookiesIdentity);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string,
            string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientAuthentication
        (OAuthValidateClientAuthenticationContext context)
        {
            // Resource owner password credentials does not provide a client ID.
            if (context.ClientId == null)
            {
                context.Validated();
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientRedirectUri
        (OAuthValidateClientRedirectUriContext context)
        {
            if (context.ClientId == _publicClientId)
            {
                Uri expectedRootUri = new Uri(context.Request.Uri, "/");

                if (expectedRootUri.AbsoluteUri == context.RedirectUri)
                {
                    context.Validated();
                }
            }

            return Task.FromResult<object>(null);
        }

        public static AuthenticationProperties CreateProperties(LoginModel loginModel, TimeSpan expireTime)
        {
            DateTime current = DateTime.Now;
            
            IDictionary<string, string>
            data = new Dictionary<string, string>
            {
                { "Name", loginModel.Name },
                { "Id", loginModel.LoginId.ToString() },
                { "IsAdmin", loginModel.IsAdmin.ToString() },
                { "DepartmentList",string.Join(",",loginModel.DepartmentList)},
                { "ExpireTime",current.AddMinutes(expireTime.TotalMinutes).ToString("yyyy-MM-dd HH:mm:ss")}
            };
            return new AuthenticationProperties(data);
        }
    }
}