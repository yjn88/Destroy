#include "audio.hpp"

EXPORT_FUNC_CPP OPEN(WCHAR* path)
{
    wstring p(path);
    wstring openCmd = L"open " + p;
    MCIERROR r = mciSendString(&openCmd[0], NULL, 0, 0);

    if (r == 0)
    {
        return TRUE;
    }
    else
    {
        return FALSE;
    }
}

EXPORT_FUNC_CPP CLOSE(WCHAR* path)
{
    wstring p(path);
    wstring closeCmd = L"close " + p;
    MCIERROR r = mciSendString(&closeCmd[0], NULL, 0, 0);

    if (r == 0)
    {
        return TRUE;
    }
    else
    {
        return FALSE;
    }
}

EXPORT_FUNC_CPP PLAY(WCHAR* path, BOOL repeat)
{
    wstring playSet = L"";
    if (repeat)
    {
        playSet + L" repeat";
    }
    wstring p(path);
    wstring playCmd = L"play " + p + playSet;
    MCIERROR r = mciSendString(&playCmd[0], NULL, 0, 0);

    if (r == 0)
    {
        return TRUE;
    }
    else
    {
        return FALSE;
    }
}

EXPORT_FUNC_CPP PAUSE(WCHAR* path)
{
    wstring p(path);
    wstring pauseCmd = L"pause " + p;
    MCIERROR r = mciSendString(&pauseCmd[0], NULL, 0, 0);

    if (r == 0)
    {
        return TRUE;
    }
    else
    {
        return FALSE;
    }
}

EXPORT_FUNC_CPP RESUME(WCHAR* path)
{
    wstring p(path);
    wstring resumeCmd = L"resume " + p;
    MCIERROR r = mciSendString(&resumeCmd[0], NULL, 0, 0);

    if (r == 0)
    {
        return TRUE;
    }
    else
    {
        return FALSE;
    }
}

EXPORT_FUNC_CPP SET_VOLUME(WCHAR* path, USHORT volume)
{
    wstring p(path);
    wstring setVolumeCmd = L"setaudio " + p + L" volume to " + to_wstring(volume);
    MCIERROR r = mciSendString(&setVolumeCmd[0], NULL, 0, 0);

    if (r == 0)
    {
        return TRUE;
    }
    else
    {
        return FALSE;
    }
}