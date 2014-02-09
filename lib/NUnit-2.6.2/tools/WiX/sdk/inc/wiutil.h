#pragma once
//-------------------------------------------------------------------------------------------------
// <copyright file="wiutil.h" company="Microsoft">
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
//    Header for Windows Installer helper functions.
// </summary>
//-------------------------------------------------------------------------------------------------

#ifdef __cplusplus
extern "C" {
#endif

#define MAX_DARWIN_KEY 73
#define MAX_DARWIN_COLUMN 255

#define ReleaseMsi(h) if (h) { ::MsiCloseHandle(h); }
#define ReleaseNullMsi(h) if (h) { ::MsiCloseHandle(h); h = NULL; }

HRESULT DAPI WiuGetComponentPath(
    __in_z LPCWSTR wzProductCode,
    __in_z LPCWSTR wzComponentId,
    __out LPWSTR* ppwzPath
    );

HRESULT DAPI WiuGetProductInfo(
    __in_z LPCWSTR wzProductCode,
    __in_z LPCWSTR wzProperty,
    __out LPWSTR* ppwzValue
    );

HRESULT DAPI WiuGetProductProperty(
    __in MSIHANDLE hProduct,
    __in_z LPCWSTR wzProperty,
    __out LPWSTR* ppwzValue
    );

#ifdef __cplusplus
}
#endif
