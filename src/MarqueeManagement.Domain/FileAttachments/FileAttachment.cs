using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace MarqueeManagement.FileAttachments;

public class FileAttachment : FullAuditedAggregateRoot<Guid>, IMultiTenant
{
    public Guid? TenantId { get; private set; }

    public string FileName { get; private set; }

    public string BlobName { get; private set; }

    public string ContentType { get; private set; }

    public long Size { get; private set; }
    public byte[] FileData { get; private set; }

    protected FileAttachment()
    {
    }

    public FileAttachment(
        Guid id,
        Guid? tenantId,
        string fileName,
        string blobName,
        string contentType,
        long size,
        byte[] fileData) 
        : base(id)
    {
        TenantId = tenantId;

        FileName = Check.NotNullOrWhiteSpace(
            fileName,
            nameof(fileName),
            FileAttachmentConsts.MaxFileNameLength);

        BlobName = Check.NotNullOrWhiteSpace(
            blobName,
            nameof(blobName),
            FileAttachmentConsts.MaxBlobNameLength);

        ContentType = Check.NotNullOrWhiteSpace(
            contentType,
            nameof(contentType),
            FileAttachmentConsts.MaxContentTypeLength);

        Size = size;
        FileData = fileData;
    }
}
