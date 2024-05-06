using Bookify.Application.Abstractions.Data;
using Bookify.Application.Abstractions.Messaging;
using Bookify.Domain.Abstractions;
using Bookify.Domain.Booking;
using Dapper;

namespace Bookify.Application.Apartments.SearchAppartments
{
    internal sealed class SearchAppartmentsQueryHandler
    :IQueryHandler<SearchApartmentsQuery, IReadOnlyList<ApartmentResponse>>
    {
        private static readonly int[] ActiveBookingStatuses =
        {
(int)BookingStatus.Reserved,
(int)BookingStatus.Confirmed,
(int)BookingStatus.Completed
        };

        private readonly ISqlConnectionFactory _connectionFactory;

        public SearchAppartmentsQueryHandler(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<Result<IReadOnlyList<ApartmentResponse>>> Handle(SearchApartmentsQuery request, CancellationToken cancellationToken)
        {
            using var connection = _connectionFactory.CreateConnection();

            if (request.dateStart > request.dateEnd)
            {
                return new List<ApartmentResponse>();
            }

            const string sql = "";

            var apartments = await connection
                                 .QueryAsync<ApartmentResponse, AddressResponse, ApartmentResponse>(
                                      sql,
                                      (apartment, address) =>
                                      {
                                          apartment.Address = address;

                                          return apartment;
                                      },
                                      new
                                      {
                                          request.dateStart,
                                          request.dateEnd,
                                          ActiveBookingStatuses
                                      },
                                      splitOn: "Country");

            return apartments.ToList();
        }
    }
}
