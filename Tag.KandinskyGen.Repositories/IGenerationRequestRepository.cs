using System.Runtime.CompilerServices;
using Tag.KandinskyGen.Repositories.Entities;

[assembly: InternalsVisibleTo("Tag.KandinskyGen.Managers")]
namespace Tag.KandinskyGen.Repositories;

internal interface IGenerationRequestRepository
{
    Task InsertGenerationActivity(GenerationActivityEntity entity);
}
