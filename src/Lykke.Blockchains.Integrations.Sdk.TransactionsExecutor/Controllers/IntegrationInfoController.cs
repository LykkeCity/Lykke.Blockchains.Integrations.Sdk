﻿using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Lykke.Blockchains.Integrations.Contract.TransactionsExecutor.Responses;
using Lykke.Blockchains.Integrations.Sdk.TransactionsExecutor.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lykke.Blockchains.Integrations.Sdk.TransactionsExecutor.Controllers
{
    [ApiController]
    [Route("/api/integration-info")]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    internal class IntegrationInfoController : ControllerBase
    {
        private readonly IIntegrationInfoService _integrationInfoService;

        public IntegrationInfoController(IIntegrationInfoService integrationInfoService)
        {
            _integrationInfoService = integrationInfoService;
        }

        [HttpGet]
        public async Task<ActionResult<IntegrationInfoResponse>> GetInfo()
        {
            var info = await _integrationInfoService.GetInfoAsync();
            if (info == null)
            {
                throw new InvalidOperationException("Not null response object expected");
            }

            var response = new IntegrationInfoResponse
            (
                info.Blockchain,
                info.Dependencies
            );

            return Ok(response);
        }
    }
}
