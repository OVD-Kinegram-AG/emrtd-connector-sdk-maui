.DEFAULT_GOAL := help

.PHONY: help android iphoneos install-android install-iphoneos clean

CONFIG ?= DEBUG

help:
	@echo "eMRTD Connector SDK MAUI Makefile"
	@echo ""
	@echo "Targets:"
	@echo "  android            Build Android ($(CONFIG))"
	@echo "  iphoneos           Build iOS ($(CONFIG))"
	@echo "  install-android    Build + install Android ($(CONFIG))"
	@echo "  install-iphoneos   Build + install iOS ($(CONFIG))"
	@echo "  clean              Clean build artifacts"
	@echo ""
	@echo "Usage:"
	@echo "  make <target>"
	@echo "  make <target> CONFIG=DEBUG"
	@echo "  make <target> CONFIG=RELEASE"
	@echo ""
	@echo "Examples:"
	@echo "  make android"
	@echo "  make install-android CONFIG=RELEASE"
	@echo "  make iphoneos CONFIG=DEBUG"
	@echo ""

android:
	dotnet build -f net9.0-android -c $(CONFIG) kinegram-emrtd-connector-maui.csproj 

iphoneos:
	dotnet build -f net9.0-ios -c $(CONFIG) kinegram-emrtd-connector-maui.csproj 

clean:
	dotnet clean
	rm -rf bin obj

install-android:
	dotnet build -t:Run -f net9.0-android -c $(CONFIG) kinegram-emrtd-connector-maui.csproj

install-iphoneos:
	dotnet build -t:Run -f net9.0-ios -c $(CONFIG) -p:_DeviceName="iPhone von Lars" kinegram-emrtd-connector-maui.csproj
