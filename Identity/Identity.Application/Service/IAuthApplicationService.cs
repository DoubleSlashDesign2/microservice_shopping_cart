﻿namespace Identity.Application.Service
{
    using Dto.Input;
    using Dto.Output;
    using System.Threading.Tasks;

    public interface IAuthApplicationService
    {
        Task<JwTokenDto> PerformAuthentication(LoginDto model);

        Task<JwTokenDto> PerformRegistration(RegisterDto model);
    }
}
