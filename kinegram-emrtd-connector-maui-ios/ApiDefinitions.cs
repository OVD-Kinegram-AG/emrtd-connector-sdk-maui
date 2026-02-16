using System;
using System.ComponentModel;
using Foundation;
using ObjCRuntime;

namespace EmrtdConnectorIos;

delegate void EmrtdPassportCompletionHandler(
		[NullAllowed] NSString result,
		[NullAllowed] NSError error
	);

[BaseType(typeof(NSObject))]
interface EmrtdConnectorObjCWrapper
{
	// @objc public init?(serverURL: URL, validationId: String, clientId: String)
	[Export("initWithServerURL:validationId:clientId:")]
	NativeHandle Constructor(NSUrl serverURL, string validationId, string clientId);

	[Export("readPassportWithDocumentNumber:dateOfBirth:dateOfExpiry:validationId:httpHeaders:enableDiagnostics:completion:")]
	void ReadPassport(
				string documentNumber,
				string dateOfBirth,
				string dateOfExpiry,
				string validationId,
				[NullAllowed] NSDictionary<NSString, NSString> httpHeaders,
				[NullAllowed] NSNumber enableDiagnostics,
				EmrtdPassportCompletionHandler completion
			);

	[Export("readPassportWithCan:validationId:httpHeaders:enableDiagnostics:completion:")]
	void ReadPassport(
				string can,
				string validationId,
				[NullAllowed] NSDictionary<NSString, NSString> httpHeaders,
				[NullAllowed] NSNumber enableDiagnostics,
				EmrtdPassportCompletionHandler completion
			);
}
