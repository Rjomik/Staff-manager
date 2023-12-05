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
        Task AddWorkers(IAsyncStreamReader<AddWorkersRequest> requestStream, IServerStreamWriter<CrudOperationResponse> responseStream);
        Task<WorkerMessage> GetWorker(long id);
        Task GetWorkersStream(IServerStreamWriter<WorkerMessage> responseStream);
        Task RemoveWorkers(RepeatedField<long> id, IServerStreamWriter<CrudOperationResponse> responseStream);
        Task UpdateWorkers(IAsyncStreamReader<UpdateWorkersRequest> requestStream, IServerStreamWriter<CrudOperationResponse> responseStream);
    }
}
