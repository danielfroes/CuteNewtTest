using UnityEngine;
using System;

namespace CuteNewtTest.Tools
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
    public class SerializeReferenceMenuAttribute : PropertyAttribute
    {

    }

}