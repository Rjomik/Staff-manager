using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GrpcApi;
using Grpc.Core;

using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace Server
{
    internal class GrpcServiceImpl : WorkerIntegration.WorkerIntegrationBase
    {
        IWorkerRepository repository;

        public GrpcServiceImpl(IWorkerRepository repository)
        {
            this.repository = repository;
        }

        public override async Task AddWorkers(IAsyncStreamReader<AddWorkersRequest> requestStream, IServerStreamWriter<CrudOperationResponse> responseStream, ServerCallContext context)
        {
            //todo validation of fields
            await repository.AddWorkers(requestStream, responseStream);
        }

        public override async Task<WorkerMessage> GetWorker(GetWorkerRequest request, ServerCallContext context)
        {
            return await repository.GetWorker(request.Id);
        }

        public override async Task GetWorkersStream(GetWorkersStreamRequest request, IServerStreamWriter<WorkerMessage> responseStream, ServerCallContext context)
        {
            await repository.GetWorkersStream(responseStream);
        }

        public override async Task RemoveWorkers(RemoveWorkersRequest request, IServerStreamWriter<CrudOperationResponse> responseStream, ServerCallContext context)
        {
            await repository.RemoveWorkers(request.Id, responseStream);
        }

        public override async Task UpdateWorkers(IAsyncStreamReader<UpdateWorkersRequest> requestStream, IServerStreamWriter<CrudOperationResponse> responseStream, ServerCallContext context)
        {
            await repository.UpdateWorkers(requestStream, responseStream);
        }
    }
}
