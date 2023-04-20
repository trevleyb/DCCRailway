using System;
using DCCRailway.Core.Utilities;

namespace DCCRailway.Core.Systems.Types; 

/// <summary>
///     A function block represents a set of bytes used to manage the functions to be sent to a loco
///     to control lights, sounds etc
/// </summary>
public class DCCFunctionBlocks {
    // Value Group Block #1 - Byte, bits 0..3: represents F1..F4, F0 
    // Value Group Block #2 - Byte, bits 0..3: represents F5..F8
    // Value Group Block #3 - Byte, bits 0..3: represents F9..F12
    // Value Group Block #4 - Byte, bits 0..7: represents F13...F20
    // Value Group Block #5 - Byte, bits 0..7: represents F21...F28 

    private readonly byte[] _block = new byte[5];

    /// <summary>
    ///     Constructor with an empty (all off) DCC Functions block
    /// </summary>
    public DCCFunctionBlocks() {
        for (var i = 0; i <= 28; i++) this[i] = false;
    }

    /// <summary>
    ///     Constructor to take a copy of an existing DCCFunctions block
    /// </summary>
    /// <param name="functions"></param>
    public DCCFunctionBlocks(DCCFunctionBlocks functions) {
        for (var i = 0; i <= 28; i++) this[i] = functions[i];
    }

    /// <summary>
    ///     Constructor that take a 5-byte array of on/off bool function switches
    /// </summary>
    /// <param name="functions"></param>
    public DCCFunctionBlocks(bool[] functions) {
        _block[0] = _block[0].SetBit(0, functions[1]); // F1
        _block[0] = _block[0].SetBit(1, functions[2]); // F2
        _block[0] = _block[0].SetBit(2, functions[3]); // F3
        _block[0] = _block[0].SetBit(3, functions[4]); // F4
        _block[0] = _block[0].SetBit(4, functions[0]); // F0

        _block[1] = _block[1].SetBit(0, functions[5]); // F5
        _block[1] = _block[1].SetBit(1, functions[6]); // F6
        _block[1] = _block[1].SetBit(2, functions[7]); // F7
        _block[1] = _block[1].SetBit(3, functions[8]); // F8

        _block[2] = _block[2].SetBit(0, functions[9]); // F9
        _block[2] = _block[2].SetBit(1, functions[10]); // F10
        _block[2] = _block[2].SetBit(2, functions[11]); // F11
        _block[2] = _block[2].SetBit(3, functions[12]); // F12

        _block[3] = _block[3].SetBit(0, functions[13]); // F13
        _block[3] = _block[3].SetBit(1, functions[14]); // F14
        _block[3] = _block[3].SetBit(2, functions[15]); // F15
        _block[3] = _block[3].SetBit(3, functions[16]); // F16
        _block[3] = _block[3].SetBit(4, functions[17]); // F17
        _block[3] = _block[3].SetBit(5, functions[18]); // F18
        _block[3] = _block[3].SetBit(6, functions[19]); // F19
        _block[3] = _block[3].SetBit(7, functions[20]); // F20

        _block[4] = _block[4].SetBit(0, functions[21]); // F21
        _block[4] = _block[4].SetBit(1, functions[22]); // F22
        _block[4] = _block[4].SetBit(2, functions[23]); // F23
        _block[4] = _block[4].SetBit(3, functions[24]); // F24
        _block[4] = _block[4].SetBit(4, functions[25]); // F25
        _block[4] = _block[4].SetBit(5, functions[26]); // F26
        _block[4] = _block[4].SetBit(6, functions[27]); // F27
        _block[4] = _block[4].SetBit(7, functions[28]); // F28
    }

    /// <summary>
    ///     Indexer to allow access to the functions 0...28
    /// </summary>
    /// <exception cref="IndexOutOfRangeException"></exception>
    public bool this[int i] {
        get {
            if (i == 0) return _block[0].GetBit(4);
            if (i >= 1 && i <= 4) return _block[0].GetBit(i - 1);
            if (i >= 5 && i <= 8) return _block[1].GetBit(i - 5);
            if (i >= 9 && i <= 12) return _block[2].GetBit(i - 9);
            if (i >= 13 && i <= 20) return _block[3].GetBit(i - 13);
            if (i >= 21 && i <= 28) return _block[4].GetBit(i - 21);
            throw new IndexOutOfRangeException("Function number must be between 0..28");
        }
        set {
            if (i < 0 || i > 28) throw new IndexOutOfRangeException("Function number must be between 0..28");
            if (i == 0) _block[0].SetBit(4, value);
            if (i >= 1 && i <= 4) _block[0].SetBit(i - 1, value);
            if (i >= 5 && i <= 8) _block[1].SetBit(i - 5, value);
            if (i >= 9 && i <= 12) _block[2].SetBit(i - 9, value);
            if (i >= 13 && i <= 20) _block[3].SetBit(i - 13, value);
            if (i >= 21 && i <= 28) _block[4].SetBit(i - 21, value);
        }
    }

    public byte GetBlock(int blockNum) {
        if (blockNum <= 0) blockNum = 1;
        if (blockNum >= 5) blockNum = 5;
        return _block[blockNum - 1];
    }
}