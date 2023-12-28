using Nop.Core.Domain.Common;
using Nop.Core.Domain.Orders;
using System;

namespace Nop.Core.Domain.Catalog
{
    public partial class RentalBooking : BaseEntity, ISoftDeletedEntity
    {
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string Cnic { get; set; }
        public string DriverName { get; set; }
        public string LicenceNumber { get; set; }
        public string Make { get; set; }
        public string CarColor { get; set; }
        public string CarNumber { get; set; }
        public string EngineNumber { get; set; }
        public string ChassisNumber { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public DateTime RentTime { get; set; }
        public decimal TotalCost { get; set; }
        public decimal AdvanceCost { get; set; }
        public decimal RemainingCost { get; set; }
        public string KarachiTo { get; set; }
        public string Address { get; set; }
        public string PhoneNumber1 { get; set; }
        public string PhoneNumber2 { get; set; }
        public bool Deleted { get; set; }
        public int DeletedById { get; set; }
        public int CreatedById { get; set; }
        public int BookingTypeId { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime UpdatedOnUtc { get; set; }

        public BookingEnum BookingType
        {
            get => (BookingEnum)BookingTypeId;
            set => BookingTypeId = (int)value;
        }

        //Advance booking
        public DateTime DateOfDeparture { get; set; }
        public DateTime ProgrammeDate { get; set; }
    }
}