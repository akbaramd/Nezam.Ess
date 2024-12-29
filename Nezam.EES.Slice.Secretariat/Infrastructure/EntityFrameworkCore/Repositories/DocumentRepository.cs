using Nezam.EES.Slice.Secretariat.Domains.Documents;
using Nezam.EES.Slice.Secretariat.Domains.Documents.Enumerations;
using Nezam.EES.Slice.Secretariat.Domains.Documents.Repositories;
using Nezam.EES.Slice.Secretariat.Domains.Participant;
using Nezam.EES.Slice.Secretariat.Domains.Participant.Repositories;
using Payeh.SharedKernel.EntityFrameworkCore;
using Payeh.SharedKernel.EntityFrameworkCore.Domain;
using Payeh.SharedKernel.EntityFrameworkCore.UnitofWork;

namespace Nezam.EES.Slice.Secretariat.Infrastructure.EntityFrameworkCore.Repositories;

public class DocumentRepository : EfCoreRepository<DocumentAggregateRoot,IPayehDbContext>,IDocumentRepository
{
    public DocumentRepository(IEfCoreDbContextProvider<IPayehDbContext> contextProvider) : base(contextProvider)
    {
    }

 
}