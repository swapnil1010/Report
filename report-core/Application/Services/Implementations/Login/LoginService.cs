using AutoMapper;
using report_core.Application.Services.Interfaces;
using report_core.Domain.DTOs.Login;
using report_core.Domain.Entities.Common;
using report_core.Domain.Interfaces;

namespace report_core.Application.Services.Implementations.Login
{
    public class LoginService : ILoginService
    {
        public IUnitOfWork _unitOfWork;
        private readonly IMapper _iMapper;
        public LoginService(IUnitOfWork unitOfWork, IMapper iMapper)
        {
            _unitOfWork = unitOfWork;
            _iMapper = iMapper;
        }
        public async Task<Response> Login(Domain.Entities.Login.Login login)
        {
            Response response = new Response();
            try
            {
                var request = _iMapper.Map<LoginDTO>(login);
                var dbResponse = await _unitOfWork.TBL_USER.GetFirstOrDefaultAsync(u => u.USER_NAME.Trim().ToLower() == request.USER_NAME.Trim().ToLower()
                                                            && u.PASSWORD == request.PASSWORD);
                if (dbResponse == null) 
                {
                    response.ReturnCode = 400;
                    response.ReturnMsg = string.Empty;
                }
                else
                {
                    response.ReturnCode = 200;
                    response.Data = _iMapper.Map<Domain.Entities.Login.Login> (dbResponse);
                }
            }
            catch (Exception ex)
            {
                response.ReturnCode = 500;
                response.ReturnMsg = ex.Message;
            }
            return response;
        }
    }
}
