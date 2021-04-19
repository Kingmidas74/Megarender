using System;
using System.Linq.Expressions;
using Megarender.Domain;

namespace Megarender.Business.Specifications
{
    public class FindByIdSpecification<T>:Specification<T>
        where T:IEntity
    {
        private readonly Guid _id;
    
        public FindByIdSpecification(Guid id)
        {
            this._id=id;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            return entity => entity.Id.Equals(this._id);
        }
    }

}