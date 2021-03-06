﻿using System;
using Application.Common.Interfaces;
using Domain;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.Queries
{
    /// <summary>
    /// Query for retrieving a job by its ID using MediatR's IRequest
    /// </summary>
    public class GetJobByIdQuery : IRequest<Job>
    {
        public Guid Id { get; }
        
        /// <summary>
        /// Query for retrieving a job by its ID using MediatR's IRequest
        /// </summary>
        /// <param name="id">ID of the requested Job</param>
        public GetJobByIdQuery(Guid id)
        {
            Id = id;
        }

        public class GetJobByIdQueryHandler : IRequestHandler<GetJobByIdQuery, Job>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly ILogger<GetAllJobsQuery.GetAllJobsQueryHandler> _logger;

            public GetJobByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetAllJobsQuery.GetAllJobsQueryHandler> logger)
            {
                _unitOfWork = unitOfWork;
                _logger = logger;
            }

            public async Task<Job> Handle(GetJobByIdQuery request, CancellationToken cancellationToken)
            {
                Job job = await _unitOfWork.Jobs.GetAsync(request.Id);
                if (job == null)
                {
                    _logger.LogWarning("No job was found with ID: {id}", request.Id);
                }
                return job;
            }
        }
    }
}
