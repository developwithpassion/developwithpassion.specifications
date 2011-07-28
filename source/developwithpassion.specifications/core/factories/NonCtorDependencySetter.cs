using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using developwithpassion.specifications.core.reflection;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.faking;

namespace developwithpassion.specifications.core.factories
{
    public class NonCtorDependencySetter : IUpdateNonCtorDependenciesOnAnItem
    {
        IManageTheDependenciesForASUT dependency_registry;

        public Func<object, IMatchAnItem<MemberAccessor>> has_no_value_specification_factory = target =>
            new AccessorHasAValue(target).not();

        static BindingFlags accessor_flags = BindingFlags.Instance | BindingFlags.Public|BindingFlags.DeclaredOnly;

        public NonCtorDependencySetter(IManageTheDependenciesForASUT dependency_registry)
        {
            this.dependency_registry = dependency_registry;
        }

        public void update(object item)
        {
            var has_no_value_specification = has_no_value_specification_factory(item);

            var accessors_to_update = item.GetType().all_accessors(accessor_flags)
                .Where(
                    field =>
                        has_no_value_specification.matches(field) ||
                            dependency_registry.has_been_provided_an(field.accessor_type));

            attempt_to_update_all_of_the_accessors(accessors_to_update,item);
        }

        void attempt_to_update_all_of_the_accessors(IEnumerable<MemberAccessor> accessors_to_update,object target)
        {
            accessors_to_update.each(accessor =>
            {
                accessor.change_value_to(target,BlockThat.ignores_exceptions(() => dependency_registry.get_dependency_of(accessor.accessor_type)));
            });
        }
    }
}