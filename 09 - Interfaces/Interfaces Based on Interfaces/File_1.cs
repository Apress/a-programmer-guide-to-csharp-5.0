// 10 - Interfaces\Interfaces Based on Interfaces
// copyright 2000 Eric Gunnerson
using System.Runtime.Serialization;
using System;
interface IComparableSerializable :
IComparable, ISerializable
{
    string GetStatusString();
}