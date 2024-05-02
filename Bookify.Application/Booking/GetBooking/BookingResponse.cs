namespace Bookify.Application.Booking.GetBooking
{
    public sealed class BookingResponse
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid ApartmentId { get; set; }

        public int Status { get; set; }

        public decimal PriceAmount { get; set; }

        public string PriceCurrency { get; set; }

        public decimal CleaningFeeAmount { get; set; }

        public decimal CleaningFeeCurrency { get; set; }  

        public decimal AmenitiesUpChargeAmount { get; set; }

        public decimal AmenitiesUpChargeCurrency { get; set; }

        public decimal TotalPriceAmount { get; set; }

        public decimal TotalPriceCurrency { get; set;}

        public decimal DurationStart { get; set; }

        public decimal DurationEnd { get; set; }

        public decimal CreatedOnUtc { get; set; }
    }
}
