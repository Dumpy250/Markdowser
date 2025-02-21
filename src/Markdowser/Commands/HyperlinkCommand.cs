﻿using Markdowser.Utilities;

using System;
using System.Diagnostics;
using System.Windows.Input;

namespace Markdowser.Commands;

public class HyperlinkCommand : ICommand
{
#pragma warning disable CS0067 // The event 'HyperlinkCommand.CanExecuteChanged' is never used

    public event EventHandler? CanExecuteChanged;

#pragma warning restore CS0067 // The event 'HyperlinkCommand.CanExecuteChanged' is never used

    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        if (parameter is string url)
        {
            Debug.WriteLine($"Hyperlink clicked: {url}");

            if (Uri.IsWellFormedUriString(url, UriKind.Absolute))
            {
                GlobalState.Url = url;
            }
            else if (Uri.IsWellFormedUriString(url, UriKind.Relative) || url.StartsWith('/'))
            {
                GlobalState.Url = new Uri(new Uri(GlobalState.Url), url).ToString();
            }
            else
            {
                Debug.WriteLine($"Invalid URL: {url}");
            }
        }
    }
}