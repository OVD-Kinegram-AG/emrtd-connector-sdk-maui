using Foundation;
using ObjCRuntime;

namespace EmrtdConnectorIos
{
    public delegate void KinegramEMRTDCompletionBlock([NullAllowed] string passportJson, [NullAllowed] NSError error);

    [BaseType(typeof(NSObject))]
    public interface KinegramEMRTDWrapper
    {
        [Export("initWithClientId:webSocketUrl:")]
        [DesignatedInitializer]
        public NativeHandle Constructor(string clientId, string url);

        [Export("readPassportWithDocumentNumber:dateOfBirth:dateOfExpiry:completion:")]
        public void ReadPassport(string documentNumber, string dateOfBirth, string dateOfExpiry, KinegramEMRTDCompletionBlock completion);

        [Export("readPassportWithCan:completion:")]
        public void ReadPassportWithCan(string can, KinegramEMRTDCompletionBlock completion);
    }
}