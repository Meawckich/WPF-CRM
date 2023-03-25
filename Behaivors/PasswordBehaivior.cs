﻿using System.Windows;
using System.Windows.Controls;
using Microsoft.Xaml.Behaviors;

namespace TestApplication.Behavior;

public class PasswordBehavior : Behavior<PasswordBox>
{
    public static readonly DependencyProperty PasswordProperty =
        DependencyProperty.Register("Password", typeof(string), typeof(PasswordBehavior), new PropertyMetadata(default(string)));

    private bool _skipUpdate;

    public string Password
    {
        get => (string)GetValue(PasswordProperty);
        set => SetValue(PasswordProperty, value);
    }

    protected override void OnAttached()
    {
        AssociatedObject.PasswordChanged += PasswordBox_PasswordChanged;
    }

    protected override void OnDetaching()
    {
        AssociatedObject.PasswordChanged -= PasswordBox_PasswordChanged;
    }

    protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);

        if (e.Property != PasswordProperty) return;
        if (_skipUpdate) return;

        _skipUpdate = true;
        AssociatedObject.Password = e.NewValue as string ?? string.Empty;
        _skipUpdate = false;
    }

    private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
    {
        _skipUpdate = true;
        Password = AssociatedObject.Password;
        _skipUpdate = false;
    }
}