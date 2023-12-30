using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Orders;
using Nop.Services.Helpers;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Services.Orders;
using Nop.Services.Security;
using Nop.Web.Areas.Admin.Factories;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using Nop.Web.Areas.Admin.Models.Orders;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc;
using Nop.Web.Framework.Mvc.Filters;
using System;
using System.Threading.Tasks;
using Wkhtmltopdf.NetCore;

namespace Nop.Web.Areas.Admin.Controllers
{
    public partial class AdvanceBookingController : BaseAdminController
    {
        #region Fields

        private readonly IOrderModelFactory _orderModelfactory;
        private readonly IOrderService _orderService;
        private readonly ILocalizationService _localizationService;
        private readonly IPermissionService _permissionService;
        private readonly IWorkContext _workContext;
        private readonly INotificationService _notificationService;
        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly IGeneratePdf _generatePdf;

        #endregion

        #region Ctor

        public AdvanceBookingController(IOrderModelFactory orderModelfactory,
            IOrderService orderService,
            ILocalizationService localizationService,
            IPermissionService permissionService,
            IWorkContext workContext,
            INotificationService notificationService,
            IDateTimeHelper dateTimeHelper,
            IGeneratePdf generatePdf)
        {
            _orderModelfactory = orderModelfactory;
            _orderService = orderService;
            _localizationService = localizationService;
            _permissionService = permissionService;
            _workContext = workContext;
            _notificationService = notificationService;
            _dateTimeHelper = dateTimeHelper;
            _generatePdf = generatePdf;
        }

        #endregion

        #region List

        public virtual IActionResult Index()
        {
            return RedirectToAction("List");
        }

        public virtual async Task<IActionResult> List()
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageCategories))
                return AccessDeniedView();

            //prepare model
            var model = _orderModelfactory.PrepareRentalBookingSearchModelAsync(new RentalBookingSearchModel());

            return View(model);
        }

        [HttpPost]
        public virtual async Task<IActionResult> List(RentalBookingSearchModel searchModel)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageCategories))
                return await AccessDeniedDataTablesJson();

            //prepare model
            var model = await _orderModelfactory.PrepareAdvanceBookingListModelAsync(searchModel);

            return Json(model);
        }

        #endregion

        #region Create / Edit / Delete

        public virtual async Task<IActionResult> Create()
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageCategories))
                return AccessDeniedView();

            //prepare model
            var model = await _orderModelfactory.PrepareAdvanceBookingModelAsync(new RentalBookingModel(), null);

            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual async Task<IActionResult> Create(RentalBookingModel model, bool continueEditing)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageCategories))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var rentalBooking = model.ToEntity<RentalBooking>();
                rentalBooking.CreatedOnUtc = DateTime.UtcNow;
                rentalBooking.UpdatedOnUtc = DateTime.UtcNow;
                rentalBooking.BookingDate = model.BookingDate;
                rentalBooking.DateOfDeparture = model.DateOfDepartue;
                rentalBooking.ProgrammeDate = model.BookingDate;
                rentalBooking.CreatedById = _workContext.GetCurrentCustomerAsync().Id;
                rentalBooking.BookingTypeId = (int)BookingEnum.AdvanceBooking;
                await _orderService.InsertRentalBookingAsync(rentalBooking);

                _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Catalog.AdvanceBooking.Created"));

                //if (!continueEditing)
                    return RedirectToAction("List");
                
                //return RedirectToAction("Edit", new { id = rentalBooking.Id });
            }

            //prepare model
            model = await _orderModelfactory.PrepareAdvanceBookingModelAsync(model, null);

            //if we got this far, something failed, redisplay form
            return View(model);
        }

        public virtual async Task<IActionResult> Edit(int id)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageCategories))
                return AccessDeniedView();

            //try to get a campaign with the specified id
            var rentalBooking = await _orderService.GetRentalBookingByIdAsync(id);
            if (rentalBooking == null)
                return RedirectToAction("List");

            //prepare model
            var model = await _orderModelfactory.PrepareRentaBookingModelAsync(new RentalBookingModel(), rentalBooking);

            return View(model);
        }

        [HttpPost]
        [ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        public virtual async Task<IActionResult> Edit(RentalBookingModel model, bool continueEditing)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageCategories))
                return AccessDeniedView();

            var rentalBooking = await _orderService.GetRentalBookingByIdAsync(model.Id);
            if (rentalBooking == null)
                return RedirectToAction("List");

            if (ModelState.IsValid)
            {
                rentalBooking = model.ToEntity(rentalBooking);
                await _orderService.UpdateRentalBookingAsync(rentalBooking);

                _notificationService.SuccessNotification("Advance Booking Updated");

                return continueEditing ? RedirectToAction("Edit", new { id = rentalBooking.Id }) : RedirectToAction("List");
            }

            //prepare model
            model = await _orderModelfactory.PrepareRentaBookingModelAsync(model, null);

            //if we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Delete(int id)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageCategories))
                return AccessDeniedView();

            //try to get a category with the specified id
            var rentalBooking = await _orderService.GetRentalBookingByIdAsync(id);
            if (rentalBooking == null)
                return RedirectToAction("List");

            await _orderService.DeleteRentalBookingAsync(rentalBooking);

            _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Catalog.AdvanceBooking.Deleted"));

            return new NullJsonResult();
        }

        #endregion

        #region Export PDF and Print

        public virtual async Task<IActionResult> ExportAdvanceBookingPdf(RentalBookingModel searchModel)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageCategories))
                return AccessDeniedView();

            var rentalBooking = await _orderService.GetRentalBookingByIdAsync(searchModel.Id);
            if (rentalBooking == null)
                return RedirectToAction("List");

            var model = await _orderModelfactory.PrepareRentaBookingModelAsync(searchModel, rentalBooking);

            var html = await RenderPartialViewToStringAsync("_AdvanceBookingPdf", model);
            var bytes = _generatePdf.GetPDF(html);

            return File(bytes, MimeTypes.ApplicationPdf, $"{model.Name}.pdf");
        }


        #endregion
    }
}