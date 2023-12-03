using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GrpcApi;
using Grpc.Core;

namespace Server
{
    internal class GrpcServiceImpl : WorkerIntegration.WorkerIntegrationBase
    {
        IDbContext dbContext;

        public override async Task AddWorkers(IAsyncStreamReader<AddWorkersRequest> requestStream, IServerStreamWriter<CrudOperationResponse> responseStream, ServerCallContext context)
        {
            //todo validation of fields
            await dbContext.AddWorkers(requestStream, responseStream);
        }

        public override async Task<WorkerMessage> GetWorker(GetWorkerRequest request, ServerCallContext context)
        {
            return await dbContext.GetWorker(request.Id);
        }

        public override async Task GetWorkersStream(GetWorkersStreamRequest request, IServerStreamWriter<WorkerMessage> responseStream, ServerCallContext context)
        {
            await dbContext.GetWorkersStream(responseStream);
        }

        public override async Task RemoveWorkers(RemoveWorkersRequest request, IServerStreamWriter<CrudOperationResponse> responseStream, ServerCallContext context)
        {
            await dbContext.RemoveWorkers(request.Id, responseStream);
        }

        public override async Task UpdateWorkers(IAsyncStreamReader<UpdateWorkersRequest> requestStream, IServerStreamWriter<CrudOperationResponse> responseStream, ServerCallContext context)
        {
            await dbContext.UpdateWorkers(requestStream, responseStream);
        }
    }
}
