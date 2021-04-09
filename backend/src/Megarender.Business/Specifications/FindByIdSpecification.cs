using System;
using System.Linq.Expressions;
using Megarender.Domain;

namespace Megarender.Business.Specifications
{
    public class FindByIdSpecification<T>:LinqSpecification<T>
        where T:IEntity
    {
        private readonly Guid Id;
    
        public FindByIdSpecification(Guid Id)
        {
            this.Id=Id;
        }

        public override Expression<Func<T, bool>> AsExpression()
        {
            return entity => entity.Id.Equals(this.Id);
        }
    }

}