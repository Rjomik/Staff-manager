using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf.Collections;
using Grpc.Core;
using GrpcApi;

namespace Server
{
    /// <summary>
    /// For further mocking
    /// </summary>
    public interface IWorkerRepository
    {
        Task AddWorkers(IAsyncStreamReader<WorkerMessage> requestStream, IServerStreamWriter<CrudOperationResponse> responseStream, ServerCallContext context);
        Task<WorkerMessage> GetWorker(long id);
        Task GetWorkersStream(IServerStreamWriter<WorkerMessage> responseStream, ServerCallContext context);
        Task<CrudOperationResponse> RemoveWorkers(RepeatedField<long> ids, ServerCallContext context);
        Task UpdateWorkers(IAsyncStreamReader<UpdateWorkersRequest> requestStream, IServerStreamWriter<CrudOperationResponse> responseStream, ServerCallContext context);
    }
}
