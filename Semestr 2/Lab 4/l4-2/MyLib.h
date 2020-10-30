#ifdef _WIN32
#include <tchar.h>
#endif

extern "C" __declspec(dllexport) __stdcall double Add(double a, double b);
extern "C" __declspec(dllexport) __stdcall double Sub(double a, double b);
extern "C" __declspec(dllexport) __stdcall double Multiply(double a, double b);
extern "C" __declspec(dllexport) __stdcall double Divide(double a, double b);
extern "C" __declspec(dllexport) __stdcall double Pow(double a, int b);
