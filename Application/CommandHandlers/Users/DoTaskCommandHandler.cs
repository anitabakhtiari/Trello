﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands;
using Infra.Extentions;
using Infra.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Trello.Hubs;

namespace Application.CommandHandlers
{
    class DoTaskCommandHandler : IRequestHandler<DoTaskCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _accessor;
        private readonly NotificationHub _hub;

        public DoTaskCommandHandler(IUnitOfWork unitOfWork, IHttpContextAccessor accessor, NotificationHub hub)
        {
            _accessor = accessor;
            _unitOfWork = unitOfWork;
            _hub = hub;
        }

        public async Task<int> Handle(DoTaskCommand request, CancellationToken cancellationToken)
        {
            var waiting=  await _unitOfWork.UserTaskRepository.DoTask(request.Id, _accessor.GetUserId());

            //if (waiting.AsyncState == null)
            //{
            //    return 404;
            //}

          
                await _unitOfWork.SaveChangeAsync();

           // var TaskList = await _unitOfWork.UserTaskRepository.GetListTasks();
          //  var AdminId = TaskList.SingleOrDefault(a => a.Id == waiting.Id);
            
           await _hub.SendMessage(waiting, $"");

            return request.Id;
        }
    }
}
