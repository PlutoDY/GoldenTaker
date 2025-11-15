using System;


namespace KKM32.Util.CustomAtrribute
{
    public enum BindingLifetime { Lazy, NonLazy }

    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class BindingLifetimeAttribute : Attribute
    {
        public BindingLifetime LifeTime { get; }

        public BindingLifetimeAttribute(BindingLifetime lifeTime) => LifeTime = lifeTime;
    }

    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class ListenToSignalAttribute : Attribute
    {
        public Type SignalType { get; }

        public ListenToSignalAttribute(Type signalType) => SignalType = signalType;
    }
}