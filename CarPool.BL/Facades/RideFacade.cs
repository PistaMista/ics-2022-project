using AutoMapper;
using CarPool.BL.Models;
using CarPool.DAL.Entities;
using CarPool.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
namespace CarPool.BL.Facades;
public class RideFacade : CrudFacade<RideEntity, RideInfoModel, RideModel>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWorkFactory _unitOfWorkFactory;
    public RideFacade(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper) : base(unitOfWorkFactory, mapper)
    {
        _unitOfWorkFactory = unitOfWorkFactory;
        _mapper = mapper;
    }
    public async Task<IEnumerable<RideInfoModel>> FilterOfRides(string originCity, string DestinationCity, DateTime date)
    {
        await using var uow = _unitOfWorkFactory.Create();

        var query = uow.
            GetRepository<RideEntity>()
            .Get();
        if(originCity != "")
        {
            query = query.Where(e => e.StartLocation.ToLower() == originCity.ToLower());
        }
        if (DestinationCity != "")
        {
            query = query.Where(e => e.EndLocation.ToLower() == DestinationCity.ToLower());
        }
        if (date != default(DateTime))
        {
            query = query.Where(e => e.StartTime >= date);
        }

        return await _mapper.ProjectTo<RideInfoModel>(query).ToArrayAsync().ConfigureAwait(false);
    }
    public async Task<IEnumerable<RideInfoModel>> FilterByDriver(Guid id)
    {
        await using var uow = _unitOfWorkFactory.Create();

        var query = uow.
            GetRepository<RideEntity>()
            .Get()
            .Where(e => e.DriverId == id);

        return await _mapper.ProjectTo<RideInfoModel>(query).ToArrayAsync().ConfigureAwait(false);
    }
    public async Task<IEnumerable<RideInfoModel>> FilterByOrigin(string originCity)
    {
        await using var uow = _unitOfWorkFactory.Create();
        
        var query = uow.
            GetRepository<RideEntity>()
            .Get()
            .Where(e => e.StartLocation.ToLower() == originCity.ToLower());

        return await _mapper.ProjectTo<RideInfoModel>(query).ToArrayAsync().ConfigureAwait(false);
    }
    public async Task<IEnumerable<RideInfoModel>> FilterByDestination(string DestinationCity)
    {
        await using var uow = _unitOfWorkFactory.Create();

        var query = uow.
            GetRepository<RideEntity>()
            .Get()
            .Where(e => e.EndLocation.ToLower() == DestinationCity.ToLower());

        return await _mapper.ProjectTo<RideInfoModel>(query).ToArrayAsync().ConfigureAwait(false);
    }
    public async Task<IEnumerable<RideInfoModel>> FilterByOlderThan(string date)
    {
        await using var uow = _unitOfWorkFactory.Create();
        DateTime dateTime = DateTime.ParseExact(date, "dd/MM/yyyy HH:mm", null);
        var query = uow.
            GetRepository<RideEntity>()
            .Get()
            .Where(e => e.StartTime >= dateTime);

        return await _mapper.ProjectTo<RideInfoModel>(query).ToArrayAsync().ConfigureAwait(false);
    }
}

