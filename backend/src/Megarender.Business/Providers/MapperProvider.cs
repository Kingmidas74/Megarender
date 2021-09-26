using AutoMapper;

namespace Megarender.Business.Providers
{
    public class MapperProvider: IMapperProvider
    {
        private readonly IMapper _mapper;
        public MapperProvider(IMapper mapper)
        {
            _mapper = mapper;
        }
        public D Map<S, D>(S source)
        {
            return _mapper.Map<D>(source);
        }
    }
}