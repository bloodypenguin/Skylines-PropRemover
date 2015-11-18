﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace PropRemover
{
    public static class Util
    {
        public static string GetEnumDescription<T>(this T value)
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }
            var fi = value.GetType().GetField(value.ToString());
            var attributes =
         (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;

            return value.ToString();
        }

        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
    }
}