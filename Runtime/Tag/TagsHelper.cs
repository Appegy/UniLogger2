﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UnityEngine
{
    internal static class TagsHelper
    {
        private static readonly Dictionary<Type, string> _tagTypesCache = new Dictionary<Type, string>();
        private static readonly Dictionary<Enum, string> _tagEnumsCache = new Dictionary<Enum, string>();

        public static IEnumerable<T> AsEnumerable<T>(this T item)
        {
            yield return item;
        }

        public static string GetTag(this object tag)
        {
            return tag switch
            {
                Enum enumTag => enumTag.GetTag(),
                Type typeTag => typeTag.GetTag(),
                string stringTag => stringTag,
                _ => tag.ToString()
            };
        }

        public static string GetTag(this Enum value)
        {
            if (!_tagEnumsCache.TryGetValue(value, out var name))
            {
                var attributes = (LoggerTagNameAttribute[])value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(LoggerTagNameAttribute), false);
                name = attributes.Length > 0 ? attributes[0].Name : value.ToString();
                _tagEnumsCache[value] = name;
            }
            return name;
        }

        public static string GetTag(this Type value)
        {
            if (!_tagTypesCache.TryGetValue(value, out var name))
            {
                var attributes = (LoggerTagNameAttribute[])value.GetCustomAttributes(typeof(LoggerTagNameAttribute), false);
                name = attributes.Length > 0 ? attributes[0].Name : value.ToString();
                _tagTypesCache[value] = name;
            }
            return name;
        }
    }
}