using TransportCompany.Application.Contracts.VehicleModels;
using TransportCompany.Domain.Entities;
using TransportCompany.Domain.Repositories;
using TransportCompany.Domain.Shared;
namespace TransportCompany.Application.Services;
public sealed class VehicleModelService : CrudService<VehicleModel, VehicleModelDto, VehicleModelCreateDto, VehicleModelUpdateDto>, IVehicleModelService
{
    public VehicleModelService(IRepository<VehicleModel> repository) : base(repository) { }
    protected override VehicleModelDto ToDto(VehicleModel m) => new(m.Id,m.BodyType.ToString(),m.BodyVolume);
    protected override VehicleModel FromCreate(VehicleModelCreateDto r,int id) => new() { Id=id, BodyType=ParseBodyType(r.BodyType), BodyVolume=r.BodyVolume };
    protected override VehicleModel FromUpdate(VehicleModel m,VehicleModelUpdateDto r)
    {
        m.BodyType = ParseBodyType(r.BodyType);
        m.BodyVolume = r.BodyVolume;
        return m;
    }
    private static BodyType ParseBodyType(string value) => Enum.TryParse<BodyType>(value,true,out var type) && Enum.IsDefined(type) ? type : throw new ArgumentException("Неизвестный тип кузова.");
}
