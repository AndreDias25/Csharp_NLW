using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RocketseatAuction.API.Contracts;
using RocketseatAuction.API.Repositories;

namespace RocketseatAuction.API.Filters
{
    public class AuthenticationUserAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private IUserRepository _repository;

        public AuthenticationUserAttribute(IUserRepository repository) => _repository = repository;
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                var token = TokenOnRequest(context.HttpContext);

               

                var email = FromBase64String(token);

                var exist = _repository.ExistUserWithEmail(email);

                if (exist == false)
                {
                    context.Result = new UnauthorizedObjectResult("E-mail not valid!");
                }
            }
            catch (Exception ex)
            {
                context.Result = new UnauthorizedObjectResult(ex.Message);
            }
        }

        private string TokenOnRequest(HttpContext context)
        {
            var authentication = context.Request.Headers.Authorization.ToString();
            //"Bearer iojioefngsdngiosngisd" vem assim o token

            if (string.IsNullOrEmpty(authentication))
            {
                throw new Exception("Token is missing");
            }

            return authentication["Bearer ".Length..]; //retorna para mim uma string a partir da ultima posicação a direta(no caso
                                                       //o espaço em branco após o bearer)
        }

        private string FromBase64String(string base64)
        {
            var data = Convert.FromBase64String(base64);

            return System.Text.Encoding.UTF8.GetString(data);
        }
    }
}
