using System;
using System.Linq.Expressions;
using System.Reflection;
using Xamarin.Forms;

namespace Imobi.Validations
{
    public abstract class ExtendedBindableObject : BindableObject
    {
        #region Public Methods

        public void RaisePropertyChanged<T>(Expression<Func<T>> property)
        {
            var name = GetMemberInfo(property).Name;
            OnPropertyChanged(name);
        }

        #endregion Public Methods



        #region Private Methods

        private MemberInfo GetMemberInfo(Expression expression)
        {
            MemberExpression operand;
            LambdaExpression lambdaExpression = (LambdaExpression)expression;
            if (lambdaExpression.Body as UnaryExpression != null)
            {
                UnaryExpression body = (UnaryExpression)lambdaExpression.Body;
                operand = (MemberExpression)body.Operand;
            }
            else
            {
                operand = (MemberExpression)lambdaExpression.Body;
            }
            return operand.Member;
        }

        #endregion Private Methods
    }
}