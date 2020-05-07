#pragma once
#include <fmi2Functions.h>
#include <stdarg.h>

#define API __declspec(dllexport)

extern "C" {
	API void ReadCStr(const char* str);

	API void Compare(const char* str);

	API void FromFMI(const char* str);

	typedef void (*TestFType) (const char*, ...);

	API void TestLogger(const char* str, TestFType logger);
}