using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace EmrtdConnectorMaui;

// This page is using the MVVM approach and therefore differs from the other pages.

public class ResultPageModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private string? _documentType;
    public string? DocumentType
    {
        get => _documentType;
        set
        {
            if (_documentType != value)
            {
                _documentType = value;
                OnPropertyChanged();
            }
        }
    }

    private string? _documentCode;
    public string? DocumentCode
    {
        get => _documentCode;
        set
        {
            if (_documentCode != value)
            {
                _documentCode = value;
                OnPropertyChanged();
            }
        }
    }

    private string? _issuingState;
    public string? IssuingState
    {
        get => _issuingState;
        set
        {
            if (_issuingState != value)
            {
                _issuingState = value;
                OnPropertyChanged();
            }
        }
    }

    private string? _primaryIdentifier;
    public string? PrimaryIdentifier
    {
        get => _primaryIdentifier;
        set
        {
            if (_primaryIdentifier != value)
            {
                _primaryIdentifier = value;
                OnPropertyChanged();
            }
        }
    }

    private string? _secondaryIdentifier;
    public string? SecondaryIdentifier
    {
        get => _secondaryIdentifier;
        set
        {
            if (_secondaryIdentifier != value)
            {
                _secondaryIdentifier = value;
                OnPropertyChanged();
            }
        }
    }

    private string? _nationality;
    public string? Nationality
    {
        get => _nationality;
        set
        {
            if (_nationality != value)
            {
                _nationality = value;
                OnPropertyChanged();
            }
        }
    }

    private string? _documentNumber;
    public string? DocumentNumber
    {
        get => _documentNumber;
        set
        {
            if (_documentNumber != value)
            {
                _documentNumber = value;
                OnPropertyChanged();
            }
        }
    }

    private string? _dateOfBirth;
    public string? DateOfBirth
    {
        get => _dateOfBirth;
        set
        {
            if (_dateOfBirth != value)
            {
                _dateOfBirth = value;
                OnPropertyChanged();
            }
        }
    }

    private string? _dateOfExpiry;
    public string? DateOfExpiry
    {
        get => _dateOfExpiry;
        set
        {
            if (_dateOfExpiry != value)
            {
                _dateOfExpiry = value;
                OnPropertyChanged();
            }
        }
    }

    private string? _gender;
    public string? Gender
    {
        get => _gender;
        set
        {
            if (_gender != value)
            {
                _gender = value;
                OnPropertyChanged();
            }
        }
    }

    private ImageSource? _facePhoto;
    public ImageSource? FacePhoto
    {
        get => _facePhoto;
        set
        {
            if (_facePhoto != value)
            {
                _facePhoto = value;
                OnPropertyChanged();
            }
        }
    }

    public ResultPageModel()
    {
    }

    public ResultPageModel(CSharpEmrtdPassport emrtdPassport)
    {
        if (emrtdPassport == null || emrtdPassport.FacePhoto == null)
        {
            // TODO handle error
            return;
        }

        if (emrtdPassport.MrzInfo == null)
        {
            // TODO handle error
            return;
        }

        // Don't block the UI
        _ = LoadImageAsync(emrtdPassport);

        PrimaryIdentifier = emrtdPassport.MrzInfo.PrimaryIdentifier;
        if (SecondaryIdentifier != null && emrtdPassport.MrzInfo.SecondaryIdentifier != null)
            SecondaryIdentifier = string.Join(" ", emrtdPassport.MrzInfo.SecondaryIdentifier);
        IssuingState = emrtdPassport.MrzInfo.IssuingState;
        DocumentType = emrtdPassport.MrzInfo.DocumentType;
        DocumentCode = emrtdPassport.MrzInfo.DocumentCode;
        Nationality = emrtdPassport.MrzInfo.Nationality;
        DocumentNumber = emrtdPassport.MrzInfo.DocumentNumber;
        DateOfBirth = emrtdPassport.MrzInfo.DateOfBirth;
        DateOfExpiry = emrtdPassport.MrzInfo.DateOfExpiry;
        Gender = emrtdPassport.MrzInfo.Gender;

        DateTime date;

        if (emrtdPassport.MrzInfo.DateOfBirth != null)
        {
            date = DateTime.ParseExact(emrtdPassport.MrzInfo.DateOfBirth, "yyMMdd", CultureInfo.InvariantCulture);
            DateOfBirth = date.ToString("dd.MM.yyyy");
        }

        if (emrtdPassport.MrzInfo.DateOfExpiry != null)
        {
            date = DateTime.ParseExact(emrtdPassport.MrzInfo.DateOfExpiry, "yyMMdd", CultureInfo.InvariantCulture);
            DateOfExpiry = date.ToString("dd.MM.yyyy");
        }
    }

    private async Task LoadImageAsync(CSharpEmrtdPassport emrtdPassport)
    {
        await Task.Run(() =>
        {
            if (emrtdPassport.FacePhoto != null)
            {
                byte[] bytes = emrtdPassport.FacePhoto.ToArray();

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    FacePhoto = ImageSource.FromStream(() => new MemoryStream(bytes));
                });
            }
        });
    }

    public void OnPropertyChanged([CallerMemberName] string name = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
