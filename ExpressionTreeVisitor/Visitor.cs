﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionTreeVisitor
{
    public class Visitor<From, To> : ExpressionVisitor
    {
        public ParameterExpression NewParameterExp { get; private set; }

        public Visitor(ParameterExpression newParameterExp)
        {
            NewParameterExp = newParameterExp;
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            return NewParameterExp;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            
            try
            {
                if (node.Member.DeclaringType == typeof(From))
                    return Expression.MakeMemberAccess(this.Visit(node.Expression),
                       typeof(To).GetMember(node.Member.Name).FirstOrDefault());
                return base.VisitMember(node);
            }
            catch { return base.VisitMember(node); }
            
        }
    }
}
