using System;
using System.Linq.Expressions;

namespace Megarender.Business.Specifications
{
    internal class ParameterReplacer : ExpressionVisitor {

        private readonly ParameterExpression _parameter;

        protected override Expression VisitParameter(ParameterExpression node) 
            => base.VisitParameter(_parameter);

        internal ParameterReplacer(ParameterExpression parameter) {
            _parameter = parameter;
        }
    }

    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> ToExpression();
        bool IsSatisfiedBy(T entity);
        Specification<T> And(Specification<T> specification);
        Specification<T> Or(Specification<T> specification);
    }

    public abstract class Specification<T> : ISpecification<T>
    {
        public abstract Expression<Func<T, bool>> ToExpression();
        public bool IsSatisfiedBy(T entity) => ToExpression().Compile()(entity);
        public Specification<T> And(Specification<T> specification) => new AndSpecification<T>(this, specification);
        public Specification<T> Or(Specification<T> specification) => new OrSpecification<T>(this, specification);
    }


    public class AndSpecification<T> : Specification<T> 
    {
        private readonly Specification<T> _left;
        private readonly Specification<T> _right;
        
        public AndSpecification(Specification<T> left, Specification<T> right) {
            _right = right;
            _left = left;
        }
        public override Expression<Func<T, bool>> ToExpression() {
            var paramExpr = Expression.Parameter(typeof(T));
            return Expression.Lambda<Func<T, bool>>((BinaryExpression)new ParameterReplacer(paramExpr).Visit(Expression.AndAlso(_left.ToExpression().Body, _right.ToExpression().Body)), paramExpr);
        }
    }


    public class OrSpecification<T> : Specification<T> 
    {
        private readonly Specification<T> _left;
        private readonly Specification<T> _right;


        public OrSpecification(Specification<T> left, Specification<T> right) {
            _right = right;
            _left = left;
        }


        public override Expression<Func<T, bool>> ToExpression() {
            var paramExpr = Expression.Parameter(typeof(T));
            return Expression.Lambda<Func<T, bool>>((BinaryExpression)new ParameterReplacer(paramExpr).Visit(Expression.OrElse(_left.ToExpression().Body, _right.ToExpression().Body)), paramExpr);
        }
    }
}