using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;
using System;

namespace Nop.Web.Areas.Admin.Models.Orders
{
    public partial record RentalBookingModel : BaseNopEntityModel
    {
        #region Properties
        
        [NopResourceDisplayName("Admin.RentalBooking.Fields.Name")]
        public string Name { get; set; }
        [NopResourceDisplayName("Admin.RentalBooking.Fields.FatherName")]
        public string FatherName { get; set; }
        [NopResourceDisplayName("Admin.RentalBooking.Fields.Cnic")]
        public string Cnic { get; set; }
        [NopResourceDisplayName("Admin.RentalBooking.Fields.DriverName")]
        public string DriverName { get; set; }
        [NopResourceDisplayName("Admin.RentalBooking.Fields.LicenceNumber")]
        public string LicenceNumber { get; set; }
        [NopResourceDisplayName("Admin.RentalBooking.Fields.Make")]
        public string Make { get; set; }
        [NopResourceDisplayName("Admin.RentalBooking.Fields.CarColor")]
        public string CarColor { get; set; }
        [NopResourceDisplayName("Admin.RentalBooking.Fields.CarNumber")]
        public string CarNumber { get; set; }
        [NopResourceDisplayName("Admin.RentalBooking.Fields.EngineNumber")]
        public string EngineNumber { get; set; }
        [NopResourceDisplayName("Admin.RentalBooking.Fields.ChassisNumber")]
        public string ChassisNumber { get; set; }
        [NopResourceDisplayName("Admin.RentalBooking.Fields.BookingDate")]
        public DateTime BookingDate { get; set; }
        [NopResourceDisplayName("Admin.RentalBooking.Fields.ReturnDate")]
        public DateTime ReturnDate { get; set; }

        [NopResourceDisplayName("Admin.RentalBooking.Fields.FormattedBookingDate")]
        public string FormattedBookingDate { get; set; }
        [NopResourceDisplayName("Admin.RentalBooking.Fields.FormattedReturnDate")]
        public string FormattedReturnDate { get; set; }
        [NopResourceDisplayName("Admin.RentalBooking.Fields.RentTime")]
        public DateTime RentTime { get; set; }
        [NopResourceDisplayName("Admin.RentalBooking.Fields.TotalCost")]
        public decimal TotalCost { get; set; }
        [NopResourceDisplayName("Admin.RentalBooking.Fields.AdvanceCost")]
        public decimal AdvanceCost { get; set; }
        [NopResourceDisplayName("Admin.RentalBooking.Fields.RemainingCost")]
        public decimal RemainingCost { get; set; }
        [NopResourceDisplayName("Admin.RentalBooking.Fields.KarachiTo")]
        public string KarachiTo { get; set; }
        [NopResourceDisplayName("Admin.RentalBooking.Fields.Address")]
        public string Address { get; set; }
        [NopResourceDisplayName("Admin.RentalBooking.Fields.PhoneNumber1")]
        public string PhoneNumber1 { get; set; }
        [NopResourceDisplayName("Admin.RentalBooking.Fields.PhoneNumber2")]
        public string PhoneNumber2 { get; set; }

        //Advance Booking
        [NopResourceDisplayName("Admin.RentalBooking.Fields.DateOfDepartue")]
        public DateTime DateOfDepartue { get; set; }
        [NopResourceDisplayName("Admin.RentalBooking.Fields.ProgrammeDate")]
        public DateTime ProgrammeDate { get; set; }
        #endregion
    }
}