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
                                    id AS Id,
                                    name AS Name,
                                    description AS Description,
                                    price_amount AS Price,
                                    price_currency AS Currency,
                                    address_country AS Country,
                                    address_city AS City
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
