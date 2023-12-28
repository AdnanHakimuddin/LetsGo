using FluentValidation;
using Nop.Core.Domain.Catalog;
using Nop.Data.Mapping;
using Nop.Services.Localization;
using Nop.Web.Areas.Admin.Models.Orders;
using Nop.Web.Framework.Validators;

namespace Nop.Web.Areas.Admin.Validators.Orders
{
    public partial class RentalBookingValidator : BaseNopValidator<RentalBookingModel>
    {
        public RentalBookingValidator(ILocalizationService localizationService, IMappingEntityAccessor mappingEntityAccessor)
        {
            RuleFor(x => x.Name).NotEmpty().WithMessageAwait(localizationService.GetResourceAsync("Admin.Catalog.RentalBooking.Fields.Name.Required"));
            //RuleFor(x => x.Cnic).NotEmpty().WithMessageAwait(localizationService.GetResourceAsync("Admin.Catalog.RentalBooking.Fields.Cnic.Required"));
            RuleFor(x => x.PhoneNumber1).NotEmpty().WithMessageAwait(localizationService.GetResourceAsync("Admin.Catalog.RentalBooking.Fields.PhoneNumber1.Required"));
            RuleFor(x => x.TotalCost).NotEmpty().WithMessageAwait(localizationService.GetResourceAsync("Admin.Catalog.RentalBooking.Fields.TotalCost.Required"));

            SetDatabaseValidationRules<RentalBooking>(mappingEntityAccessor);
        }
    }
}