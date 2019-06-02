#pragma once

#include <Windows.h>
#include <string>
#include <mmsystem.h>
#pragma comment(lib, "winmm.lib")
using namespace std;

#define EXPORT_FUNC_CPP extern "C" __declspec(dllexport) BOOL __stdcall

EXPORT_FUNC_CPP OPEN(WCHAR* path);

EXPORT_FUNC_CPP CLOSE(WCHAR* path);

EXPORT_FUNC_CPP PLAY(WCHAR* path, BOOL repeat);

EXPORT_FUNC_CPP PAUSE(WCHAR* path);

EXPORT_FUNC_CPP RESUME(WCHAR* path);

EXPORT_FUNC_CPP SET_VOLUME(WCHAR* path, USHORT volume);