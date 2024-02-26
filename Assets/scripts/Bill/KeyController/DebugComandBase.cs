using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DebugComandBase
{
    private string _commandId;
    private string _commandDescription;
    private string _commandFormat;

    public string commandID { get { return _commandId; } }
    public string commandDescription { get { return _commandDescription; } }
    public string commandFormat { get { return _commandFormat; } }

    public DebugComandBase(string id, string description, string format)
    {
        _commandId = id;
        _commandDescription = description;
        _commandFormat = format;
    }
}

public class DebugCommand : DebugComandBase
{
    private Action command;

    public DebugCommand(string id, string description, string format, Action command) : base (id, description, format)
    {
        this.command = command;
    }

    public void Invoke()
    {
        command.Invoke();
    }
}
