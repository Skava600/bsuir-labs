#pragma hdrstop
#pragma argsused

#include "MyLib.h"


extern "C" __declspec(dllexport) __stdcall double Add(double a, double b)
{
	return a + b;
}

extern "C" __declspec(dllexport) __stdcall double Sub(double a, double b)
{
	return a - b;
}

extern "C" __declspec(dllexport) __stdcall double Multiply(double a, double b)
{
	return a * b;
}

extern "C" __declspec(dllexport) __stdcall double Divide(double a, double b)
{
	return a / b;
}

extern "C" __declspec(dllexport) __stdcall double Pow(double a, int b)
{
	double c = 1;
	for(int i = 0; i < b; i++)
	{
		 c *= a;
	}
    return c;
}



