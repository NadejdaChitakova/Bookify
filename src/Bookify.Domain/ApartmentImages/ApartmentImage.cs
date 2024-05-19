using Bookify.Domain.Abstractions;
using Bookify.Domain.AttachedFiles.Events;

namespace Bookify.Domain.AttachedFiles
{
    public sealed class ApartmentImage : Entity
    {
        private ApartmentImage(
            Guid id,
            Guid apartmentId,
            Extension extension,
            FileContent fileContent) : base(id) 
        {
            Id = id;
            ApartmentId = apartmentId;
            Extension = extension;
            FileContent = fileContent;
        }
        private ApartmentImage() { }

        public Guid ApartmentId { get; init; }

        public Extension Extension { get; set; }

        public FileContent FileContent { get; set; }

        public static Result<ApartmentImage> Upload(
            Guid apartmentId,
            Extension extension,
            FileContent fileContent)
        {
            var apartmentImage = new ApartmentImage(
                                                    Guid.NewGuid(),
                                                    apartmentId, 
                                                        extension,
                                                    fileContent);

            apartmentImage.RaiseDomainEvent(new UploadPhotoDomainEvent(apartmentImage.Id));

            return apartmentImage;
        }
    }
}
