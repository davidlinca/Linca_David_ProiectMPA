using Linca_David_ProiectMPA.Models;
using Linca_David_ProiectMPA.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Linca_David_ProiectMPA.Controllers
{
    public class TypePredictionController : Controller
    {
        private readonly ITypePredictionService _predictionService;

        public TypePredictionController(ITypePredictionService predictionService)
        {
            _predictionService = predictionService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new TypePredictionViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Index(TypePredictionViewModel model)
        {
            var input = new TypePredictionInput
            {
                Type = "",
                Title = model.Title,
                Director = model.Director,
                Cast = model.Cast,
                Duration = model.Duration
            };

            var prediction = await _predictionService.PredictAsync(input);
            model.PredictedType = prediction ?? "No prediction";

            return View(model);
        }
    }
}
