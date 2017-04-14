﻿namespace House.ViewModels
{
    public interface INotifyScrollToProperty
    {
        event ScrollToPropertyHandler ScrollToProperty;
    }
    public delegate void ScrollToPropertyHandler(string PropertyName);
}