using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FixCodeManager : MonoBehaviour
{
    public static FixCodeManager Instance { get; private set; }

    public int totalFixCodeToGenerate = 10;

    public FixCode[] FixCodes { get; private set; }

    private System.Random rnd = new System.Random();

    private void Awake()
    {
        Instance = this;
        SetupCodes();
    }

    private void SetupCodes()
    {
        FixCodes = new FixCode[totalFixCodeToGenerate];
        for (int i = 0; i < totalFixCodeToGenerate; i++)
        {
            string pcId = "#" + RandomString(3);
            string fixCode = RandomString(3, true);
            FixCodes[i] = new FixCode() { pcId = pcId, fixCode = fixCode };
        }
    }

    private string RandomString(int length, bool useOnlyNumeric = false)
    {
        string chars = useOnlyNumeric ? "0123456789" : "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[rnd.Next(s.Length)]).ToArray());
    }

    public FixCode GetRandomFixCode(bool assignFirstFixCode) => !assignFirstFixCode ? FixCodes[rnd.Next(FixCodes.Length)] : FixCodes[0];
}

[System.Serializable]
public struct FixCode
{
    public string pcId;
    public string fixCode;
}
