using System;

namespace Common
{
    public interface IDisposeNotify
    {
        event Action OnDisposeEvent;
    }
}