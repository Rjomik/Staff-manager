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
using FluentNHibernate.Utils;
using System.Diagnostics.Eventing.Reader;
using FluentNHibernate;

namespace Server
{
    internal class WorkerRepository : IWorkerRepository
    {
        DbContext dbContext;

        public WorkerRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddWorkers(IAsyncStreamReader<WorkerMessage> requestStream, IServerStreamWriter<CrudOperationResponse> responseStream, ServerCallContext context)
        {
            using(var session = dbContext.SessionFactory.OpenSession())
            {
                while (await requestStream.MoveNext() && !context.CancellationToken.IsCancellationRequested)
                {
                    var newbie = requestStream.Current;
                    using (var transaction = session.BeginTransaction())
                    {
                        session.Save(new Worker(newbie));
                        await transaction.CommitAsync();
                        await responseStream.WriteAsync(new CrudOperationResponse() { Result = CrudStatusCode.Success });
                    }
                }
            }
        }

        public async Task<WorkerMessage> GetWorker(long id)
        {
            Worker source;

            using (var session = dbContext.SessionFactory.OpenSession())
                source = await session.Query<Worker>().Where(x => x.Id == id).FirstOrDefaultAsync();

            return source?.ToWorkerMessage();
        }

        public async Task GetWorkersStream(IServerStreamWriter<WorkerMessage> responseStream, ServerCallContext context)
        {
            using (var session = dbContext.SessionFactory.OpenSession())
            {
                foreach(var i in session.Query<Worker>())
                {
                    await responseStream.WriteAsync(i.ToWorkerMessage());
                }
            }
        }

        public async Task<CrudOperationResponse> RemoveWorkers(RepeatedField<long> ids, ServerCallContext context)
        {
            using (var session = dbContext.SessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    await session.Query<Worker>().Where(x => ids.Contains(x.Id)).DeleteAsync();
                    await transaction.CommitAsync();
                    return new CrudOperationResponse() { Result = CrudStatusCode.Success };
                }
            }
        }

        public async Task UpdateWorkers(IAsyncStreamReader<UpdateWorkersRequest> requestStream, IServerStreamWriter<CrudOperationResponse> responseStream, ServerCallContext context)
        {
            using (var session = dbContext.SessionFactory.OpenSession())
            {
                while (await requestStream.MoveNext() && !context.CancellationToken.IsCancellationRequested)
                {
                    var newbie = requestStream.Current;
                    using (var transaction = session.BeginTransaction())
                    {
                        var exists = session.Get<Worker>(newbie.OldWorkerInfo.Id);
                        if (exists != null)
                        {
                            if (exists.Equals(newbie.OldWorkerInfo))
                            {
                                exists.CopyProperties(newbie.NewWorkerInfo);
                                await session.UpdateAsync(exists);
                                await responseStream.WriteAsync(new CrudOperationResponse() { Result = CrudStatusCode.Success });
                            }
                            else
                                await responseStream.WriteAsync(new CrudOperationResponse() { Result = CrudStatusCode.UpdatedByOther });
                        
                        }
                        else
                            await responseStream.WriteAsync(new CrudOperationResponse() { Result = CrudStatusCode.NotFound });

                        await transaction.CommitAsync();
                    }
                }
            }
        }
    }
}
