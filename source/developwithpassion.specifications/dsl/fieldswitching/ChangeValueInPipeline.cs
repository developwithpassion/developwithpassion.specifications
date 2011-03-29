using System;
using System.Linq.Expressions;

namespace developwithpassion.specifications.dsl.fieldswitching
{
public class ChangeValueInPipeline
{
    // Fields
    private Action<SetupTearDownPair> add_behaviour;
    private Expression<Func<object>> member_expression;

    // Methods
    public ChangeValueInPipeline(Action<SetupTearDownPair> add_behaviour, Expression<Func<object>> member_expression)
    {
        this.add_behaviour = add_behaviour;
        this.member_expression = member_expression;
    }

    public void to(object new_value)
    {
        this.add_behaviour(new FieldReassignmentStartExpression().change(this.member_expression).to(new_value));
    }
}

 
}