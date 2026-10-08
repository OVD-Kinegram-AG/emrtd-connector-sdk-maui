using Foundation;
using ObjCRuntime;

namespace EmrtdConnectorIos
{
    public delegate void EmrtdConnectorCompletionBlock([NullAllowed] string passportJson, [NullAllowed] NSError error);

    // Binds EmrtdConnectorObjCWrapper of KinegramEmrtdConnector.xcframework.
    // It returns the ValidationResult as JSON.
    [BaseType(typeof(NSObject))]
    [DisableDefaultCtor]
    public interface EmrtdConnectorObjCWrapper
    {
        [Export("initWithServerURL:validationId:clientId:")]
        [DesignatedInitializer]
        public NativeHandle Constructor(NSUrl serverURL, string validationId, string clientId);

        [Export("readPassportWithDocumentNumber:dateOfBirth:dateOfExpiry:validationId:httpHeaders:enableDiagnostics:completion:")]
        public void ReadPassport(string documentNumber, string dateOfBirth, string dateOfExpiry, string validationId, [NullAllowed] NSDictionary<NSString, NSString> httpHeaders, [NullAllowed] NSNumber enableDiagnostics, EmrtdConnectorCompletionBlock completion);

        [Export("readPassportWithCan:validationId:httpHeaders:enableDiagnostics:completion:")]
        public void ReadPassportWithCan(string can, string validationId, [NullAllowed] NSDictionary<NSString, NSString> httpHeaders, [NullAllowed] NSNumber enableDiagnostics, EmrtdConnectorCompletionBlock completion);
    }
}
