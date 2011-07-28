using System;
using System.Linq;
using developwithpassion.specifications.core.reflection;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.faking;

namespace developwithpassion.specifications.core.factories
{
    public class NonCtorDependencySetter : IUpdateNonCtorDependenciesOnAnItem
    {
        IManageTheDependenciesForASUT dependency_registry;

        public Func<object, IMatchAnItem<MemberAccessor>> has_no_value_set_match_factory = target =>
            new AccessorHasAValue(target).not();

        public NonCtorDependencySetter(IManageTheDependenciesForASUT dependency_registry)
        {
            this.dependency_registry = dependency_registry;
        }

        public void update(object item)
        {
            var has_no_value_specification = has_no_value_set_match_factory(item);
            item.GetType().all_instance_accessors()
                .Where(field => has_no_value_specification.matches(field) || dependency_registry.has_been_provided_an(field.accessor_type))
                .each(field => field.change_value_to(item, dependency_registry.get_dependency_of(field.accessor_type)));
        }
    }
}