using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using movieStats;

namespace Linca_David_ProiectMPA.Controllers
{
    public class MovieStatsGrpcController : Controller
    {
        private readonly MovieStatsService.MovieStatsServiceClient _client;

        public MovieStatsGrpcController()
        {
            var channel = GrpcChannel.ForAddress("https://localhost:7083");
            _client = new MovieStatsService.MovieStatsServiceClient(channel);
        }

        public IActionResult Index()
        {
            var result = _client.GetMovieCount(new Empty());
            ViewBag.Count = result.Count;
            return View();
        }
    }
}
