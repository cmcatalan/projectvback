﻿using ProjectVBack.Crosscutting.Utils.Errors;
using System.Runtime.Serialization;

namespace ProjectVBack.Crosscutting.CustomExceptions;

[Serializable]
public class AppIGetMoneyUserCreationException : AppIGetMoneyException
{
    public AppIGetMoneyUserCreationException() : base(ExceptionMessagesTexts.CategoryCreatingError) { }

    public AppIGetMoneyUserCreationException(string message) : base(message) { }

    public AppIGetMoneyUserCreationException(string? message, Exception? inner) : base(message, inner) { }

    protected AppIGetMoneyUserCreationException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    public override void GetObjectData(SerializationInfo info, StreamingContext context) => base.GetObjectData(info, context);
}

