using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf.Collections;
using Grpc.Core;
using GrpcApi;


using Server.DataAccess;
using NHibernate.Criterion;
using NHibernate.Linq;

namespace Server
{
    internal class WorkerRepository : IWorkerRepository
    {
        DbContext dbContext;

        public WorkerRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddWorkers(IAsyncStreamReader<AddWorkersRequest> requestStream, IServerStreamWriter<CrudOperationResponse> responseStream)
        {
            throw new NotImplementedException();
        }

        public async Task<WorkerMessage> GetWorker(long id)
        {
            Worker source;

            using (var session = dbContext.SessionFactory.OpenSession())
                source = await session.Query<Worker>().Where(x => x.Id == id).FirstOrDefaultAsync();

            return source?.ToWorkerMessage();
        }

        public async Task GetWorkersStream(IServerStreamWriter<WorkerMessage> responseStream)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveWorkers(RepeatedField<long> id, IServerStreamWriter<CrudOperationResponse> responseStream)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateWorkers(IAsyncStreamReader<UpdateWorkersRequest> requestStream, IServerStreamWriter<CrudOperationResponse> responseStream)
        {
            throw new NotImplementedException();
        }
    }
}
