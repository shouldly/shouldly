// ****************************************************************
// Copyright 2011, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Windows.Forms;

namespace NUnit.UiKit
{
    public interface IMessageDisplay
    {
        DialogResult Display(string message);
        DialogResult Display(string message, MessageBoxButtons buttons);

        DialogResult Error(string message);
        DialogResult Error(string message, MessageBoxButtons buttons);
        DialogResult Error(string message, Exception exception);
        DialogResult Error(string message, Exception exception, MessageBoxButtons buttons);

        DialogResult FatalError(string message, Exception exception);

        DialogResult Info(string message);
        DialogResult Info(string message, MessageBoxButtons buttons);

        DialogResult Ask(string message);
        DialogResult Ask(string message, MessageBoxButtons buttons);
    }
}
