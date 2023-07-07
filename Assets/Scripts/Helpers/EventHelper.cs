using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Generic function to get enum full name
public static class EventHelper
{
    public static string GetFullName<T>(T enumValue) where T : Enum
    {
        return $"{enumValue.GetType().Name}.{Enum.GetName(typeof(T), enumValue)}";
    }
}
