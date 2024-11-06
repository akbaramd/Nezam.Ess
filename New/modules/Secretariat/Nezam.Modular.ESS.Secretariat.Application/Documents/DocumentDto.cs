﻿using Bonyan.Layer.Application.Dto;
using Bonyan.UserManagement.Domain.ValueObjects;
using Nezam.Modular.ESS.Identity.Application.Users.Dto;
using Nezam.Modular.ESS.Secretariat.Domain.Documents;
using Nezam.Modular.ESS.Secretariat.Domain.Documents.Enumerations;
using Nezam.Modular.ESS.Secretariat.Domain.Documents.ValueObjects;

namespace Nezam.Modular.ESS.Secretariat.Application.Documents;

public class DocumentDto : FullAuditableAggregateRootDto<DocumentId>
{
    public string Title { get; set; }
    public string Content { get; set; }
    
    public UserId OwnerUserId { get; private set; }
    public UserDto OwnerUser { get; private set; }
    
    public DocumentType Type { get; private set; }
    public DocumentStatus Status { get; private set; }

    private readonly List<DocumentAttachmentEntity> _attachments = new List<DocumentAttachmentEntity>();
    public IReadOnlyList<DocumentAttachmentEntity> Attachments => _attachments;

    private readonly List<DocumentReferralEntity> _referrals = new List<DocumentReferralEntity>();
    public IReadOnlyList<DocumentReferralEntity> Referrals => _referrals;

    public List<DocumentActivityLogDto> ActivityLogs { get; set; }
    
}

public class DocumentActivityLogDto : EntityDto<DocumentActivityLogId>
{
    public DateTime ActivityDate { get; private set; }
    public UserId UserId { get; private set; }
    public string Key { get; private set; }
    public string Description { get; private set; }
    
}
public class DocumentUpdateDto
{
    public string Title { get; set; }
    public string Content { get; set; }
    public int Type { get; set; }
}
