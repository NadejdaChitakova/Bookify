using Bookify.Domain.Abstractions;
using Bookify.Domain.AttachedFiles.Events;

namespace Bookify.Domain.AttachedFiles
{
    public sealed class ApartmentImage : Entity
    {
        private ApartmentImage(
            Guid id,
            Guid apartmentId,
            MainPhoto mainPhoto,
            FileContent fileContent) : base(id) 
        {
            Id = id;
            ApartmentId = apartmentId;
            MainPhoto = mainPhoto;
            FileContent = fileContent;
        }
        private ApartmentImage() { }

        public Guid ApartmentId { get; init; }

public MainPhoto MainPhoto { get; init; }

        public FileContent FileContent { get; init; }

        public static Result<ApartmentImage> Upload(
            Guid apartmentId,
            MainPhoto mainPhoto,
            FileContent fileContent)
        {
            var apartmentImage = new ApartmentImage(
                                                    Guid.NewGuid(),
                                                    apartmentId, 
                                                    mainPhoto,
                                                    fileContent);

            apartmentImage.RaiseDomainEvent(new UploadPhotoDomainEvent(apartmentImage.Id));

            return apartmentImage;
        }
    }
}
