#pragma once

#ifndef UNICODE
#error Please enable UNICODE for your compiler! VS: Project Properties -> General -> Character Set -> Use Unicode.
#endif

#include <Windows.h>

typedef struct _CONSOLE_INPUT_MODE
{
    BOOL _ENABLE_PROCESSED_INPUT;
    BOOL _ENABLE_LINE_INPUT;
    BOOL _ENABLE_ECHO_INPUT;
    BOOL _ENABLE_WINDOW_INPUT;
    BOOL _ENABLE_MOUSE_INPUT;
    BOOL _ENABLE_INSERT_MODE;
    BOOL _ENABLE_QUICK_EDIT_MODE;
    BOOL _ENABLE_EXTENDED_FLAGS;
    BOOL _ENABLE_AUTO_POSITION;
    BOOL _ENABLE_VIRTUAL_TERMINAL_INPUT;
} CONSOLE_INPUT_MODE;

typedef struct _CONSOLE_OUTPUT_MODE
{
    BOOL _ENABLE_PROCESSED_OUTPUT;
    BOOL _ENABLE_WRAP_AT_EOL_OUTPUT;
    BOOL _ENABLE_VIRTUAL_TERMINAL_PROCESSING;
    BOOL _DISABLE_NEWLINE_AUTO_RETURN;
    BOOL _ENABLE_LVB_GRID_WORLDWIDE;
} CONSOLE_OUTPUT_MODE;

#pragma region Window

//GetConsoleWindow
//https://docs.microsoft.com/en-us/windows/console/getconsolewindow
BOOL GET_WINDOW_HANDLE(HWND * hConsoleWindow)
{
    *hConsoleWindow = GetConsoleWindow();
    if (*hConsoleWindow == NULL)
        return FALSE;

    return TRUE;
}

//GetConsoleWindow
//https://docs.microsoft.com/en-us/windows/console/getconsolewindow
//GetWindowRect
//https://docs.microsoft.com/en-us/windows/desktop/api/winuser/nf-winuser-getwindowrect
BOOL GET_WINDOW_POS(LONG* x, LONG* y)
{
    HWND hConsoleWindow = GetConsoleWindow();
    if (hConsoleWindow == NULL)
        return FALSE;

    RECT rect;
    BOOL r = GetWindowRect(hConsoleWindow, &rect);
    if (r == FALSE)
        return FALSE;

    *x = rect.left;
    *y = rect.top;

    return TRUE;
}

//GetConsoleWindow
//https://docs.microsoft.com/en-us/windows/console/getconsolewindow
//SetWindowPos
//https://docs.microsoft.com/en-us/windows/desktop/api/winuser/nf-winuser-setwindowpos
BOOL SET_WINDOW_POS(int x, int y)
{
    HWND hConsoleWindow = GetConsoleWindow();
    if (hConsoleWindow == NULL)
        return FALSE;

    BOOL r = SetWindowPos(hConsoleWindow, HWND_NOTOPMOST, x, y, 0, 0, SWP_NOSIZE);
    return r;
}

//GetConsoleWindow
//https://docs.microsoft.com/en-us/windows/console/getconsolewindow
//GetWindowRect
//https://docs.microsoft.com/en-us/windows/desktop/api/winuser/nf-winuser-getwindowrect
BOOL GET_WINDOW_SIZE(LONG * width, LONG * height)
{
    HWND hConsoleWindow = GetConsoleWindow();
    if (hConsoleWindow == NULL)
        return FALSE;

    RECT rect;
    BOOL r = GetWindowRect(hConsoleWindow, &rect);
    if (r == FALSE)
        return FALSE;

    *width = (rect.right - rect.left);
    *height = (rect.bottom - rect.top);

    return TRUE;
}

//GetDesktopWindow
//https://docs.microsoft.com/en-us/windows/desktop/api/winuser/nf-winuser-getdesktopwindow
//GetWindowRect
//https://docs.microsoft.com/en-us/windows/desktop/api/winuser/nf-winuser-getwindowrect
BOOL GET_DESKTOP_SIZE(LONG * width, LONG * height)
{
    HWND hDesktopWindow = GetDesktopWindow();

    RECT rect;
    BOOL r = GetWindowRect(hDesktopWindow, &rect);
    if (r == FALSE)
        return FALSE;

    *width = rect.right;
    *height = rect.bottom;

    return TRUE;
}

//GetConsoleWindow
//https://docs.microsoft.com/en-us/windows/console/getconsolewindow
//GetLayeredWindowAttributes
//https://docs.microsoft.com/en-us/windows/desktop/api/winuser/nf-winuser-getlayeredwindowattributes
BOOL GET_WINDOW_ALPHA(BYTE * alpha)
{
    HWND hConsoleWindow = GetConsoleWindow();
    if (hConsoleWindow == NULL)
        return FALSE;

    COLORREF color = 0;
    DWORD flag = 0;
    BOOL r = GetLayeredWindowAttributes(hConsoleWindow, &color, alpha, &flag);
    return r;
}

//GetConsoleWindow
//https://docs.microsoft.com/en-us/windows/console/getconsolewindow
//GetWindowLongPtr
//https://docs.microsoft.com/en-us/windows/desktop/api/winuser/nf-winuser-getwindowlongptrw
//SetWindowLongPtr
//https://docs.microsoft.com/en-us/windows/desktop/api/winuser/nf-winuser-setwindowlongptrw
//SetLayeredWindowAttributes
//https://docs.microsoft.com/en-us/windows/desktop/api/winuser/nf-winuser-setlayeredwindowattributes
BOOL SET_WINDOW_ALPHA(BYTE alpha)
{
    HWND hConsoleWindow = GetConsoleWindow();
    if (hConsoleWindow == NULL)
        return FALSE;

    LONG_PTR r = GetWindowLongPtr(hConsoleWindow, GWL_EXSTYLE);
    if (r == 0)
        return FALSE;

    LONG_PTR r2 = SetWindowLongPtr(hConsoleWindow, GWL_EXSTYLE, r | WS_EX_LAYERED);
    if (r2 == 0)
        return FALSE;

    BOOL r3 = SetLayeredWindowAttributes(hConsoleWindow, 0, alpha, LWA_ALPHA);
    return r3;
}

//GetForegroundWindow
//https://docs.microsoft.com/en-us/windows/desktop/api/winuser/nf-winuser-getforegroundwindow
//GetConsoleWindow
//https://docs.microsoft.com/en-us/windows/console/getconsolewindow
BOOL GET_WINDOW_IN_FOCUS(void)
{
    HWND hForeground = GetForegroundWindow();
    if (hForeground == NULL)
        return FALSE;

    HWND hConsole = GetConsoleWindow();
    if (hConsole == NULL)
        return FALSE;

    return hForeground == hConsole;
}

//GetConsoleWindow
//https://docs.microsoft.com/en-us/windows/console/getconsolewindow
//SetWindowLongPtr
//https://docs.microsoft.com/en-us/windows/desktop/api/winuser/nf-winuser-setwindowlongptrw
//GetWindowRect
//https://docs.microsoft.com/en-us/windows/desktop/api/winuser/nf-winuser-getwindowrect
//SetWindowPos
//https://docs.microsoft.com/en-us/windows/desktop/api/winuser/nf-winuser-setwindowpos
BOOL SET_WINDOW_MENU(BOOL visible)
{
    HWND hConsoleWindow = GetConsoleWindow();
    if (hConsoleWindow == NULL)
        return FALSE;

    LONG value = 0;
    if (visible)
        value = WS_CAPTION;
    else
        value = WS_SYSMENU;
    LONG_PTR r = SetWindowLongPtr(hConsoleWindow, GWL_STYLE, value);
    if (r == 0)
        return FALSE;

    RECT rect;
    BOOL r2 = GetWindowRect(hConsoleWindow, &rect);
    if (r2 == FALSE)
        return FALSE;

    //ShowWindow
    BOOL r3 = SetWindowPos(hConsoleWindow, HWND_NOTOPMOST, rect.left, rect.top, 0, 0,
        SWP_SHOWWINDOW | SWP_NOSIZE);
    return r3;
}

//GetCursorPos
//https://docs.microsoft.com/zh-cn/windows/desktop/api/winuser/nf-winuser-getcursorpos
BOOL GET_CURSOR_POS(LONG * x, LONG * y)
{
    POINT pos;
    BOOL r = GetCursorPos(&pos);
    *x = pos.x;
    *y = pos.y;
    return r;
}

//SetCursorPos
//https://docs.microsoft.com/en-us/windows/desktop/api/winuser/nf-winuser-setcursorpos
BOOL SET_CURSOR_POS(int x, int y)
{
    BOOL r = SetCursorPos(x, y);
    return r;
}

//MessageBox
//https://docs.microsoft.com/zh-cn/windows/desktop/api/winuser/nf-winuser-messagebox
BOOL MESSAGE_BOX(WCHAR * text, WCHAR * caption, UINT type, int* choose)
{
    int r = MessageBox(NULL, text, caption, type);
    if (r == 0)
        return FALSE;

    *choose = r;
    return TRUE;
}

#pragma endregion

#pragma region Console

//GetStdHandle
//https://docs.microsoft.com/en-us/windows/console/getstdhandle
BOOL GET_CONSOLE_HANDLE(HANDLE * handle, DWORD stdHandle)
{
    *handle = GetStdHandle(stdHandle);

    if (*handle == INVALID_HANDLE_VALUE)
        return FALSE;
    if (*handle == NULL)
        return FALSE;

    return TRUE;
}

//CreateConsoleScreenBuffer
//https://docs.microsoft.com/en-us/windows/console/createconsolescreenbuffer
BOOL CREAT_CONSOLE_BUFFER(HANDLE * hConsoleScreen)
{
    *hConsoleScreen = CreateConsoleScreenBuffer
    (
        GENERIC_READ | GENERIC_WRITE,
        FILE_SHARE_READ | FILE_SHARE_WRITE,
        NULL,
        CONSOLE_TEXTMODE_BUFFER,
        NULL
    );

    if (*hConsoleScreen == INVALID_HANDLE_VALUE)
        return FALSE;

    return TRUE;
}

//SetConsoleActiveScreenBuffer
//https://docs.microsoft.com/en-us/windows/console/setconsoleactivescreenbuffer
BOOL SET_CONSOLE_ACTIVE_BUFFER(HANDLE hConsoleScreen)
{
    BOOL r = SetConsoleActiveScreenBuffer(hConsoleScreen);
    return r;
}

//GetConsoleMode
//https://docs.microsoft.com/en-us/windows/console/getconsolemode
BOOL GET_CONSOLE_MODE(HANDLE	hConsoleOutput, HANDLE hConsoleInput, CONSOLE_INPUT_MODE * cim, CONSOLE_OUTPUT_MODE * com)
{
    DWORD inputMode = 0, outputMode = 0;
    BOOL r = GetConsoleMode(hConsoleInput, &inputMode);
    BOOL r2 = GetConsoleMode(hConsoleOutput, &outputMode);

    if (r == FALSE || r2 == FALSE)
        return FALSE;

    cim->_ENABLE_PROCESSED_INPUT =
        (inputMode & ENABLE_PROCESSED_INPUT) == ENABLE_PROCESSED_INPUT;
    cim->_ENABLE_LINE_INPUT =
        (inputMode & ENABLE_LINE_INPUT) == ENABLE_LINE_INPUT;
    cim->_ENABLE_ECHO_INPUT =
        (inputMode & ENABLE_ECHO_INPUT) == ENABLE_ECHO_INPUT;
    cim->_ENABLE_WINDOW_INPUT =
        (inputMode & ENABLE_WINDOW_INPUT) == ENABLE_WINDOW_INPUT;
    cim->_ENABLE_MOUSE_INPUT =
        (inputMode & ENABLE_MOUSE_INPUT) == ENABLE_MOUSE_INPUT;
    cim->_ENABLE_INSERT_MODE =
        (inputMode & ENABLE_INSERT_MODE) == ENABLE_INSERT_MODE;
    cim->_ENABLE_QUICK_EDIT_MODE =
        (inputMode & ENABLE_QUICK_EDIT_MODE) == ENABLE_QUICK_EDIT_MODE;
    cim->_ENABLE_EXTENDED_FLAGS =
        (inputMode & ENABLE_EXTENDED_FLAGS) == ENABLE_EXTENDED_FLAGS;
    cim->_ENABLE_AUTO_POSITION =
        (inputMode & ENABLE_AUTO_POSITION) == ENABLE_AUTO_POSITION;
    cim->_ENABLE_VIRTUAL_TERMINAL_INPUT =
        (inputMode & ENABLE_VIRTUAL_TERMINAL_INPUT) == ENABLE_VIRTUAL_TERMINAL_INPUT;

    com->_ENABLE_PROCESSED_OUTPUT =
        (outputMode & ENABLE_PROCESSED_OUTPUT) == ENABLE_PROCESSED_OUTPUT;
    com->_ENABLE_WRAP_AT_EOL_OUTPUT =
        (outputMode & ENABLE_WRAP_AT_EOL_OUTPUT) == ENABLE_WRAP_AT_EOL_OUTPUT;
    com->_ENABLE_VIRTUAL_TERMINAL_PROCESSING =
        (outputMode & ENABLE_VIRTUAL_TERMINAL_PROCESSING) == ENABLE_VIRTUAL_TERMINAL_PROCESSING;
    com->_DISABLE_NEWLINE_AUTO_RETURN =
        (outputMode & DISABLE_NEWLINE_AUTO_RETURN) == DISABLE_NEWLINE_AUTO_RETURN;
    com->_ENABLE_LVB_GRID_WORLDWIDE =
        (outputMode & ENABLE_LVB_GRID_WORLDWIDE) == ENABLE_LVB_GRID_WORLDWIDE;

    return TRUE;
}

//SetConsoleMode
//https://docs.microsoft.com/en-us/windows/console/setconsolemode
BOOL SET_CONSOLE_MODE(HANDLE	hConsoleOutput, HANDLE hConsoleInput, CONSOLE_INPUT_MODE cim, CONSOLE_OUTPUT_MODE com)
{
    DWORD inputMode = 0, outputMode = 0;

    if (cim._ENABLE_PROCESSED_INPUT)
        inputMode |= ENABLE_PROCESSED_INPUT;
    if (cim._ENABLE_LINE_INPUT)
        inputMode |= ENABLE_LINE_INPUT;
    if (cim._ENABLE_ECHO_INPUT)
        inputMode |= ENABLE_ECHO_INPUT;
    if (cim._ENABLE_WINDOW_INPUT)
        inputMode |= ENABLE_WINDOW_INPUT;
    if (cim._ENABLE_MOUSE_INPUT)
        inputMode |= ENABLE_MOUSE_INPUT;
    if (cim._ENABLE_INSERT_MODE)
        inputMode |= ENABLE_INSERT_MODE;
    if (cim._ENABLE_QUICK_EDIT_MODE)
        inputMode |= ENABLE_QUICK_EDIT_MODE;
    if (cim._ENABLE_EXTENDED_FLAGS)
        inputMode |= ENABLE_EXTENDED_FLAGS;
    if (cim._ENABLE_AUTO_POSITION)
        inputMode |= ENABLE_AUTO_POSITION;
    if (cim._ENABLE_VIRTUAL_TERMINAL_INPUT)
        inputMode |= ENABLE_VIRTUAL_TERMINAL_INPUT;

    if (com._ENABLE_PROCESSED_OUTPUT)
        outputMode |= ENABLE_PROCESSED_OUTPUT;
    if (com._ENABLE_WRAP_AT_EOL_OUTPUT)
        outputMode |= ENABLE_WRAP_AT_EOL_OUTPUT;
    if (com._ENABLE_VIRTUAL_TERMINAL_PROCESSING)
        outputMode |= ENABLE_VIRTUAL_TERMINAL_PROCESSING;
    if (com._DISABLE_NEWLINE_AUTO_RETURN)
        outputMode |= DISABLE_NEWLINE_AUTO_RETURN;
    if (com._ENABLE_LVB_GRID_WORLDWIDE)
        outputMode |= ENABLE_LVB_GRID_WORLDWIDE;

    BOOL r = SetConsoleMode(hConsoleInput, inputMode);
    BOOL r2 = SetConsoleMode(hConsoleOutput, outputMode);

    return r == r2 == TRUE;
}

//GetCurrentConsoleFontEx
//https://docs.microsoft.com/en-us/windows/console/getcurrentconsolefontex
BOOL GET_CONSOLE_FONT(HANDLE hConsoleOutput, BOOL * bold, SHORT * fontWidth, SHORT * fontHeight, WCHAR fontName[LF_FACESIZE])
{
    BOOL maxinumWindow = FALSE;

    CONSOLE_FONT_INFOEX cfi;
    cfi.cbSize = sizeof(CONSOLE_FONT_INFOEX);
    BOOL r = GetCurrentConsoleFontEx(hConsoleOutput, maxinumWindow, &cfi);
    if (r == FALSE)
        return FALSE;

    *fontWidth = cfi.dwFontSize.X;
    *fontHeight = cfi.dwFontSize.Y;

    wcscpy_s(fontName, LF_FACESIZE, cfi.FaceName);

    *bold = cfi.FontWeight >= FW_BOLD;

    return TRUE;
}

//GetCurrentConsoleFontEx
//https://docs.microsoft.com/en-us/windows/console/getcurrentconsolefontex
//SetCurrentConsoleFontEx
//https://docs.microsoft.com/en-us/windows/console/setcurrentconsolefontex
BOOL SET_CONSOLE_FONT(HANDLE hConsoleOutput, BOOL bold, SHORT fontWidth, SHORT fontHeight, wchar_t* fontName, rsize_t fontNameLength)
{
    BOOL maxinumWindow = FALSE;

    CONSOLE_FONT_INFOEX cfi;
    cfi.cbSize = sizeof(CONSOLE_FONT_INFOEX);
    BOOL r = GetCurrentConsoleFontEx(hConsoleOutput, maxinumWindow, &cfi);
    if (r == FALSE)
        return FALSE;

    cfi.dwFontSize.X = fontWidth;
    cfi.dwFontSize.Y = fontHeight;
    wcscpy_s(cfi.FaceName, fontNameLength + 1, fontName);
    if (bold)
    {
        cfi.FontWeight = FW_BOLD;
    }
    else
    {
        cfi.FontWeight = FW_NORMAL;
    }

    BOOL r2 = SetCurrentConsoleFontEx(hConsoleOutput, maxinumWindow, &cfi);
    return r2;
}

//GetConsoleScreenBufferInfo
//https://docs.microsoft.com/en-us/windows/console/getconsolescreenbufferinfo
BOOL GET_CONSOLE_BUFFER_SIZE(HANDLE hConsoleOutput, SHORT * width, SHORT * height)
{
    CONSOLE_SCREEN_BUFFER_INFO csbi;
    BOOL r = GetConsoleScreenBufferInfo(hConsoleOutput, &csbi);

    *width = csbi.dwSize.X;
    *height = csbi.dwSize.Y;
    return r;
}

//SetConsoleScreenBufferSize
//https://docs.microsoft.com/en-us/windows/console/setconsolescreenbuffersize
BOOL SET_CONSOLE_BUFFER_SIZE(HANDLE hConsoleOutput, SHORT width, SHORT height)
{
    COORD size;
    size.X = width;
    size.Y = height;

    BOOL r = SetConsoleScreenBufferSize(hConsoleOutput, size);
    return r;
}

//GetConsoleScreenBufferInfo
//https://docs.microsoft.com/en-us/windows/console/getconsolescreenbufferinfo
BOOL GET_CONSOLE_WINDOW_SIZE(HANDLE hConsoleOutput, SHORT * width, SHORT * height)
{
    CONSOLE_SCREEN_BUFFER_INFO csbi;
    BOOL r = GetConsoleScreenBufferInfo(hConsoleOutput, &csbi);
    *width = csbi.srWindow.Right + 1;
    *height = csbi.srWindow.Bottom + 1;

    return r;
}

//SetConsoleWindowInfo
//https://docs.microsoft.com/en-us/windows/console/setconsolewindowinfo
BOOL SET_CONSOLE_WINDOW_SIZE(HANDLE hConsoleOutput, SHORT width, SHORT height)
{
    SMALL_RECT size;
    size.Left = 0;
    size.Top = 0;
    size.Right = width - 1;
    size.Bottom = height - 1;

    BOOL r = SetConsoleWindowInfo(hConsoleOutput, TRUE, &size);
    return r;
}

//GetConsoleScreenBufferInfoEx
//https://docs.microsoft.com/en-us/windows/console/getconsolescreenbufferinfoex
BOOL GET_CONSOLE_PALETTE(HANDLE hConsoleOutput, DWORD index, DWORD * r, DWORD * g, DWORD * b)
{
    CONSOLE_SCREEN_BUFFER_INFOEX csbi;
    csbi.cbSize = sizeof(CONSOLE_SCREEN_BUFFER_INFOEX);

    BOOL result = GetConsoleScreenBufferInfoEx(hConsoleOutput, &csbi);

    DWORD color = csbi.ColorTable[index];
    *r = color & 0x0000FF;
    *g = (color & 0x00FF00) >> 8;
    *b = (color & 0xFF0000) >> 16;

    return result;
}

//GetConsoleScreenBufferInfoEx
//https://docs.microsoft.com/en-us/windows/console/getconsolescreenbufferinfoex
//SetConsoleScreenBufferInfoEx
//https://docs.microsoft.com/en-us/windows/console/setconsolescreenbufferinfoex
BOOL SET_CONSOLE_PALETTE(HANDLE hConsoleOutput, DWORD index, DWORD r, DWORD g, DWORD b)
{
    CONSOLE_SCREEN_BUFFER_INFOEX csbi;
    csbi.cbSize = sizeof(CONSOLE_SCREEN_BUFFER_INFOEX);
    BOOL r1 = GetConsoleScreenBufferInfoEx(hConsoleOutput, &csbi);
    if (r1 == FALSE)
        return FALSE;

    csbi.ColorTable[index] = r + (g << 8) + (b << 16);

    BOOL r2 = SetConsoleScreenBufferInfoEx(hConsoleOutput, &csbi);
    return r2;
}

//GetConsoleTitle
//https://docs.microsoft.com/en-us/windows/console/getconsoletitle
BOOL GET_CONSOLE_TITLE(WCHAR title[MAX_PATH], DWORD * length)
{
    DWORD r = GetConsoleTitle(title, MAX_PATH);
    *length = r;

    if (r == 0)
        return FALSE;
    else
        return TRUE;
}

//SetConsoleTitle
//https://docs.microsoft.com/en-us/windows/console/setconsoletitle
BOOL SET_CONSOLE_TITLE(WCHAR * title)
{
    BOOL r = SetConsoleTitle(title);
    return r;
}

//GetConsoleCP
//https://docs.microsoft.com/en-us/windows/console/getconsolecp
BOOL GET_CONSOLE_CP(UINT * inputCP)
{
    *inputCP = GetConsoleCP();
    if (*inputCP == 0)
        return FALSE;
    else
        return TRUE;
}

//SetConsoleCP
//https://docs.microsoft.com/en-us/windows/console/setconsolecp
BOOL SET_CONSOLE_CP(UINT inputCP)
{
    BOOL r = SetConsoleCP(inputCP);
    return r;
}

//GetConsoleOutputCP
//https://docs.microsoft.com/en-us/windows/console/getconsoleoutputcp
BOOL GET_CONSOLE_OUTPUT_CP(UINT * outputCP)
{
    *outputCP = GetConsoleOutputCP();
    if (*outputCP == 0)
        return FALSE;
    else
        return TRUE;
}

//SetConsoleOutputCP
//https://docs.microsoft.com/en-us/windows/console/setconsoleoutputcp
BOOL SET_CONSOLE_OUTPUT_CP(UINT outputCP)
{
    BOOL r = SetConsoleOutputCP(outputCP);
    return r;
}

//GetConsoleScreenBufferInfo
//https://docs.microsoft.com/en-us/windows/console/getconsolescreenbufferinfo
BOOL GET_CONSOLE_ATTRIBUTE(HANDLE hConsoleOutput, WORD * foreColor, WORD * backColor)
{
    CONSOLE_SCREEN_BUFFER_INFO csbi;
    BOOL r = GetConsoleScreenBufferInfo(hConsoleOutput, &csbi);
    *foreColor = csbi.wAttributes & 0x000F;
    *backColor = (csbi.wAttributes & 0x00F0) / 16;
    return r;
}

//SetConsoleTextAttribute
//https://docs.microsoft.com/en-us/windows/console/setconsoletextattribute
BOOL SET_CONSOLE_ATTRIBUTE(HANDLE hConsoleOutput, WORD foreColor, WORD backColor)
{
    WORD color = backColor * 16 + foreColor;
    BOOL r = SetConsoleTextAttribute(hConsoleOutput, color);
    return r;
}

//GetConsoleCursorInfo
//https://docs.microsoft.com/en-us/windows/console/getconsolecursorinfo
BOOL GET_CONSOLE_CURSOR_INFO(HANDLE hConsoleOutput, BOOL * cursorVisible, DWORD * cursorSize)
{
    CONSOLE_CURSOR_INFO cci;

    BOOL r = GetConsoleCursorInfo(hConsoleOutput, &cci);
    *cursorVisible = cci.bVisible;
    *cursorSize = cci.dwSize;

    return r;
}

//SetConsoleCursorInfo
//https://docs.microsoft.com/en-us/windows/console/setconsolecursorinfo
BOOL SET_CONSOLE_CURSOR_INFO(HANDLE hConsoleOutput, BOOL cursorVisible, DWORD cursorSize)
{
    CONSOLE_CURSOR_INFO cci;
    cci.bVisible = cursorVisible;
    cci.dwSize = cursorSize;

    BOOL r = SetConsoleCursorInfo(hConsoleOutput, &cci);
    return r;
}

//GetConsoleScreenBufferInfo
//https://docs.microsoft.com/en-us/windows/console/getconsolescreenbufferinfo
BOOL GET_CONSOLE_CURSOR_POS(HANDLE hConsoleOutput, SHORT * x, SHORT * y)
{
    CONSOLE_SCREEN_BUFFER_INFO csbi;
    BOOL r = GetConsoleScreenBufferInfo(hConsoleOutput, &csbi);

    *x = csbi.dwCursorPosition.X;
    *y = csbi.dwCursorPosition.Y;
    return r;
}

//SetConsoleCursorPosition
//https://docs.microsoft.com/en-us/windows/console/setconsolecursorposition
BOOL SET_CONSOLE_CURSOR_POS(HANDLE hConsoleOutput, SHORT x, SHORT y)
{
    COORD position;
    position.X = x;
    position.Y = y;

    BOOL r = SetConsoleCursorPosition(hConsoleOutput, position);
    return r;
}

//ReadConsole
//https://docs.microsoft.com/en-us/windows/console/readconsole
BOOL READ_CONSOLE(HANDLE hConsoleInput, WCHAR * str, DWORD numberOfCharToRead, DWORD * read)
{
    BOOL r = ReadConsole(hConsoleInput, str, numberOfCharToRead, read, NULL);
    return r;
}

//GetNumberOfConsoleInputEvents
//https://docs.microsoft.com/en-us/windows/console/getnumberofconsoleinputevents
//ReadConsoleInput
//https://docs.microsoft.com/en-us/windows/console/readconsoleinput
BOOL READ_CONSOLE_INPUT(HANDLE hConsoleInput, SHORT * mousePosX, SHORT * mousePosY)
{
    DWORD eventNumber = 0;
    BOOL r = GetNumberOfConsoleInputEvents(hConsoleInput, &eventNumber);
    if (r == FALSE)
        return FALSE;
    if (eventNumber == 0)
        return FALSE;

    INPUT_RECORD inputBuf[64];
    BOOL r2 = ReadConsoleInput(hConsoleInput, inputBuf, eventNumber, &eventNumber);
    if (r2 == FALSE)
        return FALSE;

    BOOL mouse = FALSE;
    for (DWORD i = 0; i < eventNumber; i++)
    {
        switch (inputBuf[i].EventType)
        {
        case FOCUS_EVENT:
            //Don't care
            break;
        case MENU_EVENT:
            //Don't care
            break;
        case WINDOW_BUFFER_SIZE_EVENT:
            //Don't care
            break;
        case MOUSE_EVENT:
            //Dont' care
            //inputBuf[i].Event.MouseEvent.dwButtonState
            //inputBuf[i].Event.MouseEvent.dwControlKeyState
            //inputBuf[i].Event.MouseEvent.dwEventFlags
            *mousePosX = inputBuf[i].Event.MouseEvent.dwMousePosition.X;
            *mousePosY = inputBuf[i].Event.MouseEvent.dwMousePosition.Y;
            mouse = TRUE;
            break;
        case KEY_EVENT:
            //Don't care
            break;
        }
    }

    return mouse;
}

//FlushConsoleInputBuffer
//https://docs.microsoft.com/en-us/windows/console/flushconsoleinputbuffer
BOOL FLUSH_CONSOLE_INPUT(HANDLE hConsoleInput)
{
    BOOL r = FlushConsoleInputBuffer(hConsoleInput);
    return r;
}

//WriteConsole
//https://docs.microsoft.com/en-us/windows/console/writeconsole
BOOL WRITE_CONSOLE(HANDLE hConsoleOutput, wchar_t* charBuffer, DWORD numberOfCharsToWrite)
{
    DWORD written = 0;
    BOOL r = WriteConsole(hConsoleOutput, charBuffer, numberOfCharsToWrite, &written, NULL);
    return r;
}

//WriteConsoleOutput
//https://docs.microsoft.com/en-us/windows/console/writeconsoleoutput
BOOL WRITE_CONSOLE_OUTPUT(HANDLE hConsoleOutput, CHAR_INFO * charInfos, SHORT x, SHORT y, SHORT width, SHORT height)
{
    COORD size;
    size.X = width;
    size.Y = height;

    COORD coord;
    coord.X = 0;
    coord.Y = 0;

    SMALL_RECT smallRect;
    smallRect.Left = x;
    smallRect.Top = y;
    smallRect.Right = x + width - 1;
    smallRect.Bottom = y + height - 1;

    BOOL r = WriteConsoleOutput(hConsoleOutput, charInfos, size, coord, &smallRect);
    return r;
}

//WriteConsoleOutputCharacter
//https://docs.microsoft.com/en-us/windows/console/writeconsoleoutputcharacter
BOOL WRITE_CONSOLE_OUTPUT_CHAR(HANDLE hConsoleOutput, WCHAR * charBuffer, DWORD numberOfCharsToWrite, SHORT x, SHORT y)
{
    DWORD written = 0;
    COORD coord;
    coord.X = x;
    coord.Y = y;
    BOOL r = WriteConsoleOutputCharacter(hConsoleOutput, charBuffer, numberOfCharsToWrite, coord, &written);
    return r;
}

//WriteConsoleOutputAttribute
//https://docs.microsoft.com/en-us/windows/console/writeconsoleoutputattribute
BOOL WRITE_CONSOLE_OUTPUT_ATTRIBUTE(HANDLE hConsoleOutput, WORD * colors, DWORD numberOfColorsToWrite, SHORT x, SHORT y)
{
    DWORD written = 0;
    COORD coord;
    coord.X = x;
    coord.Y = y;
    BOOL r = WriteConsoleOutputAttribute(hConsoleOutput, colors, numberOfColorsToWrite, coord, &written);
    return r;
}

//FillConsoleOutputCharacter
//https://docs.microsoft.com/en-us/windows/console/fillconsoleoutputcharacter
BOOL FILL_CONSOLE_OUTPUT_CHAR(HANDLE hConsoleOutput, WCHAR c, DWORD width, SHORT x, SHORT y)
{
    DWORD written = 0;
    COORD coord;
    coord.X = x;
    coord.Y = y;
    BOOL r = FillConsoleOutputCharacter(hConsoleOutput, c, width, coord, &written);
    return r;
}

//FillConsoleOutputAttribute
//https://docs.microsoft.com/en-us/windows/console/fillconsoleoutputattribute
BOOL FILL_CONSOLE_OUTPUT_ATTRIBUTE(HANDLE hConsoleOutput, WORD color, DWORD width, SHORT x, SHORT y)
{
    DWORD written = 0;
    COORD coord;
    coord.X = x;
    coord.Y = y;
    BOOL r = FillConsoleOutputAttribute(hConsoleOutput, color, width, coord, &written);
    return r;
}

//GetLargestConsoleWindowSize
//https://docs.microsoft.com/en-us/windows/console/getlargestconsolewindowsize
BOOL GET_LARGEST_CONSOLE_WINDOW_SIZE(HANDLE hConsoleOutput, SHORT * width, SHORT * height)
{
    COORD coord = GetLargestConsoleWindowSize(hConsoleOutput);
    if (coord.X == 0 && coord.Y == 0)
        return FALSE;
    *width = coord.X;
    *height = coord.Y;
    return TRUE;
}

//SetConsoleCtrlHandler
//https://docs.microsoft.com/en-us/windows/console/setconsolectrlhandler
BOOL SET_CONSOLE_CTRL_HANDLER(BOOL disable)
{
    BOOL r = SetConsoleCtrlHandler(NULL, disable);
    return r;
}

#pragma endregion

#pragma region Other

//GetAsyncKeyState
//https://docs.microsoft.com/en-us/windows/desktop/api/winuser/nf-winuser-getasynckeystate
BOOL GET_KEY(int key)
{
    SHORT r = GetAsyncKeyState(key);
    return (r & 0x8000) != 0;
}

//QueryPerformanceFrequency
//https://msdn.microsoft.com/en-us/library/windows/desktop/ms644905(v=vs.85).aspx
//QueryPerformanceCounter
//https://msdn.microsoft.com/en-us/library/windows/desktop/ms644904(v=vs.85).aspx
BOOL START_TIMING(LONGLONG * freq, LONGLONG * start)
{
    LARGE_INTEGER _freq, _start;

    BOOL r = QueryPerformanceFrequency(&_freq);
    if (r == FALSE)
        return FALSE;

    BOOL r2 = QueryPerformanceCounter(&_start);
    if (r2 == FALSE)
        return FALSE;

    *freq = _freq.QuadPart;
    *start = _start.QuadPart;

    return TRUE;
}

//QueryPerformanceCounter
//https://msdn.microsoft.com/en-us/library/windows/desktop/ms644904(v=vs.85).aspx
BOOL END_TIMING(LONGLONG freq, LONGLONG start, LONGLONG * timeCost)
{
    LARGE_INTEGER _end;
    BOOL r = QueryPerformanceCounter(&_end);
    if (r == FALSE)
        return FALSE;

    *timeCost = (_end.QuadPart - start) * 1000 / freq;

    return TRUE;
}

//Sleep
//https://docs.microsoft.com/en-us/windows/desktop/api/synchapi/nf-synchapi-sleep
BOOL SLEEP(DWORD milliSeconds)
{
    Sleep(milliSeconds);
    return TRUE;
}

//CloseHandle
//https://docs.microsoft.com/zh-cn/windows/desktop/api/handleapi/nf-handleapi-closehandle
BOOL CLOSE_HANDLE(HANDLE handle)
{
    BOOL r = CloseHandle(handle);
    return r;
}

#pragma endregion