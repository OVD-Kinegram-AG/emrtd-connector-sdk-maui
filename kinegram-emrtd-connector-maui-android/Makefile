.PHONY: run

CONFIG ?= Release

all:
	dotnet build -c $(CONFIG) kinegram-emrtd-connector-maui-android.csproj

clean:
	dotnet clean
	rm -rf bin obj
	rm -rf ../LocalNuGetPackages/*
