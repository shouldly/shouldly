//-------------------------------------------------------------------------------------------------
// <copyright file="gdiputil.h" company="Microsoft">
//    Copyright (c) Microsoft Corporation.  All rights reserved.
//    
//    The use and distribution terms for this software are covered by the
//    Common Public License 1.0 (http://opensource.org/licenses/cpl1.0.php)
//    which can be found in the file CPL.TXT at the root of this distribution.
//    By using this software in any fashion, you are agreeing to be bound by
//    the terms of this license.
//    
//    You must not remove this notice, or any other, from this software.
// </copyright>
// 
// <summary>
//    GDI+ helper functions.
// </summary>
//-------------------------------------------------------------------------------------------------

#pragma once

#define ExitOnGdipFailure(g, x, s) { x = GdipHresultFromStatus(g); if (FAILED(x)) { Dutil_RootFailure(__FILE__, __LINE__, x); ExitTrace(x, s); goto LExit; } }
#define ExitOnGdipFailure1(g, x, f, s) { x = GdipHresultFromStatus(g); if (FAILED(x)) { Dutil_RootFailure(__FILE__, __LINE__, x); ExitTrace1(x, f, s); goto LExit; } }
#define ExitOnGdipFailure2(g, x, f, s, t) { x = GdipHresultFromStatus(g); if (FAILED(x)) { Dutil_RootFailure(__FILE__, __LINE__, x); ExitTrace2(x, f, s, t); goto LExit; } }

#ifdef __cplusplus
extern "C" {
#endif

HRESULT DAPI GdipBitmapFromResource(
    __in_opt HINSTANCE hinst,
    __in_z LPCSTR szId,
    __out Gdiplus::Bitmap **ppBitmap
    );

HRESULT DAPI GdipBitmapFromFile(
    __in_z LPCWSTR wzFileName,
    __out Gdiplus::Bitmap **ppBitmap
    );

HRESULT DAPI GdipHresultFromStatus(
    __in Gdiplus::Status gs
    );

#ifdef __cplusplus
}
#endif
