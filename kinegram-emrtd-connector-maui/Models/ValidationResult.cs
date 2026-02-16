using System.Collections.Generic;
using System.Text.Json.Serialization;

public sealed class ValidationResult
{
    [JsonPropertyName("mrzInfo")]
    public MrzInfo? MrzInfo { get; set; }

    [JsonPropertyName("chipAuthenticationResult")]
    public string? ChipAuthenticationResult { get; set; }

    [JsonPropertyName("activeAuthenticationResult")]
    public string? ActiveAuthenticationResult { get; set; }

    [JsonPropertyName("passiveAuthentication")]
    public bool PassiveAuthentication { get; set; }

    [JsonPropertyName("filesBinary")]
    public FilesBinary? FilesBinary { get; set; }

    [JsonPropertyName("sodInfo")]
    public SodInfo? SodInfo { get; set; }

    [JsonPropertyName("passiveAuthenticationDetails")]
    public PassiveAuthenticationDetails? PassiveAuthenticationDetails { get; set; }

    [JsonPropertyName("facePhoto")]
    public string? FacePhoto { get; set; }

    [JsonPropertyName("errors")]
    public List<ValidationError> Errors { get; set; } = new();
}

public sealed class MrzInfo
{
    [JsonPropertyName("optionalData1")]
    public string? OptionalData1 { get; set; }

    [JsonPropertyName("primaryIdentifier")]
    public string? PrimaryIdentifier { get; set; }

    [JsonPropertyName("issuingState")]
    public string? IssuingState { get; set; }

    [JsonPropertyName("secondaryIdentifier")]
    public List<string> SecondaryIdentifier { get; set; } = new();

    [JsonPropertyName("documentType")]
    public string? DocumentType { get; set; }

    [JsonPropertyName("documentNumber")]
    public string? DocumentNumber { get; set; }

    [JsonPropertyName("gender")]
    public string? Gender { get; set; }

    [JsonPropertyName("dateOfBirth")]
    public string? DateOfBirth { get; set; }

    [JsonPropertyName("nationality")]
    public string? Nationality { get; set; }

    [JsonPropertyName("documentCode")]
    public string? DocumentCode { get; set; }

    [JsonPropertyName("dateOfExpiry")]
    public string? DateOfExpiry { get; set; }
}

public sealed class FilesBinary
{
    [JsonPropertyName("dg1")]
    public string? Dg1 { get; set; }

    [JsonPropertyName("dg2")]
    public string? Dg2 { get; set; }

    [JsonPropertyName("sod")]
    public string? Sod { get; set; }
}

public sealed class SodInfo
{
    [JsonPropertyName("hashAlgorithm")]
    public string? HashAlgorithm { get; set; }

    [JsonPropertyName("hashForDataGroup")]
    public Dictionary<string, string> HashForDataGroup { get; set; } = new();
}

public sealed class PassiveAuthenticationDetails
{
    [JsonPropertyName("sodSignatureValid")]
    public bool SodSignatureValid { get; set; }

    [JsonPropertyName("dataGroupsWithValidHash")]
    public List<int> DataGroupsWithValidHash { get; set; } = new();

    [JsonPropertyName("allHashesValid")]
    public bool AllHashesValid { get; set; }

    [JsonPropertyName("documentCertificateValid")]
    public bool DocumentCertificateValid { get; set; }

    [JsonPropertyName("dataGroupsChecked")]
    public List<int> DataGroupsChecked { get; set; } = new();
}

public sealed class ValidationError
{
    [JsonExtensionData]
    public Dictionary<string, object>? Extra { get; set; }
}
