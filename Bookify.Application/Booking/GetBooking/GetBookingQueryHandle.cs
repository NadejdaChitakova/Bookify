using Bookify.Application.Abstractions.Data;
using Bookify.Application.Abstractions.Messaging;
using Bookify.Domain.Abstractions;
using Dapper;

namespace Bookify.Application.Booking.GetBooking
{
    public sealed class GetBookingQueryHandle : IQueryHandler<GetBookingQuery, BookingResponse>
    {
private readonly ISqlConnectionFactory _connectionFactory;

public GetBookingQueryHandle(ISqlConnectionFactory connectionFactory)
{
    _connectionFactory = connectionFactory;
}

public async Task<Result<BookingResponse>> Handle(GetBookingQuery request, CancellationToken cancellationToken)
        {
            using var connection = _connectionFactory.CreateConnection();

const string sql = """
                   SELECT 
                   id as Id,
                   
                   """;

            var booking = await connection.QueryFirstOrDefaultAsync(
                                                                    sql,
                                                                    new
                                                                    {
                                                                        request.BookingId
                                                                    }
                                                                    );

            return booking;
        }
    }
}
