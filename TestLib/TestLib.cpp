#include "pch.h"
#include "TestLib.h"
#include <iostream>
#include <cstring>

using namespace std;

void ReadCStr(const char* str)
{
	cout << str << endl;
}

void Compare(const char* str)
{
	auto result = strcmp(str, "Text");
	cout << "Compeare =" << result << endl;
}

void FromFMI(const char* str) {
	const char* MODEL_GUID = "{2a13a763-32b1-4de6-b9e2-fb1e3d21c786}";
	if (strcmp(str, MODEL_GUID) != 0) {
		cout << strcmp(str, MODEL_GUID) << endl;
	}
	else {
		cout << "Equals = " << strcmp(str, MODEL_GUID) << endl;
	}
}



void TestLogger(const char* str, TestFType logger)
{
	cout << "Enter to TestLogger" << endl;
	cout << "Have logger = " << logger << endl;
	cout << "Create pointer" << endl;
	auto pointer = nullptr;
	cout << "Create inst name" << endl;
	const char* instName = "SomeInstanceName";
	cout << "Create fmiStatus" << endl;
	fmi2Status fmistatus = fmi2Error;
	cout << "Create fmiStatusStr" << endl;
	const char* fmiStatusStr = "error";
	cout << "Create message" << endl;
	const char* message = "Args = %s %d %s";
	cout << "Call logger" << endl;
	/*logger(pointer,
		instName,
		fmistatus,
		fmiStatusStr,
		message);*/
	logger(message, "First", 2, "Three");
}
