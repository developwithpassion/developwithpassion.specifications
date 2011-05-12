using System;
using System.Linq.Expressions;
using System.Reflection;
using developwithpassion.specifications.extensions;

namespace developwithpassion.specifications.dsl.fieldswitching
{
  public class FieldReassignmentStartExpression
  {
    FieldSwitcherFactory field_switcher_factory;

    public FieldReassignmentStartExpression() : this(new DefaultFieldSwitcherFactory())
    {
    }

    public FieldReassignmentStartExpression(FieldSwitcherFactory field_switcher_factory)
    {
      this.field_switcher_factory = field_switcher_factory;
    }

    public ISwapValues change(Expression<Func<object>> member_expression)
    {
      return this.field_switcher_factory.create_to_target(this.get_member_from(member_expression));
    }

    MemberInfo get_member_from(Expression<Func<object>> expression)
    {
      if (expression.Body.NodeType == ExpressionType.Convert)
      {
        return expression.Body.downcast_to<UnaryExpression>().Operand.downcast_to<MemberExpression>().Member;
      }
      return expression.Body.downcast_to<MemberExpression>().Member;
    }
  }
}