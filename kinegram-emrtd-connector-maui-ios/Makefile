.PHONY: run

CONFIG ?= Debug

all:
	dotnet build -c $(CONFIG) kinegram-emrtd-connector-maui-ios.csproj

clean:
	dotnet clean
	rm -rf bin obj BindingOutput
