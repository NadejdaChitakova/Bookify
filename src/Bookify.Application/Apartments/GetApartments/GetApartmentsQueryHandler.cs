using Bookify.Application.Abstractions.Data;
using Bookify.Application.Abstractions.Messaging;
using Bookify.Domain.Abstractions;
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
                                    a.name AS Name,
                                    a.description AS Description,
                                    a.price_amount AS Price,
                                    a.price_currency AS Currency,
                                    a.address_country AS Country,
                                    a.address_city AS City,
                                    a.address_street AS Street
                                FROM apartments
                                """;

            var apartments = await connection
                                 .QueryAsync<ApartmentResponse, AddressResponse, ApartmentResponse>(
                                  sql,
                                  (apartment, address) =>
                                  {
                                      apartment.Address = address;

                                      return apartment;
                                  },
                                  splitOn: "Country");

            return apartments.ToList();
        }
    }
}
