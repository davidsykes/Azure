﻿namespace SQLiteLibrary.Attributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class NonPersistentAttribute : Attribute
    {
    }
}
