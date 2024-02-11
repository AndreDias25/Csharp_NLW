using RocketseatAuction.API.Contracts;
using RocketseatAuction.API.Entities;

namespace RocketseatAuction.API.Services
{
    public class LoggedUser : ILoggedUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _repository;
        public LoggedUser(IHttpContextAccessor httpContext, IUserRepository repository) 
        {
            _httpContextAccessor = httpContext;
            _repository = repository;

        }
        public User User()
        {

            var token = TokenOnRequest();
            var email = FromBase64String(token);

            return _repository.GetUserByEmail(email);
        }

        private string TokenOnRequest()
        {
            var authentication = _httpContextAccessor.HttpContext!.Request.Headers.Authorization.ToString();
            //"Bearer iojioefngsdngiosngisd" vem assim o token


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
