using System.Net.Mime;
using Bookify.Application.Abstractions.Data;
using Bookify.Application.Abstractions.Messaging;
using Bookify.Domain.Abstractions;
using Bookify.Domain.AttachedFiles;
using Dapper;

namespace Bookify.Application.Apartments.GetApartments
{
    internal sealed class GetApartmentsQueryHandler(ISqlConnectionFactory connectionFactory) : IQueryHandler<GetApartmentsQuery, IReadOnlyList<ApartmentResponse>>
    {
        public async Task<Result<IReadOnlyList<ApartmentResponse>>> Handle(GetApartmentsQuery request, CancellationToken cancellationToken)
        {
            using var connection = connectionFactory.CreateConnection();
            const string sql = """
                                SELECT
                                    a.id AS Id,
                                    name AS Name,
                                    description AS Description,
                                    price_amount AS Price,
                                    price_currency AS Currency,
                                    address_country AS Country,
                                    address_city AS City
                                FROM apartments a
                                join apartment_image ai 
                                   on ai.apartment_id = a.id
                                """;

            //file_content AS ApartmentPhoto,
            //    main_photo AS IsMainPhoto,
            //    ai.id AS ApartmentPhotoId

            var apartments = await connection
                                 .QueryAsync<ApartmentResponse, List<Image>, ApartmentResponse>(
                                  sql,
                                  (apartment, image) =>
                                  {
                                      apartment.Images = image;

                                      return apartment;
                                  },
                                  splitOn: "ApartmentPhotoId");

            return apartments.ToList();
        }
    }
}
