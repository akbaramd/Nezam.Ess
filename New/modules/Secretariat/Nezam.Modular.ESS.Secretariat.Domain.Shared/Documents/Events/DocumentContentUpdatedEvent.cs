﻿using Nezam.Modular.ESS.Secretariat.Domain.Shared.Documents.ValueObjects;
using Payeh.SharedKernel.Domain.DomainEvents;

namespace Nezam.Modular.ESS.Secretariat.Domain.Shared.Documents.Events;

public class DocumentContentUpdatedEvent : DomainEvent
{
    public DocumentId DocumentId { get; }

    public DocumentContentUpdatedEvent(DocumentId documentId)
    {
        DocumentId = documentId;
    }
}