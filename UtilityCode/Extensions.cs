using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace UtilityCode
{
    public static class IComparableExtensions
    {
        public enum Inclusivity
        {
            Inclusive = 0,
            Exclusive = 1
        }
        public static bool IsBetween<T>(this T Value, T LowerBound, T UpperBound, Inclusivity LowerBoundInclusivity = Inclusivity.Inclusive, Inclusivity UpperBoundInclusivity = Inclusivity.Inclusive) where T : IComparable
        {
            if (!Enum.IsDefined(typeof(Inclusivity), LowerBoundInclusivity))
                throw new ArgumentException("");
            if (!Enum.IsDefined(typeof(Inclusivity), UpperBoundInclusivity))
                throw new ArgumentException("");
            return LowerBound.CompareTo(UpperBound) <= 0 ? Value.IsBefore(UpperBound, UpperBoundInclusivity) && Value.IsAfter(LowerBound, LowerBoundInclusivity) : Value.IsBefore(LowerBound, LowerBoundInclusivity) && Value.IsAfter(UpperBound, UpperBoundInclusivity);
        }

        public static bool IsAfter<T1>(this T1 Value, T1 Pivot, Inclusivity PivotInclusivity = Inclusivity.Inclusive) where T1 : IComparable
        {
            if (!Enum.IsDefined(typeof(Inclusivity), PivotInclusivity))
                throw new ArgumentException("");
            return PivotInclusivity == Inclusivity.Inclusive ? Value.CompareTo(Pivot) >= 0 : Value.CompareTo(Pivot) > 0;
        }

        public static bool IsBefore<T1>(this T1 Value, T1 Pivot, Inclusivity PivotInclusivity = Inclusivity.Inclusive) where T1 : IComparable
        {
            if (!Enum.IsDefined(typeof(Inclusivity), PivotInclusivity))
                throw new ArgumentException("");
            return PivotInclusivity == Inclusivity.Inclusive ? Value.CompareTo(Pivot) <= 0 : Value.CompareTo(Pivot) < 0;
        }

    }
    public static class PropertyExtensions
    {
        /// <summary>
        /// Gets property information for the specified <paramref name="property"/> expression.
        /// </summary>
        /// <typeparam name="TSource">Type of the parameter in the <paramref name="property"/> expression.</typeparam>
        /// <typeparam name="TValue">Type of the property's value.</typeparam>
        /// <param name="property">The expression from which to retrieve the property information.</param>
        /// <returns>Property information for the specified expression.</returns>
        /// <exception cref="ArgumentException">The expression is not understood.</exception>
        public static PropertyInfo GetPropertyInfo<TSource, TValue>(this Expression<Func<TSource, TValue>> property)
        {
            if (property == null)
                throw new ArgumentNullException("property");

            var body = property.Body as MemberExpression;
            if (body == null)
                throw new ArgumentException("Expression is not a property", "property");

            var propertyInfo = body.Member as PropertyInfo;
            if (propertyInfo == null)
                throw new ArgumentException("Expression is not a property", "property");

            return propertyInfo;
        }
    }

    public static class PublicInstancesExtensions
    {
        public static bool PublicInstancePropertiesEqual<T>(this T self, T to, params string[] ignore) where T : class
        {
            if (self != null && to != null)
            {
                var type = typeof(T);
                var ignoreList = new List<string>(ignore);
                var unequalProperties =
                    from pi in type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    where !ignoreList.Contains(pi.Name) && pi.GetUnderlyingType().IsSimpleType() && pi.GetIndexParameters().Length == 0
                    let selfValue = type.GetProperty(pi.Name).GetValue(self, null)
                    let toValue = type.GetProperty(pi.Name).GetValue(to, null)
                    where selfValue != toValue && (selfValue == null || !selfValue.Equals(toValue))
                    select selfValue;
                return !unequalProperties.Any();
            }
            return self == to;
        }


    }

    public static class UshortExtensions
    {
        public static ushort SetByteValue(this ushort target, int targetByte, byte value)
        {
            if (!targetByte.IsBetween(0, 1)) { throw new ArgumentException("targetByte must be 1 or 0"); }
            var byteArr = BitConverter.GetBytes(target);
            byteArr[targetByte] = value;
            return BitConverter.ToUInt16(byteArr, 0);
        }

        public static ushort SetBytes(this ushort target, byte firstByteValue, byte secondByteValue)
        {
            var byteArr = BitConverter.GetBytes(target);
            byteArr[0] = firstByteValue;
            byteArr[1] = secondByteValue;
            return BitConverter.ToUInt16(byteArr, 0);
        }
        public static bool GetBit(this ushort value, int pos)
        {
            if (pos > 15) throw new ArgumentException($"bit position {pos} is out of range.");
            return (value & (1 << pos)) > 0;
        }

        public static int GetBitValue(this ushort value, int pos)
        {
            if (pos > 15) throw new ArgumentException($"bit position {pos} is out of range.");
            return GetBit(value, pos) == true ? 1 : 0;

        }

        public static ushort SetBit(this ushort value, int pos, bool newState)
        {
            if ((pos > 15))
                throw new ArgumentOutOfRangeException("Pos", pos, "The value should range from 0 to 7");
            var num = (1 << pos);
            var num2 = (num | value);
            num2 = (num2 - num);
            num = ((newState) ? 1 : 0) << pos;
            return Convert.ToUInt16(num2 + num);
        }

        public static byte[] ToBytes(this ushort value)
        {
            return BitConverter.GetBytes(value);
        }

        public static byte Byte(this ushort value, int pos)
        {
            if (!pos.IsBetween(0, 1)) { throw new ArgumentException("targetByte must be 1 or 0"); }

            return value.ToBytes()[pos];

        }


    }

    public static class TaskExtensions
    {
        public static async Task<TResult> TimeoutAfter<TResult>(this Task<TResult> task, TimeSpan timeout)
        {

            using (var timeoutCancellationTokenSource = new CancellationTokenSource())
            {

                var completedTask = await TaskEx.WhenAny(task, TaskEx.Delay(timeout, timeoutCancellationTokenSource.Token));
                if (completedTask == task)
                {
                    timeoutCancellationTokenSource.Cancel();
                    return await task;  // Very important in order to propagate exceptions
                }
                else
                {
                    throw new TimeoutException("The operation has timed out.");
                }
            }
        }
    }

    public static class LiteExtensions
    {

        //public static void Upsert<T>(this LiteCollection<T> collection, T value) where T : new()
        //{
        //    if (!collection.Update(value))
        //        collection.Insert(value);
        //}

        //public static void Upsert<T>(this LiteCollection<T> collection, T value, Func<T, BsonValue> keySelector) where T : new()
        //{
        //    if (!collection.Update(keySelector(value), value))
        //        collection.Insert(value);
        //}
    }

    public static class TypeExtensions
    {
        /// <summary>
        /// Determine whether a type is simple (String, Decimal, DateTime, etc) 
        /// or complex (i.e. custom class with public properties and methods).
        /// </summary>
        /// <see cref="http://stackoverflow.com/questions/2442534/how-to-test-if-type-is-primitive"/>
        public static bool IsSimpleType(
           this Type type)
        {
            return
               type.IsValueType ||
               type.IsPrimitive ||
               new[]
               {
               typeof(String),
               typeof(Decimal),
               typeof(DateTime),
               typeof(DateTimeOffset),
               typeof(TimeSpan),
               typeof(Guid)
               }.Contains(type) ||
               (Convert.GetTypeCode(type) != TypeCode.Object);
        }

        public static Type GetUnderlyingType(this MemberInfo member)
        {
            switch (member.MemberType)
            {
                case MemberTypes.Event:
                    return ((EventInfo)member).EventHandlerType;
                case MemberTypes.Field:
                    return ((FieldInfo)member).FieldType;
                case MemberTypes.Method:
                    return ((MethodInfo)member).ReturnType;
                case MemberTypes.Property:
                    return ((PropertyInfo)member).PropertyType;
                default:
                    throw new ArgumentException
                    (
                       "Input MemberInfo must be if type EventInfo, FieldInfo, MethodInfo, or PropertyInfo"
                    );
            }
        }
    }


    public static class ByteExtensions
    {

        public static bool GetBitBoolValue(this Byte value, int pos)
        {
            if (pos > 7) throw new ArgumentException($"bit position {pos} is out of range.");
            return (value & (1 << pos)) > 0;
        }

        public static int GetBitValue(this Byte value, int pos)
        {
            if (pos > 7) throw new ArgumentException($"bit position {pos} is out of range.");
            return GetBitBoolValue(value, pos) == true ? 1 : 0;

        }

        public static byte SetBit(this byte value, int pos, bool newState)
        {
            if (pos > 7) throw new ArgumentException($"bit position {pos} is out of range.");
            var num = 1 << pos;
            num = newState ? 1 : 0 << pos;
            return Convert.ToByte((num | value - num) + num);
        }


    }

    public static class IntExtensions
    {
        public static bool GetBitBoolValue(this int value, int pos)
        {
            if (pos > 7) throw new ArgumentException($"bit position {pos} is out of range.");
            return (value & (1 << pos)) > 0;
        }

        public static int GetBitValue(this int value, int pos)
        {
            if (pos > 7) throw new ArgumentException($"bit position {pos} is out of range.");
            return GetBitBoolValue(value, pos) == true ? 1 : 0;

        }

        public static int SetBit(this int value, int pos, bool newState)
        {
            if (pos > 7) throw new ArgumentException($"bit position {pos} is out of range.");
            var num = 1 << pos;
            num = newState ? 1 : 0 << pos;
            return (num | value - num) + num;
        }
    }

    public static class StringExtensions
    {
        /// <summary>
        /// Returns the first few characters of the string with a length
        /// specified by the given parameter. If the string's length is less than the 
        /// given length the complete string is returned. If length is zero or 
        /// less an empty string is returned
        /// </summary>
        /// <param name="s">the string to process</param>
        /// <param name="length">Number of characters to return</param>
        /// <returns></returns>
        public static string Left(this string s, int length)
        {
            length = Math.Max(length, 0);

            if (s.Length > length)
            {
                return s.Substring(0, length);
            }
            else
            {
                return s;
            }


        }

        /// <summary>
        /// Returns the last few characters of the string with a length
        /// specified by the given parameter. If the string's length is less than the 
        /// given length the complete string is returned. If length is zero or 
        /// less an empty string is returned
        /// </summary>
        /// <param name="s">the string to process</param>
        /// <param name="length">Number of characters to return</param>
        /// <returns></returns>
        public static string Right(this string s, int length)
        {
            length = Math.Max(length, 0);

            if (s.Length > length)
            {
                return s.Substring(s.Length - length, length);
            }
            else
            {
                return s;
            }
        }

        public static bool IsNumeric(this string theValue)
        {
            long retNum;
            return long.TryParse(theValue, System.Globalization.NumberStyles.Integer,
                System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
        }

        public static StringBuilder AppendLineFormat(this StringBuilder builder, string format, params object[] args)
        {
            string value = string.Format(format, args);

            builder.AppendLine(value);

            return builder;
        }
    }
}