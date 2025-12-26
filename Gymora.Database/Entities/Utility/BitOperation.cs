using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Gymora.Database.Entities.Utility
{
    [Owned]
    public class BitOperation<TUnit, TUnitValueType>
    where TUnit : Enum
    where TUnitValueType : struct,
    IBinaryNumber<TUnitValueType>,
    IBitwiseOperators<TUnitValueType, TUnitValueType, TUnitValueType>
    {
        #region Properties

        /// <summary>
        ///     sum of values
        /// </summary>
        public TUnitValueType Value { get; private set; }

        #endregion

        #region Public Methods

        /// <summary>
        ///     initial constructor
        /// </summary>
        /// <param name="initValue"></param>
        public BitOperation(TUnitValueType initValue)
        {
            Value = initValue;
        }

        /// <summary>
        ///     initial constructor
        /// </summary>
        /// <param name="initValue"></param>
        public BitOperation(TUnit initValue)
        {
            Value = Cast(initValue);
        }

        /// <summary>
        ///     initial constructor
        /// </summary>
        /// <param name="initValues"></param>
        public BitOperation(IEnumerable<TUnit> initValues)
        {
            foreach (var initValue in initValues)
            {
                Value |= Cast(initValue);
            }
        }

        /// <summary>
        ///     empty constructor
        /// </summary>
        public BitOperation()
        {
        }

        /// <summary>
        ///     make unit bits 1
        /// </summary>
        /// <param name="unit"></param>
        public BitOperation<TUnit, TUnitValueType> Add(TUnit unit)
        {
            Value |= Cast(unit);

            return this;
        }

        /// <summary>
        ///     make unit bit 0
        /// </summary>
        /// <param name="unit"></param>
        public BitOperation<TUnit, TUnitValueType> Remove(TUnit unit)
        {
            Value &= ~Cast(unit);

            return this;
        }

        /// <summary>
        ///     read each bit of value and cast to TUnit
        /// </summary>
        /// <returns></returns>
        public IList<TUnit> ToList()
        {
            return SeparateBinaries()
                .Select(x => (TUnit)(object)x).ToList();
        }

        /// <summary>
        ///     contains all unit
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public bool Contains(TUnit unit)
        {
            return (Value & Cast(unit)) == Cast(unit);
        }

        /// <summary>
        ///     contains all value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Contains(TUnitValueType value)
        {
            return (Value & value) == value;
        }

        /// <summary>
        ///     has a binary unit
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public bool Any(TUnit unit)
        {
            return (Value & Cast(unit)) > TUnitValueType.Zero;
        }

        /// <summary>
        ///     has a binary value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Any(TUnitValueType value)
        {
            return (Value & value) > TUnitValueType.Zero;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bitOperation"></param>
        /// <returns></returns>
        public IEnumerable<TUnit> Intersect(BitOperation<TUnit, TUnitValueType> bitOperation)
        {
            var result = Value & bitOperation.Value;

            return SeparateBinaries(result)
                .Select(x => (TUnit)(object)x).ToList();
        }

        /// <summary>
        ///     return list binaries
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TUnitValueType> SeparateBinaries()
        {
            var value = (long)Convert.ChangeType(Value, typeof(long));

            while (value > 0)
            {
                var maxValuedIndex = (int)Math.Log2(value);
                var maxValuedBit = (long)Math.Pow(2, maxValuedIndex);

                yield return (TUnitValueType)Convert.ChangeType(maxValuedBit, typeof(TUnitValueType));

                value ^= maxValuedBit;
            }
        }

        /// <summary>
        ///     return list binaries
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<TUnitValueType> SeparateBinaries(TUnitValueType number)
        {
            var value = (long)Convert.ChangeType(number, typeof(long));

            while (value > 0)
            {
                var maxValuedIndex = (int)Math.Log2(value);
                var maxValuedBit = (long)Math.Pow(2, maxValuedIndex);

                yield return (TUnitValueType)Convert.ChangeType(maxValuedBit, typeof(TUnitValueType));

                value ^= maxValuedBit;
            }
        }

        #endregion

        #region Private Methods

        private TUnitValueType Cast(TUnit unit)
        {
            return (TUnitValueType)Convert.ChangeType(unit, typeof(TUnitValueType));
        }

        #endregion

        #region Operators

        /// <summary>
        ///     implicit for bitOperation value with TUnitValueType
        /// </summary>
        /// <param name="bitOperation"></param>
        /// <returns></returns>
        public static implicit operator TUnitValueType(BitOperation<TUnit, TUnitValueType> bitOperation) =>
            bitOperation.Value;

        /// <summary>
        ///     explicit for set bitOperation value with TUnitValueType
        /// </summary>
        /// <param name="unitValue"></param>
        /// <returns></returns>
        public static implicit operator BitOperation<TUnit, TUnitValueType>(TUnitValueType unitValue) => new(unitValue);

        /// <summary>
        ///     explicit for set bitOperation value with TUnit
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public static implicit operator BitOperation<TUnit, TUnitValueType>(TUnit unit) => new(unit);

        /// <summary>
        ///     explicit for set bitOperation value with list of TUnit
        /// </summary>
        /// <param name="units"></param>
        /// <returns></returns>
        public static implicit operator BitOperation<TUnit, TUnitValueType>(TUnit[] units) => new(units);

        /// <summary>
        ///     operator + for add a value
        /// </summary>
        /// <param name="bitOperation"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static BitOperation<TUnit, TUnitValueType> operator +(BitOperation<TUnit, TUnitValueType> bitOperation,
            TUnit value)
            => bitOperation.Add(value);

        /// <summary>
        ///     operator - for remove a value
        /// </summary>
        /// <param name="bitOperation"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static BitOperation<TUnit, TUnitValueType> operator -(BitOperation<TUnit, TUnitValueType> bitOperation,
            TUnit value)
            => bitOperation.Remove(value);

        #endregion
    }
}
