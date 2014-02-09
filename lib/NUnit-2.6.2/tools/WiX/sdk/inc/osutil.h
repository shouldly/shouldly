#pragma once
//-------------------------------------------------------------------------------------------------
// <copyright file="osutil.h" company="Microsoft">
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
//    Operating system helper functions.
// </summary>
//-------------------------------------------------------------------------------------------------

#ifdef __cplusplus
extern "C" {
#endif

enum OS_VERSION
{
    OS_VERSION_UNKNOWN,
    OS_VERSION_WINNT,
    OS_VERSION_WIN2000,
    OS_VERSION_WINXP,
    OS_VERSION_WIN2003,
    OS_VERSION_VISTA,
    OS_VERSION_WIN2008,
    OS_VERSION_WIN7,
    OS_VERSION_WIN2008_R2,
    OS_VERSION_FUTURE
};

void DAPI OsGetVersion(
    __out OS_VERSION* pVersion,
    __out DWORD* pdwServicePack
    );
HRESULT OsIsRunningPrivileged(
    __out BOOL* pfPrivileged
    );

#ifdef __cplusplus
}
#endif
