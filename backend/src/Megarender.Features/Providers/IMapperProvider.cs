namespace Megarender.Features.Providers
{
    public interface IMapperProvider
    {
        public D Map<S, D>(S source);
    }
}