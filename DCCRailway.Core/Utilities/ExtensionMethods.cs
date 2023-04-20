using System;
using System.Linq;
using System.Text;

namespace DCCRailway.Core.Utilities; 

public static class ExtensionMethods {
    public static byte Invert(this byte inByte) {
        return (byte) ~inByte;
    }

    /// <summary>
    ///     Allows easy setting of the bits in a 8-bit byte
    /// </summary>
    /// <param name="pos">The bit position 1...8</param>
    /// <param name="value">A True (on) or False (off) value to set</param>
    /// <returns></returns>
    public static byte SetBit(this byte bits, int pos, bool value) {
        if (pos < 0 || pos > 7) throw new IndexOutOfRangeException("Bit position should be 0..7");
        if (value) //left-shift 1, then bitwise OR
            bits = (byte) (bits | (1 << pos));
        else //left-shift 1, then take complement, then bitwise AND
            bits = (byte) (bits & ~(1 << pos));
        return bits;
    }

    public static string FormatBits(this byte bits) {
        StringBuilder sb = new();
        for (var i = 1; i < 8; i++) {
            sb.Append(bits.GetBit(i) ? "1" : "0");
            sb.Append('-');
        }

        sb.Append(bits.GetBit(8) ? "1" : "0");
        return sb.ToString();
    }

    /// <summary>
    ///     Gets f a bit is set in a byte based on a position
    /// </summary>
    public static bool GetBit(this byte bits, int pos) {
        if (pos < 0 || pos > 7) throw new IndexOutOfRangeException("Bit position should be 1..8");

        //left-shift 1, then bitwise AND, then check for non-zero
        return (bits & (1 << pos)) != 0;
    }

    /// <summary>
    ///     Adds a new byte to an existing byte array returning a new array
    /// </summary>
    /// <param name="bytes">The existing array to add new data to</param>
    /// <param name="newByte">The byte to add to the existing array</param>
    /// <param name="addAtEnd">True to add the data at the end of the array, False at the beginning</param>
    /// <returns>A new array</returns>
    public static byte[] AddToArray(this byte[] bytes, byte newByte, bool addAtEnd = true) {
        var newArray = new byte[bytes.Length + 1];
        bytes.CopyTo(newArray, addAtEnd ? 0 : 1);
        newArray[addAtEnd ? bytes.Length : 0] = newByte;
        return newArray;
    }

    public static byte[] AddToArray(this byte[] bytes, byte[] newByte, bool addAtEnd = true) {
        var newArray = new byte[bytes.Length + newByte.Length];
        bytes.CopyTo(newArray, addAtEnd ? 0 : bytes.Length);
        newByte.CopyTo(newArray, addAtEnd ? bytes.Length : 0);
        return newArray;
    }

    public static byte[] ToByteArray(this int value) {
        var bytes = new byte[2];
        bytes[0] = (byte) (value >> 8); // Take the 2nd order bits
        bytes[1] = (byte) value; // Take the low order bits
        return bytes;
    }

    public static byte[] ToByteArray(this string value) {
        return Encoding.Default.GetBytes(value);
    }

    public static string ToDisplayValues(this byte[]? bytes) {
        if (bytes == null || bytes.Length == 0) return "<empty>";
        StringBuilder sb = new();
        for (var i = 0; i < bytes.Length; i++) {
            sb.Append(Convert.ToHexString(bytes, i, 1));
            if (i < bytes.Length - 1) sb.Append('-');
        }

        return sb.ToString();
    }

    public static string ToDisplayChars(this byte[]? bytes) {
        if (bytes == null || bytes.Length == 0) return "<empty>";
        StringBuilder sb = new();
        for (var i = 0; i < bytes.Length; i++) {
            sb.Append(Convert.ToChar(bytes[i]));
            if (i < bytes.Length - 1) sb.Append('-');
        }

        return sb.ToString();
    }

    public static string ToDisplayValueChars(this byte[]? bytes) {
        StringBuilder sb = new();
        sb.Append(bytes.ToDisplayValues());
        sb.Append(" (");
        sb.Append(bytes.ToDisplayChars());
        sb.Append(')');
        return sb.ToString();
    }

    public static string FromByteArray(this byte[]? bytes) {
        return bytes == null ? "" : Encoding.Default.GetString(bytes);
    }

    public static string? FromByteArray(this byte[] bytes, int length) {
        if (bytes != null && bytes.Length >= length) return Encoding.Default.GetString(bytes, 0, length);
        return null;
    }

    /// <summary>
    ///     Simple byte array comparison extension
    /// </summary>
    /// <param name="array1">Source array</param>
    /// <param name="array2">Compariative array</param>
    /// <returns>True if the two arays are equal</returns>
    public static bool Compare(this byte[]? array1, byte[]? array2) {
        if (array1 == null || array2 == null) return false;
        if (array1.Length != array2.Length) return false;
        return !array1.Where((t, i) => t != array2[i]).Any();
    }
}