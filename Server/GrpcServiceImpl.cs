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

        public override Task AddWorkers(IAsyncStreamReader<WorkerMessage> requestStream, IServerStreamWriter<CrudOperationResponse> responseStream, ServerCallContext context)
        {
            return repository.AddWorkers(requestStream, responseStream, context);
        }

        public override Task<WorkerMessage> GetWorker(GetWorkerRequest request, ServerCallContext context)
        {
            return repository.GetWorker(request.Id);
        }

        public override Task GetWorkersStream(GetWorkersStreamRequest request, IServerStreamWriter<WorkerMessage> responseStream, ServerCallContext context)
        {
            return repository.GetWorkersStream(responseStream, context);
        }

        public override Task<CrudOperationResponse> RemoveWorkers(RemoveWorkersRequest request, ServerCallContext context)
        {
            return repository.RemoveWorkers(request.Ids, context);
        }

        public override async Task UpdateWorkers(IAsyncStreamReader<UpdateWorkersRequest> requestStream, IServerStreamWriter<CrudOperationResponse> responseStream, ServerCallContext context)
        {
            await repository.UpdateWorkers(requestStream, responseStream, context);
        }
    }
}
