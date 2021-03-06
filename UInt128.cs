﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Underlink
{
    public struct UInt128
    {
        public UInt64 Big;
        public UInt64 Small;

        public UInt128(UInt64 Big, UInt64 Small)
        {
            this.Big = (ulong) System.Net.IPAddress.HostToNetworkOrder((long) Big);
            this.Small = (ulong) System.Net.IPAddress.HostToNetworkOrder((long) Small);
        }

        public UInt128(byte[] ByteArray)
        {
            this.Big = BitConverter.ToUInt64(ByteArray, 0);
            this.Small = BitConverter.ToUInt64(ByteArray, 8);
        }

        public override bool Equals(object Obj)
        {
 	        return ((UInt128) Obj).Big == this.Big &&
                   ((UInt128) Obj).Small == this.Small;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public bool MaskEquals(UInt128 Obj, UInt128 Mask)
        {
            return (this.Small & Mask.Small) == (Obj.Small & Mask.Small) &&
                   (this.Big & Mask.Big) == (Obj.Big & Mask.Big);
        }

        public static bool operator ==(UInt128 Left, UInt128 Right)
        {
            return Left.Equals(Right);
        }

        public static bool operator !=(UInt128 Left, UInt128 Right)
        {
            return !Left.Equals(Right);
        }

        public bool GreaterThan(UInt128 Obj)
        {
            return this.Big > Obj.Big ||
                   (this.Big == Obj.Big && this.Small > Obj.Small);
        }

        public static bool operator >(UInt128 Left, UInt128 Right)
        {
            return Left.GreaterThan(Right);
        }

        public static bool operator >=(UInt128 Left, UInt128 Right)
        {
            return Left.GreaterThan(Right) || Left.Equals(Right);
        }

        public bool LessThan(UInt128 Obj)
        {
            return this.Big < Obj.Big ||
                   (this.Big == Obj.Big && this.Small < Obj.Small);
        }

        public static bool operator <(UInt128 Left, UInt128 Right)
        {
            return Left.LessThan(Right);
        }

        public static bool operator <=(UInt128 Left, UInt128 Right)
        {
            return Left.GreaterThan(Right) || Left.Equals(Right);
        }

        public UInt128 Xor(UInt128 Obj)
        {
            UInt128 ReturnValue;

            ReturnValue.Big = this.Big ^ Obj.Big;
            ReturnValue.Small = this.Small ^ Obj.Small;

            return ReturnValue;
        }

        public static UInt128 operator ^(UInt128 Left, UInt128 Right)
        {
            return Left.Xor(Right);
        }

        public bool IsZero()
        {
            return this.Big == 0 && this.Small == 0;
        }

        public String ToHexString()
        {
            return String.Format("{0:X4}", System.Net.IPAddress.NetworkToHostOrder((long) this.Big)) + 
                   String.Format("{0:X4}", System.Net.IPAddress.NetworkToHostOrder((long) this.Small));
        }
    }
}
