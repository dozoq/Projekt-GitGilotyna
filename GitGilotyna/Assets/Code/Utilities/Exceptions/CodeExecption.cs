using System;
using UnityEngine;

namespace Code.Utilities
{
    
    /// <summary>
    /// End-user exception, should be used to hide detailed message from user
    /// Read the docs for detailed information about error code
    /// </summary>
    public abstract class CodeExecption : Exception
    {
        protected CodeExecption(int code) : base($"Error Occured: {code}")
        {
        }
    }
}