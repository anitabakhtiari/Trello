﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands;
using Application.Queries;
using Infra.Extentions;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Trello.Controllers
{

    public class UsersController : BaseController
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }
              

        [HttpPost]
        public async Task<ActionResult<string>> Register([FromBody] RegisterCommand register)
        {

            return await _mediator.Send(register);
        }


        [HttpPost]
        public async Task<ActionResult<string>> Login([FromBody] LoginQuery query)
        {
            return await _mediator.Send(query);
        }


   

        [HttpPost]
        public async Task<ActionResult<string>> RegisterAdmin([FromBody] RegisterAdminCommand register)
        {

            return await _mediator.Send(register);
        }





    }
}
