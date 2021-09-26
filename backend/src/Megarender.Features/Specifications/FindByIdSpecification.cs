using System;
using System.Linq.Expressions;
using Megarender.Domain;

namespace Megarender.Features.Specifications
{
    public class FindByIdSpecification<T>:Specification<T>
        where T:IEntity
    {
        private readonly Guid _id;
    
        public FindByIdSpecification(Guid id)
        {
            _id=id;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            return entity => entity.Id.Equals(_id);
        }
    }

}