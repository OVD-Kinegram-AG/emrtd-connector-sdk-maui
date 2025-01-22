using System;
using System.Collections.Generic;

namespace EmrtdConnectorMaui;

// To avoid confusion with the EmrtdPassport class in the Android project, we name this class CSharpEmrtdPassport
public class CSharpEmrtdPassport
{
    public MrzInfo MrzInfo { get; set; }
    public byte[] FacePhoto { get; set; }

    public CSharpEmrtdPassport()
    {
        MrzInfo = new MrzInfo();
    }
}

public class MrzInfo
{
    public string DocumentType { get; set; }
    public string DocumentCode { get; set; }
    public string IssuingState { get; set; }
    public string PrimaryIdentifier { get; set; }
    public List<string> SecondaryIdentifier { get; set; }
    public string Nationality { get; set; }
    public string DocumentNumber { get; set; }
    public string DateOfBirth { get; set; }
    public string DateOfExpiry { get; set; }
    public string Gender { get; set; }

    public MrzInfo()
    {
        DocumentType = string.Empty;
        DocumentCode = string.Empty;
        IssuingState = string.Empty;
        PrimaryIdentifier = string.Empty;
        SecondaryIdentifier = new List<string>();
        Nationality = string.Empty;
        DocumentNumber = string.Empty;
        DateOfBirth = string.Empty;
        DateOfExpiry = string.Empty;
        Gender = string.Empty;
    }
}
