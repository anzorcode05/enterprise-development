
using Microsoft.AspNetCore.Mvc;
using TransportCompany.Application.Contracts.VehicleModels;

namespace TransportCompany.Api.Host.Controllers;

[Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
public sealed class VehicleModelsController(
    IVehicleModelService service)
    : ApiControllerBase<VehicleModelDto, VehicleModelUpsertDto>(service)
{
    protected override int GetId(VehicleModelDto item)
    {
        return item.Id;
    }
}
