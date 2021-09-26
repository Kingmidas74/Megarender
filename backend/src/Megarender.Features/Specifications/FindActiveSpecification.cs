using System;
using System.Linq.Expressions;
using Megarender.Domain;

namespace Megarender.Features.Specifications
{
    public class FindActiveSpecification<T>:Specification<T>
        where T:IEntity
    {
        public override Expression<Func<T, bool>> ToExpression()
        {
            return entity => entity.Status.Equals(EntityStatusId.Active);
        }
    }

}