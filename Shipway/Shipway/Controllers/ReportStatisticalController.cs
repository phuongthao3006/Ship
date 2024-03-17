using Shipway.Authorize;
using ShipwayBusiness.CoreProvider;
using ShipwayBusiness.DataAccessLayer;
using System.Web.Mvc;

namespace Shipway.Controllers
{
    [ShipwayAuthorize]
    public class ReportStatisticalController : Controller
    {
        private IExhibitionProvider _exhibitionProvider;
        private INoteRequiredProvider _noteRequiredProvider;
        private IKindServiceProvider _kindServiceProvider;
        private IExhibitionStatusProvider _exhibitionStatusProvider;
        private IProvinceProvider _provinceProvider;
        private IDistrictProvider _districtProvider;
        private IWardProvider _wardProvider;
        private int? _agencyIdOfCurrentUser;

        public ReportStatisticalController()
        {
            _exhibitionProvider = new ExhibitionProvider();
            _noteRequiredProvider = new NoteRequiredProvider();
            _kindServiceProvider = new KindServiceProvider();
            _exhibitionStatusProvider = new ExhibitionStatusProvider();
            _provinceProvider = new ProvinceProvider();
            _districtProvider = new DistrictProvider();
            _wardProvider = new WardProvider();
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}