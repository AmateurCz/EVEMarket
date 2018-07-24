using System;

namespace EVEMarket.WPF.Interfaces
{
    public interface NavigationHandler
    {
        void NavigateTo(Type target);
    }
}