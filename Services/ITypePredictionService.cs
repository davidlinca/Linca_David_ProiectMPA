using System.Threading.Tasks;
using Linca_David_ProiectMPA.Models;

namespace Linca_David_ProiectMPA.Services
{
    public interface ITypePredictionService
    {
        Task<string> PredictAsync(TypePredictionInput input);
    }
}