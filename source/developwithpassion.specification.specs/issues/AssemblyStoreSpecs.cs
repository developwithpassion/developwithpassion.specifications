using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Machine.Specifications;
using developwithpassion.specifications.rhinomocks;

namespace developwithpassion.specification.specs.issues
{
    public interface IAssemblyStore
    {
        void Add(Assembly the_assembly);
        void AddAllAssemblies(IEnumerable<Assembly> assemblies);
        IEnumerable<Type> AllTypes();
    }

    public class AssemblyStore : List<Assembly>, IAssemblyStore
    {
        public void AddAllAssemblies(IEnumerable<Assembly> assemblies)
        {
            AddRange(assemblies);
        }

        public IEnumerable<Type> AllTypes()
        {
            return this.SelectMany(assembly => assembly.GetTypes());
        }
    }

    [Subject(typeof(AssemblyStore))]
    public class AssemblyStoreSpecs
    {
        public abstract class concern : Observes<IAssemblyStore,
                                            AssemblyStore>
        {
        }

        public class when_an_assembly_is_registered : concern
        {
            Establish c = () =>
            {
                the_assembly = typeof(int).Assembly;
            };

            Because b = () =>
                sut.Add(the_assembly);

            It should_store_in_in_the_list_of_all_assemblies_to_check = () =>
                concrete_sut.Contains(the_assembly);

            static Assembly the_assembly;
        }

        public class when_a_set_of_assemblies_are_added : concern
        {
            Establish c = () =>
            {
                items = AppDomain.CurrentDomain.GetAssemblies().Take(3);
                first = items.First();
                second = items.Skip(1).First();
                third = items.Skip(2).First();
            };

            Because b = () =>
                sut.AddAllAssemblies(items);

            It they_should_all_be_stored_in_the_underlying_list = () =>
                concrete_sut.ShouldContain(first, second, third);

            static Assembly first;
            static Assembly second;
            static Assembly third;
            static IEnumerable<Assembly> items;
        }

        public class concern_for_an_assembly_store_with_assemblies_added : Observes<IAssemblyStore, AssemblyStore>
        {
            Establish c = () =>
            {
                assemblies = AppDomain.CurrentDomain.GetAssemblies().Take(3);
                sut_setup.run(x => x.AddAllAssemblies(assemblies));
            };

            protected static IEnumerable<Assembly> assemblies;
        }

        public class when_requesting_all_of_the_types : concern_for_an_assembly_store_with_assemblies_added
        {
            Establish c = () =>
            {
                all_type_count = assemblies.Sum(x => x.GetTypes().Count());
            };

            Because b = () =>
                results = sut.AllTypes();

            It should_return_the_set_of_types_from_any_of_the_assemblies_loaded = () =>
                results.Count().ShouldEqual(all_type_count);

            static IEnumerable<Type> results;
            static int all_type_count;
        }
    }
}