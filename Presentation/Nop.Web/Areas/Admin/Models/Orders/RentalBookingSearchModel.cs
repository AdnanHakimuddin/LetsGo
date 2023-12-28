using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;
using System;
using System.ComponentModel.DataAnnotations;

namespace Nop.Web.Areas.Admin.Models.Orders
{
    public partial record RentalBookingSearchModel : BaseSearchModel
    {
        #region Properties

        [NopResourceDisplayName("Admin.RentalBooking.List.StartDate")]
        [UIHint("DateNullable")]
        public DateTime? StartDate { get; set; }

        [NopResourceDisplayName("Admin.RentalBooking.List.EndDate")]
        [UIHint("DateNullable")]
        public DateTime? EndDate { get; set; }
        
        [NopResourceDisplayName("Admin.RentalBooking.List.Name")]
        public string Name { get; set; }

        #endregion
    }
}