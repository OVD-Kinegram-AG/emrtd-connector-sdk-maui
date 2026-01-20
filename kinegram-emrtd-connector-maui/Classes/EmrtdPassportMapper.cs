#if ANDROID
using EmrtdConnectorAndroid;

namespace EmrtdConnectorMaui;

// This class is used to map the EmrtdPassport class from the Android project to the EmrtdPassport class in the MAUI project
public static class EmrtdPassportMapper
{
    public static CSharpEmrtdPassport? ToCSharpEmrtdPassport(EmrtdConnectorAndroid.EmrtdPassport emrtdPassport)
    {
        if (emrtdPassport?.MrzInfo == null) return null;

        return new CSharpEmrtdPassport
        {
            MrzInfo = new MrzInfo
            {
                DocumentType = emrtdPassport.MrzInfo.DocumentType,
                DocumentCode = emrtdPassport.MrzInfo.DocumentCode,
                IssuingState = emrtdPassport.MrzInfo.IssuingState,
                PrimaryIdentifier = emrtdPassport.MrzInfo.PrimaryIdentifier,
                SecondaryIdentifier = emrtdPassport.MrzInfo.SecondaryIdentifier?.ToList() ?? new List<string>(),
                Nationality = emrtdPassport.MrzInfo.Nationality,
                DocumentNumber = emrtdPassport.MrzInfo.DocumentNumber,
                DateOfBirth = emrtdPassport.MrzInfo.DateOfBirth,
                DateOfExpiry = emrtdPassport.MrzInfo.DateOfExpiry,
                Gender = emrtdPassport.MrzInfo.Gender
            },
            FacePhoto = emrtdPassport.FacePhoto?.ToArray() ?? Array.Empty<byte>()
        };
    }
}
#endif