.PHONY: run

TFM := net9.0-android
CONFIG ?= Release

all:
	dotnet build -f $(TFM) -c $(CONFIG) kinegram-emrtd-connector-maui.csproj 

clean:
	dotnet clean
	rm -rf bin obj

install:
	dotnet build -t:Run -f $(TFM) -c $(CONFIG) kinegram-emrtd-connector-maui.csproj

