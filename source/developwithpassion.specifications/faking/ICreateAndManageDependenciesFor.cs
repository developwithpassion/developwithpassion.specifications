using developwithpassion.specifications.core;

namespace developwithpassion.specifications.faking
{
    public interface ICreateAndManageDependenciesFor<Class> : SUTFactory<Class>,
                                                                    IProvideDependencies,ICreateThe<Class>
    {
        
    }
}