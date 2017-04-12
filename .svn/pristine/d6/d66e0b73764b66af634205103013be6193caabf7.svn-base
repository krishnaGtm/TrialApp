using System;
using System.Collections.Generic;
using TrialApp.ViewModels.Inetrfaces;

namespace TrialAppLT.UnitTest
{
    public class DependencyServiceStub : IDependencyService
    {
        private readonly Dictionary<Type, object> registeredServices = new Dictionary<Type, object>();

        public void Register<T>(object impl)
        {
            this.registeredServices[typeof(T)] = impl;
        }

        public T Get<T>() where T : class
        {
            return (T)registeredServices[typeof(T)];
        }

        public void Register<T>(T impl)
        {
            throw new NotImplementedException();
        }
    }
}
